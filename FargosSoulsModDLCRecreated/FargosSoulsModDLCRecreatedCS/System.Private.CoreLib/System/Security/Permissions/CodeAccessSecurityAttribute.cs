using System;

namespace System.Security.Permissions
{
	// Token: 0x020003CC RID: 972
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		// Token: 0x060031A8 RID: 12712 RVA: 0x00169F86 File Offset: 0x00169186
		protected CodeAccessSecurityAttribute(SecurityAction action) : base((SecurityAction)0)
		{
		}
	}
}
