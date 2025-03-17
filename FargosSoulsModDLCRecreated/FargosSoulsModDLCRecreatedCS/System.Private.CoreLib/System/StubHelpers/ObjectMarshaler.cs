using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020003A2 RID: 930
	internal static class ObjectMarshaler
	{
		// Token: 0x060030AC RID: 12460
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(object objSrc, IntPtr pDstVariant);

		// Token: 0x060030AD RID: 12461
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pSrcVariant);

		// Token: 0x060030AE RID: 12462
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pVariant);
	}
}
