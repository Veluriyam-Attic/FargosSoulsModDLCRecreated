using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002B0 RID: 688
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x001476C4 File Offset: 0x001468C4
		public SemaphoreFullException() : base(SR.Threading_SemaphoreFullException)
		{
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x001476D1 File Offset: 0x001468D1
		public SemaphoreFullException(string message) : base(message)
		{
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x001476DA File Offset: 0x001468DA
		public SemaphoreFullException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
