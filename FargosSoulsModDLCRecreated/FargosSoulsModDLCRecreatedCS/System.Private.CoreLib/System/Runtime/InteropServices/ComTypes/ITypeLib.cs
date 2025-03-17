using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E7 RID: 1255
	[Guid("00020402-0000-0000-C000-000000000046")]
	[NullableContext(1)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x060045C6 RID: 17862
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x060045C7 RID: 17863
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x060045C8 RID: 17864
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x060045C9 RID: 17865
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x060045CA RID: 17866
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x060045CB RID: 17867
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x060045CC RID: 17868
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060045CD RID: 17869
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x060045CE RID: 17870
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x060045CF RID: 17871
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
