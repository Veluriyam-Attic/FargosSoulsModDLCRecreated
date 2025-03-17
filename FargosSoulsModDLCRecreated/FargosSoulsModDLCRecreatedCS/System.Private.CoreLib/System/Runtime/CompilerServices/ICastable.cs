using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000537 RID: 1335
	[NullableContext(2)]
	public interface ICastable
	{
		// Token: 0x0600473B RID: 18235
		bool IsInstanceOfInterface(RuntimeTypeHandle interfaceType, [NotNullWhen(true)] out Exception castError);

		// Token: 0x0600473C RID: 18236
		RuntimeTypeHandle GetImplType(RuntimeTypeHandle interfaceType);
	}
}
