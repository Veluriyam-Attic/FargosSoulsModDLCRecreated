using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004AB RID: 1195
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000003-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IMarshal
	{
		// Token: 0x060044FB RID: 17659
		[PreserveSig]
		int GetUnmarshalClass(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out Guid pCid);

		// Token: 0x060044FC RID: 17660
		[PreserveSig]
		int GetMarshalSizeMax(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize);

		// Token: 0x060044FD RID: 17661
		[PreserveSig]
		int MarshalInterface(IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags);

		// Token: 0x060044FE RID: 17662
		[PreserveSig]
		int UnmarshalInterface(IntPtr pStm, ref Guid riid, out IntPtr ppv);

		// Token: 0x060044FF RID: 17663
		[PreserveSig]
		int ReleaseMarshalData(IntPtr pStm);

		// Token: 0x06004500 RID: 17664
		[PreserveSig]
		int DisconnectObject(int dwReserved);
	}
}
