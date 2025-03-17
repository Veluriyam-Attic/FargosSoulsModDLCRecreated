using System;

namespace System.Runtime.Loader
{
	// Token: 0x0200040D RID: 1037
	internal sealed class DefaultAssemblyLoadContext : AssemblyLoadContext
	{
		// Token: 0x06003315 RID: 13077 RVA: 0x0016D065 File Offset: 0x0016C265
		internal DefaultAssemblyLoadContext() : base(true, false, "Default")
		{
		}

		// Token: 0x04000E6D RID: 3693
		internal static readonly AssemblyLoadContext s_loadContext = new DefaultAssemblyLoadContext();
	}
}
