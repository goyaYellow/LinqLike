using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Support;
using Tools.Support.LinqLike;
using Xunit;

namespace Tools_Test.LinqLike
{
    /// <summary> <see cref="Duplication"/>に対するテストです </summary>
    public class Duplication_Test
    {
        /// <summary> <see cref="Duplication.HasDuplicate{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class 重複のチェックができる
        {
            public class 重複があればTrueを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    list.Add(500);

                    Assert.True(list.HasDuplicate());
                }

                [Fact]
                public void 文字列編()
                {
                    var list = new List<string>() { "test1", "test2", "test2" };

                    Assert.True(list.HasDuplicate());
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(1), new(3), };

                    Assert.True(list.HasDuplicate());
                }
            }

            public class 重複がなければFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();

                    Assert.False(list.HasDuplicate());
                }

                [Fact]
                public void 文字列編()
                {
                    var list = new List<string>() { "test1", "test2", "test" };

                    Assert.False(list.HasDuplicate());
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(3), new(4), };

                    Assert.False(list.HasDuplicate());
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco>();

                    Assert.False(list.HasDuplicate());
                }
            }
        }

        /// <summary> <see cref="Duplication.HasDuplicate{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>に対するテストです </summary>
        public class 重複のチェックができる_条件指定つき
        {
            public class 重複があればTrueを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    list.AddRange(Enumerable.Range(900, 1000));

                    Assert.True(list.HasDuplicate(x => x > 990));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(1), new(3), };

                    Assert.True(list.HasDuplicate(x => x.Value < 2));
                }
            }

            public class 重複がなければFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    list.AddRange(Enumerable.Range(900, 1000));

                    Assert.False(list.HasDuplicate(x => x < 890));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(1), new(3), };

                    Assert.False(list.HasDuplicate(x => x.Value >= 2));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco>();

                    Assert.False(list.HasDuplicate(x => true));
                }
            }
        }

        /// <summary> <see cref="Duplication.HasDuplicateBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class 重複のチェックができる_要素指定つき
        {
            public class 重複があればTrueを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    list.AddRange(Enumerable.Range(900, 1000));

                    Assert.True(list.HasDuplicateBy(x => x.GetHashCode()));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                    };

                    Assert.True(list.HasDuplicateBy(x => x.Value1));
                }
            }

            public class 重複がなければFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();

                    Assert.False(list.HasDuplicateBy(x => x.GetHashCode()));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                    };

                    Assert.False(list.HasDuplicateBy(x => x.Value2));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco2>();

                    Assert.False(list.HasDuplicateBy(x => x.Value1));
                    Assert.False(list.HasDuplicateBy(x => x.Value2));
                }
            }
        }

        /// <summary> <see cref="Duplication.HasDifference{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class 重複しない要素があるかチェックができる
        {
            public class 全ての要素が重複していればFalseが返る
            {
                [Fact]
                public void イント編()
                {
                    var list = new List<int>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add(1));

                    Assert.False(list.HasDifference());
                }

                [Fact]
                public void 文字列編()
                {
                    var list = new List<string>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add("test"));

                    Assert.False(list.HasDifference());
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add(new SampleReco(92)));

                    Assert.False(list.HasDifference());
                }
            }

            public class 重複していない要素があればFalseが返る
            {
                [Fact]
                public void イント編()
                {
                    var list = new List<int>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add(1));
                    list.Add(4);

                    Assert.True(list.HasDifference());
                }

                [Fact]
                public void 文字列編()
                {
                    var list = new List<string>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add("test"));
                    list.Add("str");

                    Assert.True(list.HasDifference());
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>();
                    Enumerable.Range(1, 100).ToList().ForEach(x => list.Add(new SampleReco(92)));
                    list.Add(new(10));
                    list.Add(new(20));

                    Assert.True(list.HasDifference());
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco>();

                    Assert.False(list.HasDifference());
                }
            }
        }

        /// <summary> <see cref="Duplication.HasDifference{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class 重複しない要素があるかチェックができる_条件指定つき
        {
            public class 全ての要素が重複がしていればFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    var num = 5000000;
                    list.AddRange(new List<int>() { num, num, num });

                    Assert.False(list.HasDifference(x => x == num));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(1), new(3), };

                    Assert.False(list.HasDifference(x => x.Value < 2));
                }
            }

            public class 重複していない要素があればTrueが返る
            {
                [Fact]
                public void イント編()
                {
                    var list = Enumerable.Range(1, 1000).ToList();
                    list.AddRange(Enumerable.Range(900, 1000));

                    Assert.True(list.HasDifference(x => x > 990));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco>() { new(1), new(2), new(1), new(3), };

                    Assert.True(list.HasDifference(x => x.Value > 0));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco>();
                    var list2 = Enumerable.Range(1, 1000);

                    Assert.False(list.HasDifference(x => true));
                    Assert.False(list2.HasDifference(x => x < 0));
                }
            }
        }

        /// <summary> <see cref="Duplication.HasDifferenceBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class 重複しない要素があるかチェックができる要素指定つき
        {
            public class 全ての要素が重複がしていればFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list = new List<int>();
                    var num = 8476345;
                    Enumerable.Range(1, 1000).ToList().ForEach(x => list.Add(num));

                    Assert.False(list.HasDifferenceBy(x => x.GetHashCode()));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                        new SampleReco2(new(1), new(2)),
                    };

                    Assert.False(list.HasDifferenceBy(x => x.Value1));
                }
            }

            public class 重複していない要素があればTreが返る
            {
                [Fact]
                public void イント編()
                {
                    var list = new List<int>();
                    var num = 8476345;
                    Enumerable.Range(1, 1000).ToList().ForEach(x => list.Add(num));
                    list.Add(1);

                    Assert.True(list.HasDifferenceBy(x => x.GetHashCode()));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                        new SampleReco2(new(1), new(2)),
                    };

                    Assert.True(list.HasDifferenceBy(x => x.Value2));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void 空のコレクションではFalseを返す()
                {
                    var list = new List<SampleReco2>();

                    Assert.False(list.HasDifferenceBy(x => x.Value1));
                    Assert.False(list.HasDifferenceBy(x => x.Value2));
                }
            }
        }

        /// <summary> <see cref="Duplication.FindDeplicated{TSource}(IEnumerable{TSource})"/>に対するテストです </summary>
        public class 重複している要素を一覧として取りだせる
        {
            [Fact]
            public void イント編()
            {
                var list = new List<int>() {
                    1,
                    1, 2,
                    1, 2, 3,
                };

                var expect = new List<int>() { 1, 2 };
                var actual = list.FindDeplicated();

                Assert.True(expect.SequenceEqual(actual));
            }

            [Fact]
            public void 自作クラス編()
            {
                var list = new List<SampleReco>() {
                    new (1),
                    new (1), new (2),
                    new (1), new (2), new (3),
                };

                var expect = new List<SampleReco>() { new(1), new(2) };
                var actual = list.FindDeplicated();

                Assert.True(expect.SequenceEqual(actual));
            }
        }

        /// <summary> <see cref="Duplication.FindDeplicatedBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class 重複している要素を一覧として取りだせる_要素指定付き
        {
            [Fact]
            public void イントのリスト編()
            {
                var list = new List<List<int>>() {
                    new List<int>() { 1, 2 },
                    new List<int>() { 1, 3 },
                    new List<int>() { 2, 4 },
                };

                var expect = new List<int>() { 1, 2 };
                var actual = list.FindDeplicatedBy(x => x.First());

                Assert.True(expect.SequenceEqual(actual.First()));
                Assert.Empty(list.FindDeplicatedBy(x => x[1]));　// 重複しないキーだと空なはず
            }

            [Fact]
            public void 自作クラス編()
            {
                var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                        new SampleReco2(new(2), new(4)),
                    };

                var expect = new SampleReco2(new(1), new(2));
                var actual = list.FindDeplicatedBy(x => x.Value1);

                Assert.Equal(expect, actual.First());
                Assert.Empty(list.FindDeplicatedBy(x => x.Value2)); // 重複しないキーだと空なはず
            }
        }

        /// <summary> <see cref="Duplication.DistinctBy{TSource, TCheckValue}(IEnumerable{TSource}, Func{TSource, TCheckValue})"/>に対するテストです </summary>
        public class 重複を取り除いた一覧を取得できる_要素指定付き
        {
            [Fact]
            public void イントのリスト編()
            {
                var list = new List<List<int>>() {
                    new List<int>() { 1, 2 },
                    new List<int>() { 1, 3 },
                    new List<int>() { 2, 4 },
                };

                var expect = new List<List<int>>() {
                    new List<int>() { 1, 2 },
                    new List<int>() { 2, 4 },
                };
                var actual = list.DistinctBy(x => x.First());

                Assert.True(expect.SequenceEqualNestable(actual.ToList()));

                // 重複しないキーだとすべて返るはず
                Assert.True(list.SequenceEqualNestable(list.DistinctBy(x => x[1]).ToList()));
            }

            [Fact]
            public void 自作クラス編()
            {
                var list = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(1), new(3)),
                        new SampleReco2(new(2), new(4)),
                    };

                var expect = new List<SampleReco2>() {
                        new SampleReco2(new(1), new(2)),
                        new SampleReco2(new(2), new(4)),
                    };
                var actual = list.DistinctBy(x => x.Value1);

                Assert.True(expect.SequenceEqualNestable(actual.ToList()));

                // 重複しないキーだと空なはず
                Assert.True(list.SequenceEqualNestable(list.DistinctBy(x => x.Value2).ToList()));
            }
        }

        private record SampleReco(int Value);

        private record SampleReco2(SampleReco Value1, SampleReco Value2);
    }
}
