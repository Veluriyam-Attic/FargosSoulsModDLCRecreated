using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000756 RID: 1878
	[Flags]
	public enum EventKeywords : long
	{
		// Token: 0x04001BA4 RID: 7076
		None = 0L,
		// Token: 0x04001BA5 RID: 7077
		All = -1L,
		// Token: 0x04001BA6 RID: 7078
		MicrosoftTelemetry = 562949953421312L,
		// Token: 0x04001BA7 RID: 7079
		WdiContext = 562949953421312L,
		// Token: 0x04001BA8 RID: 7080
		WdiDiagnostic = 1125899906842624L,
		// Token: 0x04001BA9 RID: 7081
		Sqm = 2251799813685248L,
		// Token: 0x04001BAA RID: 7082
		AuditFailure = 4503599627370496L,
		// Token: 0x04001BAB RID: 7083
		AuditSuccess = 9007199254740992L,
		// Token: 0x04001BAC RID: 7084
		CorrelationHint = 4503599627370496L,
		// Token: 0x04001BAD RID: 7085
		EventLogClassic = 36028797018963968L
	}
}
