using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200060A RID: 1546
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		// Token: 0x06004E13 RID: 19987 RVA: 0x0018CD55 File Offset: 0x0018BF55
		public TargetParameterCountException() : base(SR.Arg_TargetParameterCountException)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0018CD6D File Offset: 0x0018BF6D
		public TargetParameterCountException(string message) : base(message)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0018CD81 File Offset: 0x0018BF81
		public TargetParameterCountException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0014AC10 File Offset: 0x00149E10
		private TargetParameterCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
