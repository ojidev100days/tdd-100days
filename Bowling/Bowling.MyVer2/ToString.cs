using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.MyVer2
{
    internal static class Utils
    {
        public static string ToMsg<T>(this IReadOnlyList<T> list)
        {
            return "[" + string.Join(",", list) + "]";
        }
    }
}
