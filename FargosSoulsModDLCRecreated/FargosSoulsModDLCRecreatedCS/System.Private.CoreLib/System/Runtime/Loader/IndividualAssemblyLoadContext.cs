using System;

namespace System.Runtime.Loader
{
	// Token: 0x0200040E RID: 1038
	internal sealed class IndividualAssemblyLoadContext : AssemblyLoadContext
	{
		// Token: 0x06003317 RID: 13079 RVA: 0x0016D080 File Offset: 0x0016C280
		internal IndividualAssemblyLoadContext(string name) : base(false, false, name)
		{
		}
	}
}
