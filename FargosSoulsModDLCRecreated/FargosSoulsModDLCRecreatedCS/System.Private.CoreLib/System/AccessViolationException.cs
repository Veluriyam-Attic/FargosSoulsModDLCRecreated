using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A8 RID: 168
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class AccessViolationException : SystemException
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x000C71C2 File Offset: 0x000C63C2
		public AccessViolationException() : base(SR.Arg_AccessViolationException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000C71DA File Offset: 0x000C63DA
		public AccessViolationException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000C71EE File Offset: 0x000C63EE
		public AccessViolationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected AccessViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400025C RID: 604
		private IntPtr _ip;

		// Token: 0x0400025D RID: 605
		private IntPtr _target;

		// Token: 0x0400025E RID: 606
		private int _accessType;
	}
}
