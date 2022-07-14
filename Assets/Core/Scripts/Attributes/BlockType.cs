using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KableNet.Math;

namespace Assets.Core.Scripts.Attributes
{

    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true )
    ]
    public class BlockType : System.Attribute
    {
        public BlockType( string identPath, string identValue )
        {
            Identifier = new Identifier( identPath, identValue );
        }
        public Identifier Identifier { get; private set; }

    }
}
