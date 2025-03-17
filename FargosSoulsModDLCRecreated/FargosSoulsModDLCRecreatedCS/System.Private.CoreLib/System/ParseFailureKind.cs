using System;

namespace System
{
	// Token: 0x0200011B RID: 283
	internal enum ParseFailureKind
	{
		// Token: 0x04000377 RID: 887
		None,
		// Token: 0x04000378 RID: 888
		ArgumentNull,
		// Token: 0x04000379 RID: 889
		Format,
		// Token: 0x0400037A RID: 890
		FormatWithParameter,
		// Token: 0x0400037B RID: 891
		FormatWithOriginalDateTime,
		// Token: 0x0400037C RID: 892
		FormatWithFormatSpecifier,
		// Token: 0x0400037D RID: 893
		FormatWithOriginalDateTimeAndParameter,
		// Token: 0x0400037E RID: 894
		FormatBadDateTimeCalendar
	}
}
