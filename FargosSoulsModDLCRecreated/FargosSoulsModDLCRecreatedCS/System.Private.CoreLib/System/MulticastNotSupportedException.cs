using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000153 RID: 339
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		// Token: 0x06001136 RID: 4406 RVA: 0x000DEDC8 File Offset: 0x000DDFC8
		public MulticastNotSupportedException() : base(SR.Arg_MulticastNotSupportedException)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000DEDE0 File Offset: 0x000DDFE0
		public MulticastNotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000DEDF4 File Offset: 0x000DDFF4
		public MulticastNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000C7203 File Offset: 0x000C6403
		private MulticastNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
