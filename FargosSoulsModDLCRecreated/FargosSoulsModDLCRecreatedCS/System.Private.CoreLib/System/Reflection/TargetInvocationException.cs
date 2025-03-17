using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000609 RID: 1545
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x06004E10 RID: 19984 RVA: 0x0018CD27 File Offset: 0x0018BF27
		public TargetInvocationException(Exception inner) : base(SR.Arg_TargetInvocationException, inner)
		{
			base.HResult = -2146232828;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0018CD40 File Offset: 0x0018BF40
		public TargetInvocationException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232828;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x0014AC10 File Offset: 0x00149E10
		private TargetInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
