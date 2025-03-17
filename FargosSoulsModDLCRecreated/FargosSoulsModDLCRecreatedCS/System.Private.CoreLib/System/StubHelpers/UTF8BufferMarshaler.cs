using System;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200039D RID: 925
	internal static class UTF8BufferMarshaler
	{
		// Token: 0x060030A0 RID: 12448 RVA: 0x00167CF0 File Offset: 0x00166EF0
		internal unsafe static IntPtr ConvertToNative(StringBuilder sb, IntPtr pNativeBuffer, int flags)
		{
			if (sb == null)
			{
				return IntPtr.Zero;
			}
			string text = sb.ToString();
			int num = Encoding.UTF8.GetByteCount(text);
			byte* ptr = (byte*)((void*)pNativeBuffer);
			num = text.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x00167D3C File Offset: 0x00166F3C
		internal unsafe static void ConvertToManaged(StringBuilder sb, IntPtr pNative)
		{
			if (pNative == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)((void*)pNative);
			int length = string.strlen(ptr);
			sb.ReplaceBufferUtf8Internal(new ReadOnlySpan<byte>((void*)ptr, length));
		}
	}
}
