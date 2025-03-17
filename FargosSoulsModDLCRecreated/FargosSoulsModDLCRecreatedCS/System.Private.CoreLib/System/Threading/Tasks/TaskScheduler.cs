using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000339 RID: 825
	[DebuggerDisplay("Id={Id}")]
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	public abstract class TaskScheduler
	{
		// Token: 0x06002BEF RID: 11247
		protected internal abstract void QueueTask(Task task);

		// Token: 0x06002BF0 RID: 11248
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		// Token: 0x06002BF1 RID: 11249
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		protected abstract IEnumerable<Task> GetScheduledTasks();

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x001535D3 File Offset: 0x001527D3
		public virtual int MaximumConcurrencyLevel
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x001535DC File Offset: 0x001527DC
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				return false;
			}
			if (TplEventSource.Log.IsEnabled())
			{
				task.FireTaskScheduledIfNeeded(this);
			}
			bool flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException(SR.TaskScheduler_InconsistentStateAfterTryExecuteTaskInline);
			}
			return flag;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000AB30B File Offset: 0x000AA50B
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x0015365F File Offset: 0x0015285F
		internal void InternalQueueTask(Task task)
		{
			if (TplEventSource.Log.IsEnabled())
			{
				task.FireTaskScheduledIfNeeded(this);
			}
			this.QueueTask(task);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0015367B File Offset: 0x0015287B
		protected TaskScheduler()
		{
			if (Debugger.IsAttached)
			{
				this.AddToActiveTaskSchedulers();
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00153690 File Offset: 0x00152890
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x001536C5 File Offset: 0x001528C5
		public static TaskScheduler Default
		{
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x001536CC File Offset: 0x001528CC
		public static TaskScheduler Current
		{
			get
			{
				return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x001536DC File Offset: 0x001528DC
		[Nullable(2)]
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00153705 File Offset: 0x00152905
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x0015370C File Offset: 0x0015290C
		public int Id
		{
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00153747 File Offset: 0x00152947
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException(SR.TaskScheduler_ExecuteTask_WrongTaskScheduler);
			}
			return task.ExecuteEntry();
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06002BFF RID: 11263 RVA: 0x00153764 File Offset: 0x00152964
		// (remove) Token: 0x06002C00 RID: 11264 RVA: 0x00153798 File Offset: 0x00152998
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

		// Token: 0x06002C01 RID: 11265 RVA: 0x001537CB File Offset: 0x001529CB
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler.UnobservedTaskException;
			if (unobservedTaskException == null)
			{
				return;
			}
			unobservedTaskException(sender, ueea);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x001537E0 File Offset: 0x001529E0
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new List<Task>(scheduledTasks).ToArray();
			}
			foreach (Task task in array)
			{
				int id = task.Id;
			}
			return array;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00153830 File Offset: 0x00152A30
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[]
				{
					TaskScheduler.s_defaultTaskScheduler
				};
			}
			List<TaskScheduler> list = new List<TaskScheduler>();
			foreach (KeyValuePair<TaskScheduler, object> keyValuePair in ((IEnumerable<KeyValuePair<TaskScheduler, object>>)TaskScheduler.s_activeTaskSchedulers))
			{
				list.Add(keyValuePair.Key);
			}
			if (!list.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				list.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = list.ToArray();
			foreach (TaskScheduler taskScheduler in array)
			{
				int id = taskScheduler.Id;
			}
			return array;
		}

		// Token: 0x04000C20 RID: 3104
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04000C21 RID: 3105
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04000C22 RID: 3106
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04000C23 RID: 3107
		private volatile int m_taskSchedulerId;

		// Token: 0x0200033A RID: 826
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x06002C05 RID: 11269 RVA: 0x001538F0 File Offset: 0x00152AF0
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06002C06 RID: 11270 RVA: 0x001538FF File Offset: 0x00152AFF
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06002C07 RID: 11271 RVA: 0x0015390C File Offset: 0x00152B0C
			public IEnumerable<Task> ScheduledTasks
			{
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x04000C25 RID: 3109
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
