using System;

using UnityEngine;

namespace Core.Scripts.Entities
{
    /// <summary>
    /// Wraps Unity's GameObjects to act as our game's 'Entity'
    /// </summary>
    public class EntityWrapper : MonoBehaviour
    {
        private void Start( )
        {
            GameEntity.ServerSpawned( );
        }
        private void Update( )
        {
            GameEntity.ServerTick( Time.deltaTime );
        }

        public void Initialize( GameEntity gameEntity )
        {
            this.GameEntity = gameEntity;
        }
        
        public GameEntity GameEntity { get; private set; }
    }
}
