using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000174 RID: 372
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class RankException : SystemException
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x000E7FE1 File Offset: 0x000E71E1
		public RankException() : base(SR.Arg_RankException)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000E7FF9 File Offset: 0x000E71F9
		public RankException(string message) : base(message)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000E800D File Offset: 0x000E720D
		public RankException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected RankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
