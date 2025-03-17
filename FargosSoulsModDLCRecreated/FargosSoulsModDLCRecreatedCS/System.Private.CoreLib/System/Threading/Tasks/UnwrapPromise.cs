using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000320 RID: 800
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06002B1B RID: 11035 RVA: 0x001511C0 File Offset: 0x001503C0
		public UnwrapPromise(Task outerTask, bool lookForOce) : base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationBegin(base.Id, "Task.Unwrap", 0L);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this, false);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0015122C File Offset: 0x0015042C
		public void Invoke(Task completingTask)
		{
			if (RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				this.InvokeCore(completingTask);
				return;
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x00151244 File Offset: 0x00150444
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			bool flag = this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00151278 File Offset: 0x00150478
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x001512A8 File Offset: 0x001504A8
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					bool flag = this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x00151300 File Offset: 0x00150500
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (TplEventSource.Log.IsEnabled())
			{
				TplEventSource.Log.TraceOperationRelation(base.Id, CausalityRelation.Join);
			}
			bool result = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				if (TplEventSource.Log.IsEnabled())
				{
					TplEventSource.Log.TraceOperationEnd(base.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this);
				}
				Task<TResult> task2 = task as Task<TResult>;
				result = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				result = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null)
				{
					OperationCanceledException ex = exceptionDispatchInfo.SourceException as OperationCanceledException;
					if (ex != null)
					{
						result = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
						break;
					}
				}
				result = base.TrySetException(exceptionDispatchInfos);
				break;
			}
			}
			return result;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x00151400 File Offset: 0x00150600
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this, false);
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool InvokeMayRunArbitraryCode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000BF5 RID: 3061
		private byte _state;

		// Token: 0x04000BF6 RID: 3062
		private readonly bool _lookForOce;
	}
}
