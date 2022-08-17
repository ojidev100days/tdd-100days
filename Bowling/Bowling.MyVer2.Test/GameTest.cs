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
                _target.Score.Should().Be(0);
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
                target.Score.Should().Be(expect, becase);
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
                target.Score.Should().Be(expect, becase);
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
                target.Score.Should().Be(expect, becase);
            }
        }


        public class ゲーム終了時の得点を計算する
        {
            private readonly Game _target = new Game();

            [Theory]
            [InlineData(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300, "Parfect!")]
            [InlineData(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, "all gutter")]
            [InlineData(new[] { 7, 3, 9, 0, 8, 1, 10, 5, 5, 3, 3, 10, 10, 5, 3, 4, 6, 9 }, 146, "sp(7+3+9) + (9+0) + (8+1) + st(10+5+5) + sp(5+5+3) + (3+3) + st(10+10+5) + st(10+5+3) + (5+3) + sp(4+6+9)")]
            [InlineData(new[] { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6 }, 133, "(1+4) + (4+5) + sp(6+4+5) + sp(5+5+10) + st(10+0+1) + sp(7+3+6) + sp(6+4+10) + st(10+2+8) + sp(2+8+6)")]
            internal void _10フレームすべての得点を計算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _target.ThrowBall(hitPins);

                // Then
                target.Score.Should().Be(expectTotalScore, because);
            }
        }

    }
}
