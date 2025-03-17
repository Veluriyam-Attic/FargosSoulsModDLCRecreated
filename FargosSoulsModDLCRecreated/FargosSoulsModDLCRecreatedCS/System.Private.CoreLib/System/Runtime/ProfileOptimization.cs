using System;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace System.Runtime
{
	// Token: 0x020003DB RID: 987
	public static class ProfileOptimization
	{
		// Token: 0x060031F7 RID: 12791 RVA: 0x0016A5AC File Offset: 0x001697AC
		[NullableContext(1)]
		public static void SetProfileRoot(string directoryPath)
		{
			AssemblyLoadContext.Default.SetProfileOptimizationRoot(directoryPath);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x0016A5B9 File Offset: 0x001697B9
		[NullableContext(2)]
		public static void StartProfile(string profile)
		{
			AssemblyLoadContext.Default.StartProfileOptimization(profile);
		}
	}
}
