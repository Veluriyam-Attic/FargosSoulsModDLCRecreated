using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200018C RID: 396
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SystemException : Exception
	{
		// Token: 0x06001808 RID: 6152 RVA: 0x000F2A2F File Offset: 0x000F1C2F
		public SystemException() : base(SR.Arg_SystemException)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000F2A47 File Offset: 0x000F1C47
		public SystemException(string message) : base(message)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000F2A5B File Offset: 0x000F1C5B
		public SystemException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
