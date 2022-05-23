using System;

using Core.Scripts.Attributes;
using Core.Scripts.Networking.Handlers;

using KableNet.Common;
using KableNet.Math;

using Unity.VisualScripting;

namespace Core.Scripts.Networking.Registries.Entries
{
    public class PacketHandlerRegistryEntry
    {
        public static PacketHandlerRegistryEntry Create( Type entryType )
        {
            Identifier entryIdent = GetHandlerIdentifier( entryType );
            return new PacketHandlerRegistryEntry( entryIdent, entryType );
        }
        
        private static Identifier GetHandlerIdentifier(Type handler)
        {
            Identifier ret = new Identifier( "null", "null" );

            NetHandler servHandlerAttribute = (NetHandler)Attribute.GetCustomAttribute( handler, typeof(NetHandler) );

            if ( servHandlerAttribute != null )
            {
                ret = new Identifier( servHandlerAttribute.IdentifierNamespace, servHandlerAttribute.IdentifierPath );
            }

            return ret;
        }
        
        private PacketHandlerRegistryEntry( Identifier entryIdentifier, Type entryType )
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
