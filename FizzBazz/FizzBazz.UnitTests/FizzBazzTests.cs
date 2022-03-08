using FluentAssertions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace FizzBazz.UnitTests
{

    // * テストは動作するドキュメントであってほしい
    // * テストのテストをどうやるか？ -> 最初にテストを失敗させる
    // * 3つの歩幅
    //   * テスト -> 仮実装 -> 三角測量 -> 実装
    //     * ひどい実装で、最短でGreenにする（仮実装）
    //     * 茶番だが、茶番によってテストの正しさを担保する
    //     * 仮実装から三角測量をすることで、正しい実装にする
    //   * テスト -> 仮実装 -> 実装
    //     * 仮実装から即実装できるようであれば、三角測量は飛ばして、即実装してよい
    //   * テスト -> 実装
    //     * プログラマーが自信がある状態であれば、実装しちゃえば良い（明白な実装）

    namespace FizzBazzTests
    {

        /*
         - [x] 数を文字列に変換する
           - [x] 1を渡すと文字列1を返す -> 仮実装
           - [x] 2を渡すと文字列2を返す -> 三角測量
         - [x] 3の倍数の時は、数の代わりに「Fizz」に変換する
           - [x] 3を渡すと文字列Fizzを返す -> 仮実装- > 実装
         - [x] 5の倍数の時は、数の代わりに「Buzz」に変換する -> 明白な実装
           - [x] 5を渡すと文字列5を返す -> 実装
         - [x] 3と５両方の倍数の時は、数の代わりに「FizzBuzz」に変換する
           - [x] 15を渡すと文字列「FizzBuzz」を返す -> 実装
           - [x] 0を渡すと文字列FizzBuzz「」を返す -> 実装
         - [x] 1からnまで
           - [x] 1から16までテストする
         */

        namespace Convertメソッドは数を文字列に変換する
        {
            public class _3の倍数の時は数の代わりにFizzに変換する
            {
                readonly FizzBuzzConverter converter = new();

                [Fact]
                public void _3を渡すと文字列Fizzを返す()
                {
                    // given, when, then
                    converter.Convert(3).Should().Be("Fizz");
                }
            }

            public class _5の倍数の時は数の代わりにBuzzに変換する
            {
                readonly FizzBuzzConverter converter = new();

                [Fact]
                public void _5を渡すと文字列Buzzを返す()
                {
                    // given, when, then
                    converter.Convert(5).Should().Be("Buzz");
                }
            }

            public class _3と5の倍数の時は数の代わりにFizzBazzに変換する
            {
                readonly FizzBuzzConverter converter = new();

                [Fact]
                public void _15を渡すと文字列FizzBazzを返す()
                {
                    // given, when, then
                    converter.Convert(15).Should().Be("FizzBuzz");
                }

                [Fact]
                public void _0を渡すと文字列FizzBazzを返す()
                {
                    // given, when, then
                    converter.Convert(0).Should().Be("FizzBuzz");
                }
            }

            public class _0の時は数の代わりにFizzBazzに変換する
            {
                readonly FizzBuzzConverter converter = new();

                [Fact]
                public void _0を渡すと文字列FizzBazzを返す()
                {
                    // given, when, then
                    converter.Convert(0).Should().Be("FizzBuzz");
                }
            }


            public class その他の数の時はそのまま文字列に変換する
            {
                readonly FizzBuzzConverter converter = new();

                [Fact]
                public void _1を渡すとそのままの文字列を返す()
                {
                    // given, when, then
                    converter.Convert(1).Should().Be("1");
                }

            }


            public class _0からnまでのテスト
            {
                readonly FizzBuzzConverter converter = new();

                [Theory]
                [InlineData(0, "FizzBuzz")]
                [InlineData(1, "1")]
                [InlineData(2, "2")]
                [InlineData(3, "Fizz")]
                [InlineData(4, "4")]
                [InlineData(5, "Buzz")]
                [InlineData(6, "Fizz")]
                [InlineData(7, "7")]
                [InlineData(8, "8")]
                [InlineData(9, "Fizz")]
                [InlineData(10, "Buzz")]
                [InlineData(11, "11")]
                [InlineData(12, "Fizz")]
                [InlineData(13, "13")]
                [InlineData(14, "14")]
                [InlineData(15, "FizzBuzz")]
                [InlineData(16, "16")]
                public void _0から16までのテスト(int value, string expected)
                {
                    // given, when, then
                    converter.Convert(value).Should().Be(expected);
                }
            }

        }
    }
}