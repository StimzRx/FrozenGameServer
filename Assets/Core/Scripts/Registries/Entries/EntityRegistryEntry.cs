using System;

using Core.Scripts.Attributes;

using KableNet.Math;

namespace Core.Scripts.Registries.Entries
{
    public class EntityRegistryEntry
    {
        public static EntityRegistryEntry Create( Type entryType )
        {
            Identifier entryIdent = GetEntityTypeIdentifier( entryType );
            return new EntityRegistryEntry( entryIdent, entryType );
        }
        
        private static Identifier GetEntityTypeIdentifier(Type handler)
        {
            Identifier ret = new Identifier( "null", "null" );

            EntityType servHandlerAttribute = (EntityType)Attribute.GetCustomAttribute( handler, typeof(EntityType) );

            if ( servHandlerAttribute != null )
            {
                ret = new Identifier( servHandlerAttribute.IdentifierNamespace, servHandlerAttribute.IdentifierPath );
            }

            return ret;
        }
        
        private EntityRegistryEntry( Identifier entryIdentifier, Type entryType )
        {
            this.EntryIdentifier = entryIdentifier;
            this.EntryType = entryType;
        }

        public Identifier EntryIdentifier
        {
            get;
        }

        public Type EntryType
        {
            get;
        }
    }
}
