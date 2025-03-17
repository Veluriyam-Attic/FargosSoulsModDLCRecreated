using System;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x0200039F RID: 927
	internal static class VBByValStrMarshaler
	{
		// Token: 0x060030A5 RID: 12453 RVA: 0x00167E8C File Offset: 0x0016708C
		internal unsafe static IntPtr ConvertToNative(string strManaged, bool fBestFit, bool fThrowOnUnmappableChar, ref int cch)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			cch = strManaged.Length;
			int cb = checked(4 + (cch + 1) * Marshal.SystemMaxDBCSCharSize);
			byte* ptr = (byte*)((void*)Marshal.AllocCoTaskMem(cb));
			int* ptr2 = (int*)ptr;
			ptr += 4;
			if (cch == 0)
			{
				*ptr = 0;
				*ptr2 = 0;
			}
			else
			{
				int num;
				byte[] array = AnsiCharMarshaler.DoAnsiConversion(strManaged, fBestFit, fThrowOnUnmappableChar, out num);
				fixed (byte* ptr3 = &array[0])
				{
					byte* src = ptr3;
					Buffer.Memcpy(ptr, src, num);
				}
				ptr[num] = 0;
				*ptr2 = num;
			}
			return new IntPtr((void*)ptr);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x00167F0C File Offset: 0x0016710C
		internal unsafe static string ConvertToManaged(IntPtr pNative, int cch)
		{
			if (IntPtr.Zero == pNative)
			{
				return null;
			}
			return new string((sbyte*)((void*)pNative), 0, cch);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x00167F2A File Offset: 0x0016712A
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Interop.Ole32.CoTaskMemFree((IntPtr)((long)pNative - 4L));
			}
		}
	}
}
