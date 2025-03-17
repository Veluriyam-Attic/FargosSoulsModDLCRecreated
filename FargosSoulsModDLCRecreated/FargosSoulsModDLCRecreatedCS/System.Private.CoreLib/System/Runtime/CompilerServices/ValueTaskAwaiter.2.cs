using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000564 RID: 1380
	public readonly struct ValueTaskAwaiter<[Nullable(2)] TResult> : ICriticalNotifyCompletion, INotifyCompletion, IStateMachineBoxAwareAwaiter
	{
		// Token: 0x060047A1 RID: 18337 RVA: 0x0017DDDD File Offset: 0x0017CFDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(in ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x0017DDEB File Offset: 0x0017CFEB
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0017DDF8 File Offset: 0x0017CFF8
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TResult GetResult()
		{
			return this._value.Result;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0017DE08 File Offset: 0x0017D008
		[NullableContext(1)]
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			Task.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0017DE70 File Offset: 0x0017D070
		[NullableContext(1)]
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			Task.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0017DED8 File Offset: 0x0017D0D8
		void IStateMachineBoxAwareAwaiter.AwaitUnsafeOnCompleted(IAsyncStateMachineBox box)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				TaskAwaiter.UnsafeOnCompletedInternal(task, box, true);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ThreadPool.s_invokeAsyncStateMachineBox, box, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			TaskAwaiter.UnsafeOnCompletedInternal(Task.CompletedTask, box, true);
		}

		// Token: 0x04001149 RID: 4425
		private readonly ValueTask<TResult> _value;
	}
}
