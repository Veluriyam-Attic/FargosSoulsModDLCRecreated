using System;
using System.Runtime.CompilerServices;

namespace System.Security.Principal
{
	// Token: 0x020003C9 RID: 969
	[NullableContext(2)]
	public interface IPrincipal
	{
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060031A6 RID: 12710
		IIdentity Identity { get; }

		// Token: 0x060031A7 RID: 12711
		[NullableContext(1)]
		bool IsInRole(string role);
	}
}
