using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020003A6 RID: 934
	internal static class MngdNativeArrayMarshaler
	{
		// Token: 0x060030B7 RID: 12471
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, int dwFlags, IntPtr pManagedMarshaler);

		// Token: 0x060030B8 RID: 12472
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030B9 RID: 12473
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030BA RID: 12474
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int cElements);

		// Token: 0x060030BB RID: 12475
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060030BC RID: 12476
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int cElements);

		// Token: 0x060030BD RID: 12477
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNativeContents(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int cElements);

		// Token: 0x020003A7 RID: 935
		internal struct MarshalerState
		{
			// Token: 0x04000D5B RID: 3419
			private IntPtr m_pElementMT;

			// Token: 0x04000D5C RID: 3420
			private IntPtr m_Array;

			// Token: 0x04000D5D RID: 3421
			private IntPtr m_pManagedNativeArrayMarshaler;

			// Token: 0x04000D5E RID: 3422
			private int m_NativeDataValid;

			// Token: 0x04000D5F RID: 3423
			private int m_BestFitMap;

			// Token: 0x04000D60 RID: 3424
			private int m_ThrowOnUnmappableChar;

			// Token: 0x04000D61 RID: 3425
			private short m_vt;
		}
	}
}
