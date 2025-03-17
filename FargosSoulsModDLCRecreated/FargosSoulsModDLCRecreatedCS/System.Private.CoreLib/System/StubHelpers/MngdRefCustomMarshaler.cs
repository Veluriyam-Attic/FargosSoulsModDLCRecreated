using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020003AA RID: 938
	internal static class MngdRefCustomMarshaler
	{
		// Token: 0x060030CA RID: 12490
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pCMHelper);

		// Token: 0x060030CB RID: 12491
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030CC RID: 12492
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030CD RID: 12493
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030CE RID: 12494
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);
	}
}
