using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C2 RID: 1218
	[NullableContext(1)]
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumVARIANT
	{
		// Token: 0x0600455F RID: 17759
		[PreserveSig]
		int Next(int celt, [Nullable(new byte[]
		{
			1,
			2
		})] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		// Token: 0x06004560 RID: 17760
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06004561 RID: 17761
		[PreserveSig]
		int Reset();

		// Token: 0x06004562 RID: 17762
		IEnumVARIANT Clone();
	}
}
