using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002FB RID: 763
	[Nullable(0)]
	[NullableContext(1)]
	public class TaskFactory<[Nullable(2)] TResult>
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x0014BE2D File Offset: 0x0014B02D
		private TaskScheduler DefaultScheduler
		{
			get
			{
				return this.m_defaultScheduler ?? TaskScheduler.Current;
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0014BE3E File Offset: 0x0014B03E
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0014BE6C File Offset: 0x0014B06C
		public TaskFactory() : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0014BE8B File Offset: 0x0014B08B
		public TaskFactory(CancellationToken cancellationToken) : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x0014BE98 File Offset: 0x0014B098
		[NullableContext(2)]
		public TaskFactory(TaskScheduler scheduler) : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x0014BEB8 File Offset: 0x0014B0B8
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions) : this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x0014BED7 File Offset: 0x0014B0D7
		[NullableContext(2)]
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x0014BF08 File Offset: 0x0014B108
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x0014BF10 File Offset: 0x0014B110
		[Nullable(2)]
		public TaskScheduler Scheduler
		{
			[NullableContext(2)]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x0014BF18 File Offset: 0x0014B118
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x0014BF20 File Offset: 0x0014B120
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x0014BF28 File Offset: 0x0014B128
		public Task<TResult> StartNew(Func<TResult> function)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x0014BF58 File Offset: 0x0014B158
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0014BF84 File Offset: 0x0014B184
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x0014BFAD File Offset: 0x0014B1AD
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0014BFC0 File Offset: 0x0014B1C0
		public Task<TResult> StartNew([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, [Nullable(2)] object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0014BFF0 File Offset: 0x0014B1F0
		public Task<TResult> StartNew([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, [Nullable(2)] object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0014C01C File Offset: 0x0014B21C
		public Task<TResult> StartNew([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0014C046 File Offset: 0x0014B246
		public Task<TResult> StartNew([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, [Nullable(2)] object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0014C05C File Offset: 0x0014B25C
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult result = default(TResult);
			try
			{
				if (endFunction != null)
				{
					result = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex3)
			{
				ex2 = ex3;
			}
			catch (Exception ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					promise.TrySetException(ex);
				}
				else
				{
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(promise.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(promise);
					}
					if (requiresSynchronization)
					{
						promise.TrySetResult(result);
					}
					else
					{
						promise.DangerousSetResult(result);
					}
				}
			}
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0014C120 File Offset: 0x0014B320
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0014C136 File Offset: 0x0014B336
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0014C147 File Offset: 0x0014B347
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0014C154 File Offset: 0x0014B354
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			if (asyncResult == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.asyncResult);
			}
			if (endFunction == null && endAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.endMethod);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(promise.Id, "TaskFactory.FromAsync", 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			Task t = new Task(new Action<object>(delegate(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}), null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(t.Id, "TaskFactory.FromAsync Callback", 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(t);
			}
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_158;
				}
				catch (Exception exceptionObject)
				{
					promise.TrySetException(exceptionObject);
					goto IL_158;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate(object <p0>, bool <p1>)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception exceptionObject2)
				{
					promise.TrySetException(exceptionObject2);
				}
			}, null, -1, true);
			IL_158:
			return promise;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0014C2D0 File Offset: 0x0014B4D0
		public Task<TResult> FromAsync([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0014C2E1 File Offset: 0x0014B4E1
		public Task<TResult> FromAsync([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0014C2F0 File Offset: 0x0014B4F0
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.beginMethod);
			}
			if (endFunction == null && endAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.endMethod);
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise);
				}
				promise.TrySetResult();
				throw;
			}
			return promise;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0014C424 File Offset: 0x0014B624
		public Task<TResult> FromAsync<[Nullable(2)] TArg1>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0014C437 File Offset: 0x0014B637
		public Task<TResult> FromAsync<[Nullable(2)] TArg1>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x0014C448 File Offset: 0x0014B648
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.beginMethod);
			}
			if (endFunction == null && endAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.endFunction);
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise);
				}
				promise.TrySetResult();
				throw;
			}
			return promise;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x0014C580 File Offset: 0x0014B780
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x0014C595 File Offset: 0x0014B795
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x0014C5A8 File Offset: 0x0014B7A8
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.beginMethod);
			}
			if (endFunction == null && endAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.endMethod);
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise);
				}
				promise.TrySetResult();
				throw;
			}
			return promise;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x0014C6E4 File Offset: 0x0014B8E4
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0014C6FB File Offset: 0x0014B8FB
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x0014C710 File Offset: 0x0014B910
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.beginMethod);
			}
			if (endFunction == null && endAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.endMethod);
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise);
				}
				promise.TrySetResult();
				throw;
			}
			return promise;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x0014C84C File Offset: 0x0014BA4C
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0014C884 File Offset: 0x0014BA84
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), creationOptions, ct);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0014C8AC File Offset: 0x0014BAAC
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x0014C8D2 File Offset: 0x0014BAD2
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0014C8F3 File Offset: 0x0014BAF3
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0014C914 File Offset: 0x0014BB14
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0014C92D File Offset: 0x0014BB2D
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0014C953 File Offset: 0x0014BB53
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0014C974 File Offset: 0x0014BB74
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0014C995 File Offset: 0x0014BB95
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0014C9B0 File Offset: 0x0014BBB0
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x0014CA24 File Offset: 0x0014BC24
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task[]> task = TaskFactory.CommonCWAllLogic(tasksCopy);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
				{
					completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
					return ((Func<Task[], TResult>)state)(completedTasks.Result);
				}, continuationFunction, scheduler, cancellationToken, continuationOptions);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x0014CACC File Offset: 0x0014BCCC
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0014CAF2 File Offset: 0x0014BCF2
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0014CB13 File Offset: 0x0014BD13
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0014CB34 File Offset: 0x0014BD34
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0014CB4D File Offset: 0x0014BD4D
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0014CB73 File Offset: 0x0014BD73
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0014CB94 File Offset: 0x0014BD94
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0014CBB5 File Offset: 0x0014BDB5
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0014CBD0 File Offset: 0x0014BDD0
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (tasks.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList, ExceptionArgument.tasks);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks, false);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0014CC80 File Offset: 0x0014BE80
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (tasks.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList, ExceptionArgument.tasks);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks, false);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x04000B65 RID: 2917
		private readonly CancellationToken m_defaultCancellationToken;

		// Token: 0x04000B66 RID: 2918
		private readonly TaskScheduler m_defaultScheduler;

		// Token: 0x04000B67 RID: 2919
		private readonly TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04000B68 RID: 2920
		private readonly TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x020002FC RID: 764
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x060029E0 RID: 10720 RVA: 0x0014CCFB File Offset: 0x0014BEFB
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x060029E1 RID: 10721 RVA: 0x0014CD14 File Offset: 0x0014BF14
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.asyncResult);
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.InvalidOperation_WrongAsyncResultOrEndCalledMultiple, ExceptionArgument.asyncResult);
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.InvalidOperation_WrongAsyncResultOrEndCalledMultiple, ExceptionArgument.asyncResult);
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x060029E2 RID: 10722 RVA: 0x0014CD84 File Offset: 0x0014BF84
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult result = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						bool flag = base.TrySetResult(result);
					}
					else
					{
						base.DangerousSetResult(result);
					}
				}
				catch (OperationCanceledException ex)
				{
					bool flag = base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception exceptionObject)
				{
					bool flag = base.TrySetException(exceptionObject);
				}
			}

			// Token: 0x04000B69 RID: 2921
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04000B6A RID: 2922
			private TInstance m_thisRef;

			// Token: 0x04000B6B RID: 2923
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}
	}
}
