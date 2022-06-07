using KableNet.Common;

namespace Core.Scripts.Networking.Packets
{
    public abstract class PacketHandler
    {
        public abstract void HandlePacket( KablePacket packet, KableConnection conn );
    }
}
