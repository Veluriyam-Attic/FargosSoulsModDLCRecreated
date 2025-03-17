using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BB RID: 1211
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[NullableContext(1)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConnectionPoint
	{
		// Token: 0x06004548 RID: 17736
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x06004549 RID: 17737
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		// Token: 0x0600454A RID: 17738
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x0600454B RID: 17739
		void Unadvise(int dwCookie);

		// Token: 0x0600454C RID: 17740
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
