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
            ServerEntity.ServerSpawned( );
        }
        private void Update( )
        {
            ServerEntity.ServerTick( Time.deltaTime );
        }

        public void Initialize( ServerEntity serverEntity )
        {
            this.ServerEntity = serverEntity;
        }
        
        public ServerEntity ServerEntity { get; private set; }
    }
}
