using FluentAssertions;
using Xunit;

namespace Bowling.MyVer.Test
{

    // ボウリングの1ゲームのスコアを求める
    /*
     - [ ] 倒したピンを追加する
       - [ ] 倒したピンはスコアに合算される
         * [x] 初期状態のスコアは0
         * [x] 倒したピンが0の時スコアは0を返す
         * [x] 倒したピンが1の時スコアは1を返す 
         * [x] 倒したピンが1と2の時スコアは3を返す 
       - [ ] 1回で追加できるピンの数は0から10
         * [x] マイナス値を追加すると例外発生
         * [ ] 0から10の値を追加すると正常終了
         * [ ] 11以上の値を追加すると例外発生
       - [ ] 1フレームに2回ピンの数を追加できる
         * [x] 初期状態のフレームNoは1
         * [x] 2回ピンを追加するとフレームNoは2
         * [x] 4回ピンを追加するとフレームNoは3
         * [ ] TODO ストライク周りの話が出てきそう
       - [ ] 1から9フレームは1フレームで追加できるピンの合計数は0から10
         * TODO

       - 仕様候補
         - [ ] 1フレームの1回目のピンの追加でピンの数が10の場合、2回目のピンの数の追加はできない（ストライク。次のフレームに移る）
         - [ ] 10フレームのみの例外あり
           - [ ] ※後で追記
     - [ ] 現時点のフレームとそのフレームのスコアを表示する。
     - [ ] 現時点の全フレームのスコアを表示する。

     - [ ] あとで
       - [ ] スピリットを表すフラグが欲しい
     */



    namespace 倒したピンを追加する
    {
        public class 倒したピンはスコアに合算される
        {
            private readonly Game _target = new Game();

            [Fact]
            internal void 初期状態のスコアは0()
            {
                // Given
                // When
                // Then
                _target.Score.Should().Be(0);

            }

            [Fact]
            internal void 倒したピンが0の時スコアは0を返す()
            {
                // Given
                // When
                _target.Add(0);

                // Then
                _target.Score.Should().Be(0);

            }

            [Fact]
            internal void 倒したピンが1の時スコアは1を返す()
            {
                // Given
                // When
                _target.Add(1);

                // Then
                _target.Score.Should().Be(1);

            }

            [Fact]
            internal void 倒したピンが1と2の時スコアは3を返す()
            {
                // Given
                // When
                _target.Add(1);
                _target.Add(2);

                // Then
                _target.Score.Should().Be(3);
            }

        }


        public class _1フレームに2回ピンの数を追加できる
        {
            private readonly Game _target = new Game();

            [Fact]
            internal void 初期状態のフレームNoは1()
            {
                // Given
                // When
                // Then
                _target.Frame.Should().Be(1);

            }

            [Fact]
            internal void _2回ピンを追加するとフレームNoは2()
            {
                // Given
                // When
                _target.Add(1);
                _target.Add(2);

                // Then
                _target.Frame.Should().Be(2);

            }


            [Fact]
            internal void _4回ピンを追加するとフレームNoが3()
            {
                // Given
                // When
                _target.Add(1);
                _target.Add(2);
                _target.Add(3);
                _target.Add(4);

                // Then
                _target.Frame.Should().Be(3);

            }
        }


        public class _1回で追加できるピンの数は0から10
        {
            private readonly Game _target = new Game();

            [Fact]
            internal void マイナス値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _target.Add(-1);

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
                var act = () => _target.Add(pin);

                // Then
                act.Should().NotThrow();
            }

            [Fact]
            internal void _11以上の値を追加すると例外発生()
            {
                // Given
                // When
                var act = () => _target.Add(11);

                // Then
                act.Should().Throw<BowlingAppException>().WithMessage("The number of pins that can be added at one time is 0-10.(pin=11)");
            }


        }

        public class _1から9フレームは1フレームで追加できるピンの合計数は0から10
        {
            private readonly Game _target = new Game();

            // TODO
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
