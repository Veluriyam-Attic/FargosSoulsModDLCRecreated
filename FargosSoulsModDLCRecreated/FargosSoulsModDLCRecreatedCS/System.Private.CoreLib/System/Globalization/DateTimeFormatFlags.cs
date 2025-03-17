using System;

namespace System.Globalization
{
	// Token: 0x020001F5 RID: 501
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x0400079A RID: 1946
		None = 0,
		// Token: 0x0400079B RID: 1947
		UseGenitiveMonth = 1,
		// Token: 0x0400079C RID: 1948
		UseLeapYearMonth = 2,
		// Token: 0x0400079D RID: 1949
		UseSpacesInMonthNames = 4,
		// Token: 0x0400079E RID: 1950
		UseHebrewRule = 8,
		// Token: 0x0400079F RID: 1951
		UseSpacesInDayNames = 16,
		// Token: 0x040007A0 RID: 1952
		UseDigitPrefixInTokens = 32,
		// Token: 0x040007A1 RID: 1953
		NotInitialized = -1
	}
}
