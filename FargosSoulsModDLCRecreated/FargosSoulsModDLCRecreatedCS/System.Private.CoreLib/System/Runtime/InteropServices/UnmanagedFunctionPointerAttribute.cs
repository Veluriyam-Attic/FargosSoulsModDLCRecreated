using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A4 RID: 1188
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		// Token: 0x060044E9 RID: 17641 RVA: 0x00179B8A File Offset: 0x00178D8A
		public UnmanagedFunctionPointerAttribute()
		{
			this.CallingConvention = 1;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x00179B99 File Offset: 0x00178D99
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.CallingConvention = callingConvention;
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x00179BA8 File Offset: 0x00178DA8
		public CallingConvention CallingConvention { get; }

		// Token: 0x04000F6E RID: 3950
		public bool BestFitMapping;

		// Token: 0x04000F6F RID: 3951
		public bool SetLastError;

		// Token: 0x04000F70 RID: 3952
		public bool ThrowOnUnmappableChar;

		// Token: 0x04000F71 RID: 3953
		public CharSet CharSet;
	}
}
