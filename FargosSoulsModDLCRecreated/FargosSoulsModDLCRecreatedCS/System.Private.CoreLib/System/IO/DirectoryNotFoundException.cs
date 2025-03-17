using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000687 RID: 1671
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		// Token: 0x0600552D RID: 21805 RVA: 0x001A1ADA File Offset: 0x001A0CDA
		public DirectoryNotFoundException() : base(SR.Arg_DirectoryNotFoundException)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x001A1AF2 File Offset: 0x001A0CF2
		public DirectoryNotFoundException(string message) : base(message)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x001A1B06 File Offset: 0x001A0D06
		public DirectoryNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x001A1B1B File Offset: 0x001A0D1B
		[NullableContext(1)]
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
