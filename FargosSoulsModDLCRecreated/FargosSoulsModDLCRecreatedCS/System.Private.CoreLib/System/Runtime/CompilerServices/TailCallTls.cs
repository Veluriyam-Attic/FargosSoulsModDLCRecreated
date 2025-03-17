using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FA RID: 1274
	internal struct TailCallTls
	{
		// Token: 0x040010CB RID: 4299
		public unsafe PortableTailCallFrame* Frame;

		// Token: 0x040010CC RID: 4300
		public IntPtr ArgBuffer;
	}
}
