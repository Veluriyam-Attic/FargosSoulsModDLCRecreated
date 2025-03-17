using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x020000CB RID: 203
	internal sealed class SZGenericArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x000C8CFA File Offset: 0x000C7EFA
		internal SZGenericArrayEnumerator(T[] array)
		{
			this._array = array;
			this._index = -1;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000C8D10 File Offset: 0x000C7F10
		public bool MoveNext()
		{
			int num = this._index + 1;
			if (num >= this._array.Length)
			{
				this._index = this._array.Length;
				return false;
			}
			this._index = num;
			return true;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x000C8D4C File Offset: 0x000C7F4C
		public T Current
		{
			get
			{
				int index = this._index;
				T[] array = this._array;
				if (index >= array.Length)
				{
					ThrowHelper.ThrowInvalidOperationException_EnumCurrent(index);
				}
				return array[index];
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000C8D7A File Offset: 0x000C7F7A
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000C8D87 File Offset: 0x000C7F87
		void IEnumerator.Reset()
		{
			this._index = -1;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void Dispose()
		{
		}

		// Token: 0x0400027E RID: 638
		private readonly T[] _array;

		// Token: 0x0400027F RID: 639
		private int _index;

		// Token: 0x04000280 RID: 640
		internal static readonly SZGenericArrayEnumerator<T> Empty = new SZGenericArrayEnumerator<T>(new T[0]);
	}
}
