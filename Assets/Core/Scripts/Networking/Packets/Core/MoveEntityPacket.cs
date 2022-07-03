using Core.Scripts.Attributes;
using Core.Scripts.Entities;
using Core.Scripts.Helpers;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking.Packets.Core
{
    [NetPacket( "core.packet", "move_entity")]
    public class MoveEntityPacket : PacketWrapper
    {
        public MoveEntityPacket( NetId netId, Vector3 newPos, Vector3 newRot )
        {
            _netId = netId;
            _newPos = MathHelper.UnityToKable(newPos);
            _newRot = MathHelper.UnityToKable(newRot);
        }
        public MoveEntityPacket( GameEntity entity, Vector3 newPos, Vector3 newRot )
        {
            _netId = entity.NetId;
            _newPos = MathHelper.UnityToKable(newPos);
            _newRot = MathHelper.UnityToKable(newRot);
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
