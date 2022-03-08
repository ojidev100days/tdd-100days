using Xunit;
using ADS_5_1_Primes;
using FluentAssertions;

namespace ADS_5_1_Primes.Tests
{

    namespace PrimeGeneratorTests
    {
        /*
         * �f���i�������A�p: prime number�j�Ƃ́A2 �ȏ�̎��R���ŁA���̖񐔂� 1 �Ǝ������g�݂̂ł�����̂̂��Ƃł���B
         * - [ ] �w�肵�����l�܂ł̑f���𐶐�����
         *   - [ ] 1 �ȉ���n���� [] ��Ԃ�
         *   - [ ] 2 ��n���� [2] ��Ԃ�
         */


        public class _�f���𐶐�����
        {
            [Fact]
            public void _1��n���Ƌ�z���Ԃ�()
            {
                // given, when, then
                PrimeGenerator.GeneratePrimes(1).Should().HaveCount(0);
            }
        }
    }
}