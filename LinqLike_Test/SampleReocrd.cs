using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable SA1649 // File name should match first type name

namespace LinqLike_Test
{

    public record Sample1(int Value) : IComparable<Sample1>
    {
        /// <inheritdoc/>
        public int CompareTo(Sample1? other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (this.Value < other.Value) return -1;
            if (this.Value > other.Value) return 1;
            return 0;
        }
    }

    public record Sample2(int Value);

    public record Sample3(int Value);

    public record SampleComplexReocrd(Sample1 Value1, Sample2 Valeu2, Sample3 Valeu3);
}

#pragma warning restore SA1649 // File name should match first type name