using System;

#pragma warning disable SA1649 // File name should match first type name

namespace LinqLike
{
    /// <summary> バグでしかないんじゃない？というタイミングでスローされる例外です </summary>
    public class JustBugException : Exception
    {
        /// <summary>　テンプレ例外インスタンス　</summary>
        public static JustBugException Template { get; } = new JustBugException("ありえねー。十中八九バグ☺️。カス💢");

        /// <summary> Initializes a new instance of the <see cref="JustBugException"/> class. </summary>
        /// <param name="massage"> メッセージ </param>
        /// <param name="innerException"> インナーエクセプション </param>
        public JustBugException(string? massage, Exception? innerException = null) : base(massage, innerException)
        {
        }
    }
}

#pragma warning restore SA1649 // File name should match first type name