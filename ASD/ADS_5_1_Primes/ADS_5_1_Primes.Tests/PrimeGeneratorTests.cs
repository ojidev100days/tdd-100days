using Xunit;
using ADS_5_1_Primes;
using FluentAssertions;

namespace ADS_5_1_Primes.Tests
{

    namespace PrimeGeneratorTests
    {
        /*
         * 素数（そすう、英: prime number）とは、2 以上の自然数で、正の約数が 1 と自分自身のみであるもののことである。
         * - [ ] 指定した数値までの素数を生成する
         *   - [ ] 1 以下を渡すと [] を返す
         *   - [ ] 2 を渡すと [2] を返す
         */


        public class _素数を生成する
        {
            [Fact]
            public void _1を渡すと空配列を返す()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(1).Should().HaveCount(0);
            }
        }
    }
}