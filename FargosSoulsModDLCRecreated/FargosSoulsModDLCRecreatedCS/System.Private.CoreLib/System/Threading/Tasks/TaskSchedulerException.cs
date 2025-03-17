using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x0200033E RID: 830
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		// Token: 0x06002C15 RID: 11285 RVA: 0x001539BF File Offset: 0x00152BBF
		public TaskSchedulerException() : base(SR.TaskSchedulerException_ctor_DefaultMessage)
		{
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000DB117 File Offset: 0x000DA317
		public TaskSchedulerException(string message) : base(message)
		{
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x001539CC File Offset: 0x00152BCC
		public TaskSchedulerException(Exception innerException) : base(SR.TaskSchedulerException_ctor_DefaultMessage, innerException)
		{
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000DB120 File Offset: 0x000DA320
		public TaskSchedulerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
