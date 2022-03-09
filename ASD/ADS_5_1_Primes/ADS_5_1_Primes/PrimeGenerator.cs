using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_5_1_Primes
{
    internal class PrimeGenerator
    {
        public static int[] GeneratePrimes(int maxValue)
        {
            // 素数が生成できない数値の場合、から配列を返す
            if (maxValue <= 1) return new　int[0];

            return new[] { 2 };
        }
    }
}
