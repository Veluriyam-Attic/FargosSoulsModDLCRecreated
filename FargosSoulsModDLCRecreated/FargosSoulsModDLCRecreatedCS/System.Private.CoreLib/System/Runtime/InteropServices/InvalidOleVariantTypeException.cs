using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000490 RID: 1168
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		// Token: 0x0600449C RID: 17564 RVA: 0x0017938C File Offset: 0x0017858C
		public InvalidOleVariantTypeException() : base(SR.Arg_InvalidOleVariantTypeException)
		{
			base.HResult = -2146233039;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x001793A4 File Offset: 0x001785A4
		public InvalidOleVariantTypeException(string message) : base(message)
		{
			base.HResult = -2146233039;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x001793B8 File Offset: 0x001785B8
		public InvalidOleVariantTypeException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233039;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
