using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks
{
	// Token: 0x0200034C RID: 844
	internal static class TaskToApm
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x00154E1C File Offset: 0x0015401C
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			return new TaskToApm.TaskAsyncResult(task, state, callback);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x00154E28 File Offset: 0x00154028
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskAsyncResult taskAsyncResult = asyncResult as TaskToApm.TaskAsyncResult;
			if (taskAsyncResult != null)
			{
				taskAsyncResult._task.GetAwaiter().GetResult();
				return;
			}
			TaskToApm.ThrowArgumentException(asyncResult);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x00154E5C File Offset: 0x0015405C
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskAsyncResult taskAsyncResult = asyncResult as TaskToApm.TaskAsyncResult;
			if (taskAsyncResult != null)
			{
				Task<TResult> task = taskAsyncResult._task as Task<TResult>;
				if (task != null)
				{
					return task.GetAwaiter().GetResult();
				}
			}
			TaskToApm.ThrowArgumentException(asyncResult);
			return default(TResult);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00154EA0 File Offset: 0x001540A0
		[DoesNotReturn]
		private static void ThrowArgumentException(IAsyncResult asyncResult)
		{
			throw (asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(null, "asyncResult");
		}

		// Token: 0x0200034D RID: 845
		internal sealed class TaskAsyncResult : IAsyncResult
		{
			// Token: 0x06002C82 RID: 11394 RVA: 0x00154EBC File Offset: 0x001540BC
			internal TaskAsyncResult(Task task, object state, AsyncCallback callback)
			{
				this._task = task;
				this.AsyncState = state;
				if (task.IsCompleted)
				{
					this.CompletedSynchronously = 1;
					if (callback != null)
					{
						callback(this);
						return;
					}
				}
				else if (callback != null)
				{
					this._callback = callback;
					this._task.ConfigureAwait(false).GetAwaiter().OnCompleted(new Action(this.InvokeCallback));
				}
			}

			// Token: 0x06002C83 RID: 11395 RVA: 0x00154F29 File Offset: 0x00154129
			private void InvokeCallback()
			{
				this._callback(this);
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x06002C84 RID: 11396 RVA: 0x00154F37 File Offset: 0x00154137
			public object AsyncState { get; }

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06002C85 RID: 11397 RVA: 0x00154F3F File Offset: 0x0015413F
			public bool CompletedSynchronously { get; }

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06002C86 RID: 11398 RVA: 0x00154F47 File Offset: 0x00154147
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06002C87 RID: 11399 RVA: 0x00154F54 File Offset: 0x00154154
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this._task).AsyncWaitHandle;
				}
			}

			// Token: 0x04000C60 RID: 3168
			internal readonly Task _task;

			// Token: 0x04000C61 RID: 3169
			private readonly AsyncCallback _callback;
		}
	}
}
