using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000509 RID: 1289
	[Nullable(0)]
	[NullableContext(1)]
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder<[Nullable(2)] TResult>
	{
		// Token: 0x06004696 RID: 18070 RVA: 0x0017BB50 File Offset: 0x0017AD50
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static AsyncValueTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncValueTaskMethodBuilder<TResult>);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x0017AD82 File Offset: 0x00179F82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<[Nullable(0)] TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x0017B0F6 File Offset: 0x0017A2F6
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			AsyncMethodBuilderCore.SetStateMachine(stateMachine, null);
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x0017BB68 File Offset: 0x0017AD68
		public void SetResult(TResult result)
		{
			if (this.m_task == null)
			{
				this._result = result;
				this.m_task = AsyncValueTaskMethodBuilder<TResult>.s_syncSuccessSentinel;
				return;
			}
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				Unsafe.As<AsyncValueTaskMethodBuilder<TResult>.StateMachineBox>(this.m_task).SetResult(result);
				return;
			}
			AsyncTaskMethodBuilder<TResult>.SetExistingTaskResult(Unsafe.As<Task<TResult>>(this.m_task), result);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x0017BBBA File Offset: 0x0017ADBA
		public void SetException(Exception exception)
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<TResult>.SetException(exception, Unsafe.As<object, AsyncValueTaskMethodBuilder<TResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<TResult>.SetException(exception, Unsafe.As<object, Task<TResult>>(ref this.m_task));
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x0017BBE8 File Offset: 0x0017ADE8
		internal static void SetException(Exception exception, [NotNull] ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox boxFieldRef)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			AsyncValueTaskMethodBuilder<TResult>.StateMachineBox stateMachineBox;
			if ((stateMachineBox = boxFieldRef) == null)
			{
				AsyncValueTaskMethodBuilder<TResult>.StateMachineBox stateMachineBox2;
				boxFieldRef = (stateMachineBox2 = AsyncValueTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox());
				stateMachineBox = stateMachineBox2;
			}
			stateMachineBox.SetException(exception);
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x0017BC18 File Offset: 0x0017AE18
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public ValueTask<TResult> Task
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				if (this.m_task == AsyncValueTaskMethodBuilder<TResult>.s_syncSuccessSentinel)
				{
					return new ValueTask<TResult>(this._result);
				}
				if (AsyncTaskCache.s_valueTaskPoolingEnabled)
				{
					AsyncValueTaskMethodBuilder<TResult>.StateMachineBox stateMachineBox = Unsafe.As<AsyncValueTaskMethodBuilder<TResult>.StateMachineBox>(this.m_task);
					if (stateMachineBox == null)
					{
						stateMachineBox = (this.m_task = AsyncValueTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox());
					}
					return new ValueTask<TResult>(stateMachineBox, stateMachineBox.Version);
				}
				Task<TResult> task = Unsafe.As<Task<TResult>>(this.m_task);
				if (task == null)
				{
					task = (this.m_task = new Task<TResult>());
				}
				return new ValueTask<TResult>(task);
			}
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x0017BC90 File Offset: 0x0017AE90
		public void AwaitOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<TResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, AsyncValueTaskMethodBuilder<TResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<TResult>.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, Task<TResult>>(ref this.m_task));
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x0017BCC0 File Offset: 0x0017AEC0
		internal static void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine, ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox box) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				awaiter.OnCompleted(AsyncValueTaskMethodBuilder<TResult>.GetStateMachineBox<TStateMachine>(ref stateMachine, ref box).MoveNextAction);
			}
			catch (Exception exception)
			{
				System.Threading.Tasks.Task.ThrowAsync(exception, null);
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0017BD04 File Offset: 0x0017AF04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<[Nullable(0)] TAwaiter, [Nullable(0)] TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (AsyncTaskCache.s_valueTaskPoolingEnabled)
			{
				AsyncValueTaskMethodBuilder<TResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, AsyncValueTaskMethodBuilder<TResult>.StateMachineBox>(ref this.m_task));
				return;
			}
			AsyncTaskMethodBuilder<TResult>.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine, Unsafe.As<object, Task<TResult>>(ref this.m_task));
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x0017BD34 File Offset: 0x0017AF34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine, [NotNull] ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox boxRef) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			IAsyncStateMachineBox stateMachineBox = AsyncValueTaskMethodBuilder<TResult>.GetStateMachineBox<TStateMachine>(ref stateMachine, ref boxRef);
			AsyncTaskMethodBuilder<VoidTaskResult>.AwaitUnsafeOnCompleted<TAwaiter>(ref awaiter, stateMachineBox);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x0017BD50 File Offset: 0x0017AF50
		private static IAsyncStateMachineBox GetStateMachineBox<TStateMachine>(ref TStateMachine stateMachine, [NotNull] ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox boxFieldRef) where TStateMachine : IAsyncStateMachine
		{
			ExecutionContext executionContext = ExecutionContext.Capture();
			AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> stateMachineBox = boxFieldRef as AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>;
			if (stateMachineBox != null)
			{
				if (stateMachineBox.Context != executionContext)
				{
					stateMachineBox.Context = executionContext;
				}
				return stateMachineBox;
			}
			AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<IAsyncStateMachine> stateMachineBox2 = boxFieldRef as AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<IAsyncStateMachine>;
			if (stateMachineBox2 != null)
			{
				if (stateMachineBox2.StateMachine == null)
				{
					Debugger.NotifyOfCrossThreadDependency();
					stateMachineBox2.StateMachine = stateMachine;
				}
				stateMachineBox2.Context = executionContext;
				return stateMachineBox2;
			}
			Debugger.NotifyOfCrossThreadDependency();
			AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> orCreateBox = AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.GetOrCreateBox();
			boxFieldRef = orCreateBox;
			orCreateBox.StateMachine = stateMachine;
			orCreateBox.Context = executionContext;
			return orCreateBox;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x0017BDD4 File Offset: 0x0017AFD4
		internal static AsyncValueTaskMethodBuilder<TResult>.StateMachineBox CreateWeaklyTypedStateMachineBox()
		{
			return new AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<IAsyncStateMachine>();
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x0017BDDB File Offset: 0x0017AFDB
		internal object ObjectIdForDebugger
		{
			get
			{
				if (this.m_task == null)
				{
					this.m_task = (AsyncTaskCache.s_valueTaskPoolingEnabled ? AsyncValueTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox() : AsyncTaskMethodBuilder<TResult>.CreateWeaklyTypedStateMachineBox());
				}
				return this.m_task;
			}
		}

		// Token: 0x040010E2 RID: 4322
		internal static readonly object s_syncSuccessSentinel = AsyncTaskCache.s_valueTaskPoolingEnabled ? new AsyncValueTaskMethodBuilder<TResult>.SyncSuccessSentinelStateMachineBox() : new Task<TResult>(default(TResult));

		// Token: 0x040010E3 RID: 4323
		private object m_task;

		// Token: 0x040010E4 RID: 4324
		private TResult _result;

		// Token: 0x0200050A RID: 1290
		internal abstract class StateMachineBox : IValueTaskSource<TResult>, IValueTaskSource
		{
			// Token: 0x060046A5 RID: 18085 RVA: 0x0017BE32 File Offset: 0x0017B032
			public void SetResult(TResult result)
			{
				this._valueTaskSource.SetResult(result);
			}

			// Token: 0x060046A6 RID: 18086 RVA: 0x0017BE40 File Offset: 0x0017B040
			public void SetException(Exception error)
			{
				this._valueTaskSource.SetException(error);
			}

			// Token: 0x060046A7 RID: 18087 RVA: 0x0017BE4E File Offset: 0x0017B04E
			public ValueTaskSourceStatus GetStatus(short token)
			{
				return this._valueTaskSource.GetStatus(token);
			}

			// Token: 0x060046A8 RID: 18088 RVA: 0x0017BE5C File Offset: 0x0017B05C
			public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
			{
				this._valueTaskSource.OnCompleted(continuation, state, token, flags);
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x060046A9 RID: 18089 RVA: 0x0017BE6E File Offset: 0x0017B06E
			public short Version
			{
				get
				{
					return this._valueTaskSource.Version;
				}
			}

			// Token: 0x060046AA RID: 18090 RVA: 0x0017BE7C File Offset: 0x0017B07C
			TResult IValueTaskSource<!0>.GetResult(short token)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x060046AB RID: 18091 RVA: 0x000C2700 File Offset: 0x000C1900
			void IValueTaskSource.GetResult(short token)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x040010E5 RID: 4325
			protected Action _moveNextAction;

			// Token: 0x040010E6 RID: 4326
			public ExecutionContext Context;

			// Token: 0x040010E7 RID: 4327
			protected ManualResetValueTaskSourceCore<TResult> _valueTaskSource;
		}

		// Token: 0x0200050B RID: 1291
		private sealed class SyncSuccessSentinelStateMachineBox : AsyncValueTaskMethodBuilder<TResult>.StateMachineBox
		{
			// Token: 0x060046AD RID: 18093 RVA: 0x0017BE90 File Offset: 0x0017B090
			public SyncSuccessSentinelStateMachineBox()
			{
				base.SetResult(default(TResult));
			}
		}

		// Token: 0x0200050C RID: 1292
		private sealed class StateMachineBox<TStateMachine> : AsyncValueTaskMethodBuilder<TResult>.StateMachineBox, IValueTaskSource<!0>, IValueTaskSource, IAsyncStateMachineBox, IThreadPoolWorkItem where TStateMachine : IAsyncStateMachine
		{
			// Token: 0x060046AE RID: 18094 RVA: 0x0017BEB4 File Offset: 0x0017B0B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal static AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> GetOrCreateBox()
			{
				if (Interlocked.CompareExchange(ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheLock, 1, 0) == 0)
				{
					AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> stateMachineBox = AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cache;
					if (stateMachineBox != null)
					{
						AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cache = stateMachineBox._next;
						stateMachineBox._next = null;
						AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheSize--;
						Volatile.Write(ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheLock, 0);
						return stateMachineBox;
					}
					Volatile.Write(ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheLock, 0);
				}
				return new AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>();
			}

			// Token: 0x060046AF RID: 18095 RVA: 0x0017BF14 File Offset: 0x0017B114
			private void ReturnOrDropBox()
			{
				this.StateMachine = default(TStateMachine);
				this.Context = null;
				this._valueTaskSource.Reset();
				if ((ushort)this._valueTaskSource.Version == 65535)
				{
					return;
				}
				if (Interlocked.CompareExchange(ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheLock, 1, 0) == 0)
				{
					if (AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheSize < AsyncTaskCache.s_valueTaskPoolingCacheSize)
					{
						this._next = AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cache;
						AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cache = this;
						AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheSize++;
					}
					Volatile.Write(ref AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_cacheLock, 0);
				}
			}

			// Token: 0x060046B0 RID: 18096 RVA: 0x0017BF95 File Offset: 0x0017B195
			private static void ExecutionContextCallback(object s)
			{
				Unsafe.As<AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>>(s).StateMachine.MoveNext();
			}

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x060046B1 RID: 18097 RVA: 0x0017BFB0 File Offset: 0x0017B1B0
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

			// Token: 0x060046B2 RID: 18098 RVA: 0x0017BFDD File Offset: 0x0017B1DD
			void IThreadPoolWorkItem.Execute()
			{
				this.MoveNext();
			}

			// Token: 0x060046B3 RID: 18099 RVA: 0x0017BFE8 File Offset: 0x0017B1E8
			public void MoveNext()
			{
				ExecutionContext context = this.Context;
				if (context == null)
				{
					this.StateMachine.MoveNext();
					return;
				}
				ExecutionContext.RunInternal(context, AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.s_callback, this);
			}

			// Token: 0x060046B4 RID: 18100 RVA: 0x0017C020 File Offset: 0x0017B220
			TResult IValueTaskSource<!0>.GetResult(short token)
			{
				TResult result;
				try
				{
					result = this._valueTaskSource.GetResult(token);
				}
				finally
				{
					this.ReturnOrDropBox();
				}
				return result;
			}

			// Token: 0x060046B5 RID: 18101 RVA: 0x0017C054 File Offset: 0x0017B254
			void IValueTaskSource.GetResult(short token)
			{
				try
				{
					this._valueTaskSource.GetResult(token);
				}
				finally
				{
					this.ReturnOrDropBox();
				}
			}

			// Token: 0x060046B6 RID: 18102 RVA: 0x0017C088 File Offset: 0x0017B288
			IAsyncStateMachine IAsyncStateMachineBox.GetStateMachineObject()
			{
				return this.StateMachine;
			}

			// Token: 0x040010E8 RID: 4328
			private static readonly ContextCallback s_callback = new ContextCallback(AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine>.ExecutionContextCallback);

			// Token: 0x040010E9 RID: 4329
			private static int s_cacheLock;

			// Token: 0x040010EA RID: 4330
			private static AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> s_cache;

			// Token: 0x040010EB RID: 4331
			private static int s_cacheSize;

			// Token: 0x040010EC RID: 4332
			private AsyncValueTaskMethodBuilder<TResult>.StateMachineBox<TStateMachine> _next;

			// Token: 0x040010ED RID: 4333
			public TStateMachine StateMachine;
		}
	}
}
