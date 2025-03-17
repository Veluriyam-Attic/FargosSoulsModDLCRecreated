using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x0200035F RID: 863
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		// Token: 0x06002D7D RID: 11645 RVA: 0x00159C1B File Offset: 0x00158E1B
		public DecoderFallbackException() : base(SR.Arg_ArgumentException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00159C33 File Offset: 0x00158E33
		public DecoderFallbackException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00159C47 File Offset: 0x00158E47
		public DecoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00159C5C File Offset: 0x00158E5C
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index) : base(message)
		{
			this._bytesUnknown = bytesUnknown;
			this._index = index;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000C8919 File Offset: 0x000C7B19
		private DecoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x00159C73 File Offset: 0x00158E73
		public byte[] BytesUnknown
		{
			get
			{
				return this._bytesUnknown;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x00159C7B File Offset: 0x00158E7B
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04000C8E RID: 3214
		private readonly byte[] _bytesUnknown;

		// Token: 0x04000C8F RID: 3215
		private readonly int _index;
	}
}
