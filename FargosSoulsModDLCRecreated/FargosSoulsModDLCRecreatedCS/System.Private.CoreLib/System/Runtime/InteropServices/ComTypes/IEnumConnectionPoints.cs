using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BD RID: 1213
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[NullableContext(1)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		// Token: 0x0600454F RID: 17743
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		// Token: 0x06004550 RID: 17744
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06004551 RID: 17745
		void Reset();

		// Token: 0x06004552 RID: 17746
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
