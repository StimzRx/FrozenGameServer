using System;

namespace Core.Scripts.Attributes
{
    /// <summary>
    /// Attribute for classes that extend PacketWrapper.
    /// Used to mark attributes of the packet, such as its Identifier
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true)
    ]
    public class NetPacket : System.Attribute
    {
        public NetPacket( string identifierNamespace, string identifierPath )
        {
            this.identNamespace = identifierNamespace;
            this.identPath = identifierPath;
        }

        private string identNamespace, identPath;

        public string IdentifierNamespace
        {
            get
            {
                return identNamespace;
            }
        }

        public string IdentifierPath
        {
            get
            {
                return identPath;
            }
        }
        
    }
}
