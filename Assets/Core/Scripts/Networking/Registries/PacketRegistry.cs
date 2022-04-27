using System;
using System.Collections.Generic;
using System.Linq;

using Core.Scripts.Networking.Handlers;
using Core.Scripts.Networking.Handlers.Core;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking.Registries
{
    public static class PacketRegistry
    {
        internal static void HandlePacket( KablePacket packet, KableConnection sourceConnection )
        {

            Identifier packetIdent = packet.ReadIdentifier( );
            if ( register.Any( x => x.Key.Equals( packetIdent ) ) )
            {
                try
                {
                    register.FirstOrDefault( x => x.Key.Equals( packetIdent ) ).Value.HandlePacket( packet, sourceConnection );    
                }
                catch ( Exception ex )
                {
                    Debug.LogError( $"[PacketRegistry][HandlePacket] { ex }" );
                }
            }
            else
            {
                Debug.LogError( $"[PacketRegistry][HandlePacket] Unknown Packet with ident: { packetIdent }" );
            }
            
        }
        
        private static Dictionary<Identifier, KableHandler> register = new Dictionary<Identifier, KableHandler>( )
        {
            { new Identifier( "core", "auth_me" ), new AuthMeHandler(  ) },
        };
    }
}
