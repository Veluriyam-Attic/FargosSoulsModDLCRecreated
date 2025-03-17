using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C5 RID: 1221
	[NullableContext(1)]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x06004577 RID: 17783
		void GetClassID(out Guid pClassID);

		// Token: 0x06004578 RID: 17784
		[PreserveSig]
		int IsDirty();

		// Token: 0x06004579 RID: 17785
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x0600457A RID: 17786
		[NullableContext(2)]
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x0600457B RID: 17787
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x0600457C RID: 17788
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
