using System;

namespace System.Threading
{
	// Token: 0x020002A8 RID: 680
	internal class ReaderWriterCount
	{
		// Token: 0x04000A7E RID: 2686
		public long lockID;

		// Token: 0x04000A7F RID: 2687
		public int readercount;

		// Token: 0x04000A80 RID: 2688
		public int writercount;

		// Token: 0x04000A81 RID: 2689
		public int upgradecount;

		// Token: 0x04000A82 RID: 2690
		public ReaderWriterCount next;
	}
}
