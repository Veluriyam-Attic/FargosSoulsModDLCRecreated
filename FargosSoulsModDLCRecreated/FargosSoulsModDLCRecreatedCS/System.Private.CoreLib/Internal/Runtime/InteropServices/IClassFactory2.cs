using System;
using System.Runtime.InteropServices;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x0200081E RID: 2078
	[ComVisible(false)]
	[Guid("B196B28F-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IClassFactory2 : IClassFactory
	{
		// Token: 0x06006280 RID: 25216
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, ref Guid riid, out IntPtr ppvObject);

		// Token: 0x06006281 RID: 25217
		void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);

		// Token: 0x06006282 RID: 25218
		void GetLicInfo(ref LICINFO pLicInfo);

		// Token: 0x06006283 RID: 25219
		void RequestLicKey(int dwReserved, [MarshalAs(UnmanagedType.BStr)] out string pBstrKey);

		// Token: 0x06006284 RID: 25220
		void CreateInstanceLic([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, [MarshalAs(UnmanagedType.Interface)] object pUnkReserved, ref Guid riid, [MarshalAs(UnmanagedType.BStr)] string bstrKey, out IntPtr ppvObject);
	}
}
