using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Core.Scripts.Interfaces;
using Assets.Core.Scripts.Serialization;

namespace Assets.Core.Scripts.Mapping
{
    public class BlockState : Serializable<BlockState>
    {
        public BlockState(Block blockType)
        {
            Block = blockType;
        }

        public SerialData ToSerial( )
        {
            SerialData serialData = new SerialData();

            SerialData blockSerialData = Block.ToSerial( );
            serialData.Write( blockSerialData.Count );
            serialData.Write( Block.ToSerial( ) );

            return serialData;
        }
        public static BlockState FromSerial(SerialData data)
        {
            int readAmt = data.ReadInt( );
            SerialData blockSerialData = new SerialData(data.ReadByte( readAmt ));
            Block block = Block.FromSerial( blockSerialData );

            return new BlockState( block );
        }

        public Block Block { get; protected set; }
    }
}
