using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F8 RID: 248
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class FormatException : SystemException
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x000D00B8 File Offset: 0x000CF2B8
		public FormatException() : base(SR.Arg_FormatException)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000D00D0 File Offset: 0x000CF2D0
		public FormatException(string message) : base(message)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000D00E4 File Offset: 0x000CF2E4
		public FormatException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
