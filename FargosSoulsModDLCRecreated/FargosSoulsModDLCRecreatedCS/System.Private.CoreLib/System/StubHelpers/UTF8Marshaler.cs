using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200039C RID: 924
	internal static class UTF8Marshaler
	{
		// Token: 0x0600309D RID: 12445 RVA: 0x00167C34 File Offset: 0x00166E34
		internal unsafe static IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			byte* ptr = (byte*)((void*)pNativeBuffer);
			int num;
			if (ptr != null)
			{
				num = (strManaged.Length + 1) * 3;
				num = strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			else
			{
				num = Encoding.UTF8.GetByteCount(strManaged);
				ptr = (byte*)((void*)Marshal.AllocCoTaskMem(num + 1));
				strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x00167CA4 File Offset: 0x00166EA4
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			byte* ptr = (byte*)((void*)cstr);
			int byteLength = string.strlen(ptr);
			return string.CreateStringFromEncoding(ptr, byteLength, Encoding.UTF8);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x00167CDA File Offset: 0x00166EDA
		internal static void ClearNative(IntPtr pNative)
		{
			if (pNative != IntPtr.Zero)
			{
				Interop.Ole32.CoTaskMemFree(pNative);
			}
		}
	}
}
