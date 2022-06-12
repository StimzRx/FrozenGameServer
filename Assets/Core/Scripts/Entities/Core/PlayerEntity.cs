using Core.Scripts.Attributes;
using Core.Scripts.Networking;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities.Core
{
    [EntityType("core", "player_entity")]
    public class PlayerEntity : GameEntity
    {
        public PlayerEntity( EntityWrapper wrapper, NetId netId ) : base( wrapper, netId )
        {
            Debug.Log( "PlayerEntity created..." );
        }
    
        public NetPlayer NetPlayer { get; }
    }
}
