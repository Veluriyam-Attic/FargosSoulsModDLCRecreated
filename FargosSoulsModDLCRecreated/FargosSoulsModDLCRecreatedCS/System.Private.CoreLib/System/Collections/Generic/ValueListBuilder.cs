using System;
using System.Buffers;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200080E RID: 2062
	[DefaultMember("Item")]
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x0600622E RID: 25134 RVA: 0x001D350B File Offset: 0x001D270B
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x0600622F RID: 25135 RVA: 0x001D3522 File Offset: 0x001D2722
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001D352C File Offset: 0x001D272C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x001D356F File Offset: 0x001D276F
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x001D3588 File Offset: 0x001D2788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x001D35AC File Offset: 0x001D27AC
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			bool flag = this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x04001D4C RID: 7500
		private Span<T> _span;

		// Token: 0x04001D4D RID: 7501
		private T[] _arrayFromPool;

		// Token: 0x04001D4E RID: 7502
		private int _pos;
	}
}
