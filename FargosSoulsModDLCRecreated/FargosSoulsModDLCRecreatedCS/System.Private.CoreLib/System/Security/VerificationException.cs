using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020003C7 RID: 967
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class VerificationException : SystemException
	{
		// Token: 0x0600319F RID: 12703 RVA: 0x00169F45 File Offset: 0x00169145
		public VerificationException() : base(SR.Verification_Exception)
		{
			base.HResult = -2146233075;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x00169F5D File Offset: 0x0016915D
		public VerificationException(string message) : base(message)
		{
			base.HResult = -2146233075;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x00169F71 File Offset: 0x00169171
		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233075;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
