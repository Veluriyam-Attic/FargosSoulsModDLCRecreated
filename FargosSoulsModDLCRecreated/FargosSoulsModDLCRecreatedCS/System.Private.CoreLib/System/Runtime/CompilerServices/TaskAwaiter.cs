using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000555 RID: 1365
	public readonly struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, ITaskAwaiter
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x0017D7F2 File Offset: 0x0017C9F2
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0017D7FB File Offset: 0x0017C9FB
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x0017D808 File Offset: 0x0017CA08
		[NullableContext(1)]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x0017D818 File Offset: 0x0017CA18
		[NullableContext(1)]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x0017D828 File Offset: 0x0017CA28
		[StackTraceHidden]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x0017D835 File Offset: 0x0017CA35
		[StackTraceHidden]
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x0017D848 File Offset: 0x0017CA48
		[StackTraceHidden]
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				bool flag = task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsCompletedSuccessfully)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x0017D884 File Offset: 0x0017CA84
		[StackTraceHidden]
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0017D8DB File Offset: 0x0017CADB
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			if (TplEventSource.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x0017D910 File Offset: 0x0017CB10
		internal static void UnsafeOnCompletedInternal(Task task, IAsyncStateMachineBox stateMachineBox, bool continueOnCapturedContext)
		{
			if (TplEventSource.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
			{
				task.SetContinuationForAwait(TaskAwaiter.OutputWaitEtwEvents(task, stateMachineBox.MoveNextAction), continueOnCapturedContext, false);
				return;
			}
			task.UnsafeSetContinuationForAwait(stateMachineBox, continueOnCapturedContext);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x0017D944 File Offset: 0x0017CB44
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(task);
			}
			TplEventSource log = TplEventSource.Log;
			if (log.IsEnabled())
			{
				Task internalCurrent = Task.InternalCurrent;
				Task task2 = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				log.TaskWaitBegin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id, TplEventSource.TaskWaitBehavior.Asynchronous, (task2 != null) ? task2.Id : 0);
			}
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate(Action innerContinuation, Task innerTask)
			{
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(innerTask);
				}
				TplEventSource log2 = TplEventSource.Log;
				Guid currentThreadActivityId = default(Guid);
				bool flag = log2.IsEnabled();
				if (flag)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					log2.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, innerTask.Id);
					if (log2.TasksSetActivityIds && (innerTask.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(TplEventSource.CreateGuidForTaskID(innerTask.Id), out currentThreadActivityId);
					}
				}
				innerContinuation();
				if (flag)
				{
					log2.TaskWaitContinuationComplete(innerTask.Id);
					if (log2.TasksSetActivityIds && (innerTask.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
				}
			}, task);
		}

		// Token: 0x04001139 RID: 4409
		internal readonly Task m_task;
	}
}
