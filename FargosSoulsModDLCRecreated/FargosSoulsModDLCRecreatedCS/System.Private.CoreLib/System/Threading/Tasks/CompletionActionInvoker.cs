using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000319 RID: 793
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x00151113 File Offset: 0x00150313
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x00151129 File Offset: 0x00150329
		void IThreadPoolWorkItem.Execute()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x04000BD2 RID: 3026
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04000BD3 RID: 3027
		private readonly Task m_completingTask;
	}
}
