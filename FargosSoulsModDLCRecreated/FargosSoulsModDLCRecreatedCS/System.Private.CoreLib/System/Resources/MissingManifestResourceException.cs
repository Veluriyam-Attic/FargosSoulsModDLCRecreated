using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000574 RID: 1396
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		// Token: 0x060047E0 RID: 18400 RVA: 0x0017EE40 File Offset: 0x0017E040
		public MissingManifestResourceException() : base(SR.Arg_MissingManifestResourceException)
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0017EE58 File Offset: 0x0017E058
		public MissingManifestResourceException(string message) : base(message)
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0017EE6C File Offset: 0x0017E06C
		public MissingManifestResourceException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
