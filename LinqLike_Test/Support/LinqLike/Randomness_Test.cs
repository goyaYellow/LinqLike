using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tools.Support.LinqLike;
using Tools.ValueObject.SizeObj;
using Xunit;

namespace Tools_Test.LinqLike
{
    /// <summary> <see cref="Randomness"/>に対するテストです </summary>
    public class Randomness_Test
    {
        private const int Count = 50;
        private static ImmutableList<int> Source = Enumerable.Range(0, Count).ToImmutableList();

        /// <summary> <see cref="Randomness.RandomAll{T}(IEnumerable{T})"/>に対するテストです </summary>
        public class 要素のをランダムに取得できる
        {
            [Fact]
            public void ランダムな要素が返る()
            {
                var geted = new HashSet<int>();
                while (true)
                {
                    var teked = Source.Random();

                    Assert.Contains(teked, Source);
                    if (geted.Add(teked) is false) break;
                }

                Assert.True(true); // いつかはここを通るはず。
            }

            [Fact]
            public void 要素が空のイテレータに対して使用すると例外をスローする()
            {
                Assert.Throws<InvalidOperationException>(() => new List<int>().Random());
            }
        }

        /// <summary> <see cref="Randomness.RandomAll{T}(IEnumerable{T})"/>に対するテストです </summary>
        public class 要素の順番がランダム化された状態で指定数分要素を取得できる
        {
            [Fact]
            public void 順番がランダム化された状態で指定数分要素が返る()
            {
                while (true)
                {
                    var takeCount = Count / 2;
                    var teked = Source.RandomTake(takeCount);

                    Assert.Equal(takeCount, teked.Count());
                    Assert.True(Source.SequenceContains(teked));
                    if (Source.SequenceEqual(teked) is false) break;
                }

                Assert.True(true); // いつかはここを通るはず。
            }

            [Fact]
            public void 要素数を超える数を指定すると例外をスローする()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Source.RandomTake(Source.Count() + 1));
            }
        }

        /// <summary> <see cref="Randomness.RandomAll{T}(IEnumerable{T})"/>に対するテストです </summary>
        public class 要素の順番がランダム化されたイテレータを取得できる
        {
            [Fact]
            public void 要素の数は変わらず順番がランダム化されたイテレータが返る()
            {
                while (true)
                {
                    var teked = Source.RandomAll();

                    Assert.True(Source.SequenceEqualOrderless(teked)); // 順不同でなら同じ要素をもつイテレータになるはず
                    if (Source.SequenceEqual(teked) is false) break;
                }

                Assert.True(true); // いつかはここを通るはず。
            }
        }
    }
}
