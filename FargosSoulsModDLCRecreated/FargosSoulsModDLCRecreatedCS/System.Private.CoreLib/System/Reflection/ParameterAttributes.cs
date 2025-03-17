using System;

namespace System.Reflection
{
	// Token: 0x020005F1 RID: 1521
	[Flags]
	public enum ParameterAttributes
	{
		// Token: 0x040013A7 RID: 5031
		None = 0,
		// Token: 0x040013A8 RID: 5032
		In = 1,
		// Token: 0x040013A9 RID: 5033
		Out = 2,
		// Token: 0x040013AA RID: 5034
		Lcid = 4,
		// Token: 0x040013AB RID: 5035
		Retval = 8,
		// Token: 0x040013AC RID: 5036
		Optional = 16,
		// Token: 0x040013AD RID: 5037
		HasDefault = 4096,
		// Token: 0x040013AE RID: 5038
		HasFieldMarshal = 8192,
		// Token: 0x040013AF RID: 5039
		Reserved3 = 16384,
		// Token: 0x040013B0 RID: 5040
		Reserved4 = 32768,
		// Token: 0x040013B1 RID: 5041
		ReservedMask = 61440
	}
}
