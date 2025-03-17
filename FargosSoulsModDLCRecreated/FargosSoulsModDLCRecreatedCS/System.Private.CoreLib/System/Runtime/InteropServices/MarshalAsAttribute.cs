using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000493 RID: 1171
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x060044A2 RID: 17570 RVA: 0x001793E4 File Offset: 0x001785E4
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.Value = unmanagedType;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x001793E4 File Offset: 0x001785E4
		public MarshalAsAttribute(short unmanagedType)
		{
			this.Value = unmanagedType;
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x001793F3 File Offset: 0x001785F3
		public UnmanagedType Value { get; }

		// Token: 0x04000F55 RID: 3925
		public VarEnum SafeArraySubType;

		// Token: 0x04000F56 RID: 3926
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04000F57 RID: 3927
		public int IidParameterIndex;

		// Token: 0x04000F58 RID: 3928
		public UnmanagedType ArraySubType;

		// Token: 0x04000F59 RID: 3929
		public short SizeParamIndex;

		// Token: 0x04000F5A RID: 3930
		public int SizeConst;

		// Token: 0x04000F5B RID: 3931
		public string MarshalType;

		// Token: 0x04000F5C RID: 3932
		public Type MarshalTypeRef;

		// Token: 0x04000F5D RID: 3933
		public string MarshalCookie;
	}
}
