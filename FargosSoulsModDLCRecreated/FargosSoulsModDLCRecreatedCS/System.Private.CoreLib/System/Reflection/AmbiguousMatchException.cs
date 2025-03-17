using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005B6 RID: 1462
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		// Token: 0x06004BC5 RID: 19397 RVA: 0x0018B07A File Offset: 0x0018A27A
		public AmbiguousMatchException() : base(SR.RFLCT_Ambiguous)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0018B092 File Offset: 0x0018A292
		public AmbiguousMatchException(string message) : base(message)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0018B0A6 File Offset: 0x0018A2A6
		public AmbiguousMatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x000C7203 File Offset: 0x000C6403
		private AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
