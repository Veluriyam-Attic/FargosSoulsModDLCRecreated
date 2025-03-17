using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000139 RID: 313
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x000DB08D File Offset: 0x000DA28D
		public InvalidOperationException() : base(SR.Arg_InvalidOperationException)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000DB0A5 File Offset: 0x000DA2A5
		public InvalidOperationException(string message) : base(message)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000DB0B9 File Offset: 0x000DA2B9
		public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
