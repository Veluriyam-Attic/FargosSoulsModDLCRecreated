using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000557 RID: 1367
	public readonly struct TaskAwaiter<[Nullable(2)] TResult> : ICriticalNotifyCompletion, INotifyCompletion, ITaskAwaiter
	{
		// Token: 0x0600477D RID: 18301 RVA: 0x0017DAAF File Offset: 0x0017CCAF
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x0017DAB8 File Offset: 0x0017CCB8
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0017DAC5 File Offset: 0x0017CCC5
		[NullableContext(1)]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0017DAD5 File Offset: 0x0017CCD5
		[NullableContext(1)]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x0017DAE5 File Offset: 0x0017CCE5
		[StackTraceHidden]
		[NullableContext(1)]
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x0400113C RID: 4412
		private readonly Task<TResult> m_task;
	}
}
