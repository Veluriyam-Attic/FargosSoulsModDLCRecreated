using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x020003D2 RID: 978
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class CryptographicException : SystemException
	{
		// Token: 0x060031CF RID: 12751 RVA: 0x0016A0B9 File Offset: 0x001692B9
		public CryptographicException() : base(SR.Arg_CryptographyException)
		{
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x0016A0C6 File Offset: 0x001692C6
		public CryptographicException(int hr) : base(SR.Arg_CryptographyException)
		{
			base.HResult = hr;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x001476D1 File Offset: 0x001468D1
		[NullableContext(2)]
		public CryptographicException(string message) : base(message)
		{
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x001476DA File Offset: 0x001468DA
		[NullableContext(2)]
		public CryptographicException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x0016A0DA File Offset: 0x001692DA
		public CryptographicException(string format, [Nullable(2)] string insert) : base(string.Format(format, insert))
		{
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000C7203 File Offset: 0x000C6403
		protected CryptographicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
