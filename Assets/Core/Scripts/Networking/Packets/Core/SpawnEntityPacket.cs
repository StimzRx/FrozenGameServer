using Core.Scripts.Attributes;

using KableNet.Common;
using KableNet.Math;

using UnityEditor.Build.Content;

namespace Core.Scripts.Networking.Packets.Core
{
    [NetPacket("core", "spawn_entity_packet")]
    public class SpawnEntityPacket : PacketWrapper
    {
        public SpawnEntityPacket( Identifier entityIdent, NetId netId )
        {
            this.EntityIdentifier = entityIdent;
            this.NetId = netId;
        }
        
        public Identifier EntityIdentifier { get; private set; }
        public NetId NetId { get; private set; }
        
        protected override void ToPacket( KablePacket p )
        {
            p.Write( EntityIdentifier );
            p.Write( NetId );
        }
    }
}
