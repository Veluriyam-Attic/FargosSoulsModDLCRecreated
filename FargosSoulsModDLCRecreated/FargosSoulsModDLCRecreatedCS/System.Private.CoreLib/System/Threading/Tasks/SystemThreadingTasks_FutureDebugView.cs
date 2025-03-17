using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FA RID: 762
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x0600299E RID: 10654 RVA: 0x0014BD7C File Offset: 0x0014AF7C
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x0014BD8C File Offset: 0x0014AF8C
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x0014BDBC File Offset: 0x0014AFBC
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x0014BDC9 File Offset: 0x0014AFC9
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x0014BDD6 File Offset: 0x0014AFD6
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x0014BDE3 File Offset: 0x0014AFE3
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x0014BDF0 File Offset: 0x0014AFF0
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x0014BE20 File Offset: 0x0014B020
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04000B64 RID: 2916
		private readonly Task<TResult> m_task;
	}
}
