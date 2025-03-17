using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049C RID: 1180
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		// Token: 0x060044B7 RID: 17591 RVA: 0x00179494 File Offset: 0x00178694
		public SafeArrayTypeMismatchException() : base(SR.Arg_SafeArrayTypeMismatchException)
		{
			base.HResult = -2146233037;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x001794AC File Offset: 0x001786AC
		public SafeArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233037;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x001794C0 File Offset: 0x001786C0
		public SafeArrayTypeMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233037;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
