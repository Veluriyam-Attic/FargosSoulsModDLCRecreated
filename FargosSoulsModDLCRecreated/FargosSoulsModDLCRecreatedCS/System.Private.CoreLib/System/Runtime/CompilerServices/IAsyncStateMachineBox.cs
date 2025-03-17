using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000536 RID: 1334
	internal interface IAsyncStateMachineBox
	{
		// Token: 0x06004738 RID: 18232
		void MoveNext();

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06004739 RID: 18233
		Action MoveNextAction { get; }

		// Token: 0x0600473A RID: 18234
		IAsyncStateMachine GetStateMachineObject();
	}
}
