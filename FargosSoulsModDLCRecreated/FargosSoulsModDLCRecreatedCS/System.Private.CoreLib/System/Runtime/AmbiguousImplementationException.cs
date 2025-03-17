using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x020003D5 RID: 981
	[TypeForwardedFrom("System.Runtime, Version=4.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class AmbiguousImplementationException : Exception
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x0016A152 File Offset: 0x00169352
		public AmbiguousImplementationException() : base(SR.AmbiguousImplementationException_NullMessage)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x0016A16A File Offset: 0x0016936A
		public AmbiguousImplementationException(string message) : base(message)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x0016A17E File Offset: 0x0016937E
		public AmbiguousImplementationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000C84E2 File Offset: 0x000C76E2
		private AmbiguousImplementationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
