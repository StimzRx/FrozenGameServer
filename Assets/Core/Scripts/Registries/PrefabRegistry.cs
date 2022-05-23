using System.Collections.Generic;

using Core.Scripts.Helpers;
using Core.Scripts.Networking.Handlers;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Registries
{
    /// <summary>
    /// Handles registration and retrieval of preset GameObject prefabs'
    /// </summary>
    public static class PrefabRegistry
    {
        internal static void Initialize( PrefabReference[] references )
        {
            _register = new List<PrefabReference>( references );
        }

        public static GameObject GetPrefab( Identifier identifier )
        {
            PrefabReference[ ] refBuffer;
            lock ( _register )
            {
                refBuffer = _register.ToArray( );
            }
            
            foreach (PrefabReference reference in refBuffer)
            {
                if ( reference.GetIdentifier( ) == identifier )
                {
                    return reference.GetPrefab( );
                }
            }
            return null;
        }

        private static List<PrefabReference> _register = new List<PrefabReference>( );
    }
}
