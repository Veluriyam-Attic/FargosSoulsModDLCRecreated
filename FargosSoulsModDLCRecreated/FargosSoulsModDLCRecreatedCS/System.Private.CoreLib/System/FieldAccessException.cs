using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F6 RID: 246
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		// Token: 0x06000DC2 RID: 3522 RVA: 0x000D006D File Offset: 0x000CF26D
		public FieldAccessException() : base(SR.Arg_FieldAccessException)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000D0085 File Offset: 0x000CF285
		public FieldAccessException(string message) : base(message)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000D0099 File Offset: 0x000CF299
		public FieldAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000D00AE File Offset: 0x000CF2AE
		[NullableContext(1)]
		protected FieldAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
