using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002EA RID: 746
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		// Token: 0x0600292A RID: 10538 RVA: 0x0014ABCF File Offset: 0x00149DCF
		public WaitHandleCannotBeOpenedException() : base(SR.Threading_WaitHandleCannotBeOpenedException)
		{
			base.HResult = -2146233044;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0014ABE7 File Offset: 0x00149DE7
		public WaitHandleCannotBeOpenedException(string message) : base(message)
		{
			base.HResult = -2146233044;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0014ABFB File Offset: 0x00149DFB
		public WaitHandleCannotBeOpenedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233044;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0014AC10 File Offset: 0x00149E10
		[NullableContext(1)]
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
