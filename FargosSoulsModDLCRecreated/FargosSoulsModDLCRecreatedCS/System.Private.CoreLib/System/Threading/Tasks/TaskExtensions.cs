using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000334 RID: 820
	[NullableContext(1)]
	[Nullable(0)]
	public static class TaskExtensions
	{
		// Token: 0x06002B88 RID: 11144 RVA: 0x00152669 File Offset: 0x00151869
		public static Task Unwrap(this Task<Task> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			Task result;
			if (task.IsCompletedSuccessfully)
			{
				if ((result = task.Result) == null)
				{
					return Task.FromCanceled(new CancellationToken(true));
				}
			}
			else
			{
				result = Task.CreateUnwrapPromise<VoidTaskResult>(task, false);
			}
			return result;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0015269E File Offset: 0x0015189E
		public static Task<TResult> Unwrap<[Nullable(2)] TResult>(this Task<Task<TResult>> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			Task<TResult> result;
			if (task.IsCompletedSuccessfully)
			{
				if ((result = task.Result) == null)
				{
					return Task.FromCanceled<TResult>(new CancellationToken(true));
				}
			}
			else
			{
				result = Task.CreateUnwrapPromise<TResult>(task, false);
			}
			return result;
		}
	}
}
