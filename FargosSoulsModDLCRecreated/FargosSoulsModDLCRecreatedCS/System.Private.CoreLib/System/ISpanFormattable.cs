using System;

namespace System
{
	// Token: 0x0200013F RID: 319
	internal interface ISpanFormattable
	{
		// Token: 0x06001030 RID: 4144
		bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider);
	}
}
