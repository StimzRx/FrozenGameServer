using System;

using Core.Scripts.Helpers;
using Core.Scripts.Registries;
using Core.Scripts.Singletons;

using UnityEngine;

/// <summary>
/// A really jank class meant to interface Unity's events with our (equally jank) Game
/// </summary>
public class ServerUnityInterface : MonoBehaviour
{
    void Start( )
    {
        PrefabRegistry.Initialize( prefabReferences );
        
        GameServer.Start( 5665 );
    }

    private void Update( )
    {
        GameServer.UnityTick(  );
    }

    private void OnApplicationQuit( )
    {
        GameServer.Shutdown(  );
    }

    [SerializeField] public PrefabReference[ ] prefabReferences;
}
