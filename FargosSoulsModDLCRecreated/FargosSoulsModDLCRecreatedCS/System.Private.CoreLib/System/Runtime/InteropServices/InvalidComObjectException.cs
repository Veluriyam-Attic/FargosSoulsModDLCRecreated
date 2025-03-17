using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200048F RID: 1167
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x06004498 RID: 17560 RVA: 0x0017934B File Offset: 0x0017854B
		public InvalidComObjectException() : base(SR.Arg_InvalidComObjectException)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00179363 File Offset: 0x00178563
		public InvalidComObjectException(string message) : base(message)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x00179377 File Offset: 0x00178577
		public InvalidComObjectException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
