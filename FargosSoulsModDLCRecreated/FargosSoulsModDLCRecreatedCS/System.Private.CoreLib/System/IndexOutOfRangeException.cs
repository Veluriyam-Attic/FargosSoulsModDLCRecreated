using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000131 RID: 305
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x000DA535 File Offset: 0x000D9735
		public IndexOutOfRangeException() : base(SR.Arg_IndexOutOfRangeException)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000DA54D File Offset: 0x000D974D
		public IndexOutOfRangeException(string message) : base(message)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000DA561 File Offset: 0x000D9761
		public IndexOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000C7203 File Offset: 0x000C6403
		private IndexOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
