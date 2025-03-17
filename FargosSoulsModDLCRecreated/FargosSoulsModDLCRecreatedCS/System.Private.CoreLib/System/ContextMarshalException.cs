using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DE RID: 222
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x000CAD53 File Offset: 0x000C9F53
		public ContextMarshalException() : this(SR.Arg_ContextMarshalException, null)
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000CAD61 File Offset: 0x000C9F61
		public ContextMarshalException(string message) : this(message, null)
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000CAD6B File Offset: 0x000C9F6B
		public ContextMarshalException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233084;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ContextMarshalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
