using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x020003A5 RID: 933
	internal static class InterfaceMarshaler
	{
		// Token: 0x060030B4 RID: 12468
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ConvertToNative(object objSrc, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060030B5 RID: 12469
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr ppUnk, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060030B6 RID: 12470
		[DllImport("QCall")]
		internal static extern void ClearNative(IntPtr pUnk);
	}
}
