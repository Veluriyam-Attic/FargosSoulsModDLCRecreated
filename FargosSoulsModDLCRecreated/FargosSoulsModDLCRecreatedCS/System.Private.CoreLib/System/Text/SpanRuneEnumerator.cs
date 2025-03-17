using System;

namespace System.Text
{
	// Token: 0x0200037E RID: 894
	public ref struct SpanRuneEnumerator
	{
		// Token: 0x06002F56 RID: 12118 RVA: 0x0016020B File Offset: 0x0015F40B
		internal SpanRuneEnumerator(ReadOnlySpan<char> buffer)
		{
			this._remaining = buffer;
			this._current = default(Rune);
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x00160220 File Offset: 0x0015F420
		public Rune Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x00160228 File Offset: 0x0015F428
		public SpanRuneEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00160230 File Offset: 0x0015F430
		public bool MoveNext()
		{
			if (this._remaining.IsEmpty)
			{
				this._current = default(Rune);
				return false;
			}
			int num = Rune.ReadFirstRuneFromUtf16Buffer(this._remaining);
			if (num < 0)
			{
				num = Rune.ReplacementChar.Value;
			}
			this._current = Rune.UnsafeCreate((uint)num);
			this._remaining = this._remaining.Slice(this._current.Utf16SequenceLength);
			return true;
		}

		// Token: 0x04000CF4 RID: 3316
		private ReadOnlySpan<char> _remaining;

		// Token: 0x04000CF5 RID: 3317
		private Rune _current;
	}
}
