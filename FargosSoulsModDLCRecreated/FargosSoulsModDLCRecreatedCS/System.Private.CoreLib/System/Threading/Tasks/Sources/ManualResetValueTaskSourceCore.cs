using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000352 RID: 850
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Auto)]
	public struct ManualResetValueTaskSourceCore<[Nullable(2)] TResult>
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x00154F61 File Offset: 0x00154161
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x00154F69 File Offset: 0x00154169
		public bool RunContinuationsAsynchronously { readonly get; set; }

		// Token: 0x06002C90 RID: 11408 RVA: 0x00154F74 File Offset: 0x00154174
		public void Reset()
		{
			this._version += 1;
			this._completed = false;
			this._result = default(TResult);
			this._error = null;
			this._executionContext = null;
			this._capturedContext = null;
			this._continuation = null;
			this._continuationState = null;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00154FC6 File Offset: 0x001541C6
		public void SetResult(TResult result)
		{
			this._result = result;
			this.SignalCompletion();
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x00154FD5 File Offset: 0x001541D5
		public void SetException(Exception error)
		{
			this._error = ExceptionDispatchInfo.Capture(error);
			this.SignalCompletion();
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x00154FE9 File Offset: 0x001541E9
		public short Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x00154FF1 File Offset: 0x001541F1
		public ValueTaskSourceStatus GetStatus(short token)
		{
			this.ValidateToken(token);
			if (this._continuation == null || !this._completed)
			{
				return ValueTaskSourceStatus.Pending;
			}
			if (this._error == null)
			{
				return ValueTaskSourceStatus.Succeeded;
			}
			if (!(this._error.SourceException is OperationCanceledException))
			{
				return ValueTaskSourceStatus.Faulted;
			}
			return ValueTaskSourceStatus.Canceled;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0015502B File Offset: 0x0015422B
		[StackTraceHidden]
		public TResult GetResult(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				ThrowHelper.ThrowInvalidOperationException();
			}
			ExceptionDispatchInfo error = this._error;
			if (error != null)
			{
				error.Throw();
			}
			return this._result;
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x00155058 File Offset: 0x00154258
		[NullableContext(2)]
		public void OnCompleted([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			this.ValidateToken(token);
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				this._executionContext = ExecutionContext.Capture();
			}
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					this._capturedContext = synchronizationContext;
				}
				else
				{
					TaskScheduler taskScheduler = TaskScheduler.Current;
					if (taskScheduler != TaskScheduler.Default)
					{
						this._capturedContext = taskScheduler;
					}
				}
			}
			object obj = this._continuation;
			if (obj == null)
			{
				this._continuationState = state;
				obj = Interlocked.CompareExchange<Action<object>>(ref this._continuation, continuation, null);
			}
			if (obj != null)
			{
				if (obj != ManualResetValueTaskSourceCoreShared.s_sentinel)
				{
					ThrowHelper.ThrowInvalidOperationException();
				}
				object capturedContext = this._capturedContext;
				if (capturedContext != null)
				{
					SynchronizationContext synchronizationContext2 = capturedContext as SynchronizationContext;
					if (synchronizationContext2 != null)
					{
						synchronizationContext2.Post(delegate(object s)
						{
							Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
							tuple.Item1(tuple.Item2);
						}, Tuple.Create<Action<object>, object>(continuation, state));
						return;
					}
					TaskScheduler taskScheduler2 = capturedContext as TaskScheduler;
					if (taskScheduler2 == null)
					{
						return;
					}
					Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler2);
				}
				else
				{
					if (this._executionContext != null)
					{
						ThreadPool.QueueUserWorkItem<object>(continuation, state, true);
						return;
					}
					ThreadPool.UnsafeQueueUserWorkItem<object>(continuation, state, true);
					return;
				}
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x00155184 File Offset: 0x00154384
		private void ValidateToken(short token)
		{
			if (token != this._version)
			{
				ThrowHelper.ThrowInvalidOperationException();
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00155194 File Offset: 0x00154394
		private void SignalCompletion()
		{
			if (this._completed)
			{
				ThrowHelper.ThrowInvalidOperationException();
			}
			this._completed = true;
			if (this._continuation == null && Interlocked.CompareExchange<Action<object>>(ref this._continuation, ManualResetValueTaskSourceCoreShared.s_sentinel, null) == null)
			{
				return;
			}
			if (this._executionContext != null)
			{
				this.InvokeContinuationWithContext();
				return;
			}
			if (this._capturedContext != null)
			{
				this.InvokeSchedulerContinuation();
				return;
			}
			if (this.RunContinuationsAsynchronously)
			{
				ThreadPool.UnsafeQueueUserWorkItem<object>(this._continuation, this._continuationState, true);
				return;
			}
			this._continuation(this._continuationState);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0015521C File Offset: 0x0015441C
		private void InvokeContinuationWithContext()
		{
			ExecutionContext executionContext = ExecutionContext.CaptureForRestore();
			ExecutionContext.Restore(this._executionContext);
			if (this._capturedContext == null)
			{
				if (this.RunContinuationsAsynchronously)
				{
					try
					{
						ThreadPool.QueueUserWorkItem<object>(this._continuation, this._continuationState, true);
						return;
					}
					finally
					{
						ExecutionContext.RestoreInternal(executionContext);
					}
				}
				ExceptionDispatchInfo exceptionDispatchInfo = null;
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				try
				{
					this._continuation(this._continuationState);
				}
				catch (Exception source)
				{
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
				}
				finally
				{
					SynchronizationContext.SetSynchronizationContext(synchronizationContext);
					ExecutionContext.RestoreInternal(executionContext);
				}
				if (exceptionDispatchInfo != null)
				{
					exceptionDispatchInfo.Throw();
				}
				return;
			}
			try
			{
				this.InvokeSchedulerContinuation();
			}
			finally
			{
				ExecutionContext.RestoreInternal(executionContext);
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x001552E8 File Offset: 0x001544E8
		private void InvokeSchedulerContinuation()
		{
			object capturedContext = this._capturedContext;
			SynchronizationContext synchronizationContext = capturedContext as SynchronizationContext;
			if (synchronizationContext != null)
			{
				synchronizationContext.Post(delegate(object s)
				{
					Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
					tuple.Item1(tuple.Item2);
				}, Tuple.Create<Action<object>, object>(this._continuation, this._continuationState));
				return;
			}
			TaskScheduler taskScheduler = capturedContext as TaskScheduler;
			if (taskScheduler == null)
			{
				return;
			}
			Task.Factory.StartNew(this._continuation, this._continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
		}

		// Token: 0x04000C6D RID: 3181
		private Action<object> _continuation;

		// Token: 0x04000C6E RID: 3182
		private object _continuationState;

		// Token: 0x04000C6F RID: 3183
		private ExecutionContext _executionContext;

		// Token: 0x04000C70 RID: 3184
		private object _capturedContext;

		// Token: 0x04000C71 RID: 3185
		private bool _completed;

		// Token: 0x04000C72 RID: 3186
		private TResult _result;

		// Token: 0x04000C73 RID: 3187
		private ExceptionDispatchInfo _error;

		// Token: 0x04000C74 RID: 3188
		private short _version;
	}
}
