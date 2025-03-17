using System;

namespace System.Reflection
{
	// Token: 0x02000598 RID: 1432
	internal sealed class LoaderAllocator
	{
		// Token: 0x060049A5 RID: 18853 RVA: 0x00185F7C File Offset: 0x0018517C
		private LoaderAllocator()
		{
			this.m_slots = new object[5];
			this.m_scout = new LoaderAllocatorScout();
		}

		// Token: 0x04001214 RID: 4628
		private LoaderAllocatorScout m_scout;

		// Token: 0x04001215 RID: 4629
		private object[] m_slots;

		// Token: 0x04001216 RID: 4630
		internal CerHashtable<RuntimeMethodInfo, RuntimeMethodInfo> m_methodInstantiations;

		// Token: 0x04001217 RID: 4631
		private int m_slotsUsed;
	}
}
