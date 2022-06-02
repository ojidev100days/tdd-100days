using FluentAssertions;
using Xunit;

namespace Bowling.MyVer.Test
{

    // ボウリングの1ゲームの得点を求める
    /*
     * ボウリングは細長いレーンの先に並んだ10本のピンに向かってボールを投げ、何本倒したかを競うゲームである。
     * ゲームは10フレーム投球し、倒したピンの合計で競う。
     * 各フレームでは、毎回新しく10本のピンがセットされ、プレーヤーはフレーム内で2回投球できる。
     *   1フレーム内で倒したピンの数の合計が、そのフレームの得点となる。
     * プレーヤーが1回の投球ですべてのピンを倒すことを「ストライク」と呼ぶ。
     *   「ストライク」となった場合、そのフレームは終了する。
     *   「ストライク」となった場合、得点は、その投球に続く２回の投球で倒したピンの数の合計となる。
     * プレーヤーが2回の投球ですべてのピンを倒すことを「スペア」と呼ぶ。
     *   「スペア」となった場合、得点は、その投球に続く1会の投球で倒したピンの数の合計となる。
     * 10フレーム目で「ストライク」とった場合、「ストライク」の得点を計上するために、プレーヤーはもう2回投球することができる。
     * 10フレーム目で「スペア」とった場合、「スペア」の得点を計上するために、プレーヤーはもう1回投球することができる。
     - [ ] 倒したピンを追加する

       - [x] 1回で追加できるピンの数は0から10
         * [x] マイナス値を追加すると例外発生
         * [x] 0から10の値を追加すると正常終了
         * [x] 11以上の値を追加すると例外発生

       - [x] 1から9フレームはピンの合計数は10まで
         * [x] 1と10の組み合わせで追加すると例外発生
         * [x] 9と2の組み合わせで追加すると例外発生

       - [ ] 1フレームに2回ピンの数を追加できる
         * [x] 初期状態のフレームNoは1
         * [x] 2回ピンを追加するとフレームNoは2
         * [x] 4回ピンを追加するとフレームNoは3
         * [x] ストライクを取るとフレームは次に進む

     - [ ] 得点を計算する
       - [ ] 倒したピンは得点に合算される
         * [x] 初期状態の得点は0
         * [x] 倒したピンが0の時得点は0を返す
         * [x] 倒したピンが1の時得点は1を返す 
         * [x] 倒したピンが1と2の時得点は3を返す 
       - [ ] ストライクの場合
         * [x] 


       - [ ] 10フレームのみの例外
         - [ ] ストライクを取るともう2回ピンを追加できる
         - [ ] スペアをとるともう1回ピンを追加できる

         * TODO

       - 仕様候補
         - [ ] 10フレームのみの例外あり
           - [ ] ※後で追記
     - [ ] 現時点のフレームとそのフレームの得点を表示する。
     - [ ] 現時点の全フレームの得点を表示する。

     - [ ] あとで
       - [ ] スピリットを表すフラグが欲しい
     */



    namespace 倒したピンを追加する
    {
        public class 倒したピンは得点に合算される
        {
            private readonly Game _initialState = new Game();

            [Fact]
            internal void 初期状態の得点は0()
            {
                // Given
                // When
                // Then
                _initialState.TotalScore.Should().Be(0);

            }

            [Fact]
            internal void 倒したピンが0の時得点は0を返す()
            {
                // Given
                // When
                var target = _initialState.Add(0);

                // Then
                target.TotalScore.Should().Be(0);

            }

            [Fact]
            internal void 倒したピンが1の時得点は1を返す()
            {
                // Given
                // When
                var target = _initialState.Add(1);

                // Then
                target.TotalScore.Should().Be(1);

            }

            [Fact]
            internal void 倒したピンが1と2の時得点は3を返す()
            {
                // Given
                // When
                var target = _initialState.Add(1).Add(2);

                // Then
                target.TotalScore.Should().Be(3);
            }

        }


        public class _1フレームに2回ピンの数を追加できる
        {
            private readonly Game _initialState = new Game();

            [Fact]
            internal void 初期状態のフレームNoは1()
            {
                // Given
                // When
                // Then
                _initialState.CurrentFrameNo.Should().Be(1);

            }

            [Theory]
            [InlineData(new int[] { 1 }, 1)]
            [InlineData(new int[] { 1, 2 }, 1)]
            [InlineData(new int[] { 1, 2, 3 }, 2)]
            [InlineData(new int[] { 1, 2, 3, 4 }, 2)]
            internal void ストライク以外は2投球に1回フレームが進む(int[] pins, int expectedFrameNo)
            {
                // Given
                // When
                var target = _initialState.Add(pins);

                // Then
                target.CurrentFrameNo.Should().Be(expectedFrameNo);

            }

            [Theory]
            [InlineData(new int[] { 10 }, 1)]
            [InlineData(new int[] { 10, 1 }, 2)]
            [InlineData(new int[] { 10, 10, 1 }, 3)]
            internal void ストライクを取るとフレームは次に進む(int[] pins, int expectedFrameNo)
            {
                // Given
                // When
                var target = _initialState.Add(pins);

                // Then
                target.CurrentFrameNo.Should().Be(expectedFrameNo);

            }

        }


        public class _1回で追加できるピンの数は0から10
        {
            private readonly Game _initialState = new Game();

            [Fact]
            internal void マイナス値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _initialState.Add(-1);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage("The number of pins that can be added at one time is 0-10.(hitPin=-1)");
            }

            [Theory]
            [InlineData(0)]
            [InlineData(10)]
            internal void _0から10の値を追加すると正常終了(int pin)
            {
                // Given
                // When
                var act = () => _initialState.Add(pin);

                // Then
                act.Should().NotThrow();
            }

            [Fact]
            internal void _11以上の値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _initialState.Add(11);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage("The number of pins that can be added at one time is 0-10.(hitPin=11)");
            }


        }



        namespace _1フレームで追加できるピンの合計数
        {

            public class _1から9フレームは追加できるピンの合計数は10まで
            {
                private readonly Game _initialState = new Game();

                [Fact]
                internal void _1と10の組み合わせで追加すると例外発生()
                {
                    // Given
                    // When
                    var target = _initialState.Add(1);
                    var act = () => target.Add(10);

                    // Then
                    act.Should().Throw<BowlingAppException>().WithMessage("The total number of pins that can be added in a frame is limited to 10.(frame=[[1]], hitPin=10)");
                }

                [Fact]
                internal void _9と2の組み合わせで追加すると例外発生()
                {
                    // Given
                    // When
                    var target = _initialState.Add(9);
                    var act = () => target.Add(2);

                    // Then
                    act.Should().Throw<BowlingAppException>().WithMessage("The total number of pins that can be added in a frame is limited to 10.(frame=[[9]], hitPin=2)");
                }
            }

        }

    }



    // TODO 今テストが書けないので後で
    //namespace 倒したピンの数を追加できる
    //{
    //    public class ピンの数は0から10の範囲で追加する
    //    {
    //        [Fact]
    //        public void _0を追加したら＿＿＿()
    //        {
    //            // かけない、、、
    //        }
    //    }
    //}

}
