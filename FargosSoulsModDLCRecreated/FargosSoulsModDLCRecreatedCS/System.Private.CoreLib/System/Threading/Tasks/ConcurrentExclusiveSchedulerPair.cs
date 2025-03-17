using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002EF RID: 751
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x0014AC47 File Offset: 0x00149E47
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0014AC4E File Offset: 0x00149E4E
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMode;
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0014AC56 File Offset: 0x00149E56
		public ConcurrentExclusiveSchedulerPair() : this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x0014AC69 File Offset: 0x00149E69
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler) : this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0014AC78 File Offset: 0x00149E78
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel) : this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x0014AC84 File Offset: 0x00149E84
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x0014AD50 File Offset: 0x00149F50
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x0014ADA0 File Offset: 0x00149FA0
		public Task Completion
		{
			get
			{
				return this.EnsureCompletionStateInitialized();
			}
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x0014ADA8 File Offset: 0x00149FA8
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x0014ADD4 File Offset: 0x00149FD4
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0014ADF0 File Offset: 0x00149FF0
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0014ADFE File Offset: 0x00149FFE
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x0014AE10 File Offset: 0x0014A010
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0014AE74 File Offset: 0x0014A074
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair concurrentExclusiveSchedulerPair = (ConcurrentExclusiveSchedulerPair)state;
					List<Exception> exceptions = concurrentExclusiveSchedulerPair.m_completionState.m_exceptions;
					bool flag = (exceptions != null && exceptions.Count > 0) ? concurrentExclusiveSchedulerPair.m_completionState.TrySetException(exceptions) : concurrentExclusiveSchedulerPair.m_completionState.TrySetResult();
					concurrentExclusiveSchedulerPair.m_threadProcessingMode.Dispose();
				}, this);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0014AEC0 File Offset: 0x0014A0C0
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = completionState;
			if (completionState2.m_exceptions == null)
			{
				completionState2.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0014AF05 File Offset: 0x0014A105
		public TaskScheduler ConcurrentScheduler
		{
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0014AF0D File Offset: 0x0014A10D
		public TaskScheduler ExclusiveScheduler
		{
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x0014AF15 File Offset: 0x0014A115
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x0014AF27 File Offset: 0x0014A127
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0014AF3C File Offset: 0x0014A13C
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					if (this.TryQueueThreadPoolWorkItem(fairly))
					{
						goto IL_17A;
					}
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_17A;
					}
					catch (Exception exception)
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task ?? Task.FromException(exception));
						goto IL_17A;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						if (!this.TryQueueThreadPoolWorkItem(fairly))
						{
							try
							{
								task = new Task(delegate(object thisPair)
								{
									((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
								}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
								task.Start(this.m_underlyingTaskScheduler);
							}
							catch (Exception exception2)
							{
								this.m_processingCount--;
								this.FaultWithTask(task ?? Task.FromException(exception2));
							}
						}
						num++;
					}
				}
				IL_17A:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0014B0E8 File Offset: 0x0014A2E8
		private bool TryQueueThreadPoolWorkItem(bool fairly)
		{
			if (TaskScheduler.Default == this.m_underlyingTaskScheduler)
			{
				ConcurrentExclusiveSchedulerPair.SchedulerWorkItem schedulerWorkItem;
				if ((schedulerWorkItem = this.m_threadPoolWorkItem) == null)
				{
					schedulerWorkItem = (this.m_threadPoolWorkItem = new ConcurrentExclusiveSchedulerPair.SchedulerWorkItem(this));
				}
				IThreadPoolWorkItem callBack = schedulerWorkItem;
				ThreadPool.UnsafeQueueUserWorkItemInternal(callBack, !fairly);
				return true;
			}
			return false;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0014B12C File Offset: 0x0014A32C
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0014B1D8 File Offset: 0x0014A3D8
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x0014B2A8 File Offset: 0x0014A4A8
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0014B2F8 File Offset: 0x0014A4F8
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04000B41 RID: 2881
		private readonly ThreadLocal<ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMode = new ThreadLocal<ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04000B42 RID: 2882
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04000B43 RID: 2883
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04000B44 RID: 2884
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04000B45 RID: 2885
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04000B46 RID: 2886
		private readonly int m_maxItemsPerTask;

		// Token: 0x04000B47 RID: 2887
		private int m_processingCount;

		// Token: 0x04000B48 RID: 2888
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04000B49 RID: 2889
		private ConcurrentExclusiveSchedulerPair.SchedulerWorkItem m_threadPoolWorkItem;

		// Token: 0x020002F0 RID: 752
		private sealed class CompletionState : Task
		{
			// Token: 0x04000B4A RID: 2890
			internal bool m_completionRequested;

			// Token: 0x04000B4B RID: 2891
			internal bool m_completionQueued;

			// Token: 0x04000B4C RID: 2892
			internal List<Exception> m_exceptions;
		}

		// Token: 0x020002F1 RID: 753
		private sealed class SchedulerWorkItem : IThreadPoolWorkItem
		{
			// Token: 0x0600294A RID: 10570 RVA: 0x0014B317 File Offset: 0x0014A517
			internal SchedulerWorkItem(ConcurrentExclusiveSchedulerPair pair)
			{
				this._pair = pair;
			}

			// Token: 0x0600294B RID: 10571 RVA: 0x0014B326 File Offset: 0x0014A526
			void IThreadPoolWorkItem.Execute()
			{
				if (this._pair.m_processingCount == -1)
				{
					this._pair.ProcessExclusiveTasks();
					return;
				}
				this._pair.ProcessConcurrentTasks();
			}

			// Token: 0x04000B4D RID: 2893
			private readonly ConcurrentExclusiveSchedulerPair _pair;
		}

		// Token: 0x020002F2 RID: 754
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x0600294C RID: 10572 RVA: 0x0014B350 File Offset: 0x0014A550
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> tasks;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					tasks = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					tasks = producerConsumerQueue;
				}
				this.m_tasks = tasks;
			}

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x0600294D RID: 10573 RVA: 0x0014B392 File Offset: 0x0014A592
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x0600294E RID: 10574 RVA: 0x0014B39C File Offset: 0x0014A59C
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().ToString());
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x0600294F RID: 10575 RVA: 0x0014B414 File Offset: 0x0014A614
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06002950 RID: 10576 RVA: 0x0014B420 File Offset: 0x0014A620
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				if (this.m_pair.m_threadProcessingMode.Value != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x06002951 RID: 10577 RVA: 0x0014B494 File Offset: 0x0014A694
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x06002952 RID: 10578 RVA: 0x0014B4FC File Offset: 0x0014A6FC
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x06002953 RID: 10579 RVA: 0x0014B521 File Offset: 0x0014A721
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x06002954 RID: 10580 RVA: 0x0014B529 File Offset: 0x0014A729
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x04000B4E RID: 2894
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x04000B4F RID: 2895
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x04000B50 RID: 2896
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x04000B51 RID: 2897
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x04000B52 RID: 2898
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x020002F3 RID: 755
			private sealed class DebugView
			{
				// Token: 0x06002956 RID: 10582 RVA: 0x0014B549 File Offset: 0x0014A749
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x1700088A RID: 2186
				// (get) Token: 0x06002957 RID: 10583 RVA: 0x0014B558 File Offset: 0x0014A758
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x1700088B RID: 2187
				// (get) Token: 0x06002958 RID: 10584 RVA: 0x0014B565 File Offset: 0x0014A765
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x1700088C RID: 2188
				// (get) Token: 0x06002959 RID: 10585 RVA: 0x0014B572 File Offset: 0x0014A772
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x04000B53 RID: 2899
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x020002F4 RID: 756
		private sealed class DebugView
		{
			// Token: 0x0600295A RID: 10586 RVA: 0x0014B57F File Offset: 0x0014A77F
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x0600295B RID: 10587 RVA: 0x0014B58E File Offset: 0x0014A78E
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x0600295C RID: 10588 RVA: 0x0014B59B File Offset: 0x0014A79B
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x0600295D RID: 10589 RVA: 0x0014B5AD File Offset: 0x0014A7AD
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x0600295E RID: 10590 RVA: 0x0014B5BF File Offset: 0x0014A7BF
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x0600295F RID: 10591 RVA: 0x0014B5DC File Offset: 0x0014A7DC
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x04000B54 RID: 2900
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x020002F5 RID: 757
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x04000B56 RID: 2902
			NotCurrentlyProcessing = 0,
			// Token: 0x04000B57 RID: 2903
			ProcessingExclusiveTask = 1,
			// Token: 0x04000B58 RID: 2904
			ProcessingConcurrentTasks = 2,
			// Token: 0x04000B59 RID: 2905
			Completing = 4,
			// Token: 0x04000B5A RID: 2906
			Completed = 8
		}
	}
}
