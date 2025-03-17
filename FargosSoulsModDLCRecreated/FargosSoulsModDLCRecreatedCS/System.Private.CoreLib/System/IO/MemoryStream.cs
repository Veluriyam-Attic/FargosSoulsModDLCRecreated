using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200069B RID: 1691
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryStream : Stream
	{
		// Token: 0x060055C7 RID: 21959 RVA: 0x001A4C94 File Offset: 0x001A3E94
		public MemoryStream() : this(0)
		{
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x001A4CA0 File Offset: 0x001A3EA0
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_NegativeCapacity);
			}
			this._buffer = ((capacity != 0) ? new byte[capacity] : Array.Empty<byte>());
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._isOpen = true;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x001A4D00 File Offset: 0x001A3F00
		public MemoryStream(byte[] buffer) : this(buffer, true)
		{
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x001A4D0C File Offset: 0x001A3F0C
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._isOpen = true;
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001A4D59 File Offset: 0x001A3F59
		public MemoryStream(byte[] buffer, int index, int count) : this(buffer, index, count, true, false)
		{
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x001A4D66 File Offset: 0x001A3F66
		public MemoryStream(byte[] buffer, int index, int count, bool writable) : this(buffer, index, count, writable, false)
		{
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x001A4D74 File Offset: 0x001A3F74
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
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
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._isOpen = true;
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x001A4E15 File Offset: 0x001A4015
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x001A4E15 File Offset: 0x001A4015
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x001A4E1D File Offset: 0x001A401D
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x001A4E25 File Offset: 0x001A4025
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x001A4E35 File Offset: 0x001A4035
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x001A4E48 File Offset: 0x001A4048
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
					this._lastReadTask = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x001A4E90 File Offset: 0x001A4090
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException(SR.IO_StreamTooLong);
			}
			if (value > this._capacity)
			{
				int num = Math.Max(value, 256);
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				if (this._capacity * 2 > 2147483591)
				{
					num = Math.Max(value, 2147483591);
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x000AB30B File Offset: 0x000AA50B
		public override void Flush()
		{
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x001A4EFC File Offset: 0x001A40FC
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Flush();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x001A4F44 File Offset: 0x001A4144
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException(SR.UnauthorizedAccess_MemStreamBuffer);
			}
			return this._buffer;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x001A4F5F File Offset: 0x001A415F
		[NullableContext(0)]
		public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			if (!this._exposable)
			{
				buffer = default(ArraySegment<byte>);
				return false;
			}
			buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
			return true;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x001A4F97 File Offset: 0x001A4197
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x001A4F9F File Offset: 0x001A419F
		internal int InternalGetPosition()
		{
			return this._position;
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x001A4FA8 File Offset: 0x001A41A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan<byte> InternalReadSpan(int count)
		{
			this.EnsureNotClosed();
			int position = this._position;
			int num = position + count;
			if (num > this._length)
			{
				this._position = this._length;
				throw Error.GetEndOfFile();
			}
			ReadOnlySpan<byte> result = new ReadOnlySpan<byte>(this._buffer, position, count);
			this._position = num;
			return result;
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001A4FF8 File Offset: 0x001A41F8
		internal int InternalEmulateRead(int count)
		{
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x001A5034 File Offset: 0x001A4234
		// (set) Token: 0x060055DE RID: 21982 RVA: 0x001A504C File Offset: 0x001A424C
		public virtual int Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity - this._origin;
			}
			set
			{
				if ((long)value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
				}
				this.EnsureNotClosed();
				if (!this._expandable && value != this.Capacity)
				{
					throw new NotSupportedException(SR.NotSupported_MemStreamNotExpandable);
				}
				if (this._expandable && value != this._capacity)
				{
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.BlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = Array.Empty<byte>();
					}
					this._capacity = value;
				}
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x001A50E9 File Offset: 0x001A42E9
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._length - this._origin);
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060055E0 RID: 21984 RVA: 0x001A50FF File Offset: 0x001A42FF
		// (set) Token: 0x060055E1 RID: 21985 RVA: 0x001A5118 File Offset: 0x001A4318
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._position - this._origin);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				this.EnsureNotClosed();
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_StreamLength);
				}
				this._position = this._origin + (int)value;
			}
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x001A5168 File Offset: 0x001A4368
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x001A522C File Offset: 0x001A442C
		[NullableContext(0)]
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.Read(buffer);
			}
			this.EnsureNotClosed();
			int num = Math.Min(this._length - this._position, buffer.Length);
			if (num <= 0)
			{
				return 0;
			}
			new Span<byte>(this._buffer, this._position, num).CopyTo(buffer);
			this._position += num;
			return num;
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x001A52A8 File Offset: 0x001A44A8
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (OperationCanceledException exception)
			{
				task = Task.FromCanceled<int>(exception);
			}
			catch (Exception exception2)
			{
				task = Task.FromException<int>(exception2);
			}
			return task;
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x001A537C File Offset: 0x001A457C
		[NullableContext(0)]
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			ValueTask<int> result;
			try
			{
				ArraySegment<byte> arraySegment;
				result = new ValueTask<int>(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count) : this.Read(buffer.Span));
			}
			catch (OperationCanceledException exception)
			{
				result = new ValueTask<int>(Task.FromCanceled<int>(exception));
			}
			catch (Exception exception2)
			{
				result = ValueTask.FromException<int>(exception2);
			}
			return result;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x001A5414 File Offset: 0x001A4614
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			if (this._position >= this._length)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			return buffer[position];
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x001A5450 File Offset: 0x001A4650
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				base.CopyTo(destination, bufferSize);
				return;
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - position);
			if (num > 0)
			{
				destination.Write(this._buffer, position, num);
			}
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x001A54B0 File Offset: 0x001A46B0
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - this._position);
			if (num == 0)
			{
				return Task.CompletedTask;
			}
			MemoryStream memoryStream = destination as MemoryStream;
			if (memoryStream == null)
			{
				return destination.WriteAsync(this._buffer, position, num, cancellationToken);
			}
			Task result;
			try
			{
				memoryStream.Write(this._buffer, position, num);
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x001A5568 File Offset: 0x001A4768
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_StreamLength);
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
			{
				int num = this._origin + (int)offset;
				if (offset < 0L || num < this._origin)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				this._position = num;
				break;
			}
			case SeekOrigin.Current:
			{
				int num2 = this._position + (int)offset;
				if ((long)this._position + offset < (long)this._origin || num2 < this._origin)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				this._position = num2;
				break;
			}
			case SeekOrigin.End:
			{
				int num3 = this._length + (int)offset;
				if ((long)this._length + offset < (long)this._origin || num3 < this._origin)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				this._position = num3;
				break;
			}
			default:
				throw new ArgumentException(SR.Argument_InvalidSeekOrigin);
			}
			return (long)this._position;
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x001A565C File Offset: 0x001A485C
		public override void SetLength(long value)
		{
			if (value < 0L || value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_StreamLength);
			}
			this.EnsureWriteable();
			if (value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_StreamLength);
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x001A56FC File Offset: 0x001A48FC
		public virtual byte[] ToArray()
		{
			int num = this._length - this._origin;
			if (num == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._buffer, this._origin, array, 0, num);
			return array;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x001A573C File Offset: 0x001A493C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException(SR.IO_StreamTooLong);
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity)
				{
					bool flag2 = this.EnsureCapacity(num);
					if (flag2)
					{
						flag = false;
					}
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8 && buffer != this._buffer)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x001A5858 File Offset: 0x001A4A58
		[NullableContext(0)]
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				base.Write(buffer);
				return;
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + buffer.Length;
			if (num < 0)
			{
				throw new IOException(SR.IO_StreamTooLong);
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity)
				{
					bool flag2 = this.EnsureCapacity(num);
					if (flag2)
					{
						flag = false;
					}
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			buffer.CopyTo(new Span<byte>(this._buffer, this._position, buffer.Length));
			this._position = num;
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x001A5928 File Offset: 0x001A4B28
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Write(buffer, offset, count);
				result = Task.CompletedTask;
			}
			catch (OperationCanceledException exception)
			{
				result = Task.FromCanceled(exception);
			}
			catch (Exception exception2)
			{
				result = Task.FromException(exception2);
			}
			return result;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x001A59D8 File Offset: 0x001A4BD8
		[NullableContext(0)]
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled(cancellationToken);
			}
			ValueTask valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					this.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				else
				{
					this.Write(buffer.Span);
				}
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (OperationCanceledException exception)
			{
				valueTask = new ValueTask(Task.FromCanceled(exception));
			}
			catch (Exception exception2)
			{
				valueTask = ValueTask.FromException(exception2);
			}
			return valueTask;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x001A5A6C File Offset: 0x001A4C6C
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity)
				{
					bool flag2 = this.EnsureCapacity(num);
					if (flag2)
					{
						flag = false;
					}
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001A5B02 File Offset: 0x001A4D02
		public virtual void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", SR.ArgumentNull_Stream);
			}
			this.EnsureNotClosed();
			stream.Write(this._buffer, this._origin, this._length - this._origin);
		}

		// Token: 0x04001863 RID: 6243
		private byte[] _buffer;

		// Token: 0x04001864 RID: 6244
		private readonly int _origin;

		// Token: 0x04001865 RID: 6245
		private int _position;

		// Token: 0x04001866 RID: 6246
		private int _length;

		// Token: 0x04001867 RID: 6247
		private int _capacity;

		// Token: 0x04001868 RID: 6248
		private bool _expandable;

		// Token: 0x04001869 RID: 6249
		private bool _writable;

		// Token: 0x0400186A RID: 6250
		private readonly bool _exposable;

		// Token: 0x0400186B RID: 6251
		private bool _isOpen;

		// Token: 0x0400186C RID: 6252
		private Task<int> _lastReadTask;
	}
}
