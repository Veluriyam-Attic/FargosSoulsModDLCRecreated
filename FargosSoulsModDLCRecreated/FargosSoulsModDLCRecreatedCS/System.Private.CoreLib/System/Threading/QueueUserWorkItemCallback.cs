using System;

namespace System.Threading
{
	// Token: 0x020002CC RID: 716
	internal sealed class QueueUserWorkItemCallback : QueueUserWorkItemCallbackBase
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x00149F04 File Offset: 0x00149104
		internal QueueUserWorkItemCallback(WaitCallback callback, object state, ExecutionContext context)
		{
			this._callback = callback;
			this._state = state;
			this._context = context;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x00149F21 File Offset: 0x00149121
		public override void Execute()
		{
			base.Execute();
			ExecutionContext.RunForThreadPoolUnsafe<QueueUserWorkItemCallback>(this._context, QueueUserWorkItemCallback.s_executionContextShim, this);
		}

		// Token: 0x04000AF2 RID: 2802
		private WaitCallback _callback;

		// Token: 0x04000AF3 RID: 2803
		private readonly object _state;

		// Token: 0x04000AF4 RID: 2804
		private readonly ExecutionContext _context;

		// Token: 0x04000AF5 RID: 2805
		private static readonly Action<QueueUserWorkItemCallback> s_executionContextShim = delegate(QueueUserWorkItemCallback quwi)
		{
			WaitCallback callback = quwi._callback;
			quwi._callback = null;
			callback(quwi._state);
		};
	}
}
