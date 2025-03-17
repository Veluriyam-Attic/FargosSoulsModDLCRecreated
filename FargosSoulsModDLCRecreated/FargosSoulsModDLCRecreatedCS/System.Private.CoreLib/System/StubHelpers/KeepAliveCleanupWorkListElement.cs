using System;

namespace System.StubHelpers
{
	// Token: 0x020003B0 RID: 944
	internal sealed class KeepAliveCleanupWorkListElement : CleanupWorkListElement
	{
		// Token: 0x060030E0 RID: 12512 RVA: 0x00168569 File Offset: 0x00167769
		public KeepAliveCleanupWorkListElement(object obj)
		{
			this.m_obj = obj;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00168578 File Offset: 0x00167778
		protected override void DestroyCore()
		{
			GC.KeepAlive(this.m_obj);
		}

		// Token: 0x04000D77 RID: 3447
		private object m_obj;
	}
}
