using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E2 RID: 1250
	[Guid("00020401-0000-0000-C000-000000000046")]
	[NullableContext(1)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x06004591 RID: 17809
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06004592 RID: 17810
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06004593 RID: 17811
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06004594 RID: 17812
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x06004595 RID: 17813
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x06004596 RID: 17814
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x06004597 RID: 17815
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x06004598 RID: 17816
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x06004599 RID: 17817
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x0600459A RID: 17818
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600459B RID: 17819
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x0600459C RID: 17820
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x0600459D RID: 17821
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x0600459E RID: 17822
		void CreateInstance([Nullable(2)] [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x0600459F RID: 17823
		[NullableContext(2)]
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060045A0 RID: 17824
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x060045A1 RID: 17825
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x060045A2 RID: 17826
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x060045A3 RID: 17827
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
