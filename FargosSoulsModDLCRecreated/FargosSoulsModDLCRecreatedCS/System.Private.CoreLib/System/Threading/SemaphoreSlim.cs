using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x020002B1 RID: 689
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	public class SemaphoreSlim : IDisposable
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x001476E4 File Offset: 0x001468E4
		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x001476F0 File Offset: 0x001468F0
		public WaitHandle AvailableWaitHandle
		{
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle != null)
				{
					return this.m_waitHandle;
				}
				StrongBox<bool> lockObjAndDisposed = this.m_lockObjAndDisposed;
				lock (lockObjAndDisposed)
				{
					if (this.m_waitHandle == null)
					{
						this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
					}
				}
				return this.m_waitHandle;
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00147770 File Offset: 0x00146970
		public SemaphoreSlim(int initialCount) : this(initialCount, int.MaxValue)
		{
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00147780 File Offset: 0x00146980
		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SR.SemaphoreSlim_ctor_InitialCountWrong);
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SR.SemaphoreSlim_ctor_MaxCountWrong);
			}
			this.m_maxCount = maxCount;
			this.m_currentCount = initialCount;
			this.m_lockObjAndDisposed = new StrongBox<bool>();
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x001477E6 File Offset: 0x001469E6
		public void Wait()
		{
			this.Wait(-1, CancellationToken.None);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x001477F5 File Offset: 0x001469F5
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00147800 File Offset: 0x00146A00
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.SemaphoreSlim_Wait_TimeoutWrong);
			}
			return this.Wait((int)timeout.TotalMilliseconds, CancellationToken.None);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00147850 File Offset: 0x00146A50
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.SemaphoreSlim_Wait_TimeoutWrong);
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00147899 File Offset: 0x00146A99
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, CancellationToken.None);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x001478A8 File Offset: 0x00146AA8
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.SemaphoreSlim_Wait_TimeoutWrong);
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout == 0 && this.m_currentCount == 0)
			{
				return false;
			}
			uint startTime = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				startTime = TimeoutHelper.GetTime();
			}
			bool result = false;
			Task<bool> task = null;
			bool flag = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.UnsafeRegister(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				if (this.m_currentCount == 0)
				{
					int num = SpinWait.SpinCountforSpinBeforeWait * 4;
					SpinWait spinWait = default(SpinWait);
					while (spinWait.Count < num)
					{
						spinWait.SpinOnce(-1);
						if (this.m_currentCount != 0)
						{
							break;
						}
					}
				}
				try
				{
				}
				finally
				{
					Monitor.Enter(this.m_lockObjAndDisposed, ref flag);
					if (flag)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_asyncHead != null)
				{
					task = this.WaitAsync(millisecondsTimeout, cancellationToken);
				}
				else
				{
					OperationCanceledException ex = null;
					if (this.m_currentCount == 0)
					{
						if (millisecondsTimeout == 0)
						{
							return false;
						}
						try
						{
							result = this.WaitUntilCountOrTimeout(millisecondsTimeout, startTime, cancellationToken);
						}
						catch (OperationCanceledException ex2)
						{
							ex = ex2;
						}
					}
					if (this.m_currentCount > 0)
					{
						result = true;
						this.m_currentCount--;
					}
					else if (ex != null)
					{
						throw ex;
					}
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObjAndDisposed);
				}
				cancellationTokenRegistration.Dispose();
			}
			if (task == null)
			{
				return result;
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00147A60 File Offset: 0x00146C60
		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				bool flag = Monitor.Wait(this.m_lockObjAndDisposed, num);
				if (this.m_countOfWaitersPulsedToWake != 0)
				{
					this.m_countOfWaitersPulsedToWake--;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00147AC0 File Offset: 0x00146CC0
		public Task WaitAsync()
		{
			return this.WaitAsync(-1, default(CancellationToken));
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00147ADD File Offset: 0x00146CDD
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			return this.WaitAsync(-1, cancellationToken);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00147AE8 File Offset: 0x00146CE8
		public Task<bool> WaitAsync(int millisecondsTimeout)
		{
			return this.WaitAsync(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00147B08 File Offset: 0x00146D08
		public Task<bool> WaitAsync(TimeSpan timeout)
		{
			return this.WaitAsync(timeout, default(CancellationToken));
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00147B28 File Offset: 0x00146D28
		public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.SemaphoreSlim_Wait_TimeoutWrong);
			}
			return this.WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00147B74 File Offset: 0x00146D74
		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.SemaphoreSlim_Wait_TimeoutWrong);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<bool>(cancellationToken);
			}
			StrongBox<bool> lockObjAndDisposed = this.m_lockObjAndDisposed;
			Task<bool> result;
			lock (lockObjAndDisposed)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
					result = SemaphoreSlim.s_trueTask;
				}
				else if (millisecondsTimeout == 0)
				{
					result = SemaphoreSlim.s_falseTask;
				}
				else
				{
					SemaphoreSlim.TaskNode taskNode = this.CreateAndAddAsyncWaiter();
					result = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : this.WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken));
				}
			}
			return result;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00147C54 File Offset: 0x00146E54
		private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
		{
			SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
			if (this.m_asyncHead == null)
			{
				this.m_asyncHead = taskNode;
				this.m_asyncTail = taskNode;
			}
			else
			{
				this.m_asyncTail.Next = taskNode;
				taskNode.Prev = this.m_asyncTail;
				this.m_asyncTail = taskNode;
			}
			return taskNode;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x00147CA0 File Offset: 0x00146EA0
		private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
		{
			bool result = this.m_asyncHead == task || task.Prev != null;
			if (task.Next != null)
			{
				task.Next.Prev = task.Prev;
			}
			if (task.Prev != null)
			{
				task.Prev.Next = task.Next;
			}
			if (this.m_asyncHead == task)
			{
				this.m_asyncHead = task.Next;
			}
			if (this.m_asyncTail == task)
			{
				this.m_asyncTail = task.Prev;
			}
			task.Next = (task.Prev = null);
			return result;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x00147D30 File Offset: 0x00146F30
		private Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__33 <WaitUntilCountOrTimeoutAsync>d__;
			<WaitUntilCountOrTimeoutAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WaitUntilCountOrTimeoutAsync>d__.<>4__this = this;
			<WaitUntilCountOrTimeoutAsync>d__.asyncWaiter = asyncWaiter;
			<WaitUntilCountOrTimeoutAsync>d__.millisecondsTimeout = millisecondsTimeout;
			<WaitUntilCountOrTimeoutAsync>d__.cancellationToken = cancellationToken;
			<WaitUntilCountOrTimeoutAsync>d__.<>1__state = -1;
			<WaitUntilCountOrTimeoutAsync>d__.<>t__builder.Start<SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__33>(ref <WaitUntilCountOrTimeoutAsync>d__);
			return <WaitUntilCountOrTimeoutAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00147D8B File Offset: 0x00146F8B
		public int Release()
		{
			return this.Release(1);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x00147D94 File Offset: 0x00146F94
		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SR.SemaphoreSlim_Release_CountWrong);
			}
			StrongBox<bool> lockObjAndDisposed = this.m_lockObjAndDisposed;
			int num2;
			lock (lockObjAndDisposed)
			{
				int num = this.m_currentCount;
				num2 = num;
				if (this.m_maxCount - num < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				num += releaseCount;
				int waitCount = this.m_waitCount;
				int num3 = Math.Min(num, waitCount) - this.m_countOfWaitersPulsedToWake;
				if (num3 > 0)
				{
					if (num3 > releaseCount)
					{
						num3 = releaseCount;
					}
					this.m_countOfWaitersPulsedToWake += num3;
					for (int i = 0; i < num3; i++)
					{
						Monitor.Pulse(this.m_lockObjAndDisposed);
					}
				}
				if (this.m_asyncHead != null)
				{
					int num4 = num - waitCount;
					while (num4 > 0 && this.m_asyncHead != null)
					{
						num--;
						num4--;
						SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
						this.RemoveAsyncWaiter(asyncHead);
						asyncHead.TrySetResult(true);
					}
				}
				this.m_currentCount = num;
				if (this.m_waitHandle != null && num2 == 0 && num > 0)
				{
					this.m_waitHandle.Set();
				}
			}
			return num2;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x00147ECC File Offset: 0x001470CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00147EDC File Offset: 0x001470DC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				WaitHandle waitHandle = this.m_waitHandle;
				if (waitHandle != null)
				{
					waitHandle.Dispose();
					this.m_waitHandle = null;
				}
				this.m_lockObjAndDisposed.Value = true;
				this.m_asyncHead = null;
				this.m_asyncTail = null;
			}
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x00147F24 File Offset: 0x00147124
		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = (SemaphoreSlim)obj;
			StrongBox<bool> lockObjAndDisposed = semaphoreSlim.m_lockObjAndDisposed;
			lock (lockObjAndDisposed)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObjAndDisposed);
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x00147F70 File Offset: 0x00147170
		private void CheckDispose()
		{
			if (this.m_lockObjAndDisposed.Value)
			{
				throw new ObjectDisposedException(null, SR.SemaphoreSlim_Disposed);
			}
		}

		// Token: 0x04000AAD RID: 2733
		private volatile int m_currentCount;

		// Token: 0x04000AAE RID: 2734
		private readonly int m_maxCount;

		// Token: 0x04000AAF RID: 2735
		private int m_waitCount;

		// Token: 0x04000AB0 RID: 2736
		private int m_countOfWaitersPulsedToWake;

		// Token: 0x04000AB1 RID: 2737
		private readonly StrongBox<bool> m_lockObjAndDisposed;

		// Token: 0x04000AB2 RID: 2738
		private volatile ManualResetEvent m_waitHandle;

		// Token: 0x04000AB3 RID: 2739
		private SemaphoreSlim.TaskNode m_asyncHead;

		// Token: 0x04000AB4 RID: 2740
		private SemaphoreSlim.TaskNode m_asyncTail;

		// Token: 0x04000AB5 RID: 2741
		private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04000AB6 RID: 2742
		private static readonly Task<bool> s_falseTask = new Task<bool>(false, false, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04000AB7 RID: 2743
		private static readonly Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);

		// Token: 0x020002B2 RID: 690
		private sealed class TaskNode : Task<bool>
		{
			// Token: 0x0600284A RID: 10314 RVA: 0x00147FDE File Offset: 0x001471DE
			internal TaskNode() : base(null, TaskCreationOptions.RunContinuationsAsynchronously)
			{
			}

			// Token: 0x04000AB8 RID: 2744
			internal SemaphoreSlim.TaskNode Prev;

			// Token: 0x04000AB9 RID: 2745
			internal SemaphoreSlim.TaskNode Next;
		}
	}
}
