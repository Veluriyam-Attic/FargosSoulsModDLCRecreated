using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000363 RID: 867
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DecoderReplacementFallback : DecoderFallback
	{
		// Token: 0x06002DAE RID: 11694 RVA: 0x0015A684 File Offset: 0x00159884
		public DecoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x0015A694 File Offset: 0x00159894
		public DecoderReplacementFallback(string replacement)
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

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x0015A70E File Offset: 0x0015990E
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0015A716 File Offset: 0x00159916
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0015A71E File Offset: 0x0015991E
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x0015A72C File Offset: 0x0015992C
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this._strDefault == decoderReplacementFallback._strDefault;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x0015A756 File Offset: 0x00159956
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04000C9B RID: 3227
		internal static readonly DecoderReplacementFallback s_default = new DecoderReplacementFallback();

		// Token: 0x04000C9C RID: 3228
		private readonly string _strDefault;
	}
}
