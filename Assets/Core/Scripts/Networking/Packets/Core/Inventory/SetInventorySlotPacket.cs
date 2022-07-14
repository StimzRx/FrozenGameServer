using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Core.Scripts.Inventories;
using Assets.Core.Scripts.Serialization;

using Core.Scripts.Attributes;
using Core.Scripts.Networking.Packets;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Assets.Core.Scripts.Networking.Packets.Core.Inventory
{
    [NetPacket( "core.packet", "set_inventory_slot" )]
    public class SetInventorySlotPacket : PacketWrapper
    {
        public Vec2i Location { get; set; }
        public ItemStack Stack { get; set; }

        protected override void ToPacket( KablePacket p )
        {
            p.Write( Location );
            SerialData serialData = Stack.ToSerial( );
            byte[ ] serialBuffer = serialData.GetRaw( );
            p.Write( serialBuffer.Length );
            p.Write( serialBuffer );
        }
    }
}
