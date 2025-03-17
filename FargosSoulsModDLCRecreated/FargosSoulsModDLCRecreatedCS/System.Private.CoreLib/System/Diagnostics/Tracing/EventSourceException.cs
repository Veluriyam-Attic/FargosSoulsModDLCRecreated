using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000743 RID: 1859
	[NullableContext(2)]
	[Nullable(0)]
	[Serializable]
	public class EventSourceException : Exception
	{
		// Token: 0x06005B83 RID: 23427 RVA: 0x001BE2E1 File Offset: 0x001BD4E1
		public EventSourceException() : base(SR.EventSource_ListenerWriteFailure)
		{
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x000DB117 File Offset: 0x000DA317
		public EventSourceException(string message) : base(message)
		{
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x000DB120 File Offset: 0x000DA320
		public EventSourceException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected EventSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x001BE2EE File Offset: 0x001BD4EE
		internal EventSourceException(Exception innerException) : base(SR.EventSource_ListenerWriteFailure, innerException)
		{
		}
	}
}
