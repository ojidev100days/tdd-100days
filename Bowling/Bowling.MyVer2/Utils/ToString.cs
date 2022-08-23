using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2.Utils
{
    internal static class ToStringExtensions
    {
        public static string ToStr<T>(this IEnumerable<T> list)
        {
            return "[" + string.Join(",", list) + "]";
        }
    }
}
