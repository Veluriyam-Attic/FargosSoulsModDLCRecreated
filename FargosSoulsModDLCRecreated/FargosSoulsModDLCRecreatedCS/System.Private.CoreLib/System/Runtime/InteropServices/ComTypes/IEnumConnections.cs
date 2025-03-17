using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BF RID: 1215
	[NullableContext(1)]
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnections
	{
		// Token: 0x06004553 RID: 17747
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, IntPtr pceltFetched);

		// Token: 0x06004554 RID: 17748
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06004555 RID: 17749
		void Reset();

		// Token: 0x06004556 RID: 17750
		void Clone(out IEnumConnections ppenum);
	}
}
