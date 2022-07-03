using Assets.Core.Scripts.Attributes;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    [ItemType("", 0, "core.items", "empty")]
    public class Item
    {

        public static Item EMPTY
        {
            get
            {
                return new Item( );
            }
        }
        
    }
}
