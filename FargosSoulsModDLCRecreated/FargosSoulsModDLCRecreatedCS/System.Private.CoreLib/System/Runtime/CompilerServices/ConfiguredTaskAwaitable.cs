using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200055A RID: 1370
	public readonly struct ConfiguredTaskAwaitable
	{
		// Token: 0x06004782 RID: 18306 RVA: 0x0017DAFD File Offset: 0x0017CCFD
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x0017DB0C File Offset: 0x0017CD0C
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x0400113D RID: 4413
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x0200055B RID: 1371
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IConfiguredTaskAwaiter
		{
			// Token: 0x06004784 RID: 18308 RVA: 0x0017DB14 File Offset: 0x0017CD14
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x06004785 RID: 18309 RVA: 0x0017DB24 File Offset: 0x0017CD24
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x06004786 RID: 18310 RVA: 0x0017DB31 File Offset: 0x0017CD31
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x06004787 RID: 18311 RVA: 0x0017DB46 File Offset: 0x0017CD46
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x06004788 RID: 18312 RVA: 0x0017DB5B File Offset: 0x0017CD5B
			[StackTraceHidden]
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x0400113E RID: 4414
			internal readonly Task m_task;

			// Token: 0x0400113F RID: 4415
			internal readonly bool m_continueOnCapturedContext;
		}
	}
}
