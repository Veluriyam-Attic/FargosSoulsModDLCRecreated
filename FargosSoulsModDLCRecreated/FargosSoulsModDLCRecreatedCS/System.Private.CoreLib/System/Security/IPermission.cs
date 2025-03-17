using System;
using System.Runtime.CompilerServices;

namespace System.Security
{
	// Token: 0x020003B4 RID: 948
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[NullableContext(2)]
	public interface IPermission : ISecurityEncodable
	{
		// Token: 0x0600310C RID: 12556
		[NullableContext(1)]
		IPermission Copy();

		// Token: 0x0600310D RID: 12557
		void Demand();

		// Token: 0x0600310E RID: 12558
		IPermission Intersect(IPermission target);

		// Token: 0x0600310F RID: 12559
		bool IsSubsetOf(IPermission target);

		// Token: 0x06003110 RID: 12560
		IPermission Union(IPermission target);
	}
}
