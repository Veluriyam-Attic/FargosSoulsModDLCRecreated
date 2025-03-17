using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C6 RID: 1222
	[NullableContext(1)]
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRunningObjectTable
	{
		// Token: 0x0600457D RID: 17789
		int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

		// Token: 0x0600457E RID: 17790
		void Revoke(int dwRegister);

		// Token: 0x0600457F RID: 17791
		[PreserveSig]
		int IsRunning(IMoniker pmkObjectName);

		// Token: 0x06004580 RID: 17792
		[PreserveSig]
		int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x06004581 RID: 17793
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x06004582 RID: 17794
		[PreserveSig]
		int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x06004583 RID: 17795
		void EnumRunning(out IEnumMoniker ppenumMoniker);
	}
}
