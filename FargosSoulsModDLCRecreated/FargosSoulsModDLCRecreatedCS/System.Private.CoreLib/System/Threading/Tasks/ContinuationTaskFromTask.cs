using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000326 RID: 806
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x06002B4E RID: 11086 RVA: 0x00151928 File Offset: 0x00150B28
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions) : base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0015195C File Offset: 0x00150B5C
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04000BFC RID: 3068
		private Task m_antecedent;
	}
}
