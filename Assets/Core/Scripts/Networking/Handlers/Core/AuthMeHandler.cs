using Core.Scripts.Attributes;
using Core.Scripts.Entities.Core;
using Core.Scripts.Networking.Packets;
using Core.Scripts.Networking.Packets.Core;
using Core.Scripts.Singletons;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking.Handlers.Core
{
    /// <summary>
    /// "Authentication" packet's handler
    /// </summary>
    [NetHandler("core", "auth_me_packet")]
    public class AuthMeHandler : PacketHandler
    {
        public override void HandlePacket( KablePacket p, KableConnection src )
        {
            string versionRaw = p.ReadString( );
            if ( versionRaw != "0.0.1" )
            {
                // Invalid version number,
                // REJECT!
                Debug.LogError( "Invalid version number:" + versionRaw );
                return;
            }

            string usernameRaw = p.ReadString( );

            NetPlayer netPlr = GameServer.ToNetPlayer( src );
            if ( netPlr == null )
            {
                // Some sort of internal processing error if we get here.
                // Reject...?
                Debug.LogError( "NetPLR Is null?!?" );
                return;
            }

            netPlr.SendTcp( new ReadyPacket( netPlr.NetId ) );
            
            Debug.Log( $"User '{ usernameRaw }'[{ netPlr.NetId }] has joined..." );

            PlayerEntity plrEnt = GameServer.SpawnEntityByType<PlayerEntity>( netPlr.NetId );
        }
    }
}
