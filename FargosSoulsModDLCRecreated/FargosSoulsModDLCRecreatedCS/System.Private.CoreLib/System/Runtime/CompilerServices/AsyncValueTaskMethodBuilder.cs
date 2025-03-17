using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000508 RID: 1288
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder
	{
		// Token: 0x0600468C RID: 18060 RVA: 0x0017B9A8 File Offset: 0x0017ABA8
		public static AsyncValueTaskMethodBuilder Create()
		{
			return default(AsyncValueTaskMethodBuilder);
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x0017AD82 File Offset: 0x00179F82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x0017B0F6 File Offset: 0x0017A2F6
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			AsyncMethodBuilderCore.SetStateMachine(stateMachine, null);
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x0017B9C0 File Offset: 0x0017ABC0
		public void SetResult()
		{
			if (this.m_task == null)
			{
				this.m_task = AsyncValueTaskMethodBuilder.s_syncSuccessSentinel;
				return;
			}
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				Unsafe.As<AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox>(this.m_task).SetResult(default(VoidTaskResult));
				return;
			}
			AsyncTaskMethodBuilder<VoidTaskResult>.SetExistingTaskResult(Unsafe.As<Task<VoidTaskResult>>(this.m_task), default(VoidTaskResult));
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x0017BA1B File Offset: 0x0017AC1B
		public void SetException(Exception exception)
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<VoidTaskResult>.SetException(exception, Unsafe.As<object, AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<VoidTaskResult>.SetException(exception, Unsafe.As<object, Task<VoidTaskResult>>(ref this.m_task));
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x0017BA48 File Offset: 0x0017AC48
		public ValueTask Task
		{
			get
			{
				if (this.m_task == AsyncValueTaskMethodBuilder.s_syncSuccessSentinel)
				{
					return default(ValueTask);
				}
				if (AsyncTaskCache.s_valueTaskPoolingEnabled)
				{
					AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox stateMachineBox = Unsafe.As<AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox>(this.m_task);
					if (stateMachineBox == null)
					{
						stateMachineBox = (this.m_task = AsyncValueTaskMethodBuilder<VoidTaskResult>.CreateWeaklyTypedStateMachineBox());
					}
					return new ValueTask(stateMachineBox, stateMachineBox.Version);
				}
				Task<VoidTaskResult> task = Unsafe.As<Task<VoidTaskResult>>(this.m_task);
				if (task == null)
				{
					task = (this.m_task = new Task<VoidTaskResult>());
				}
				return new ValueTask(task);
			}
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x0017BABE File Offset: 0x0017ACBE
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<VoidTaskResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<VoidTaskResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, Task<VoidTaskResult>>(ref this.m_task));
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x0017BAEC File Offset: 0x0017ACEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<VoidTaskResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, AsyncValueTaskMethodBuilder<VoidTaskResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<VoidTaskResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, Task<VoidTaskResult>>(ref this.m_task));
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x0017BB1A File Offset: 0x0017AD1A
		internal object ObjectIdForDebugger
		{
			get
			{
				if (this.m_task == null)
				{
					this.m_task = (AsyncTaskCache.s_valueTaskPoolingEnabled ? AsyncValueTaskMethodBuilder<VoidTaskResult>.CreateWeaklyTypedStateMachineBox() : AsyncTaskMethodBuilder<VoidTaskResult>.CreateWeaklyTypedStateMachineBox());
				}
				return this.m_task;
			}
		}

		// Token: 0x040010E0 RID: 4320
		private static readonly object s_syncSuccessSentinel = AsyncValueTaskMethodBuilder<VoidTaskResult>.s_syncSuccessSentinel;

		// Token: 0x040010E1 RID: 4321
		private object m_task;
	}
}
