using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047F RID: 1151
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x06004473 RID: 17523 RVA: 0x00179196 File Offset: 0x00178396
		public DllImportAttribute(string dllName)
		{
			this.Value = dllName;
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x001791A5 File Offset: 0x001783A5
		public string Value { get; }

		// Token: 0x04000F34 RID: 3892
		[Nullable(2)]
		public string EntryPoint;

		// Token: 0x04000F35 RID: 3893
		public CharSet CharSet;

		// Token: 0x04000F36 RID: 3894
		public bool SetLastError;

		// Token: 0x04000F37 RID: 3895
		public bool ExactSpelling;

		// Token: 0x04000F38 RID: 3896
		public CallingConvention CallingConvention;

		// Token: 0x04000F39 RID: 3897
		public bool BestFitMapping;

		// Token: 0x04000F3A RID: 3898
		public bool PreserveSig;

		// Token: 0x04000F3B RID: 3899
		public bool ThrowOnUnmappableChar;
	}
}
