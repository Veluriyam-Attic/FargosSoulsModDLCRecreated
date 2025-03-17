using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000346 RID: 838
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder))]
	[Nullable(0)]
	[NullableContext(1)]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask : IEquatable<ValueTask>
	{
		// Token: 0x06002C47 RID: 11335 RVA: 0x001544CB File Offset: 0x001536CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x001544EC File Offset: 0x001536EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._continueOnCapturedContext = true;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0015450D File Offset: 0x0015370D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x00154524 File Offset: 0x00153724
		public static ValueTask CompletedTask
		{
			get
			{
				return default(ValueTask);
			}
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0015453A File Offset: 0x0015373A
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ValueTask<TResult> FromResult<[Nullable(2)] TResult>(TResult result)
		{
			return new ValueTask<TResult>(result);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x00154542 File Offset: 0x00153742
		public static ValueTask FromCanceled(CancellationToken cancellationToken)
		{
			return new ValueTask(Task.FromCanceled(cancellationToken));
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0015454F File Offset: 0x0015374F
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ValueTask<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
		{
			return new ValueTask<TResult>(Task.FromCanceled<TResult>(cancellationToken));
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0015455C File Offset: 0x0015375C
		public static ValueTask FromException(Exception exception)
		{
			return new ValueTask(Task.FromException(exception));
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00154569 File Offset: 0x00153769
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ValueTask<TResult> FromException<[Nullable(2)] TResult>(Exception exception)
		{
			return new ValueTask<TResult>(Task.FromException<TResult>(exception));
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x00154576 File Offset: 0x00153776
		public override int GetHashCode()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x00154589 File Offset: 0x00153789
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTask && this.Equals((ValueTask)obj);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x001545A1 File Offset: 0x001537A1
		public bool Equals(ValueTask other)
		{
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x001545C1 File Offset: 0x001537C1
		public static bool operator ==(ValueTask left, ValueTask right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x001545CB File Offset: 0x001537CB
		public static bool operator !=(ValueTask left, ValueTask right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x001545D8 File Offset: 0x001537D8
		public Task AsTask()
		{
			object obj = this._obj;
			Task result;
			if (obj != null)
			{
				if ((result = (obj as Task)) == null)
				{
					return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource>(obj));
				}
			}
			else
			{
				result = Task.CompletedTask;
			}
			return result;
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0015460B File Offset: 0x0015380B
		public ValueTask Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask(this.AsTask());
			}
			return this;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x00154628 File Offset: 0x00153828
		private Task GetTaskForValueTaskSource(IValueTaskSource t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					t.GetResult(this._token);
					return Task.CompletedTask;
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task task = new Task();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					return ValueTask.s_canceledTask;
				}
			}
			return new ValueTask.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x001546B4 File Offset: 0x001538B4
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x001546F4 File Offset: 0x001538F4
		public bool IsCompletedSuccessfully
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x00154734 File Offset: 0x00153934
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00154774 File Offset: 0x00153974
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x001547B4 File Offset: 0x001539B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ThrowIfCompletedUnsuccessfully()
		{
			object obj = this._obj;
			if (obj != null)
			{
				Task task = obj as Task;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return;
				}
				Unsafe.As<IValueTaskSource>(obj).GetResult(this._token);
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x001547ED File Offset: 0x001539ED
		public ValueTaskAwaiter GetAwaiter()
		{
			return new ValueTaskAwaiter(ref this);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x001547F8 File Offset: 0x001539F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			ValueTask valueTask = new ValueTask(this._obj, this._token, continueOnCapturedContext);
			return new ConfiguredValueTaskAwaitable(ref valueTask);
		}

		// Token: 0x04000C4F RID: 3151
		private static readonly Task s_canceledTask = Task.FromCanceled(new CancellationToken(true));

		// Token: 0x04000C50 RID: 3152
		internal readonly object _obj;

		// Token: 0x04000C51 RID: 3153
		internal readonly short _token;

		// Token: 0x04000C52 RID: 3154
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x02000347 RID: 839
		private sealed class ValueTaskSourceAsTask : Task
		{
			// Token: 0x06002C60 RID: 11360 RVA: 0x00154831 File Offset: 0x00153A31
			internal ValueTaskSourceAsTask(IValueTaskSource source, short token)
			{
				this._token = token;
				this._source = source;
				source.OnCompleted(ValueTask.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x04000C53 RID: 3155
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							source.GetResult(valueTaskSourceAsTask._token);
							valueTaskSourceAsTask.TrySetResult();
						}
						catch (Exception ex)
						{
							if (status == ValueTaskSourceStatus.Canceled)
							{
								OperationCanceledException ex2 = ex as OperationCanceledException;
								if (ex2 != null)
								{
									valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
								}
								else
								{
									valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
								}
							}
							else
							{
								valueTaskSourceAsTask.TrySetException(ex);
							}
						}
						return;
					}
				}
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
			};

			// Token: 0x04000C54 RID: 3156
			private IValueTaskSource _source;

			// Token: 0x04000C55 RID: 3157
			private readonly short _token;
		}
	}
}
