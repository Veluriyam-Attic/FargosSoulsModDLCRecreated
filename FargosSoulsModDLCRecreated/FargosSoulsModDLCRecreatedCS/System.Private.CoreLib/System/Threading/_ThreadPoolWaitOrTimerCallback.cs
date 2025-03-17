using System;

namespace System.Threading
{
	// Token: 0x020002D1 RID: 721
	internal sealed class _ThreadPoolWaitOrTimerCallback
	{
		// Token: 0x060028D5 RID: 10453 RVA: 0x0014A061 File Offset: 0x00149261
		internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool flowExecutionContext)
		{
			this._waitOrTimerCallback = waitOrTimerCallback;
			this._state = state;
			if (flowExecutionContext)
			{
				this._executionContext = ExecutionContext.Capture();
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0014A085 File Offset: 0x00149285
		private static void WaitOrTimerCallback_Context_t(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0014A08E File Offset: 0x0014928E
		private static void WaitOrTimerCallback_Context_f(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0014A098 File Offset: 0x00149298
		private static void WaitOrTimerCallback_Context(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0014A0C0 File Offset: 0x001492C0
		internal static void PerformWaitOrTimerCallback(_ThreadPoolWaitOrTimerCallback helper, bool timedOut)
		{
			ExecutionContext executionContext = helper._executionContext;
			if (executionContext == null)
			{
				WaitOrTimerCallback waitOrTimerCallback = helper._waitOrTimerCallback;
				waitOrTimerCallback(helper._state, timedOut);
				return;
			}
			ExecutionContext.Run(executionContext, timedOut ? _ThreadPoolWaitOrTimerCallback._ccbt : _ThreadPoolWaitOrTimerCallback._ccbf, helper);
		}

		// Token: 0x04000AFE RID: 2814
		private readonly WaitOrTimerCallback _waitOrTimerCallback;

		// Token: 0x04000AFF RID: 2815
		private readonly ExecutionContext _executionContext;

		// Token: 0x04000B00 RID: 2816
		private readonly object _state;

		// Token: 0x04000B01 RID: 2817
		private static readonly ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);

		// Token: 0x04000B02 RID: 2818
		private static readonly ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
	}
}
