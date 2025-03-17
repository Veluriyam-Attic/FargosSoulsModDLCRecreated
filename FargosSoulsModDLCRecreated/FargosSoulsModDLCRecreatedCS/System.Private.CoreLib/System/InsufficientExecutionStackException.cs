using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000132 RID: 306
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x000DA576 File Offset: 0x000D9776
		public InsufficientExecutionStackException() : base(SR.Arg_InsufficientExecutionStackException)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000DA58E File Offset: 0x000D978E
		public InsufficientExecutionStackException(string message) : base(message)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000DA5A2 File Offset: 0x000D97A2
		public InsufficientExecutionStackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000C7203 File Offset: 0x000C6403
		private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
