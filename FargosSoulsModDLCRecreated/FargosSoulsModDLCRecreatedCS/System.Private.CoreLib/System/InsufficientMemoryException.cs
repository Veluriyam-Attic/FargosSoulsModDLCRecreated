using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000133 RID: 307
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x000DA5B7 File Offset: 0x000D97B7
		public InsufficientMemoryException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.HResult = -2146233027;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000DA5D0 File Offset: 0x000D97D0
		public InsufficientMemoryException(string message) : base(message)
		{
			base.HResult = -2146233027;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000DA5E4 File Offset: 0x000D97E4
		public InsufficientMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233027;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000DA5F9 File Offset: 0x000D97F9
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
