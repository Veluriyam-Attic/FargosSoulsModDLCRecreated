using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CF RID: 207
	[Nullable(0)]
	[NullableContext(1)]
	public class AssemblyLoadEventArgs : EventArgs
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x000C9250 File Offset: 0x000C8450
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.LoadedAssembly = loadedAssembly;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000C925F File Offset: 0x000C845F
		public Assembly LoadedAssembly { get; }
	}
}
