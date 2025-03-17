using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x0200014D RID: 333
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x000DC159 File Offset: 0x000DB359
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000DC16D File Offset: 0x000DB36D
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000DC17C File Offset: 0x000DB37C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		// Token: 0x0400041F RID: 1055
		private readonly ReadOnlyMemory<T> _memory;
	}
}
