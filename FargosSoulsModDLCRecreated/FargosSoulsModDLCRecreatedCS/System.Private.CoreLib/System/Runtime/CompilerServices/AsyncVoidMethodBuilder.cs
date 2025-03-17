using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200050D RID: 1293
	[NullableContext(1)]
	[Nullable(0)]
	public struct AsyncVoidMethodBuilder
	{
		// Token: 0x060046B9 RID: 18105 RVA: 0x0017C0B0 File Offset: 0x0017B2B0
		public static AsyncVoidMethodBuilder Create()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext != null)
			{
				synchronizationContext.OperationStarted();
			}
			return new AsyncVoidMethodBuilder
			{
				_synchronizationContext = synchronizationContext
			};
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x0017AD82 File Offset: 0x00179F82
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x0017C0DD File Offset: 0x0017B2DD
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._builder.SetStateMachine(stateMachine);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0017C0EB File Offset: 0x0017B2EB
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x0017C0FA File Offset: 0x0017B2FA
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x0017C109 File Offset: 0x0017B309
		public void SetResult()
		{
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationEnd(this.Task.Id, AsyncCausalityStatus.Completed);
			}
			this._builder.SetResult();
			if (this._synchronizationContext != null)
			{
				this.NotifySynchronizationContextOfCompletion();
			}
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x0017C148 File Offset: 0x0017B348
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationEnd(this.Task.Id, AsyncCausalityStatus.Error);
			}
			if (this._synchronizationContext != null)
			{
				try
				{
					Task.ThrowAsync(exception, this._synchronizationContext);
					goto IL_50;
				}
				finally
				{
					this.NotifySynchronizationContextOfCompletion();
				}
			}
			Task.ThrowAsync(exception, null);
			IL_50:
			this._builder.SetResult();
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x0017C1C0 File Offset: 0x0017B3C0
		private void NotifySynchronizationContextOfCompletion()
		{
			try
			{
				this._synchronizationContext.OperationCompleted();
			}
			catch (Exception exception)
			{
				Task.ThrowAsync(exception, null);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060046C1 RID: 18113 RVA: 0x0017C1F4 File Offset: 0x0017B3F4
		private Task Task
		{
			get
			{
				return this._builder.Task;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x0017C201 File Offset: 0x0017B401
		internal object ObjectIdForDebugger
		{
			get
			{
				return this._builder.ObjectIdForDebugger;
			}
		}

		// Token: 0x040010EE RID: 4334
		private SynchronizationContext _synchronizationContext;

		// Token: 0x040010EF RID: 4335
		private AsyncTaskMethodBuilder _builder;
	}
}
