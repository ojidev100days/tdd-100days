using System;
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

            var crossedOut = UnCrossIntegersUpTo(maxValue);
            CrossOutMultiples(crossedOut);
            return PutUncrossedIntegersIntoResult(crossedOut);
        }


        /// <summary>
        /// 篩の初期化
        /// </summary>
        /// <param name="maxValue"></param>
        private static bool[] UnCrossIntegersUpTo(int maxValue)
        {
            // 宣言
            var crossedOut = new bool[maxValue + 1];

            // 配列を true に初期化
            for (int i = 2; i < crossedOut.Length; i++)
            {
                crossedOut[i] = false;
            }

            return crossedOut;
        }

        private static void CrossOutMultiples(bool[] crossedOut)
        {

            // ふるい落とす
            for (int i = 2; i < CaclMaxPrimeFactor(crossedOut); i++)
            {
                if (NotCrossed(crossedOut, i))
                {
                    CrossOutMultiplesOf(crossedOut, i);
                }
            }
        }

        private static void CrossOutMultiplesOf(bool[] crossedOut, int i)
        {
            // i がのぞかれていなければ、その倍数を除く
            for (int multiple = 2 * i; multiple < crossedOut.Length; multiple += i)
            {
                crossedOut[multiple] = true; // 倍数は素数ではない
            }
        }

        private static bool NotCrossed(bool[] isCrossed, int i)
        {
            return !isCrossed[i];
        }

        private static int CaclMaxPrimeFactor(bool[] crossedOut)
        {
            // 配列に格納されているいかなる倍数も、その配列サイズの平方根に等しいか、それよりも小さい素数因子を持っている。
            // したがって、その平方根よりも大きな数の倍数をチェックする必要はない
            return (int)Math.Sqrt(crossedOut.Length);
        }

        private static int[] PutUncrossedIntegersIntoResult(bool[] crossedOut)
        {
            var primes = new int[NumberOfUnCrossedIntegers(crossedOut)];

            // 素数の抜き出し
            for (int i = 2, j = 0; i < crossedOut.Length; i++)
            {
                if (NotCrossed(crossedOut, i)) primes[j++] = i;
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
