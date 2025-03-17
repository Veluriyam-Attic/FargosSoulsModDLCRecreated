using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003EC RID: 1004
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x0600325F RID: 12895 RVA: 0x0016B16F File Offset: 0x0016A36F
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this._members = members;
			this._data = info;
			this._types = types;
			this._numItems = numItems - 1;
			this._currItem = -1;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x0016B19D File Offset: 0x0016A39D
		public bool MoveNext()
		{
			if (this._currItem < this._numItems)
			{
				this._currItem++;
				this._current = true;
			}
			else
			{
				this._current = false;
			}
			return this._current;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x0016B1D1 File Offset: 0x0016A3D1
		[Nullable(2)]
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x0016B1E0 File Offset: 0x0016A3E0
		public SerializationEntry Current
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				return new SerializationEntry(this._members[this._currItem], this._data[this._currItem], this._types[this._currItem]);
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x0016B22C File Offset: 0x0016A42C
		public void Reset()
		{
			this._currItem = -1;
			this._current = false;
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x0016B23C File Offset: 0x0016A43C
		public string Name
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				return this._members[this._currItem];
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x0016B25E File Offset: 0x0016A45E
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				return this._data[this._currItem];
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x0016B280 File Offset: 0x0016A480
		public Type ObjectType
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
				return this._types[this._currItem];
			}
		}

		// Token: 0x04000E04 RID: 3588
		private readonly string[] _members;

		// Token: 0x04000E05 RID: 3589
		private readonly object[] _data;

		// Token: 0x04000E06 RID: 3590
		private readonly Type[] _types;

		// Token: 0x04000E07 RID: 3591
		private readonly int _numItems;

		// Token: 0x04000E08 RID: 3592
		private int _currItem;

		// Token: 0x04000E09 RID: 3593
		private bool _current;
	}
}
