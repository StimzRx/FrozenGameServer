using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Core.Scripts.Attributes;
using Assets.Core.Scripts.Interfaces;
using Assets.Core.Scripts.Serialization;

namespace Assets.Core.Scripts.Mapping
{
    [BlockType("core.block", "debug_block")]
    public class Block : Serializable<Block>
    {
        public Block()
        {

        }

        public SerialData ToSerial( )
        {

        }
        public static Block FromSerial(SerialData data)
        {

        }
    }
}
