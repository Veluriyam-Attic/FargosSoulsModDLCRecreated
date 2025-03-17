using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002B9 RID: 697
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		// Token: 0x06002872 RID: 10354 RVA: 0x00148AA2 File Offset: 0x00147CA2
		public SynchronizationLockException() : base(SR.Arg_SynchronizationLockException)
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00148ABA File Offset: 0x00147CBA
		public SynchronizationLockException(string message) : base(message)
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00148ACE File Offset: 0x00147CCE
		public SynchronizationLockException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
