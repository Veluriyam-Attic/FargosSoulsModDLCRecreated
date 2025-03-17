using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200013A RID: 314
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x000DB0CE File Offset: 0x000DA2CE
		public InvalidProgramException() : base(SR.InvalidProgram_Default)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000DB0E6 File Offset: 0x000DA2E6
		public InvalidProgramException(string message) : base(message)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000DB0FA File Offset: 0x000DA2FA
		public InvalidProgramException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000C7203 File Offset: 0x000C6403
		private InvalidProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
