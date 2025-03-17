using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200014B RID: 331
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MemberAccessException : SystemException
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x000DBB52 File Offset: 0x000DAD52
		public MemberAccessException() : base(SR.Arg_AccessException)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000DBB6A File Offset: 0x000DAD6A
		public MemberAccessException(string message) : base(message)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000DBB7E File Offset: 0x000DAD7E
		public MemberAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected MemberAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
