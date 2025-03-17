using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200036F RID: 879
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x06002E1F RID: 11807 RVA: 0x0015BB63 File Offset: 0x0015AD63
		[NullableContext(1)]
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString + fallback.DefaultString;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0015BB90 File Offset: 0x0015AD90
		public override bool Fallback(char charUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				if (char.IsHighSurrogate(charUnknown) && this._fallbackCount >= 0 && char.IsLowSurrogate(this._strDefault[this._fallbackIndex + 1]))
				{
					EncoderFallbackBuffer.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this._strDefault[this._fallbackIndex + 1]));
				}
				EncoderFallbackBuffer.ThrowLastCharRecursive((int)charUnknown);
			}
			this._fallbackCount = this._strDefault.Length / 2;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x0015BC18 File Offset: 0x0015AE18
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format(SR.ArgumentOutOfRange_Range, 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format(SR.ArgumentOutOfRange_Range, 56320, 57343));
			}
			if (this._fallbackCount >= 1)
			{
				EncoderFallbackBuffer.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x0015BCC0 File Offset: 0x0015AEC0
		public override char GetNextChar()
		{
			this._fallbackCount--;
			this._fallbackIndex++;
			if (this._fallbackCount < 0)
			{
				return '\0';
			}
			if (this._fallbackCount == 2147483647)
			{
				this._fallbackCount = -1;
				return '\0';
			}
			return this._strDefault[this._fallbackIndex];
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x0015BD1B File Offset: 0x0015AF1B
		public override bool MovePrevious()
		{
			if (this._fallbackCount >= -1 && this._fallbackIndex >= 0)
			{
				this._fallbackIndex--;
				this._fallbackCount++;
				return true;
			}
			return false;
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x0015BD4E File Offset: 0x0015AF4E
		public override int Remaining
		{
			get
			{
				if (this._fallbackCount >= 0)
				{
					return this._fallbackCount;
				}
				return 0;
			}
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x0015BD61 File Offset: 0x0015AF61
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = 0;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x04000CBC RID: 3260
		private readonly string _strDefault;

		// Token: 0x04000CBD RID: 3261
		private int _fallbackCount = -1;

		// Token: 0x04000CBE RID: 3262
		private int _fallbackIndex = -1;
	}
}
