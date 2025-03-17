using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200072F RID: 1839
	[Flags]
	public enum EventSourceSettings
	{
		// Token: 0x04001AB7 RID: 6839
		Default = 0,
		// Token: 0x04001AB8 RID: 6840
		ThrowOnEventWriteErrors = 1,
		// Token: 0x04001AB9 RID: 6841
		EtwManifestEventFormat = 4,
		// Token: 0x04001ABA RID: 6842
		EtwSelfDescribingEventFormat = 8
	}
}
