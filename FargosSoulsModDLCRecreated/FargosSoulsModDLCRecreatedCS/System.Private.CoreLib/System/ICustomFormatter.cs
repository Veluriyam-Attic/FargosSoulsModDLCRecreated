using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200012B RID: 299
	[NullableContext(2)]
	public interface ICustomFormatter
	{
		// Token: 0x06000F69 RID: 3945
		[return: Nullable(1)]
		string Format(string format, object arg, IFormatProvider formatProvider);
	}
}
