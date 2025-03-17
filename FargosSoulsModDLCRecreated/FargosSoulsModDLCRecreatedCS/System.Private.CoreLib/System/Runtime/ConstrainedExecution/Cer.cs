using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020003F3 RID: 1011
	[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public enum Cer
	{
		// Token: 0x04000E1B RID: 3611
		None,
		// Token: 0x04000E1C RID: 3612
		MayFail,
		// Token: 0x04000E1D RID: 3613
		Success
	}
}
