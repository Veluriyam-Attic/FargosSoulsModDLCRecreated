using System;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x0200039E RID: 926
	internal static class BSTRMarshaler
	{
		// Token: 0x060030A2 RID: 12450 RVA: 0x00167D74 File Offset: 0x00166F74
		internal unsafe static IntPtr ConvertToNative(string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			byte b;
			bool flag = strManaged.TryGetTrailByte(out b);
			uint num = (uint)(strManaged.Length * 2);
			if (flag)
			{
				num += 1U;
			}
			byte* ptr;
			if (pNativeBuffer != IntPtr.Zero)
			{
				*(int*)((void*)pNativeBuffer) = (int)num;
				ptr = (byte*)((void*)pNativeBuffer) + 4;
			}
			else
			{
				ptr = (byte*)((void*)Interop.OleAut32.SysAllocStringByteLen(null, num));
				if (ptr == null)
				{
					throw new OutOfMemoryException();
				}
			}
			char* ptr2;
			if (strManaged == null)
			{
				ptr2 = null;
			}
			else
			{
				fixed (char* ptr3 = strManaged.GetPinnableReference())
				{
					ptr2 = ptr3;
				}
			}
			char* src = ptr2;
			Buffer.Memcpy(ptr, (byte*)src, (strManaged.Length + 1) * 2);
			char* ptr3 = null;
			if (flag)
			{
				ptr[num - 1U] = b;
			}
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x00167E18 File Offset: 0x00167018
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			uint num = Marshal.SysStringByteLen(bstr);
			StubHelpers.CheckStringLength(num);
			string text;
			if (num == 1U)
			{
				text = string.FastAllocateString(0);
			}
			else
			{
				text = new string((char*)((void*)bstr), 0, (int)(num / 2U));
			}
			if ((num & 1U) == 1U)
			{
				text.SetTrailByte(((byte*)((void*)bstr))[num - 1U]);
			}
			return text;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x00167E77 File Offset: 0x00167077
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Interop.OleAut32.SysFreeString(pNative);
			}
		}
	}
}
