using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C2 RID: 194
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x000C8460 File Offset: 0x000C7660
		public AppDomainUnloadedException() : base(SR.Arg_AppDomainUnloadedException)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000C8478 File Offset: 0x000C7678
		public AppDomainUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000C848C File Offset: 0x000C768C
		public AppDomainUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
