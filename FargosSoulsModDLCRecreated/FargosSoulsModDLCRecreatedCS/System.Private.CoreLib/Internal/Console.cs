using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Internal
{
	// Token: 0x02000811 RID: 2065
	public static class Console
	{
		// Token: 0x0600623D RID: 25149 RVA: 0x001D36C4 File Offset: 0x001D28C4
		[NullableContext(1)]
		public unsafe static void Write(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] array;
			byte* bytes2;
			if ((array = bytes) == null || array.Length == 0)
			{
				bytes2 = null;
			}
			else
			{
				bytes2 = &array[0];
			}
			int num;
			Interop.Kernel32.WriteFile(Console._outputHandle, bytes2, bytes.Length, out num, IntPtr.Zero);
			array = null;
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x001D370D File Offset: 0x001D290D
		[NullableContext(2)]
		public static void WriteLine(string s)
		{
			Console.Write(s + "\r\n");
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x001D371F File Offset: 0x001D291F
		public static void WriteLine()
		{
			Console.Write("\r\n");
		}

		// Token: 0x04001D51 RID: 7505
		private static readonly SafeFileHandle _outputHandle = new SafeFileHandle(Interop.Kernel32.GetStdHandle(-11), false);
	}
}
