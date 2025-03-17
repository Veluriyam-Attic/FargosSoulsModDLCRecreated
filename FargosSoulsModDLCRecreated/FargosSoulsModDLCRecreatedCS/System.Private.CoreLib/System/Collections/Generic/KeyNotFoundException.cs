using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000802 RID: 2050
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class KeyNotFoundException : SystemException
	{
		// Token: 0x060061BB RID: 25019 RVA: 0x001D2298 File Offset: 0x001D1498
		public KeyNotFoundException() : base(SR.Arg_KeyNotFound)
		{
			base.HResult = -2146232969;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x001D22B0 File Offset: 0x001D14B0
		public KeyNotFoundException(string message) : base(message)
		{
			base.HResult = -2146232969;
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x001D22C4 File Offset: 0x001D14C4
		public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232969;
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
