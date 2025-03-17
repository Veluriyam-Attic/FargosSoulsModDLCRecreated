using System;

namespace System.Buffers
{
	// Token: 0x0200024C RID: 588
	public interface IPinnable
	{
		// Token: 0x0600243A RID: 9274
		MemoryHandle Pin(int elementIndex);

		// Token: 0x0600243B RID: 9275
		void Unpin();
	}
}
