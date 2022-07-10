using Assets.Core.Scripts.Networking.Packets.Core.Inventory;
using Core.Scripts.Attributes;
using Core.Scripts.Networking;
using Core.Scripts.Singletons;
using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities.Core
{
    [EntityType("core.entity", "player")]
    public class PlayerEntity : GameEntity
    {
        public PlayerEntity( EntityWrapper wrapper, NetId netId ) : base( wrapper, netId )
        {

        }

        override internal void ServerSpawned( )
        {
            base.ServerSpawned( );

            NetPlayer = GameServer.FindNetPlayer( NetId );

            NetPlayer.SendTcp( new SetupInventoryPacket( )
            {
                SlotsX = Inventory.SlotCountX,
                SlotsY = Inventory.SlotCountY,
                IsRemote = false,
            } );
        }

        override internal void ServerTick( float deltaTime )
        {
            base.ServerTick( deltaTime );
        }

        public NetPlayer NetPlayer { get; protected set; }
    }
}
