using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FD RID: 1277
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		// Token: 0x06004649 RID: 17993 RVA: 0x0017AD6C File Offset: 0x00179F6C
		public static AsyncIteratorMethodBuilder Create()
		{
			return default(AsyncIteratorMethodBuilder);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x0017AD82 File Offset: 0x00179F82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0017AD8A File Offset: 0x00179F8A
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0017AD99 File Offset: 0x00179F99
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0017ADA8 File Offset: 0x00179FA8
		public void Complete()
		{
			this._methodBuilder.SetResult();
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x0017ADB5 File Offset: 0x00179FB5
		internal object ObjectIdForDebugger
		{
			get
			{
				return this._methodBuilder.ObjectIdForDebugger;
			}
		}

		// Token: 0x040010CF RID: 4303
		private AsyncTaskMethodBuilder _methodBuilder;
	}
}
