using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000CE RID: 206
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x000C920F File Offset: 0x000C840F
		public ArrayTypeMismatchException() : base(SR.Arg_ArrayTypeMismatchException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000C9227 File Offset: 0x000C8427
		public ArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000C923B File Offset: 0x000C843B
		public ArrayTypeMismatchException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
