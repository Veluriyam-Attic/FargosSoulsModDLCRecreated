using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000546 RID: 1350
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x0600474A RID: 18250 RVA: 0x0017D632 File Offset: 0x0017C832
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this.Value = methodImplOptions;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0017D632 File Offset: 0x0017C832
		public MethodImplAttribute(short value)
		{
			this.Value = value;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		public MethodImplAttribute()
		{
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x0017D641 File Offset: 0x0017C841
		public MethodImplOptions Value { get; }

		// Token: 0x04001127 RID: 4391
		public MethodCodeType MethodCodeType;
	}
}
