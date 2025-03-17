using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000755 RID: 1877
	public enum EventChannel : byte
	{
		// Token: 0x04001B9E RID: 7070
		None,
		// Token: 0x04001B9F RID: 7071
		Admin = 16,
		// Token: 0x04001BA0 RID: 7072
		Operational,
		// Token: 0x04001BA1 RID: 7073
		Analytic,
		// Token: 0x04001BA2 RID: 7074
		Debug
	}
}
