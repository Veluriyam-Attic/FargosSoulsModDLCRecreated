using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002A1 RID: 673
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x060027AB RID: 10155 RVA: 0x000DB10F File Offset: 0x000DA30F
		public LockRecursionException()
		{
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000DB117 File Offset: 0x000DA317
		public LockRecursionException(string message) : base(message)
		{
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000DB120 File Offset: 0x000DA320
		public LockRecursionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000C84E2 File Offset: 0x000C76E2
		[NullableContext(1)]
		protected LockRecursionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
