using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005F0 RID: 1520
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x0018BFA2 File Offset: 0x0018B1A2
		// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x0018BFAA File Offset: 0x0018B1AA
		public bool StripAfterObfuscation { get; set; } = true;

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x0018BFB3 File Offset: 0x0018B1B3
		// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x0018BFBB File Offset: 0x0018B1BB
		public bool Exclude { get; set; } = true;

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x0018BFC4 File Offset: 0x0018B1C4
		// (set) Token: 0x06004CDA RID: 19674 RVA: 0x0018BFCC File Offset: 0x0018B1CC
		public bool ApplyToMembers { get; set; } = true;

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x0018BFD5 File Offset: 0x0018B1D5
		// (set) Token: 0x06004CDC RID: 19676 RVA: 0x0018BFDD File Offset: 0x0018B1DD
		public string Feature { get; set; } = "all";
	}
}
