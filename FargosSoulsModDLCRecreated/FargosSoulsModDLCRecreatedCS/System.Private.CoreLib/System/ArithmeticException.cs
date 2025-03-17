using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C8 RID: 200
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x000C8A2C File Offset: 0x000C7C2C
		public ArithmeticException() : base(SR.Arg_ArithmeticException)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000C8A44 File Offset: 0x000C7C44
		public ArithmeticException(string message) : base(message)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000C8A58 File Offset: 0x000C7C58
		public ArithmeticException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ArithmeticException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
