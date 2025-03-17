using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005D3 RID: 1491
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		// Token: 0x06004C26 RID: 19494 RVA: 0x0018B7F5 File Offset: 0x0018A9F5
		public CustomAttributeFormatException() : this(SR.Arg_CustomAttributeFormatException)
		{
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0018B802 File Offset: 0x0018AA02
		public CustomAttributeFormatException(string message) : this(message, null)
		{
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0018B80C File Offset: 0x0018AA0C
		public CustomAttributeFormatException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232827;
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0018B821 File Offset: 0x0018AA21
		[NullableContext(1)]
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
