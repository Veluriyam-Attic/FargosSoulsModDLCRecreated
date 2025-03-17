using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031A RID: 794
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x06002B12 RID: 11026 RVA: 0x0015113C File Offset: 0x0015033C
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0015114B File Offset: 0x0015034B
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x00151158 File Offset: 0x00150358
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x00151165 File Offset: 0x00150365
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x00151172 File Offset: 0x00150372
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00151180 File Offset: 0x00150380
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x001511B0 File Offset: 0x001503B0
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04000BD4 RID: 3028
		private readonly Task m_task;
	}
}
