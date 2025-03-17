using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000403 RID: 1027
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04000E3E RID: 3646
		None = 0,
		// Token: 0x04000E3F RID: 3647
		AppDomainID = 1,
		// Token: 0x04000E40 RID: 3648
		ProcessID = 2,
		// Token: 0x04000E41 RID: 3649
		CLRInstanceID = 4,
		// Token: 0x04000E42 RID: 3650
		AssemblyName = 8,
		// Token: 0x04000E43 RID: 3651
		TypeName = 16
	}
}
