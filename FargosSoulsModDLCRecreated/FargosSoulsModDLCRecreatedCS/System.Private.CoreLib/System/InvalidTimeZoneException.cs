using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200013B RID: 315
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		// Token: 0x06001027 RID: 4135 RVA: 0x000DB10F File Offset: 0x000DA30F
		public InvalidTimeZoneException()
		{
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000DB117 File Offset: 0x000DA317
		public InvalidTimeZoneException(string message) : base(message)
		{
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000DB120 File Offset: 0x000DA320
		public InvalidTimeZoneException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
