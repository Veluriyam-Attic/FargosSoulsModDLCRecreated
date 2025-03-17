using System;
using System.Collections;

namespace System
{
	// Token: 0x020000C9 RID: 201
	internal sealed class ArrayEnumerator : IEnumerator, ICloneable
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x000C8A70 File Offset: 0x000C7C70
		internal ArrayEnumerator(Array array, int index, int count)
		{
			this.array = array;
			this.index = index - 1;
			this.startIndex = index;
			this.endIndex = index + count;
			this._indices = new int[array.Rank];
			int num = 1;
			for (int i = 0; i < array.Rank; i++)
			{
				this._indices[i] = array.GetLowerBound(i);
				num *= array.GetLength(i);
			}
			int[] indices = this._indices;
			indices[indices.Length - 1]--;
			this._complete = (num == 0);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000C8B00 File Offset: 0x000C7D00
		private void IncArray()
		{
			int rank = this.array.Rank;
			this._indices[rank - 1]++;
			for (int i = rank - 1; i >= 0; i--)
			{
				if (this._indices[i] > this.array.GetUpperBound(i))
				{
					if (i == 0)
					{
						this._complete = true;
						return;
					}
					for (int j = i; j < rank; j++)
					{
						this._indices[j] = this.array.GetLowerBound(j);
					}
					this._indices[i - 1]++;
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000AC0FA File Offset: 0x000AB2FA
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000C8B8E File Offset: 0x000C7D8E
		public bool MoveNext()
		{
			if (this._complete)
			{
				this.index = this.endIndex;
				return false;
			}
			this.index++;
			this.IncArray();
			return !this._complete;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000C8BC3 File Offset: 0x000C7DC3
		public object Current
		{
			get
			{
				if (this.index < this.startIndex)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
				}
				if (this._complete)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
				}
				return this.array.GetValue(this._indices);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000C8BF8 File Offset: 0x000C7DF8
		public void Reset()
		{
			this.index = this.startIndex - 1;
			int num = 1;
			for (int i = 0; i < this.array.Rank; i++)
			{
				this._indices[i] = this.array.GetLowerBound(i);
				num *= this.array.GetLength(i);
			}
			this._complete = (num == 0);
			int[] indices = this._indices;
			indices[indices.Length - 1]--;
		}

		// Token: 0x04000275 RID: 629
		private readonly Array array;

		// Token: 0x04000276 RID: 630
		private int index;

		// Token: 0x04000277 RID: 631
		private readonly int endIndex;

		// Token: 0x04000278 RID: 632
		private readonly int startIndex;

		// Token: 0x04000279 RID: 633
		private readonly int[] _indices;

		// Token: 0x0400027A RID: 634
		private bool _complete;
	}
}
