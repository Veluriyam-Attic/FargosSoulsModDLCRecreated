using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000488 RID: 1160
	[NullableContext(1)]
	public interface ICustomFactory
	{
		// Token: 0x0600448A RID: 17546
		MarshalByRefObject CreateInstance(Type serverType);
	}
}
