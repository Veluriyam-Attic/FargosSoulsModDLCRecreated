using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002D6 RID: 726
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		// Token: 0x060028E2 RID: 10466 RVA: 0x0014A157 File Offset: 0x00149357
		public ThreadStateException() : base(SR.Arg_ThreadStateException)
		{
			base.HResult = -2146233056;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0014A16F File Offset: 0x0014936F
		public ThreadStateException(string message) : base(message)
		{
			base.HResult = -2146233056;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0014A183 File Offset: 0x00149383
		public ThreadStateException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233056;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ThreadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
