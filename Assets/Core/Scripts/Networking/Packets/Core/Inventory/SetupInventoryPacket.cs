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
    [NetPacket( "core", "setup_inventory_packet" )]
    public class SetupInventoryPacket : PacketWrapper
    {

        protected override void ToPacket( KablePacket p )
        {
            throw new NotImplementedException( );
        }
    }
}
