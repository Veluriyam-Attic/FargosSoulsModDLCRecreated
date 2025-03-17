using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200012F RID: 303
	[NullableContext(2)]
	public interface IFormattable
	{
		// Token: 0x06000F6D RID: 3949
		[return: Nullable(1)]
		string ToString(string format, IFormatProvider formatProvider);
	}
}
