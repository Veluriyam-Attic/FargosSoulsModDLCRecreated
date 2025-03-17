using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
	// Token: 0x020007CA RID: 1994
	[DebuggerDisplay("Head = {Head}, Tail = {Tail}")]
	[StructLayout(LayoutKind.Explicit, Size = 192)]
	internal struct PaddedHeadAndTail
	{
		// Token: 0x04001CFB RID: 7419
		[FieldOffset(64)]
		public int Head;

		// Token: 0x04001CFC RID: 7420
		[FieldOffset(128)]
		public int Tail;
	}
}
