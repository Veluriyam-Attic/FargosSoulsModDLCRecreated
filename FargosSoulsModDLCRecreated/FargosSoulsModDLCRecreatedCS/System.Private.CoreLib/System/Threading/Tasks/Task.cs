using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002F7 RID: 759
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_FutureDebugView<>))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
	public class Task<[Nullable(2)] TResult> : Task
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x0014B30F File Offset: 0x0014A50F
		internal Task()
		{
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0014B667 File Offset: 0x0014A867
		internal Task(object state, TaskCreationOptions options) : base(state, options, true)
		{
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0014B674 File Offset: 0x0014A874
		internal Task(TResult result) : base(false, TaskCreationOptions.None, default(CancellationToken))
		{
			this.m_result = result;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0014B699 File Offset: 0x0014A899
		internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct) : base(canceled, creationOptions, ct)
		{
			if (!canceled)
			{
				this.m_result = result;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0014B6B0 File Offset: 0x0014A8B0
		public Task(Func<TResult> function) : this(function, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0014B6D1 File Offset: 0x0014A8D1
		public Task(Func<TResult> function, CancellationToken cancellationToken) : this(function, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0014B6E0 File Offset: 0x0014A8E0
		public Task(Func<TResult> function, TaskCreationOptions creationOptions) : this(function, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0014B706 File Offset: 0x0014A906
		public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(function, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0014B71C File Offset: 0x0014A91C
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state) : this(function, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0014B73E File Offset: 0x0014A93E
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state, CancellationToken cancellationToken) : this(function, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0014B750 File Offset: 0x0014A950
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state, TaskCreationOptions creationOptions) : this(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0014B777 File Offset: 0x0014A977
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(function, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0014B78D File Offset: 0x0014A98D
		internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler) : base(valueSelector, null, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0014B79F File Offset: 0x0014A99F
		internal Task(Delegate valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler) : base(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0014B7B4 File Offset: 0x0014A9B4
		internal static Task<TResult> StartNew(Task parent, Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (function == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.function);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task<TResult> task = new Task<TResult>(function, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0014B7F4 File Offset: 0x0014A9F4
		internal static Task<TResult> StartNew(Task parent, Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (function == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.function);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task<TResult> task = new Task<TResult>(function, state, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x0014B834 File Offset: 0x0014AA34
		private string DebuggerDisplayResultDescription
		{
			get
			{
				if (!base.IsCompletedSuccessfully)
				{
					return SR.TaskT_DebuggerNoResult;
				}
				TResult result = this.m_result;
				return ((result != null) ? result.ToString() : null) ?? "";
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x0014B877 File Offset: 0x0014AA77
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate action = this.m_action;
				return ((action != null) ? action.Method.ToString() : null) ?? "{null}";
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x0014B89C File Offset: 0x0014AA9C
		internal bool TrySetResult(TResult result)
		{
			bool result2 = false;
			if (base.AtomicStateUpdate(67108864, 90177536))
			{
				this.m_result = result;
				Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 16777216);
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					base.NotifyParentIfPotentiallyAttachedTask();
					contingentProperties.SetCompleted();
				}
				base.FinishContinuations();
				result2 = true;
			}
			return result2;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0014B900 File Offset: 0x0014AB00
		internal void DangerousSetResult(TResult result)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (((contingentProperties != null) ? contingentProperties.m_parent : null) != null)
			{
				bool flag = this.TrySetResult(result);
				return;
			}
			this.m_result = result;
			this.m_stateFlags |= 16777216;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x0014B947 File Offset: 0x0014AB47
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TResult Result
		{
			get
			{
				if (!base.IsWaitNotificationEnabledOrNotRanToCompletion)
				{
					return this.m_result;
				}
				return this.GetResultCore(true);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x0014B95F File Offset: 0x0014AB5F
		internal TResult ResultOnSuccess
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x0014B968 File Offset: 0x0014AB68
		internal TResult GetResultCore(bool waitCompletionNotification)
		{
			if (!base.IsCompleted)
			{
				base.InternalWait(-1, default(CancellationToken));
			}
			if (waitCompletionNotification)
			{
				base.NotifyDebuggerOfWaitCompletionIfNecessary();
			}
			if (!base.IsCompletedSuccessfully)
			{
				base.ThrowIfExceptional(true);
			}
			return this.m_result;
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x0014B9AD File Offset: 0x0014ABAD
		public new static TaskFactory<TResult> Factory
		{
			get
			{
				return Task<TResult>.s_Factory;
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0014B9B4 File Offset: 0x0014ABB4
		internal override void InnerInvoke()
		{
			Func<TResult> func = this.m_action as Func<TResult>;
			if (func != null)
			{
				this.m_result = func();
				return;
			}
			Func<object, TResult> func2 = this.m_action as Func<object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0014B9FF File Offset: 0x0014ABFF
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public new TaskAwaiter<TResult> GetAwaiter()
		{
			return new TaskAwaiter<TResult>(this);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0014BA07 File Offset: 0x0014AC07
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public new ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable<TResult>(this, continueOnCapturedContext);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0014BA10 File Offset: 0x0014AC10
		public Task ContinueWith(Action<Task<TResult>> continuationAction)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0014BA33 File Offset: 0x0014AC33
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0014BA44 File Offset: 0x0014AC44
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0014BA64 File Offset: 0x0014AC64
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0014BA87 File Offset: 0x0014AC87
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0014BA94 File Offset: 0x0014AC94
		internal Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationAction);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, null, creationOptions, internalOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0014BAD8 File Offset: 0x0014ACD8
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			1,
			2
		})] Action<Task<TResult>, object> continuationAction, [Nullable(2)] object state)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0014BAFC File Offset: 0x0014ACFC
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			1,
			2
		})] Action<Task<TResult>, object> continuationAction, [Nullable(2)] object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0014BB10 File Offset: 0x0014AD10
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			1,
			2
		})] Action<Task<TResult>, object> continuationAction, [Nullable(2)] object state, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0014BB30 File Offset: 0x0014AD30
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			1,
			2
		})] Action<Task<TResult>, object> continuationAction, [Nullable(2)] object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0014BB54 File Offset: 0x0014AD54
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			1,
			2
		})] Action<Task<TResult>, object> continuationAction, [Nullable(2)] object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0014BB64 File Offset: 0x0014AD64
		internal Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationAction);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, state, creationOptions, internalOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0014BBA8 File Offset: 0x0014ADA8
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0014BBCB File Offset: 0x0014ADCB
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0014BBDC File Offset: 0x0014ADDC
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0014BBFC File Offset: 0x0014ADFC
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0014BC1F File Offset: 0x0014AE1F
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0014BC2C File Offset: 0x0014AE2C
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, null, creationOptions, internalOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0014BC70 File Offset: 0x0014AE70
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TNewResult> ContinueWith<TNewResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<Task<TResult>, object, TNewResult> continuationFunction, object state)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0014BC94 File Offset: 0x0014AE94
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TNewResult> ContinueWith<TNewResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0014BCA8 File Offset: 0x0014AEA8
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<Task<TResult>, object, TNewResult> continuationFunction, [Nullable(2)] object state, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0014BCC8 File Offset: 0x0014AEC8
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TNewResult> ContinueWith<TNewResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0014BCEC File Offset: 0x0014AEEC
		public Task<TNewResult> ContinueWith<[Nullable(2)] TNewResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<Task<TResult>, object, TNewResult> continuationFunction, [Nullable(2)] object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0014BCFC File Offset: 0x0014AEFC
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.continuationFunction);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, state, creationOptions, internalOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x04000B60 RID: 2912
		internal TResult m_result;

		// Token: 0x04000B61 RID: 2913
		private static readonly TaskFactory<TResult> s_Factory = new TaskFactory<TResult>();

		// Token: 0x020002F8 RID: 760
		internal static class TaskWhenAnyCast
		{
			// Token: 0x04000B62 RID: 2914
			internal static readonly Func<Task<Task>, Task<TResult>> Value = (Task<Task> completed) => (Task<TResult>)completed.Result;
		}
	}
}
