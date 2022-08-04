using Bowling.MyVer2;

namespace Bowling.MyVer2.Test
{

    namespace 得点を計算する
    {

        public class 倒したピンの数の合計が得点となる
        {
            private readonly Game _target = new Game();

            [Fact]
            public void ゲーム開始時の得点は0()
            {
                // Given
                // When
                // Then
                _target.GetScore().Should().Be(0);
            }

            [Theory]
            [InlineData(new[] { 1, 2 }, 3, "(1+2)")]
            [InlineData(new[] { 1, 2, 3, 4 }, 10, "(1+2)+(3+4)")]
            public void フレームで倒したピンを合計する(int[] throwBalls, int expect, string becase)
            {
                // Given
                var target = _target.ThrowBall(throwBalls);
                // When
                // Then
                target.GetScore().Should().Be(expect, becase);
            }

            [Theory]
            [InlineData(new[] { 1 }, 0, "(TBD)")]
            [InlineData(new[] { 1, 2, 3 }, 3, "(1+2)+(TBD)")]
            public void 完了していないフレームは得点を計算しない(int[] throwBalls, int expect, string becase)
            {
                // Given
                var target = _target.ThrowBall(throwBalls);
                // When
                // Then
                target.GetScore().Should().Be(expect, becase);
            }
        }


        public class ストライクの場合は後の２回の投球で倒したピンの数をストライクの得点に加算する
        {
            private readonly Game _target = new();

            [Theory]
            //[InlineData(new[] { 10 }, 0, "(TBD)")]
            [InlineData(new[] { 10, 1 }, 0, "(TBD)")]
            public void ストライク後の２回の投球がない場合は得点を計算しない(int[] throwBalls, int expect, string becase)
            {
                // Given
                var target = _target.ThrowBall(throwBalls);
                // When
                // Then
                target.GetScore().Should().Be(expect, becase);
            }
        }
    }
}
