using Core.Scripts.Attributes;
using Core.Scripts.Helpers;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking.Packets.Core
{
    [NetPacket("core", "teleport_entity_packet")]
    public class TeleportEntityPacket : PacketWrapper
    {
        public TeleportEntityPacket( NetId netId, Vector3 newPos, Vector3 newRot )
        {
            _netId = netId;
            _newPos = MathHelper.UnityToKable( newPos );
            _newRot = MathHelper.UnityToKable( newRot );
        }

        private NetId _netId;
        private Vec3f _newPos, _newRot;

        protected override void ToPacket( KablePacket p )
        {
            p.Write( _netId );
            p.Write( _newPos );
            p.Write( _newRot );
        }
    }
}
