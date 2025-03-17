using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(2)]
	[Nullable(0)]
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x000D002C File Offset: 0x000CF22C
		public ExecutionEngineException() : base(SR.Arg_ExecutionEngineException)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000D0044 File Offset: 0x000CF244
		public ExecutionEngineException(string message) : base(message)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000D0058 File Offset: 0x000CF258
		public ExecutionEngineException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000C7203 File Offset: 0x000C6403
		private ExecutionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
