using System;
using System.Collections.Generic;
using System.Linq;

using Core.Scripts.Attributes;
using Core.Scripts.Entities;
using Core.Scripts.Entities.Core;
using Core.Scripts.Networking.Handlers.Core;
using Core.Scripts.Networking.Registries.Entries;
using Core.Scripts.Registries.Entries;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Registries
{
    public static class EntityRegistry
    {
        
        /// <summary>
        /// Generates a new GameEntity instance which has a matching Identifier to the given Identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="wrapper"></param>
        /// <param name="entNetId"></param>
        /// <returns></returns>
        public static GameEntity CreateGameEntity( Identifier identifier, EntityWrapper wrapper, NetId entNetId )
        {
            EntityRegistryEntry[ ] refBuffer;
            lock ( _register )
            {
                refBuffer = _register.ToArray( );
            }
            
            foreach (EntityRegistryEntry reference in refBuffer)
            {
                if ( reference.EntryIdentifier == identifier )
                {
                    GameEntity gameEnt = (GameEntity)Activator.CreateInstance( reference.EntryType, wrapper, entNetId );
                    
                    return gameEnt;
                }
            }
            return null;
        }

        public static Identifier GetIdentifierForGameEntity<T>( ) where T : GameEntity
        {
            Identifier ret = new Identifier( "null", "null" );

            EntityType servHandlerAttribute = (EntityType)Attribute.GetCustomAttribute( typeof(T), typeof(EntityType) );

            if ( servHandlerAttribute != null )
            {
                ret = new Identifier( servHandlerAttribute.IdentifierNamespace, servHandlerAttribute.IdentifierPath );
            }
            else
            {
                Debug.LogError( $"[EntityRegistry.GetIdentifierForGameEntity<T>] EntityTypeAttribute wasnt found on type: { typeof(T).Namespace }.{ typeof(T).Name }" );
            }

            return ret;
        }
        
        /// <summary>
        /// The list/registry of EntityTypes'
        /// </summary>
        private static List< EntityRegistryEntry > _register = new List <EntityRegistryEntry >( )
        {
            { EntityRegistryEntry.Create( typeof(PlayerEntity) ) },
        };
    }
}
