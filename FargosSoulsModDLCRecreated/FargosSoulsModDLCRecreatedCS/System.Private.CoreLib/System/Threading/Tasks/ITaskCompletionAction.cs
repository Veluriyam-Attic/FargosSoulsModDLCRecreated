using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031F RID: 799
	internal interface ITaskCompletionAction
	{
		// Token: 0x06002B19 RID: 11033
		void Invoke(Task completingTask);

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002B1A RID: 11034
		bool InvokeMayRunArbitraryCode { get; }
	}
}
