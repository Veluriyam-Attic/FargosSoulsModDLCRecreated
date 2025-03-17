using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001AC RID: 428
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		// Token: 0x06001A2F RID: 6703 RVA: 0x000FC431 File Offset: 0x000FB631
		public TypeAccessException() : base(SR.Arg_TypeAccessException)
		{
			base.HResult = -2146233021;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000FC449 File Offset: 0x000FB649
		public TypeAccessException(string message) : base(message)
		{
			base.HResult = -2146233021;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000FC45D File Offset: 0x000FB65D
		public TypeAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233021;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000CFAD4 File Offset: 0x000CECD4
		[NullableContext(1)]
		protected TypeAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
