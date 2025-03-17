using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200032C RID: 812
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06002B5D RID: 11101 RVA: 0x00151D81 File Offset: 0x00150F81
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext) : base(action, flowExecutionContext)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00151D94 File Offset: 0x00150F94
		internal sealed override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.Current)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEventSource log = TplEventSource.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00151E18 File Offset: 0x00151018
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			TplEventSource log = TplEventSource.Log;
			if (log.TasksSetActivityIds && synchronizationContextAwaitTaskContinuation.m_continuationId != 0)
			{
				synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(synchronizationContextAwaitTaskContinuation.m_continuationId, synchronizationContextAwaitTaskContinuation.m_action));
				return;
			}
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00151E7C File Offset: 0x0015107C
		private static Action GetActionLogDelegate(int continuationId, Action action)
		{
			return delegate()
			{
				Guid activityId = TplEventSource.CreateGuidForTaskID(continuationId);
				Guid currentThreadActivityId;
				EventSource.SetCurrentThreadActivityId(activityId, out currentThreadActivityId);
				try
				{
					action();
				}
				finally
				{
					EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
				}
			};
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x00151EA9 File Offset: 0x001510A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback result;
			if ((result = SynchronizationContextAwaitTaskContinuation.s_postActionCallback) == null)
			{
				result = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return result;
		}

		// Token: 0x04000C03 RID: 3075
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04000C04 RID: 3076
		private static ContextCallback s_postActionCallback;

		// Token: 0x04000C05 RID: 3077
		private readonly SynchronizationContext m_syncContext;
	}
}
