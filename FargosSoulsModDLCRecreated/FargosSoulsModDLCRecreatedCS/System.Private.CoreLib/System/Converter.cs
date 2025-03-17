using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000BB RID: 187
	// (Invoke) Token: 0x0600098D RID: 2445
	public delegate TOutput Converter<[Nullable(2)] in TInput, [Nullable(2)] out TOutput>(TInput input);
}
