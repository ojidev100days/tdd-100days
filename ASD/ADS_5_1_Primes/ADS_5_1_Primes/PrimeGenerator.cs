﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_5_1_Primes
{
    internal class PrimeGenerator
    {
        /// <summary>
        /// 指定された値までの素数を生成します。
        /// 素数を見つけるアルゴリズムとして [エラトステネスの篩](https://ja.wikipedia.org/wiki/%E3%82%A8%E3%83%A9%E3%83%88%E3%82%B9%E3%83%86%E3%83%8D%E3%82%B9%E3%81%AE%E7%AF%A9) を使用します。
        /// <para>
        /// キュレネのエラトステネスは紀元前 276 年にリビアのキュレネに生まれ、紀元前 194 年アレキサンドリアで没した。
        /// 最初に地球の全集を計算した人である。
        /// また、閏年の暦の作成に取り組んでいたことでも知られ、アレキサンドリアの図書館を管理していた人物でもある。
        /// </para>
        /// <para>
        /// このアルゴリズムは単純である。2から始まる整数配列を与え、2の倍数をすべて消す。
        /// まだ消えていない次の整数を見つけ、その倍数をすべて削除する。
        /// 一番大きい数の平方根を超えるまで、この作業を繰り返す。
        /// </para>
        /// </summary>
        /// <param name="maxValue">素数を見つける最大の数</param>
        /// <returns>素数の配列</returns>
        public static int[] GeneratePrimes(int maxValue)
        {
            // 素数が生成できない数値の場合、から配列を返す
            if (maxValue <= 1) return new int[0];

            var unCrossed = InitializeArrayOfIntegers(maxValue);
            CrossOutMultiples(unCrossed);
            return PutUncrossedIntegersIntoResult(unCrossed);
        }


        /// <summary>
        /// 篩の初期化
        /// </summary>
        /// <param name="maxValue"></param>
        private static bool[] InitializeArrayOfIntegers(int maxValue)
        {
            // 宣言
            var isCrossed = new bool[maxValue + 1];

            // 配列を true に初期化
            for (int i = 2; i < isCrossed.Length; i++)
            {
                isCrossed[i] = false;
            }

            return isCrossed;
        }

        private static void CrossOutMultiples(bool[] isCrossed)
        {

            // ふるい落とす
            for (int i = 2; i < CaclMaxPrimeFactor(isCrossed); i++)
            {
                if (NotCrossed(isCrossed, i))
                {
                    CrossOutMultiplesOf(isCrossed, i);
                }
            }
        }

        private static void CrossOutMultiplesOf(bool[] isCrossed, int i)
        {
            // i がのぞかれていなければ、その倍数を除く
            for (int multiple = 2 * i; multiple < isCrossed.Length; multiple += i)
            {
                isCrossed[multiple] = true; // 倍数は素数ではない
            }
        }

        private static bool NotCrossed(bool[] isCrossed, int i)
        {
            return !isCrossed[i];
        }

        private static int CaclMaxPrimeFactor(bool[] isCrossed)
        {
            // p の倍数をすべて削除する。ただし、p は素数である。
            // したがって、削除される倍数はすべて、素数因子 p と倍数因子 q をかけ合わせた数として表現できる。
            // もし、p が配列のサイズの平方根よりも大きい場合は、倍数因子 q が1より大きくなることはあり得ない。
            // したがって、p は配列に格納されている数の中で最大の素因数であり、同時に繰り返しの上限であることになる
            return (int)Math.Sqrt(isCrossed.Length) + 1;
        }

        private static int[] PutUncrossedIntegersIntoResult(bool[] isCrossed)
        {
            var primes = new int[NumberOfUnCrossedIntegers(isCrossed)];

            // 素数の抜き出し
            for (int i = 2, j = 0; i < isCrossed.Length; i++)
            {
                if (NotCrossed(isCrossed, i)) primes[j++] = i;
            }

            return primes;
        }

        private static int NumberOfUnCrossedIntegers(bool[] isCrossed)
        {
            // 見つけた素数の個数をカウント
            int count = 0;
            for (int i = 2; i < isCrossed.Length; i++)
            {
                if (!isCrossed[i]) count++;
            }

            return count;
        }
    }
}
