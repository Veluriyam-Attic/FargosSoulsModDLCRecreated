using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Globalization
{
	// Token: 0x02000227 RID: 551
	[NullableContext(1)]
	[Nullable(0)]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x001343DD File Offset: 0x001335DD
		internal TextElementEnumerator(string str, int startIndex)
		{
			this._str = str;
			this._strStartIndex = startIndex;
			this.Reset();
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x001343FC File Offset: 0x001335FC
		public bool MoveNext()
		{
			this._currentTextElementSubstr = null;
			int num = this._currentTextElementOffset + this._currentTextElementLength;
			this._currentTextElementOffset = num;
			this._currentTextElementLength = 0;
			if (num >= this._str.Length)
			{
				return false;
			}
			this._currentTextElementLength = TextSegmentationUtility.GetLengthOfFirstUtf16ExtendedGraphemeCluster(this._str.AsSpan(num));
			return true;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x00134454 File Offset: 0x00133654
		public object Current
		{
			get
			{
				return this.GetTextElement();
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0013445C File Offset: 0x0013365C
		public string GetTextElement()
		{
			string text = this._currentTextElementSubstr;
			if (text == null)
			{
				if (this._currentTextElementOffset >= this._str.Length)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				text = this._str.Substring(this._currentTextElementOffset, this._currentTextElementLength);
				this._currentTextElementSubstr = text;
			}
			return text;
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x001344B1 File Offset: 0x001336B1
		public int ElementIndex
		{
			get
			{
				if (this._currentTextElementOffset >= this._str.Length)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				return this._currentTextElementOffset - this._strStartIndex;
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x001344DE File Offset: 0x001336DE
		public void Reset()
		{
			this._currentTextElementOffset = this._str.Length;
			this._currentTextElementLength = this._strStartIndex - this._str.Length;
			this._currentTextElementSubstr = null;
		}

		// Token: 0x040008E0 RID: 2272
		private readonly string _str;

		// Token: 0x040008E1 RID: 2273
		private readonly int _strStartIndex;

		// Token: 0x040008E2 RID: 2274
		private int _currentTextElementOffset;

		// Token: 0x040008E3 RID: 2275
		private int _currentTextElementLength;

		// Token: 0x040008E4 RID: 2276
		private string _currentTextElementSubstr;
	}
}
