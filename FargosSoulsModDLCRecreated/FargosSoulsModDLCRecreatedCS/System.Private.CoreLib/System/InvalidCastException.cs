using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000138 RID: 312
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x000DB03C File Offset: 0x000DA23C
		public InvalidCastException() : base(SR.Arg_InvalidCastException)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000DB054 File Offset: 0x000DA254
		public InvalidCastException(string message) : base(message)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000DB068 File Offset: 0x000DA268
		public InvalidCastException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000DB07D File Offset: 0x000DA27D
		public InvalidCastException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected InvalidCastException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
