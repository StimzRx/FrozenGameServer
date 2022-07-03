using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true )
    ]
    internal class ItemType : System.Attribute
    {
        public ItemType( string name, int maxStackSize, string identPath, string identValue )
        {
            Name = name;
            MaxStackSize = maxStackSize;
            Identifier = new Identifier(identPath, identValue);
        }

        public string Name { get; private set; }
        public int MaxStackSize { get; private set; }
        public Identifier Identifier { get; private set; }

    }
}
