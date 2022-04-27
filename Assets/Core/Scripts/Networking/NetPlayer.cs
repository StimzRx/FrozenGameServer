using System;
using System.Net.Sockets;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking
{
    public class NetPlayer
    {
        public NetPlayer( KableConnection connection )
        {
            KableConnection = connection;
            KableConnection.ConnectedEvent += OnConnected;
            KableConnection.ConnectErroredEvent += OnConnectFailure;
            KableConnection.ConnectionErroredEvent += OnKableError;

            this.NetId = NetId.Generate( );
        }
        
        private void OnConnected( KableConnection source )
        {
            Debug.LogError( $"[ServerPlayer.{ this.NetId }] Connected!" );
        }
        private void OnKableError( Exception ex, KableConnection source )
        {
            Debug.LogError( $"[ServerPlayer.{ this.NetId }][KableError]{ ex }" );
        }
        private void OnConnectFailure( SocketException exception, KableConnection source )
        {
            Debug.LogError( $"[ServerPlayer.{ this.NetId }][Connect Failure]{ exception }" );
        }

        public bool ConnectionMatches( KableConnection conn )
        {
            return this.KableConnection == conn;
        }

        internal KableConnection KableConnection { get; private set; }
        public NetId NetId { get; protected set; }
    }
}
