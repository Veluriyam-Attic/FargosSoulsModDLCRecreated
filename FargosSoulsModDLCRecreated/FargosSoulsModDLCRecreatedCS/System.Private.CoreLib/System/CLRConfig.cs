using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200005A RID: 90
	internal static class CLRConfig
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x000AF969 File Offset: 0x000AEB69
		internal static bool GetBoolValue(string switchName, out bool exist)
		{
			return CLRConfig.GetConfigBoolValue(switchName, out exist);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000AF974 File Offset: 0x000AEB74
		internal unsafe static bool GetBoolValueWithFallbacks(string switchName, string environmentName, bool defaultValue)
		{
			bool flag;
			bool boolValue = CLRConfig.GetBoolValue(switchName, out flag);
			if (flag)
			{
				return boolValue;
			}
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			Span<char> buffer = span;
			switch (Interop.Kernel32.GetEnvironmentVariable(environmentName, buffer))
			{
			case 1:
				if (*buffer[0] == '0')
				{
					return false;
				}
				if (*buffer[0] == '1')
				{
					return true;
				}
				break;
			case 4:
				if (bool.IsTrueStringIgnoreCase(buffer.Slice(0, 4)))
				{
					return true;
				}
				break;
			case 5:
				if (bool.IsFalseStringIgnoreCase(buffer.Slice(0, 5)))
				{
					return false;
				}
				break;
			}
			return defaultValue;
		}

		// Token: 0x060001EA RID: 490
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool GetConfigBoolValue(string configSwitchName, out bool exist);
	}
}
