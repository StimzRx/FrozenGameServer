using Core.Scripts.Attributes;

using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Packets.Core
{
    [NetPacket( "core.packet", "destroy_entity")]
    public class DestroyEntityPacket : PacketWrapper
    {
        public DestroyEntityPacket( NetId netId )
        {
            _netId = netId;
        }

        private NetId _netId;
        
        protected override void ToPacket( KablePacket p )
        {
            p.Write( _netId );
        }
    }
}
