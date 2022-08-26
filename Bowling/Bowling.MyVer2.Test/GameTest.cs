using Bowling.MyVer2;

namespace Bowling.MyVer2.Test
{


    namespace 投球する
    {

        public class _1回で追加できるピンの数は0から10
        {
            private readonly Game _initialGame = new Game();

            [Fact]
            internal void マイナス値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _initialGame.ThrowBall(-1);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage("The number of pins that can be added at one time is 0-10.(pin=-1)");
            }

            [Theory]
            [InlineData(0)]
            [InlineData(10)]
            internal void _0から10の値を追加すると正常終了(int pin)
            {
                // Given
                // When
                var act = () => _initialGame.ThrowBall(pin);

                // Then
                act.Should().NotThrow();
            }

            [Fact]
            internal void _11以上の値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _initialGame.ThrowBall(11);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage("The number of pins that can be added at one time is 0-10.(pin=11)");
            }
        }

        public class ゲーム終了後は投球できない
        {
            private readonly Game _initialGame = new();

            private static readonly int[][] _nineFrames = Enumerable.Range(0, 9).Select(x => new[] { 1, 0 }).ToArray();

            [Theory]
            [InlineData(new int[] { 0, 9 }, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 9, 2 }, "スペア")]
            [InlineData(new int[] { 10, 10, 10 }, "ストライク")]
            internal void _10フレーム完了まで投球できる(int[] lastFramePins, string because)
            {
                // Given
                // 10フレーム分の投球内容
                var throwBalls = _nineFrames.Concat(new int[][] { lastFramePins }).SelectMany(x => x).ToArray();
                // When
                var act = () => _initialGame.ThrowBall(throwBalls);

                // Then
                act.Should().NotThrow<BowlingAppException>(because);

            }

            [Theory]
            [InlineData(new int[] { 1, 0 }, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 9, 1 }, "スペア")]
            [InlineData(new int[] { 10, 10, 10 }, "ストライク")]
            internal void _10フレーム完了後は投球できない(int[] lastFramePins, string because)
            {
                // Given
                // 10フレーム分の投球内容
                var throwBalls = _nineFrames.Concat(new int[][] { lastFramePins }).SelectMany(x => x).ToArray();
                var target = _initialGame.ThrowBall(throwBalls);

                // When
                var act = () => target.ThrowBall(1);

                // Then
                act.Should().Throw<BowlingAppException>(because).WithMessage("The game is already over. (hitPins=*, currentIndex=*)");

            }


            [Theory]
            [InlineData(new int[] { 2, 7, 3 }, "スペア,ストライク、以外")]
            [InlineData(new int[] { 2, 8, 3, 4 }, "スペア")]
            [InlineData(new int[] { 10, 10, 10, 10 }, "ストライク")]
            internal void _10フレームを超える投球は受け付けない(int[] lastFramePins, string because)
            {
                // Given
                // 10フレーム分の投球内容
                var throwBalls = _nineFrames.Concat(new int[][] { lastFramePins }).SelectMany(x => x).ToArray();

                // When
                var act = () => _initialGame.ThrowBall(throwBalls); ;

                // Then
                act.Should().Throw<BowlingAppException>(because).WithMessage("The game is already over. (hitPins=*, currentIndex=*)");

            }

            [Theory]
            [InlineData(new int[] { 1, 0 }, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 9, 1 }, "スペア")]
            [InlineData(new int[] { 10, 10, 10 }, "ストライク")]
            internal void すべての投球が終わった場合ゲームは完了済みとなる(int[] lastFramePins, string because)
            {
                // Given
                // 10フレーム分の投球内容
                var throwBalls = _nineFrames.Concat(new int[][] { lastFramePins }).SelectMany(x => x).ToArray();

                // When
                var target = _initialGame.ThrowBall(throwBalls);

                // Then
                target.IsComplete.Should().BeTrue(because);
            }
        }
    }


    namespace フレーム
    {
        public class フレームには２回分の投球を含めることができる
        {

            private static readonly int[][] _nineFrames = Enumerable.Range(0, 9).Select(x => new[] { 1, 0 }).ToArray();

            private readonly Game _initialGame = new();

            [Fact]
            internal void 初期状態のフレームの数は0()
            {
                // Given
                // When
                // Then
                _initialGame.Frames.Should().BeEmpty();

            }

            [Theory]
            [InlineData(new int[] { 1 }, 1, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 2 }, 1, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 2, 3 }, 2, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 2, 3, 4 }, 2, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, "スペア、ストライク以外")]
            [InlineData(new int[] { 1, 9 }, 1, "スペア")]
            [InlineData(new int[] { 1, 9, 1 }, 2, "スペア")]
            [InlineData(new int[] { 1, 9, 1, 9 }, 2, "スペア")]
            [InlineData(new int[] { 1, 9, 2, 8, 3 }, 3, "スペア")]
            [InlineData(new int[] { 10 }, 1, "ストライク")]
            [InlineData(new int[] { 10, 10 }, 2, "ストライク")]
            [InlineData(new int[] { 10, 10, 10 }, 3, "ストライク")]
            internal void ピンを2回倒すごとにフレームが進む(int[] pins, int expectedFrameCount, string because)
            {
                // Given
                // When
                var target = _initialGame.ThrowBall(pins);

                // Then
                target.Frames.Count.Should().Be(expectedFrameCount, because);

            }

        }

        public class フレームに追加できるピンの合計数は10まで
        {
            private readonly Game _initialGame = new Game();

            [Theory]
            [InlineData(new int[] { 1, 10 })]
            [InlineData(new int[] { 2, 9 })]
            [InlineData(new int[] { 10, 3, 8 })]
            [InlineData(new int[] { 1, 9, 4, 7 })]
            internal void フレームのピンの合計数が11以上の場合は例外発生(int[] pins)
            {
                // Given
                // When
                var act = () => _initialGame.ThrowBall(pins);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage($"The total number of pins that can be added in a frame is limited to 10.(hitPinsInFrame=[*])");
            }
        }
    }

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


        public class ストライクの場合は後の2回の投球で倒したピンの数をストライクの得点に加算する
        {
            private readonly Game _target = new();

            [Theory]
            [InlineData(new[] { 10 }, 0, "(TBD)")]
            [InlineData(new[] { 10, 1 }, 0, "(TBD)")]
            [InlineData(new[] { 10, 1, 2 }, 16, "(10+1+2)+(1+2)")]
            [InlineData(new[] { 10, 10, 2 }, 22, "(10+10+2)+(TDB)")]
            [InlineData(new[] { 10, 10, 10 }, 30, "(10+10+10)+(TDB)+(TDB)")]
            public void フレームで倒したピンを合計する(int[] throwBalls, int expect, string becase)
            {
                // Given
                var target = _target.ThrowBall(throwBalls);
                // When
                // Then
                target.Score.Should().Be(expect, becase);
            }
        }

        public class スペアの場合は後の1回の投球で倒したピンの数をスペアの得点に加算する
        {
            private readonly Game _target = new();

            [Theory]
            [InlineData(new[] { 1, 9 }, 0, "(TBD)")]
            [InlineData(new[] { 1, 9, 2 }, 12, "(1+9+2)+(TDB)")]
            [InlineData(new[] { 1, 9, 2, 7 }, 21, "(1+9+2)+(2+7)")]
            [InlineData(new[] { 1, 9, 2, 8 }, 12, "(1+9+2)+(TDB)")]
            [InlineData(new[] { 1, 9, 2, 8, 3 }, 25, "(1+9+2)+(2+8+3)+(TDB)")]
            public void フレームで倒したピンを合計する(int[] throwBalls, int expect, string becase)
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
