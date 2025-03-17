using System;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000459 RID: 1113
	[Guid("00020400-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDispatch
	{
		// Token: 0x060043EB RID: 17387
		int GetTypeInfoCount();

		// Token: 0x060043EC RID: 17388
		ITypeInfo GetTypeInfo(int iTInfo, int lcid);

		// Token: 0x060043ED RID: 17389
		void GetIDsOfNames(ref Guid riid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] [In] string[] rgszNames, int cNames, int lcid, [Out] int[] rgDispId);

		// Token: 0x060043EE RID: 17390
		void Invoke(int dispIdMember, ref Guid riid, int lcid, InvokeFlags wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
