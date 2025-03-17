using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000505 RID: 1285
	[NullableContext(1)]
	[Nullable(0)]
	public struct AsyncTaskMethodBuilder<[Nullable(2)] TResult>
	{
		// Token: 0x0600466C RID: 18028 RVA: 0x0017B1C8 File Offset: 0x0017A3C8
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static AsyncTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncTaskMethodBuilder<TResult>);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x0017AD82 File Offset: 0x00179F82
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x0017B1DE File Offset: 0x0017A3DE
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			AsyncMethodBuilderCore.SetStateMachine(stateMachine, this.m_task);
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x0017B1EC File Offset: 0x0017A3EC
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			AsyncTaskMethodBuilder<TResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, ref this.m_task);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x0017B1FC File Offset: 0x0017A3FC
		internal static void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine, ref Task<TResult> taskField) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				awaiter.OnCompleted(AsyncTaskMethodBuilder<TResult>.GetStateMachineBox<TStateMachine>(ref stateMachine, ref taskField).MoveNextAction);
			}
			catch (Exception exception)
			{
				System.Threading.Tasks.Task.ThrowAsync(exception, null);
			}
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x0017B240 File Offset: 0x0017A440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			AsyncTaskMethodBuilder<TResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, ref this.m_task);
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x0017B250 File Offset: 0x0017A450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine, [NotNull] ref Task<TResult> taskField) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			IAsyncStateMachineBox stateMachineBox = AsyncTaskMethodBuilder<TResult>.GetStateMachineBox<TStateMachine>(ref stateMachine, ref taskField);
			AsyncTaskMethodBuilder<TResult>.AwaitUnsafeOnCompleted<TAwaiter>(ref awaiter, stateMachineBox);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0017B26C File Offset: 0x0017A46C
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		internal static void AwaitUnsafeOnCompleted<TAwaiter>(ref TAwaiter awaiter, IAsyncStateMachineBox box) where TAwaiter : ICriticalNotifyCompletion
		{
			if (default(TAwaiter) != null && awaiter is ITaskAwaiter)
			{
				ref TaskAwaiter ptr = ref Unsafe.As<TAwaiter, TaskAwaiter>(ref awaiter);
				TaskAwaiter.UnsafeOnCompletedInternal(ptr.m_task, box, true);
				return;
			}
			if (default(TAwaiter) != null && awaiter is IConfiguredTaskAwaiter)
			{
				ref ConfiguredTaskAwaitable.ConfiguredTaskAwaiter ptr2 = ref Unsafe.As<TAwaiter, ConfiguredTaskAwaitable.ConfiguredTaskAwaiter>(ref awaiter);
				TaskAwaiter.UnsafeOnCompletedInternal(ptr2.m_task, box, ptr2.m_continueOnCapturedContext);
				return;
			}
			if (default(TAwaiter) != null && awaiter is IStateMachineBoxAwareAwaiter)
			{
				try
				{
					((IStateMachineBoxAwareAwaiter)((object)awaiter)).AwaitUnsafeOnCompleted(box);
					return;
				}
				catch (Exception exception)
				{
					System.Threading.Tasks.Task.ThrowAsync(exception, null);
					return;
				}
			}
			try
			{
				awaiter.UnsafeOnCompleted(box.MoveNextAction);
			}
			catch (Exception exception2)
			{
				System.Threading.Tasks.Task.ThrowAsync(exception2, null);
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x0017B370 File Offset: 0x0017A570
		private static IAsyncStateMachineBox GetStateMachineBox<TStateMachine>(ref TStateMachine stateMachine, [NotNull] ref Task<TResult> taskField) where TStateMachine : IAsyncStateMachine
		{
			ExecutionContext executionContext = ExecutionContext.Capture();
			AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine> asyncStateMachineBox = taskField as AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>;
			if (asyncStateMachineBox != null)
			{
				if (asyncStateMachineBox.Context != executionContext)
				{
					asyncStateMachineBox.Context = executionContext;
				}
				return asyncStateMachineBox;
			}
			AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<IAsyncStateMachine> asyncStateMachineBox2 = taskField as AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<IAsyncStateMachine>;
			if (asyncStateMachineBox2 != null)
			{
				if (asyncStateMachineBox2.StateMachine == null)
				{
					Debugger.NotifyOfCrossThreadDependency();
					asyncStateMachineBox2.StateMachine = stateMachine;
				}
				asyncStateMachineBox2.Context = executionContext;
				return asyncStateMachineBox2;
			}
			Debugger.NotifyOfCrossThreadDependency();
			AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine> asyncStateMachineBox3 = AsyncMethodBuilderCore.TrackAsyncMethodCompletion ? AsyncTaskMethodBuilder<TResult>.CreateDebugFinalizableAsyncStateMachineBox<TStateMachine>() : new AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>();
			taskField = asyncStateMachineBox3;
			asyncStateMachineBox3.StateMachine = stateMachine;
			asyncStateMachineBox3.Context = executionContext;
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(asyncStateMachineBox3.Id, "Async: " + stateMachine.GetType().Name, 0L);
			}
			if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
			{
				System.Threading.Tasks.Task.AddToActiveTasks(asyncStateMachineBox3);
			}
			return asyncStateMachineBox3;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x0017B449 File Offset: 0x0017A649
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine> CreateDebugFinalizableAsyncStateMachineBox<TStateMachine>() where TStateMachine : IAsyncStateMachine
		{
			return new AsyncTaskMethodBuilder<TResult>.DebugFinalizableAsyncStateMachineBox<TStateMachine>();
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x0017B450 File Offset: 0x0017A650
		public Task<TResult> Task
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_task ?? this.InitializeTaskAsPromise();
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x0017B464 File Offset: 0x0017A664
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Task<TResult> InitializeTaskAsPromise()
		{
			return this.m_task = new Task<TResult>();
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0017B47F File Offset: 0x0017A67F
		internal static Task<TResult> CreateWeaklyTypedStateMachineBox()
		{
			if (!AsyncMethodBuilderCore.TrackAsyncMethodCompletion)
			{
				return new AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<IAsyncStateMachine>();
			}
			return AsyncTaskMethodBuilder<TResult>.CreateDebugFinalizableAsyncStateMachineBox<IAsyncStateMachine>();
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x0017B493 File Offset: 0x0017A693
		public void SetResult(TResult result)
		{
			if (this.m_task == null)
			{
				this.m_task = AsyncTaskMethodBuilder<TResult>.GetTaskForResult(result);
				return;
			}
			AsyncTaskMethodBuilder<TResult>.SetExistingTaskResult(this.m_task, result);
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x0017B4B6 File Offset: 0x0017A6B6
		internal static void SetExistingTaskResult(Task<TResult> task, TResult result)
		{
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationEnd(task.Id, AsyncCausalityStatus.Completed);
			}
			if (!task.TrySetResult(result))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x0017B4E5 File Offset: 0x0017A6E5
		public void SetException(Exception exception)
		{
			AsyncTaskMethodBuilder<TResult>.SetException(exception, ref this.m_task);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x0017B4F4 File Offset: 0x0017A6F4
		internal static void SetException(Exception exception, ref Task<TResult> taskField)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			Task<TResult> task;
			if ((task = taskField) == null)
			{
				Task<TResult> task2;
				taskField = (task2 = new Task<TResult>());
				task = task2;
			}
			Task<TResult> task3 = task;
			OperationCanceledException ex = exception as OperationCanceledException;
			if (!((ex != null) ? task3.TrySetCanceled(ex.CancellationToken, ex) : task3.TrySetException(exception)))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x0017B547 File Offset: 0x0017A747
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			AsyncTaskMethodBuilder<TResult>.SetNotificationForWaitCompletion(enabled, ref this.m_task);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x0017B558 File Offset: 0x0017A758
		internal static void SetNotificationForWaitCompletion(bool enabled, [NotNull] ref Task<TResult> taskField)
		{
			Task<TResult> task;
			if ((task = taskField) == null)
			{
				Task<TResult> task2;
				taskField = (task2 = AsyncTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox());
				task = task2;
			}
			task.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x0017B57C File Offset: 0x0017A77C
		internal object ObjectIdForDebugger
		{
			get
			{
				Task<TResult> result;
				if ((result = this.m_task) == null)
				{
					result = (this.m_task = AsyncTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox());
				}
				return result;
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x0017B5A4 File Offset: 0x0017A7A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Task<TResult> GetTaskForResult(TResult result)
		{
			if (default(TResult) != null)
			{
				if (typeof(TResult) == typeof(bool))
				{
					Task<bool> value = ((bool)((object)result)) ? AsyncTaskCache.s_trueTask : AsyncTaskCache.s_falseTask;
					return Unsafe.As<Task<TResult>>(value);
				}
				if (typeof(TResult) == typeof(int))
				{
					int num = (int)((object)result);
					if (num < 9 && num >= -1)
					{
						Task<int> value2 = AsyncTaskCache.s_int32Tasks[num - -1];
						return Unsafe.As<Task<TResult>>(value2);
					}
				}
				else if ((typeof(TResult) == typeof(uint) && (uint)((object)result) == 0U) || (typeof(TResult) == typeof(byte) && (byte)((object)result) == 0) || (typeof(TResult) == typeof(sbyte) && (sbyte)((object)result) == 0) || (typeof(TResult) == typeof(char) && (char)((object)result) == '\0') || (typeof(TResult) == typeof(long) && (long)((object)result) == 0L) || (typeof(TResult) == typeof(ulong) && (ulong)((object)result) == 0UL) || (typeof(TResult) == typeof(short) && (short)((object)result) == 0) || (typeof(TResult) == typeof(ushort) && (ushort)((object)result) == 0) || (typeof(TResult) == typeof(IntPtr) && (IntPtr)0 == (IntPtr)((object)result)) || (typeof(TResult) == typeof(UIntPtr) && (UIntPtr)0 == (UIntPtr)((object)result)))
				{
					return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
				}
			}
			else if (result == null)
			{
				return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
			}
			return new Task<TResult>(result);
		}

		// Token: 0x040010DA RID: 4314
		internal static readonly Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default(TResult));

		// Token: 0x040010DB RID: 4315
		private Task<TResult> m_task;

		// Token: 0x02000506 RID: 1286
		private sealed class DebugFinalizableAsyncStateMachineBox<TStateMachine> : AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine> where TStateMachine : IAsyncStateMachine
		{
			// Token: 0x06004682 RID: 18050 RVA: 0x0017B838 File Offset: 0x0017AA38
			~DebugFinalizableAsyncStateMachineBox()
			{
				if (!base.IsCompleted)
				{
					TplEventSource.Log.IncompleteAsyncMethod(this);
				}
			}
		}

		// Token: 0x02000507 RID: 1287
		private class AsyncStateMachineBox<TStateMachine> : Task<TResult>, IAsyncStateMachineBox where TStateMachine : IAsyncStateMachine
		{
			// Token: 0x06004684 RID: 18052 RVA: 0x0017B87C File Offset: 0x0017AA7C
			private static void ExecutionContextCallback(object s)
			{
				Unsafe.As<AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>>(s).StateMachine.MoveNext();
			}

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x06004685 RID: 18053 RVA: 0x0017B894 File Offset: 0x0017AA94
			public Action MoveNextAction
			{
				get
				{
					Action result;
					if ((result = this._moveNextAction) == null)
					{
						result = (this._moveNextAction = new Action(this.MoveNext));
					}
					return result;
				}
			}

			// Token: 0x06004686 RID: 18054 RVA: 0x0017B8C1 File Offset: 0x0017AAC1
			internal sealed override void ExecuteFromThreadPool(Thread threadPoolThread)
			{
				this.MoveNext(threadPoolThread);
			}

			// Token: 0x06004687 RID: 18055 RVA: 0x0017B8CA File Offset: 0x0017AACA
			public void MoveNext()
			{
				this.MoveNext(null);
			}

			// Token: 0x06004688 RID: 18056 RVA: 0x0017B8D4 File Offset: 0x0017AAD4
			private void MoveNext(Thread threadPoolThread)
			{
				bool flag = TplEventSource.Log.IsEnabled();
				if (flag)
				{
					TplEventSource.Log.TraceSynchronousWorkBegin(base.Id, CausalitySynchronousWork.Execution);
				}
				ExecutionContext context = this.Context;
				if (context == null)
				{
					this.StateMachine.MoveNext();
				}
				else if (threadPoolThread == null)
				{
					ExecutionContext.RunInternal(context, AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>.s_callback, this);
				}
				else
				{
					ExecutionContext.RunFromThreadPoolDispatchLoop(threadPoolThread, context, AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>.s_callback, this);
				}
				if (base.IsCompleted)
				{
					if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
					{
						System.Threading.Tasks.Task.RemoveFromActiveTasks(this);
					}
					this.StateMachine = default(TStateMachine);
					this.Context = null;
					if (AsyncMethodBuilderCore.TrackAsyncMethodCompletion)
					{
						GC.SuppressFinalize(this);
					}
				}
				if (flag)
				{
					TplEventSource.Log.TraceSynchronousWorkEnd(CausalitySynchronousWork.Execution);
				}
			}

			// Token: 0x06004689 RID: 18057 RVA: 0x0017B97E File Offset: 0x0017AB7E
			IAsyncStateMachine IAsyncStateMachineBox.GetStateMachineObject()
			{
				return this.StateMachine;
			}

			// Token: 0x040010DC RID: 4316
			private static readonly ContextCallback s_callback = new ContextCallback(AsyncTaskMethodBuilder<TResult>.AsyncStateMachineBox<TStateMachine>.ExecutionContextCallback);

			// Token: 0x040010DD RID: 4317
			private Action _moveNextAction;

			// Token: 0x040010DE RID: 4318
			public TStateMachine StateMachine;

			// Token: 0x040010DF RID: 4319
			public ExecutionContext Context;
		}
	}
}
