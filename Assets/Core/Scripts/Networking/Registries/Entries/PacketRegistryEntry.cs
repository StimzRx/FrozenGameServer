using System;

using Core.Scripts.Attributes;

using KableNet.Math;

namespace Core.Scripts.Networking.Registries.Entries
{
    public class PacketRegistryEntry
    {
        public static PacketRegistryEntry Create( Type entryType )
        {
            Identifier entryIdent = GetHandlerIdentifier( entryType );
            return new PacketRegistryEntry( entryIdent, entryType );
        }
        
        private static Identifier GetHandlerIdentifier(Type handler)
        {
            Identifier ret = new Identifier( "null", "null" );

            NetPacket servPacketAttribute = (NetPacket)Attribute.GetCustomAttribute( handler, typeof(NetPacket) );

            if ( servPacketAttribute != null )
            {
                ret = new Identifier( servPacketAttribute.IdentifierNamespace, servPacketAttribute.IdentifierPath );
            }

            return ret;
        }
        
        private PacketRegistryEntry( Identifier entryIdentifier, Type entryType )
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
