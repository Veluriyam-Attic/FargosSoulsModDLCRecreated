using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200039A RID: 922
	internal static class AnsiCharMarshaler
	{
		// Token: 0x06003095 RID: 12437 RVA: 0x00167934 File Offset: 0x00166B34
		internal unsafe static byte[] DoAnsiConversion(string str, bool fBestFit, bool fThrowOnUnmappableChar, out int cbLength)
		{
			byte[] array = new byte[checked((str.Length + 1) * Marshal.SystemMaxDBCSCharSize)];
			fixed (byte* ptr = &array[0])
			{
				byte* buffer = ptr;
				cbLength = Marshal.StringToAnsiString(str, buffer, array.Length, fBestFit, fThrowOnUnmappableChar);
			}
			return array;
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x00167974 File Offset: 0x00166B74
		internal unsafe static byte ConvertToNative(char managedChar, bool fBestFit, bool fThrowOnUnmappableChar)
		{
			int num = 2 * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = Marshal.StringToAnsiString(managedChar.ToString(), ptr, num, fBestFit, fThrowOnUnmappableChar);
			return *ptr;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x001679A4 File Offset: 0x00166BA4
		internal static char ConvertToManaged(byte nativeChar)
		{
			ReadOnlySpan<byte> bytes = new ReadOnlySpan<byte>(ref nativeChar, 1);
			string @string = Encoding.Default.GetString(bytes);
			return @string[0];
		}
	}
}
