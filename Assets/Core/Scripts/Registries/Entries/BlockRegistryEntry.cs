using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Core.Scripts.Attributes;
using KableNet.Math;

namespace Assets.Core.Scripts.Registries.Entries
{
    public class BlockRegistryEntry
    {
        public static BlockRegistryEntry Create( Type entryType )
        {
            BlockType att = GetEntryTypeAttribute( entryType );
            return new BlockRegistryEntry( att.Identifier, entryType, att );
        }

        private static BlockType GetEntryTypeAttribute( Type handler )
        {
            BlockType attribute = ( BlockType )Attribute.GetCustomAttribute( handler, typeof( BlockType ) );

            return attribute;
        }

        private BlockRegistryEntry( Identifier entryIdentifier, Type entryType, BlockType raw )
        {
            this.EntryIdentifier = entryIdentifier;
            this.EntryType = entryType;
            this.RawAttribute = raw;
        }

        public Identifier EntryIdentifier { get; }

        public Type EntryType { get; }
        public BlockType RawAttribute { get; }
    }
}
