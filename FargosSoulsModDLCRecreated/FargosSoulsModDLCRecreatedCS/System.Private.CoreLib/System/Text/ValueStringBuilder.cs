using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000391 RID: 913
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct ValueStringBuilder
	{
		// Token: 0x06003038 RID: 12344 RVA: 0x001647A4 File Offset: 0x001639A4
		internal void AppendSpanFormattable<T>(T value, string format, IFormatProvider provider) where T : ISpanFormattable, IFormattable
		{
			int num;
			if (value.TryFormat(this._chars.Slice(this._pos), out num, format, provider))
			{
				this._pos += num;
				return;
			}
			this.Append(value.ToString(format, provider));
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00164800 File Offset: 0x00163A00
		internal void AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			ICustomFormatter customFormatter = (ICustomFormatter)((provider != null) ? provider.GetFormat(typeof(ICustomFormatter)) : null);
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							ValueStringBuilder.ThrowFormatError();
						}
					}
					else if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_8F;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_8F:
				if (num == length)
				{
					return;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					ValueStringBuilder.ThrowFormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						ValueStringBuilder.ThrowFormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						ValueStringBuilder.ThrowFormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							ValueStringBuilder.ThrowFormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						ValueStringBuilder.ThrowFormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							ValueStringBuilder.ThrowFormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
				if (c == ':')
				{
					num++;
					int num4 = num;
					for (;;)
					{
						if (num == length)
						{
							ValueStringBuilder.ThrowFormatError();
						}
						c = format[num];
						if (c == '}')
						{
							break;
						}
						if (c == '{')
						{
							ValueStringBuilder.ThrowFormatError();
						}
						num++;
					}
					if (num > num4)
					{
						readOnlySpan = format.AsSpan(num4, num - num4);
					}
				}
				else if (c != '}')
				{
					ValueStringBuilder.ThrowFormatError();
				}
				num++;
				string text = null;
				string text2 = null;
				if (customFormatter != null)
				{
					if (readOnlySpan.Length != 0)
					{
						text2 = new string(readOnlySpan);
					}
					text = customFormatter.Format(text2, obj, provider);
				}
				if (text == null)
				{
					ISpanFormattable spanFormattable = obj as ISpanFormattable;
					int num5;
					if (spanFormattable != null && (flag || num3 == 0) && spanFormattable.TryFormat(this._chars.Slice(this._pos), out num5, readOnlySpan, provider))
					{
						this._pos += num5;
						int num6 = num3 - num5;
						if (flag && num6 > 0)
						{
							this.Append(' ', num6);
							continue;
						}
						continue;
					}
					else
					{
						IFormattable formattable = obj as IFormattable;
						if (formattable != null)
						{
							if (readOnlySpan.Length != 0 && text2 == null)
							{
								text2 = new string(readOnlySpan);
							}
							text = formattable.ToString(text2, provider);
						}
						else if (obj != null)
						{
							text = obj.ToString();
						}
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
				int num7 = num3 - text.Length;
				if (!flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
				this.Append(text);
				if (flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
			}
			throw new FormatException(SR.Format_IndexOutOfRange);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00156A11 File Offset: 0x00155C11
		private static void ThrowFormatError()
		{
			throw new FormatException(SR.Format_InvalidString);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x00164B60 File Offset: 0x00163D60
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00164B77 File Offset: 0x00163D77
		public ValueStringBuilder(int initialCapacity)
		{
			this._arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialCapacity);
			this._chars = this._arrayToReturnToPool;
			this._pos = 0;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x00164BA2 File Offset: 0x00163DA2
		// (set) Token: 0x0600303E RID: 12350 RVA: 0x00164BAA File Offset: 0x00163DAA
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x00164BB3 File Offset: 0x00163DB3
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00164BC0 File Offset: 0x00163DC0
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._pos);
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x00164BDE File Offset: 0x00163DDE
		public ref char GetPinnableReference()
		{
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00164BEB File Offset: 0x00163DEB
		public unsafe ref char GetPinnableReference(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x1700097A RID: 2426
		public char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x00164C2C File Offset: 0x00163E2C
		public override string ToString()
		{
			string result = this._chars.Slice(0, this._pos).ToString();
			this.Dispose();
			return result;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x00164C61 File Offset: 0x00163E61
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x00164C69 File Offset: 0x00163E69
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x00164CA6 File Offset: 0x00163EA6
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00164CBF File Offset: 0x00163EBF
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x00164CDA File Offset: 0x00163EDA
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x00164CF0 File Offset: 0x00163EF0
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x00164D34 File Offset: 0x00163F34
		public void Insert(int index, string s)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			int length2 = this._pos - index;
			this._chars.Slice(index, length2).CopyTo(this._chars.Slice(index + length));
			s.AsSpan().CopyTo(this._chars.Slice(index));
			this._pos += length;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x00164DBC File Offset: 0x00163FBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x00164E00 File Offset: 0x00164000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			if (s == null)
			{
				return;
			}
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x00164E54 File Offset: 0x00164054
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x00164EB8 File Offset: 0x001640B8
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x00164F20 File Offset: 0x00164120
		public unsafe void Append(char* value, int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x00164F90 File Offset: 0x00164190
		public void Append(ReadOnlySpan<char> value)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x00164FF4 File Offset: 0x001641F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x00165035 File Offset: 0x00164235
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x00165048 File Offset: 0x00164248
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int additionalCapacityBeyondPos)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + additionalCapacityBeyondPos, this._chars.Length * 2));
			this._chars.Slice(0, this._pos).CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x001650C8 File Offset: 0x001642C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04000D41 RID: 3393
		private char[] _arrayToReturnToPool;

		// Token: 0x04000D42 RID: 3394
		private Span<char> _chars;

		// Token: 0x04000D43 RID: 3395
		private int _pos;
	}
}
