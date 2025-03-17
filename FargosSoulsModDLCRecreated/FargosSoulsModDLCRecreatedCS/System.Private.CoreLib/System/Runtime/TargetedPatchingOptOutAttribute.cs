using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x020003DA RID: 986
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x0016A595 File Offset: 0x00169795
		public string Reason { get; }

		// Token: 0x060031F6 RID: 12790 RVA: 0x0016A59D File Offset: 0x0016979D
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.Reason = reason;
		}
	}
}
