using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000535 RID: 1333
	[NullableContext(1)]
	public interface IAsyncStateMachine
	{
		// Token: 0x06004736 RID: 18230
		void MoveNext();

		// Token: 0x06004737 RID: 18231
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
