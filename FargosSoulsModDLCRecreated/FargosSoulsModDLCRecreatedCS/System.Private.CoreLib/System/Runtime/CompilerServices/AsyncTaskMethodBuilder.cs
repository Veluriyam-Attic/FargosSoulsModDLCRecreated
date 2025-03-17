using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000504 RID: 1284
	[NullableContext(1)]
	[Nullable(0)]
	public struct AsyncTaskMethodBuilder
	{
		// Token: 0x06004661 RID: 18017 RVA: 0x0017B0E0 File Offset: 0x0017A2E0
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x0017AD82 File Offset: 0x00179F82
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x0017B0F6 File Offset: 0x0017A2F6
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			AsyncMethodBuilderCore.SetStateMachine(stateMachine, null);
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x0017B0FF File Offset: 0x0017A2FF
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			AsyncTaskMethodBuilder<VoidTaskResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, ref this.m_task);
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x0017B10E File Offset: 0x0017A30E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			AsyncTaskMethodBuilder<VoidTaskResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, ref this.m_task);
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x0017B11D File Offset: 0x0017A31D
		public Task Task
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_task ?? this.InitializeTaskAsPromise();
			}
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x0017B130 File Offset: 0x0017A330
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Task<VoidTaskResult> InitializeTaskAsPromise()
		{
			return this.m_task = new Task<VoidTaskResult>();
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x0017B14C File Offset: 0x0017A34C
		public void SetResult()
		{
			if (this.m_task == null)
			{
				this.m_task = Task.s_cachedCompleted;
				return;
			}
			AsyncTaskMethodBuilder<VoidTaskResult>.SetExistingTaskResult(this.m_task, default(VoidTaskResult));
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0017B181 File Offset: 0x0017A381
		public void SetException(Exception exception)
		{
			AsyncTaskMethodBuilder<VoidTaskResult>.SetException(exception, ref this.m_task);
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0017B18F File Offset: 0x0017A38F
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			AsyncTaskMethodBuilder<VoidTaskResult>.SetNotificationForWaitCompletion(enabled, ref this.m_task);
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x0017B1A0 File Offset: 0x0017A3A0
		internal object ObjectIdForDebugger
		{
			get
			{
				Task<VoidTaskResult> result;
				if ((result = this.m_task) == null)
				{
					result = (this.m_task = AsyncTaskMethodBuilder<VoidTaskResult>.CreateWeaklyTypedStateMachineBox());
				}
				return result;
			}
		}

		// Token: 0x040010D9 RID: 4313
		private Task<VoidTaskResult> m_task;
	}
}
