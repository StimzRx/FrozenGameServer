using System;

namespace Core.Scripts.Attributes
{
    /// <summary>
    /// Attribute for classes that extend KableHandler.
    /// Used to mark attributes of the handler, such as its Identifier
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true )
    ]
    public class NetHandler : System.Attribute
    {
        public NetHandler( string identifierNamespace, string identifierPath )
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
