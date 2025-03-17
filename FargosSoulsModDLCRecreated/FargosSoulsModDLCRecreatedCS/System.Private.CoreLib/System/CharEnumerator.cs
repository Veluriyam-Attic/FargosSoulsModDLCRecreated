using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000DB RID: 219
	public sealed class CharEnumerator : IEnumerator, IEnumerator<char>, IDisposable, ICloneable
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x000CAC4F File Offset: 0x000C9E4F
		internal CharEnumerator(string str)
		{
			this._str = str;
			this._index = -1;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000AC0FA File Offset: 0x000AB2FA
		[NullableContext(1)]
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000CAC68 File Offset: 0x000C9E68
		public bool MoveNext()
		{
			if (this._index < this._str.Length - 1)
			{
				this._index++;
				this._currentElement = this._str[this._index];
				return true;
			}
			this._index = this._str.Length;
			return false;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000CACC3 File Offset: 0x000C9EC3
		public void Dispose()
		{
			if (this._str != null)
			{
				this._index = this._str.Length;
			}
			this._str = null;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000CACE5 File Offset: 0x000C9EE5
		[Nullable(2)]
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000CACF2 File Offset: 0x000C9EF2
		public char Current
		{
			get
			{
				if (this._index == -1)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
				}
				if (this._index >= this._str.Length)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
				}
				return this._currentElement;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000CAD2C File Offset: 0x000C9F2C
		public void Reset()
		{
			this._currentElement = '\0';
			this._index = -1;
		}

		// Token: 0x040002AC RID: 684
		private string _str;

		// Token: 0x040002AD RID: 685
		private int _index;

		// Token: 0x040002AE RID: 686
		private char _currentElement;
	}
}
