using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000335 RID: 821
	[Nullable(0)]
	[NullableContext(1)]
	public class TaskFactory
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x001526D3 File Offset: 0x001518D3
		private TaskScheduler DefaultScheduler
		{
			get
			{
				return this.m_defaultScheduler ?? TaskScheduler.Current;
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x001526E4 File Offset: 0x001518E4
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			TaskScheduler result;
			if ((result = this.m_defaultScheduler) == null)
			{
				if (currTask == null || (currTask.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return TaskScheduler.Default;
				}
				result = currTask.ExecutingTaskScheduler;
			}
			return result;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0015270C File Offset: 0x0015190C
		public TaskFactory() : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0015272B File Offset: 0x0015192B
		public TaskFactory(CancellationToken cancellationToken) : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00152738 File Offset: 0x00151938
		[NullableContext(2)]
		public TaskFactory(TaskScheduler scheduler) : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00152758 File Offset: 0x00151958
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions) : this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00152777 File Offset: 0x00151977
		[NullableContext(2)]
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x001527A8 File Offset: 0x001519A8
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.creationOptions);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x001527B7 File Offset: 0x001519B7
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x001527BF File Offset: 0x001519BF
		[Nullable(2)]
		public TaskScheduler Scheduler
		{
			[NullableContext(2)]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x001527C7 File Offset: 0x001519C7
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x001527CF File Offset: 0x001519CF
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x001527D8 File Offset: 0x001519D8
		public Task StartNew(Action action)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00152808 File Offset: 0x00151A08
		public Task StartNew(Action action, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00152834 File Offset: 0x00151A34
		public Task StartNew(Action action, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0015285E File Offset: 0x00151A5E
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00152874 File Offset: 0x00151A74
		public Task StartNew([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, [Nullable(2)] object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x001528A4 File Offset: 0x00151AA4
		public Task StartNew([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, [Nullable(2)] object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x001528D0 File Offset: 0x00151AD0
		public Task StartNew([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x001528FA File Offset: 0x00151AFA
		public Task StartNew([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> action, [Nullable(2)] object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x00152910 File Offset: 0x00151B10
		public Task<TResult> StartNew<[Nullable(2)] TResult>(Func<TResult> function)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00152940 File Offset: 0x00151B40
		public Task<TResult> StartNew<[Nullable(2)] TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0015296C File Offset: 0x00151B6C
		public Task<TResult> StartNew<[Nullable(2)] TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x00152995 File Offset: 0x00151B95
		public Task<TResult> StartNew<[Nullable(2)] TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x001529A8 File Offset: 0x00151BA8
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> StartNew<TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x001529D8 File Offset: 0x00151BD8
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> StartNew<TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x00152A04 File Offset: 0x00151C04
		[NullableContext(2)]
		[return: Nullable(1)]
		public Task<TResult> StartNew<TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00152A2E File Offset: 0x00151C2E
		public Task<TResult> StartNew<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			2,
			1
		})] Func<object, TResult> function, [Nullable(2)] object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00152A44 File Offset: 0x00151C44
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
		{
			return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00152A5A File Offset: 0x00151C5A
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
		{
			return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x00152A6B File Offset: 0x00151C6B
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, null, endMethod, creationOptions, scheduler);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00152A78 File Offset: 0x00151C78
		public Task FromAsync([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, [Nullable(2)] object state)
		{
			return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00152A89 File Offset: 0x00151C89
		public Task FromAsync([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, null, endMethod, state, creationOptions);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00152A96 File Offset: 0x00151C96
		public Task FromAsync<[Nullable(2)] TArg1>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, [Nullable(2)] object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x00152AA9 File Offset: 0x00151CA9
		public Task FromAsync<[Nullable(2)] TArg1>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x00152AB8 File Offset: 0x00151CB8
		public Task FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00152ACD File Offset: 0x00151CCD
		public Task FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x00152ADE File Offset: 0x00151CDE
		public Task FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00152AF5 File Offset: 0x00151CF5
		public Task FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00152B08 File Offset: 0x00151D08
		public Task<TResult> FromAsync<[Nullable(2)] TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00152B1E File Offset: 0x00151D1E
		public Task<TResult> FromAsync<[Nullable(2)] TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x00152B2F File Offset: 0x00151D2F
		public Task<TResult> FromAsync<[Nullable(2)] TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x00152B3C File Offset: 0x00151D3C
		public Task<TResult> FromAsync<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00152B4D File Offset: 0x00151D4D
		public Task<TResult> FromAsync<[Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			2,
			1
		})] Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x00152B5A File Offset: 0x00151D5A
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x00152B6D File Offset: 0x00151D6D
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00152B7C File Offset: 0x00151D7C
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00152B91 File Offset: 0x00151D91
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00152BA2 File Offset: 0x00151DA2
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00152BB9 File Offset: 0x00151DB9
		public Task<TResult> FromAsync<[Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TArg3, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})] Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, [Nullable(2)] object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00152BCC File Offset: 0x00151DCC
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", SR.Task_FromAsync_LongRunning);
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", SR.Task_FromAsync_PreferFairness);
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.creationOptions);
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00152C08 File Offset: 0x00151E08
		internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise, false);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00152C4C File Offset: 0x00151E4C
		internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise<T> completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise, false);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00152C8E File Offset: 0x00151E8E
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00152CB8 File Offset: 0x00151EB8
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00152CDD File Offset: 0x00151EDD
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00152D02 File Offset: 0x00151F02
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00152D1F File Offset: 0x00151F1F
		public Task ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00152D49 File Offset: 0x00151F49
		public Task ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x00152D6E File Offset: 0x00151F6E
		public Task ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x00152D93 File Offset: 0x00151F93
		public Task ContinueWhenAll<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x00152DB0 File Offset: 0x00151FB0
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x00152DDA File Offset: 0x00151FDA
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x00152DFF File Offset: 0x00151FFF
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00152E24 File Offset: 0x00152024
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00152E41 File Offset: 0x00152041
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x00152E6B File Offset: 0x0015206B
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x00152E90 File Offset: 0x00152090
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x00152EB5 File Offset: 0x001520B5
		public Task<TResult> ContinueWhenAll<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x00152ED4 File Offset: 0x001520D4
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks, bool isSyncBlocking = false)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks, isSyncBlocking);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(SR.Task_MultiTaskContinuation_NullTask, "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise, isSyncBlocking);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00152F59 File Offset: 0x00152159
		internal static void CommonCWAnyLogicCleanup(Task<Task> continuation)
		{
			((TaskFactory.CompleteOnInvokePromise)continuation).Invoke(null);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00152F67 File Offset: 0x00152167
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x00152F91 File Offset: 0x00152191
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00152FB6 File Offset: 0x001521B6
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00152FDB File Offset: 0x001521DB
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00152FF8 File Offset: 0x001521F8
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00153022 File Offset: 0x00152222
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00153047 File Offset: 0x00152247
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0015306C File Offset: 0x0015226C
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x00153089 File Offset: 0x00152289
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x001530B3 File Offset: 0x001522B3
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x001530D8 File Offset: 0x001522D8
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x001530FD File Offset: 0x001522FD
		public Task<TResult> ContinueWhenAny<[Nullable(2)] TAntecedentResult, [Nullable(2)] TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0015311A File Offset: 0x0015231A
		public Task ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00153144 File Offset: 0x00152344
		public Task ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x00153169 File Offset: 0x00152369
		public Task ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0015318E File Offset: 0x0015238E
		public Task ContinueWhenAny<[Nullable(2)] TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x001531AC File Offset: 0x001523AC
		internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(SR.Task_MultiTaskContinuation_EmptyTaskList, "tasks");
			}
			Task[] array = new Task[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(SR.Task_MultiTaskContinuation_NullTask, "tasks");
				}
			}
			return array;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x00153210 File Offset: 0x00152410
		internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(SR.Task_MultiTaskContinuation_EmptyTaskList, "tasks");
			}
			Task<TResult>[] array = new Task<TResult>[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(SR.Task_MultiTaskContinuation_NullTask, "tasks");
				}
			}
			return array;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00153274 File Offset: 0x00152474
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", SR.Task_ContinueWith_ESandLR);
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", SR.Task_MultiTaskContinuation_FireOptions);
			}
		}

		// Token: 0x04000C16 RID: 3094
		private readonly CancellationToken m_defaultCancellationToken;

		// Token: 0x04000C17 RID: 3095
		private readonly TaskScheduler m_defaultScheduler;

		// Token: 0x04000C18 RID: 3096
		private readonly TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04000C19 RID: 3097
		private readonly TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000336 RID: 822
		private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
		{
			// Token: 0x06002BE4 RID: 11236 RVA: 0x001532CC File Offset: 0x001524CC
			internal CompleteOnCountdownPromise(Task[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "TaskFactory.ContinueWhenAll", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06002BE5 RID: 11237 RVA: 0x00153320 File Offset: 0x00152520
			public void Invoke(Task completingTask)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x0015339D File Offset: 0x0015259D
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x04000C1A RID: 3098
			private readonly Task[] _tasks;

			// Token: 0x04000C1B RID: 3099
			private int _count;
		}

		// Token: 0x02000337 RID: 823
		private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
		{
			// Token: 0x06002BE8 RID: 11240 RVA: 0x001533B4 File Offset: 0x001525B4
			internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "TaskFactory.ContinueWhenAll<>", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06002BE9 RID: 11241 RVA: 0x00153408 File Offset: 0x00152608
			public void Invoke(Task completingTask)
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06002BEB RID: 11243 RVA: 0x00153488 File Offset: 0x00152688
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this._tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x04000C1C RID: 3100
			private readonly Task<T>[] _tasks;

			// Token: 0x04000C1D RID: 3101
			private int _count;
		}

		// Token: 0x02000338 RID: 824
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x06002BEC RID: 11244 RVA: 0x001534AC File Offset: 0x001526AC
			public CompleteOnInvokePromise(IList<Task> tasks, bool isSyncBlocking)
			{
				this._tasks = tasks;
				if (isSyncBlocking)
				{
					this._stateFlags = 2;
				}
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationBegin(base.Id, "TaskFactory.ContinueWhenAny", 0L);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06002BED RID: 11245 RVA: 0x00153504 File Offset: 0x00152704
			public void Invoke(Task completingTask)
			{
				int stateFlags = this._stateFlags;
				int num = stateFlags & 2;
				if ((stateFlags & 1) == 0 && Interlocked.Exchange(ref this._stateFlags, num | 1) == num)
				{
					if (TplEventSource.Log.IsEnabled())
					{
						TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Choice);
						TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(this);
					}
					bool flag = base.TrySetResult(completingTask);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06002BEE RID: 11246 RVA: 0x001535C6 File Offset: 0x001527C6
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return (this._stateFlags & 2) == 0;
				}
			}

			// Token: 0x04000C1E RID: 3102
			private IList<Task> _tasks;

			// Token: 0x04000C1F RID: 3103
			private int _stateFlags;
		}
	}
}
