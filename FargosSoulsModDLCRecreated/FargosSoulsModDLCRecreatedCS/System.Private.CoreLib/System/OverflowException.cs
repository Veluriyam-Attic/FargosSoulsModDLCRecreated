using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000169 RID: 361
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		// Token: 0x0600125F RID: 4703 RVA: 0x000E6A93 File Offset: 0x000E5C93
		public OverflowException() : base(SR.Arg_OverflowException)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000E6AAB File Offset: 0x000E5CAB
		public OverflowException(string message) : base(message)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000E6ABF File Offset: 0x000E5CBF
		public OverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000CFA89 File Offset: 0x000CEC89
		[NullableContext(1)]
		protected OverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
