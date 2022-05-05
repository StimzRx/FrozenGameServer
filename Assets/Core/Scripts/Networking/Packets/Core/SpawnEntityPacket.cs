using KableNet.Common;
using KableNet.Math;

using UnityEditor.Build.Content;

namespace Core.Scripts.Networking.Packets.Core
{
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

        public override Identifier identifier => new Identifier( "core", "spawn_entity_packet" );
    }
}
