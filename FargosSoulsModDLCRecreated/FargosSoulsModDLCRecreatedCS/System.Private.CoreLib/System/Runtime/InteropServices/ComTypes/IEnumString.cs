using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C1 RID: 1217
	[Guid("00000101-0000-0000-C000-000000000046")]
	[NullableContext(1)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumString
	{
		// Token: 0x0600455B RID: 17755
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600455C RID: 17756
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600455D RID: 17757
		void Reset();

		// Token: 0x0600455E RID: 17758
		void Clone(out IEnumString ppenum);
	}
}
