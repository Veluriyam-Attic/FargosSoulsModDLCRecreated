using System;
using System.Runtime.CompilerServices;

namespace System.Security.Principal
{
	// Token: 0x020003C8 RID: 968
	[NullableContext(2)]
	public interface IIdentity
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060031A3 RID: 12707
		string Name { get; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060031A4 RID: 12708
		string AuthenticationType { get; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060031A5 RID: 12709
		bool IsAuthenticated { get; }
	}
}
