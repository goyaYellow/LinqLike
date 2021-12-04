using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Logger;
using Tools.OriginalException;
using Xunit;

namespace Tools_Test.Logger
{
    public class Logger_Test
    {
        private const string DefoltMassage = "test";
        private const Level DefoltLevel = Level.Debug;

        private readonly Tools.Logger.Logger logger = new Tools.Logger.Logger();

        [Fact]
        public void インスタンス化できる()
        {
            var logger = new Tools.Logger.Logger();

            Assert.IsType<Tools.Logger.Logger>(logger);
        }

        [Fact]
        public void 正常な引数を渡したら例外をスローしない()
        {
            this.logger.Log(DefoltLevel, DefoltMassage);

            // 上で失敗しなければ成功
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void massageに空文字やnullを渡しても例外をスローしない(string? massage)
        {
            this.logger.Log(DefoltLevel, massage);

            // 上で失敗しなければ成功
        }

        [Fact]
        public void メッセージにカンマ入りの文字列を渡すと例外をスローする()
        {
            Assert.Throws<ArgumentException>(() => this.logger.Log(DefoltLevel, DefoltMassage + "," + DefoltMassage));
        }
    }
}
