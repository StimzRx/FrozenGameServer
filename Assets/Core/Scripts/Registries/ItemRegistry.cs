using Assets.Core.Scripts.Attributes;
using Assets.Core.Scripts.Inventories;
using Assets.Core.Scripts.Registries.Entries;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Scripts.Registries
{
    public static class ItemRegistry
    {

        private static List<ItemRegistryEntry> _register = new List<ItemRegistryEntry>( )
        {
            //{ ItemRegistryEntry.Create( typeof(null) ) },
        };


        public static Item CreateItem( Identifier identifier )
        {
            ItemRegistryEntry[ ] refBuffer;
            lock ( _register )
            {
                refBuffer = _register.ToArray( );
            }

            foreach ( ItemRegistryEntry reference in refBuffer )
            {
                if ( reference.EntryIdentifier == identifier )
                {
                    Item itemInstance = ( Item )Activator.CreateInstance( reference.EntryType );
                    return itemInstance;
                }
            }
            return null;
        }

        public static Identifier GetIdentifierForItem( Item itemInstance )
        {
            Identifier ret = new Identifier( "null", "null" );

            ItemType typeAttribute = ( ItemType )Attribute.GetCustomAttribute( itemInstance.GetType( ), typeof( ItemType ) );

            if ( typeAttribute is not null )
            {
                ret = typeAttribute.Identifier;
            }
            else
            {
                Debug.LogError( $"[ItemRegistry.GetIdentifierForItem()] ItemTypeAttribute wasnt found on type: {itemInstance.GetType( ).Namespace}.{itemInstance.GetType( ).Name}" );
            }

            return ret;
        }

        public static Identifier GetIdentifierForItem<T>( ) where T : Item
        {
            Identifier ret = new Identifier( "null", "null" );

            ItemType attribute = ( ItemType )Attribute.GetCustomAttribute( typeof( T ), typeof( ItemType ) );

            if ( attribute != null )
            {
                ret = attribute.Identifier;
            }
            else
            {
                Debug.LogError( $"[ItemRegistry.GetIdentifierForItem<T>] ItemTypeAttribute wasnt found on type: {typeof( T ).Namespace}.{typeof( T ).Name}" );
            }

            return ret;
        }
    }
}
