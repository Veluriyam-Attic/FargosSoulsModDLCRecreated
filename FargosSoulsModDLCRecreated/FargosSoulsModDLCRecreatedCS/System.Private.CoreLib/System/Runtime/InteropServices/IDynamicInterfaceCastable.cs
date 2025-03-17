using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200048B RID: 1163
	public interface IDynamicInterfaceCastable
	{
		// Token: 0x06004491 RID: 17553
		bool IsInterfaceImplemented(RuntimeTypeHandle interfaceType, bool throwIfNotImplemented);

		// Token: 0x06004492 RID: 17554
		RuntimeTypeHandle GetInterfaceImplementation(RuntimeTypeHandle interfaceType);
	}
}
