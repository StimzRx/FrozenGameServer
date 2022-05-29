using Core.Scripts.Networking;

using KableNet.Math;

namespace Core.Scripts.Entities.Core
{
    public class PlayerEntity : GameEntity
    {
        public PlayerEntity( NetPlayer netPlayer, EntityWrapper wrapper, NetId netId ) : base( wrapper, netId )
        {
            this.NetPlayer = netPlayer;
        }
    
        public NetPlayer NetPlayer { get; }
    }
}
