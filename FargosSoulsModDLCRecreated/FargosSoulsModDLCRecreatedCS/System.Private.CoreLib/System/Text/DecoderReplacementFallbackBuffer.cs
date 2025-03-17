using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000364 RID: 868
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x0015A76F File Offset: 0x0015996F
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0015A791 File Offset: 0x00159991
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this._strDefault.Length == 0)
			{
				return false;
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return true;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x0015A7CC File Offset: 0x001599CC
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

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0015A827 File Offset: 0x00159A27
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

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x0015A85A File Offset: 0x00159A5A
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

		// Token: 0x06002DBB RID: 11707 RVA: 0x0015A86D File Offset: 0x00159A6D
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0015A885 File Offset: 0x00159A85
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this._strDefault.Length;
		}

		// Token: 0x04000C9D RID: 3229
		private readonly string _strDefault;

		// Token: 0x04000C9E RID: 3230
		private int _fallbackCount = -1;

		// Token: 0x04000C9F RID: 3231
		private int _fallbackIndex = -1;
	}
}
