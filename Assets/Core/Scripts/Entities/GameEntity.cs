using Core.Scripts.Events.Entity;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities
{
    public class GameEntity
    {
        public GameEntity( EntityWrapper wrapper, NetId netId )
        {
            this.NetId = netId;
            this.WrapperComponent = wrapper;
            this.WrapperObject = wrapper.gameObject;
        }

        internal virtual void ServerSpawned( )
        {
            ServerEntityEvents.TriggerOnEntitySpawn( this, WrapperObject.transform.position );
        }

        internal virtual void ServerTick( float deltaTime )
        {

        }

        public virtual void Destroy( )
        {
            
        }

        public NetId NetId { get; protected set; }
        public EntityWrapper WrapperComponent { get; private set; }
        public GameObject WrapperObject { get; private set; }

        public static Identifier EntityIdentifier { get; protected set; } = new Identifier( "core", "empty_entity" );
    }
}
