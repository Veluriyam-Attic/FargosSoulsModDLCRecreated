using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200032A RID: 810
	internal abstract class TaskContinuation
	{
		// Token: 0x06002B56 RID: 11094
		internal abstract void Run(Task completedTask, bool canInlineContinuationTask);

		// Token: 0x06002B57 RID: 11095 RVA: 0x00151B70 File Offset: 0x00150D70
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.InternalQueueTask(task);
				}
			}
			catch (Exception innerException)
			{
				TaskSchedulerException exceptionObject = new TaskSchedulerException(innerException);
				task.AddException(exceptionObject);
				task.Finish(false);
			}
		}

		// Token: 0x06002B58 RID: 11096
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();
	}
}
