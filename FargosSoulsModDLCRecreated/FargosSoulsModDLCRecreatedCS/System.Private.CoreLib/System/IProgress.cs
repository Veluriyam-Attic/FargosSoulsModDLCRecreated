using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013E RID: 318
	[NullableContext(1)]
	public interface IProgress<[Nullable(2)] in T>
	{
		// Token: 0x0600102F RID: 4143
		void Report(T value);
	}
}
