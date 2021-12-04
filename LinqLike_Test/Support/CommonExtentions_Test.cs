using System;
using System.Collections.Generic;
using Tools.Support;
using Tools.ValueObject.SizeObj;
using Xunit;

namespace Tools_Test.Support
{
    /// <summary> <see cref="CommonExtentions"/>に対するテストです </summary>
    public class CommonExtentions_Test
    {
        /// <summary> <see cref="CommonExtentions.AssertNotExistNull(object?)"/>に対するテストです </summary>
        public class メンバにNullが含まれるかをテストできる
        {
            [Fact]
            public void Nullを含まないインスタンスだとFalseを返す()
            {
                Reco1? data = new Reco1(1);

                // チェック内容
                // 値型、参照型、リスト（空）、リスト（イント）、リスト（参照型）、リスト（Bool）、リスト（Enum）、辞書（文字、イント）
                1.AssertNotExistNull();
                data.AssertNotExistNull();
                new Reco2(data, data).AssertNotExistNull();
                new List<int?>().AssertNotExistNull();
                new List<int?>() { 1, 2, 3 }.AssertNotExistNull();
                new List<Reco1?>() { new(1), new(2), new(3) }.AssertNotExistNull();
                new List<bool?>() { true, false, true }.AssertNotExistNull();
                new List<EnumSample?>() { EnumSample.First, EnumSample.Second }.AssertNotExistNull();
                new Dictionary<string, int?>() { { "test1", 1 }, { "test2", 2 } }.AssertNotExistNull();
            }

            [Fact]
            public void Nullに使用するとTrueを返す()
            {
                Reco1? data = null;

                Assert.Throws<NullReferenceException>(() => data.AssertNotExistNull());
            }

            [Fact]
            public void 値型メンバがNullだとTrueを返す()
            {
                Reco1? data = new Reco1(null);

                Assert.Throws<NullReferenceException>(() => data.AssertNotExistNull());
            }

            [Fact]
            public void 参照型メンバがNullだとTrueを返す()
            {
                Reco1? data = new Reco1(1);

                Assert.Throws<NullReferenceException>(() => new Reco2(data, null).AssertNotExistNull());
                Assert.Throws<NullReferenceException>(() => new Reco2(null, data).AssertNotExistNull());
            }

            [Fact]
            public void コレクションの要素がNullだとTrueを返す()
            {
                Assert.Throws<NullReferenceException>(() => new List<int?>() { 1, 2, null, 4 }.AssertNotExistNull());
                Assert.Throws<NullReferenceException>(() => new List<Reco1?>() { new(1), new(2), null, new(4) }.AssertNotExistNull());
                Assert.Throws<NullReferenceException>(() => new List<bool?>() { true, false, null, false }.AssertNotExistNull());
                Assert.Throws<NullReferenceException>(() => new List<EnumSample?>() { EnumSample.First, EnumSample.Second, null }.AssertNotExistNull());
                Assert.Throws<NullReferenceException>(() => new Dictionary<string, int?>() { { "test1", 1 }, { "test2", null } }.AssertNotExistNull());
            }

            [Fact]
            public void 深い階層の値でもNullだとTrueを返す()
            {
                var reco = new Reco3(new(new(1), new(3)), false, new List<Reco1>() { new(5), new(7), new(8), new(9), new(null) });

                Assert.Throws<NullReferenceException>(() => reco.AssertNotExistNull());
            }

            private enum EnumSample
            {
                First,
                Second,
            }

            private record Reco1(int? i);

            private record Reco2(Reco1? data1, Reco1? data2);

            private record Reco3(Reco2 data1, bool data2, List<Reco1> data3);
        }

        public class Int拡張について
        {
            /// <summary> <see cref="CommonExtentions.IsPositive(int)"/> </summary>
            public class 正の値であるかをチェックできる
            {
                [Theory]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void 正の値の時に呼び出すとTrueを返す(int source)
                {
                    Assert.True(source.IsPositive());
                }

                [Theory]
                [InlineData(-1)]
                [InlineData(int.MinValue)]
                public void 負の値の時に呼び出すとFalseを返す(int source)
                {
                    Assert.False(source.IsPositive());
                }

                [Fact]
                public void 値が0の時に呼び出すとFalseを返す()
                {
                    int source = 0;
                    Assert.False(source.IsPositive());
                }
            }

            /// <summary> <see cref="CommonExtentions.IsNegative(int)"/> </summary>
            public class 負の値であるかをチェックできる
            {
                [Theory]
                [InlineData(-1)]
                [InlineData(int.MinValue)]
                public void 負の値の時に呼び出すとTrueを返す(int source)
                {
                    Assert.True(source.IsNegative());
                }

                [Theory]
                [InlineData(1)]
                [InlineData(int.MaxValue)]
                public void 正の値の時に呼び出すとFalseを返す(int source)
                {
                    Assert.False(source.IsNegative());
                }

                [Fact]
                public void 値が0の時に呼び出すとFalseを返す()
                {
                    int source = 0;
                    Assert.False(source.IsNegative());
                }
            }

            /// <summary> <see cref="CommonExtentions.IsEven(int)"/> </summary>
            public class 偶数であるかをチェックできる
            {
                [Theory]
                [InlineData(2)]
                [InlineData(1000)]
                [InlineData(0)]
                public void 偶数の値の時に呼び出すとTrueを返す(int source)
                {
                    Assert.True(source.IsEven());
                }

                [Theory]
                [InlineData(1)]
                [InlineData(50000001)]
                public void 奇数の値の時に呼び出すとFalseを返す(int source)
                {
                    Assert.False(source.IsEven());
                }
            }

            /// <summary> <see cref="CommonExtentions.IsOdd(int)"/> </summary>
            public class 奇数であるかをチェックできる
            {
                [Theory]
                [InlineData(1)]
                [InlineData(50000001)]
                public void 奇数の値の時に呼び出すとTrueを返す(int source)
                {
                    Assert.True(source.IsOdd());
                }

                [Theory]
                [InlineData(2)]
                [InlineData(1000)]
                [InlineData(0)]
                public void 偶数の値の時に呼び出すとFalseを返す(int source)
                {
                    Assert.False(source.IsOdd());
                }
            }
        }

        public class Double拡張について
        {
            /// <summary> <see cref="CommonExtentions.IsPositive(double)"/> </summary>
            public class 正の値であるかをチェックできる
            {
                [Theory]
                [InlineData(1.1)]
                [InlineData(double.MaxValue)]
                public void 正の値の時に呼び出すとTrueを返す(double source)
                {
                    Assert.True(source.IsPositive());
                }

                [Theory]
                [InlineData(-1.2)]
                [InlineData(double.MinValue)]
                public void 負の値の時に呼び出すとFalseを返す(double source)
                {
                    Assert.False(source.IsPositive());
                }

                [Fact]
                public void 値が0の時に呼び出すとFalseを返す()
                {
                    double source = 0;
                    Assert.False(source.IsPositive());
                }
            }

            /// <summary> <see cref="CommonExtentions.IsNegative(double)"/> </summary>
            public class 負の値であるかをチェックできる
            {
                [Theory]
                [InlineData(-1.4)]
                [InlineData(double.MinValue)]
                public void 負の値の時に呼び出すとTrueを返す(double source)
                {
                    Assert.True(source.IsNegative());
                }

                [Theory]
                [InlineData(1.2)]
                [InlineData(double.MaxValue)]
                public void 正の値の時に呼び出すとFalseを返す(double source)
                {
                    Assert.False(source.IsNegative());
                }

                [Fact]
                public void 値が0の時に呼び出すとFalseを返す()
                {
                    double source = 0;
                    Assert.False(source.IsNegative());
                }
            }

            /// <summary> <see cref="CommonExtentions.ConvertDecimalToInteger(double, int)"/> </summary>
            public class 小数を繰り上げて整数に変換できる
            {
                [Theory]
                [InlineData(0.192, 3, 192)]
                [InlineData(11.192, 3, 192)]
                [InlineData(-0.192, 3, 192)]
                [InlineData(0.192, 2, 19)]
                [InlineData(0.192, 4, 1920)]
                [InlineData(0.192, 0, 0)]
                public void 小数の値を指定桁数分繰り上げた値を返す(double source, int digitNum, int expect)
                {
                    var actual = source.ConvertDecimalToInteger(digitNum);

                    Assert.Equal(expect, actual);
                }

                [Fact]
                public void 繰り上げ桁数に負の値を指定すると例外をスローする() {
                    var source = 0.5;
                    Assert.Throws<ArgumentException>(() => source.ConvertDecimalToInteger(-1));
                }
            }
        }

        /// <summary> <see cref="CommonExtentions.IsNullOrEmpty(string?)"/> と  <see cref="CommonExtentions.IsNullOrWhiteSpace(string?)(string?)"/> に対するテストです </summary>
        public class 文字列が無効でないか確認できる
        {
            [Fact]
            public void Nullまたは空文字でないかを確認できる()
            {
                string? nullstr = null;
                Assert.True(nullstr.IsNullOrEmpty());
                Assert.True(string.Empty.IsNullOrEmpty());
                Assert.False(" ".IsNullOrEmpty());
                Assert.False("　".IsNullOrEmpty());
                Assert.False("a".IsNullOrEmpty());
                Assert.False("あ".IsNullOrEmpty());
            }

            [Fact]
            public void Nullまたは空文字または空白でないかを確認できる()
            {
                string? nullstr = null;
                Assert.True(nullstr.IsNullOrWhiteSpace());
                Assert.True(string.Empty.IsNullOrWhiteSpace());
                Assert.True(" ".IsNullOrWhiteSpace());
                Assert.True("　".IsNullOrWhiteSpace());
                Assert.True(" 　".IsNullOrWhiteSpace());
                Assert.False("a".IsNullOrWhiteSpace());
                Assert.False("あ".IsNullOrWhiteSpace());
            }
        }

        /// <summary> <see cref="CommonExtentions.ReplaceInRange(string, char, char, char)"/>に対するテストです </summary>
        public class 文字列中のセパレータで指定された範囲内において文字を置換する
        {
            [Fact]
            public void ダブルクォトで囲んでいる範囲だけカンマがタブに変換されて返ってくる()
            {
                string source = "Test,\"Test,Test\",Test2,Test3,\"Test4,Test4\",Test5";
                string expect = "Test,\"Test\tTest\",Test2,Test3,\"Test4\tTest4\",Test5";

                var actual = source.ReplaceInRange('\"', ',', '\t');

                Assert.Equal(expect, actual);
            }

            [Fact]
            public void ダブルクォトで囲んでいる範囲だけカンマがタブに変換されて返ってくる_先頭からダブルクォト有Ver()
            {
                string source = "\"Test,Test\",Test2,Test3,\"Test4,Test4\",Test5";
                string expect = "\"Test\tTest\",Test2,Test3,\"Test4\tTest4\",Test5";

                var actual = source.ReplaceInRange('\"', ',', '\t');

                Assert.Equal(expect, actual);
            }
        }

        public class オブジェクトをDeepCopyeできる
        {
            [Fact]
            public void インスタンスから呼び出すと同値で別参照のインスタンスを返す()
            {
                var source = new Size(new(1), new(2), new(3));

                var actual = source.DeepCopy();

                Assert.False(object.ReferenceEquals(source, actual));
                Assert.Equal(source, actual);

                // DeepCopyなのでメンバも参照が異なるはず
                Assert.False(object.ReferenceEquals(source.Length, actual.Length));
                Assert.Equal(source.Length, actual.Length);
                Assert.False(object.ReferenceEquals(source.Width, actual.Width));
                Assert.Equal(source.Width, actual.Width);
                Assert.False(object.ReferenceEquals(source.Height, actual.Height));
                Assert.Equal(source.Height, actual.Height);
            }

            [Fact]
            public void Jsonデシリアライズに失敗するクラスだと例外をスローする()
            {
                var source = new NotJsonCovertable("12");

                Assert.Throws<InvalidOperationException>(() => source.DeepCopy());
            }

            private record NotJsonCovertable
            {
                public int Value { get; }

                public NotJsonCovertable(string value)
                {
                    this.Value = int.Parse(value); // プロパティとコンストラクタの引数で型が違うのでデシリアライズで失敗するはず
                }
            }
        }
    }
}
