using Assets.Core.Scripts.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Interfaces
{
    public interface Serializable<T>
    {
        public abstract SerialData ToSerial( );

        public static T FromSerial( SerialData data ) => throw new NotImplementedException( );
    }
}
