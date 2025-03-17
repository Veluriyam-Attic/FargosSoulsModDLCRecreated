using System;

namespace System.IO
{
	// Token: 0x020006C0 RID: 1728
	internal static class DriveInfoInternal
	{
		// Token: 0x06005835 RID: 22581 RVA: 0x001AF6E4 File Offset: 0x001AE8E4
		public unsafe static string[] GetLogicalDrives()
		{
			int logicalDrives = Interop.Kernel32.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			IntPtr intPtr = stackalloc byte[(UIntPtr)6];
			*intPtr = 65;
			*(intPtr + 2) = 58;
			*(intPtr + (IntPtr)2 * 2) = 92;
			Span<char> span = new Span<char>(intPtr, 3);
			Span<char> span2 = span;
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = span2.ToString();
				}
				num >>= 1;
				ref char ptr = ref span2[0];
				ptr += '\u0001';
			}
			return array;
		}
	}
}
