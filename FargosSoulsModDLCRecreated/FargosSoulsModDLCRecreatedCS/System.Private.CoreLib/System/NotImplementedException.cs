using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000156 RID: 342
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x000DEF06 File Offset: 0x000DE106
		public NotImplementedException() : base(SR.Arg_NotImplementedException)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000DEF1E File Offset: 0x000DE11E
		public NotImplementedException(string message) : base(message)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000DEF32 File Offset: 0x000DE132
		public NotImplementedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected NotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
