using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200069F RID: 1695
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class PathTooLongException : IOException
	{
		// Token: 0x06005649 RID: 22089 RVA: 0x001A7DB6 File Offset: 0x001A6FB6
		public PathTooLongException() : base(SR.IO_PathTooLong)
		{
			base.HResult = -2147024690;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x001A7DCE File Offset: 0x001A6FCE
		public PathTooLongException(string message) : base(message)
		{
			base.HResult = -2147024690;
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x001A7DE2 File Offset: 0x001A6FE2
		public PathTooLongException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024690;
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x001A1B1B File Offset: 0x001A0D1B
		[NullableContext(1)]
		protected PathTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
