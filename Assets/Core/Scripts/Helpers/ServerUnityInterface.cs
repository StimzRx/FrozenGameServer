using System;

using Core.Scripts.Singletons;

using UnityEngine;

public class ServerUnityInterface : MonoBehaviour
{
    void Start( )
    {
        GameServer.Init( 5665 );
    }

    private void Update( )
    {
        GameServer.UnityTick(  );
    }

    private void OnApplicationQuit( )
    {
        GameServer.Shutdown(  );
    }
}
