using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_Test
{
    public static class CoreExtensionMethods
    {
        // http://stackoverflow.com/a/2683487/1233003
        public static T Clamp<T> (this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
