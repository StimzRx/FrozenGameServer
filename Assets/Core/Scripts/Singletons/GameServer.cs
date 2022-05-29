using System.Collections;
using System.Collections.Generic;

using Core.Scripts.Entities;
using Core.Scripts.Entities.Core;
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
            _kableServer.StartListening(  );

            Debug.Log( "Started server!" );
        }
        
        /// <summary>
        /// Triggered when a new client connects to this server
        /// </summary>
        /// <param name="connection"></param>
        private static void OnKableConnection( KableConnection connection )
        {
            connection.PacketReadyEvent += OnClientPacketReady;
            Debug.LogError( $"[GameServer][OnKableConnection] New Connection..." );
            NetPlayer netPlr = new NetPlayer( connection );
            lock ( NetClients )
            {
                NetClients.Add( netPlr.NetId, netPlr );
            }
            
            GameObject plrObj = GameObject.Instantiate( PrefabRegistry.GetPrefab( new Identifier( "core", "entity_player" ) ) );
            EntityWrapper wrapper = plrObj.GetComponent<EntityWrapper>( );
            
            if ( wrapper == null )
            {
                Debug.LogError( "entity_player has no EntityWrapper component!" );
            }
            
            PlayerEntity servPlr = new PlayerEntity( netPlr, wrapper, netPlr.NetId );
            
            lock ( WorldEntities )
            {
                WorldEntities.Add( netPlr.NetId, servPlr );
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
            
            PacketRegistry.TriggerHandler( packetIdent, packet, source );
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
        /// Called whenever Unity has a game tick (every frame)
        /// </summary>
        internal static void UnityTick( )
        {
            lock ( NetClients )
            {
                foreach (KeyValuePair<NetId, NetPlayer> pair in NetClients)
                {
                    pair.Value.KableConnection.ProcessBuffer( );
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
