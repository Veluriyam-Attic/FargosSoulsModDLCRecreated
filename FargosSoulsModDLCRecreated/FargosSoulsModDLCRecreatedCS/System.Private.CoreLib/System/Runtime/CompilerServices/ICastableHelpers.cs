using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004F1 RID: 1265
	internal static class ICastableHelpers
	{
		// Token: 0x06004606 RID: 17926 RVA: 0x0017A869 File Offset: 0x00179A69
		internal static bool IsInstanceOfInterface(ICastable castable, RuntimeType type, [NotNullWhen(true)] out Exception castError)
		{
			return castable.IsInstanceOfInterface(new RuntimeTypeHandle(type), out castError);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x0017A878 File Offset: 0x00179A78
		internal static RuntimeType GetImplType(ICastable castable, RuntimeType interfaceType)
		{
			return castable.GetImplType(new RuntimeTypeHandle(interfaceType)).GetRuntimeType();
		}
	}
}
