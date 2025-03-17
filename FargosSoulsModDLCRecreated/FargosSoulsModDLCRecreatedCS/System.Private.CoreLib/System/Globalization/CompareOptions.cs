using System;

namespace System.Globalization
{
	// Token: 0x020001EA RID: 490
	[Flags]
	public enum CompareOptions
	{
		// Token: 0x040006DA RID: 1754
		None = 0,
		// Token: 0x040006DB RID: 1755
		IgnoreCase = 1,
		// Token: 0x040006DC RID: 1756
		IgnoreNonSpace = 2,
		// Token: 0x040006DD RID: 1757
		IgnoreSymbols = 4,
		// Token: 0x040006DE RID: 1758
		IgnoreKanaType = 8,
		// Token: 0x040006DF RID: 1759
		IgnoreWidth = 16,
		// Token: 0x040006E0 RID: 1760
		OrdinalIgnoreCase = 268435456,
		// Token: 0x040006E1 RID: 1761
		StringSort = 536870912,
		// Token: 0x040006E2 RID: 1762
		Ordinal = 1073741824
	}
}
