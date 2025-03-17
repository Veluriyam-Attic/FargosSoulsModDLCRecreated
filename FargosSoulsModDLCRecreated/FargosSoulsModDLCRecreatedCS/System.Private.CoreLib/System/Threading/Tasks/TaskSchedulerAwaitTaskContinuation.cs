using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200032F RID: 815
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x00151F34 File Offset: 0x00151134
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext) : base(action, flowExecutionContext)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x00151F48 File Offset: 0x00151148
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception exception)
				{
					Task.ThrowAsync(exception, null);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04000C09 RID: 3081
		private readonly TaskScheduler m_scheduler;
	}
}
