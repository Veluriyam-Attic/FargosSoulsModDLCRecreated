using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000608 RID: 1544
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TargetException : ApplicationException
	{
		// Token: 0x06004E0C RID: 19980 RVA: 0x0018CCFF File Offset: 0x0018BEFF
		public TargetException() : this(null)
		{
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x0018CD08 File Offset: 0x0018BF08
		public TargetException(string message) : this(message, null)
		{
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0018CD12 File Offset: 0x0018BF12
		public TargetException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232829;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0014AC10 File Offset: 0x00149E10
		[NullableContext(1)]
		protected TargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
