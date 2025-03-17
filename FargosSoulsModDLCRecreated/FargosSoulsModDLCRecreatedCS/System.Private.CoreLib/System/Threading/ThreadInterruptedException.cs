using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002BD RID: 701
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x00148B0D File Offset: 0x00147D0D
		public ThreadInterruptedException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.HResult = -2146233063;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00148B26 File Offset: 0x00147D26
		public ThreadInterruptedException(string message) : base(message)
		{
			base.HResult = -2146233063;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00148B3A File Offset: 0x00147D3A
		public ThreadInterruptedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233063;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
