using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200067E RID: 1662
	[NullableContext(1)]
	[Nullable(0)]
	public class BinaryWriter : IDisposable, IAsyncDisposable
	{
		// Token: 0x060054C5 RID: 21701 RVA: 0x0019F068 File Offset: 0x0019E268
		protected BinaryWriter()
		{
			this.OutStream = Stream.Null;
			this._buffer = new byte[16];
			this._encoding = EncodingCache.UTF8NoBOM;
			this._encoder = this._encoding.GetEncoder();
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0019F0A4 File Offset: 0x0019E2A4
		public BinaryWriter(Stream output) : this(output, EncodingCache.UTF8NoBOM, false)
		{
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0019F0B3 File Offset: 0x0019E2B3
		public BinaryWriter(Stream output, Encoding encoding) : this(output, encoding, false)
		{
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0019F0C0 File Offset: 0x0019E2C0
		public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(SR.Argument_StreamNotWritable);
			}
			this.OutStream = output;
			this._buffer = new byte[16];
			this._encoding = encoding;
			this._encoder = this._encoding.GetEncoder();
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0019F135 File Offset: 0x0019E335
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0019F13E File Offset: 0x0019E33E
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._leaveOpen)
				{
					this.OutStream.Flush();
					return;
				}
				this.OutStream.Close();
			}
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0019F135 File Offset: 0x0019E335
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0019F164 File Offset: 0x0019E364
		public virtual ValueTask DisposeAsync()
		{
			ValueTask result;
			try
			{
				if (base.GetType() == typeof(BinaryWriter))
				{
					if (this._leaveOpen)
					{
						return new ValueTask(this.OutStream.FlushAsync());
					}
					this.OutStream.Close();
				}
				else
				{
					this.Dispose();
				}
				result = default(ValueTask);
			}
			catch (Exception exception)
			{
				result = ValueTask.FromException(exception);
			}
			return result;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060054CD RID: 21709 RVA: 0x0019F1E0 File Offset: 0x0019E3E0
		public virtual Stream BaseStream
		{
			get
			{
				this.Flush();
				return this.OutStream;
			}
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0019F1EE File Offset: 0x0019E3EE
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0019F1FB File Offset: 0x0019E3FB
		public virtual long Seek(int offset, SeekOrigin origin)
		{
			return this.OutStream.Seek((long)offset, origin);
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0019F20B File Offset: 0x0019E40B
		public virtual void Write(bool value)
		{
			this.OutStream.WriteByte(value ? 1 : 0);
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0019F220 File Offset: 0x0019E420
		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0019F22E File Offset: 0x0019E42E
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			this.OutStream.WriteByte((byte)value);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0019F23D File Offset: 0x0019E43D
		public virtual void Write(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0019F25D File Offset: 0x0019E45D
		public virtual void Write(byte[] buffer, int index, int count)
		{
			this.OutStream.Write(buffer, index, count);
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0019F270 File Offset: 0x0019E470
		public unsafe virtual void Write(char ch)
		{
			if (char.IsSurrogate(ch))
			{
				throw new ArgumentException(SR.Arg_SurrogatesNotAllowedAsSingleChar);
			}
			int bytes2;
			fixed (byte* ptr = &this._buffer[0])
			{
				byte* bytes = ptr;
				bytes2 = this._encoder.GetBytes(&ch, 1, bytes, this._buffer.Length, true);
			}
			this.OutStream.Write(this._buffer, 0, bytes2);
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x0019F2D4 File Offset: 0x0019E4D4
		public virtual void Write(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0019F310 File Offset: 0x0019E510
		public virtual void Write(char[] chars, int index, int count)
		{
			byte[] bytes = this._encoding.GetBytes(chars, index, count);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0019F33C File Offset: 0x0019E53C
		public virtual void Write(double value)
		{
			BinaryPrimitives.WriteDoubleLittleEndian(this._buffer, value);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x0019F362 File Offset: 0x0019E562
		public virtual void Write(decimal value)
		{
			decimal.GetBytes(value, this._buffer);
			this.OutStream.Write(this._buffer, 0, 16);
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0019F385 File Offset: 0x0019E585
		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0019F385 File Offset: 0x0019E585
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0019F3B0 File Offset: 0x0019E5B0
		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0019F400 File Offset: 0x0019E600
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0019F450 File Offset: 0x0019E650
		public virtual void Write(long value)
		{
			BinaryPrimitives.WriteInt64LittleEndian(this._buffer, value);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x0019F476 File Offset: 0x0019E676
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			BinaryPrimitives.WriteUInt64LittleEndian(this._buffer, value);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0019F49C File Offset: 0x0019E69C
		public unsafe virtual void Write(float value)
		{
			uint num = *(uint*)(&value);
			this._buffer[0] = (byte)num;
			this._buffer[1] = (byte)(num >> 8);
			this._buffer[2] = (byte)(num >> 16);
			this._buffer[3] = (byte)(num >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0019F4F4 File Offset: 0x0019E6F4
		public virtual void Write(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int byteCount = this._encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this._largeByteBuffer == null)
			{
				this._largeByteBuffer = new byte[256];
				this._maxChars = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
			}
			if (byteCount <= this._largeByteBuffer.Length)
			{
				this._encoding.GetBytes(value, this._largeByteBuffer);
				this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
				return;
			}
			int i = value.Length;
			int num = 0;
			ReadOnlySpan<char> readOnlySpan = value;
			if (this._encoding.GetType() == typeof(UTF8Encoding))
			{
				while (i > 0)
				{
					int num2;
					int count;
					bool flag;
					this._encoder.Convert(readOnlySpan.Slice(num), this._largeByteBuffer, i <= this._maxChars, out num2, out count, out flag);
					this.OutStream.Write(this._largeByteBuffer, 0, count);
					num += num2;
					i -= num2;
				}
				return;
			}
			this.WriteWhenEncodingIsNotUtf8(value, byteCount);
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0019F61C File Offset: 0x0019E81C
		private unsafe void WriteWhenEncodingIsNotUtf8(string value, int len)
		{
			int i = value.Length;
			int num = 0;
			while (i > 0)
			{
				int num2 = (i > this._maxChars) ? this._maxChars : i;
				checked
				{
					if (num < 0 || num2 < 0 || num > value.Length - num2)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					char* ptr;
					if (value == null)
					{
						ptr = null;
					}
					else
					{
						fixed (char* ptr2 = value.GetPinnableReference())
						{
							ptr = ptr2;
						}
					}
					char* ptr3 = ptr;
					int bytes2;
					fixed (byte* ptr4 = &this._largeByteBuffer[0])
					{
						byte* bytes = ptr4;
						bytes2 = this._encoder.GetBytes(ptr3 + num, num2, bytes, this._largeByteBuffer.Length, num2 == i);
					}
					char* ptr2 = null;
					this.OutStream.Write(this._largeByteBuffer, 0, bytes2);
				}
				num += num2;
				i -= num2;
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0019F6DC File Offset: 0x0019E8DC
		[NullableContext(0)]
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() == typeof(BinaryWriter))
			{
				this.OutStream.Write(buffer);
				return;
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0019F75C File Offset: 0x0019E95C
		[NullableContext(0)]
		public virtual void Write(ReadOnlySpan<char> chars)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(this._encoding.GetMaxByteCount(chars.Length));
			try
			{
				int bytes = this._encoding.GetBytes(chars, array);
				this.Write(array, 0, bytes);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0019F7C4 File Offset: 0x0019E9C4
		public void Write7BitEncodedInt(int value)
		{
			uint num;
			for (num = (uint)value; num > 127U; num >>= 7)
			{
				this.Write((byte)(num | 4294967168U));
			}
			this.Write((byte)num);
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0019F7F4 File Offset: 0x0019E9F4
		public void Write7BitEncodedInt64(long value)
		{
			ulong num;
			for (num = (ulong)value; num > 127UL; num >>= 7)
			{
				this.Write((byte)((uint)num | 4294967168U));
			}
			this.Write((byte)num);
		}

		// Token: 0x040017BB RID: 6075
		public static readonly BinaryWriter Null = new BinaryWriter();

		// Token: 0x040017BC RID: 6076
		protected Stream OutStream;

		// Token: 0x040017BD RID: 6077
		private readonly byte[] _buffer;

		// Token: 0x040017BE RID: 6078
		private readonly Encoding _encoding;

		// Token: 0x040017BF RID: 6079
		private readonly Encoder _encoder;

		// Token: 0x040017C0 RID: 6080
		private readonly bool _leaveOpen;

		// Token: 0x040017C1 RID: 6081
		private byte[] _largeByteBuffer;

		// Token: 0x040017C2 RID: 6082
		private int _maxChars;
	}
}
