using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000EB RID: 235
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x000CFA48 File Offset: 0x000CEC48
		public DivideByZeroException() : base(SR.Arg_DivideByZero)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000CFA60 File Offset: 0x000CEC60
		public DivideByZeroException(string message) : base(message)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000CFA74 File Offset: 0x000CEC74
		public DivideByZeroException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000CFA89 File Offset: 0x000CEC89
		[NullableContext(1)]
		protected DivideByZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
