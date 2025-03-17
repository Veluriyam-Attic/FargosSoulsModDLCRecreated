using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000157 RID: 343
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class NotSupportedException : SystemException
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x000DEF47 File Offset: 0x000DE147
		public NotSupportedException() : base(SR.Arg_NotSupportedException)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000DEF5F File Offset: 0x000DE15F
		public NotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000DEF73 File Offset: 0x000DE173
		public NotSupportedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
