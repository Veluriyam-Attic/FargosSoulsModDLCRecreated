using System;

namespace System
{
	// Token: 0x02000118 RID: 280
	internal ref struct DTSubString
	{
		// Token: 0x17000153 RID: 339
		internal unsafe char this[int relativeIndex]
		{
			get
			{
				return (char)(*this.s[this.index + relativeIndex]);
			}
		}

		// Token: 0x04000365 RID: 869
		internal ReadOnlySpan<char> s;

		// Token: 0x04000366 RID: 870
		internal int index;

		// Token: 0x04000367 RID: 871
		internal int length;

		// Token: 0x04000368 RID: 872
		internal DTSubStringType type;

		// Token: 0x04000369 RID: 873
		internal int value;
	}
}
