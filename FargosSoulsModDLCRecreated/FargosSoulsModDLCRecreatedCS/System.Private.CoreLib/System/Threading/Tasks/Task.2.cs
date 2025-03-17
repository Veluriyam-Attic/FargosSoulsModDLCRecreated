using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Internal.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200030D RID: 781
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskDebugView))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
	public class Task : IAsyncResult, IDisposable
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0014D5B5 File Offset: 0x0014C7B5
		[Nullable(2)]
		private Task ParentForDebugger
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties == null)
				{
					return null;
				}
				return contingentProperties.m_parent;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x0014D5C8 File Offset: 0x0014C7C8
		private int StateFlagsForDebugger
		{
			get
			{
				return this.m_stateFlags;
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0014D5D4 File Offset: 0x0014C7D4
		internal static bool AddToActiveTasks(Task task)
		{
			LazyInitializer.EnsureInitialized<Dictionary<int, Task>>(ref Task.s_currentActiveTasks, () => new Dictionary<int, Task>());
			int id = task.Id;
			Dictionary<int, Task> obj = Task.s_currentActiveTasks;
			lock (obj)
			{
				Task.s_currentActiveTasks[id] = task;
			}
			return true;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0014D64C File Offset: 0x0014C84C
		internal static void RemoveFromActiveTasks(Task task)
		{
			if (Task.s_currentActiveTasks == null)
			{
				return;
			}
			int id = task.Id;
			Dictionary<int, Task> obj = Task.s_currentActiveTasks;
			lock (obj)
			{
				Task.s_currentActiveTasks.Remove(id);
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x0014D6A0 File Offset: 0x0014C8A0
		internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
		{
			if (canceled)
			{
				this.m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
				this.m_contingentProperties = new Task.ContingentProperties
				{
					m_cancellationToken = ct,
					m_internalCancellationRequested = 1
				};
				return;
			}
			this.m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0014D6F2 File Offset: 0x0014C8F2
		internal Task()
		{
			this.m_stateFlags = 33555456;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0014D708 File Offset: 0x0014C908
		internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
		{
			if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.creationOptions);
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent != null)
				{
					this.EnsureContingentPropertiesInitializedUnsafe().m_parent = internalCurrent;
				}
			}
			this.TaskConstructorCore(null, state, default(CancellationToken), creationOptions, InternalTaskOptions.PromiseTask, null);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0014D75C File Offset: 0x0014C95C
		public Task(Action action) : this(action, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0014D77E File Offset: 0x0014C97E
		public Task(Action action, CancellationToken cancellationToken) : this(action, null, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0014D790 File Offset: 0x0014C990
		public Task(Action action, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0014D7B7 File Offset: 0x0014C9B7
		public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0014D7CC File Offset: 0x0014C9CC
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, object state) : this(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0014D7EE File Offset: 0x0014C9EE
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, object state, CancellationToken cancellationToken) : this(action, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0014D800 File Offset: 0x0014CA00
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, object state, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x0014D827 File Offset: 0x0014CA27
		[NullableContext(2)]
		public Task([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0014D840 File Offset: 0x0014CA40
		internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
			}
			if (parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.EnsureContingentPropertiesInitializedUnsafe().m_parent = parent;
			}
			this.TaskConstructorCore(action, state, cancellationToken, creationOptions, internalOptions, scheduler);
			this.CapturedContext = ExecutionContext.Capture();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0014D890 File Offset: 0x0014CA90
		internal void TaskConstructorCore(Delegate action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			this.m_action = action;
			this.m_stateObject = state;
			this.m_taskScheduler = scheduler;
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.creationOptions);
			}
			int num = (int)(creationOptions | (TaskCreationOptions)internalOptions);
			this.m_stateFlags = ((this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None) ? (num | 33554432) : num);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				Task parent = contingentProperties.m_parent;
				if (parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					parent.AddNewChild();
				}
			}
			if (cancellationToken.CanBeCanceled)
			{
				this.AssignCancellationToken(cancellationToken, null, null);
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0014D928 File Offset: 0x0014CB28
		private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitializedUnsafe();
			contingentProperties.m_cancellationToken = cancellationToken;
			try
			{
				if ((this.Options & (TaskCreationOptions)13312) == TaskCreationOptions.None)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						this.InternalCancel();
					}
					else
					{
						CancellationTokenRegistration value;
						if (antecedent == null)
						{
							value = cancellationToken.UnsafeRegister(delegate(object t)
							{
								((Task)t).InternalCancel();
							}, this);
						}
						else
						{
							value = cancellationToken.UnsafeRegister(delegate(object t)
							{
								Tuple<Task, Task, TaskContinuation> tuple = (Tuple<Task, Task, TaskContinuation>)t;
								Task item = tuple.Item1;
								Task item2 = tuple.Item2;
								item2.RemoveContinuation(tuple.Item3);
								item.InternalCancel();
							}, new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation));
						}
						contingentProperties.m_cancellationRegistration = new StrongBox<CancellationTokenRegistration>(value);
					}
				}
			}
			catch
			{
				Task.ContingentProperties contingentProperties2 = this.m_contingentProperties;
				Task task = (contingentProperties2 != null) ? contingentProperties2.m_parent : null;
				if (task != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (task.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					task.DisregardChild();
				}
				throw;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x0014B877 File Offset: 0x0014AA77
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate action = this.m_action;
				return ((action != null) ? action.Method.ToString() : null) ?? "{null}";
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x0014DA10 File Offset: 0x0014CC10
		internal TaskCreationOptions Options
		{
			get
			{
				return Task.OptionsMethod(this.m_stateFlags);
			}
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x0014DA1F File Offset: 0x0014CC1F
		internal static TaskCreationOptions OptionsMethod(int flags)
		{
			return (TaskCreationOptions)(flags & 65535);
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x0014DA28 File Offset: 0x0014CC28
		internal bool AtomicStateUpdate(int newBits, int illegalBits)
		{
			int stateFlags = this.m_stateFlags;
			return (stateFlags & illegalBits) == 0 && (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) == stateFlags || this.AtomicStateUpdateSlow(newBits, illegalBits));
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0014DA64 File Offset: 0x0014CC64
		private bool AtomicStateUpdateSlow(int newBits, int illegalBits)
		{
			int num = this.m_stateFlags;
			while ((num & illegalBits) == 0)
			{
				int num2 = Interlocked.CompareExchange(ref this.m_stateFlags, num | newBits, num);
				if (num2 == num)
				{
					return true;
				}
				num = num2;
			}
			return false;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0014DA9C File Offset: 0x0014CC9C
		internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
		{
			int num = oldFlags = this.m_stateFlags;
			while ((num & illegalBits) == 0)
			{
				oldFlags = Interlocked.CompareExchange(ref this.m_stateFlags, num | newBits, num);
				if (oldFlags == num)
				{
					return true;
				}
				num = oldFlags;
			}
			return false;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x0014DADC File Offset: 0x0014CCDC
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			if (enabled)
			{
				bool flag = this.AtomicStateUpdate(268435456, 90177536);
				return;
			}
			Interlocked.And(ref this.m_stateFlags, -268435457);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0014DB0F File Offset: 0x0014CD0F
		internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
		{
			if (this.IsWaitNotificationEnabled && this.ShouldNotifyDebuggerOfWaitCompletion)
			{
				this.NotifyDebuggerOfWaitCompletion();
				return true;
			}
			return false;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0014DB2C File Offset: 0x0014CD2C
		internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
		{
			foreach (Task task in tasks)
			{
				if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x0014DB63 File Offset: 0x0014CD63
		internal bool IsWaitNotificationEnabledOrNotRanToCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.m_stateFlags & 285212672) != 16777216;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x0014DB80 File Offset: 0x0014CD80
		internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
		{
			get
			{
				return this.IsWaitNotificationEnabled;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x0014DB95 File Offset: 0x0014CD95
		internal bool IsWaitNotificationEnabled
		{
			get
			{
				return (this.m_stateFlags & 268435456) != 0;
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0014DBA8 File Offset: 0x0014CDA8
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void NotifyDebuggerOfWaitCompletion()
		{
			this.SetNotificationForWaitCompletion(false);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0014DBB1 File Offset: 0x0014CDB1
		internal bool MarkStarted()
		{
			return this.AtomicStateUpdate(65536, 4259840);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0014DBC4 File Offset: 0x0014CDC4
		internal void FireTaskScheduledIfNeeded(TaskScheduler ts)
		{
			if ((this.m_stateFlags & 1073741824) == 0)
			{
				this.m_stateFlags |= 1073741824;
				Task internalCurrent = Task.InternalCurrent;
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				Task task = (contingentProperties != null) ? contingentProperties.m_parent : null;
				TplEventSource.Log.TaskScheduled(ts.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, this.Id, (task == null) ? 0 : task.Id, (int)this.Options, 1);
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0014DC48 File Offset: 0x0014CE48
		internal void AddNewChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized();
			if (contingentProperties.m_completionCountdown == 1)
			{
				contingentProperties.m_completionCountdown++;
				return;
			}
			Interlocked.Increment(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0014DC88 File Offset: 0x0014CE88
		internal void DisregardChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized();
			Interlocked.Decrement(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0014DCA8 File Offset: 0x0014CEA8
		public void Start()
		{
			this.Start(TaskScheduler.Current);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0014DCB8 File Offset: 0x0014CEB8
		public void Start(TaskScheduler scheduler)
		{
			int stateFlags = this.m_stateFlags;
			if (Task.IsCompletedMethod(stateFlags))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_Start_TaskCompleted);
			}
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_Start_Promise);
			}
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_Start_ContinuationTask);
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_Start_AlreadyStarted);
			}
			this.ScheduleAndStart(true);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0014DD2B File Offset: 0x0014CF2B
		public void RunSynchronously()
		{
			this.InternalRunSynchronously(TaskScheduler.Current, true);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0014DD39 File Offset: 0x0014CF39
		public void RunSynchronously(TaskScheduler scheduler)
		{
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			this.InternalRunSynchronously(scheduler, true);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0014DD50 File Offset: 0x0014CF50
		internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
		{
			int stateFlags = this.m_stateFlags;
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_RunSynchronously_Continuation);
			}
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_RunSynchronously_Promise);
			}
			if (Task.IsCompletedMethod(stateFlags))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_RunSynchronously_TaskCompleted);
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_RunSynchronously_AlreadyStarted);
			}
			if (this.MarkStarted())
			{
				bool flag = false;
				try
				{
					if (!scheduler.TryRunInline(this, false))
					{
						scheduler.InternalQueueTask(this);
						flag = true;
					}
					if (waitForCompletion && !this.IsCompleted)
					{
						this.SpinThenBlockingWait(-1, default(CancellationToken));
					}
					return;
				}
				catch (Exception innerException)
				{
					if (!flag)
					{
						TaskSchedulerException ex = new TaskSchedulerException(innerException);
						this.AddException(ex);
						this.Finish(false);
						this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
						throw ex;
					}
					throw;
				}
			}
			ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_RunSynchronously_TaskCompleted);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0014DE3C File Offset: 0x0014D03C
		internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions)
		{
			if (scheduler == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.scheduler);
			}
			Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0014DE74 File Offset: 0x0014D074
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref Task.s_taskIdCounter);
			}
			while (num == 0);
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.NewID(num);
			}
			return num;
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x0014DEA8 File Offset: 0x0014D0A8
		public int Id
		{
			get
			{
				if (this.m_taskId == 0)
				{
					int value = Task.NewId();
					Interlocked.CompareExchange(ref this.m_taskId, value, 0);
				}
				return this.m_taskId;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x0014DEDC File Offset: 0x0014D0DC
		public static int? CurrentId
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent != null)
				{
					return new int?(internalCurrent.Id);
				}
				return null;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x0014DF07 File Offset: 0x0014D107
		[Nullable(2)]
		internal static Task InternalCurrent
		{
			get
			{
				return Task.t_currentTask;
			}
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0014DF0E File Offset: 0x0014D10E
		internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
			{
				return null;
			}
			return Task.InternalCurrent;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0014DF1C File Offset: 0x0014D11C
		[Nullable(2)]
		public AggregateException Exception
		{
			[NullableContext(2)]
			get
			{
				AggregateException result = null;
				if (this.IsFaulted)
				{
					result = this.GetExceptions(false);
				}
				return result;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x0014DF3C File Offset: 0x0014D13C
		public TaskStatus Status
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				TaskStatus result;
				if ((stateFlags & 2097152) != 0)
				{
					result = TaskStatus.Faulted;
				}
				else if ((stateFlags & 4194304) != 0)
				{
					result = TaskStatus.Canceled;
				}
				else if ((stateFlags & 16777216) != 0)
				{
					result = TaskStatus.RanToCompletion;
				}
				else if ((stateFlags & 8388608) != 0)
				{
					result = TaskStatus.WaitingForChildrenToComplete;
				}
				else if ((stateFlags & 131072) != 0)
				{
					result = TaskStatus.Running;
				}
				else if ((stateFlags & 65536) != 0)
				{
					result = TaskStatus.WaitingToRun;
				}
				else if ((stateFlags & 33554432) != 0)
				{
					result = TaskStatus.WaitingForActivation;
				}
				else
				{
					result = TaskStatus.Created;
				}
				return result;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x0014DFB0 File Offset: 0x0014D1B0
		public bool IsCanceled
		{
			get
			{
				return (this.m_stateFlags & 6291456) == 4194304;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x0014DFC8 File Offset: 0x0014D1C8
		internal bool IsCancellationRequested
		{
			get
			{
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				return contingentProperties != null && (contingentProperties.m_internalCancellationRequested == 1 || contingentProperties.m_cancellationToken.IsCancellationRequested);
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0014DFFE File Offset: 0x0014D1FE
		internal Task.ContingentProperties EnsureContingentPropertiesInitialized()
		{
			return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, () => new Task.ContingentProperties());
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0014E02C File Offset: 0x0014D22C
		internal Task.ContingentProperties EnsureContingentPropertiesInitializedUnsafe()
		{
			Task.ContingentProperties result;
			if ((result = this.m_contingentProperties) == null)
			{
				result = (this.m_contingentProperties = new Task.ContingentProperties());
			}
			return result;
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x0014E054 File Offset: 0x0014D254
		internal CancellationToken CancellationToken
		{
			get
			{
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				if (contingentProperties != null)
				{
					return contingentProperties.m_cancellationToken;
				}
				return default(CancellationToken);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0014E080 File Offset: 0x0014D280
		internal bool IsCancellationAcknowledged
		{
			get
			{
				return (this.m_stateFlags & 1048576) != 0;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x0014E094 File Offset: 0x0014D294
		public bool IsCompleted
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				return Task.IsCompletedMethod(stateFlags);
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0014E0B0 File Offset: 0x0014D2B0
		private static bool IsCompletedMethod(int flags)
		{
			return (flags & 23068672) != 0;
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0014E0BC File Offset: 0x0014D2BC
		public bool IsCompletedSuccessfully
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x0014E0D3 File Offset: 0x0014D2D3
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.Options & (TaskCreationOptions)(-65281);
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0014E0E4 File Offset: 0x0014D2E4
		internal void SpinUntilCompleted()
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.IsCompleted)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x0014E10C File Offset: 0x0014D30C
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				bool flag = (this.m_stateFlags & 262144) != 0;
				if (flag)
				{
					ThrowHelper.ThrowObjectDisposedException(ExceptionResource.Task_ThrowIfDisposed);
				}
				return this.CompletedEvent.WaitHandle;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x0014E140 File Offset: 0x0014D340
		[Nullable(2)]
		public object AsyncState
		{
			[NullableContext(2)]
			get
			{
				return this.m_stateObject;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x0014E148 File Offset: 0x0014D348
		[Nullable(2)]
		internal TaskScheduler ExecutingTaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x0014E150 File Offset: 0x0014D350
		public static TaskFactory Factory { get; } = new TaskFactory();

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x0014E157 File Offset: 0x0014D357
		public static Task CompletedTask
		{
			get
			{
				return Task.s_cachedCompleted;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x0014E160 File Offset: 0x0014D360
		internal ManualResetEventSlim CompletedEvent
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized();
				if (contingentProperties.m_completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, null) != null)
					{
						manualResetEventSlim.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						manualResetEventSlim.Set();
					}
				}
				return contingentProperties.m_completionEvent;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x0014E1BC File Offset: 0x0014D3BC
		internal bool ExceptionRecorded
		{
			get
			{
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				return contingentProperties != null && contingentProperties.m_exceptionsHolder != null && contingentProperties.m_exceptionsHolder.ContainsFaultList;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x0014E1F1 File Offset: 0x0014D3F1
		public bool IsFaulted
		{
			get
			{
				return (this.m_stateFlags & 2097152) != 0;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x0014E204 File Offset: 0x0014D404
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x0014E238 File Offset: 0x0014D438
		[Nullable(2)]
		internal ExecutionContext CapturedContext
		{
			get
			{
				if ((this.m_stateFlags & 536870912) == 536870912)
				{
					return null;
				}
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return ((contingentProperties != null) ? contingentProperties.m_capturedContext : null) ?? ExecutionContext.Default;
			}
			set
			{
				if (value == null)
				{
					this.m_stateFlags |= 536870912;
					return;
				}
				if (value != ExecutionContext.Default)
				{
					this.EnsureContingentPropertiesInitializedUnsafe().m_capturedContext = value;
				}
			}
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0014E268 File Offset: 0x0014D468
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0014E278 File Offset: 0x0014D478
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((this.Options & (TaskCreationOptions)16384) != TaskCreationOptions.None)
				{
					return;
				}
				if (!this.IsCompleted)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.Task_Dispose_NotCompleted);
				}
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				if (contingentProperties != null)
				{
					ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
					if (completionEvent != null)
					{
						contingentProperties.m_completionEvent = null;
						if (!completionEvent.IsSet)
						{
							completionEvent.Set();
						}
						completionEvent.Dispose();
					}
				}
			}
			this.m_stateFlags |= 262144;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0014E2F4 File Offset: 0x0014D4F4
		internal void ScheduleAndStart(bool needsProtection)
		{
			if (needsProtection)
			{
				if (!this.MarkStarted())
				{
					return;
				}
			}
			else
			{
				this.m_stateFlags |= 65536;
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (TplEventSource.Log.IsEnabled() && (this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
			{
				TplEventSource.Log.TraceOperationBegin(this.Id, "Task: " + this.m_action.Method.Name, 0L);
			}
			try
			{
				this.m_taskScheduler.InternalQueueTask(this);
			}
			catch (Exception innerException)
			{
				TaskSchedulerException ex = new TaskSchedulerException(innerException);
				this.AddException(ex);
				this.Finish(false);
				if ((this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
				{
					this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
				}
				throw ex;
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0014E3D0 File Offset: 0x0014D5D0
		internal void AddException(object exceptionObject)
		{
			this.AddException(exceptionObject, false);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0014E3DC File Offset: 0x0014D5DC
		internal void AddException(object exceptionObject, bool representsCancellation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized();
			if (contingentProperties.m_exceptionsHolder == null)
			{
				TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
				if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, null) != null)
				{
					taskExceptionHolder.MarkAsHandled(false);
				}
			}
			Task.ContingentProperties obj = contingentProperties;
			lock (obj)
			{
				contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
			}
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0014E450 File Offset: 0x0014D650
		private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
		{
			Exception ex = null;
			if (includeTaskCanceledExceptions && this.IsCanceled)
			{
				ex = new TaskCanceledException(this);
				ex.SetCurrentStackTrace();
			}
			if (this.ExceptionRecorded)
			{
				return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, ex);
			}
			if (ex != null)
			{
				return new AggregateException(new Exception[]
				{
					ex
				});
			}
			return null;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0014E4A8 File Offset: 0x0014D6A8
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0014E4BC File Offset: 0x0014D6BC
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
			if (contingentProperties == null)
			{
				return null;
			}
			TaskExceptionHolder exceptionsHolder = contingentProperties.m_exceptionsHolder;
			if (exceptionsHolder == null)
			{
				return null;
			}
			return exceptionsHolder.GetCancellationExceptionDispatchInfo();
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0014E4E4 File Offset: 0x0014D6E4
		internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
		{
			Exception exceptions = this.GetExceptions(includeTaskCanceledExceptions);
			if (exceptions != null)
			{
				this.UpdateExceptionObservedStatus();
				throw exceptions;
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0014E504 File Offset: 0x0014D704
		internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
		{
			ExceptionDispatchInfo state2 = ExceptionDispatchInfo.Capture(exception);
			if (targetContext != null)
			{
				try
				{
					targetContext.Post(delegate(object state)
					{
						((ExceptionDispatchInfo)state).Throw();
					}, state2);
					return;
				}
				catch (Exception ex)
				{
					state2 = ExceptionDispatchInfo.Capture(new AggregateException(new Exception[]
					{
						exception,
						ex
					}));
				}
			}
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}, state2);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0014E598 File Offset: 0x0014D798
		internal void UpdateExceptionObservedStatus()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			Task task = (contingentProperties != null) ? contingentProperties.m_parent : null;
			if (task != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (task.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && Task.InternalCurrent == task)
			{
				this.m_stateFlags |= 524288;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x0014E5ED File Offset: 0x0014D7ED
		internal bool IsExceptionObservedByParent
		{
			get
			{
				return (this.m_stateFlags & 524288) != 0;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x0014E600 File Offset: 0x0014D800
		internal bool IsDelegateInvoked
		{
			get
			{
				return (this.m_stateFlags & 131072) != 0;
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0014E613 File Offset: 0x0014D813
		internal void Finish(bool userDelegateExecute)
		{
			if (this.m_contingentProperties == null)
			{
				this.FinishStageTwo();
				return;
			}
			this.FinishSlow(userDelegateExecute);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0014E62C File Offset: 0x0014D82C
		private void FinishSlow(bool userDelegateExecute)
		{
			if (!userDelegateExecute)
			{
				this.FinishStageTwo();
				return;
			}
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties.m_completionCountdown == 1 || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
			else
			{
				this.AtomicStateUpdate(8388608, 23068672);
			}
			List<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
			if (exceptionalChildren != null)
			{
				List<Task> obj = exceptionalChildren;
				lock (obj)
				{
					exceptionalChildren.RemoveAll((Task t) => t.IsExceptionObservedByParent);
				}
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0014E6D8 File Offset: 0x0014D8D8
		private void FinishStageTwo()
		{
			Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
			if (contingentProperties != null)
			{
				this.AddExceptionsFromChildren(contingentProperties);
			}
			int num;
			if (this.ExceptionRecorded)
			{
				num = 2097152;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(this.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this);
				}
			}
			else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
			{
				num = 4194304;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(this.Id, AsyncCausalityStatus.Canceled);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this);
				}
			}
			else
			{
				num = 16777216;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(this.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this);
				}
			}
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
			contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			this.FinishStageThree();
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0014E7E0 File Offset: 0x0014D9E0
		internal void FinishStageThree()
		{
			this.m_action = null;
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.m_capturedContext = null;
				this.NotifyParentIfPotentiallyAttachedTask();
			}
			this.FinishContinuations();
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0014E814 File Offset: 0x0014DA14
		internal void NotifyParentIfPotentiallyAttachedTask()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			Task task = (contingentProperties != null) ? contingentProperties.m_parent : null;
			if (task != null && (task.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & 65535 & 4) != 0)
			{
				task.ProcessChildCompletion(this);
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0014E85C File Offset: 0x0014DA5C
		internal void ProcessChildCompletion(Task childTask)
		{
			Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
			if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
			{
				if (contingentProperties.m_exceptionalChildren == null)
				{
					Interlocked.CompareExchange<List<Task>>(ref contingentProperties.m_exceptionalChildren, new List<Task>(), null);
				}
				List<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
				if (exceptionalChildren != null)
				{
					List<Task> obj = exceptionalChildren;
					lock (obj)
					{
						exceptionalChildren.Add(childTask);
					}
				}
			}
			if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0014E8F0 File Offset: 0x0014DAF0
		internal void AddExceptionsFromChildren(Task.ContingentProperties props)
		{
			List<Task> exceptionalChildren = props.m_exceptionalChildren;
			if (exceptionalChildren != null)
			{
				List<Task> obj = exceptionalChildren;
				lock (obj)
				{
					foreach (Task task in exceptionalChildren)
					{
						if (task.IsFaulted && !task.IsExceptionObservedByParent)
						{
							TaskExceptionHolder exceptionsHolder = Volatile.Read<Task.ContingentProperties>(ref task.m_contingentProperties).m_exceptionsHolder;
							this.AddException(exceptionsHolder.CreateExceptionObject(false, null));
						}
					}
				}
				props.m_exceptionalChildren = null;
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0014E9A8 File Offset: 0x0014DBA8
		internal bool ExecuteEntry()
		{
			int num = 0;
			if (!this.AtomicStateUpdate(131072, 23199744, ref num) && (num & 4194304) == 0)
			{
				return false;
			}
			if (!this.IsCancellationRequested & !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask, null);
			}
			else
			{
				this.ExecuteEntryCancellationRequestedOrCanceled();
			}
			return true;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0014EA00 File Offset: 0x0014DC00
		internal virtual void ExecuteFromThreadPool(Thread threadPoolThread)
		{
			this.ExecuteEntryUnsafe(threadPoolThread);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0014EA09 File Offset: 0x0014DC09
		internal void ExecuteEntryUnsafe(Thread threadPoolThread)
		{
			this.m_stateFlags |= 131072;
			if (!this.IsCancellationRequested & !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask, threadPoolThread);
				return;
			}
			this.ExecuteEntryCancellationRequestedOrCanceled();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0014EA4C File Offset: 0x0014DC4C
		internal void ExecuteEntryCancellationRequestedOrCanceled()
		{
			if (!this.IsCanceled)
			{
				int num = Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
				if ((num & 4194304) == 0)
				{
					this.CancellationCleanupLogic();
				}
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0014EA8C File Offset: 0x0014DC8C
		private void ExecuteWithThreadLocal(ref Task currentTaskSlot, Thread threadPoolThread = null)
		{
			Task task = currentTaskSlot;
			TplEventSource log = TplEventSource.Log;
			Guid currentThreadActivityId = default(Guid);
			bool flag = log.IsEnabled();
			if (flag)
			{
				if (log.TasksSetActivityIds)
				{
					EventSource.SetCurrentThreadActivityId(TplEventSource.CreateGuidForTaskID(this.Id), out currentThreadActivityId);
				}
				if (task != null)
				{
					log.TaskStarted(task.m_taskScheduler.Id, task.Id, this.Id);
				}
				else
				{
					log.TaskStarted(TaskScheduler.Current.Id, 0, this.Id);
				}
			}
			bool flag2 = TplEventSource.Log.IsEnabled();
			if (flag2)
			{
				TplEventSource.Log.TraceSynchronousWorkBegin(this.Id, CausalitySynchronousWork.Execution);
			}
			try
			{
				currentTaskSlot = this;
				try
				{
					ExecutionContext capturedContext = this.CapturedContext;
					if (capturedContext == null)
					{
						this.InnerInvoke();
					}
					else if (threadPoolThread == null)
					{
						ExecutionContext.RunInternal(capturedContext, Task.s_ecCallback, this);
					}
					else
					{
						ExecutionContext.RunFromThreadPoolDispatchLoop(threadPoolThread, capturedContext, Task.s_ecCallback, this);
					}
				}
				catch (Exception unhandledException)
				{
					this.HandleException(unhandledException);
				}
				if (flag2)
				{
					TplEventSource.Log.TraceSynchronousWorkEnd(CausalitySynchronousWork.Execution);
				}
				this.Finish(true);
			}
			finally
			{
				currentTaskSlot = task;
				if (flag)
				{
					if (task != null)
					{
						log.TaskCompleted(task.m_taskScheduler.Id, task.Id, this.Id, this.IsFaulted);
					}
					else
					{
						log.TaskCompleted(TaskScheduler.Current.Id, 0, this.Id, this.IsFaulted);
					}
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
				}
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0014EBFC File Offset: 0x0014DDFC
		internal virtual void InnerInvoke()
		{
			Action action = this.m_action as Action;
			if (action != null)
			{
				action();
				return;
			}
			Action<object> action2 = this.m_action as Action<object>;
			if (action2 != null)
			{
				action2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0014EC3C File Offset: 0x0014DE3C
		private void HandleException(Exception unhandledException)
		{
			OperationCanceledException ex = unhandledException as OperationCanceledException;
			if (ex != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == ex.CancellationToken)
			{
				this.SetCancellationAcknowledged();
				this.AddException(ex, true);
				return;
			}
			this.AddException(unhandledException);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0014EC89 File Offset: 0x0014DE89
		public TaskAwaiter GetAwaiter()
		{
			return new TaskAwaiter(this);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0014EC91 File Offset: 0x0014DE91
		public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0014EC9C File Offset: 0x0014DE9C
		internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			TaskContinuation taskContinuation = null;
			if (continueOnCapturedContext)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					taskContinuation = new SynchronizationContextAwaitTaskContinuation(synchronizationContext, continuationAction, flowExecutionContext);
				}
				else
				{
					TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
					if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
					{
						taskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext);
					}
				}
			}
			if (taskContinuation == null && flowExecutionContext)
			{
				taskContinuation = new AwaitTaskContinuation(continuationAction, true);
			}
			if (taskContinuation != null)
			{
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, false);
					return;
				}
			}
			else if (!this.AddTaskContinuation(continuationAction, false))
			{
				AwaitTaskContinuation.UnsafeScheduleAction(continuationAction, this);
			}
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0014ED2C File Offset: 0x0014DF2C
		internal void UnsafeSetContinuationForAwait(IAsyncStateMachineBox stateMachineBox, bool continueOnCapturedContext)
		{
			if (continueOnCapturedContext)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = new SynchronizationContextAwaitTaskContinuation(synchronizationContext, stateMachineBox.MoveNextAction, false);
					if (!this.AddTaskContinuation(synchronizationContextAwaitTaskContinuation, false))
					{
						synchronizationContextAwaitTaskContinuation.Run(this, false);
					}
					return;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
				{
					TaskSchedulerAwaitTaskContinuation taskSchedulerAwaitTaskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, stateMachineBox.MoveNextAction, false);
					if (!this.AddTaskContinuation(taskSchedulerAwaitTaskContinuation, false))
					{
						taskSchedulerAwaitTaskContinuation.Run(this, false);
					}
					return;
				}
			}
			if (!this.AddTaskContinuation(stateMachineBox, false))
			{
				ThreadPool.UnsafeQueueUserWorkItemInternal(stateMachineBox, true);
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x0014EDC0 File Offset: 0x0014DFC0
		public static YieldAwaitable Yield()
		{
			return default(YieldAwaitable);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x0014EDD8 File Offset: 0x0014DFD8
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0014EDF8 File Offset: 0x0014DFF8
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.timeout);
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0014EE34 File Offset: 0x0014E034
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0014EE40 File Offset: 0x0014E040
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0014EE60 File Offset: 0x0014E060
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.millisecondsTimeout);
			}
			if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				return true;
			}
			if (!this.InternalWait(millisecondsTimeout, cancellationToken))
			{
				return false;
			}
			if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				this.NotifyDebuggerOfWaitCompletionIfNecessary();
				if (this.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				this.ThrowIfExceptional(true);
			}
			return true;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0014EEB4 File Offset: 0x0014E0B4
		private bool WrappedTryRunInline()
		{
			if (this.m_taskScheduler == null)
			{
				return false;
			}
			bool result;
			try
			{
				result = this.m_taskScheduler.TryRunInline(this, true);
			}
			catch (Exception innerException)
			{
				throw new TaskSchedulerException(innerException);
			}
			return result;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x0014EEF8 File Offset: 0x0014E0F8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return this.InternalWaitCore(millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x0014EF04 File Offset: 0x0014E104
		private bool InternalWaitCore(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = this.IsCompleted;
			if (flag)
			{
				return true;
			}
			TplEventSource log = TplEventSource.Log;
			bool flag2 = log.IsEnabled();
			if (flag2)
			{
				Task internalCurrent = Task.InternalCurrent;
				log.TaskWaitBegin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.Id, TplEventSource.TaskWaitBehavior.Synchronous, 0);
			}
			Debugger.NotifyOfCrossThreadDependency();
			flag = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken));
			if (flag2)
			{
				Task internalCurrent2 = Task.InternalCurrent;
				if (internalCurrent2 != null)
				{
					log.TaskWaitEnd(internalCurrent2.m_taskScheduler.Id, internalCurrent2.Id, this.Id);
				}
				else
				{
					log.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
				}
				log.TaskWaitContinuationComplete(this.Id);
			}
			return flag;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x0014EFEC File Offset: 0x0014E1EC
		private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = millisecondsTimeout == -1;
			uint num = (uint)(flag ? 0 : Environment.TickCount);
			bool flag2 = this.SpinWait(millisecondsTimeout);
			if (!flag2)
			{
				Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
				try
				{
					this.AddCompletionAction(setOnInvokeMres, true);
					if (flag)
					{
						flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
					}
					else
					{
						uint num2 = (uint)(Environment.TickCount - (int)num);
						if ((ulong)num2 < (ulong)((long)millisecondsTimeout))
						{
							flag2 = setOnInvokeMres.Wait((int)((long)millisecondsTimeout - (long)((ulong)num2)), cancellationToken);
						}
					}
				}
				finally
				{
					if (!this.IsCompleted)
					{
						this.RemoveContinuation(setOnInvokeMres);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x0014F074 File Offset: 0x0014E274
		private bool SpinWait(int millisecondsTimeout)
		{
			if (this.IsCompleted)
			{
				return true;
			}
			if (millisecondsTimeout == 0)
			{
				return false;
			}
			int spinCountforSpinBeforeWait = System.Threading.SpinWait.SpinCountforSpinBeforeWait;
			SpinWait spinWait = default(SpinWait);
			while (spinWait.Count < spinCountforSpinBeforeWait)
			{
				spinWait.SpinOnce(-1);
				if (this.IsCompleted)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x0014F0C0 File Offset: 0x0014E2C0
		internal void InternalCancel()
		{
			TaskSchedulerException ex = null;
			bool flag = false;
			if ((this.m_stateFlags & 65536) != 0)
			{
				TaskScheduler taskScheduler = this.m_taskScheduler;
				try
				{
					flag = (taskScheduler != null && taskScheduler.TryDequeue(this));
				}
				catch (Exception innerException)
				{
					ex = new TaskSchedulerException(innerException);
				}
			}
			this.RecordInternalCancellationRequest();
			bool flag2 = false;
			if (flag)
			{
				flag2 = this.AtomicStateUpdate(4194304, 4325376);
			}
			else if ((this.m_stateFlags & 65536) == 0)
			{
				flag2 = this.AtomicStateUpdate(4194304, 23265280);
			}
			if (flag2)
			{
				this.CancellationCleanupLogic();
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x0014F164 File Offset: 0x0014E364
		internal void InternalCancelContinueWithInitialState()
		{
			this.m_stateFlags |= 4194304;
			this.CancellationCleanupLogic();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0014F182 File Offset: 0x0014E382
		internal void RecordInternalCancellationRequest()
		{
			this.EnsureContingentPropertiesInitialized().m_internalCancellationRequested = 1;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x0014F194 File Offset: 0x0014E394
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
		{
			this.RecordInternalCancellationRequest();
			if (tokenToRecord != default(CancellationToken))
			{
				this.m_contingentProperties.m_cancellationToken = tokenToRecord;
			}
			if (cancellationException != null)
			{
				this.AddException(cancellationException, true);
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0014F1D0 File Offset: 0x0014E3D0
		internal void CancellationCleanupLogic()
		{
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
			Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationEnd(this.Id, AsyncCausalityStatus.Canceled);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.RemoveFromActiveTasks(this);
			}
			this.FinishStageThree();
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0014F242 File Offset: 0x0014E442
		private void SetCancellationAcknowledged()
		{
			this.m_stateFlags |= 1048576;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0014F25C File Offset: 0x0014E45C
		internal bool TrySetResult()
		{
			if (this.AtomicStateUpdate(83886080, 90177536))
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					this.NotifyParentIfPotentiallyAttachedTask();
					contingentProperties.SetCompleted();
				}
				this.FinishContinuations();
				return true;
			}
			return false;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0014F29C File Offset: 0x0014E49C
		internal bool TrySetException(object exceptionObject)
		{
			bool result = false;
			this.EnsureContingentPropertiesInitialized();
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.AddException(exceptionObject);
				this.Finish(false);
				result = true;
			}
			return result;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0014F2D5 File Offset: 0x0014E4D5
		internal bool TrySetCanceled(CancellationToken tokenToRecord)
		{
			return this.TrySetCanceled(tokenToRecord, null);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0014F2E0 File Offset: 0x0014E4E0
		internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
		{
			bool result = false;
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
				this.CancellationCleanupLogic();
				result = true;
			}
			return result;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0014F314 File Offset: 0x0014E514
		internal void FinishContinuations()
		{
			object obj = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
			if (obj != null)
			{
				this.RunContinuations(obj);
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0014F33C File Offset: 0x0014E53C
		private void RunContinuations(object continuationObject)
		{
			TplEventSource tplEventSource = TplEventSource.Log;
			if (!tplEventSource.IsEnabled())
			{
				tplEventSource = null;
			}
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceSynchronousWorkBegin(this.Id, CausalitySynchronousWork.CompletionNotification);
			}
			bool flag = (this.m_stateFlags & 64) == 0 && RuntimeHelpers.TryEnsureSufficientExecutionStack();
			IAsyncStateMachineBox asyncStateMachineBox = continuationObject as IAsyncStateMachineBox;
			if (asyncStateMachineBox != null)
			{
				AwaitTaskContinuation.RunOrScheduleAction(asyncStateMachineBox, flag);
				Task.LogFinishCompletionNotification();
				return;
			}
			Action action = continuationObject as Action;
			if (action != null)
			{
				AwaitTaskContinuation.RunOrScheduleAction(action, flag);
				Task.LogFinishCompletionNotification();
				return;
			}
			TaskContinuation taskContinuation = continuationObject as TaskContinuation;
			if (taskContinuation != null)
			{
				taskContinuation.Run(this, flag);
				Task.LogFinishCompletionNotification();
				return;
			}
			ITaskCompletionAction taskCompletionAction = continuationObject as ITaskCompletionAction;
			if (taskCompletionAction == null)
			{
				List<object> list = (List<object>)continuationObject;
				List<object> obj = list;
				lock (obj)
				{
				}
				int count = list.Count;
				if (flag)
				{
					bool flag3 = false;
					for (int i = 0; i < count; i++)
					{
						object obj2 = list[i];
						if (obj2 != null)
						{
							ContinueWithTaskContinuation continueWithTaskContinuation = obj2 as ContinueWithTaskContinuation;
							if (continueWithTaskContinuation != null)
							{
								if ((continueWithTaskContinuation.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
								{
									list[i] = null;
									if (tplEventSource != null)
									{
										tplEventSource.RunningContinuationList(this.Id, i, continueWithTaskContinuation);
									}
									continueWithTaskContinuation.Run(this, false);
								}
							}
							else if (!(obj2 is ITaskCompletionAction))
							{
								if (flag3)
								{
									list[i] = null;
									if (tplEventSource != null)
									{
										tplEventSource.RunningContinuationList(this.Id, i, obj2);
									}
									IAsyncStateMachineBox asyncStateMachineBox2 = obj2 as IAsyncStateMachineBox;
									if (asyncStateMachineBox2 == null)
									{
										Action action2 = obj2 as Action;
										if (action2 == null)
										{
											((TaskContinuation)obj2).Run(this, false);
										}
										else
										{
											AwaitTaskContinuation.RunOrScheduleAction(action2, false);
										}
									}
									else
									{
										AwaitTaskContinuation.RunOrScheduleAction(asyncStateMachineBox2, false);
									}
								}
								flag3 = true;
							}
						}
					}
				}
				for (int j = 0; j < count; j++)
				{
					object obj3 = list[j];
					if (obj3 != null)
					{
						list[j] = null;
						if (tplEventSource != null)
						{
							tplEventSource.RunningContinuationList(this.Id, j, obj3);
						}
						IAsyncStateMachineBox asyncStateMachineBox3 = obj3 as IAsyncStateMachineBox;
						if (asyncStateMachineBox3 == null)
						{
							Action action3 = obj3 as Action;
							if (action3 == null)
							{
								TaskContinuation taskContinuation2 = obj3 as TaskContinuation;
								if (taskContinuation2 == null)
								{
									this.RunOrQueueCompletionAction((ITaskCompletionAction)obj3, flag);
								}
								else
								{
									taskContinuation2.Run(this, flag);
								}
							}
							else
							{
								AwaitTaskContinuation.RunOrScheduleAction(action3, flag);
							}
						}
						else
						{
							AwaitTaskContinuation.RunOrScheduleAction(asyncStateMachineBox3, flag);
						}
					}
				}
				Task.LogFinishCompletionNotification();
				return;
			}
			this.RunOrQueueCompletionAction(taskCompletionAction, flag);
			Task.LogFinishCompletionNotification();
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0014F5B0 File Offset: 0x0014E7B0
		private void RunOrQueueCompletionAction(ITaskCompletionAction completionAction, bool allowInlining)
		{
			if (allowInlining || !completionAction.InvokeMayRunArbitraryCode)
			{
				completionAction.Invoke(this);
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItemInternal(new CompletionActionInvoker(completionAction, this), true);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0014F5D2 File Offset: 0x0014E7D2
		private static void LogFinishCompletionNotification()
		{
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceSynchronousWorkEnd(CausalitySynchronousWork.CompletionNotification);
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0014F5EC File Offset: 0x0014E7EC
		public Task ContinueWith(Action<Task> continuationAction)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0014F60F File Offset: 0x0014E80F
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0014F620 File Offset: 0x0014E820
		public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0014F640 File Offset: 0x0014E840
		public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0014F663 File Offset: 0x0014E863
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0014F670 File Offset: 0x0014E870
		private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
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
			Task task = new ContinuationTaskFromTask(this, continuationAction, null, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x0014F6B4 File Offset: 0x0014E8B4
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<Task, object> continuationAction, [Nullable(2)] object state)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0014F6D8 File Offset: 0x0014E8D8
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<Task, object> continuationAction, [Nullable(2)] object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x0014F6EC File Offset: 0x0014E8EC
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<Task, object> continuationAction, [Nullable(2)] object state, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0014F70C File Offset: 0x0014E90C
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<Task, object> continuationAction, [Nullable(2)] object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0014F730 File Offset: 0x0014E930
		public Task ContinueWith([Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<Task, object> continuationAction, [Nullable(2)] object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0014F740 File Offset: 0x0014E940
		private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
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
			Task task = new ContinuationTaskFromTask(this, continuationAction, state, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0014F784 File Offset: 0x0014E984
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>(Func<Task, TResult> continuationFunction)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0014F7A7 File Offset: 0x0014E9A7
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0014F7B8 File Offset: 0x0014E9B8
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0014F7D8 File Offset: 0x0014E9D8
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x0014F7FB File Offset: 0x0014E9FB
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0014F808 File Offset: 0x0014EA08
		private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
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
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, null, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0014F84C File Offset: 0x0014EA4C
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> ContinueWith<TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<Task, object, TResult> continuationFunction, object state)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0014F870 File Offset: 0x0014EA70
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> ContinueWith<TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0014F884 File Offset: 0x0014EA84
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<Task, object, TResult> continuationFunction, [Nullable(2)] object state, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0014F8A4 File Offset: 0x0014EAA4
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> ContinueWith<TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0014F8C8 File Offset: 0x0014EAC8
		public Task<TResult> ContinueWith<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<Task, object, TResult> continuationFunction, [Nullable(2)] object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0014F8D8 File Offset: 0x0014EAD8
		private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
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
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, state, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0014F91C File Offset: 0x0014EB1C
		internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.continuationOptions, ExceptionResource.Task_ContinueWith_ESandLR);
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.RunContinuationsAsynchronously | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.continuationOptions);
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) == (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.continuationOptions, ExceptionResource.Task_ContinueWith_NotOnAnything);
			}
			creationOptions = (TaskCreationOptions)(continuationOptions & (TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously));
			internalOptions = (((continuationOptions & TaskContinuationOptions.LazyCancellation) != TaskContinuationOptions.None) ? (InternalTaskOptions.ContinuationTask | InternalTaskOptions.LazyCancellation) : InternalTaskOptions.ContinuationTask);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0014F984 File Offset: 0x0014EB84
		internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			TaskContinuation taskContinuation = new ContinueWithTaskContinuation(continuationTask, options, scheduler);
			if (cancellationToken.CanBeCanceled)
			{
				if (this.IsCompleted || cancellationToken.IsCancellationRequested)
				{
					continuationTask.AssignCancellationToken(cancellationToken, null, null);
				}
				else
				{
					continuationTask.AssignCancellationToken(cancellationToken, this, taskContinuation);
				}
			}
			if (!continuationTask.IsCompleted)
			{
				if ((this.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None && !(this is ITaskCompletionAction))
				{
					TplEventSource log = TplEventSource.Log;
					if (log.IsEnabled())
					{
						log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), continuationTask.Id);
					}
				}
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, true);
				}
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0014FA2C File Offset: 0x0014EC2C
		internal void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers = false)
		{
			if (!this.AddTaskContinuation(action, addBeforeOthers))
			{
				action.Invoke(this);
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0014FA40 File Offset: 0x0014EC40
		private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
		{
			object continuationObject = this.m_continuationObject;
			if (continuationObject != Task.s_taskCompletionSentinel && !(continuationObject is List<object>))
			{
				Interlocked.CompareExchange(ref this.m_continuationObject, new List<object>
				{
					continuationObject
				}, continuationObject);
			}
			List<object> list = this.m_continuationObject as List<object>;
			if (list != null)
			{
				List<object> obj = list;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						if (list.Count == list.Capacity)
						{
							list.RemoveAll((object l) => l == null);
						}
						if (addBeforeOthers)
						{
							list.Insert(0, tc);
						}
						else
						{
							list.Add(tc);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0014FB18 File Offset: 0x0014ED18
		private bool AddTaskContinuation(object tc, bool addBeforeOthers)
		{
			return !this.IsCompleted && ((this.m_continuationObject == null && Interlocked.CompareExchange(ref this.m_continuationObject, tc, null) == null) || this.AddTaskContinuationComplex(tc, addBeforeOthers));
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0014FB48 File Offset: 0x0014ED48
		internal void RemoveContinuation(object continuationObject)
		{
			object continuationObject2 = this.m_continuationObject;
			if (continuationObject2 == Task.s_taskCompletionSentinel)
			{
				return;
			}
			List<object> list = continuationObject2 as List<object>;
			if (list == null)
			{
				if (Interlocked.CompareExchange(ref this.m_continuationObject, new List<object>(), continuationObject) == continuationObject)
				{
					return;
				}
				list = (this.m_continuationObject as List<object>);
			}
			if (list != null)
			{
				List<object> obj = list;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						int num = list.IndexOf(continuationObject);
						if (num != -1)
						{
							list[num] = null;
						}
					}
				}
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0014FBEC File Offset: 0x0014EDEC
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(params Task[] tasks)
		{
			Task.WaitAllCore(tasks, -1, default(CancellationToken));
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0014FC0C File Offset: 0x0014EE0C
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.timeout);
			}
			return Task.WaitAllCore(tasks, (int)num, default(CancellationToken));
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0014FC48 File Offset: 0x0014EE48
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAllCore(tasks, millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0014FC65 File Offset: 0x0014EE65
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
		{
			Task.WaitAllCore(tasks, -1, cancellationToken);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0014FC70 File Offset: 0x0014EE70
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return Task.WaitAllCore(tasks, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0014FC7C File Offset: 0x0014EE7C
		private static bool WaitAllCore(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (millisecondsTimeout < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.millisecondsTimeout);
			}
			cancellationToken.ThrowIfCancellationRequested();
			List<Exception> exceptions = null;
			List<Task> list = null;
			List<Task> list2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				Task task = tasks[i];
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_WaitMulti_NullTask, ExceptionArgument.tasks);
				}
				bool flag4 = task.IsCompleted;
				if (!flag4)
				{
					if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
					{
						Task.AddToList<Task>(task, ref list, tasks.Length);
					}
					else
					{
						flag4 = (task.WrappedTryRunInline() && task.IsCompleted);
						if (!flag4)
						{
							Task.AddToList<Task>(task, ref list, tasks.Length);
						}
					}
				}
				if (flag4)
				{
					if (task.IsFaulted)
					{
						flag = true;
					}
					else if (task.IsCanceled)
					{
						flag2 = true;
					}
					if (task.IsWaitNotificationEnabled)
					{
						Task.AddToList<Task>(task, ref list2, 1);
					}
				}
			}
			if (list != null)
			{
				flag3 = Task.WaitAllBlockingCore(list, millisecondsTimeout, cancellationToken);
				if (flag3)
				{
					foreach (Task task2 in list)
					{
						if (task2.IsFaulted)
						{
							flag = true;
						}
						else if (task2.IsCanceled)
						{
							flag2 = true;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							Task.AddToList<Task>(task2, ref list2, 1);
						}
					}
				}
				GC.KeepAlive(tasks);
			}
			if (flag3 && list2 != null)
			{
				foreach (Task task3 in list2)
				{
					if (task3.NotifyDebuggerOfWaitCompletionIfNecessary())
					{
						break;
					}
				}
			}
			if (flag3 && (flag || flag2))
			{
				if (!flag)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				foreach (Task t in tasks)
				{
					Task.AddExceptionsForCompletedTask(ref exceptions, t);
				}
				ThrowHelper.ThrowAggregateException(exceptions);
			}
			return flag3;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0014FE68 File Offset: 0x0014F068
		private static void AddToList<T>(T item, ref List<T> list, int initSize)
		{
			if (list == null)
			{
				list = new List<T>(initSize);
			}
			list.Add(item);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0014FE80 File Offset: 0x0014F080
		private static bool WaitAllBlockingCore(List<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = false;
			Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
			try
			{
				foreach (Task task in tasks)
				{
					task.AddCompletionAction(setOnCountdownMres, true);
				}
				flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
			}
			finally
			{
				if (!flag)
				{
					foreach (Task task2 in tasks)
					{
						if (!task2.IsCompleted)
						{
							task2.RemoveContinuation(setOnCountdownMres);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0014FF44 File Offset: 0x0014F144
		internal static void AddExceptionsForCompletedTask(ref List<Exception> exceptions, Task t)
		{
			AggregateException exceptions2 = t.GetExceptions(true);
			if (exceptions2 != null)
			{
				t.UpdateExceptionObservedStatus();
				if (exceptions == null)
				{
					exceptions = new List<Exception>(exceptions2.InnerExceptions.Count);
				}
				exceptions.AddRange(exceptions2.InnerExceptions);
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0014FF88 File Offset: 0x0014F188
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(params Task[] tasks)
		{
			return Task.WaitAnyCore(tasks, -1, default(CancellationToken));
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0014FFA8 File Offset: 0x0014F1A8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.timeout);
			}
			return Task.WaitAnyCore(tasks, (int)num, default(CancellationToken));
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0014FFE4 File Offset: 0x0014F1E4
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
		{
			return Task.WaitAnyCore(tasks, -1, cancellationToken);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x0014FFF0 File Offset: 0x0014F1F0
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAnyCore(tasks, millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0015000D File Offset: 0x0014F20D
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return Task.WaitAnyCore(tasks, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00150018 File Offset: 0x0014F218
		private static int WaitAnyCore(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (millisecondsTimeout < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.millisecondsTimeout);
			}
			cancellationToken.ThrowIfCancellationRequested();
			int num = -1;
			for (int i = 0; i < tasks.Length; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_WaitMulti_NullTask, ExceptionArgument.tasks);
				}
				if (num == -1 && task.IsCompleted)
				{
					num = i;
				}
			}
			if (num == -1 && tasks.Length != 0)
			{
				Task<Task> task2 = TaskFactory.CommonCWAnyLogic(tasks, true);
				bool flag = task2.Wait(millisecondsTimeout, cancellationToken);
				if (flag)
				{
					num = Array.IndexOf<Task>(tasks, task2.Result);
				}
				else
				{
					TaskFactory.CommonCWAnyLogicCleanup(task2);
				}
			}
			GC.KeepAlive(tasks);
			return num;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x001500A9 File Offset: 0x0014F2A9
		public static Task<TResult> FromResult<[Nullable(2)] TResult>(TResult result)
		{
			return new Task<TResult>(result);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x001500B4 File Offset: 0x0014F2B4
		public static Task FromException(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			Task task = new Task();
			bool flag = task.TrySetException(exception);
			return task;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x001500DC File Offset: 0x0014F2DC
		public static Task<TResult> FromException<[Nullable(2)] TResult>(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			Task<TResult> task = new Task<TResult>();
			bool flag = task.TrySetException(exception);
			return task;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00150102 File Offset: 0x0014F302
		public static Task FromCanceled(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.cancellationToken);
			}
			return new Task(true, TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0015011C File Offset: 0x0014F31C
		public static Task<TResult> FromCanceled<[Nullable(2)] TResult>(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.cancellationToken);
			}
			return new Task<TResult>(true, default(TResult), TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0015014C File Offset: 0x0014F34C
		internal static Task FromCanceled(OperationCanceledException exception)
		{
			Task task = new Task();
			bool flag = task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00150170 File Offset: 0x0014F370
		internal static Task<TResult> FromCanceled<TResult>(OperationCanceledException exception)
		{
			Task<TResult> task = new Task<TResult>();
			bool flag = task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00150194 File Offset: 0x0014F394
		public static Task Run(Action action)
		{
			return Task.InternalStartNew(null, action, null, default(CancellationToken), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x001501B9 File Offset: 0x0014F3B9
		public static Task Run(Action action, CancellationToken cancellationToken)
		{
			return Task.InternalStartNew(null, action, null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x001501CC File Offset: 0x0014F3CC
		public static Task<TResult> Run<[Nullable(2)] TResult>(Func<TResult> function)
		{
			return Task<TResult>.StartNew(null, function, default(CancellationToken), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x001501F0 File Offset: 0x0014F3F0
		public static Task<TResult> Run<[Nullable(2)] TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			return Task<TResult>.StartNew(null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00150204 File Offset: 0x0014F404
		public static Task Run([Nullable(new byte[]
		{
			1,
			2
		})] Func<Task> function)
		{
			return Task.Run(function, default(CancellationToken));
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00150220 File Offset: 0x0014F420
		public static Task Run([Nullable(new byte[]
		{
			1,
			2
		})] Func<Task> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.function);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task<Task> outerTask = Task<Task>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			return new UnwrapPromise<VoidTaskResult>(outerTask, true);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00150264 File Offset: 0x0014F464
		public static Task<TResult> Run<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<Task<TResult>> function)
		{
			return Task.Run<TResult>(function, default(CancellationToken));
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00150280 File Offset: 0x0014F480
		public static Task<TResult> Run<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<Task<TResult>> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.function);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<TResult>(cancellationToken);
			}
			Task<Task<TResult>> outerTask = Task<Task<TResult>>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			return new UnwrapPromise<TResult>(outerTask, true);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x001502C4 File Offset: 0x0014F4C4
		public static Task Delay(TimeSpan delay)
		{
			return Task.Delay(delay, default(CancellationToken));
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x001502E0 File Offset: 0x0014F4E0
		public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.delay, ExceptionResource.Task_Delay_InvalidDelay);
			}
			return Task.Delay((int)num, cancellationToken);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00150318 File Offset: 0x0014F518
		public static Task Delay(int millisecondsDelay)
		{
			return Task.Delay(millisecondsDelay, default(CancellationToken));
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00150334 File Offset: 0x0014F534
		public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
		{
			if (millisecondsDelay < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.millisecondsDelay, ExceptionResource.Task_Delay_InvalidMillisecondsDelay);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (millisecondsDelay == 0)
			{
				return Task.CompletedTask;
			}
			if (!cancellationToken.CanBeCanceled)
			{
				return new Task.DelayPromise(millisecondsDelay);
			}
			return new Task.DelayPromiseWithCancellation(millisecondsDelay, cancellationToken);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00150374 File Offset: 0x0014F574
		public static Task WhenAll(IEnumerable<Task> tasks)
		{
			Task[] array = tasks as Task[];
			if (array != null)
			{
				return Task.WhenAll(array);
			}
			ICollection<Task> collection = tasks as ICollection<Task>;
			if (collection != null)
			{
				int num = 0;
				array = new Task[collection.Count];
				foreach (Task task in tasks)
				{
					if (task == null)
					{
						ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll(array);
			}
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			List<Task> list = new List<Task>();
			foreach (Task task2 in tasks)
			{
				if (task2 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll(list.ToArray());
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00150468 File Offset: 0x0014F668
		public static Task WhenAll(params Task[] tasks)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll(tasks);
			}
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				array[i] = task;
			}
			return Task.InternalWhenAll(array);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x001504BA File Offset: 0x0014F6BA
		private static Task InternalWhenAll(Task[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise(tasks);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x001504CC File Offset: 0x0014F6CC
		public static Task<TResult[]> WhenAll<[Nullable(2)] TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<TResult>[] array = tasks as Task<TResult>[];
			if (array != null)
			{
				return Task.WhenAll<TResult>(array);
			}
			ICollection<Task<TResult>> collection = tasks as ICollection<Task<TResult>>;
			if (collection != null)
			{
				int num = 0;
				array = new Task<TResult>[collection.Count];
				foreach (Task<TResult> task in tasks)
				{
					if (task == null)
					{
						ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll<TResult>(array);
			}
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			List<Task<TResult>> list = new List<Task<TResult>>();
			foreach (Task<TResult> task2 in tasks)
			{
				if (task2 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll<TResult>(list.ToArray());
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x001505C0 File Offset: 0x0014F7C0
		public static Task<TResult[]> WhenAll<[Nullable(2)] TResult>(params Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll<TResult>(tasks);
			}
			Task<TResult>[] array = new Task<TResult>[num];
			for (int i = 0; i < num; i++)
			{
				Task<TResult> task = tasks[i];
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				array[i] = task;
			}
			return Task.InternalWhenAll<TResult>(array);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00150614 File Offset: 0x0014F814
		private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise<TResult>(tasks);
			}
			return new Task<TResult[]>(false, Array.Empty<TResult>(), TaskCreationOptions.None, default(CancellationToken));
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00150644 File Offset: 0x0014F844
		public static Task<Task> WhenAny(params Task[] tasks)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			if (tasks.Length == 2)
			{
				return Task.WhenAny(tasks[0], tasks[1]);
			}
			if (tasks.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList, ExceptionArgument.tasks);
			}
			int num = tasks.Length;
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				array[i] = task;
			}
			return TaskFactory.CommonCWAnyLogic(array, false);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x001506AC File Offset: 0x0014F8AC
		public static Task<Task> WhenAny(Task task1, Task task2)
		{
			if (task1 == null || task2 == null)
			{
				throw new ArgumentNullException((task1 == null) ? "task1" : "task2");
			}
			if (task1.IsCompleted)
			{
				return Task.FromResult<Task>(task1);
			}
			if (!task2.IsCompleted)
			{
				return new Task.TwoTaskWhenAnyPromise<Task>(task1, task2);
			}
			return Task.FromResult<Task>(task2);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x001506FC File Offset: 0x0014F8FC
		public static Task<Task> WhenAny(IEnumerable<Task> tasks)
		{
			if (tasks == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.tasks);
			}
			List<Task> list = new List<Task>();
			foreach (Task task in tasks)
			{
				if (task == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
				}
				list.Add(task);
			}
			if (list.Count == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList, ExceptionArgument.tasks);
			}
			return TaskFactory.CommonCWAnyLogic(list, false);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00150778 File Offset: 0x0014F978
		public static Task<Task<TResult>> WhenAny<[Nullable(2)] TResult>(params Task<TResult>[] tasks)
		{
			if (tasks != null && tasks.Length == 2)
			{
				return Task.WhenAny<TResult>(tasks[0], tasks[1]);
			}
			Task<Task> task = Task.WhenAny(tasks);
			return task.ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast.Value, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x001507C4 File Offset: 0x0014F9C4
		public static Task<Task<TResult>> WhenAny<[Nullable(2)] TResult>(Task<TResult> task1, Task<TResult> task2)
		{
			if (task1 == null || task2 == null)
			{
				throw new ArgumentNullException((task1 == null) ? "task1" : "task2");
			}
			if (task1.IsCompleted)
			{
				return Task.FromResult<Task<TResult>>(task1);
			}
			if (!task2.IsCompleted)
			{
				return new Task.TwoTaskWhenAnyPromise<Task<TResult>>(task1, task2);
			}
			return Task.FromResult<Task<TResult>>(task2);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00150814 File Offset: 0x0014FA14
		public static Task<Task<TResult>> WhenAny<[Nullable(2)] TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<Task> task = Task.WhenAny(tasks);
			return task.ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast.Value, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00150846 File Offset: 0x0014FA46
		internal static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
		{
			return new UnwrapPromise<TResult>(outerTask, lookForOce);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0015084F File Offset: 0x0014FA4F
		internal virtual Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_continuationObject != this)
			{
				return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
			}
			return null;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0015086C File Offset: 0x0014FA6C
		private static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
		{
			if (continuationObject != null)
			{
				Action action = continuationObject as Action;
				if (action != null)
				{
					return new Delegate[]
					{
						AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action)
					};
				}
				TaskContinuation taskContinuation = continuationObject as TaskContinuation;
				if (taskContinuation != null)
				{
					return taskContinuation.GetDelegateContinuationsForDebugger();
				}
				Task task = continuationObject as Task;
				if (task != null)
				{
					Delegate[] delegateContinuationsForDebugger = task.GetDelegateContinuationsForDebugger();
					if (delegateContinuationsForDebugger != null)
					{
						return delegateContinuationsForDebugger;
					}
				}
				ITaskCompletionAction taskCompletionAction = continuationObject as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					return new Delegate[]
					{
						new Action<Task>(taskCompletionAction.Invoke)
					};
				}
				List<object> list = continuationObject as List<object>;
				if (list != null)
				{
					List<Delegate> list2 = new List<Delegate>();
					foreach (object continuationObject2 in list)
					{
						Delegate[] delegatesFromContinuationObject = Task.GetDelegatesFromContinuationObject(continuationObject2);
						if (delegatesFromContinuationObject != null)
						{
							foreach (Delegate @delegate in delegatesFromContinuationObject)
							{
								if (@delegate != null)
								{
									list2.Add(@delegate);
								}
							}
						}
					}
					return list2.ToArray();
				}
			}
			return null;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00150978 File Offset: 0x0014FB78
		private static Task GetActiveTaskFromId(int taskId)
		{
			Task result = null;
			Dictionary<int, Task> dictionary = Task.s_currentActiveTasks;
			if (dictionary != null)
			{
				dictionary.TryGetValue(taskId, out result);
			}
			return result;
		}

		// Token: 0x04000BA3 RID: 2979
		[ThreadStatic]
		internal static Task t_currentTask;

		// Token: 0x04000BA4 RID: 2980
		internal static int s_taskIdCounter;

		// Token: 0x04000BA5 RID: 2981
		private volatile int m_taskId;

		// Token: 0x04000BA6 RID: 2982
		internal Delegate m_action;

		// Token: 0x04000BA7 RID: 2983
		internal object m_stateObject;

		// Token: 0x04000BA8 RID: 2984
		internal TaskScheduler m_taskScheduler;

		// Token: 0x04000BA9 RID: 2985
		internal volatile int m_stateFlags;

		// Token: 0x04000BAA RID: 2986
		private volatile object m_continuationObject;

		// Token: 0x04000BAB RID: 2987
		private static readonly object s_taskCompletionSentinel = new object();

		// Token: 0x04000BAC RID: 2988
		internal static bool s_asyncDebuggingEnabled;

		// Token: 0x04000BAD RID: 2989
		private static Dictionary<int, Task> s_currentActiveTasks;

		// Token: 0x04000BAE RID: 2990
		internal Task.ContingentProperties m_contingentProperties;

		// Token: 0x04000BB0 RID: 2992
		internal static readonly Task<VoidTaskResult> s_cachedCompleted = new Task<VoidTaskResult>(false, default(VoidTaskResult), (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04000BB1 RID: 2993
		private static readonly ContextCallback s_ecCallback = delegate(object obj)
		{
			Unsafe.As<Task>(obj).InnerInvoke();
		};

		// Token: 0x0200030E RID: 782
		internal class ContingentProperties
		{
			// Token: 0x06002AE5 RID: 10981 RVA: 0x001509F4 File Offset: 0x0014FBF4
			internal void SetCompleted()
			{
				ManualResetEventSlim completionEvent = this.m_completionEvent;
				if (completionEvent != null)
				{
					completionEvent.Set();
				}
			}

			// Token: 0x06002AE6 RID: 10982 RVA: 0x00150A14 File Offset: 0x0014FC14
			internal void UnregisterCancellationCallback()
			{
				if (this.m_cancellationRegistration != null)
				{
					try
					{
						this.m_cancellationRegistration.Value.Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
					this.m_cancellationRegistration = null;
				}
			}

			// Token: 0x04000BB2 RID: 2994
			internal ExecutionContext m_capturedContext;

			// Token: 0x04000BB3 RID: 2995
			internal volatile ManualResetEventSlim m_completionEvent;

			// Token: 0x04000BB4 RID: 2996
			internal volatile TaskExceptionHolder m_exceptionsHolder;

			// Token: 0x04000BB5 RID: 2997
			internal CancellationToken m_cancellationToken;

			// Token: 0x04000BB6 RID: 2998
			internal StrongBox<CancellationTokenRegistration> m_cancellationRegistration;

			// Token: 0x04000BB7 RID: 2999
			internal volatile int m_internalCancellationRequested;

			// Token: 0x04000BB8 RID: 3000
			internal volatile int m_completionCountdown = 1;

			// Token: 0x04000BB9 RID: 3001
			internal volatile List<Task> m_exceptionalChildren;

			// Token: 0x04000BBA RID: 3002
			internal Task m_parent;
		}

		// Token: 0x0200030F RID: 783
		private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06002AE8 RID: 10984 RVA: 0x00150A69 File Offset: 0x0014FC69
			internal SetOnInvokeMres() : base(false, 0)
			{
			}

			// Token: 0x06002AE9 RID: 10985 RVA: 0x00150A73 File Offset: 0x0014FC73
			public void Invoke(Task completingTask)
			{
				base.Set();
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000AC09B File Offset: 0x000AB29B
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x02000310 RID: 784
		private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06002AEB RID: 10987 RVA: 0x00150A7B File Offset: 0x0014FC7B
			internal SetOnCountdownMres(int count)
			{
				this._count = count;
			}

			// Token: 0x06002AEC RID: 10988 RVA: 0x00150A8A File Offset: 0x0014FC8A
			public void Invoke(Task completingTask)
			{
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					base.Set();
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002AED RID: 10989 RVA: 0x000AC09B File Offset: 0x000AB29B
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000BBB RID: 3003
			private int _count;
		}

		// Token: 0x02000311 RID: 785
		private class DelayPromise : Task
		{
			// Token: 0x06002AEE RID: 10990 RVA: 0x00150AA0 File Offset: 0x0014FCA0
			internal DelayPromise(int millisecondsDelay)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "Task.Delay", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				if (millisecondsDelay != -1)
				{
					this._timer = new TimerQueueTimer(delegate(object state)
					{
						((Task.DelayPromise)state).CompleteTimedOut();
					}, this, (uint)millisecondsDelay, uint.MaxValue, false);
					if (base.IsCompleted)
					{
						this._timer.Close();
					}
				}
			}

			// Token: 0x06002AEF RID: 10991 RVA: 0x00150B29 File Offset: 0x0014FD29
			private void CompleteTimedOut()
			{
				if (base.TrySetResult())
				{
					this.Cleanup();
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
				}
			}

			// Token: 0x06002AF0 RID: 10992 RVA: 0x00150B63 File Offset: 0x0014FD63
			protected virtual void Cleanup()
			{
				TimerQueueTimer timer = this._timer;
				if (timer == null)
				{
					return;
				}
				timer.Close();
			}

			// Token: 0x04000BBC RID: 3004
			private readonly TimerQueueTimer _timer;
		}

		// Token: 0x02000313 RID: 787
		private sealed class DelayPromiseWithCancellation : Task.DelayPromise
		{
			// Token: 0x06002AF4 RID: 10996 RVA: 0x00150B8E File Offset: 0x0014FD8E
			internal DelayPromiseWithCancellation(int millisecondsDelay, CancellationToken token) : base(millisecondsDelay)
			{
				this._token = token;
				this._registration = token.UnsafeRegister(delegate(object state)
				{
					((Task.DelayPromiseWithCancellation)state).CompleteCanceled();
				}, this);
			}

			// Token: 0x06002AF5 RID: 10997 RVA: 0x00150BCB File Offset: 0x0014FDCB
			private void CompleteCanceled()
			{
				if (base.TrySetCanceled(this._token))
				{
					this.Cleanup();
				}
			}

			// Token: 0x06002AF6 RID: 10998 RVA: 0x00150BE1 File Offset: 0x0014FDE1
			protected override void Cleanup()
			{
				this._registration.Dispose();
				base.Cleanup();
			}

			// Token: 0x04000BBF RID: 3007
			private readonly CancellationToken _token;

			// Token: 0x04000BC0 RID: 3008
			private readonly CancellationTokenRegistration _registration;
		}

		// Token: 0x02000315 RID: 789
		private sealed class WhenAllPromise : Task, ITaskCompletionAction
		{
			// Token: 0x06002AFA RID: 11002 RVA: 0x00150C10 File Offset: 0x0014FE10
			internal WhenAllPromise(Task[] tasks)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "Task.WhenAll", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				foreach (Task task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this, false);
					}
				}
			}

			// Token: 0x06002AFB RID: 11003 RVA: 0x00150C94 File Offset: 0x0014FE94
			public void Invoke(Task completedTask)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled && task == null)
						{
							task = task2;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					base.TrySetResult();
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06002AFD RID: 11005 RVA: 0x00150D88 File Offset: 0x0014FF88
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x04000BC3 RID: 3011
			private readonly Task[] m_tasks;

			// Token: 0x04000BC4 RID: 3012
			private int m_count;
		}

		// Token: 0x02000316 RID: 790
		private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
		{
			// Token: 0x06002AFE RID: 11006 RVA: 0x00150DA0 File Offset: 0x0014FFA0
			internal WhenAllPromise(Task<T>[] tasks)
			{
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "Task.WhenAll", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				foreach (Task<T> task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this, false);
					}
				}
			}

			// Token: 0x06002AFF RID: 11007 RVA: 0x00150E24 File Offset: 0x00150024
			public void Invoke(Task ignored)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					T[] array = new T[this.m_tasks.Length];
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task<T> task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled)
						{
							if (task == null)
							{
								task = task2;
							}
						}
						else
						{
							array[i] = task2.GetResultCore(false);
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					base.TrySetResult(array);
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00150F40 File Offset: 0x00150140
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this.m_tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x04000BC5 RID: 3013
			private readonly Task<T>[] m_tasks;

			// Token: 0x04000BC6 RID: 3014
			private int m_count;
		}

		// Token: 0x02000317 RID: 791
		private sealed class TwoTaskWhenAnyPromise<TTask> : Task<TTask>, ITaskCompletionAction where TTask : Task
		{
			// Token: 0x06002B02 RID: 11010 RVA: 0x00150F64 File Offset: 0x00150164
			public TwoTaskWhenAnyPromise(TTask task1, TTask task2)
			{
				this._task1 = task1;
				this._task2 = task2;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "Task.WhenAny", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				task1.AddCompletionAction(this, false);
				task2.AddCompletionAction(this, false);
				if (task1.IsCompleted)
				{
					task2.RemoveContinuation(this);
				}
			}

			// Token: 0x06002B03 RID: 11011 RVA: 0x00150FEC File Offset: 0x001501EC
			public void Invoke(Task completingTask)
			{
				Task task;
				if ((task = Interlocked.Exchange<TTask>(ref this._task1, default(TTask))) != null)
				{
					Task task2 = this._task2;
					this._task2 = default(TTask);
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Choice);
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					if (!task.IsCompleted)
					{
						task.RemoveContinuation(this);
					}
					else
					{
						task2.RemoveContinuation(this);
					}
					bool flag = base.TrySetResult((TTask)((object)completingTask));
				}
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000BC7 RID: 3015
			private TTask _task1;

			// Token: 0x04000BC8 RID: 3016
			private TTask _task2;
		}
	}
}
