using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D9 RID: 217
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x000CA06E File Offset: 0x000C926E
		public CannotUnloadAppDomainException() : base(SR.Arg_CannotUnloadAppDomainException)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000CA086 File Offset: 0x000C9286
		public CannotUnloadAppDomainException(string message) : base(message)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000CA09A File Offset: 0x000C929A
		public CannotUnloadAppDomainException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
