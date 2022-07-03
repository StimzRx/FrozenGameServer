using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal static class ThreadHelper
{
    public static void Queue( Action action )
    {
        if ( action is null )
            return;

        lock( _actions )
        {
            _actions.Add( action );
        }
    }

    internal static void Process( )
    {
        List<Action> buffer;
        lock( _actions )
        {
            buffer = new List<Action>( _actions );
            _actions.Clear( );
        }

        if(buffer.Count <= 0)
        {
            return;
        }

        foreach(Action ac in buffer)
        {
            try
            {
                ac.Invoke( );
            }
            catch(Exception ex)
            {
                Queue( ( ) =>
                {
                    Debug.LogError( $"ThreadHelper.Process() Exception:\n{ex}" );
                } );
            }
        }
    }

    private static List<Action> _actions = new List<Action>( );
}
