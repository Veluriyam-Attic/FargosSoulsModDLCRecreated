using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A0 RID: 416
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		// Token: 0x06001955 RID: 6485 RVA: 0x000DB10F File Offset: 0x000DA30F
		public TimeZoneNotFoundException()
		{
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000DB117 File Offset: 0x000DA317
		public TimeZoneNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000DB120 File Offset: 0x000DA320
		public TimeZoneNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
