using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000566 RID: 1382
	public readonly struct YieldAwaitable
	{
		// Token: 0x060047A8 RID: 18344 RVA: 0x0017DF34 File Offset: 0x0017D134
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		// Token: 0x02000567 RID: 1383
		public readonly struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IStateMachineBoxAwareAwaiter
		{
			// Token: 0x17000ACD RID: 2765
			// (get) Token: 0x060047A9 RID: 18345 RVA: 0x000AC09B File Offset: 0x000AB29B
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060047AA RID: 18346 RVA: 0x0017DF4A File Offset: 0x0017D14A
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			// Token: 0x060047AB RID: 18347 RVA: 0x0017DF53 File Offset: 0x0017D153
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x060047AC RID: 18348 RVA: 0x0017DF5C File Offset: 0x0017D15C
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				if (TplEventSource.Log.IsEnabled())
				{
					continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
				}
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					synchronizationContext.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x060047AD RID: 18349 RVA: 0x0017DFFC File Offset: 0x0017D1FC
			void IStateMachineBoxAwareAwaiter.AwaitUnsafeOnCompleted(IAsyncStateMachineBox box)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					YieldAwaitable.YieldAwaiter.QueueContinuation(box.MoveNextAction, false);
					return;
				}
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					synchronizationContext.Post(delegate(object s)
					{
						((IAsyncStateMachineBox)s).MoveNext();
					}, box);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler == TaskScheduler.Default)
				{
					ThreadPool.UnsafeQueueUserWorkItemInternal(box, false);
					return;
				}
				Task.Factory.StartNew(delegate(object s)
				{
					((IAsyncStateMachineBox)s).MoveNext();
				}, box, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
			}

			// Token: 0x060047AE RID: 18350 RVA: 0x0017E0B8 File Offset: 0x0017D2B8
			private static Action OutputCorrelationEtwEvent(Action continuation)
			{
				int num = Task.NewId();
				Task internalCurrent = Task.InternalCurrent;
				TplEventSource.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, num);
				return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate(Action innerContinuation, Task continuationIdTask)
				{
					TplEventSource log = TplEventSource.Log;
					log.TaskWaitContinuationStarted(((Task<int>)continuationIdTask).Result);
					Guid currentThreadActivityId = default(Guid);
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(TplEventSource.CreateGuidForTaskID(((Task<int>)continuationIdTask).Result), out currentThreadActivityId);
					}
					innerContinuation();
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
					log.TaskWaitContinuationComplete(((Task<int>)continuationIdTask).Result);
				}, Task.FromResult<int>(num));
			}

			// Token: 0x060047AF RID: 18351 RVA: 0x0017E11D File Offset: 0x0017D31D
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			// Token: 0x060047B0 RID: 18352 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void GetResult()
			{
			}

			// Token: 0x0400114A RID: 4426
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x0400114B RID: 4427
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
