using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000712 RID: 1810
	internal struct EventPipeEventInstanceData
	{
		// Token: 0x04001A33 RID: 6707
		internal IntPtr ProviderID;

		// Token: 0x04001A34 RID: 6708
		internal uint EventID;

		// Token: 0x04001A35 RID: 6709
		internal uint ThreadID;

		// Token: 0x04001A36 RID: 6710
		internal long TimeStamp;

		// Token: 0x04001A37 RID: 6711
		internal Guid ActivityId;

		// Token: 0x04001A38 RID: 6712
		internal Guid ChildActivityId;

		// Token: 0x04001A39 RID: 6713
		internal IntPtr Payload;

		// Token: 0x04001A3A RID: 6714
		internal uint PayloadLength;
	}
}
