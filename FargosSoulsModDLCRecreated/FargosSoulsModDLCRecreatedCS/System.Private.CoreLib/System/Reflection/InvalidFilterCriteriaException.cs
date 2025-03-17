using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005E0 RID: 1504
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		// Token: 0x06004C5C RID: 19548 RVA: 0x0018BBE0 File Offset: 0x0018ADE0
		public InvalidFilterCriteriaException() : this(SR.Arg_InvalidFilterCriteriaException)
		{
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0018BBED File Offset: 0x0018ADED
		public InvalidFilterCriteriaException(string message) : this(message, null)
		{
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0018BBF7 File Offset: 0x0018ADF7
		public InvalidFilterCriteriaException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232831;
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0014AC10 File Offset: 0x00149E10
		[NullableContext(1)]
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
