using Assets.Core.Scripts.Attributes;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    [ItemType("core.items", "empty")]
    public class Item
    {
        public string Name { get; private set; }
        public int MaxStackSize { get; private set; }
        
    }
}
