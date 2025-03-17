using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020003F4 RID: 1012
	[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public enum Consistency
	{
		// Token: 0x04000E1F RID: 3615
		MayCorruptProcess,
		// Token: 0x04000E20 RID: 3616
		MayCorruptAppDomain,
		// Token: 0x04000E21 RID: 3617
		MayCorruptInstance,
		// Token: 0x04000E22 RID: 3618
		WillNotCorruptState
	}
}
