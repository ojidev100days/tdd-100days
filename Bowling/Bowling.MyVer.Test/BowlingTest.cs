﻿using System;
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
     *   「スペア」となった場合、得点は、その投球に続く1回の投球で倒したピンの数の合計となる。
     * 10フレーム目で「ストライク」とった場合、「ストライク」の得点を計上するために、プレーヤーはもう2回投球することができる。
     * 10フレーム目で「スペア」とった場合、「スペア」の得点を計上するために、プレーヤーはもう1回投球することができる。
     */
    /*
     - [x] 倒したピンを追加する
       - [x] 1回で追加できるピンの数は0から10
         * [x] マイナス値を追加すると例外発生
         * [x] 0から10の値を追加すると正常終了
         * [x] 11以上の値を追加すると例外発生

       - [x] 1から9フレーム
         - [x] 追加できるピンの合計数は10まで
           * [x] 1と10の組み合わせで追加すると例外発生
           * [x] 9と2の組み合わせで追加すると例外発生
         - [x] 1フレームに2回ピンの数を追加できる
           * [x] 初期状態のフレームNoは0
           * [x] 2回ピンを追加するとフレームNoは1
           * [x] 4回ピンを追加するとフレームNoは2
           * [x] ストライクを取るとフレームは次に進む


     - [x] 得点を計算する
       - [x] ストライク以外
         - [x] 倒したピンは得点に合算される
           * [x] 初期状態の得点は0
           * [x] 倒したピンが0の時得点は0を返す
           * [x] 倒したピンが1の時得点は1を返す 
           * [x] 倒したピンが1と2の時得点は3を返す 
       - [x] ストライクの場合
         * [x] その投球に続く２回の投球で倒したピンの数の合計となる
         * [x] その投球に続く２回の投球がまだない場合、スコアは0とする
       - [x] スペアの場合

     - [ ] 10フレームのみ
       - [ ] ストライクを取るともう2回ピンを追加できる
       - [ ] スペアをとるともう1回ピンを追加できる
       - [ ] 上記以外は2回便を追加できる。

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

        namespace _1から9フレーム
        {
            namespace ストライク以外
            {
                public class ピンを2回倒すと次のフレームに進む
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
                    [InlineData(new int[] { 1, 2 }, 2)]
                    [InlineData(new int[] { 1, 2, 3 }, 2)]
                    [InlineData(new int[] { 1, 2, 3, 4 }, 3)]
                    internal void ピンを2回倒すごとにフレームが進む(int[] pins, int expectedFrameNo)
                    {
                        // Given
                        // When
                        var target = _initialState.Add(pins);

                        // Then
                        target.CurrentFrameNo.Should().Be(expectedFrameNo);

                    }

                    [Theory]
                    [InlineData(new int[] { 10 }, 2)]
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


                public class 追加できるピンの合計数は10まで
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

            namespace ストライクの場合
            {

                public class ストライクを取ると次のフレームに進む
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
                    [InlineData(new int[] { 10 }, 2)]
                    [InlineData(new int[] { 10, 10 }, 3)]
                    [InlineData(new int[] { 10, 10, 10 }, 4)]
                    internal void ストライクを取るとフレームは次に進む(int[] pins, int expectedFrameNo)
                    {
                        // Given
                        // When
                        var target = _initialState.Add(pins);

                        // Then
                        target.CurrentFrameNo.Should().Be(expectedFrameNo);

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


    namespace ストライクとスペア以外の得点を計算する
    {
        public class 倒したピンは得点に合算される
        {
            private readonly Game _initialState = new();

            [Fact]
            internal void 初期状態の得点は0()
            {
                // Given
                // When
                // Then
                _initialState.TotalScore.Should().Be(0);

            }

            [Theory]
            [InlineData(new[] { 0 }, 0, "(TDB)")]
            [InlineData(new[] { 1 }, 0, "(TDB)")]
            [InlineData(new[] { 9 }, 0, "(TDB)")]
            [InlineData(new[] { 1, 2, 3 }, 3, "(1+2) + (TDB)")]
            [InlineData(new[] { 1, 2, 3, 4 }, 10, "(1+2) + (3+4)")]
            internal void 完了していないフレームのスコアは計算しない(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);

            }
        }
    }

    namespace ストライクとスペアの得点を計算する
    {
        public class ストライクのスコアは後の2回の投球を加算する
        {
            private readonly Game _initialState = new();

            [Fact]
            internal void ストライク後の投球なしの時は得点は0を返す()
            {
                // Given
                // When
                var target = _initialState.Add(10);

                // Then
                target.TotalScore.Should().Be(0);
            }

            [Fact]
            internal void ストライク後の投球が1回の時は得点は0を返す()
            {
                // Given
                // When
                var target = _initialState.Add(10).Add(1);

                // Then
                target.TotalScore.Should().Be(0);
            }

            [Theory]
            [InlineData(new int[] { 10, 1, 2 }, 16, "Strike(10+1+2) + (1+2)")]
            [InlineData(new int[] { 10, 0, 1 }, 12, "Strike(10+0+1) + (0+1)")]
            [InlineData(new int[] { 10, 0, 0 }, 10, "Strike(10+0+0) + (0+0)")]
            [InlineData(new int[] { 10, 10, 2 }, 22, "Strike(10+10+2) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 2, 3 }, 42, "Strike(10+10+2) + Strike(10+2+3) + (2+3)")]
            [InlineData(new int[] { 10, 10, 10 }, 30, "Strike(10+10+2) + Strike(TBD) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 10, 3 }, 53, "Strike(10+10+10) + Strike(10+10+3) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 10, 3, 4 }, 77, "Strike(10+10+10) + Strike(10+10+3) + Strike(10+3+4) + (3+4)")]
            [InlineData(new int[] { 10, 10, 10, 10 }, 60, "Strike(10+10+10) + Strike(10+10+10) + Strike(TDB)")]
            [InlineData(new int[] { 10, 10, 10, 10, 4 }, 84, "Strike(10+10+10) + Strike(10+10+10) + Strike(10+10+4), Strike(TDB)")]
            [InlineData(new int[] { 10, 10, 10, 10, 4, 5 }, 112, "Strike(10+10+10) + Strike(10+10+10) + Strike(10+10+4), Strike(10+4+5) + (4+5)")]
            internal void ストライクは後の2回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }


            [Theory]
            [InlineData(new int[] { 10, 1, 9 }, 20, "Strike(10+1+9) + Spare(TBD)")]
            [InlineData(new int[] { 10, 1, 9, 1 }, 31, "Strike(10+1+9) + Spare(1+9+1) + (TDB)")]
            internal void ストライクの後がスペアでもストライクは後の2回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }

        }

        public class スペアのスコアは後の1回の投球を加算する
        {
            private readonly Game _initialState = new();

            [Fact]
            internal void スペア後の投球なしの時は得点は0を返す()
            {
                // Given
                // When
                var target = _initialState.Add(1, 9);

                // Then
                target.TotalScore.Should().Be(0);
            }

            [Theory]
            [InlineData(new int[] { 1, 9, 1 }, 11, "Spare(1+9+1) + (TBD)")]
            [InlineData(new int[] { 1, 9, 0 }, 10, "Spare(1+9+0) + (TBD)")]
            [InlineData(new int[] { 1, 9, 1, 2 }, 14, "Spare(1+9+1) + (1+2)")]
            [InlineData(new int[] { 1, 9, 0, 1 }, 11, "Spare(1+9+0) + (0+1)")]
            [InlineData(new int[] { 1, 9, 1, 0 }, 12, "Spare(1+9+1) + (1+0)")]
            [InlineData(new int[] { 1, 9, 1, 9 }, 11, "Spare(1+9+1) + Spare(TBD)")]
            [InlineData(new int[] { 1, 9, 1, 9, 1 }, 22, "Spare(1+9+1) + Spare(1+9+1) + (TDB)")]
            internal void スペアは後の1回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }


            [Theory]
            [InlineData(new int[] { 1, 9, 10 }, 20, "Spare(1+9+10) + Strike(TBD)")]
            [InlineData(new int[] { 1, 9, 10, 1 }, 20, "Spare(1+9+10) + Strike(TBD)")]
            [InlineData(new int[] { 1, 9, 10, 1, 2 }, 36, "Spare(1+9+10) + Strike(10+1+2) + (1+2)")]
            internal void スペアの後がストライクでもスペアは後の1回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }

        }

    }

    namespace 最終フレームの得点を計算する
    {

        public class ストライクを取るともう2回ピンを追加できる
        {
            private readonly Game _initialState = new();

            private readonly Frame _gutter = new Frame(0, 0);

            [Fact]
            internal void ストライク後の投球なしの時は得点は0を返す()
            {

                throw new NotImplementedException("ピンの追加問題を可決してから");

                // Given
                // When
                var target = _initialState.Add(_gutter, _gutter, _gutter, _gutter, _gutter, _gutter, _gutter, _gutter, _gutter); // 9フレームまでgutter
                target = target.Add(10);

                // Then
                target.TotalScore.Should().Be(0);
            }

            //[Fact]
            internal void ストライク後の投球が1回の時は得点は0を返す()
            {
                // Given
                // When
                var target = _initialState.Add(10).Add(1);

                // Then
                target.TotalScore.Should().Be(0);
            }

            //[Theory]
            [InlineData(new int[] { 10, 1, 2 }, 16, "Strike(10+1+2) + (1+2)")]
            [InlineData(new int[] { 10, 0, 1 }, 12, "Strike(10+0+1) + (0+1)")]
            [InlineData(new int[] { 10, 0, 0 }, 10, "Strike(10+0+0) + (0+0)")]
            [InlineData(new int[] { 10, 10, 2 }, 22, "Strike(10+10+2) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 2, 3 }, 42, "Strike(10+10+2) + Strike(10+2+3) + (2+3)")]
            [InlineData(new int[] { 10, 10, 10 }, 30, "Strike(10+10+2) + Strike(TBD) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 10, 3 }, 53, "Strike(10+10+10) + Strike(10+10+3) + Strike(TBD)")]
            [InlineData(new int[] { 10, 10, 10, 3, 4 }, 77, "Strike(10+10+10) + Strike(10+10+3) + Strike(10+3+4) + (3+4)")]
            [InlineData(new int[] { 10, 10, 10, 10 }, 60, "Strike(10+10+10) + Strike(10+10+10) + Strike(TDB)")]
            [InlineData(new int[] { 10, 10, 10, 10, 4 }, 84, "Strike(10+10+10) + Strike(10+10+10) + Strike(10+10+4), Strike(TDB)")]
            [InlineData(new int[] { 10, 10, 10, 10, 4, 5 }, 112, "Strike(10+10+10) + Strike(10+10+10) + Strike(10+10+4), Strike(10+4+5) + (4+5)")]
            internal void ストライクは後の2回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }


            //[Theory]
            [InlineData(new int[] { 10, 1, 9 }, 20, "Strike(10+1+9) + Spare(TBD)")]
            [InlineData(new int[] { 10, 1, 9, 1 }, 31, "Strike(10+1+9) + Spare(1+9+1) + (TDB)")]
            internal void ストライクの後がスペアでもストライクは後の2回の投球を加算する(int[] hitPins, int expectTotalScore, string because)
            {
                // Given
                // When
                var target = _initialState.Add(hitPins);

                // Then
                target.TotalScore.Should().Be(expectTotalScore, because);
            }


        }


    }
}
