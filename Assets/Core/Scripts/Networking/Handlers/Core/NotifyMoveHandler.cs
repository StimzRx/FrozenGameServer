﻿using Core.Scripts.Attributes;
using Core.Scripts.Helpers;
using Core.Scripts.Networking.Packets;
using Core.Scripts.Singletons;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Networking.Handlers.Core
{
    [NetHandler("core", "notify_move_packet")]
    public class NotifyMoveHandler : PacketHandler
    {
        public override void HandlePacket( KablePacket packet, KableConnection conn )
        {
            Vector3 newPos = MathHelper.KableToUnity(packet.ReadVec3f( ));
            Vector3 newRot = MathHelper.KableToUnity(packet.ReadVec3f( ));

            NetId tarNetId = GameServer.ToNetPlayer( conn ).NetId;
            GameServer.FindGameEntity( tarNetId ).Move( newPos, newRot );
        }
    }
}