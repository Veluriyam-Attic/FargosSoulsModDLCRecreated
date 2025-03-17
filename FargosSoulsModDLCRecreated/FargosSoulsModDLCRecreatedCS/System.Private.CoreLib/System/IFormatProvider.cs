using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200012E RID: 302
	[NullableContext(2)]
	public interface IFormatProvider
	{
		// Token: 0x06000F6C RID: 3948
		object GetFormat(Type formatType);
	}
}
