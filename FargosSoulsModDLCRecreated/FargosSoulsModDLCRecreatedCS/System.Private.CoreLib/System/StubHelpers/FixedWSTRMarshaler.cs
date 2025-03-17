using System;

namespace System.StubHelpers
{
	// Token: 0x020003A1 RID: 929
	internal static class FixedWSTRMarshaler
	{
		// Token: 0x060030AB RID: 12459 RVA: 0x00167F90 File Offset: 0x00167190
		internal unsafe static void ConvertToNative(string strManaged, IntPtr nativeHome, int length)
		{
			ReadOnlySpan<char> readOnlySpan = strManaged;
			Span<char> destination = new Span<char>((void*)nativeHome, length);
			int num = Math.Min(readOnlySpan.Length, length - 1);
			readOnlySpan.Slice(0, num).CopyTo(destination);
			*destination[num] = '\0';
		}
	}
}
