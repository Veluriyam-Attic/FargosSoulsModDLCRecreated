using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004F9 RID: 1273
	internal struct PortableTailCallFrame
	{
		// Token: 0x040010C8 RID: 4296
		public unsafe PortableTailCallFrame* Prev;

		// Token: 0x040010C9 RID: 4297
		public IntPtr TailCallAwareReturnAddress;

		// Token: 0x040010CA RID: 4298
		public method NextCall;
	}
}
