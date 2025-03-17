using System;

namespace System.Collections.Generic
{
	// Token: 0x02000810 RID: 2064
	internal ref struct BitHelper
	{
		// Token: 0x06006239 RID: 25145 RVA: 0x001D3629 File Offset: 0x001D2829
		internal BitHelper(Span<int> span, bool clear)
		{
			if (clear)
			{
				span.Clear();
			}
			this._span = span;
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x001D363C File Offset: 0x001D283C
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._span.Length)
			{
				*this._span[num] |= 1 << bitPosition % 32;
			}
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x001D3678 File Offset: 0x001D2878
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			return num < this._span.Length && (*this._span[num] & 1 << bitPosition % 32) != 0;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x001D36B4 File Offset: 0x001D28B4
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04001D50 RID: 7504
		private readonly Span<int> _span;
	}
}
