using Core.Scripts.Attributes;

using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Packets.Core
{

    [NetPacket("core", "ready_packet")]
    public class ReadyPacket : PacketWrapper
    {

        protected override void ToPacket( KablePacket p )
        {
            // Not sending any data besides this packet's
            // Identifier, which is handled in the background
        }

        //public override Identifier identifier { get { return new Identifier( "core", "ready" ); } }
    }
}
