using Core.Scripts.Entities;

using Unity.VisualScripting;

using UnityEngine;

namespace Core.Scripts.Events.Entity
{
    public class ServerEntityEvents
    {
        /// <summary>
        /// Triggered whenever a Entity is spawned in the game world.
        /// </summary>
        public delegate void OnEntitySpawn( GameEntity entity, Vector3 position );
        public static event OnEntitySpawn EntitySpawnEvent;
        internal static void InvokeOnEntitySpawn( GameEntity entity, Vector3 position )
            => EntitySpawnEvent?.Invoke( entity, position );
        
        
        /// <summary>
        /// Triggered whenever a Entity is de-spawned in the game world.
        /// </summary>
        public delegate void OnEntityDestroyed( GameEntity entity );
        public static event OnEntityDestroyed EntityDestroyedEvent;
        internal static void InvokeOnEntityDestroyed( GameEntity entity )
            => EntityDestroyedEvent?.Invoke( entity );
    }
}
