using System;
using System.Net.Sockets;
using System.Threading.Tasks;

using Core.Scripts.Entities;
using Core.Scripts.Entities.Core;
using Core.Scripts.Events.Entity;
using Core.Scripts.Networking.Packets;
using Core.Scripts.Networking.Packets.Core;
using Core.Scripts.Singletons;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking
{
    /// <summary>
    /// Represents a 'client' of our server. Should be paired with a GamePlayer type, which handles our in-world code
    /// </summary>
    public class NetPlayer
    {
        public NetPlayer( KableConnection connection )
        {
            KableConnection = connection;
            KableConnection.ConnectedEvent += OnConnected;
            KableConnection.ConnectErroredEvent += OnConnectFailure;
            KableConnection.ConnectionErroredEvent += OnKableError;

            NetId = NetId.Generate( );

            // Game event registrations
            ServerEntityEvents.EntitySpawnEvent += OnEntitySpawn;
            ServerEntityEvents.EntityDestroyedEvent += OnEntityDestroyed;
        }

        // ------------ Subscribed Events ------------
        private void OnEntitySpawn( GameEntity entity, Vector3 position )
        {
            if ( entity is PlayerEntity )
            {
                if ( !entity.NetId.Equals( NetId ) )
                {
                    SendTcp( new SpawnEntityPacket( new Identifier( "core", "player_entity" ), entity.NetId ) );
                }
            }
        }
        private void OnEntityDestroyed( GameEntity entity )
        {
            // TODO: Implement THIS
            
            // return for now.
            return;
            
            if ( entity is PlayerEntity )
            {
                if ( !entity.NetId.Equals( this.NetId ) )
                {
                    
                }
            }
        }
        
        // -------------------------------------------

        public void SendTcp( PacketWrapper packet )
        {
            SendTcpAsync( packet ).Wait( );
        }
        public async Task SendTcpAsync( PacketWrapper packet )
        {
            await KableConnection.SendPacketTcpAsync( packet.GetAsPacket( ) );
        }

        public void SendUdp( PacketWrapper packet )
        {
            SendUdpAsync( packet ).Wait( );
        }
        public async Task SendUdpAsync( PacketWrapper packet )
        {
            await KableConnection.SendPacketUdpAsync( packet.GetAsPacket( ) );
        }
        
        /// <summary>
        /// Connection to client has been established
        /// </summary>
        /// <param name="source"></param>
        private void OnConnected( KableConnection source )
        {
            Debug.LogError( $"[NetPlayer.{ this.NetId }] Connected!" );
        }
        
        /// <summary>
        /// KableConnection encountered a runtime error during read/write to network stream
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="source"></param>
        private void OnKableError( Exception ex, KableConnection source )
        {
            Debug.LogError( $"[NetPlayer.{ this.NetId }][KableError]{ ex }" );
            Dispose( );
            GameServer.DestroyEntity( GameServer.FindGameEntity( NetId ) );
        }
        
        /// <summary>
        /// KableConnection failed to be established
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        private void OnConnectFailure( SocketException exception, KableConnection source )
        {
            Debug.LogError( $"[NetPlayer.{ this.NetId }][Connect Failure]{ exception }" );
        }

        /// <summary>
        /// Does the given KableConnection match this NetPlayer's KableConnection
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool DoesKableConnectionMatch( KableConnection conn )
        {
            return this.KableConnection == conn;
        }

        public void Dispose( )
        {
            Destroyed = true;
            
            KableConnection.ConnectedEvent -= OnConnected;
            KableConnection.ConnectErroredEvent -= OnConnectFailure;
            KableConnection.ConnectionErroredEvent -= OnKableError;
            
            ServerEntityEvents.EntitySpawnEvent -= OnEntitySpawn;
            ServerEntityEvents.EntityDestroyedEvent -= OnEntityDestroyed;
        }

        internal KableConnection KableConnection { get; private set; }
        
        public NetId NetId { get; protected set; }

        public bool Destroyed { get; private set; } = false;
    }
}
