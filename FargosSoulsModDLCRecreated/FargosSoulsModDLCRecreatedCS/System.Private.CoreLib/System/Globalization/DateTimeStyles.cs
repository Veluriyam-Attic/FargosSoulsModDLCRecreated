using System;

namespace System.Globalization
{
	// Token: 0x020001FC RID: 508
	[Flags]
	public enum DateTimeStyles
	{
		// Token: 0x04000800 RID: 2048
		None = 0,
		// Token: 0x04000801 RID: 2049
		AllowLeadingWhite = 1,
		// Token: 0x04000802 RID: 2050
		AllowTrailingWhite = 2,
		// Token: 0x04000803 RID: 2051
		AllowInnerWhite = 4,
		// Token: 0x04000804 RID: 2052
		AllowWhiteSpaces = 7,
		// Token: 0x04000805 RID: 2053
		NoCurrentDateDefault = 8,
		// Token: 0x04000806 RID: 2054
		AdjustToUniversal = 16,
		// Token: 0x04000807 RID: 2055
		AssumeLocal = 32,
		// Token: 0x04000808 RID: 2056
		AssumeUniversal = 64,
		// Token: 0x04000809 RID: 2057
		RoundtripKind = 128
	}
}
