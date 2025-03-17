using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000193 RID: 403
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class TimeoutException : SystemException
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x000F35BF File Offset: 0x000F27BF
		public TimeoutException() : base(SR.Arg_TimeoutException)
		{
			base.HResult = -2146233083;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000F35D7 File Offset: 0x000F27D7
		public TimeoutException(string message) : base(message)
		{
			base.HResult = -2146233083;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000F35EB File Offset: 0x000F27EB
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233083;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected TimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
