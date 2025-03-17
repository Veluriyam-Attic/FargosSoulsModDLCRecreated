using System;

namespace System
{
	// Token: 0x02000089 RID: 137
	internal enum TypeNameFormatFlags
	{
		// Token: 0x040001B7 RID: 439
		FormatBasic,
		// Token: 0x040001B8 RID: 440
		FormatNamespace,
		// Token: 0x040001B9 RID: 441
		FormatFullInst,
		// Token: 0x040001BA RID: 442
		FormatAssembly = 4,
		// Token: 0x040001BB RID: 443
		FormatSignature = 8,
		// Token: 0x040001BC RID: 444
		FormatNoVersion = 16,
		// Token: 0x040001BD RID: 445
		FormatAngleBrackets = 64,
		// Token: 0x040001BE RID: 446
		FormatStubInfo = 128,
		// Token: 0x040001BF RID: 447
		FormatGenericParam = 256
	}
}
