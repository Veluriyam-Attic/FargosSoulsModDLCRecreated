using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001B4 RID: 436
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		// Token: 0x06001ACF RID: 6863 RVA: 0x000FCED8 File Offset: 0x000FC0D8
		public UnauthorizedAccessException() : base(SR.Arg_UnauthorizedAccessException)
		{
			base.HResult = -2147024891;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000FCEF0 File Offset: 0x000FC0F0
		public UnauthorizedAccessException(string message) : base(message)
		{
			base.HResult = -2147024891;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000FCF04 File Offset: 0x000FC104
		public UnauthorizedAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024891;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
