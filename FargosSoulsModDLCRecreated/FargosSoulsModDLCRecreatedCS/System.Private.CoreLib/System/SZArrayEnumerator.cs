using System;
using System.Collections;

namespace System
{
	// Token: 0x020000CA RID: 202
	internal sealed class SZArrayEnumerator : IEnumerator, ICloneable
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x000C8C6D File Offset: 0x000C7E6D
		internal SZArrayEnumerator(Array array)
		{
			this._array = array;
			this._index = -1;
			this._endIndex = array.Length;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000AC0FA File Offset: 0x000AB2FA
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000C8C8F File Offset: 0x000C7E8F
		public bool MoveNext()
		{
			if (this._index < this._endIndex)
			{
				this._index++;
				return this._index < this._endIndex;
			}
			return false;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000C8CBD File Offset: 0x000C7EBD
		public object Current
		{
			get
			{
				if (this._index < 0)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
				}
				if (this._index >= this._endIndex)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
				}
				return this._array.GetValue(this._index);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000C8CF1 File Offset: 0x000C7EF1
		public void Reset()
		{
			this._index = -1;
		}

		// Token: 0x0400027B RID: 635
		private readonly Array _array;

		// Token: 0x0400027C RID: 636
		private int _index;

		// Token: 0x0400027D RID: 637
		private readonly int _endIndex;
	}
}
