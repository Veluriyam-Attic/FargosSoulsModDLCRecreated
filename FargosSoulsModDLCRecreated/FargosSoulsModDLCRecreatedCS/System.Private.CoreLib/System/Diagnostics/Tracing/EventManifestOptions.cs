using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200073D RID: 1853
	[Flags]
	public enum EventManifestOptions
	{
		// Token: 0x04001AFE RID: 6910
		None = 0,
		// Token: 0x04001AFF RID: 6911
		Strict = 1,
		// Token: 0x04001B00 RID: 6912
		AllCultures = 2,
		// Token: 0x04001B01 RID: 6913
		OnlyIfNeededForRegistration = 4,
		// Token: 0x04001B02 RID: 6914
		AllowEventSourceOverride = 8
	}
}
