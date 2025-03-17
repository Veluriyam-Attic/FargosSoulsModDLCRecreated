using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x020002DA RID: 730
	internal sealed class TimerQueueTimer : IThreadPoolWorkItem
	{
		// Token: 0x060028ED RID: 10477 RVA: 0x0014A1DC File Offset: 0x001493DC
		internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, bool flowExecutionContext)
		{
			this._timerCallback = timerCallback;
			this._state = state;
			this._dueTime = uint.MaxValue;
			this._period = uint.MaxValue;
			if (flowExecutionContext)
			{
				this._executionContext = ExecutionContext.Capture();
			}
			this._associatedTimerQueue = TimerQueue.Instances[Thread.GetCurrentProcessorId() % TimerQueue.Instances.Length];
			if (dueTime != 4294967295U)
			{
				this.Change(dueTime, period);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0014A244 File Offset: 0x00149444
		internal bool Change(uint dueTime, uint period)
		{
			TimerQueue associatedTimerQueue = this._associatedTimerQueue;
			bool result;
			lock (associatedTimerQueue)
			{
				if (this._canceled)
				{
					throw new ObjectDisposedException(null, SR.ObjectDisposed_Generic);
				}
				this._period = period;
				if (dueTime == 4294967295U)
				{
					this._associatedTimerQueue.DeleteTimer(this);
					result = true;
				}
				else
				{
					if (FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
					{
						FrameworkEventSource.Log.ThreadTransferSendObj(this, 1, string.Empty, true, (int)dueTime, (int)period);
					}
					result = this._associatedTimerQueue.UpdateTimer(this, dueTime, period);
				}
			}
			return result;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0014A2E4 File Offset: 0x001494E4
		public void Close()
		{
			TimerQueue associatedTimerQueue = this._associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				if (!this._canceled)
				{
					this._canceled = true;
					this._associatedTimerQueue.DeleteTimer(this);
				}
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0014A340 File Offset: 0x00149540
		public bool Close(WaitHandle toSignal)
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this._associatedTimerQueue;
			bool result;
			lock (associatedTimerQueue)
			{
				if (this._canceled)
				{
					result = false;
				}
				else
				{
					this._canceled = true;
					this._notifyWhenNoCallbacksRunning = toSignal;
					this._associatedTimerQueue.DeleteTimer(this);
					flag = (this._callbacksRunning == 0);
					result = true;
				}
			}
			if (flag)
			{
				this.SignalNoCallbacksRunning();
			}
			return result;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0014A3C0 File Offset: 0x001495C0
		public ValueTask CloseAsync()
		{
			TimerQueue associatedTimerQueue = this._associatedTimerQueue;
			ValueTask result;
			lock (associatedTimerQueue)
			{
				object notifyWhenNoCallbacksRunning = this._notifyWhenNoCallbacksRunning;
				if (this._canceled)
				{
					if (notifyWhenNoCallbacksRunning is WaitHandle)
					{
						InvalidOperationException ex = new InvalidOperationException(SR.InvalidOperation_TimerAlreadyClosed);
						ex.SetCurrentStackTrace();
						return ValueTask.FromException(ex);
					}
				}
				else
				{
					this._canceled = true;
					this._associatedTimerQueue.DeleteTimer(this);
				}
				if (this._callbacksRunning == 0)
				{
					result = default(ValueTask);
				}
				else if (notifyWhenNoCallbacksRunning == null)
				{
					Task<bool> task = new Task<bool>(null, TaskCreationOptions.RunContinuationsAsynchronously);
					this._notifyWhenNoCallbacksRunning = task;
					result = new ValueTask(task);
				}
				else
				{
					result = new ValueTask((Task<bool>)notifyWhenNoCallbacksRunning);
				}
			}
			return result;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0014A48C File Offset: 0x0014968C
		void IThreadPoolWorkItem.Execute()
		{
			this.Fire(true);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0014A498 File Offset: 0x00149698
		internal void Fire(bool isThreadPool = false)
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this._associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				flag = this._canceled;
				if (!flag)
				{
					this._callbacksRunning++;
				}
			}
			if (flag)
			{
				return;
			}
			this.CallCallback(isThreadPool);
			bool flag3 = false;
			TimerQueue associatedTimerQueue2 = this._associatedTimerQueue;
			lock (associatedTimerQueue2)
			{
				this._callbacksRunning--;
				if (this._canceled && this._callbacksRunning == 0 && this._notifyWhenNoCallbacksRunning != null)
				{
					flag3 = true;
				}
			}
			if (flag3)
			{
				this.SignalNoCallbacksRunning();
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0014A560 File Offset: 0x00149760
		internal void SignalNoCallbacksRunning()
		{
			object notifyWhenNoCallbacksRunning = this._notifyWhenNoCallbacksRunning;
			WaitHandle waitHandle = notifyWhenNoCallbacksRunning as WaitHandle;
			if (waitHandle != null)
			{
				EventWaitHandle.Set(waitHandle.SafeWaitHandle);
				return;
			}
			((Task<bool>)notifyWhenNoCallbacksRunning).TrySetResult(true);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0014A59C File Offset: 0x0014979C
		internal void CallCallback(bool isThreadPool)
		{
			if (FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceiveObj(this, 1, string.Empty);
			}
			ExecutionContext executionContext = this._executionContext;
			if (executionContext == null)
			{
				this._timerCallback(this._state);
				return;
			}
			if (isThreadPool)
			{
				ExecutionContext.RunFromThreadPoolDispatchLoop(Thread.CurrentThread, executionContext, TimerQueueTimer.s_callCallbackInContext, this);
				return;
			}
			ExecutionContext.RunInternal(executionContext, TimerQueueTimer.s_callCallbackInContext, this);
		}

		// Token: 0x04000B16 RID: 2838
		private readonly TimerQueue _associatedTimerQueue;

		// Token: 0x04000B17 RID: 2839
		internal TimerQueueTimer _next;

		// Token: 0x04000B18 RID: 2840
		internal TimerQueueTimer _prev;

		// Token: 0x04000B19 RID: 2841
		internal bool _short;

		// Token: 0x04000B1A RID: 2842
		internal long _startTicks;

		// Token: 0x04000B1B RID: 2843
		internal uint _dueTime;

		// Token: 0x04000B1C RID: 2844
		internal uint _period;

		// Token: 0x04000B1D RID: 2845
		private readonly TimerCallback _timerCallback;

		// Token: 0x04000B1E RID: 2846
		private readonly object _state;

		// Token: 0x04000B1F RID: 2847
		private readonly ExecutionContext _executionContext;

		// Token: 0x04000B20 RID: 2848
		private int _callbacksRunning;

		// Token: 0x04000B21 RID: 2849
		private volatile bool _canceled;

		// Token: 0x04000B22 RID: 2850
		private volatile object _notifyWhenNoCallbacksRunning;

		// Token: 0x04000B23 RID: 2851
		private static readonly ContextCallback s_callCallbackInContext = delegate(object state)
		{
			TimerQueueTimer timerQueueTimer = (TimerQueueTimer)state;
			timerQueueTimer._timerCallback(timerQueueTimer._state);
		};
	}
}
