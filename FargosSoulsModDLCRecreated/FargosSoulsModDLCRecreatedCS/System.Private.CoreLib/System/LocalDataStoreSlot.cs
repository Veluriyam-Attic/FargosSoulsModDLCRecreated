using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000148 RID: 328
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x000DB7D3 File Offset: 0x000DA9D3
		internal LocalDataStoreSlot(ThreadLocal<object> data)
		{
			this.Data = data;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000DB7E8 File Offset: 0x000DA9E8
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x000DB7F0 File Offset: 0x000DA9F0
		[Nullable(new byte[]
		{
			1,
			2
		})]
		internal ThreadLocal<object> Data { get; private set; }

		// Token: 0x0600106E RID: 4206 RVA: 0x000DB7FC File Offset: 0x000DA9FC
		~LocalDataStoreSlot()
		{
		}
	}
}
