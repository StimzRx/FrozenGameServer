﻿using Assets.Core.Scripts.Attributes;
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
        public string Name { get; set; }
        public int MaxStackSize { get; set; }
        public float Weight { get; set; }

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

        public static Item EMPTY
        {
            get
            {
                return new Item( )
                {
                    Name = "",
                    MaxStackSize = 0,
                    Weight = 0,
                };
            }
        }
    }
}
