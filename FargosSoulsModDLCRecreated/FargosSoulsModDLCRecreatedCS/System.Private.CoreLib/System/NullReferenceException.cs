using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200015A RID: 346
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x000DF101 File Offset: 0x000DE301
		public NullReferenceException() : base(SR.Arg_NullReferenceException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000C71DA File Offset: 0x000C63DA
		public NullReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000C71EE File Offset: 0x000C63EE
		public NullReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected NullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
