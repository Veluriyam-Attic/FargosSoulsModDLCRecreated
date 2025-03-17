using System;

namespace System.Security
{
	// Token: 0x020003BB RID: 955
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		// Token: 0x06003156 RID: 12630 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		public SecurityCriticalAttribute()
		{
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x00169107 File Offset: 0x00168307
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this.Scope = scope;
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x00169116 File Offset: 0x00168316
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope { get; }
	}
}
