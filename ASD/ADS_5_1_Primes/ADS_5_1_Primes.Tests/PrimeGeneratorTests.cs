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
         * - [ ] nまでの素数
         */


        public class _素数を生成する
        {
            [Fact]
            public void _2を渡すと2を返す()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(2).Should().Equal(2);
            }

            [Fact]
            public void _3を渡す2と3を返す()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(3).Should().Equal(2, 3);
            }
        }

        public class _素数を生成できない場合は空配列を生成する
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(1)]
            public void _空配列を返す(int value)
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(value).Should().BeEmpty();
            }
        }

        public class _nまでの素数を求める
        {
            [Fact]
            public void _100を渡すと25個の素数を返し２５番目の素数は97になる()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(100).Should().HaveCount(25);
                PrimeGenerator.GeneratePrimes(100)[24].Should().Be(97);
            }
        }
    }
}