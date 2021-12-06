﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Support;

namespace Tools.ValueObject.SizeObj
{
    /// <summary> 高さ </summary>
    /// <remarks> イミュターブルな値オブジェクトです </remarks>
    public record Height : IntValueObject<Height>
    {
        /// <summary> Initializes a new instance of the <see cref="Height"/> class. </summary>
        /// <param name="value">高さ</param>
        public Height(int value) : base(value)
        {
        }
    }
}
