using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000331 RID: 817
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x06002B6D RID: 11117 RVA: 0x00152028 File Offset: 0x00151228
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture();
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x00152048 File Offset: 0x00151248
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
			{
				CapturedContext = this.m_capturedContext
			};
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0015207C File Offset: 0x0015127C
		internal override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEventSource log = TplEventSource.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			ThreadPool.UnsafeQueueUserWorkItemInternal(this, true);
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x001520F0 File Offset: 0x001512F0
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x00152134 File Offset: 0x00151334
		void IThreadPoolWorkItem.Execute()
		{
			TplEventSource log = TplEventSource.Log;
			ExecutionContext capturedContext = this.m_capturedContext;
			if (!log.IsEnabled() && capturedContext == null)
			{
				this.m_action();
				return;
			}
			Guid currentThreadActivityId = default(Guid);
			if (log.TasksSetActivityIds && this.m_continuationId != 0)
			{
				Guid activityId = TplEventSource.CreateGuidForTaskID(this.m_continuationId);
				EventSource.SetCurrentThreadActivityId(activityId, out currentThreadActivityId);
			}
			try
			{
				if (capturedContext == null || capturedContext.IsDefault)
				{
					this.m_action();
				}
				else
				{
					ExecutionContext.RunForThreadPoolUnsafe<Action>(capturedContext, AwaitTaskContinuation.s_invokeAction, this.m_action);
				}
			}
			finally
			{
				if (log.TasksSetActivityIds && this.m_continuationId != 0)
				{
					EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
				}
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x001521E4 File Offset: 0x001513E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			return AwaitTaskContinuation.s_invokeContextCallback;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x001521EC File Offset: 0x001513EC
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				ExecutionContext capturedContext = this.m_capturedContext;
				if (capturedContext == null)
				{
					callback(state);
				}
				else
				{
					ExecutionContext.RunInternal(capturedContext, callback, state);
				}
			}
			catch (Exception exception)
			{
				Task.ThrowAsync(exception, null);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0015224C File Offset: 0x0015144C
		internal static void RunOrScheduleAction(Action action, bool allowInlining)
		{
			ref Task ptr = ref Task.t_currentTask;
			Task task = ptr;
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action, task);
				return;
			}
			try
			{
				if (task != null)
				{
					ptr = null;
				}
				action();
			}
			catch (Exception exception)
			{
				Task.ThrowAsync(exception, null);
			}
			finally
			{
				if (task != null)
				{
					ptr = task;
				}
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x001522B0 File Offset: 0x001514B0
		internal static void RunOrScheduleAction(IAsyncStateMachineBox box, bool allowInlining)
		{
			ref Task ptr = ref Task.t_currentTask;
			Task task = ptr;
			if (allowInlining && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				try
				{
					if (task != null)
					{
						ptr = null;
					}
					box.MoveNext();
				}
				catch (Exception exception)
				{
					Task.ThrowAsync(exception, null);
				}
				finally
				{
					if (task != null)
					{
						ptr = task;
					}
				}
				return;
			}
			if (TplEventSource.Log.IsEnabled())
			{
				AwaitTaskContinuation.UnsafeScheduleAction(box.MoveNextAction, task);
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItemInternal(box, true);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x00152330 File Offset: 0x00151530
		internal static void UnsafeScheduleAction(Action action, Task task)
		{
			AwaitTaskContinuation awaitTaskContinuation = new AwaitTaskContinuation(action, false);
			TplEventSource log = TplEventSource.Log;
			if (log.IsEnabled() && task != null)
			{
				awaitTaskContinuation.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, awaitTaskContinuation.m_continuationId);
			}
			ThreadPool.UnsafeQueueUserWorkItemInternal(awaitTaskContinuation, true);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0015238E File Offset: 0x0015158E
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			return new Delegate[]
			{
				AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action)
			};
		}

		// Token: 0x04000C0C RID: 3084
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04000C0D RID: 3085
		protected readonly Action m_action;

		// Token: 0x04000C0E RID: 3086
		protected int m_continuationId;

		// Token: 0x04000C0F RID: 3087
		private static readonly ContextCallback s_invokeContextCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04000C10 RID: 3088
		private static readonly Action<Action> s_invokeAction = delegate(Action action)
		{
			action();
		};
	}
}
