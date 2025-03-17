using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000494 RID: 1172
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		// Token: 0x060044A5 RID: 17573 RVA: 0x001793FB File Offset: 0x001785FB
		public MarshalDirectiveException() : base(SR.Arg_MarshalDirectiveException)
		{
			base.HResult = -2146233035;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00179413 File Offset: 0x00178613
		public MarshalDirectiveException(string message) : base(message)
		{
			base.HResult = -2146233035;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x00179427 File Offset: 0x00178627
		public MarshalDirectiveException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233035;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
