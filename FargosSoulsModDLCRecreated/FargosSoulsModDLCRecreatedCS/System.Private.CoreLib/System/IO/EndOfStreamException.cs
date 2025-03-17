using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000689 RID: 1673
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x06005532 RID: 21810 RVA: 0x001A1B33 File Offset: 0x001A0D33
		public EndOfStreamException() : base(SR.Arg_EndOfStreamException)
		{
			base.HResult = -2147024858;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x001A1B4B File Offset: 0x001A0D4B
		public EndOfStreamException(string message) : base(message)
		{
			base.HResult = -2147024858;
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x001A1B5F File Offset: 0x001A0D5F
		public EndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024858;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x001A1B1B File Offset: 0x001A0D1B
		[NullableContext(1)]
		protected EndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
