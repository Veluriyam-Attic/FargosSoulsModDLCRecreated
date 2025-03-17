using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000130 RID: 304
	public readonly struct Index : IEquatable<Index>
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x000DA3C6 File Offset: 0x000D95C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Index(int value, bool fromEnd = false)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			if (fromEnd)
			{
				this._value = ~value;
				return;
			}
			this._value = value;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000DA3E4 File Offset: 0x000D95E4
		private Index(int value)
		{
			this._value = value;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000DA3ED File Offset: 0x000D95ED
		public static Index Start
		{
			get
			{
				return new Index(0);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000DA3F5 File Offset: 0x000D95F5
		public static Index End
		{
			get
			{
				return new Index(-1);
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000DA3FD File Offset: 0x000D95FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromStart(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(value);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000DA40E File Offset: 0x000D960E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromEnd(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(~value);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000DA420 File Offset: 0x000D9620
		public int Value
		{
			get
			{
				if (this._value < 0)
				{
					return ~this._value;
				}
				return this._value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x000DA439 File Offset: 0x000D9639
		public bool IsFromEnd
		{
			get
			{
				return this._value < 0;
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000DA444 File Offset: 0x000D9644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetOffset(int length)
		{
			int num = this._value;
			if (this.IsFromEnd)
			{
				num += length + 1;
			}
			return num;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x000DA467 File Offset: 0x000D9667
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is Index && this._value == ((Index)value)._value;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000DA486 File Offset: 0x000D9686
		public bool Equals(Index other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000DA496 File Offset: 0x000D9696
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000DA49E File Offset: 0x000D969E
		public static implicit operator Index(int value)
		{
			return Index.FromStart(value);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000DA4A8 File Offset: 0x000D96A8
		[NullableContext(1)]
		public override string ToString()
		{
			if (this.IsFromEnd)
			{
				return this.ToStringFromEnd();
			}
			return ((uint)this.Value).ToString();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000DA4D4 File Offset: 0x000D96D4
		private unsafe string ToStringFromEnd()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)22], 11);
			Span<char> span2 = span;
			int num;
			bool flag = ((uint)this.Value).TryFormat(span2.Slice(1), out num, default(ReadOnlySpan<char>), null);
			*span2[0] = '^';
			return new string(span2.Slice(0, num + 1));
		}

		// Token: 0x040003E9 RID: 1001
		private readonly int _value;
	}
}
