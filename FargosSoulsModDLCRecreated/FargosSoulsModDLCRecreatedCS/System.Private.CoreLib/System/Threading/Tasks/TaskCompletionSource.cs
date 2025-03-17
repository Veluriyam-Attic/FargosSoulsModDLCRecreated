using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000324 RID: 804
	[NullableContext(1)]
	[Nullable(0)]
	public class TaskCompletionSource
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x00151510 File Offset: 0x00150710
		public TaskCompletionSource()
		{
			this._task = new Task();
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x00151523 File Offset: 0x00150723
		public TaskCompletionSource(TaskCreationOptions creationOptions) : this(null, creationOptions)
		{
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0015152D File Offset: 0x0015072D
		[NullableContext(2)]
		public TaskCompletionSource(object state) : this(state, TaskCreationOptions.None)
		{
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00151537 File Offset: 0x00150737
		[NullableContext(2)]
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this._task = new Task(state, creationOptions, true);
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x0015154D File Offset: 0x0015074D
		public Task Task
		{
			get
			{
				return this._task;
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00151555 File Offset: 0x00150755
		public void SetException(Exception exception)
		{
			if (!this.TrySetException(exception))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00151567 File Offset: 0x00150767
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0015157C File Offset: 0x0015077C
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

		// Token: 0x06002B38 RID: 11064 RVA: 0x001515BC File Offset: 0x001507BC
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

		// Token: 0x06002B39 RID: 11065 RVA: 0x0015165C File Offset: 0x0015085C
		public void SetResult()
		{
			if (!this.TrySetResult())
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00151670 File Offset: 0x00150870
		public bool TrySetResult()
		{
			bool flag = this._task.TrySetResult();
			if (!flag)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00151698 File Offset: 0x00150898
		public void SetCanceled()
		{
			this.SetCanceled(default(CancellationToken));
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x001516B4 File Offset: 0x001508B4
		public void SetCanceled(CancellationToken cancellationToken)
		{
			if (!this.TrySetCanceled(cancellationToken))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x001516C8 File Offset: 0x001508C8
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x001516E4 File Offset: 0x001508E4
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this._task.TrySetCanceled(cancellationToken);
			if (!flag && !this._task.IsCompleted)
			{
				this._task.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x04000BFA RID: 3066
		private readonly Task _task;
	}
}
