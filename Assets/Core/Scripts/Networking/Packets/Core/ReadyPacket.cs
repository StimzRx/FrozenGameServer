using Core.Scripts.Attributes;

using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Packets.Core
{

    [NetPacket("core", "ready_packet")]
    public class ReadyPacket : PacketWrapper
    {
        public ReadyPacket( NetId clientsNetId )
        {
            _clientsNetId = clientsNetId;
        }
        private NetId _clientsNetId;

        protected override void ToPacket( KablePacket p )
        {
            p.Write( _clientsNetId );
        }

    }
}
