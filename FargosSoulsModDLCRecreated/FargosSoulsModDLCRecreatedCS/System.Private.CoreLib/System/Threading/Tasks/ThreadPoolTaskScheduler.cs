using System;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x0200033F RID: 831
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06002C1A RID: 11290 RVA: 0x001539DA File Offset: 0x00152BDA
		internal ThreadPoolTaskScheduler()
		{
			int id = base.Id;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x001539EC File Offset: 0x00152BEC
		protected internal override void QueueTask(Task task)
		{
			TaskCreationOptions options = task.Options;
			if ((options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
				{
					IsBackground = true
				}.Start(task);
				return;
			}
			bool preferLocal = (options & TaskCreationOptions.PreferFairness) == TaskCreationOptions.None;
			ThreadPool.UnsafeQueueUserWorkItemInternal(task, preferLocal);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x00153A30 File Offset: 0x00152C30
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			try
			{
				task.ExecuteEntryUnsafe(null);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return true;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x00153A70 File Offset: 0x00152C70
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x00153A78 File Offset: 0x00152C78
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00153A85 File Offset: 0x00152C85
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<object> tpwItems)
		{
			foreach (object obj in tpwItems)
			{
				Task task = obj as Task;
				if (task != null)
				{
					yield return task;
				}
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x00153A95 File Offset: 0x00152C95
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x04000C2B RID: 3115
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = delegate(object s)
		{
			((Task)s).ExecuteEntryUnsafe(null);
		};
	}
}
