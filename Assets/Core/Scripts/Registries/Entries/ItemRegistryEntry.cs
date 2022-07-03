using Assets.Core.Scripts.Attributes;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Registries.Entries
{
    internal class ItemRegistryEntry
    {
        public static ItemRegistryEntry Create( Type entryType )
        {
            ItemType att = GetEntryTypeAttribute( entryType );
            return new ItemRegistryEntry( att.Identifier, entryType, att );
        }

        private static ItemType GetEntryTypeAttribute( Type handler )
        {
            ItemType attribute = ( ItemType )Attribute.GetCustomAttribute( handler, typeof( ItemType ) );

            return attribute;
        }

        private ItemRegistryEntry( Identifier entryIdentifier, Type entryType, ItemType raw )
        {
            this.EntryIdentifier = entryIdentifier;
            this.EntryType = entryType;
            this.RawAttribute = raw;
        }

        public Identifier EntryIdentifier { get; }

        public Type EntryType { get; }
        public ItemType RawAttribute { get; }
    }
}
