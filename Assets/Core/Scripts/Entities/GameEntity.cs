using Core.Scripts.Events.Entity;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Entities
{
    public class GameEntity
    {
        private const float NetworkLerpSeconds = 0.5f;
        
        public GameEntity( EntityWrapper wrapper, NetId netId )
        {
            NetId = netId;
            WrapperComponent = wrapper;
            WrapperObject = wrapper.gameObject;
            WrapperTransform = wrapper.transform;
        }

        internal virtual void ServerSpawned( )
        {
            
        }

        internal virtual void ServerTick( float deltaTime )
        {
            if ( PositionLerpAmt < 1f )
            {
                WrapperTransform.position = Vector3.Lerp( PreviousPosition, TargetPosition, PositionLerpAmt );
                PositionLerpAmt += deltaTime / NetworkLerpSeconds;
            }
            if ( RotationLerpAmt < 1f )
            {
                Quaternion prevRot = Quaternion.Euler( PreviousRotation );
                Quaternion tarRot = Quaternion.Euler( TargetRotation );
                WrapperTransform.rotation = Quaternion.Lerp( prevRot, tarRot, RotationLerpAmt );
                RotationLerpAmt += deltaTime / NetworkLerpSeconds;
            }
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
            TargetPosition = newPos;
            TargetRotation = newRot;
            PreviousPosition = TargetPosition;
            PreviousRotation = TargetRotation;

            WrapperTransform.position = TargetPosition;
            WrapperTransform.eulerAngles = TargetRotation;
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

            PositionLerpAmt = 0f;
            RotationLerpAmt = 0f;
        }

        public NetId NetId { get; private set; }
        public EntityWrapper WrapperComponent { get; private set; }
        public GameObject WrapperObject { get; private set; }
        public Transform WrapperTransform { get; private set; }

        public Vector3 PreviousPosition { get; protected set; } = Vector3.zero;
        public Vector3 TargetPosition { get; protected set; } = Vector3.zero;
        public Vector3 PreviousRotation { get; protected set; } = Vector3.zero;
        public Vector3 TargetRotation { get; protected set; } = Vector3.zero;
        public float PositionLerpAmt { get; protected set; } = 1f;
        public float RotationLerpAmt { get; protected set; } = 1f;

        // ------------ STATS ------------ \\
        public float BaseMovementSpeed { get; protected set; }
        public float BonusMovementSpeed { get; protected set; }
        public float SprintSpeedMultiplier { get; protected set; }

        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }

        public float Stamina { get; protected set; }
        public float MaxStamina { get; protected set; }

    }
}
