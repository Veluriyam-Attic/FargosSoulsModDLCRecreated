using System;
using System.Runtime.CompilerServices;

namespace System.Security.Permissions
{
	// Token: 0x020003CF RID: 975
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public abstract class SecurityAttribute : Attribute
	{
		// Token: 0x060031A9 RID: 12713 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		protected SecurityAttribute(SecurityAction action)
		{
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x00169F8F File Offset: 0x0016918F
		// (set) Token: 0x060031AB RID: 12715 RVA: 0x00169F97 File Offset: 0x00169197
		public SecurityAction Action { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x00169FA0 File Offset: 0x001691A0
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x00169FA8 File Offset: 0x001691A8
		public bool Unrestricted { get; set; }

		// Token: 0x060031AE RID: 12718
		[NullableContext(2)]
		public abstract IPermission CreatePermission();
	}
}
