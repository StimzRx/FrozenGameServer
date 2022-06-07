using System.Reflection;

using Core.Scripts.Networking.Packets;

namespace Core.Scripts.Registries.Entries
{
    public class TriggerCacheEntry
    {
        public TriggerCacheEntry( PacketHandler baseHandler, MethodInfo methodInfo )
        {
            this.BaseHandler = baseHandler;
            this.MethodInfo = methodInfo;
        }
        
        public PacketHandler BaseHandler { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
    }
}
