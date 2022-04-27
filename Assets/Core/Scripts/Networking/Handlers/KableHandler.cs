using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Handlers
{
    public abstract class KableHandler
    {
        public abstract void HandlePacket( KablePacket p, KableConnection src );
    }
}
