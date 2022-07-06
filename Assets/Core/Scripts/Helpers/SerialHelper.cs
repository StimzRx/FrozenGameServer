using Assets.Core.Scripts.Inventories;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Helpers
{
    public static class SerialHelper
    {
        public static List<byte> Serialize(Item data)
        {
            if(data is null)
            {
                return new List<byte>( );
            }

            return data.ToSerial( ).ToList( );
        }

        public static List<byte> Serialize(ItemStack data)
        {
            if( data is null )
            {
                return new List<byte>( );
            }

            return data.ToSerial( ).ToList( );
        }

        public static byte[] Serialize(int data)
        {
            return BitConverter.GetBytes( data );
        }
        public static byte[ ] Serialize(long data)
        {
            return BitConverter.GetBytes( data );
        }
        public static byte[ ] Serialize( float data )
        {
            return BitConverter.GetBytes( data );
        }
        public static byte[ ] Serialize( bool data )
        {
            return BitConverter.GetBytes( data );
        }
        public static byte[ ] Serialize( short data )
        {
            return BitConverter.GetBytes( data );
        }
        public static byte[ ] Serialize( string data )
        {
            List<byte> byteBuffer = new List<byte>( );
            byteBuffer.AddRange( BitConverter.GetBytes( data.Length ) );
            byteBuffer.AddRange( Encoding.Unicode.GetBytes( data ) );
            return byteBuffer.ToArray( );
        }
        public static byte[ ] Serialize( Vec2i data )
        {
            List<byte> byteBuffer = new List<byte>( );
            byteBuffer.AddRange( Serialize( data.x ) );
            byteBuffer.AddRange( Serialize( data.y ) );
            return byteBuffer.ToArray( );
        }
        public static byte[ ] Serialize(Vec3f data)
        {
            List<byte> byteBuffer = new List<byte>( );
            byteBuffer.AddRange( Serialize( data.x ) );
            byteBuffer.AddRange( Serialize( data.y ) );
            byteBuffer.AddRange( Serialize( data.z ) );
            return byteBuffer.ToArray( );
        }
        public static byte[ ] Serialize(Identifier data)
        {
            List<byte> byteBuffer = new List<byte>( );
            byteBuffer.AddRange( Serialize( data.path ) );
            byteBuffer.AddRange( Serialize( data.value ) );

            return byteBuffer.ToArray( );
        }

        public static int ToInt( byte[ ] data, int readPos = 0)
        {
            return BitConverter.ToInt32( data, readPos );
        }
        public static float ToFloat( byte[ ] data, int readPos = 0)
        {
            return BitConverter.ToSingle( data, readPos );
        }
        public static bool ToBool( byte[ ] data, int readPos = 0 )
        {
            return BitConverter.ToBoolean( data, readPos );
        }

        public static long ToLong( byte[ ] data, int readPos = 0 )
        {
            return BitConverter.ToInt64( data, readPos );
        }

        public static string ToString( byte[ ] data, int readPos = 0)
        {
            int sizeMarker = BitConverter.ToInt32( data, readPos );

            byte[ ] rawStringBuffer = new byte[ data.Length - 4 ];
            Array.Copy( data, readPos + 4, rawStringBuffer, 0, sizeMarker );

            return Encoding.Unicode.GetString( rawStringBuffer );
        }
        public static Vec2i ToVec2i( byte[ ] data, int readPos = 0 )
        {
            int x = ToInt( data );
            int y = ToInt( data, readPos + 4 );
            return new Vec2i( x, y );
        }

        public static Vec3f ToVec3f( byte[ ] data, int readPos = 0 )
        {
            float x = ToFloat( data );
            float y = ToFloat( data, readPos + 4 );
            float z = ToFloat( data, readPos + 8 );

            return new Vec3f( x, y, z );
        }
    }
}
