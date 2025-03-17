using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x0200081C RID: 2076
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(false)]
	[Guid("00000001-0000-0000-C000-000000000046")]
	[NullableContext(2)]
	[ComImport]
	public interface IClassFactory
	{
		// Token: 0x0600627E RID: 25214
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, ref Guid riid, out IntPtr ppvObject);

		// Token: 0x0600627F RID: 25215
		void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
	}
}
