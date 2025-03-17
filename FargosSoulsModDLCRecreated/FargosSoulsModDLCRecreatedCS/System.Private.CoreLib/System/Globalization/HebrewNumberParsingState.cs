using System;

namespace System.Globalization
{
	// Token: 0x0200020A RID: 522
	internal enum HebrewNumberParsingState
	{
		// Token: 0x0400083C RID: 2108
		InvalidHebrewNumber,
		// Token: 0x0400083D RID: 2109
		NotHebrewDigit,
		// Token: 0x0400083E RID: 2110
		FoundEndOfHebrewNumber,
		// Token: 0x0400083F RID: 2111
		ContinueParsing
	}
}
