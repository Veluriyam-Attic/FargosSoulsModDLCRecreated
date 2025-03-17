using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200067F RID: 1663
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BufferedStream : Stream
	{
		// Token: 0x060054E8 RID: 21736 RVA: 0x0019F82F File Offset: 0x0019EA2F
		internal SemaphoreSlim LazyEnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0019F85B File Offset: 0x0019EA5B
		public BufferedStream(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x0019F86C File Offset: 0x0019EA6C
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.Format(SR.ArgumentOutOfRange_MustBePositive, "bufferSize"));
			}
			this._stream = stream;
			this._bufferSize = bufferSize;
			if (!this._stream.CanRead && !this._stream.CanWrite)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_StreamClosed);
			}
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x0019F8DF File Offset: 0x0019EADF
		private void EnsureNotClosed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_StreamClosed);
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0019F8F5 File Offset: 0x0019EAF5
		private void EnsureCanSeek()
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(SR.NotSupported_UnseekableStream);
			}
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0019F90F File Offset: 0x0019EB0F
		private void EnsureCanRead()
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException(SR.NotSupported_UnreadableStream);
			}
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0019F929 File Offset: 0x0019EB29
		private void EnsureCanWrite()
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException(SR.NotSupported_UnwritableStream);
			}
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0019F944 File Offset: 0x0019EB44
		private void EnsureShadowBufferAllocated()
		{
			if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
			{
				return;
			}
			byte[] array = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._writePos);
			this._buffer = array;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0019F9A7 File Offset: 0x0019EBA7
		private void EnsureBufferAllocated()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x0019F9C2 File Offset: 0x0019EBC2
		public Stream UnderlyingStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x0019F9CA File Offset: 0x0019EBCA
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x0019F9D2 File Offset: 0x0019EBD2
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._stream.CanRead;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0019F9E9 File Offset: 0x0019EBE9
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._stream.CanWrite;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x0019FA00 File Offset: 0x0019EC00
		public override bool CanSeek
		{
			get
			{
				return this._stream != null && this._stream.CanSeek;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060054F6 RID: 21750 RVA: 0x0019FA17 File Offset: 0x0019EC17
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._stream.Length;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x0019FA39 File Offset: 0x0019EC39
		// (set) Token: 0x060054F8 RID: 21752 RVA: 0x0019FA68 File Offset: 0x0019EC68
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				return this._stream.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._stream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0019FAC4 File Offset: 0x0019ECC4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0019FB24 File Offset: 0x0019ED24
		public override ValueTask DisposeAsync()
		{
			BufferedStream.<DisposeAsync>d__35 <DisposeAsync>d__;
			<DisposeAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<DisposeAsync>d__.<>4__this = this;
			<DisposeAsync>d__.<>1__state = -1;
			<DisposeAsync>d__.<>t__builder.Start<BufferedStream.<DisposeAsync>d__35>(ref <DisposeAsync>d__);
			return <DisposeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0019FB68 File Offset: 0x0019ED68
		public override void Flush()
		{
			this.EnsureNotClosed();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return;
			}
			if (this._readPos < this._readLen)
			{
				if (this._stream.CanSeek)
				{
					this.FlushRead();
				}
				if (this._stream.CanWrite)
				{
					this._stream.Flush();
				}
				return;
			}
			if (this._stream.CanWrite)
			{
				this._stream.Flush();
			}
			this._writePos = (this._readPos = (this._readLen = 0));
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0019FBF6 File Offset: 0x0019EDF6
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			return this.FlushAsyncInternal(cancellationToken);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0019FC18 File Offset: 0x0019EE18
		private Task FlushAsyncInternal(CancellationToken cancellationToken)
		{
			BufferedStream.<FlushAsyncInternal>d__38 <FlushAsyncInternal>d__;
			<FlushAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsyncInternal>d__.<>4__this = this;
			<FlushAsyncInternal>d__.cancellationToken = cancellationToken;
			<FlushAsyncInternal>d__.<>1__state = -1;
			<FlushAsyncInternal>d__.<>t__builder.Start<BufferedStream.<FlushAsyncInternal>d__38>(ref <FlushAsyncInternal>d__);
			return <FlushAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0019FC63 File Offset: 0x0019EE63
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._stream.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0019FCA0 File Offset: 0x0019EEA0
		private void ClearReadBufferBeforeWrite()
		{
			if (this._readPos == this._readLen)
			{
				this._readPos = (this._readLen = 0);
				return;
			}
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(SR.NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed);
			}
			this.FlushRead();
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0019FCEA File Offset: 0x0019EEEA
		private void FlushWrite()
		{
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._stream.Flush();
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0019FD18 File Offset: 0x0019EF18
		private ValueTask FlushWriteAsync(CancellationToken cancellationToken)
		{
			BufferedStream.<FlushWriteAsync>d__42 <FlushWriteAsync>d__;
			<FlushWriteAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<FlushWriteAsync>d__.<>4__this = this;
			<FlushWriteAsync>d__.cancellationToken = cancellationToken;
			<FlushWriteAsync>d__.<>1__state = -1;
			<FlushWriteAsync>d__.<>t__builder.Start<BufferedStream.<FlushWriteAsync>d__42>(ref <FlushWriteAsync>d__);
			return <FlushWriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0019FD64 File Offset: 0x0019EF64
		private int ReadFromBuffer(byte[] array, int offset, int count)
		{
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				return 0;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.BlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			return num;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0019FDB0 File Offset: 0x0019EFB0
		private int ReadFromBuffer(Span<byte> destination)
		{
			int num = Math.Min(this._readLen - this._readPos, destination.Length);
			if (num > 0)
			{
				new ReadOnlySpan<byte>(this._buffer, this._readPos, num).CopyTo(destination);
				this._readPos += num;
			}
			return num;
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0019FE08 File Offset: 0x0019F008
		private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
		{
			int result;
			try
			{
				error = null;
				result = this.ReadFromBuffer(array, offset, count);
			}
			catch (Exception ex)
			{
				error = ex;
				result = 0;
			}
			return result;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0019FE40 File Offset: 0x0019F040
		public override int Read(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(array, offset, count);
			if (num == count)
			{
				return num;
			}
			int num2 = num;
			if (num > 0)
			{
				count -= num;
				offset += num;
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (count >= this._bufferSize)
			{
				return this._stream.Read(array, offset, count) + num2;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			num = this.ReadFromBuffer(array, offset, count);
			return num + num2;
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0019FF34 File Offset: 0x0019F134
		[NullableContext(0)]
		public override int Read(Span<byte> destination)
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(destination);
			if (num == destination.Length)
			{
				return num;
			}
			if (num > 0)
			{
				destination = destination.Slice(num);
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (destination.Length >= this._bufferSize)
			{
				return this._stream.Read(destination) + num;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			return this.ReadFromBuffer(destination) + num;
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0019FFDC File Offset: 0x0019F1DC
		private Task<int> LastSyncCompletedReadTask(int val)
		{
			Task<int> task = this._lastSyncCompletedReadTask;
			if (task != null && task.Result == val)
			{
				return task;
			}
			task = Task.FromResult<int>(val);
			this._lastSyncCompletedReadTask = task;
			return task;
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x001A0010 File Offset: 0x0019F210
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
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync(cancellationToken);
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = (num == count || ex != null);
					if (flag)
					{
						return (ex == null) ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(new Memory<byte>(buffer, offset + num, count - num), cancellationToken, num, task).AsTask();
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x001A0118 File Offset: 0x0019F318
		[NullableContext(0)]
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync(cancellationToken);
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					num = this.ReadFromBuffer(buffer.Span);
					flag = (num == buffer.Length);
					if (flag)
					{
						return new ValueTask<int>(num);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(buffer.Slice(num), cancellationToken, num, task);
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x001A01B4 File Offset: 0x0019F3B4
		private ValueTask<int> ReadFromUnderlyingStreamAsync(Memory<byte> buffer, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask)
		{
			BufferedStream.<ReadFromUnderlyingStreamAsync>d__51 <ReadFromUnderlyingStreamAsync>d__;
			<ReadFromUnderlyingStreamAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadFromUnderlyingStreamAsync>d__.<>4__this = this;
			<ReadFromUnderlyingStreamAsync>d__.buffer = buffer;
			<ReadFromUnderlyingStreamAsync>d__.cancellationToken = cancellationToken;
			<ReadFromUnderlyingStreamAsync>d__.bytesAlreadySatisfied = bytesAlreadySatisfied;
			<ReadFromUnderlyingStreamAsync>d__.semaphoreLockTask = semaphoreLockTask;
			<ReadFromUnderlyingStreamAsync>d__.<>1__state = -1;
			<ReadFromUnderlyingStreamAsync>d__.<>t__builder.Start<BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref <ReadFromUnderlyingStreamAsync>d__);
			return <ReadFromUnderlyingStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x001603BF File Offset: 0x0015F5BF
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x001604A5 File Offset: 0x0015F6A5
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x001A0218 File Offset: 0x0019F418
		public override int ReadByte()
		{
			if (this._readPos == this._readLen)
			{
				return this.ReadByteSlow();
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x001A0254 File Offset: 0x0019F454
		private int ReadByteSlow()
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			this._readPos = 0;
			if (this._readLen == 0)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x001A02CC File Offset: 0x0019F4CC
		private void WriteToBuffer(byte[] array, ref int offset, ref int count)
		{
			int num = Math.Min(this._bufferSize - this._writePos, count);
			if (num <= 0)
			{
				return;
			}
			this.EnsureBufferAllocated();
			Buffer.BlockCopy(array, offset, this._buffer, this._writePos, num);
			this._writePos += num;
			count -= num;
			offset += num;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x001A0328 File Offset: 0x0019F528
		private int WriteToBuffer(ReadOnlySpan<byte> buffer)
		{
			int num = Math.Min(this._bufferSize - this._writePos, buffer.Length);
			if (num > 0)
			{
				this.EnsureBufferAllocated();
				buffer.Slice(0, num).CopyTo(new Span<byte>(this._buffer, this._writePos, num));
				this._writePos += num;
			}
			return num;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x001A038C File Offset: 0x0019F58C
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.ArgumentNull_Buffer);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num;
			bool flag;
			checked
			{
				num = this._writePos + count;
				flag = (num + count < this._bufferSize + this._bufferSize);
			}
			if (!flag)
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						Buffer.BlockCopy(array, offset, this._buffer, this._writePos, count);
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(array, offset, count);
				return;
			}
			this.WriteToBuffer(array, ref offset, ref count);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this.WriteToBuffer(array, ref offset, ref count);
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x001A04E8 File Offset: 0x0019F6E8
		[NullableContext(0)]
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num;
			bool flag;
			checked
			{
				num = this._writePos + buffer.Length;
				flag = (num + buffer.Length < this._bufferSize + this._bufferSize);
			}
			if (!flag)
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						buffer.CopyTo(new Span<byte>(this._buffer, this._writePos, buffer.Length));
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(buffer);
				return;
			}
			int start = this.WriteToBuffer(buffer);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			buffer = buffer.Slice(start);
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			start = this.WriteToBuffer(buffer);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x001A0610 File Offset: 0x0019F810
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
			return this.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001A0684 File Offset: 0x0019F884
		[NullableContext(0)]
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync(cancellationToken);
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = (buffer.Length < this._bufferSize - this._writePos);
					if (flag)
					{
						int num = this.WriteToBuffer(buffer.Span);
						return default(ValueTask);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.WriteToUnderlyingStreamAsync(buffer, cancellationToken, task);
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x001A0734 File Offset: 0x0019F934
		private ValueTask WriteToUnderlyingStreamAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken, Task semaphoreLockTask)
		{
			BufferedStream.<WriteToUnderlyingStreamAsync>d__62 <WriteToUnderlyingStreamAsync>d__;
			<WriteToUnderlyingStreamAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<WriteToUnderlyingStreamAsync>d__.<>4__this = this;
			<WriteToUnderlyingStreamAsync>d__.buffer = buffer;
			<WriteToUnderlyingStreamAsync>d__.cancellationToken = cancellationToken;
			<WriteToUnderlyingStreamAsync>d__.semaphoreLockTask = semaphoreLockTask;
			<WriteToUnderlyingStreamAsync>d__.<>1__state = -1;
			<WriteToUnderlyingStreamAsync>d__.<>t__builder.Start<BufferedStream.<WriteToUnderlyingStreamAsync>d__62>(ref <WriteToUnderlyingStreamAsync>d__);
			return <WriteToUnderlyingStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x001603D8 File Offset: 0x0015F5D8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001604AD File Offset: 0x0015F6AD
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x001A0790 File Offset: 0x0019F990
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			if (this._writePos == 0)
			{
				this.EnsureCanWrite();
				this.ClearReadBufferBeforeWrite();
				this.EnsureBufferAllocated();
			}
			if (this._writePos >= this._bufferSize - 1)
			{
				this.FlushWrite();
			}
			byte[] buffer = this._buffer;
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x001A07EC File Offset: 0x0019F9EC
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return this._stream.Seek(offset, origin);
			}
			if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long position = this.Position;
			long num = this._stream.Seek(offset, origin);
			this._readPos = (int)(num - (position - (long)this._readPos));
			if (0 <= this._readPos && this._readPos < this._readLen)
			{
				this._stream.Seek((long)(this._readLen - this._readPos), SeekOrigin.Current);
			}
			else
			{
				this._readPos = (this._readLen = 0);
			}
			return num;
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x001A08B4 File Offset: 0x0019FAB4
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			this.EnsureCanWrite();
			this.Flush();
			this._stream.SetLength(value);
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x001A08F0 File Offset: 0x0019FAF0
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			int num = this._readLen - this._readPos;
			if (num > 0)
			{
				destination.Write(this._buffer, this._readPos, num);
				this._readPos = (this._readLen = 0);
			}
			else if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this._stream.CopyTo(destination, bufferSize);
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x001A0958 File Offset: 0x0019FB58
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.CopyToAsyncCore(destination, bufferSize, cancellationToken);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x001A097C File Offset: 0x0019FB7C
		private Task CopyToAsyncCore(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			BufferedStream.<CopyToAsyncCore>d__70 <CopyToAsyncCore>d__;
			<CopyToAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CopyToAsyncCore>d__.<>4__this = this;
			<CopyToAsyncCore>d__.destination = destination;
			<CopyToAsyncCore>d__.bufferSize = bufferSize;
			<CopyToAsyncCore>d__.cancellationToken = cancellationToken;
			<CopyToAsyncCore>d__.<>1__state = -1;
			<CopyToAsyncCore>d__.<>t__builder.Start<BufferedStream.<CopyToAsyncCore>d__70>(ref <CopyToAsyncCore>d__);
			return <CopyToAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x040017C3 RID: 6083
		private Stream _stream;

		// Token: 0x040017C4 RID: 6084
		private byte[] _buffer;

		// Token: 0x040017C5 RID: 6085
		private readonly int _bufferSize;

		// Token: 0x040017C6 RID: 6086
		private int _readPos;

		// Token: 0x040017C7 RID: 6087
		private int _readLen;

		// Token: 0x040017C8 RID: 6088
		private int _writePos;

		// Token: 0x040017C9 RID: 6089
		private Task<int> _lastSyncCompletedReadTask;

		// Token: 0x040017CA RID: 6090
		private SemaphoreSlim _asyncActiveSemaphore;
	}
}
