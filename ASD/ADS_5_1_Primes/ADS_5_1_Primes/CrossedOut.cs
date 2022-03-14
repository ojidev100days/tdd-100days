using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_5_1_Primes
{
    internal class CrossedOut
    {

        /// <summary>
        /// 篩の初期化
        /// </summary>
        /// <param name="maxValue"></param>
        public static CrossedOut UnCrossIntegersUpTo(int maxValue)
        {
            // 宣言
            var crossedOut = new bool[maxValue + 1];

            // 配列を true に初期化
            for (int i = 2; i < crossedOut.Length; i++)
            {
                crossedOut[i] = false;
            }

            return new CrossedOut(crossedOut);
        }



        private readonly bool[] _crossedOut;


        public int MaxPrimeFactor => (int)Math.Sqrt(_crossedOut.Length);

        public int Length => _crossedOut.Length;

        public CrossedOut(bool[] crossedOut)
        {
            this._crossedOut = crossedOut;
        }



        public void CrossOutMultiplesOf(int i)
        {
            // i がのぞかれていなければ、その倍数を除く
            for (int multiple = 2 * i; multiple < _crossedOut.Length; multiple += i)
            {
                _crossedOut[multiple] = true; // 倍数は素数ではない
            }
        }

        public bool NotCrossed(int i)
        {
            return !_crossedOut[i];
        }

        public int NumberOfUnCrossedIntegers()
        {
            // 見つけた素数の個数をカウント
            int count = 0;
            for (int i = 2; i < _crossedOut.Length; i++)
            {
                if (!_crossedOut[i]) count++;
            }

            return count;
        }

    }
}
