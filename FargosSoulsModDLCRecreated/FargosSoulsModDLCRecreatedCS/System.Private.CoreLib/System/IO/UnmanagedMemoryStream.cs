using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006BD RID: 1725
	public class UnmanagedMemoryStream : Stream
	{
		// Token: 0x060057F3 RID: 22515 RVA: 0x0019D904 File Offset: 0x0019CB04
		protected UnmanagedMemoryStream()
		{
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x001AE796 File Offset: 0x001AD996
		[NullableContext(1)]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read);
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x001AE7A8 File Offset: 0x001AD9A8
		[NullableContext(1)]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access);
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x001AE7BC File Offset: 0x001AD9BC
		[NullableContext(1)]
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException(SR.Argument_InvalidSafeBufferOffLen);
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CalledTwice);
			}
			byte* ptr = null;
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException(SR.ArgumentOutOfRange_UnmanagedMemStreamWrapAround);
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x001AE8A8 File Offset: 0x001ADAA8
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read);
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x001AE8BA File Offset: 0x001ADABA
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access);
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x001AE8D0 File Offset: 0x001ADAD0
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_LengthGreaterThanCapacity);
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_UnmanagedMemStreamWrapAround);
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", SR.ArgumentOutOfRange_Enum);
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CalledTwice);
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x001AE998 File Offset: 0x001ADB98
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060057FB RID: 22523 RVA: 0x001AE9AF File Offset: 0x001ADBAF
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060057FC RID: 22524 RVA: 0x001AE9B7 File Offset: 0x001ADBB7
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001AE9CE File Offset: 0x001ADBCE
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x001AE9E6 File Offset: 0x001ADBE6
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001AE9F6 File Offset: 0x001ADBF6
		private void EnsureReadable()
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x001A4E35 File Offset: 0x001A4035
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x001AEA06 File Offset: 0x001ADC06
		public override void Flush()
		{
			this.EnsureNotClosed();
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x001A4EFC File Offset: 0x001A40FC
		[NullableContext(1)]
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

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x001AEA0E File Offset: 0x001ADC0E
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return Interlocked.Read(ref this._length);
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x001AEA21 File Offset: 0x001ADC21
		public long Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x001AEA2F File Offset: 0x001ADC2F
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x001AEA4A File Offset: 0x001ADC4A
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				return Interlocked.Read(ref this._position);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				Interlocked.Exchange(ref this._position, value);
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x001AEA7C File Offset: 0x001ADC7C
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x001AEAD0 File Offset: 0x001ADCD0
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(SR.NotSupported_UmsSafeBuffer);
				}
				this.EnsureNotClosed();
				long num = Interlocked.Read(ref this._position);
				if (num > this._capacity)
				{
					throw new IndexOutOfRangeException(SR.IndexOutOfRange_UMSPosition);
				}
				return this._mem + num;
			}
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(SR.NotSupported_UmsSafeBuffer);
				}
				this.EnsureNotClosed();
				if (value < this._mem)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				long num = (long)(value - this._mem);
				if (num < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_UnmanagedMemStreamLength);
				}
				Interlocked.Exchange(ref this._position, num);
			}
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x001AEB38 File Offset: 0x001ADD38
		[NullableContext(1)]
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
			return this.ReadCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x001AEBA1 File Offset: 0x001ADDA1
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				return this.ReadCore(buffer);
			}
			return base.Read(buffer);
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x001AEBCC File Offset: 0x001ADDCC
		internal unsafe int ReadCore(Span<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = Math.Min(num2 - num, (long)buffer.Length);
			if (num3 <= 0L)
			{
				return 0;
			}
			int num4 = (int)num3;
			if (num4 < 0)
			{
				return 0;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* dest = reference;
				if (this._buffer != null)
				{
					byte* ptr = null;
					try
					{
						this._buffer.AcquirePointer(ref ptr);
						Buffer.Memcpy(dest, ptr + num + this._offset, num4);
						goto IL_A5;
					}
					finally
					{
						if (ptr != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(dest, this._mem + num, num4);
				IL_A5:;
			}
			Interlocked.Exchange(ref this._position, num + num3);
			return num4;
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x001AECA4 File Offset: 0x001ADEA4
		[NullableContext(1)]
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
			catch (Exception exception)
			{
				task = Task.FromException<int>(exception);
			}
			return task;
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x001AED60 File Offset: 0x001ADF60
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
			catch (Exception exception)
			{
				result = ValueTask.FromException<int>(exception);
			}
			return result;
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x001AEDDC File Offset: 0x001ADFDC
		public unsafe override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (num >= num2)
			{
				return -1;
			}
			Interlocked.Exchange(ref this._position, num + 1L);
			if (this._buffer != null)
			{
				byte* ptr = null;
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + num)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[num];
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001AEE78 File Offset: 0x001AE078
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				Interlocked.Exchange(ref this._position, offset);
				break;
			case SeekOrigin.Current:
			{
				long num = Interlocked.Read(ref this._position);
				if (offset + num < 0L)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				Interlocked.Exchange(ref this._position, offset + num);
				break;
			}
			case SeekOrigin.End:
			{
				long num2 = Interlocked.Read(ref this._length);
				if (num2 + offset < 0L)
				{
					throw new IOException(SR.IO_SeekBeforeBegin);
				}
				Interlocked.Exchange(ref this._position, num2 + offset);
				break;
			}
			default:
				throw new ArgumentException(SR.Argument_InvalidSeekOrigin);
			}
			return Interlocked.Read(ref this._position);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x001AEF34 File Offset: 0x001AE134
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException(SR.NotSupported_UmsSafeBuffer);
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (value > this._capacity)
			{
				throw new IOException(SR.IO_FixedCapacity);
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (value > num2)
			{
				Buffer.ZeroMemory(this._mem + num2, (UIntPtr)(value - num2));
			}
			Interlocked.Exchange(ref this._length, value);
			if (num > value)
			{
				Interlocked.Exchange(ref this._position, value);
			}
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x001AEFD8 File Offset: 0x001AE1D8
		[NullableContext(1)]
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
			this.WriteCore(new ReadOnlySpan<byte>(buffer, offset, count));
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x001AF041 File Offset: 0x001AE241
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				this.WriteCore(buffer);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001AF06C File Offset: 0x001AE26C
		internal unsafe void WriteCore(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + (long)buffer.Length;
			if (num3 < 0L)
			{
				throw new IOException(SR.IO_StreamTooLong);
			}
			if (num3 > this._capacity)
			{
				throw new NotSupportedException(SR.IO_FixedCapacity);
			}
			if (this._buffer == null)
			{
				if (num > num2)
				{
					Buffer.ZeroMemory(this._mem + num2, (UIntPtr)(num - num2));
				}
				if (num3 > num2)
				{
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* src = reference;
				if (this._buffer != null)
				{
					long num4 = this._capacity - num;
					if (num4 < (long)buffer.Length)
					{
						throw new ArgumentException(SR.Arg_BufferTooSmall);
					}
					byte* ptr = null;
					try
					{
						this._buffer.AcquirePointer(ref ptr);
						Buffer.Memcpy(ptr + num + this._offset, src, buffer.Length);
						goto IL_10C;
					}
					finally
					{
						if (ptr != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(this._mem + num, src, buffer.Length);
				IL_10C:;
			}
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x001AF1A8 File Offset: 0x001AE3A8
		[NullableContext(1)]
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
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x001AF244 File Offset: 0x001AE444
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
			catch (Exception exception)
			{
				valueTask = ValueTask.FromException(exception);
			}
			return valueTask;
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x001AF2C0 File Offset: 0x001AE4C0
		public unsafe override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + 1L;
			if (num >= num2)
			{
				if (num3 < 0L)
				{
					throw new IOException(SR.IO_StreamTooLong);
				}
				if (num3 > this._capacity)
				{
					throw new NotSupportedException(SR.IO_FixedCapacity);
				}
				if (this._buffer == null)
				{
					if (num > num2)
					{
						Buffer.ZeroMemory(this._mem + num2, (UIntPtr)(num - num2));
					}
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + num)[this._offset] = value;
					goto IL_C0;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[num] = value;
			IL_C0:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x04001933 RID: 6451
		private SafeBuffer _buffer;

		// Token: 0x04001934 RID: 6452
		private unsafe byte* _mem;

		// Token: 0x04001935 RID: 6453
		private long _length;

		// Token: 0x04001936 RID: 6454
		private long _capacity;

		// Token: 0x04001937 RID: 6455
		private long _position;

		// Token: 0x04001938 RID: 6456
		private long _offset;

		// Token: 0x04001939 RID: 6457
		private FileAccess _access;

		// Token: 0x0400193A RID: 6458
		private bool _isOpen;

		// Token: 0x0400193B RID: 6459
		private Task<int> _lastReadTask;
	}
}
