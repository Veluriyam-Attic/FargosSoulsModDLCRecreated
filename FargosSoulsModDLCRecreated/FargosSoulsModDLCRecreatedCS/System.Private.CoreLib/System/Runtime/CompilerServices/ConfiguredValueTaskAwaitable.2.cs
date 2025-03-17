using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000525 RID: 1317
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable<[Nullable(2)] TResult>
	{
		// Token: 0x0600470D RID: 18189 RVA: 0x0017D065 File Offset: 0x0017C265
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(in ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0017D073 File Offset: 0x0017C273
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter(ref this._value);
		}

		// Token: 0x04001110 RID: 4368
		private readonly ValueTask<TResult> _value;

		// Token: 0x02000526 RID: 1318
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IStateMachineBoxAwareAwaiter
		{
			// Token: 0x0600470F RID: 18191 RVA: 0x0017D080 File Offset: 0x0017C280
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(in ValueTask<TResult> value)
			{
				this._value = value;
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06004710 RID: 18192 RVA: 0x0017D08E File Offset: 0x0017C28E
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x06004711 RID: 18193 RVA: 0x0017D09B File Offset: 0x0017C29B
			[NullableContext(1)]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TResult GetResult()
			{
				return this._value.Result;
			}

			// Token: 0x06004712 RID: 18194 RVA: 0x0017D0A8 File Offset: 0x0017C2A8
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				Task.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x06004713 RID: 18195 RVA: 0x0017D14C File Offset: 0x0017C34C
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				Task.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x06004714 RID: 18196 RVA: 0x0017D1EC File Offset: 0x0017C3EC
			void IStateMachineBoxAwareAwaiter.AwaitUnsafeOnCompleted(IAsyncStateMachineBox box)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					TaskAwaiter.UnsafeOnCompletedInternal(task, box, this._value._continueOnCapturedContext);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ThreadPool.s_invokeAsyncStateMachineBox, box, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				TaskAwaiter.UnsafeOnCompletedInternal(Task.CompletedTask, box, this._value._continueOnCapturedContext);
			}

			// Token: 0x04001111 RID: 4369
			private readonly ValueTask<TResult> _value;
		}
	}
}
