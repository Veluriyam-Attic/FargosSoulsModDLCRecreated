using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002BA RID: 698
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06002876 RID: 10358 RVA: 0x00148AE3 File Offset: 0x00147CE3
		internal ThreadAbortException()
		{
			base.HResult = -2146233040;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x000C26FD File Offset: 0x000C18FD
		public object ExceptionState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000C7203 File Offset: 0x000C6403
		private ThreadAbortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
