using Core.Scripts.Singletons;

using KableNet.Common;

using UnityEngine;

namespace Core.Scripts.Networking.Handlers.Core
{
    /// <summary>
    /// "Authentication" packet's handler
    /// </summary>
    public class AuthMeHandler : KableHandler
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
            
            Debug.Log( $"User '{ usernameRaw }'[{ netPlr.NetId }] has joined..." );
        }
    }
}
