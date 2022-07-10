using Assets.Core.Scripts.Attributes;
using Assets.Core.Scripts.Interfaces;
using Assets.Core.Scripts.Registries;
using Assets.Core.Scripts.Serialization;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    [ItemType("core.items", "empty")]
    public class Item : Serializable<Item>
    {
        public string Name { get; protected set; }
        public int MaxStackSize { get; protected set; }
        public int Weight { get; protected set; }

        public SerialData ToSerial( )
        {
            SerialData data = new SerialData( );
            data.Write( ItemRegistry.GetIdentifierForItem( this ) );
            return data;
        }

        public static Item FromSerial(SerialData data)
        {
            Identifier ident = data.ReadIdentifier( );
            return ItemRegistry.CreateItem( ident );
        }
    }
}
