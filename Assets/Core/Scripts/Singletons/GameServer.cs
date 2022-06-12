using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Core.Scripts.Entities;
using Core.Scripts.Entities.Core;
using Core.Scripts.Events.Entity;
using Core.Scripts.Helpers;
using Core.Scripts.Networking;
using Core.Scripts.Registries;

using KableNet.Common;
using KableNet.Math;
using KableNet.Server;

using UnityEngine;

namespace Core.Scripts.Singletons
{
    /// <summary>
    /// Represents a server's game state. Especially in regards to networking
    /// </summary>
    public static class GameServer
    {
        /// <summary>
        /// Starts the GameServer
        /// </summary>
        /// <param name="port"></param>
        public static void Start( int port )
        {
            // Check if server is already running
            if ( serverRunning )
                return;

            serverRunning = true;

            serverPort = port;

            _kableServer = new KableServer( serverPort );
            _kableServer.NewConnectionEvent += OnKableConnection;
            _kableServer.NewConnectionErroredEvent += OnKableConnectionErrored;

            try
            {
                _kableServer.StartListening(  );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"[GameServer.Start]Generic/Unhandled exception in GameServer & KableNet: {ex}" );
                throw;
            }

            Debug.Log( "Started server!" );
        }
        
        /// <summary>
        /// Triggered when a new client connects to this server
        /// </summary>
        /// <param name="connection"></param>
        private static void OnKableConnection( KableConnection connection )
        {
            connection.PacketReadyEvent += OnClientPacketReady;
            Debug.Log( $"[GameServer][OnKableConnection] New Connection..." );
            NetPlayer netPlr = new NetPlayer( connection );
            lock ( NetClients )
            {
                NetClients.Add( netPlr.NetId, netPlr );
            }
        }
        
        /// <summary>
        /// Called whenever a KableConnection has a packet that is ready to be processed
        /// </summary>
        /// <param name="packet">Unread KablePacket</param>
        /// <param name="source">Source KableConnection</param>
        private static void OnClientPacketReady( KablePacket packet, KableConnection source )
        {
            Debug.LogError( $"[GameServer] Packet ready with '{ packet.Count }' bytes." );

            Identifier packetIdent = packet.ReadIdentifier( );

            try
            {
                PacketRegistry.TriggerHandler( packetIdent, packet, source );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"[GameServer.OnClientPacketReady]Exception: {ex}" );
                throw;
            }
        }
        /// <summary>
        /// Triggered when a runtime error occurs during read/write of a KableConnection
        /// </summary>
        /// <param name="errormessage"></param>
        private static void OnKableConnectionErrored( string errormessage )
        {
            Debug.LogError( $"[GameServer][OnKableConnectionErrored]{ errormessage }" );
        }

        /// <summary>
        /// Finds and returns a player that has a matching KableConnection to the one given
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static NetPlayer ToNetPlayer( KableConnection conn )
        {
            lock ( NetClients )
            {
                foreach (KeyValuePair<NetId,NetPlayer> i in NetClients)
                {
                    if ( i.Value.DoesKableConnectionMatch( conn ) )
                    {
                        return i.Value;
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// Finds and returns a player with a matching NetId to the one given
        /// </summary>
        /// <param name="searchNetId"></param>
        /// <returns></returns>
        public static NetPlayer FindNetPlayer( NetId searchNetId )
        {
            lock ( NetClients )
            {
                foreach (KeyValuePair<NetId, NetPlayer> i in NetClients)
                {
                    if ( i.Key == searchNetId )
                    {
                        return i.Value;
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// Finds a GameEntity in the world using its NetId
        /// </summary>
        /// <param name="searchNetId"></param>
        /// <returns></returns>
        public static GameEntity FindGameEntity( NetId searchNetId )
        {
            // This entire method needs to be optimized!!!
            GameEntity retEnt = null;
            
            lock ( WorldEntities )
            {
                if ( WorldEntities.ContainsKey( searchNetId ) )
                {
                    retEnt = WorldEntities.First( x => x.Key.Equals( searchNetId ) ).Value;
                }
            }

            return retEnt;
        }

        /// <summary>
        /// Generates a new instance of a registered entity type extending GameEntity that matches the given Identifier
        /// </summary>
        /// <param name="entityIdentifier"></param>
        /// <returns></returns>
        public static GameEntity SpawnEntityByIdentifier( Identifier entityIdentifier )
        {
            NetId entNetId = NetId.Generate(  );
            
            GameObject gameObj = PrefabRegistry.GetPrefab( entityIdentifier );
            if ( gameObj is null )
            {
                Debug.LogError( $"[GameServer][SpawnEntityByIdentifier]( { entityIdentifier } ); Failed to find prefab matching the entityIdentifier!" );
                return null;
            }

            GameObject spawnedObj = GameObject.Instantiate( gameObj );
            EntityWrapper entWrapper = spawnedObj.GetComponent<EntityWrapper>( );
            if ( entWrapper is null )
            {
                Debug.LogError( $"[GameServer][SpawnEntityByIdentifier]( { entityIdentifier } ); Failed to find EntityWrapper component on spawned Object!" );
                return null;
            }

            GameEntity gameEnt = EntityRegistry.CreateGameEntity( entityIdentifier, entWrapper, entNetId );

            lock ( WorldEntities )
            {
                WorldEntities.Add( gameEnt.NetId, gameEnt );
            }
            ServerEntityEvents.TriggerOnEntitySpawn( gameEnt, Vector3.zero );
            
            return gameEnt;
        }

        /// <summary>
        /// Generates a new instance of the given Entity Type
        /// </summary>
        /// <param name="entType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SpawnEntityByType<T>( ) where T : GameEntity
        {
            Identifier entIdent = EntityRegistry.GetIdentifierForGameEntity<T>( );

            
            GameEntity tmpEnt = SpawnEntityByIdentifier( entIdent );
            
            return null;
        }

        /// <summary>
        /// Destroys the given GameEntity in a way that is intended to be "clean"
        /// </summary>
        /// <param name="gameEntity"></param>
        /// <returns>If entity was in world</returns>
        public static bool DestroyEntity( GameEntity gameEntity )
        {
            if ( gameEntity is not null )
            {
                gameEntity.Destroy(  );
                
                bool foundInWorld = false;
                lock ( WorldEntities )
                {
                    foundInWorld = WorldEntities.ContainsKey( gameEntity.NetId );
                    if ( foundInWorld )
                    {
                        WorldEntities.Remove( gameEntity.NetId );
                    }
                }

                if ( foundInWorld )
                {
                    ServerEntityEvents.TriggerOnEntityDestroyed( gameEntity );
                    
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Called whenever Unity has a game tick (every frame)
        /// </summary>
        internal static void UnityTick( )
        {
            lock ( NetClients )
            {
                foreach (KeyValuePair<NetId, NetPlayer> pair in NetClients)
                {
                    try
                    {
                        pair.Value.KableConnection.ProcessBuffer( );
                    }
                    catch ( Exception ex )
                    {
                        Debug.LogError( "Critical Error in GameServer.UnityTick:"+ex.ToString(  ) );
                        NetClients.Clear(  );
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// Called when Unity is shutting down
        /// </summary>
        internal static void Shutdown( )
        {
            lock ( NetClients )
            {
                foreach (KeyValuePair<NetId, NetPlayer> pair in NetClients)
                {
                    // Close down every KableConnection client gracefully (or at least try to)
                    pair.Value.KableConnection.Close( );
                }
            }
        }

        public static int serverPort { get; private set; }
        public static bool serverRunning { get; private set; } = false;

        readonly private static Dictionary<NetId, NetPlayer> NetClients = new Dictionary<NetId, NetPlayer>( );
        readonly private static Dictionary<NetId, GameEntity> WorldEntities = new Dictionary<NetId, GameEntity>( );

        private static KableServer _kableServer;
    }
}
