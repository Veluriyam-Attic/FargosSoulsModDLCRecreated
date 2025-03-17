using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005DE RID: 1502
	[NullableContext(1)]
	[Nullable(0)]
	public struct InterfaceMapping
	{
		// Token: 0x04001360 RID: 4960
		public Type TargetType;

		// Token: 0x04001361 RID: 4961
		public Type InterfaceType;

		// Token: 0x04001362 RID: 4962
		public MethodInfo[] TargetMethods;

		// Token: 0x04001363 RID: 4963
		public MethodInfo[] InterfaceMethods;
	}
}
