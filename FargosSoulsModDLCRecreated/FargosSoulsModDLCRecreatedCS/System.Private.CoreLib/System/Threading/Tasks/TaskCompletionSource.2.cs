using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000325 RID: 805
	[NullableContext(1)]
	[Nullable(0)]
	public class TaskCompletionSource<[Nullable(2)] TResult>
	{
		// Token: 0x06002B3F RID: 11071 RVA: 0x0015171A File Offset: 0x0015091A
		public TaskCompletionSource()
		{
			this._task = new Task<TResult>();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0015172D File Offset: 0x0015092D
		public TaskCompletionSource(TaskCreationOptions creationOptions) : this(null, creationOptions)
		{
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00151737 File Offset: 0x00150937
		[NullableContext(2)]
		public TaskCompletionSource(object state) : this(state, TaskCreationOptions.None)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00151741 File Offset: 0x00150941
		[NullableContext(2)]
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this._task = new Task<TResult>(state, creationOptions);
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x00151756 File Offset: 0x00150956
		public Task<TResult> Task
		{
			get
			{
				return this._task;
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0015175E File Offset: 0x0015095E
		public void SetException(Exception exception)
		{
			if (!this.TrySetException(exception))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00151770 File Offset: 0x00150970
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00151784 File Offset: 0x00150984
		public bool TrySetException(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			bool flag = this._task.TrySetException(exception);
			if (!flag && !this._task.IsCompleted)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x001517C4 File Offset: 0x001509C4
		public bool TrySetException(IEnumerable<Exception> exceptions)
		{
			if (exceptions == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exceptions);
			}
			List<Exception> list = new List<Exception>();
			foreach (Exception ex in exceptions)
			{
				if (ex == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.TaskCompletionSourceT_TrySetException_NullException, ExceptionArgument.exceptions);
				}
				list.Add(ex);
			}
			if (list.Count == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.TaskCompletionSourceT_TrySetException_NoExceptions, ExceptionArgument.exceptions);
			}
			bool flag = this._task.TrySetException(list);
			if (!flag && !this._task.IsCompleted)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x00151864 File Offset: 0x00150A64
		public void SetResult(TResult result)
		{
			if (!this.TrySetResult(result))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00151878 File Offset: 0x00150A78
		public bool TrySetResult(TResult result)
		{
			bool flag = this._task.TrySetResult(result);
			if (!flag)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x001518A4 File Offset: 0x00150AA4
		public void SetCanceled()
		{
			this.SetCanceled(default(CancellationToken));
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x001518C0 File Offset: 0x00150AC0
		public void SetCanceled(CancellationToken cancellationToken)
		{
			if (!this.TrySetCanceled(cancellationToken))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x001518D4 File Offset: 0x00150AD4
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x001518F0 File Offset: 0x00150AF0
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this._task.TrySetCanceled(cancellationToken);
			if (!flag && !this._task.IsCompleted)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x04000BFB RID: 3067
		private readonly Task<TResult> _task;
	}
}
