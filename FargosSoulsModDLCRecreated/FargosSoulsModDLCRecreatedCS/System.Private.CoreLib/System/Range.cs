using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000173 RID: 371
	public readonly struct Range : IEquatable<Range>
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x000E7D70 File Offset: 0x000E6F70
		public Index Start { get; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x000E7D78 File Offset: 0x000E6F78
		public Index End { get; }

		// Token: 0x06001295 RID: 4757 RVA: 0x000E7D80 File Offset: 0x000E6F80
		public Range(Index start, Index end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000E7D90 File Offset: 0x000E6F90
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			if (value is Range)
			{
				Range range = (Range)value;
				if (range.Start.Equals(this.Start))
				{
					return range.End.Equals(this.End);
				}
			}
			return false;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000E7DDC File Offset: 0x000E6FDC
		public bool Equals(Range other)
		{
			return other.Start.Equals(this.Start) && other.End.Equals(this.End);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000E7E18 File Offset: 0x000E7018
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Start.GetHashCode(), this.End.GetHashCode());
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000E7E54 File Offset: 0x000E7054
		[NullableContext(1)]
		public unsafe override string ToString()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)48], 24);
			Span<char> span2 = span;
			int num = 0;
			if (this.Start.IsFromEnd)
			{
				*span2[0] = '^';
				num = 1;
			}
			int num2;
			bool flag = ((uint)this.Start.Value).TryFormat(span2.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			*span2[num++] = '.';
			*span2[num++] = '.';
			if (this.End.IsFromEnd)
			{
				*span2[num++] = '^';
			}
			flag = ((uint)this.End.Value).TryFormat(span2.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			return new string(span2.Slice(0, num));
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000E7F47 File Offset: 0x000E7147
		public static Range StartAt(Index start)
		{
			return new Range(start, Index.End);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000E7F54 File Offset: 0x000E7154
		public static Range EndAt(Index end)
		{
			return new Range(Index.Start, end);
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x000E7F61 File Offset: 0x000E7161
		public static Range All
		{
			get
			{
				return new Range(Index.Start, Index.End);
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000E7F74 File Offset: 0x000E7174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: TupleElementNames(new string[]
		{
			"Offset",
			"Length"
		})]
		public ValueTuple<int, int> GetOffsetAndLength(int length)
		{
			Index start = this.Start;
			int num;
			if (start.IsFromEnd)
			{
				num = length - start.Value;
			}
			else
			{
				num = start.Value;
			}
			Index end = this.End;
			int num2;
			if (end.IsFromEnd)
			{
				num2 = length - end.Value;
			}
			else
			{
				num2 = end.Value;
			}
			if (num2 > length || num > num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new ValueTuple<int, int>(num, num2 - num);
		}
	}
}
