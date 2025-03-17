using System;

namespace System.Security
{
	// Token: 0x020003B6 RID: 950
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public interface IStackWalk
	{
		// Token: 0x06003113 RID: 12563
		void Assert();

		// Token: 0x06003114 RID: 12564
		void Demand();

		// Token: 0x06003115 RID: 12565
		void Deny();

		// Token: 0x06003116 RID: 12566
		void PermitOnly();
	}
}
