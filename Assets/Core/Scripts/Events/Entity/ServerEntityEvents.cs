using Core.Scripts.Entities;

using Unity.VisualScripting;

using UnityEngine;

namespace Core.Scripts.Events.Entity
{
    public class ServerEntityEvents
    {
        public delegate void EntitySpawnEvent( ServerEntity entity, Vector3 position );
        public static event EntitySpawnEvent EntitySpawned;
        internal static void InvokeOnEntitySpawn( ServerEntity entity, Vector3 position )
        {
            EntitySpawned?.Invoke( entity, position );
        }

        public delegate void EntityDestroyedEvent( ServerEntity entity );
        public static event EntityDestroyedEvent EntityDestroyed;
        internal static void InvokeOnEntityDestroyed( ServerEntity entity )
        {
            EntityDestroyed?.Invoke( entity );
        }
    }
}
