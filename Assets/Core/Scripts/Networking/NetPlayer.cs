using System;
using System.Net.Sockets;

using Core.Scripts.Entities;
using Core.Scripts.Events.Entity;
using Core.Scripts.Networking.Packets.Core;

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

            ServerEntityEvents.EntitySpawned += OnEntitySpawn;

            this.NetId = NetId.Generate( );
        }
        private void OnEntitySpawn( ServerEntity entity, Vector3 position )
        {
            if ( entity is ServerPlayer )
            {
                KableConnection.SendPacketTCPAsync( new SpawnEntityPacket( new Identifier( "core", "player_entity" ), entity.NetId ).GetAsPacket( ) ).Wait( );
            }
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
