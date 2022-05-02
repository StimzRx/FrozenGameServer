using System.Collections;
using System.Collections.Generic;

using Core.Scripts.Helpers;
using Core.Scripts.Networking;
using Core.Scripts.Registries;

using KableNet.Common;
using KableNet.Math;
using KableNet.Server;

using UnityEngine;

using PacketRegistry = Core.Scripts.Networking.Registries.PacketRegistry;

namespace Core.Scripts.Singletons
{
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
        private static void OnKableConnection( KableConnection connection )
        {
            connection.PacketReadyEvent += OnClientPacketReady;
            Debug.LogError( $"[GameServer][OnKableConnection] New Connection..." );
            NetPlayer netPlr = new NetPlayer( connection );
            lock ( PlayerList )
            {
                PlayerList.Add( netPlr.NetId, netPlr );
            }
            
            GameObject plrObj = GameObject.Instantiate( PrefabRegistry.GetPrefab( new Identifier( "core", "entity_player" ) ) );
            ServerEntity servEnt = plrObj.GetComponent<ServerEntity>( );
            if ( servEnt == null )
            {
                Debug.LogError( "entity_player has no ServerEntity component!" );
            }

            servEnt.Initialize( netPlr.NetId );
            
            lock ( EntityList )
            {
                EntityList.Add( netPlr.NetId, servEnt );
            }
        }
        private static void OnClientPacketReady( KablePacket packet, KableConnection source )
        {
            Debug.LogError( $"[GameServer] Packet ready with '{ packet.Count }' bytes." );

            PacketRegistry.HandlePacket( packet, source );
        }
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
            lock ( PlayerList )
            {
                foreach (KeyValuePair<NetId,NetPlayer> i in PlayerList)
                {
                    if ( i.Value.ConnectionMatches( conn ) )
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
            lock ( PlayerList )
            {
                foreach (KeyValuePair<NetId, NetPlayer> i in PlayerList)
                {
                    if ( i.Key == searchNetId )
                    {
                        return i.Value;
                    }
                }
            }

            return null;
        }
        
        internal static void UnityTick( )
        {
            lock ( PlayerList )
            {
                foreach (KeyValuePair<NetId, NetPlayer> pair in PlayerList)
                {
                    pair.Value.KableConnection.ProcessBuffer( );
                }
            }
        }
        internal static void Shutdown( )
        {
            lock ( PlayerList )
            {
                foreach (KeyValuePair<NetId, NetPlayer> pair in PlayerList)
                {
                    try
                    {
                        pair.Value.KableConnection.Close( );
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static int serverPort { get; private set; }
        public static bool serverRunning { get; private set; } = false;

        readonly private static Dictionary<NetId, NetPlayer> PlayerList = new Dictionary<NetId, NetPlayer>( );
        readonly private static Dictionary<NetId, ServerEntity> EntityList = new Dictionary<NetId, ServerEntity>( );

        private static KableServer _kableServer;
    }
}
