using Core.Scripts.Events.Entity;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities
{
    public class GameEntity
    {
        public GameEntity( EntityWrapper wrapper, NetId netId )
        {
            NetId = netId;
            WrapperComponent = wrapper;
            WrapperObject = wrapper.gameObject;
            WrapperTransform = wrapper.transform;
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

        /// <summary>
        /// Instantly teleports entity to the given location. For sliding, see #Move
        /// </summary>
        /// <param name="newPos"></param>
        /// <param name="newRot"></param>
        public virtual void Teleport( Vector3 newPos, Vector3 newRot )
        {
            
        }
        
        /// <summary>
        /// Slowly slides/lerps the entity from their previous position to the new position. For teleporting, see #Teleport
        /// </summary>
        /// <param name="newPos"></param>
        /// <param name="newRot"></param>
        public virtual void Move( Vector3 newPos, Vector3 newRot )
        {
            PreviousPosition = WrapperTransform.position;
            PreviousRotation = WrapperTransform.eulerAngles;

            TargetPosition = newPos;
            TargetRotation = newRot;

            WrapperTransform.position = TargetPosition;
            WrapperTransform.eulerAngles = TargetRotation;
        }

        public NetId NetId { get; private set; }
        public EntityWrapper WrapperComponent { get; private set; }
        public GameObject WrapperObject { get; private set; }
        public Transform WrapperTransform { get; private set; }

        public Vector3 PreviousPosition { get; protected set; } = Vector3.zero;
        public Vector3 TargetPosition { get; protected set; } = Vector3.zero;
        public Vector3 PreviousRotation { get; protected set; } = Vector3.zero;
        public Vector3 TargetRotation { get; protected set; } = Vector3.zero;
    }
}
