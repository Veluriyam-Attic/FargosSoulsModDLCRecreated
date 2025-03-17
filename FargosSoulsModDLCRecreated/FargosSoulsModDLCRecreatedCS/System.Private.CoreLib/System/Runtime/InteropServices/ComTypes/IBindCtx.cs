using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BA RID: 1210
	[NullableContext(1)]
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IBindCtx
	{
		// Token: 0x0600453E RID: 17726
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x0600453F RID: 17727
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06004540 RID: 17728
		void ReleaseBoundObjects();

		// Token: 0x06004541 RID: 17729
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x06004542 RID: 17730
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x06004543 RID: 17731
		[NullableContext(2)]
		void GetRunningObjectTable(out IRunningObjectTable pprot);

		// Token: 0x06004544 RID: 17732
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06004545 RID: 17733
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [Nullable(2)] [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06004546 RID: 17734
		[NullableContext(2)]
		void EnumObjectParam(out IEnumString ppenum);

		// Token: 0x06004547 RID: 17735
		[PreserveSig]
		int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}
