using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000354 RID: 852
	internal static class ManualResetValueTaskSourceCoreShared
	{
		// Token: 0x06002C9F RID: 11423 RVA: 0x00155399 File Offset: 0x00154599
		private static void CompletionSentinel(object _)
		{
			ThrowHelper.ThrowInvalidOperationException();
		}

		// Token: 0x04000C79 RID: 3193
		internal static readonly Action<object> s_sentinel = new Action<object>(ManualResetValueTaskSourceCoreShared.CompletionSentinel);
	}
}
