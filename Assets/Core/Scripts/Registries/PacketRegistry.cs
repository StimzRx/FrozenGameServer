using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Core.Scripts.Attributes;
using Core.Scripts.Networking.Handlers;
using Core.Scripts.Networking.Handlers.Core;
using Core.Scripts.Networking.Packets.Core;
using Core.Scripts.Networking.Registries.Entries;

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
            
            try
            {
                tarType.GetMethod( "HandlePacket" )?.Invoke( null, new object[ ]
                {
                    p,
                    conn,
                } );
            }
            catch ( Exception ex )
            {
                Debug.LogError( $"[PacketRegistry.TriggerHandler()]: { ex.Message }\nFull:{ ex.ToString(  ) }" );
            }
        }
        
        /// <summary>
        /// The list/registry of NetworkHandlers'
        /// </summary>
        private static List< PacketHandlerRegistryEntry > _handlerRegister = new List <PacketHandlerRegistryEntry >( )
        {
            //{ PacketHandlerRegistryEntry.Create( typeof(AuthMeHandler) ) }
        };

        /// <summary>
        /// The list/registry of NetworkPackets'
        /// </summary>
        private static List<PacketRegistryEntry> _packetRegister = new List<PacketRegistryEntry>()
        {
            //{ PacketRegistryEntry.Create( typeof(ReadyPacket) ) },
        };
        
        
    }
}
