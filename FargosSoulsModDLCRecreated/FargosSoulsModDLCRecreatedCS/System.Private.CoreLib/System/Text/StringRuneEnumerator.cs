using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200037F RID: 895
	public struct StringRuneEnumerator : IEnumerable<Rune>, IEnumerable, IEnumerator<Rune>, IDisposable, IEnumerator
	{
		// Token: 0x06002F5A RID: 12122 RVA: 0x0016029F File Offset: 0x0015F49F
		internal StringRuneEnumerator(string value)
		{
			this._string = value;
			this._current = default(Rune);
			this._nextIndex = 0;
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x001602BB File Offset: 0x0015F4BB
		public Rune Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x001602C3 File Offset: 0x0015F4C3
		public StringRuneEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x001602CC File Offset: 0x0015F4CC
		public bool MoveNext()
		{
			if ((ulong)this._nextIndex >= (ulong)((long)this._string.Length))
			{
				this._current = default(Rune);
				return false;
			}
			if (!Rune.TryGetRuneAt(this._string, this._nextIndex, out this._current))
			{
				this._current = Rune.ReplacementChar;
			}
			this._nextIndex += this._current.Utf16SequenceLength;
			return true;
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x00160339 File Offset: 0x0015F539
		[Nullable(2)]
		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000AB30B File Offset: 0x000AA50B
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x00160346 File Offset: 0x0015F546
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00160346 File Offset: 0x0015F546
		IEnumerator<Rune> IEnumerable<Rune>.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x00160353 File Offset: 0x0015F553
		void IEnumerator.Reset()
		{
			this._current = default(Rune);
			this._nextIndex = 0;
		}

		// Token: 0x04000CF6 RID: 3318
		private readonly string _string;

		// Token: 0x04000CF7 RID: 3319
		private Rune _current;

		// Token: 0x04000CF8 RID: 3320
		private int _nextIndex;
	}
}
