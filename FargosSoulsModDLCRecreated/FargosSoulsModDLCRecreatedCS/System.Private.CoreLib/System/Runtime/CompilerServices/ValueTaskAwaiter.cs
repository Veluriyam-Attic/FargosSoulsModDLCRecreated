using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000562 RID: 1378
	public readonly struct ValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IStateMachineBoxAwareAwaiter
	{
		// Token: 0x06004797 RID: 18327 RVA: 0x0017DC44 File Offset: 0x0017CE44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(in ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x0017DC52 File Offset: 0x0017CE52
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0017DC5F File Offset: 0x0017CE5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			this._value.ThrowIfCompletedUnsuccessfully();
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0017DC6C File Offset: 0x0017CE6C
		[NullableContext(1)]
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			Task.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0017DCD4 File Offset: 0x0017CED4
		[NullableContext(1)]
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			Task.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0017DD3C File Offset: 0x0017CF3C
		void IStateMachineBoxAwareAwaiter.AwaitUnsafeOnCompleted(IAsyncStateMachineBox box)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				TaskAwaiter.UnsafeOnCompletedInternal(task, box, true);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ThreadPool.s_invokeAsyncStateMachineBox, box, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			TaskAwaiter.UnsafeOnCompletedInternal(Task.CompletedTask, box, true);
		}

		// Token: 0x04001146 RID: 4422
		internal static readonly Action<object> s_invokeActionDelegate = delegate(object state)
		{
			Action action = state as Action;
			if (action == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			action();
		};

		// Token: 0x04001147 RID: 4423
		private readonly ValueTask _value;
	}
}
