using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000760 RID: 1888
	public enum EventFieldFormat
	{
		// Token: 0x04001BC8 RID: 7112
		Default,
		// Token: 0x04001BC9 RID: 7113
		String = 2,
		// Token: 0x04001BCA RID: 7114
		Boolean,
		// Token: 0x04001BCB RID: 7115
		Hexadecimal,
		// Token: 0x04001BCC RID: 7116
		Xml = 11,
		// Token: 0x04001BCD RID: 7117
		Json,
		// Token: 0x04001BCE RID: 7118
		HResult = 15
	}
}
