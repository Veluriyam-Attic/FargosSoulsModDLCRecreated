using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E3 RID: 227
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x000CD418 File Offset: 0x000CC618
		public DataMisalignedException() : base(SR.Arg_DataMisalignedException)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000CD430 File Offset: 0x000CC630
		public DataMisalignedException(string message) : base(message)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000CD444 File Offset: 0x000CC644
		public DataMisalignedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000C7203 File Offset: 0x000C6403
		private DataMisalignedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
