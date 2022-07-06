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
        public ItemType( string identPath, string identValue )
        {
            Identifier = new Identifier(identPath, identValue);
        }
        public Identifier Identifier { get; private set; }

    }
}
