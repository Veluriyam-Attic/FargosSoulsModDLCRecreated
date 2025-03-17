using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058D RID: 1421
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeRecord
	{
		// Token: 0x040011DD RID: 4573
		internal ConstArray blob;

		// Token: 0x040011DE RID: 4574
		internal MetadataToken tkCtor;
	}
}
