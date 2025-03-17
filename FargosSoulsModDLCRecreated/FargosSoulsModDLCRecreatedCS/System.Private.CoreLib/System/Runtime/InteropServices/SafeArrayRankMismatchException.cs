using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049B RID: 1179
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		// Token: 0x060044B3 RID: 17587 RVA: 0x00179453 File Offset: 0x00178653
		public SafeArrayRankMismatchException() : base(SR.Arg_SafeArrayRankMismatchException)
		{
			base.HResult = -2146233032;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0017946B File Offset: 0x0017866B
		public SafeArrayRankMismatchException(string message) : base(message)
		{
			base.HResult = -2146233032;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0017947F File Offset: 0x0017867F
		public SafeArrayRankMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233032;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
