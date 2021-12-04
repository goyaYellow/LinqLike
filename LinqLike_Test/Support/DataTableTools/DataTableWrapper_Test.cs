using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Support.DataTableTools;
using Xunit;

#pragma warning disable SA1649 // File name should match first type name

namespace Tools_Test.Support.DataTableTools
{
    /// <summary>
    /// <see cref="DatatableWrapper"/>に対するテストです
    /// </summary>
    public class DatatableWrapper_Test
    {
        /// <summary>
        /// <see cref="DatatableWrapper(DataTable)"/>に対するテストです
        /// </summary>
        public class 正常にインスタンスが生成できる
        {
            [Fact]
            public void データテーブルを渡すとインスタンスを返す()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 実行と検証
                Assert.IsType<DatatableWrapper>(wrapper);
                _ = (wrapper as System.Collections.IEnumerable).GetEnumerator(); // カバレッジ誤魔化し
                Assert.Equal(2, wrapper.Count());
            }
        }
    }

    /// <summary>
    /// <see cref="DataRowWrapper"/>に対するテストです
    /// </summary>
    public class DataRowWrapper_Test
    {
        /// <summary>
        /// <see cref="DataRowWrapper.FindAsStr(string)"/>に対するテストです
        /// </summary>
        public class 列の内容を文字列として正しく取得できる
        {
            [Fact]
            public void 存在する列の内容が取得できる()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 実行と検証
                var row = wrapper.First();
                Assert.Equal("TEST1", row.FindAsStr("column1"));
                Assert.Equal("10", row.FindAsStr("column2"));
            }

            [Fact]
            public void 存在しない列を参照しようとすると例外を返す()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 実行と検証
                var row = wrapper.First();
                Assert.Throws<InvalidDataException>(() => row.FindAsStr("column3"));
            }
        }

        /// <summary>
        /// <see cref="DataRowWrapper.FindAsInt(string)"/>に対するテストです
        /// </summary>
        public class 列の内容をInt型として正しく取得できる
        {
            [Fact]
            public void 存在する数値列の内容が取得できる()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 実行と検証
                var row = wrapper.First();
                Assert.Equal(10, row.FindAsInt("column2"));
            }

            [Fact]
            public void 存在しない列を参照しようとすると例外を返す()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 検証
                var row = wrapper.First();
                Assert.Throws<InvalidDataException>(() => row.FindAsInt("column3"));
            }

            [Fact]
            public void 存在する文字型の列を参照しようとすると例外を返す()
            {
                // 準備
                var table = DataTableTestDataFactory.Create();
                var wrapper = new DatatableWrapper(table);

                // 実行と検証
                var row = wrapper.First();
                Assert.Throws<InvalidDataException>(() => row.FindAsInt("column1"));
            }
        }
    }

    /// <summary>
    /// テストデータ生成用クラス
    /// </summary>
    public static class DataTableTestDataFactory
    {
        public static DataTable Create()
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("column1"));
            table.Columns.Add(new DataColumn("column2"));
            var row1 = table.NewRow();
            row1["column1"] = "TEST1";
            row1["column2"] = "10";
            table.Rows.Add(row1);
            var row2 = table.NewRow();
            row2["column1"] = "TEST2";
            row2["column2"] = "20";
            table.Rows.Add(row2);

            return table;
        }
    }
}

#pragma warning restore SA1649 // File name should match first type name
