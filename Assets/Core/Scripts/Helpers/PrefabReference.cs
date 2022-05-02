using System;

using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Helpers
{
    [Serializable]
    public class PrefabReference
    {
        [SerializeField] private GameObject prefabReference;
        [SerializeField] private string prefabIdentifier;

        public GameObject GetPrefab( )
        {
            return prefabReference;
        }
        public Identifier GetIdentifier( )
        {
            Identifier ret = new Identifier( "null", "null" );
            if ( prefabIdentifier.Contains( ':' ) )
            {
                string[ ] splits = prefabIdentifier.Split( ':' );
                ret = new Identifier( splits[ 0 ], splits[ 1 ] );
            }

            return ret;
        }
    }
}
