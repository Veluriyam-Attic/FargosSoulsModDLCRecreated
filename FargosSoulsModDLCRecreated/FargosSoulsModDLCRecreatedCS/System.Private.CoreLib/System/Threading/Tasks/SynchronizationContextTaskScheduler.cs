using System;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x0200033B RID: 827
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x06002C08 RID: 11272 RVA: 0x00153919 File Offset: 0x00152B19
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException(SR.TaskScheduler_FromCurrentSynchronizationContext_NoCurrent);
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0015393B File Offset: 0x00152B3B
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0015394E File Offset: 0x00152B4E
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000C26FD File Offset: 0x000C18FD
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000C26 RID: 3110
		private readonly SynchronizationContext m_synchronizationContext;

		// Token: 0x04000C27 RID: 3111
		private static readonly SendOrPostCallback s_postCallback = delegate(object s)
		{
			((Task)s).ExecuteEntry();
		};
	}
}
