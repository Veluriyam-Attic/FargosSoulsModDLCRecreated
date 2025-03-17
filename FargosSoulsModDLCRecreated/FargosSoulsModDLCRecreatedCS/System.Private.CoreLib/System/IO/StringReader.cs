using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006AE RID: 1710
	[NullableContext(1)]
	[Nullable(0)]
	public class StringReader : TextReader
	{
		// Token: 0x060056D7 RID: 22231 RVA: 0x001ABA6A File Offset: 0x001AAC6A
		public StringReader(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			this._s = s;
			this._length = s.Length;
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x001A80AB File Offset: 0x001A72AB
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x001ABA94 File Offset: 0x001AAC94
		protected override void Dispose(bool disposing)
		{
			this._s = null;
			this._pos = 0;
			this._length = 0;
			base.Dispose(disposing);
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x001ABAB2 File Offset: 0x001AACB2
		public override int Peek()
		{
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			return (int)this._s[this._pos];
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x001ABAEC File Offset: 0x001AACEC
		public override int Read()
		{
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			string s = this._s;
			int pos = this._pos;
			this._pos = pos + 1;
			return (int)s[pos];
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x001ABB3C File Offset: 0x001AAD3C
		public override int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			int num = this._length - this._pos;
			if (num > 0)
			{
				if (num > count)
				{
					num = count;
				}
				this._s.CopyTo(this._pos, buffer, index, num);
				this._pos += num;
			}
			return num;
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x001ABBE8 File Offset: 0x001AADE8
		[NullableContext(0)]
		public override int Read(Span<char> buffer)
		{
			if (base.GetType() != typeof(StringReader))
			{
				return base.Read(buffer);
			}
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			int num = this._length - this._pos;
			if (num > 0)
			{
				if (num > buffer.Length)
				{
					num = buffer.Length;
				}
				this._s.AsSpan(this._pos, num).CopyTo(buffer);
				this._pos += num;
			}
			return num;
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x001ABC76 File Offset: 0x001AAE76
		[NullableContext(0)]
		public override int ReadBlock(Span<char> buffer)
		{
			return this.Read(buffer);
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x001ABC80 File Offset: 0x001AAE80
		public override string ReadToEnd()
		{
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			string result;
			if (this._pos == 0)
			{
				result = this._s;
			}
			else
			{
				result = this._s.Substring(this._pos, this._length - this._pos);
			}
			this._pos = this._length;
			return result;
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x001ABCE0 File Offset: 0x001AAEE0
		[NullableContext(2)]
		public override string ReadLine()
		{
			if (this._s == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ReaderClosed);
			}
			int i;
			for (i = this._pos; i < this._length; i++)
			{
				char c = this._s[i];
				if (c == '\r' || c == '\n')
				{
					string result = this._s.Substring(this._pos, i - this._pos);
					this._pos = i + 1;
					if (c == '\r' && this._pos < this._length && this._s[this._pos] == '\n')
					{
						this._pos++;
					}
					return result;
				}
			}
			if (i > this._pos)
			{
				string result2 = this._s.Substring(this._pos, i - this._pos);
				this._pos = i;
				return result2;
			}
			return null;
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x001ABDB6 File Offset: 0x001AAFB6
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public override Task<string> ReadLineAsync()
		{
			return Task.FromResult<string>(this.ReadLine());
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x001ABDC3 File Offset: 0x001AAFC3
		public override Task<string> ReadToEndAsync()
		{
			return Task.FromResult<string>(this.ReadToEnd());
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x001ABDD0 File Offset: 0x001AAFD0
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x001ABE34 File Offset: 0x001AB034
		[NullableContext(0)]
		public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(this.ReadBlock(buffer.Span));
			}
			return ValueTask.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x001ABE58 File Offset: 0x001AB058
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			return Task.FromResult<int>(this.Read(buffer, index, count));
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x001ABEBC File Offset: 0x001AB0BC
		[NullableContext(0)]
		public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(this.Read(buffer.Span));
			}
			return ValueTask.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x040018F4 RID: 6388
		private string _s;

		// Token: 0x040018F5 RID: 6389
		private int _pos;

		// Token: 0x040018F6 RID: 6390
		private int _length;
	}
}
