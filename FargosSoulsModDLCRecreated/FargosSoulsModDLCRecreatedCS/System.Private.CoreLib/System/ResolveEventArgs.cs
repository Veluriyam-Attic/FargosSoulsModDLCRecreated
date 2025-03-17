using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000178 RID: 376
	[NullableContext(1)]
	[Nullable(0)]
	public class ResolveEventArgs : EventArgs
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x000E8869 File Offset: 0x000E7A69
		public ResolveEventArgs(string name)
		{
			this.Name = name;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000E8878 File Offset: 0x000E7A78
		public ResolveEventArgs(string name, [Nullable(2)] Assembly requestingAssembly)
		{
			this.Name = name;
			this.RequestingAssembly = requestingAssembly;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000E888E File Offset: 0x000E7A8E
		public string Name { get; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000E8896 File Offset: 0x000E7A96
		[Nullable(2)]
		public Assembly RequestingAssembly { [NullableContext(2)] get; }
	}
}
