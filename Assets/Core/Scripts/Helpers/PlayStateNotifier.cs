using Core.Scripts.Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class PlayStateNotifier
{
    static PlayStateNotifier( )
    {
        EditorApplication.playModeStateChanged += PlayModeChanged;
    }

    private static void PlayModeChanged( PlayModeStateChange obj )
    {

        if ( !EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying )
        {
            GameServer.Shutdown( );
        }
    }
}
