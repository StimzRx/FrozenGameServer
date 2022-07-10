using Assets.Core.Scripts.Inventories;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Serialization
{
    public class SerialData
    {
        public SerialData( ) {  }
        public SerialData( List<byte> data )
        {
            rawBuffer = data;
        }
        public SerialData( byte[ ] data )
        {
            rawBuffer = data.ToList( );
        }


        public void ResetReaderPosition( )
        {
            readPosition = 0;
        }

        public byte[ ] GetRaw()
        {
            return (byte[])rawBuffer.ToArray( ).Clone();
        }

        public void Write( Item data )
        {
            if ( data is null )
            {
                return;
            }

            rawBuffer.AddRange(data.ToSerial( ).GetRaw( ).ToList( ));
        }

        public void Write( ItemStack data )
        {
            if ( data is null )
            {
                return;
            }

            rawBuffer.AddRange(data.ToSerial( ).GetRaw( ).ToList( ));
        }

        public void Write( SerialData data )
        {

        }

        public void Write( byte[ ] data)
        {
            if(data is null)
            {
                return;
            }

            rawBuffer.AddRange( data );
        }

        public void Write(List<byte> data)
        {
            if ( data is null )
                return;

            rawBuffer.AddRange( data );
        }

        public void Write( int data )
        {
            rawBuffer.AddRange( BitConverter.GetBytes( data ) );
        }
        public void Write( long data )
        {
            rawBuffer.AddRange( BitConverter.GetBytes( data ) );
        }
        public void Write( float data )
        {
            rawBuffer.AddRange( BitConverter.GetBytes( data ) );
        }
        public void Write( bool data )
        {
            rawBuffer.AddRange( BitConverter.GetBytes( data ) );
        }
        public void Write( short data )
        {
            rawBuffer.AddRange( BitConverter.GetBytes( data ) );
        }
        public void Write( string data )
        {
            if( data is null )
            {
                return;
            }

            List<byte> byteBuffer = new List<byte>( );
            byteBuffer.AddRange( BitConverter.GetBytes( data.Length ) );
            byteBuffer.AddRange( Encoding.Unicode.GetBytes( data ) );
            rawBuffer.AddRange( byteBuffer );
        }
        public void Write( Vec2i data )
        {
            Write( data.x );
            Write( data.y );
        }
        public void Write( Vec3f data )
        {
            Write( data.x );
            Write( data.y );
            Write( data.z );
        }
        public void Write( Identifier data )
        {
            Write( data.path );
            Write( data.value );
        }

        public int ReadInt( )
        {
            byte[ ] buff = rawBuffer.GetRange( readPosition, BYTES_NORMAL ).ToArray();

            readPosition += BYTES_NORMAL;

            return BitConverter.ToInt32( buff, 0);
        }
        public float ReadFloat( )
        {
            byte[ ] buff = rawBuffer.GetRange( readPosition, BYTES_NORMAL ).ToArray( );

            readPosition += BYTES_NORMAL;

            return BitConverter.ToSingle( buff, 0 );
        }
        public bool ReadBool( )
        {
            byte[ ] buff = rawBuffer.GetRange( readPosition, 1 ).ToArray( );

            readPosition += 1;

            return BitConverter.ToBoolean( buff, 0 );
        }

        public long ReadLong( )
        {
            byte[ ] buff = rawBuffer.GetRange( readPosition, BYTES_LONG ).ToArray( );

            readPosition += BYTES_LONG;

            return BitConverter.ToInt32( buff, 0 );
        }

        public string ReadString( )
        {
            byte[ ] buff = rawBuffer.GetRange( readPosition, BYTES_NORMAL ).ToArray( );
            int sizeMarker = BitConverter.ToInt32( buff, 0 );
            readPosition += BYTES_NORMAL;
            buff = rawBuffer.GetRange( readPosition, sizeMarker ).ToArray( );
            readPosition += sizeMarker;
            return Encoding.Unicode.GetString( buff );
        }
        public Vec2i ReadVec2i( )
        {
            int x = ReadInt( );
            int y = ReadInt( );
            return new Vec2i( x, y );
        }

        public Vec3f ReadVec3f( )
        {
            float x = ReadFloat( );
            float y = ReadFloat( );
            float z = ReadFloat( );

            return new Vec3f( x, y, z );
        }
        public Identifier ReadIdentifier()
        {
            string path = ReadString( );
            string val = ReadString( );

            return new Identifier( path, val );
        }


        protected List<byte> rawBuffer = new( );
        protected int readPosition = 0;

        private const int BYTES_SHORT = 2;
        private const int BYTES_NORMAL = 4;
        private const int BYTES_LONG = 8;
    }
}
