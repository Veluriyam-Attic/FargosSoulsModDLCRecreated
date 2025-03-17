using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200036E RID: 878
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		// Token: 0x06002E17 RID: 11799 RVA: 0x0015BA7B File Offset: 0x0015AC7B
		public EncoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x0015BA88 File Offset: 0x0015AC88
		public EncoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidCharSequenceNoIndex, "replacement"));
			}
			this._strDefault = replacement;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0015BB02 File Offset: 0x0015AD02
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x0015BB0A File Offset: 0x0015AD0A
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x0015BB12 File Offset: 0x0015AD12
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0015BB20 File Offset: 0x0015AD20
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this._strDefault == encoderReplacementFallback._strDefault;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x0015BB4A File Offset: 0x0015AD4A
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04000CBA RID: 3258
		internal static readonly EncoderReplacementFallback s_default = new EncoderReplacementFallback();

		// Token: 0x04000CBB RID: 3259
		private readonly string _strDefault;
	}
}
