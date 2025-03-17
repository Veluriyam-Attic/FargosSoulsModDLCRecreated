using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C6 RID: 198
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x000C88BE File Offset: 0x000C7ABE
		public ArgumentNullException() : base(SR.ArgumentNull_Generic)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000C88D6 File Offset: 0x000C7AD6
		public ArgumentNullException(string paramName) : base(SR.ArgumentNull_Generic, paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000C88EF File Offset: 0x000C7AEF
		public ArgumentNullException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000C8904 File Offset: 0x000C7B04
		public ArgumentNullException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000C8919 File Offset: 0x000C7B19
		[NullableContext(1)]
		protected ArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
