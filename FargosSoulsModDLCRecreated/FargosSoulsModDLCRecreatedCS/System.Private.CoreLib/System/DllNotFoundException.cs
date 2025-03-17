using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000EC RID: 236
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x000CFA93 File Offset: 0x000CEC93
		public DllNotFoundException() : base(SR.Arg_DllNotFoundException)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000CFAAB File Offset: 0x000CECAB
		public DllNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000CFABF File Offset: 0x000CECBF
		public DllNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000CFAD4 File Offset: 0x000CECD4
		[NullableContext(1)]
		protected DllNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
