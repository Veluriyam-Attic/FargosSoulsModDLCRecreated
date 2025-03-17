using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
using Internal.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000349 RID: 841
	[NullableContext(1)]
	[Nullable(0)]
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask<[Nullable(2)] TResult> : IEquatable<ValueTask<TResult>>
	{
		// Token: 0x06002C65 RID: 11365 RVA: 0x00154918 File Offset: 0x00153B18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(TResult result)
		{
			this._result = result;
			this._obj = null;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x00154936 File Offset: 0x00153B36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task<TResult> task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00154963 File Offset: 0x00153B63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource<TResult> source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x00154990 File Offset: 0x00153B90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, TResult result, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._result = result;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x001549B0 File Offset: 0x00153BB0
		public override int GetHashCode()
		{
			if (this._obj != null)
			{
				return this._obj.GetHashCode();
			}
			if (this._result == null)
			{
				return 0;
			}
			TResult result = this._result;
			return result.GetHashCode();
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x001549F4 File Offset: 0x00153BF4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTask<TResult> && this.Equals((ValueTask<TResult>)obj);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x00154A0C File Offset: 0x00153C0C
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] ValueTask<TResult> other)
		{
			if (this._obj == null && other._obj == null)
			{
				return EqualityComparer<TResult>.Default.Equals(this._result, other._result);
			}
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00154A5E File Offset: 0x00153C5E
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] ValueTask<TResult> left, [Nullable(new byte[]
		{
			0,
			1
		})] ValueTask<TResult> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x00154A68 File Offset: 0x00153C68
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] ValueTask<TResult> left, [Nullable(new byte[]
		{
			0,
			1
		})] ValueTask<TResult> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x00154A78 File Offset: 0x00153C78
		public Task<TResult> AsTask()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return AsyncTaskMethodBuilder<TResult>.GetTaskForResult(this._result);
			}
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				return task;
			}
			return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource<TResult>>(obj));
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x00154AB3 File Offset: 0x00153CB3
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ValueTask<TResult> Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask<TResult>(this.AsTask());
			}
			return this;
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x00154AD0 File Offset: 0x00153CD0
		private Task<TResult> GetTaskForValueTaskSource(IValueTaskSource<TResult> t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					return AsyncTaskMethodBuilder<TResult>.GetTaskForResult(t.GetResult(this._token));
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException<TResult>(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task<TResult> task = new Task<TResult>();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					Task<TResult> task2 = ValueTask<TResult>.s_canceledTask;
					if (task2 == null)
					{
						task2 = (ValueTask<TResult>.s_canceledTask = Task.FromCanceled<TResult>(new CancellationToken(true)));
					}
					return task2;
				}
			}
			return new ValueTask<TResult>.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x00154B78 File Offset: 0x00153D78
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
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x00154BB8 File Offset: 0x00153DB8
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
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x00154BF8 File Offset: 0x00153DF8
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00154C38 File Offset: 0x00153E38
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x00154C78 File Offset: 0x00153E78
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TResult Result
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return this._result;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return task.ResultOnSuccess;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetResult(this._token);
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x00154CBE File Offset: 0x00153EBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ValueTaskAwaiter<TResult> GetAwaiter()
		{
			return new ValueTaskAwaiter<TResult>(ref this);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00154CC8 File Offset: 0x00153EC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ConfiguredValueTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			ValueTask<TResult> valueTask = new ValueTask<TResult>(this._obj, this._result, this._token, continueOnCapturedContext);
			return new ConfiguredValueTaskAwaitable<TResult>(ref valueTask);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x00154CF8 File Offset: 0x00153EF8
		[NullableContext(2)]
		public override string ToString()
		{
			if (this.IsCompletedSuccessfully)
			{
				Debugger.NotifyOfCrossThreadDependency();
				TResult result = this.Result;
				if (result != null)
				{
					return result.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x04000C57 RID: 3159
		private static Task<TResult> s_canceledTask;

		// Token: 0x04000C58 RID: 3160
		internal readonly object _obj;

		// Token: 0x04000C59 RID: 3161
		internal readonly TResult _result;

		// Token: 0x04000C5A RID: 3162
		internal readonly short _token;

		// Token: 0x04000C5B RID: 3163
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x0200034A RID: 842
		private sealed class ValueTaskSourceAsTask : Task<TResult>
		{
			// Token: 0x06002C79 RID: 11385 RVA: 0x00154D34 File Offset: 0x00153F34
			public ValueTaskSourceAsTask(IValueTaskSource<TResult> source, short token)
			{
				this._source = source;
				this._token = token;
				source.OnCompleted(ValueTask<TResult>.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x04000C5C RID: 3164
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask<TResult>.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask<TResult>.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource<TResult> source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							valueTaskSourceAsTask.TrySetResult(source.GetResult(valueTaskSourceAsTask._token));
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

			// Token: 0x04000C5D RID: 3165
			private IValueTaskSource<TResult> _source;

			// Token: 0x04000C5E RID: 3166
			private readonly short _token;
		}
	}
}
