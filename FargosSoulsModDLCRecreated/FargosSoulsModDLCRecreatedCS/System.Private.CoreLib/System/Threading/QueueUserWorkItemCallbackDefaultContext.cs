using System;

namespace System.Threading
{
	// Token: 0x020002CF RID: 719
	internal sealed class QueueUserWorkItemCallbackDefaultContext : QueueUserWorkItemCallbackBase
	{
		// Token: 0x060028D1 RID: 10449 RVA: 0x00149FD7 File Offset: 0x001491D7
		internal QueueUserWorkItemCallbackDefaultContext(WaitCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00149FF0 File Offset: 0x001491F0
		public override void Execute()
		{
			base.Execute();
			WaitCallback callback = this._callback;
			this._callback = null;
			callback(this._state);
		}

		// Token: 0x04000AFA RID: 2810
		private WaitCallback _callback;

		// Token: 0x04000AFB RID: 2811
		private readonly object _state;
	}
}
