using System;

namespace System.StubHelpers
{
	// Token: 0x020003AF RID: 943
	internal abstract class CleanupWorkListElement
	{
		// Token: 0x060030DC RID: 12508
		protected abstract void DestroyCore();

		// Token: 0x060030DD RID: 12509 RVA: 0x00168528 File Offset: 0x00167728
		public void Destroy()
		{
			this.DestroyCore();
			for (CleanupWorkListElement next = this.m_Next; next != null; next = next.m_Next)
			{
				next.DestroyCore();
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x00168554 File Offset: 0x00167754
		public static void AddToCleanupList(ref CleanupWorkListElement list, CleanupWorkListElement newElement)
		{
			if (list == null)
			{
				list = newElement;
				return;
			}
			newElement.m_Next = list;
			list = newElement;
		}

		// Token: 0x04000D76 RID: 3446
		private CleanupWorkListElement m_Next;
	}
}
