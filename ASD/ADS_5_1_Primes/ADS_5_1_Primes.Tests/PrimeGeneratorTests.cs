using Xunit;
using ADS_5_1_Primes;
using FluentAssertions;

namespace ADS_5_1_Primes.Tests
{

    namespace PrimeGeneratorTests
    {
        /*
         * ‘f”i‚»‚·‚¤A‰p: prime numberj‚Æ‚ÍA2 ˆÈã‚Ì©‘R”‚ÅA³‚Ì–ñ”‚ª 1 ‚Æ©•ª©g‚Ì‚İ‚Å‚ ‚é‚à‚Ì‚Ì‚±‚Æ‚Å‚ ‚éB
         * - [ ] w’è‚µ‚½”’l‚Ü‚Å‚Ì‘f”‚ğ¶¬‚·‚é
         *   - [ ] 1 ˆÈ‰º‚ğ“n‚·‚Æ [] ‚ğ•Ô‚·
         *   - [ ] 2 ‚ğ“n‚·‚Æ [2] ‚ğ•Ô‚·
         */


        public class _‘f”‚ğ¶¬‚·‚é
        {
            [Fact]
            public void _2‚ğ“n‚·‚Æ2‚ğ•Ô‚·()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(2).Should().Equal(2);
            }

            [Fact]
            public void _3‚ğ“n‚·2‚Æ3‚ğ•Ô‚·()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(3).Should().Equal(2, 3);
            }
        }

        public class _‘f”‚ğ¶¬‚Å‚«‚È‚¢ê‡‚Í‚©‚ç”z—ñ‚ğ•Ô‚·
        {
            [Fact]
            public void _1‚ğ“n‚·‚Æ‹ó”z—ñ‚ğ•Ô‚·()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(1).Should().HaveCount(0);
            }
        }
    }
}