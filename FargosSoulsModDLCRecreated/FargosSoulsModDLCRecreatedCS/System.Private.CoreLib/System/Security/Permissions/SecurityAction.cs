using System;

namespace System.Security.Permissions
{
	// Token: 0x020003CE RID: 974
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public enum SecurityAction
	{
		// Token: 0x04000DB1 RID: 3505
		Assert = 3,
		// Token: 0x04000DB2 RID: 3506
		Demand = 2,
		// Token: 0x04000DB3 RID: 3507
		Deny = 4,
		// Token: 0x04000DB4 RID: 3508
		InheritanceDemand = 7,
		// Token: 0x04000DB5 RID: 3509
		LinkDemand = 6,
		// Token: 0x04000DB6 RID: 3510
		PermitOnly = 5,
		// Token: 0x04000DB7 RID: 3511
		RequestMinimum = 8,
		// Token: 0x04000DB8 RID: 3512
		RequestOptional,
		// Token: 0x04000DB9 RID: 3513
		RequestRefuse
	}
}
