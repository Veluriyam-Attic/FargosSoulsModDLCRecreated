using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001AF RID: 431
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x000FC520 File Offset: 0x000FB720
		public TypeUnloadedException() : base(SR.Arg_TypeUnloadedException)
		{
			base.HResult = -2146234349;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000FC538 File Offset: 0x000FB738
		public TypeUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234349;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000FC54C File Offset: 0x000FB74C
		public TypeUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234349;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
