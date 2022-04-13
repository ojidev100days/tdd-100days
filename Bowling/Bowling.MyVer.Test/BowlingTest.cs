using FluentAssertions;
using Xunit;

namespace Bowling.MyVer.Test
{

    // ボウリングの1ゲームのスコアを求める
    /*
     - [ ] 倒したピンを追加する
       - [ ] 倒したピンはスコアに合算される
         * 初期状態のスコアは0
         * 倒したピンが0の時スコアは0を返す
         * 倒したピンが1の時スコアは1を返す 
         * 倒したピンが1と2の時スコアは3を返す 
       - [ ] ピンの数は0から10の範囲で追加する。
         * [ ] 
       - [ ] ピンは10フレームが完了するまで追加できる
       - [ ] 1フレームに2回ピンの数を追加できる。ただし例外あり。
         - [ ] 1フレームで追加で切るピンの数は、2回合わせて0から10。
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
            public void 初期状態のスコアは0()
            {
                // Given
                // When
                // Then
                _target.Score.Should().Be(0);

            }

            [Fact]
            public void 倒したピンが0の時スコアは0を返す()
            {
                // Given
                // When
                _target.Add(0);

                // Then
                _target.Score.Should().Be(0);

            }

            [Fact]
            public void 倒したピンが1の時スコアは1を返す()
            {
                // Given
                // When
                _target.Add(1);

                // Then
                _target.Score.Should().Be(1);

            }

            [Fact]
            public void 倒したピンが1と2の時スコアは3を返す()
            {
                // Given
                // When
                _target.Add(1);
                _target.Add(2);

                // Then
                _target.Score.Should().Be(3);
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
