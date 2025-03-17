using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E9 RID: 1001
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SerializationException : SystemException
	{
		// Token: 0x0600321C RID: 12828 RVA: 0x0016A68C File Offset: 0x0016988C
		public SerializationException() : base(SR.SerializationException)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x0016A6A4 File Offset: 0x001698A4
		public SerializationException(string message) : base(message)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0016A6B8 File Offset: 0x001698B8
		public SerializationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
