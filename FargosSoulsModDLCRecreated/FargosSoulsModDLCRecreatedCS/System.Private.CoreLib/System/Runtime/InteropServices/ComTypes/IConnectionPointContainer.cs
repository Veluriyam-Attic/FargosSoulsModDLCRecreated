using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BC RID: 1212
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[NullableContext(1)]
	[ComImport]
	public interface IConnectionPointContainer
	{
		// Token: 0x0600454D RID: 17741
		void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

		// Token: 0x0600454E RID: 17742
		[NullableContext(2)]
		void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
	}
}
