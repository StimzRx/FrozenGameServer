using System;

using UnityEngine;

namespace Core.Scripts.Entities
{
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
