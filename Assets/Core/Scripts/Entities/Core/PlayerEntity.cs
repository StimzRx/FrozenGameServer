using Core.Scripts.Attributes;
using Core.Scripts.Networking;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities.Core
{
    [EntityType("core.entity", "player")]
    public class PlayerEntity : GameEntity
    {
        public PlayerEntity( EntityWrapper wrapper, NetId netId ) : base( wrapper, netId )
        {
            Debug.Log( "PlayerEntity created..." );
        }

        override internal void ServerSpawned( )
        {
            base.ServerSpawned( );
        }

        override internal void ServerTick( float deltaTime )
        {
            base.ServerTick( deltaTime );
        }

        public NetPlayer NetPlayer { get; }
    }
}
