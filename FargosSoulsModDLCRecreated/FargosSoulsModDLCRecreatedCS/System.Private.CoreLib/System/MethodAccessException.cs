using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200014F RID: 335
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x000DEC22 File Offset: 0x000DDE22
		public MethodAccessException() : base(SR.Arg_MethodAccessException)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000DEC3A File Offset: 0x000DDE3A
		public MethodAccessException(string message) : base(message)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000DEC4E File Offset: 0x000DDE4E
		public MethodAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000D00AE File Offset: 0x000CF2AE
		[NullableContext(1)]
		protected MethodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
