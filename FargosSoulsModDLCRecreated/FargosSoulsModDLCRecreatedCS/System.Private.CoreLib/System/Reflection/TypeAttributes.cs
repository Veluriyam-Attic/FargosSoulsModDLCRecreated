using System;

namespace System.Reflection
{
	// Token: 0x0200060B RID: 1547
	[Flags]
	public enum TypeAttributes
	{
		// Token: 0x040013E2 RID: 5090
		VisibilityMask = 7,
		// Token: 0x040013E3 RID: 5091
		NotPublic = 0,
		// Token: 0x040013E4 RID: 5092
		Public = 1,
		// Token: 0x040013E5 RID: 5093
		NestedPublic = 2,
		// Token: 0x040013E6 RID: 5094
		NestedPrivate = 3,
		// Token: 0x040013E7 RID: 5095
		NestedFamily = 4,
		// Token: 0x040013E8 RID: 5096
		NestedAssembly = 5,
		// Token: 0x040013E9 RID: 5097
		NestedFamANDAssem = 6,
		// Token: 0x040013EA RID: 5098
		NestedFamORAssem = 7,
		// Token: 0x040013EB RID: 5099
		LayoutMask = 24,
		// Token: 0x040013EC RID: 5100
		AutoLayout = 0,
		// Token: 0x040013ED RID: 5101
		SequentialLayout = 8,
		// Token: 0x040013EE RID: 5102
		ExplicitLayout = 16,
		// Token: 0x040013EF RID: 5103
		ClassSemanticsMask = 32,
		// Token: 0x040013F0 RID: 5104
		Class = 0,
		// Token: 0x040013F1 RID: 5105
		Interface = 32,
		// Token: 0x040013F2 RID: 5106
		Abstract = 128,
		// Token: 0x040013F3 RID: 5107
		Sealed = 256,
		// Token: 0x040013F4 RID: 5108
		SpecialName = 1024,
		// Token: 0x040013F5 RID: 5109
		Import = 4096,
		// Token: 0x040013F6 RID: 5110
		Serializable = 8192,
		// Token: 0x040013F7 RID: 5111
		WindowsRuntime = 16384,
		// Token: 0x040013F8 RID: 5112
		StringFormatMask = 196608,
		// Token: 0x040013F9 RID: 5113
		AnsiClass = 0,
		// Token: 0x040013FA RID: 5114
		UnicodeClass = 65536,
		// Token: 0x040013FB RID: 5115
		AutoClass = 131072,
		// Token: 0x040013FC RID: 5116
		CustomFormatClass = 196608,
		// Token: 0x040013FD RID: 5117
		CustomFormatMask = 12582912,
		// Token: 0x040013FE RID: 5118
		BeforeFieldInit = 1048576,
		// Token: 0x040013FF RID: 5119
		RTSpecialName = 2048,
		// Token: 0x04001400 RID: 5120
		HasSecurity = 262144,
		// Token: 0x04001401 RID: 5121
		ReservedMask = 264192
	}
}
