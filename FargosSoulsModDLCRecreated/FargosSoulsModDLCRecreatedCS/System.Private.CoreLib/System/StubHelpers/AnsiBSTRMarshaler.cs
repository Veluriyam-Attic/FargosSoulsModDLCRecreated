using System;

namespace System.StubHelpers
{
	// Token: 0x020003A0 RID: 928
	internal static class AnsiBSTRMarshaler
	{
		// Token: 0x060030A8 RID: 12456 RVA: 0x00167F4C File Offset: 0x0016714C
		internal static IntPtr ConvertToNative(int flags, string strManaged)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			byte[] str = null;
			int len = 0;
			if (strManaged.Length > 0)
			{
				str = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out len);
			}
			return Interop.OleAut32.SysAllocStringByteLen(str, (uint)len);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x00167ADC File Offset: 0x00166CDC
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			return new string((sbyte*)((void*)bstr));
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x00167E77 File Offset: 0x00167077
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Interop.OleAut32.SysFreeString(pNative);
			}
		}
	}
}
