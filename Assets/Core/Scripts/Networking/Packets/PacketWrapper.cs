using KableNet.Common;
using KableNet.Math;

namespace Core.Scripts.Networking.Packets
{
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

            /*  The following line will cause a crash if
             *  a packet class doesnt override the identifier field!
             *  Allowing it for now, for debugging reasons.
             * */

            handoffPacket.Write( identifier.path );
            handoffPacket.Write( identifier.value );

            ToPacket( handoffPacket );

            return handoffPacket;
        }

        /// <summary>
        /// Serializes this packets data for sending it over the network stream.
        /// Just write directly to the 'packet' given.
        /// </summary>
        /// <param name="packet"></param>
        protected abstract void ToPacket( KablePacket p );

        /// <summary>
        /// The identifier for this packet. Make sure to override this
        /// as it gets automatically serialized!
        /// </summary>
        public virtual Identifier identifier { get; }
    }
}
