using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002D4 RID: 724
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x060028DF RID: 10463 RVA: 0x0014A126 File Offset: 0x00149326
		internal ThreadStartException() : base(SR.Arg_ThreadStartException)
		{
			base.HResult = -2146233051;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0014A13E File Offset: 0x0014933E
		internal ThreadStartException(Exception reason) : base(SR.Arg_ThreadStartException, reason)
		{
			base.HResult = -2146233051;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000C7203 File Offset: 0x000C6403
		private ThreadStartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
