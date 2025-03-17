using System;

namespace System.Threading
{
	// Token: 0x02000288 RID: 648
	public sealed class AutoResetEvent : EventWaitHandle
	{
		// Token: 0x06002709 RID: 9993 RVA: 0x00143DD4 File Offset: 0x00142FD4
		public AutoResetEvent(bool initialState) : base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
