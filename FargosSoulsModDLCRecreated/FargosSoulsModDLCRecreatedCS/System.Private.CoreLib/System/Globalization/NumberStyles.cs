using System;

namespace System.Globalization
{
	// Token: 0x0200021C RID: 540
	[Flags]
	public enum NumberStyles
	{
		// Token: 0x040008AF RID: 2223
		None = 0,
		// Token: 0x040008B0 RID: 2224
		AllowLeadingWhite = 1,
		// Token: 0x040008B1 RID: 2225
		AllowTrailingWhite = 2,
		// Token: 0x040008B2 RID: 2226
		AllowLeadingSign = 4,
		// Token: 0x040008B3 RID: 2227
		AllowTrailingSign = 8,
		// Token: 0x040008B4 RID: 2228
		AllowParentheses = 16,
		// Token: 0x040008B5 RID: 2229
		AllowDecimalPoint = 32,
		// Token: 0x040008B6 RID: 2230
		AllowThousands = 64,
		// Token: 0x040008B7 RID: 2231
		AllowExponent = 128,
		// Token: 0x040008B8 RID: 2232
		AllowCurrencySymbol = 256,
		// Token: 0x040008B9 RID: 2233
		AllowHexSpecifier = 512,
		// Token: 0x040008BA RID: 2234
		Integer = 7,
		// Token: 0x040008BB RID: 2235
		HexNumber = 515,
		// Token: 0x040008BC RID: 2236
		Number = 111,
		// Token: 0x040008BD RID: 2237
		Float = 167,
		// Token: 0x040008BE RID: 2238
		Currency = 383,
		// Token: 0x040008BF RID: 2239
		Any = 511
	}
}
