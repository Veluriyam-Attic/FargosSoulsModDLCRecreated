using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000EE RID: 238
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x06000DA8 RID: 3496 RVA: 0x000CFF78 File Offset: 0x000CF178
		public DuplicateWaitObjectException() : base(SR.Arg_DuplicateWaitObjectException)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000CFF90 File Offset: 0x000CF190
		public DuplicateWaitObjectException(string parameterName) : base(SR.Arg_DuplicateWaitObjectException, parameterName)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000CFFA9 File Offset: 0x000CF1A9
		public DuplicateWaitObjectException(string parameterName, string message) : base(message, parameterName)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000CFFBE File Offset: 0x000CF1BE
		public DuplicateWaitObjectException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000C8919 File Offset: 0x000C7B19
		[NullableContext(1)]
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
