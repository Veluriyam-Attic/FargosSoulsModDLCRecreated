using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A5 RID: 1189
	public enum UnmanagedType
	{
		// Token: 0x04000F73 RID: 3955
		Bool = 2,
		// Token: 0x04000F74 RID: 3956
		I1,
		// Token: 0x04000F75 RID: 3957
		U1,
		// Token: 0x04000F76 RID: 3958
		I2,
		// Token: 0x04000F77 RID: 3959
		U2,
		// Token: 0x04000F78 RID: 3960
		I4,
		// Token: 0x04000F79 RID: 3961
		U4,
		// Token: 0x04000F7A RID: 3962
		I8,
		// Token: 0x04000F7B RID: 3963
		U8,
		// Token: 0x04000F7C RID: 3964
		R4,
		// Token: 0x04000F7D RID: 3965
		R8,
		// Token: 0x04000F7E RID: 3966
		Currency = 15,
		// Token: 0x04000F7F RID: 3967
		BStr = 19,
		// Token: 0x04000F80 RID: 3968
		LPStr,
		// Token: 0x04000F81 RID: 3969
		LPWStr,
		// Token: 0x04000F82 RID: 3970
		LPTStr,
		// Token: 0x04000F83 RID: 3971
		ByValTStr,
		// Token: 0x04000F84 RID: 3972
		IUnknown = 25,
		// Token: 0x04000F85 RID: 3973
		IDispatch,
		// Token: 0x04000F86 RID: 3974
		Struct,
		// Token: 0x04000F87 RID: 3975
		Interface,
		// Token: 0x04000F88 RID: 3976
		SafeArray,
		// Token: 0x04000F89 RID: 3977
		ByValArray,
		// Token: 0x04000F8A RID: 3978
		SysInt,
		// Token: 0x04000F8B RID: 3979
		SysUInt,
		// Token: 0x04000F8C RID: 3980
		VBByRefStr = 34,
		// Token: 0x04000F8D RID: 3981
		AnsiBStr,
		// Token: 0x04000F8E RID: 3982
		TBStr,
		// Token: 0x04000F8F RID: 3983
		VariantBool,
		// Token: 0x04000F90 RID: 3984
		FunctionPtr,
		// Token: 0x04000F91 RID: 3985
		AsAny = 40,
		// Token: 0x04000F92 RID: 3986
		LPArray = 42,
		// Token: 0x04000F93 RID: 3987
		LPStruct,
		// Token: 0x04000F94 RID: 3988
		CustomMarshaler,
		// Token: 0x04000F95 RID: 3989
		Error,
		// Token: 0x04000F96 RID: 3990
		IInspectable,
		// Token: 0x04000F97 RID: 3991
		HString,
		// Token: 0x04000F98 RID: 3992
		LPUTF8Str
	}
}
