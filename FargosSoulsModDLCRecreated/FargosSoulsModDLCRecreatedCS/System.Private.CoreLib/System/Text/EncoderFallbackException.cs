using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000368 RID: 872
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		// Token: 0x06002DD8 RID: 11736 RVA: 0x00159C1B File Offset: 0x00158E1B
		public EncoderFallbackException() : base(SR.Arg_ArgumentException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00159C33 File Offset: 0x00158E33
		public EncoderFallbackException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x00159C47 File Offset: 0x00158E47
		public EncoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0015ADE5 File Offset: 0x00159FE5
		internal EncoderFallbackException(string message, char charUnknown, int index) : base(message)
		{
			this._charUnknown = charUnknown;
			this._index = index;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0015ADFC File Offset: 0x00159FFC
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index) : base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format(SR.ArgumentOutOfRange_Range, 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", SR.Format(SR.ArgumentOutOfRange_Range, 56320, 57343));
			}
			this._charUnknownHigh = charUnknownHigh;
			this._charUnknownLow = charUnknownLow;
			this._index = index;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000C8919 File Offset: 0x000C7B19
		private EncoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x0015AE88 File Offset: 0x0015A088
		public char CharUnknown
		{
			get
			{
				return this._charUnknown;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x0015AE90 File Offset: 0x0015A090
		public char CharUnknownHigh
		{
			get
			{
				return this._charUnknownHigh;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0015AE98 File Offset: 0x0015A098
		public char CharUnknownLow
		{
			get
			{
				return this._charUnknownLow;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x0015AEA0 File Offset: 0x0015A0A0
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0015AEA8 File Offset: 0x0015A0A8
		public bool IsUnknownSurrogate()
		{
			return this._charUnknownHigh > '\0';
		}

		// Token: 0x04000CA3 RID: 3235
		private readonly char _charUnknown;

		// Token: 0x04000CA4 RID: 3236
		private readonly char _charUnknownHigh;

		// Token: 0x04000CA5 RID: 3237
		private readonly char _charUnknownLow;

		// Token: 0x04000CA6 RID: 3238
		private readonly int _index;
	}
}
