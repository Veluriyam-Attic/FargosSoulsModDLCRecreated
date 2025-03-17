using System;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x02000825 RID: 2085
	public static class InMemoryAssemblyLoader
	{
		// Token: 0x060062A5 RID: 25253 RVA: 0x001D4DC4 File Offset: 0x001D3FC4
		public static void LoadInMemoryAssembly(IntPtr moduleHandle, IntPtr assemblyPath)
		{
			string text = Marshal.PtrToStringUni(assemblyPath);
			if (text == null)
			{
				throw new ArgumentOutOfRangeException("assemblyPath");
			}
			AssemblyLoadContext assemblyLoadContext = new IsolatedComponentLoadContext(text);
			assemblyLoadContext.LoadFromInMemoryModule(moduleHandle);
		}
	}
}
