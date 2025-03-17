using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002EB RID: 747
	[NullableContext(1)]
	[Nullable(0)]
	public static class WaitHandleExtensions
	{
		// Token: 0x0600292E RID: 10542 RVA: 0x0014AC1A File Offset: 0x00149E1A
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0014AC30 File Offset: 0x00149E30
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, [Nullable(2)] SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
