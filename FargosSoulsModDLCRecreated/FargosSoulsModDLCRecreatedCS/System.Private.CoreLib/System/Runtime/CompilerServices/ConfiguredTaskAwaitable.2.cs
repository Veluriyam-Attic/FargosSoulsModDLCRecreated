using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200055C RID: 1372
	public readonly struct ConfiguredTaskAwaitable<[Nullable(2)] TResult>
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x0017DB68 File Offset: 0x0017CD68
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x0017DB77 File Offset: 0x0017CD77
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04001140 RID: 4416
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x0200055D RID: 1373
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IConfiguredTaskAwaiter
		{
			// Token: 0x0600478B RID: 18315 RVA: 0x0017DB7F File Offset: 0x0017CD7F
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000AC7 RID: 2759
			// (get) Token: 0x0600478C RID: 18316 RVA: 0x0017DB8F File Offset: 0x0017CD8F
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x0600478D RID: 18317 RVA: 0x0017DB9C File Offset: 0x0017CD9C
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x0600478E RID: 18318 RVA: 0x0017DBB1 File Offset: 0x0017CDB1
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x0600478F RID: 18319 RVA: 0x0017DBC6 File Offset: 0x0017CDC6
			[NullableContext(1)]
			[StackTraceHidden]
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x04001141 RID: 4417
			private readonly Task<TResult> m_task;

			// Token: 0x04001142 RID: 4418
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
