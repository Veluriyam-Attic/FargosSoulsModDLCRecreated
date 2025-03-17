using System;

namespace System.Threading
{
	// Token: 0x020002A2 RID: 674
	public sealed class ManualResetEvent : EventWaitHandle
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x001459A6 File Offset: 0x00144BA6
		public ManualResetEvent(bool initialState) : base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
