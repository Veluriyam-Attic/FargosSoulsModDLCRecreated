using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x0200081B RID: 2075
	internal sealed class IsolatedComponentLoadContext : AssemblyLoadContext
	{
		// Token: 0x0600627B RID: 25211 RVA: 0x001D4259 File Offset: 0x001D3459
		public IsolatedComponentLoadContext(string componentAssemblyPath) : base("IsolatedComponentLoadContext(" + componentAssemblyPath + ")", false)
		{
			this._resolver = new AssemblyDependencyResolver(componentAssemblyPath);
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x001D4280 File Offset: 0x001D3480
		protected override Assembly Load(AssemblyName assemblyName)
		{
			string text = this._resolver.ResolveAssemblyToPath(assemblyName);
			if (text != null)
			{
				return base.LoadFromAssemblyPath(text);
			}
			return null;
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x001D42A8 File Offset: 0x001D34A8
		protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
		{
			string text = this._resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
			if (text != null)
			{
				return base.LoadUnmanagedDllFromPath(text);
			}
			return IntPtr.Zero;
		}

		// Token: 0x04001D58 RID: 7512
		private readonly AssemblyDependencyResolver _resolver;
	}
}
