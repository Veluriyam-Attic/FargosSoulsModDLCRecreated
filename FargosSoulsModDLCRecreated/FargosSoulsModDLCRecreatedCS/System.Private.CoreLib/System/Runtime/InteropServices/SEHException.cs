using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049F RID: 1183
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SEHException : ExternalException
	{
		// Token: 0x060044DA RID: 17626 RVA: 0x00179B23 File Offset: 0x00178D23
		public SEHException()
		{
			base.HResult = -2147467259;
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x00178E01 File Offset: 0x00178001
		public SEHException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00178E15 File Offset: 0x00178015
		public SEHException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x00178E3A File Offset: 0x0017803A
		[NullableContext(1)]
		protected SEHException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
