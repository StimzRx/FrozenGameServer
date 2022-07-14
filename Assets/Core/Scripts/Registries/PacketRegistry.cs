using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Core.Scripts.Networking.Packets.Core.Inventory;
using Core.Scripts.Attributes;
using Core.Scripts.Networking.Handlers;
using Core.Scripts.Networking.Handlers.Core;
using Core.Scripts.Networking.Packets;
using Core.Scripts.Networking.Packets.Core;
using Core.Scripts.Networking.Registries.Entries;
using Core.Scripts.Registries.Entries;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Registries
{
    /// <summary>
    /// Handles registration and triggering of network packets' and handlers'
    /// </summary>
    public static class PacketRegistry
    {

        /// <summary>
        /// The list/registry of NetworkHandlers'
        /// </summary>
        private static List<PacketHandlerRegistryEntry> _handlerRegister = new List<PacketHandlerRegistryEntry>( )
        {
            // Game State Updates
            { PacketHandlerRegistryEntry.Create( typeof(AuthMeHandler) ) },
            
            // Entity Updates
            { PacketHandlerRegistryEntry.Create( typeof(RequestMoveHandler) ) },
        };

        /// <summary>
        /// The list/registry of NetworkPackets'
        /// </summary>
        private static List<PacketRegistryEntry> _packetRegister = new List<PacketRegistryEntry>( )
        {
            // Game State Updates
            { PacketRegistryEntry.Create( typeof(ReadyPacket) ) },
            
            // Entity Creation/Destruction
            { PacketRegistryEntry.Create( typeof(SpawnEntityPacket) ) },
            { PacketRegistryEntry.Create( typeof(DestroyEntityPacket) ) },
            
            // Entity Updates
            { PacketRegistryEntry.Create( typeof(MoveEntityPacket) ) },
            { PacketRegistryEntry.Create( typeof(TeleportEntityPacket) )},

            // Inventory
            { PacketRegistryEntry.Create( typeof( SetupInventoryPacket ) ) },
            { PacketRegistryEntry.Create( typeof( SetInventorySlotPacket ) ) },
        };




        /// <summary>
        /// Triggers a NetworkHandler
        /// </summary>
        /// <param name="identifier">The Packets Identifier</param>
        /// <param name="p">The Unread KablePacket</param>
        /// <param name="conn">The Source KableConnection</param>
        public static void TriggerHandler( Identifier identifier, KablePacket p, KableConnection conn )
        {
            Type tarType = _handlerRegister.FirstOrDefault( x => x.EntryIdentifier.Equals( identifier ) )?.EntryType;

            if ( tarType is null )
            {
                Debug.LogError( $"[PacketRegistry.TriggerHandler()]: Invalid KableHandler for { identifier }" );
                return;
            }

            MethodInfo toInvoke = null;
            PacketHandler baseHandler = null;
            
            bool didFind = false;
            if ( TriggerCache.ContainsKey( tarType ) )
            {
                baseHandler = TriggerCache[ tarType ].BaseHandler;
                toInvoke = TriggerCache[ tarType ].MethodInfo;
                
                didFind = true;
            }

            baseHandler ??= (PacketHandler)Activator.CreateInstance( tarType );
            toInvoke ??= tarType.GetMethod( "HandlePacket" );

            if ( !didFind )
            {
                TriggerCache.Add( tarType, new TriggerCacheEntry( baseHandler, toInvoke ) );
            }
            
            if ( toInvoke is null )
            {
                Debug.LogError( $"[PacketRegistry.TriggerHandler()] type '{ tarType.Namespace }.{ tarType.Name }' has no static method called 'HandlePacket' with params '(KablePacket, KableConnection)'!" );
            }
            else
            {
                toInvoke.Invoke( baseHandler, new object[ ]
                {
                    p,
                    conn,
                } );
            }
            
        }

        public static Identifier GetPacketIdentifier<T>( ) where T : PacketWrapper
        {
            Identifier ident = _handlerRegister.FirstOrDefault( x => x.EntryType == typeof(T) ).EntryIdentifier;
            if ( ident is null )
            {
                ident = new Identifier( "null", "null" );
            }
            return ident;
        }

        readonly private static Dictionary<Type, TriggerCacheEntry> TriggerCache = new Dictionary<Type, TriggerCacheEntry>( );


    }
}
