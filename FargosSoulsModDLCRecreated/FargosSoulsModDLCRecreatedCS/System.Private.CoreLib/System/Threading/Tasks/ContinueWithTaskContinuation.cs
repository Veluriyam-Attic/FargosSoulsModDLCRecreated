using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200032B RID: 811
	internal sealed class ContinueWithTaskContinuation : TaskContinuation
	{
		// Token: 0x06002B5A RID: 11098 RVA: 0x00151BE8 File Offset: 0x00150DE8
		internal ContinueWithTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
		{
			this.m_task = task;
			this.m_options = options;
			this.m_taskScheduler = scheduler;
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(this.m_task.Id, "Task.ContinueWith: " + task.m_action.Method.Name, 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this.m_task);
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00151C60 File Offset: 0x00150E60
		internal override void Run(Task completedTask, bool canInlineContinuationTask)
		{
			Task task = this.m_task;
			this.m_task = null;
			TaskContinuationOptions options = this.m_options;
			bool flag = completedTask.IsCompletedSuccessfully ? ((options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None) : (completedTask.IsCanceled ? ((options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None) : ((options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None));
			if (flag)
			{
				if (!task.IsCanceled && TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationRelation(task.Id, CausalityRelation.AssignDelegate);
				}
				task.m_taskScheduler = this.m_taskScheduler;
				if (canInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
				{
					TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
					return;
				}
				try
				{
					task.ScheduleAndStart(true);
					return;
				}
				catch (TaskSchedulerException)
				{
					return;
				}
			}
			Task.ContingentProperties contingentProperties = task.m_contingentProperties;
			if (contingentProperties == null || contingentProperties.m_cancellationToken == default(CancellationToken))
			{
				task.InternalCancelContinueWithInitialState();
				return;
			}
			task.InternalCancel();
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00151D48 File Offset: 0x00150F48
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_task == null)
			{
				return null;
			}
			if (this.m_task.m_action != null)
			{
				return new Delegate[]
				{
					this.m_task.m_action
				};
			}
			return this.m_task.GetDelegateContinuationsForDebugger();
		}

		// Token: 0x04000C00 RID: 3072
		internal Task m_task;

		// Token: 0x04000C01 RID: 3073
		internal readonly TaskContinuationOptions m_options;

		// Token: 0x04000C02 RID: 3074
		private readonly TaskScheduler m_taskScheduler;
	}
}
