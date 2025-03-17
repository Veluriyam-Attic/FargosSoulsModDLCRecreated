using System;

namespace System.Globalization
{
	// Token: 0x020001F3 RID: 499
	[Flags]
	public enum CultureTypes
	{
		// Token: 0x0400078D RID: 1933
		NeutralCultures = 1,
		// Token: 0x0400078E RID: 1934
		SpecificCultures = 2,
		// Token: 0x0400078F RID: 1935
		InstalledWin32Cultures = 4,
		// Token: 0x04000790 RID: 1936
		AllCultures = 7,
		// Token: 0x04000791 RID: 1937
		UserCustomCulture = 8,
		// Token: 0x04000792 RID: 1938
		ReplacementCultures = 16,
		// Token: 0x04000793 RID: 1939
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		WindowsOnlyCultures = 32,
		// Token: 0x04000794 RID: 1940
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		FrameworkCultures = 64
	}
}
