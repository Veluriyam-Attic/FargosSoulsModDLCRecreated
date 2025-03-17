using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000523 RID: 1315
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x0017CE62 File Offset: 0x0017C062
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(in ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x0017CE70 File Offset: 0x0017C070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter(ref this._value);
		}

		// Token: 0x0400110E RID: 4366
		private readonly ValueTask _value;

		// Token: 0x02000524 RID: 1316
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IStateMachineBoxAwareAwaiter
		{
			// Token: 0x06004707 RID: 18183 RVA: 0x0017CE7D File Offset: 0x0017C07D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(in ValueTask value)
			{
				this._value = value;
			}

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x06004708 RID: 18184 RVA: 0x0017CE8B File Offset: 0x0017C08B
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x06004709 RID: 18185 RVA: 0x0017CE98 File Offset: 0x0017C098
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				this._value.ThrowIfCompletedUnsuccessfully();
			}

			// Token: 0x0600470A RID: 18186 RVA: 0x0017CEA8 File Offset: 0x0017C0A8
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task = obj as Task;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				Task.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x0600470B RID: 18187 RVA: 0x0017CF48 File Offset: 0x0017C148
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task = obj as Task;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				Task.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600470C RID: 18188 RVA: 0x0017CFE8 File Offset: 0x0017C1E8
			void IStateMachineBoxAwareAwaiter.AwaitUnsafeOnCompleted(IAsyncStateMachineBox box)
			{
				object obj = this._value._obj;
				Task task = obj as Task;
				if (task != null)
				{
					TaskAwaiter.UnsafeOnCompletedInternal(task, box, this._value._continueOnCapturedContext);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ThreadPool.s_invokeAsyncStateMachineBox, box, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				TaskAwaiter.UnsafeOnCompletedInternal(Task.CompletedTask, box, this._value._continueOnCapturedContext);
			}

			// Token: 0x0400110F RID: 4367
			private readonly ValueTask _value;
		}
	}
}
