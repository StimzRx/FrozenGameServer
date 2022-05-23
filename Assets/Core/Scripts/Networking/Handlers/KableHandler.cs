using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Handlers
{
    /// <summary>
    /// The jank "interface" that is a base type for all NetworkHandlers'
    /// </summary>
    public abstract class KableHandler
    {
        public abstract void HandlePacket( KablePacket p, KableConnection src );
    }
}
