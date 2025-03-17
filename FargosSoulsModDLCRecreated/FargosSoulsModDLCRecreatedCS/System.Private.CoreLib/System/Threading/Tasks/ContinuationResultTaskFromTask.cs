using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000327 RID: 807
	internal sealed class ContinuationResultTaskFromTask<TResult> : Task<TResult>
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x001519B4 File Offset: 0x00150BB4
		public ContinuationResultTaskFromTask(Task antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions) : base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x001519E8 File Offset: 0x00150BE8
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task, TResult> func = this.m_action as Func<Task, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task, object, TResult> func2 = this.m_action as Func<Task, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04000BFD RID: 3069
		private Task m_antecedent;
	}
}
