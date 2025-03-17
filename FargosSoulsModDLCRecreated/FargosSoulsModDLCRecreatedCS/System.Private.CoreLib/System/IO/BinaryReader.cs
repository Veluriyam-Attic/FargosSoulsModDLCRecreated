using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200067D RID: 1661
	[NullableContext(1)]
	[Nullable(0)]
	public class BinaryReader : IDisposable
	{
		// Token: 0x060054A1 RID: 21665 RVA: 0x0019E73E File Offset: 0x0019D93E
		public BinaryReader(Stream input) : this(input, Encoding.UTF8, false)
		{
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0019E74D File Offset: 0x0019D94D
		public BinaryReader(Stream input, Encoding encoding) : this(input, encoding, false)
		{
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0019E758 File Offset: 0x0019D958
		public BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!input.CanRead)
			{
				throw new ArgumentException(SR.Argument_StreamNotReadable);
			}
			this._stream = input;
			this._decoder = encoding.GetDecoder();
			this._maxCharsSize = encoding.GetMaxCharCount(128);
			int num = encoding.GetMaxByteCount(1);
			if (num < 16)
			{
				num = 16;
			}
			this._buffer = new byte[num];
			this._2BytesPerChar = (encoding is UnicodeEncoding);
			this._isMemoryStream = (this._stream.GetType() == typeof(MemoryStream));
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0019E810 File Offset: 0x0019DA10
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x0019E818 File Offset: 0x0019DA18
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing && !this._leaveOpen)
				{
					this._stream.Close();
				}
				this._disposed = true;
			}
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0019E83F File Offset: 0x0019DA3F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0019E83F File Offset: 0x0019DA3F
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0019E848 File Offset: 0x0019DA48
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw Error.GetFileNotOpen();
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0019E858 File Offset: 0x0019DA58
		public virtual int PeekChar()
		{
			this.ThrowIfDisposed();
			if (!this._stream.CanSeek)
			{
				return -1;
			}
			long position = this._stream.Position;
			int result = this.Read();
			this._stream.Position = position;
			return result;
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0019E89C File Offset: 0x0019DA9C
		public unsafe virtual int Read()
		{
			this.ThrowIfDisposed();
			int num = 0;
			long num2 = 0L;
			if (this._stream.CanSeek)
			{
				num2 = this._stream.Position;
			}
			if (this._charBytes == null)
			{
				this._charBytes = new byte[128];
			}
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)2], 1);
			Span<char> chars = span;
			while (num == 0)
			{
				int num3 = this._2BytesPerChar ? 2 : 1;
				int num4 = this._stream.ReadByte();
				this._charBytes[0] = (byte)num4;
				if (num4 == -1)
				{
					num3 = 0;
				}
				if (num3 == 2)
				{
					num4 = this._stream.ReadByte();
					this._charBytes[1] = (byte)num4;
					if (num4 == -1)
					{
						num3 = 1;
					}
				}
				if (num3 == 0)
				{
					return -1;
				}
				try
				{
					num = this._decoder.GetChars(new ReadOnlySpan<byte>(this._charBytes, 0, num3), chars, false);
				}
				catch
				{
					if (this._stream.CanSeek)
					{
						this._stream.Seek(num2 - this._stream.Position, SeekOrigin.Current);
					}
					throw;
				}
			}
			return (int)(*chars[0]);
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0019E9B4 File Offset: 0x0019DBB4
		public virtual byte ReadByte()
		{
			return this.InternalReadByte();
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0019E9BC File Offset: 0x0019DBBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private byte InternalReadByte()
		{
			this.ThrowIfDisposed();
			int num = this._stream.ReadByte();
			if (num == -1)
			{
				throw Error.GetEndOfFile();
			}
			return (byte)num;
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0019E9E7 File Offset: 0x0019DBE7
		[CLSCompliant(false)]
		public virtual sbyte ReadSByte()
		{
			return (sbyte)this.InternalReadByte();
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0019E9F0 File Offset: 0x0019DBF0
		public virtual bool ReadBoolean()
		{
			return this.InternalReadByte() > 0;
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0019E9FC File Offset: 0x0019DBFC
		public virtual char ReadChar()
		{
			int num = this.Read();
			if (num == -1)
			{
				throw Error.GetEndOfFile();
			}
			return (char)num;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0019EA1C File Offset: 0x0019DC1C
		public virtual short ReadInt16()
		{
			return BinaryPrimitives.ReadInt16LittleEndian(this.InternalRead(2));
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0019EA2A File Offset: 0x0019DC2A
		[CLSCompliant(false)]
		public virtual ushort ReadUInt16()
		{
			return BinaryPrimitives.ReadUInt16LittleEndian(this.InternalRead(2));
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0019EA38 File Offset: 0x0019DC38
		public virtual int ReadInt32()
		{
			return BinaryPrimitives.ReadInt32LittleEndian(this.InternalRead(4));
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0019EA46 File Offset: 0x0019DC46
		[CLSCompliant(false)]
		public virtual uint ReadUInt32()
		{
			return BinaryPrimitives.ReadUInt32LittleEndian(this.InternalRead(4));
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0019EA54 File Offset: 0x0019DC54
		public virtual long ReadInt64()
		{
			return BinaryPrimitives.ReadInt64LittleEndian(this.InternalRead(8));
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0019EA62 File Offset: 0x0019DC62
		[CLSCompliant(false)]
		public virtual ulong ReadUInt64()
		{
			return BinaryPrimitives.ReadUInt64LittleEndian(this.InternalRead(8));
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0019EA70 File Offset: 0x0019DC70
		public virtual float ReadSingle()
		{
			return BitConverter.Int32BitsToSingle(BinaryPrimitives.ReadInt32LittleEndian(this.InternalRead(4)));
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0019EA83 File Offset: 0x0019DC83
		public virtual double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReadInt64LittleEndian(this.InternalRead(8)));
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0019EA98 File Offset: 0x0019DC98
		public virtual decimal ReadDecimal()
		{
			ReadOnlySpan<byte> span = this.InternalRead(16);
			decimal result;
			try
			{
				result = decimal.ToDecimal(span);
			}
			catch (ArgumentException innerException)
			{
				throw new IOException(SR.Arg_DecBitCtor, innerException);
			}
			return result;
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0019EAD8 File Offset: 0x0019DCD8
		public virtual string ReadString()
		{
			this.ThrowIfDisposed();
			int num = 0;
			int num2 = this.Read7BitEncodedInt();
			if (num2 < 0)
			{
				throw new IOException(SR.Format(SR.IO_InvalidStringLen_Len, num2));
			}
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (this._charBytes == null)
			{
				this._charBytes = new byte[128];
			}
			if (this._charBuffer == null)
			{
				this._charBuffer = new char[this._maxCharsSize];
			}
			StringBuilder stringBuilder = null;
			int chars;
			for (;;)
			{
				int count = (num2 - num > 128) ? 128 : (num2 - num);
				int num3 = this._stream.Read(this._charBytes, 0, count);
				if (num3 == 0)
				{
					break;
				}
				chars = this._decoder.GetChars(this._charBytes, 0, num3, this._charBuffer, 0);
				if (num == 0 && num3 == num2)
				{
					goto Block_8;
				}
				if (stringBuilder == null)
				{
					stringBuilder = StringBuilderCache.Acquire(Math.Min(num2, 360));
				}
				stringBuilder.Append(this._charBuffer, 0, chars);
				num += num3;
				if (num >= num2)
				{
					goto Block_10;
				}
			}
			throw Error.GetEndOfFile();
			Block_8:
			return new string(this._charBuffer, 0, chars);
			Block_10:
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0019EBE8 File Offset: 0x0019DDE8
		public virtual int Read(char[] buffer, int index, int count)
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
			this.ThrowIfDisposed();
			return this.InternalReadChars(new Span<char>(buffer, index, count));
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0019EC57 File Offset: 0x0019DE57
		[NullableContext(0)]
		public virtual int Read(Span<char> buffer)
		{
			this.ThrowIfDisposed();
			return this.InternalReadChars(buffer);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0019EC68 File Offset: 0x0019DE68
		private int InternalReadChars(Span<char> buffer)
		{
			int num = 0;
			while (!buffer.IsEmpty)
			{
				int num2 = buffer.Length;
				if (this._2BytesPerChar)
				{
					num2 <<= 1;
				}
				if (num2 > 1)
				{
					DecoderNLS decoderNLS = this._decoder as DecoderNLS;
					if (decoderNLS == null || decoderNLS.HasState)
					{
						num2--;
						if (this._2BytesPerChar && num2 > 2)
						{
							num2 -= 2;
						}
					}
				}
				ReadOnlySpan<byte> bytes;
				if (this._isMemoryStream)
				{
					MemoryStream memoryStream = (MemoryStream)this._stream;
					int start = memoryStream.InternalGetPosition();
					num2 = memoryStream.InternalEmulateRead(num2);
					bytes = new ReadOnlySpan<byte>(memoryStream.InternalGetBuffer(), start, num2);
				}
				else
				{
					if (this._charBytes == null)
					{
						this._charBytes = new byte[128];
					}
					if (num2 > 128)
					{
						num2 = 128;
					}
					num2 = this._stream.Read(this._charBytes, 0, num2);
					bytes = new ReadOnlySpan<byte>(this._charBytes, 0, num2);
				}
				if (bytes.IsEmpty)
				{
					break;
				}
				int chars = this._decoder.GetChars(bytes, buffer, false);
				buffer = buffer.Slice(chars);
				num += chars;
			}
			return num;
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0019ED78 File Offset: 0x0019DF78
		public virtual char[] ReadChars(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return Array.Empty<char>();
			}
			char[] array = new char[count];
			int num = this.InternalReadChars(new Span<char>(array));
			if (num != count)
			{
				char[] array2 = new char[num];
				Buffer.BlockCopy(array, 0, array2, 0, 2 * num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0019EDD8 File Offset: 0x0019DFD8
		public virtual int Read(byte[] buffer, int index, int count)
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
			this.ThrowIfDisposed();
			return this._stream.Read(buffer, index, count);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0019EE47 File Offset: 0x0019E047
		[NullableContext(0)]
		public virtual int Read(Span<byte> buffer)
		{
			this.ThrowIfDisposed();
			return this._stream.Read(buffer);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0019EE5C File Offset: 0x0019E05C
		public virtual byte[] ReadBytes(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[count];
			int num = 0;
			do
			{
				int num2 = this._stream.Read(array, num, count);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				count -= num2;
			}
			while (count > 0);
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0019EED0 File Offset: 0x0019E0D0
		private ReadOnlySpan<byte> InternalRead(int numBytes)
		{
			if (this._isMemoryStream)
			{
				return ((MemoryStream)this._stream).InternalReadSpan(numBytes);
			}
			this.ThrowIfDisposed();
			int num = 0;
			for (;;)
			{
				int num2 = this._stream.Read(this._buffer, num, numBytes - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num >= numBytes)
				{
					goto Block_3;
				}
			}
			throw Error.GetEndOfFile();
			Block_3:
			return this._buffer;
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0019EF34 File Offset: 0x0019E134
		protected virtual void FillBuffer(int numBytes)
		{
			if (numBytes < 0 || numBytes > this._buffer.Length)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.ArgumentOutOfRange_BinaryReaderFillBuffer);
			}
			int num = 0;
			this.ThrowIfDisposed();
			int num2;
			if (numBytes != 1)
			{
				for (;;)
				{
					num2 = this._stream.Read(this._buffer, num, numBytes - num);
					if (num2 == 0)
					{
						break;
					}
					num += num2;
					if (num >= numBytes)
					{
						return;
					}
				}
				throw Error.GetEndOfFile();
			}
			num2 = this._stream.ReadByte();
			if (num2 == -1)
			{
				throw Error.GetEndOfFile();
			}
			this._buffer[0] = (byte)num2;
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0019EFB8 File Offset: 0x0019E1B8
		public int Read7BitEncodedInt()
		{
			uint num = 0U;
			byte b;
			for (int i = 0; i < 28; i += 7)
			{
				b = this.ReadByte();
				num |= (uint)((uint)(b & 127) << i);
				if (b <= 127)
				{
					return (int)num;
				}
			}
			b = this.ReadByte();
			if (b > 15)
			{
				throw new FormatException(SR.Format_Bad7BitInt);
			}
			return (int)(num | (uint)((uint)b << 28));
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0019F010 File Offset: 0x0019E210
		public long Read7BitEncodedInt64()
		{
			ulong num = 0UL;
			byte b;
			for (int i = 0; i < 63; i += 7)
			{
				b = this.ReadByte();
				num |= ((ulong)b & 127UL) << i;
				if (b <= 127)
				{
					return (long)num;
				}
			}
			b = this.ReadByte();
			if (b > 1)
			{
				throw new FormatException(SR.Format_Bad7BitInt);
			}
			return (long)(num | (ulong)b << 63);
		}

		// Token: 0x040017B1 RID: 6065
		private readonly Stream _stream;

		// Token: 0x040017B2 RID: 6066
		private readonly byte[] _buffer;

		// Token: 0x040017B3 RID: 6067
		private readonly Decoder _decoder;

		// Token: 0x040017B4 RID: 6068
		private byte[] _charBytes;

		// Token: 0x040017B5 RID: 6069
		private char[] _charBuffer;

		// Token: 0x040017B6 RID: 6070
		private readonly int _maxCharsSize;

		// Token: 0x040017B7 RID: 6071
		private readonly bool _2BytesPerChar;

		// Token: 0x040017B8 RID: 6072
		private readonly bool _isMemoryStream;

		// Token: 0x040017B9 RID: 6073
		private readonly bool _leaveOpen;

		// Token: 0x040017BA RID: 6074
		private bool _disposed;
	}
}
