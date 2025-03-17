using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000699 RID: 1689
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		// Token: 0x060055BE RID: 21950 RVA: 0x001A4C46 File Offset: 0x001A3E46
		public InvalidDataException() : base(SR.GenericInvalidData)
		{
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x001476D1 File Offset: 0x001468D1
		public InvalidDataException(string message) : base(message)
		{
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x001476DA File Offset: 0x001468DA
		public InvalidDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x000C7203 File Offset: 0x000C6403
		private InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
