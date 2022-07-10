using Core.Scripts.Attributes;
using Core.Scripts.Networking.Packets;
using KableNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Networking.Packets.Core.Inventory
{
    [NetPacket( "core.packet", "setup_inventory" )]
    public class SetupInventoryPacket : PacketWrapper
    {
        public int SlotsX { get; set; }
        public int SlotsY { get; set; }
        public bool IsRemote { get; set; }

        protected override void ToPacket( KablePacket p )
        {
            p.Write( SlotsX );
            p.Write( SlotsY );
            p.Write( IsRemote );
        }
    }
}
