using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tools.Support.LinqLike;
using Tools.ValueObject.SizeObj;
using Xunit;

namespace Tools_Test.LinqLike
{
    /// <summary> <see cref="LinqLike"/>に対するテストです </summary>
    public class Commons_Test
    {
        /// <summary> <see cref="Commons.AsString{TSource}(IEnumerable{TSource}, char)"/>に対するテストです </summary>
        public class イテレータオブジェクトのメンバを結合した文字列を取得できる
        {
            [Fact]
            public void イテレータオブジェクトのメンバを結合した文字列が返る()
            {
                var data = new List<int?>() { 1, 2, null };

                var except = "{ { 1 }. { 2 }. { null } }";

                var actual = data.AsString();

                Assert.True(except.Equals(actual));
            }
        }

        /// <summary> <see cref="Commons.Sepalate{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>に対するテストです </summary>
        public class 条件式に合致するしないで要素を分割した一覧を取得できる
        {
            [Fact]
            public void 条件式に合致するしないで要素を分割できる()
            {
                var odd = new List<int>() { 1, 3, 5 };
                var even = new List<int>() { 2, 4, 6 };
                var data = odd.Union(even);

                (var matched, var unMatched) = data.Sepalate(x => x % 2 == 1);

                Assert.Equal(odd.Count + even.Count, data.Count());
                Assert.True(odd.SequenceEqual(matched));
                Assert.True(even.SequenceEqual(unMatched));
            }
        }

        /// <summary> <see cref="Commons.CreteHashCode{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class イテレータの要素からハッシュ値を生成できる
        {
            // テスト用にCount分のランダム数列ゲット
            private const int Count = 20;
            private static readonly ImmutableList<int> Source = Enumerable.Range(0, Count * 2).RandomTake(Count).ToImmutableList();
            private static readonly ImmutableList<int> Other = Source.ToImmutableList();

            [Fact]
            public void 要素が同じなら同じハッシュ値になる()
            {
                Assert.Equal(Source.CreteHashCode(), Other.CreteHashCode());
            }

            [Fact]
            public void 要素が同じなら同じハッシュ値になる_順番が違うVer()
            {
                Assert.Equal(Source.OrderBy(x => x).CreteHashCode(), Other.OrderByDescending(x => x).CreteHashCode());
            }

            [Fact]
            public void 要素が同じなら同じハッシュ値になる_イテレータの実態が違うVer()
            {
                Assert.Equal(Source.ToList().CreteHashCode(), Other.ToHashSet().CreteHashCode());
            }

            [Fact]
            public void 要素が同じなら同じハッシュ値になる_重複データがあるVer()
            {
                // 含まれてない値を探す
                int notContains = 1; // 0のハッシュ値は追加しても無駄なので１始まり
                while (true)
                {
                    if (Source.Contains(notContains) is false)
                        break;
                    notContains++;
                }

                var added = new List<int>() { notContains, notContains, notContains };
                Assert.Equal(Source.AddRange(added).CreteHashCode(), Other.AddRange(added).CreteHashCode()); // 両方足したVer
                Assert.NotEqual(Source.Add(notContains).CreteHashCode(), Other.CreteHashCode()); // 片方だけ足したVer

                // XORをつかっているので重複データ数の数が値がっでも奇数同士 or 偶数同士だと同じハッシュ値になっちゃう
                Assert.Equal(
                    Source.AddRange(added).CreteHashCode(), // １回追加
                    Other.AddRange(added).AddRange(added).AddRange(added).CreteHashCode() // 3回追加
                    );
                Assert.NotEqual(
                    Source.AddRange(added).CreteHashCode(), // １回追加
                    Other.AddRange(added).AddRange(added).CreteHashCode() // 2回追加
                    );
            }

            [Fact]
            public void 要素が異なると違うハッシュ値になる()
            {
                Assert.NotEqual(Source.CreteHashCode(), Other.Add(14332).CreteHashCode()); // 多いver
                Assert.NotEqual(Source.CreteHashCode(), Other.Skip(1).CreteHashCode()); // 少ないver
            }
        }

        /// <summary> <see cref="ForEntity.SequenceDeepCopy{TSource}(IEnumerable{TSource})"/> に対するテストです </summary>
        public class イテレータのDeepCopyが出来る
        {
            [Fact]
            public void 要素まで別参照だが値が同じ別の列挙が返る()
            {
                var source = new List<Length> {
                    new (1),
                    new (2),
                    new (3),
                    new (4),
                };

                var copy = source.SequenceDeepCopy();

                Assert.True(source.SequenceEqual(copy));
                Assert.False(ReferenceEquals(source, copy));
                Assert.False(ReferenceEquals(source[0], copy.ElementAt(0)));
                Assert.False(ReferenceEquals(source[1], copy.ElementAt(1)));
                Assert.False(ReferenceEquals(source[2], copy.ElementAt(2)));
                Assert.False(ReferenceEquals(source[3], copy.ElementAt(3)));
            }
        }

        /// <summary> <see cref="Commons.Empty{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class イテレータが空かを判別できる
        {
            [Fact]
            public void サイズが0ならTrueが返る()
            {
                Assert.True(new List<int>().Empty());
                Assert.True(new List<string>().Empty());
                Assert.True(string.Empty.Empty());
            }

            [Fact]
            public void サイズが1以上らFalseが返る()
            {
                Assert.False(new List<int>() { 1 }.Empty());
                Assert.False(Enumerable.Range(1, 1000000).Empty());
                Assert.False(new List<int?>() { 1, null }.Empty());
                Assert.False(new List<int?>() { null }.Empty());
            }
        }

        /// <summary> <see cref="Commons.Empty{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>に対するテストです </summary>
        public class イテレータが空かを判別できる_セレクター付き
        {
            [Fact]
            public void 指定した条件の要素のサイズが0ならTrueが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { null, null, null, null };

                Assert.True(data.Empty(x => x > 10));
                Assert.True(data.Empty(x => x > -10 && x < -2));
                Assert.True(dataWithNull.Empty(x => x is not null));
                Assert.True(new List<int>().Empty(x => x > 9));
            }

            [Fact]
            public void 指定した条件の要素のサイズが1以上ならFalseが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { 1, 2, null, 3, null };

                Assert.False(data.Empty(x => x > 5));
                Assert.False(data.Empty(x => x > 5 && x < 8));
                Assert.False(dataWithNull.Empty(x => x is null));
            }
        }

        /// <summary> <see cref="Commons.Only{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class イテレータのサイズが1かを判別できる
        {
            [Fact]
            public void サイズが1ならTrueが返る()
            {
                Assert.True(new List<int>() { 1 }.Only());
                Assert.True(new List<int?>() { null }.Only());
                Assert.True("A".Only());
            }

            [Fact]
            public void サイズが1以外ならFalseが返る()
            {
                Assert.False(new List<int>().Only());
                Assert.False(new List<int>() { 1, 2 }.Only());
                Assert.False(Enumerable.Range(1, 50).Only());
            }
        }

        /// <summary> <see cref="Commons.Only{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>に対するテストです </summary>
        public class イテレータのサイズが1かを判別できる_セレクター付き
        {
            [Fact]
            public void 指定した条件の要素のサイズが1ならTrueが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { 1, 2, null, 3, 4 };

                Assert.True(data.Only(x => x == 5));
                Assert.True(data.Only(x => x > 5 && x < 7));
                Assert.True(dataWithNull.Only(x => x is null));
            }

            [Fact]
            public void 指定した条件の要素のサイズが1以外ならFalseが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { 1, 2, null, 3, null };

                Assert.False(data.Only(x => x > 8));
                Assert.False(data.Only(x => x > 20));
                Assert.False(data.Only(x => x > 6 && x < 9));
                Assert.False(dataWithNull.Only(x => x is null));
                Assert.False(new List<int>().Only(x => x > 9));
            }
        }

        /// <summary> <see cref="Commons.Many{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class イテレータのサイズが２以上かを判別できる
        {
            [Fact]
            public void サイズが2以上ならTrueが返る()
            {
                Assert.True(new List<int>() { 1, 2 }.Many());
                Assert.True(Enumerable.Range(1, 1000000).Many());
                Assert.True(new List<int?>() { 1, null }.Many());
                Assert.True(new List<int?>() { null, null, null }.Many());
            }

            [Fact]
            public void サイズが2未満ならFalseが返る()
            {
                Assert.False(new List<int?>().Many());
                Assert.False(new List<int>() { 1 }.Many());
            }
        }

        /// <summary> <see cref="Commons.Many{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>に対するテストです </summary>
        public class イテレータのサイズが２以上かを判別できる_セレクター付き
        {
            [Fact]
            public void 指定した条件の要素のサイズが2以上ならTrueが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { 1, 2, null, 3, null };

                Assert.True(data.Many(x => x > 5));
                Assert.True(data.Many(x => x > 5 && x < 8));
                Assert.True(dataWithNull.Many(x => x is null));
            }

            [Fact]
            public void 指定した条件の要素のサイズが2未満ならFalseが返る()
            {
                var data = Enumerable.Range(1, 10);
                var dataWithNull = new List<int?>() { 1, 2, null, 3 };

                Assert.False(data.Many(x => x > 9));
                Assert.False(data.Many(x => x > 6 && x < 8));
                Assert.False(dataWithNull.Many(x => x is null));
                Assert.False(new List<int>().Many(x => x > 9));
            }
        }

        /// <summary> <see cref="Commons.MaxBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class セレクターで取得した値が最大な要素を取得する
        {
            [Fact]
            public void 最大値を持つインスタンスを返す()
            {
                var baseSize = new Size(new(10), new(10), new(10));
                var hasMaxLength = baseSize with { Length = new(100000) };
                var sizes = new List<Size>() {
                    baseSize,
                    baseSize with { Length = new(20) },
                    baseSize with { Width = new(20) },
                    baseSize with { Height = new(20) },
                    hasMaxLength,
                };

                var actual = sizes.MaxBy(x => x.Length);

                Assert.Equal(hasMaxLength, actual);
            }

            [Fact]
            public void サイズが0の場合は例外をスローする()
            {
                var sizes = new List<Size>() { };

                Assert.Throws<InvalidOperationException>(() => sizes.MaxBy(x => x.Length));
            }
        }

        /// <summary> <see cref="Commons.MinBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class セレクターで取得した値が最小な要素を取得する
        {
            [Fact]
            public void 最小値を持つインスタンスを返す()
            {
                var baseSize = new Size(new(10), new(10), new(10));
                var hasMinLength = baseSize with { Length = new(1) };
                var sizes = new List<Size>() {
                    baseSize,
                    baseSize with { Length = new(20) },
                    baseSize with { Width = new(20) },
                    baseSize with { Height = new(20) },
                    hasMinLength,
                };

                var actual = sizes.MinBy(x => x.Length);

                Assert.Equal(hasMinLength, actual);
            }

            [Fact]
            public void サイズが0の場合は例外をスローする()
            {
                var sizes = new List<Size>() { };

                Assert.Throws<InvalidOperationException>(() => sizes.MinBy(x => x.Length));
            }
        }
    }
}
