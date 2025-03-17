using System;

namespace Internal.Win32
{
	// Token: 0x02000814 RID: 2068
	internal static class Registry
	{
		// Token: 0x04001D53 RID: 7507
		public static readonly RegistryKey CurrentUser = RegistryKey.OpenBaseKey((IntPtr)(-2147483647));

		// Token: 0x04001D54 RID: 7508
		public static readonly RegistryKey LocalMachine = RegistryKey.OpenBaseKey((IntPtr)(-2147483646));
	}
}
