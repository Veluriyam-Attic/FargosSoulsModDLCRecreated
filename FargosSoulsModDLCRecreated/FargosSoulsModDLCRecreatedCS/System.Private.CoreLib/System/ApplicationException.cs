using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ApplicationException : Exception
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x000C84A1 File Offset: 0x000C76A1
		public ApplicationException() : base(SR.Arg_ApplicationException)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000C84B9 File Offset: 0x000C76B9
		public ApplicationException(string message) : base(message)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000C84CD File Offset: 0x000C76CD
		public ApplicationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
