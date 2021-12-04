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
    /// <summary> <see cref="EqualsLike"/>に対するテストです </summary>
    public class EqualsLike_Test
    {
        /// <summary> <see cref="EqualsLike.SequenceEqualNestable{TSource}(IEnumerable{TSource}, IEnumerable{TSource}?)"/>に対するテストです </summary>
        public class 入れ子になったリストの等価性評価ができる
        {
            public class 等価であればTrueを返す
            {
                [Fact]
                public void イント編()
                {
                    var list1 = new List<List<int>>() { new List<int>() { 1, 1, 2 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };
                    var list2 = new List<List<int>>() { new List<int>() { 1, 1, 2 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void ダブル編()
                {
                    var list1 = new List<List<double>>() { new List<double>() { 1.0, 2.1, 3.2 }, new List<double>() { 4.3, 5.4, 6.5 }, new List<double>() { 1.0, 2.1, 3.2 } };
                    var list2 = new List<List<double>>() { new List<double>() { 1.0, 2.1, 3.2 }, new List<double>() { 4.3, 5.4, 6.5 }, new List<double>() { 1.0, 2.1, 3.2 } };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 文字列編()
                {
                    var list1 = new List<List<string>>() { new List<string>() { "aa", "bb" }, new List<string>() { "cc", "dd" }, new List<string>() { "ee", "ff" } };
                    var list2 = new List<List<string>>() { new List<string>() { "aa", "bb" }, new List<string>() { "cc", "dd" }, new List<string>() { "ee", "ff" } };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 自作クラス編()
                {
                    var list1 = new List<List<SampleReco>>() { new List<SampleReco>() { new(1), new(1) }, new List<SampleReco>() { new(3), new(4) }, new List<SampleReco>() { new(5), new(6) } };
                    var list2 = new List<List<SampleReco>>() { new List<SampleReco>() { new(1), new(1) }, new List<SampleReco>() { new(3), new(4) }, new List<SampleReco>() { new(5), new(6) } };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 三次元リスト編()
                {
                    var list1 = new List<List<List<int>>>() {
                        new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } },
                        new List<List<int>>() { new List<int>() { 3, 2, 1 }, new List<int>() { 6, 5, 4 } },
                    };
                    var list2 = new List<List<List<int>>>() {
                        new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } },
                        new List<List<int>>() { new List<int>() { 3, 2, 1 }, new List<int>() { 6, 5, 4 } },
                    };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 四次元リスト編()
                {
                    var list1 = new List<List<List<List<int>>>>() {
                        new List<List<List<int>>>() {
                            new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() },
                            new List<List<int>>(),
                            new List<List<int>>() { new List<int>() { 3, 2, 1 }, new List<int>() { 6, 5, 4 } },
                        },
                        new List<List<List<int>>>() {
                            new List<List<int>>() { new List<int>() { 11, 12, 13 }, new List<int>() { 14, 15, 16 }, new List<int>() },
                            new List<List<int>>(),
                            new List<List<int>>() { new List<int>() { 23, 22, 21 }, new List<int>() { 26, 25, 24 } },
                        },
                    };
                    var list2 = new List<List<List<List<int>>>>() {
                        new List<List<List<int>>>() {
                            new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() },
                            new List<List<int>>(),
                            new List<List<int>>() { new List<int>() { 3, 2, 1 }, new List<int>() { 6, 5, 4 } },
                        },
                        new List<List<List<int>>>() {
                            new List<List<int>>() { new List<int>() { 11, 12, 13 }, new List<int>() { 14, 15, 16 }, new List<int>() },
                            new List<List<int>>(),
                            new List<List<int>>() { new List<int>() { 23, 22, 21 }, new List<int>() { 26, 25, 24 } },
                        },
                    };

                    Assert.True(list1.SequenceEqualNestable(list2));
                }
            }

            public class 等価でなければばFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list1 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };
                    var list2 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 4, 6 }, new List<int>() { 1, 2, 3 } };

                    Assert.False(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void ダブル編()
                {
                    var list1 = new List<List<double>>() { new List<double>() { 1.0, 2.1, 3.2 }, new List<double>() { 4.3, 5.4, 6.5 }, new List<double>() { 1.0, 2.1, 3.2 } };
                    var list2 = new List<List<double>>() { new List<double>() { 1.0, 2.1, 3.1 }, new List<double>() { 4.3, 5.4, 6.5 }, new List<double>() { 1.0, 2.1, 3.2 } };

                    Assert.False(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 文字列編()
                {
                    var list1 = new List<List<string>>() { new List<string>() { "aa", "bb" }, new List<string>() { "cc", "dd" }, new List<string>() { "ee", "ff" } };
                    var list2 = new List<List<string>>() { new List<string>() { "aa", "bb" }, new List<string>() { "cc", "bd" }, new List<string>() { "ee", "ff" } };

                    Assert.False(list1.SequenceEqualNestable(list2));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void Nullを渡すとFalseを返す()
                {
                    var list1 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };

                    Assert.False(list1.SequenceEqualNestable(null));
                }

                [Fact]
                public void サイズの違うコレクションを渡すとFalseを返す()
                {
                    var list1 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };
                    var list2 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } };

                    Assert.False(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 空のコレクションで比較するとTrueを返す()
                {
                    var list1 = new List<List<int>>();
                    var list2 = new List<List<int>>();

                    Assert.True(list1.SequenceEqualNestable(list2));
                }

                [Fact]
                public void 空のコレクションを含むコレクションで比較できる()
                {
                    var list1 = new List<List<int>>() { new List<int>(), new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } };
                    var list2 = new List<List<int>>() { new List<int>(), new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } };
                    var list3 = new List<List<int>>() { new List<int>(), new List<int>() { 1, 1, 1 }, new List<int>() { 4, 5, 6 } };

                    Assert.True(list1.SequenceEqualNestable(list2));
                    Assert.False(list1.SequenceEqualNestable(list3));
                }

                [Fact]
                public void 同じ参照を渡すとTruを返す()
                {
                    var list1 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 }, new List<int>() { 1, 2, 3 } };
                    var list2 = list1;
                    list2.Add(new List<int>() { 4, 5, 6 });

                    Assert.True(list1.SequenceEqualNestable(list2));
                }
            }
        }

        /// <summary> <see cref="EqualsLike.SequenceEqualOrderless{TSource}(IEnumerable{TSource}, IEnumerable{TSource}?)"/>に対するテストです </summary>
        public class 順番を考慮せずにイテレータの等価性評価ができる
        {
            public class リスト編
            {
                public class 等価であればTrueを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new List<int>() { 1, 1, 1, 2, 3 };
                        var list2 = new List<int>() { 1, 1, 1, 2, 3 }.RandomAll().ToList();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new List<SampleReco>() { new(1), new(1), new(1), new(2), new(3), new(4) };
                        var list2 = new List<SampleReco>() { new(1), new(1), new(1), new(2), new(3), new(4) }.RandomAll().ToList();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }
                }

                public class 等価でなければばFalseを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new List<int>() { 1, 1, 2, 3, 4 };
                        var list2 = new List<int>() { 0, 1, 2, 3, 4 }.RandomAll().ToList();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new List<SampleReco>() { new(1), new(1), new(2), new(3), new(4), new(5) };
                        var list2 = new List<SampleReco>() { new(0), new(1), new(2), new(3), new(4), new(5) }.RandomAll().ToList();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }
                }

                public class 変わったパターン
                {
                    [Fact]
                    public void Nullを渡すとFalseを返す()
                    {
                        var list1 = new List<int>() { 1, 1, 1, 2, 3 };

                        Assert.False(list1.SequenceEqualOrderless(null));
                    }

                    [Fact]
                    public void サイズの違うコレクションを渡すとFalseを返す()
                    {
                        var list1 = new List<int>() { 1, 1, 1, 2, 3 };
                        var list2 = new List<int>() { 1, 3, 2, 1, 1, 1, 1 };

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void 空のコレクションで比較するとTrueを返す()
                    {
                        var list1 = new List<int>();
                        var list2 = new List<int>();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void 同じ参照を渡すとTruを返す()
                    {
                        var list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3 };
                        var list2 = list1;
                        list2.Add(1);
                        list2.Add(1);

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    /// <summary> ただし、参照等価である場合はTrueが返ります </summary>
                    [Fact]
                    public void コレクションを含むコレクションで比較するとFalseが返る()
                    {
                        var list1 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } };
                        var list2 = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 4, 5, 6 } };

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }
                }
            }

            public class セット編
            {
                public class 等価であればTrueを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new HashSet<int>() { 1, 1, 1, 2, 3 };
                        var list2 = new HashSet<int>() { 1, 1, 2, 2, 3 }.RandomAll().ToHashSet();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new HashSet<SampleReco>() { new(1), new(1), new(1), new(2), new(3), new(4) };
                        var list2 = new HashSet<SampleReco>() { new(1), new(1), new(2), new(2), new(3), new(4) }.RandomAll().ToHashSet();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }
                }

                public class 等価でなければばFalseを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new HashSet<int>() { 1, 1, 2, 3, 4 };
                        var list2 = new HashSet<int>() { 0, 1, 2, 3, 4 }.RandomAll().ToHashSet();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new HashSet<SampleReco>() { new(1), new(1), new(2), new(3), new(4), new(5) };
                        var list2 = new HashSet<SampleReco>() { new(0), new(1), new(2), new(3), new(4), new(5) }.RandomAll().ToHashSet();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }
                }

                public class 変わったパターン
                {
                    [Fact]
                    public void Nullを渡すとFalseを返す()
                    {
                        var list1 = new HashSet<int>() { 1, 1, 1, 2, 3 };

                        Assert.False(list1.SequenceEqualOrderless(null));
                    }

                    [Fact]
                    public void 空のコレクションで比較するとTrueを返す()
                    {
                        var list1 = new HashSet<int>();
                        var list2 = new HashSet<int>();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void 同じ参照を渡すとTruを返す()
                    {
                        var list1 = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3 };
                        var list2 = list1;
                        list2.Add(11);
                        list2.Add(12);

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }
                }
            }

            public class 辞書偏
            {
                public class 等価であればTrueを返す
                {
                    [Fact]
                    public void キー＿文字列＿値＿イント_編()
                    {
                        var list1 = new Dictionary<string, int>() {
                        { "key1", 10 },
                        { "key2", 20 },
                    };
                        var list2 = new Dictionary<string, int>() {
                        { "key2", 20 },
                        { "key1", 10 },
                    };

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void キー＿イント＿値＿Record_編()
                    {
                        var list1 = new Dictionary<int, SampleReco>() {
                        { 1, new (11) },
                        { 2, new (22) },
                    };
                        var list2 = new Dictionary<int, SampleReco>() {
                        { 2, new (22) },
                        { 1, new (11) },
                    };

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }
                }

                public class 等価でなければばFalseを返す
                {
                    public class 値が違う
                    {
                        [Fact]
                        public void キー＿文字列＿値＿イント_編()
                        {
                            var list1 = new Dictionary<string, int>() {
                        { "key1", 10 },
                        { "key2", 20 },
                    };
                            var list2 = new Dictionary<string, int>() {
                        { "key2", 10 },
                        { "key1", 10 },
                    };

                            Assert.False(list1.SequenceEqualOrderless(list2));
                        }

                        [Fact]
                        public void キー＿イント＿値＿Record_編()
                        {
                            var list1 = new Dictionary<int, SampleReco>() {
                        { 1, new (11) },
                        { 2, new (33) },
                    };
                            var list2 = new Dictionary<int, SampleReco>() {
                        { 2, new (22) },
                        { 1, new (11) },
                    };

                            Assert.False(list1.SequenceEqualOrderless(list2));
                        }
                    }

                    public class キーが違う
                    {
                        [Fact]
                        public void キー＿文字列＿値＿イント_編()
                        {
                            var list1 = new Dictionary<string, int>() {
                        { "key1", 10 },
                        { "key3", 20 },
                    };
                            var list2 = new Dictionary<string, int>() {
                        { "key2", 20 },
                        { "key1", 10 },
                    };

                            Assert.False(list1.SequenceEqualOrderless(list2));
                        }

                        [Fact]
                        public void キー＿イント＿値＿Record_編()
                        {
                            var list1 = new Dictionary<int, SampleReco>() {
                        { 1, new (11) },
                        { 3, new (22) },
                    };
                            var list2 = new Dictionary<int, SampleReco>() {
                        { 2, new (22) },
                        { 1, new (11) },
                    };

                            Assert.False(list1.SequenceEqualOrderless(list2));
                        }
                    }
                }

                public class 変わったパターン
                {
                    [Fact]
                    public void Nullを渡すとFalseを返す()
                    {
                        var list1 = new Dictionary<string, int>() {
                        { "key1", 10 },
                        { "key2", 20 },
                    };

                        Assert.False(list1.SequenceEqualOrderless(null));
                    }

                    [Fact]
                    public void サイズの違う辞書を渡すとFalseを返す()
                    {
                        var list1 = new Dictionary<string, int>() {
                        { "key1", 10 },
                    };
                        var list2 = new Dictionary<string, int>() {
                        { "key2", 20 },
                        { "key1", 10 },
                    };

                        Assert.False(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void 空の辞書で比較するとTrueを返す()
                    {
                        var list1 = new Dictionary<string, int>();
                        var list2 = new Dictionary<string, int>();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }

                    [Fact]
                    public void 同じ参照を渡すとTruを返す()
                    {
                        var list1 = new Dictionary<string, int>() {
                        { "key2", 2 },
                        { "key1", 1 },
                    };
                        var list2 = list1;
                        list2["key3"] = 3;

                        Assert.True(list1.SequenceEqualOrderless(list2));
                    }
                }
            }

            public class リストとセット編
            {
                public class 等価であればTrueを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new HashSet<int>() { 1, 2, 3, 3, 3 };
                        var list2 = new List<int>() { 1, 3, 2 }.RandomAll().ToList();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                        Assert.True(list2.SequenceEqualOrderless(list1));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new HashSet<SampleReco>() { new(1), new(2), new(3), new(4), new(4), new(4) };
                        var list2 = new List<SampleReco>() { new(1), new(2), new(3), new(4) }.RandomAll().ToList();

                        Assert.True(list1.SequenceEqualOrderless(list2));
                        Assert.True(list2.SequenceEqualOrderless(list1));
                    }
                }

                public class 等価でなければばFalseを返す
                {
                    [Fact]
                    public void イント編()
                    {
                        var list1 = new HashSet<int>() { 0, 1, 2, 3, 4 };
                        var list2 = new List<int>() { 1, 2, 3, 4 }.RandomAll().ToList();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                        Assert.False(list2.SequenceEqualOrderless(list1));
                    }

                    [Fact]
                    public void Record編()
                    {
                        var list1 = new List<SampleReco>() { new(0), new(1), new(2), new(3), new(4), new(5) };
                        var list2 = new List<SampleReco>() { new(1), new(2), new(3), new(4), new(5) }.RandomAll().ToList();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                        Assert.False(list2.SequenceEqualOrderless(list1));
                    }
                }

                public class 変わったパターン
                {
                    [Fact]
                    public void リスト側に重複があるとFalseを返す()
                    {
                        var list1 = new List<SampleReco>() { new(0), new(1), new(2), new(3), new(4), new(5) };
                        var list2 = new List<SampleReco>() { new(1), new(2), new(3), new(4), new(5), new(5) }.RandomAll().ToList();

                        Assert.False(list1.SequenceEqualOrderless(list2));
                        Assert.False(list2.SequenceEqualOrderless(list1));
                    }
                }
            }
        }

        /// <summary> <see cref="EqualsLike.SequenceContains{TSource}(IEnumerable{TSource}, IEnumerable{TSource}?)"/>に対するテストです </summary>
        public class 比較対象の要素を全て含むか確認できる
        {
            public class 要素を全て含めばTrueを返す
            {
                [Fact]
                public void イント編()
                {
                    var list1 = new List<int>() { 1, 1, 1, 2, 3 };
                    var list2 = new List<int>() { 1, 1, 1, 2, 3 };
                    var list3 = new List<int>() { 1, 1, 2 };

                    Assert.True(list1.SequenceContains(list2));
                    Assert.True(list1.SequenceContains(list3));
                }

                [Fact]
                public void Record編()
                {
                    var list1 = new List<SampleReco>() { new(1), new(1), new(1), new(2), new(3), new(4) };
                    var list2 = new List<SampleReco>() { new(1), new(1), new(1), new(2), new(3), new(4) };
                    var list3 = new List<SampleReco>() { new(1), new(1), new(2), new(3) };

                    Assert.True(list1.SequenceContains(list2));
                    Assert.True(list1.SequenceContains(list3));
                }
            }

            public class 要素を全て含まなければFalseを返す
            {
                [Fact]
                public void イント編()
                {
                    var list1 = new List<int>() { 1, 1, 2, 3, 4 };
                    var list2 = new List<int>() { 0, 1 };
                    var list3 = new List<int>() { 1, 1, 1 };

                    Assert.False(list1.SequenceContains(list2));
                    Assert.False(list1.SequenceContains(list3));
                }

                [Fact]
                public void Record編()
                {
                    var list1 = new List<SampleReco>() { new(1), new(1), new(2), new(3), new(4), new(5) };
                    var list2 = new List<SampleReco>() { new(0), new(1) };
                    var list3 = new List<SampleReco>() { new(1), new(1), new(1) };

                    Assert.False(list1.SequenceContains(list2));
                    Assert.False(list1.SequenceContains(list3));
                }
            }

            public class 変わったパターン
            {
                [Fact]
                public void Nullを渡すとTrueを返す()
                {
                    var list1 = new List<int>() { 1, 1, 1, 2, 3 };

                    Assert.True(list1.SequenceContains(null));
                }

                [Fact]
                public void サイズからして含有できないコレクションを渡すとFalseを返す()
                {
                    var list1 = new List<int>() { 1, 1, 1, 2, 3 };
                    var list2 = new List<int>() { 1, 3, 2, 1, 1, 1, 1 };

                    Assert.False(list1.SequenceContains(list2));
                }

                [Fact]
                public void 空のコレクションで比較するとTrueを返す()
                {
                    var list1 = new List<int>();
                    var list2 = new List<int>();

                    Assert.True(list1.SequenceContains(list2));
                }

                [Fact]
                public void 同じ参照を渡すとTruを返す()
                {
                    var list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3 };
                    var list2 = list1;
                    list2.Add(1);
                    list2.Add(1);

                    Assert.True(list1.SequenceContains(list2));
                }
            }
        }

        private record SampleReco(int value);
    }
}
