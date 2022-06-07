using System;

using Core.Scripts.Attributes;
using Core.Scripts.Registries;

using KableNet.Common;
using KableNet.Math;

using Unity.VisualScripting;

namespace Core.Scripts.Networking.Packets
{
    /// <summary>
    /// The base type for all NetworkPackets'
    /// </summary>
    public abstract class PacketWrapper
    {
        /// <summary>
        /// INTERNAL ONLY!
        /// Gets this packet class instance as a 'KablePacket' AKA Serialized and ready to be
        /// sent to the network stream
        /// </summary>
        /// <returns></returns>
        internal virtual KablePacket GetAsPacket( )
        {
            KablePacket handoffPacket = new KablePacket( );
            
            Identifier tarIdent = new Identifier( "null", "null" );

            NetPacket netPacketAttribute = (NetPacket)Attribute.GetCustomAttribute( GetType(  ), typeof(NetPacket) );

            if ( netPacketAttribute != null )
            {
                tarIdent = new Identifier( netPacketAttribute.IdentifierNamespace, netPacketAttribute.IdentifierPath );
            }
            
            handoffPacket.Write( tarIdent.path );
            handoffPacket.Write( tarIdent.value );

            ToPacket( handoffPacket );

            return handoffPacket;
        }

        /// <summary>
        /// Serializes this packets data for sending it over the network stream.
        /// Just write directly to the 'packet' given.
        /// </summary>
        /// <param name="packet"></param>
        protected abstract void ToPacket( KablePacket p );
    }
}
