using System;

namespace System.Threading
{
	// Token: 0x02000270 RID: 624
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06002648 RID: 9800 RVA: 0x00141D10 File Offset: 0x00140F10
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
