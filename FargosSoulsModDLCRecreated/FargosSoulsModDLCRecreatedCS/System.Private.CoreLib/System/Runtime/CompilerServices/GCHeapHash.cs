using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004EC RID: 1260
	[StructLayout(LayoutKind.Sequential)]
	internal class GCHeapHash
	{
		// Token: 0x040010AD RID: 4269
		private Array _data;

		// Token: 0x040010AE RID: 4270
		private int _count;

		// Token: 0x040010AF RID: 4271
		private int _deletedCount;
	}
}
