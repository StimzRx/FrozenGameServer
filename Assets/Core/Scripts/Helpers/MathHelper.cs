using KableNet.Math;

using UnityEngine;

namespace Core.Scripts.Helpers
{
    public static class MathHelper
    {
        public static Vec3f UnityToKable( Vector3 input )
        {
            return new Vec3f( input.x, input.y, input.z );
        }
        public static Vector3 KableToUnity( Vec3f input )
        {
            return new Vector3( input.x, input.y, input.z );
        }
    }
}
