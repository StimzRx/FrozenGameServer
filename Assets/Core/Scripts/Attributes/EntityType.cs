using System;

namespace Core.Scripts.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true )
    ]
    public class EntityType : System.Attribute
    {
        public EntityType( string identifierNamespace, string identifierPath )
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
