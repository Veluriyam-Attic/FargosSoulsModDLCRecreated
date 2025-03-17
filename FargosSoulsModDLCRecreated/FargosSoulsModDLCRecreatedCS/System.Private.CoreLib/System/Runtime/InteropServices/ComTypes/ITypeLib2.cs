using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E8 RID: 1256
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020411-0000-0000-C000-000000000046")]
	[NullableContext(1)]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		// Token: 0x060045D0 RID: 17872
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x060045D1 RID: 17873
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x060045D2 RID: 17874
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x060045D3 RID: 17875
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x060045D4 RID: 17876
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x060045D5 RID: 17877
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x060045D6 RID: 17878
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060045D7 RID: 17879
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x060045D8 RID: 17880
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x060045D9 RID: 17881
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		// Token: 0x060045DA RID: 17882
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x060045DB RID: 17883
		[LCIDConversion(1)]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x060045DC RID: 17884
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		// Token: 0x060045DD RID: 17885
		void GetAllCustData(IntPtr pCustData);
	}
}
