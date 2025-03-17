using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007BC RID: 1980
	[Obsolete("Please use IEqualityComparer instead.")]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public interface IHashCodeProvider
	{
		// Token: 0x06005FA7 RID: 24487
		int GetHashCode(object obj);
	}
}
