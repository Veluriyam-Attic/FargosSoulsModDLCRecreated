using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000168 RID: 360
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x000E6A51 File Offset: 0x000E5C51
		public OutOfMemoryException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.HResult = -2147024882;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000E6A6A File Offset: 0x000E5C6A
		public OutOfMemoryException(string message) : base(message)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000E6A7E File Offset: 0x000E5C7E
		public OutOfMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
