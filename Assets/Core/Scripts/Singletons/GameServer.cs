using System.Collections;
using System.Collections.Generic;

using Core.Scripts.Networking;
using Core.Scripts.Networking.Registries;

using KableNet.Common;
using KableNet.Math;
using KableNet.Server;

using UnityEngine;

namespace Core.Scripts.Singletons
{
    public static class GameServer
    {
        /// <summary>
        /// Initializes and Starts the GameServer
        /// </summary>
        /// <param name="port"></param>
        public static void Init( int port )
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
            lock ( _playerList )
            {
                _playerList.Add( netPlr.NetId, netPlr );
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
            foreach (KeyValuePair<NetId,NetPlayer> i in _playerList)
            {
                if ( i.Value.ConnectionMatches( conn ) )
                {
                    return i.Value;
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
            foreach (KeyValuePair<NetId,NetPlayer> i in _playerList)
            {
                if ( i.Key == searchNetId )
                {
                    return i.Value;
                }
            }

            return null;
        }
        
        internal static void UnityTick( )
        {
            foreach (KeyValuePair<NetId,NetPlayer> pair in _playerList)
            {
                pair.Value.KableConnection.ProcessBuffer(  );
            }
        }
        internal static void Shutdown( )
        {
            foreach (KeyValuePair<NetId,NetPlayer> pair in _playerList)
            {
                try
                {
                    pair.Value.KableConnection.Close(  );
                }
                catch {  }
            }
        }

        public static int serverPort { get; private set; }
        public static bool serverRunning { get; private set; } = false;

        private static Dictionary<NetId, NetPlayer> _playerList = new Dictionary<NetId, NetPlayer>( );

        private static KableServer _kableServer;
    }
}
