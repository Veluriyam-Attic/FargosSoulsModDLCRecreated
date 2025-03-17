using System;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x0200039B RID: 923
	internal static class CSTRMarshaler
	{
		// Token: 0x06003098 RID: 12440 RVA: 0x001679D0 File Offset: 0x00166BD0
		internal unsafe static IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			byte* ptr = (byte*)((void*)pNativeBuffer);
			int num;
			if (ptr != null || Marshal.SystemMaxDBCSCharSize == 1)
			{
				num = checked((strManaged.Length + 1) * Marshal.SystemMaxDBCSCharSize + 1);
				bool flag = false;
				if (ptr == null)
				{
					ptr = (byte*)((void*)Marshal.AllocCoTaskMem(num));
					flag = true;
				}
				try
				{
					num = Marshal.StringToAnsiString(strManaged, ptr, num, (flags & 255) != 0, flags >> 8 != 0);
					goto IL_DA;
				}
				catch (Exception obj) when (flag)
				{
					Marshal.FreeCoTaskMem((IntPtr)((void*)ptr));
					throw;
				}
			}
			if (strManaged.Length == 0)
			{
				num = 0;
				ptr = (byte*)((void*)Marshal.AllocCoTaskMem(2));
			}
			else
			{
				byte[] array = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out num);
				ptr = (byte*)((void*)Marshal.AllocCoTaskMem(num + 2));
				fixed (byte* ptr2 = &array[0])
				{
					byte* src = ptr2;
					Buffer.Memcpy(ptr, src, num);
				}
			}
			IL_DA:
			ptr[num] = 0;
			ptr[num + 1] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x00167ADC File Offset: 0x00166CDC
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			return new string((sbyte*)((void*)cstr));
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x00167AF8 File Offset: 0x00166CF8
		internal static void ClearNative(IntPtr pNative)
		{
			Interop.Ole32.CoTaskMemFree(pNative);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x00167B00 File Offset: 0x00166D00
		internal unsafe static void ConvertFixedToNative(int flags, string strManaged, IntPtr pNativeBuffer, int length)
		{
			if (strManaged == null)
			{
				if (length > 0)
				{
					*(byte*)((void*)pNativeBuffer) = 0;
				}
				return;
			}
			int num = strManaged.Length;
			if (num >= length)
			{
				num = length - 1;
			}
			byte* ptr = (byte*)((void*)pNativeBuffer);
			bool flag = flags >> 8 != 0;
			bool flag2 = (flags & 255) != 0;
			uint num2 = 0U;
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
			char* lpWideCharStr = ptr2;
			int num3 = Interop.Kernel32.WideCharToMultiByte(0U, flag2 ? 0U : 1024U, lpWideCharStr, num, ptr, length, IntPtr.Zero, flag ? new IntPtr((void*)(&num2)) : IntPtr.Zero);
			char* ptr3 = null;
			if (num2 != 0U)
			{
				throw new ArgumentException(SR.Interop_Marshal_Unmappable_Char);
			}
			if (num3 == length)
			{
				num3--;
			}
			ptr[num3] = 0;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x00167BB0 File Offset: 0x00166DB0
		internal unsafe static string ConvertFixedToManaged(IntPtr cstr, int length)
		{
			int num = length + 2;
			if (num < length)
			{
				throw new OutOfMemoryException();
			}
			Span<sbyte> span = new Span<sbyte>((void*)cstr, length);
			int num2 = num;
			Span<sbyte> span2 = new Span<sbyte>(stackalloc byte[(UIntPtr)num2], num2);
			Span<sbyte> destination = span2;
			span.CopyTo(destination);
			*destination[length - 1] = 0;
			*destination[length] = 0;
			*destination[length + 1] = 0;
			fixed (sbyte* pinnableReference = destination.GetPinnableReference())
			{
				sbyte* ptr = pinnableReference;
				return new string(ptr, 0, string.strlen((byte*)ptr));
			}
		}
	}
}
