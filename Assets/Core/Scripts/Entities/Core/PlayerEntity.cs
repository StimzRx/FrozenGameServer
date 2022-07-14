using Assets.Core.Scripts.Inventories;
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

            NetPlayer.SendTcp( new SetInventorySlotPacket( )
            {
                Location = new Vec2i(0,0),
                Stack = new ItemStack()
                {
                    StackName = "STACK_TEST",
                    StackDescription = "STACK_DESCRIPTION",
                    Count = 23,
                    Item = new Item()
                    {
                        Name = "STACK_TEST_ITEM",
                        MaxStackSize = 1,
                        Weight = 0.1f,
                    },
                },
            } );
        }

        override internal void ServerTick( float deltaTime )
        {
            base.ServerTick( deltaTime );
        }

        public NetPlayer NetPlayer { get; protected set; }
    }
}
