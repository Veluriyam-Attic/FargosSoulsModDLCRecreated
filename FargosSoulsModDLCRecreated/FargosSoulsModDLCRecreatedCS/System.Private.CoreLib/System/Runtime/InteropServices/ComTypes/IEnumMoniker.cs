using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C0 RID: 1216
	[NullableContext(1)]
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumMoniker
	{
		// Token: 0x06004557 RID: 17751
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		// Token: 0x06004558 RID: 17752
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06004559 RID: 17753
		void Reset();

		// Token: 0x0600455A RID: 17754
		void Clone(out IEnumMoniker ppenum);
	}
}
