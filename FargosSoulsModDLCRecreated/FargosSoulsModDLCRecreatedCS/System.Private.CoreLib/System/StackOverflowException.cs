using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000183 RID: 387
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		// Token: 0x060017D4 RID: 6100 RVA: 0x000F2456 File Offset: 0x000F1656
		public StackOverflowException() : base(SR.Arg_StackOverflowException)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000F246E File Offset: 0x000F166E
		public StackOverflowException(string message) : base(message)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000F2482 File Offset: 0x000F1682
		public StackOverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000C7203 File Offset: 0x000C6403
		private StackOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
