using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000495 RID: 1173
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class UnmanagedCallersOnlyAttribute : Attribute
	{
		// Token: 0x04000F5E RID: 3934
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Type[] CallConvs;

		// Token: 0x04000F5F RID: 3935
		[Nullable(2)]
		public string EntryPoint;
	}
}
