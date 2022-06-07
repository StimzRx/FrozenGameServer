using System;
using System.Net.Sockets;

using Core.Scripts.Entities;
using Core.Scripts.Entities.Core;
using Core.Scripts.Events.Entity;
using Core.Scripts.Networking.Packets.Core;

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

            this.NetId = NetId.Generate( );

            // Game event registrations
            ServerEntityEvents.EntitySpawnEvent += OnEntitySpawn;
            ServerEntityEvents.EntityDestroyedEvent += OnEntityDestroyed;
        }

        // ------------ Subscribed Events ------------
        private void OnEntitySpawn( GameEntity entity, Vector3 position )
        {
            if ( entity is PlayerEntity )
            {
                if ( entity.NetId.Equals( this.NetId ) )
                {
                    
                }
                else
                {
                    //KableConnection.SendPacketTCPAsync( new SpawnEntityPacket( new Identifier( "core", "player_entity" ), entity.NetId ).GetAsPacket( ) ).Wait( );
                }
            }
        }
        private void OnEntityDestroyed( GameEntity entity )
        {
            if ( entity is PlayerEntity )
            {
                if ( entity.NetId.Equals( this.NetId ) )
                {
                    
                }
                else
                {
                    //KableConnection.SendPacketTCPAsync( new SpawnEntityPacket( new Identifier( "core", "entity_player" ), entity.NetId ).GetAsPacket( ) ).Wait( );
                }
            }
        }
        
        // -------------------------------------------

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

        internal KableConnection KableConnection { get; private set; }
        
        public NetId NetId { get; protected set; }
    }
}
