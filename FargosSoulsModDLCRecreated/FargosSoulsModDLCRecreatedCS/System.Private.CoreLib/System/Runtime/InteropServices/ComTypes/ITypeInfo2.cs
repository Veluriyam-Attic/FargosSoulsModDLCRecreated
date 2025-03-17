using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E3 RID: 1251
	[NullableContext(1)]
	[Guid("00020412-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo2 : ITypeInfo
	{
		// Token: 0x060045A4 RID: 17828
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x060045A5 RID: 17829
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x060045A6 RID: 17830
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x060045A7 RID: 17831
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x060045A8 RID: 17832
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x060045A9 RID: 17833
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x060045AA RID: 17834
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x060045AB RID: 17835
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x060045AC RID: 17836
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x060045AD RID: 17837
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060045AE RID: 17838
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x060045AF RID: 17839
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x060045B0 RID: 17840
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x060045B1 RID: 17841
		void CreateInstance([Nullable(2)] [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x060045B2 RID: 17842
		[NullableContext(2)]
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060045B3 RID: 17843
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x060045B4 RID: 17844
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x060045B5 RID: 17845
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x060045B6 RID: 17846
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);

		// Token: 0x060045B7 RID: 17847
		void GetTypeKind(out TYPEKIND pTypeKind);

		// Token: 0x060045B8 RID: 17848
		void GetTypeFlags(out int pTypeFlags);

		// Token: 0x060045B9 RID: 17849
		void GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, out int pFuncIndex);

		// Token: 0x060045BA RID: 17850
		void GetVarIndexOfMemId(int memid, out int pVarIndex);

		// Token: 0x060045BB RID: 17851
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x060045BC RID: 17852
		void GetFuncCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x060045BD RID: 17853
		void GetParamCustData(int indexFunc, int indexParam, ref Guid guid, out object pVarVal);

		// Token: 0x060045BE RID: 17854
		void GetVarCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x060045BF RID: 17855
		void GetImplTypeCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x060045C0 RID: 17856
		[LCIDConversion(1)]
		void GetDocumentation2(int memid, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x060045C1 RID: 17857
		void GetAllCustData(IntPtr pCustData);

		// Token: 0x060045C2 RID: 17858
		void GetAllFuncCustData(int index, IntPtr pCustData);

		// Token: 0x060045C3 RID: 17859
		void GetAllParamCustData(int indexFunc, int indexParam, IntPtr pCustData);

		// Token: 0x060045C4 RID: 17860
		void GetAllVarCustData(int index, IntPtr pCustData);

		// Token: 0x060045C5 RID: 17861
		void GetAllImplTypeCustData(int index, IntPtr pCustData);
	}
}
