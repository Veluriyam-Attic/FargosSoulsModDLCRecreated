using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006BE RID: 1726
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x06005817 RID: 22551 RVA: 0x001AF3AC File Offset: 0x001AE5AC
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005818 RID: 22552 RVA: 0x001AF3BB File Offset: 0x001AE5BB
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x001AF3C8 File Offset: 0x001AE5C8
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x001AF3D5 File Offset: 0x001AE5D5
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x001AF3E4 File Offset: 0x001AE5E4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x001AF41C File Offset: 0x001AE61C
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x001AF429 File Offset: 0x001AE629
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException(SR.UnauthorizedAccess_MemStreamBuffer);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x001AF435 File Offset: 0x001AE635
		public override bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			buffer = default(ArraySegment<byte>);
			return false;
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600581F RID: 22559 RVA: 0x001AF43F File Offset: 0x001AE63F
		// (set) Token: 0x06005820 RID: 22560 RVA: 0x001AF44D File Offset: 0x001AE64D
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException(SR.IO_FixedCapacity);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x001AF459 File Offset: 0x001AE659
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x001AF466 File Offset: 0x001AE666
		// (set) Token: 0x06005823 RID: 22563 RVA: 0x001AF473 File Offset: 0x001AE673
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x001AF481 File Offset: 0x001AE681
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x001AF491 File Offset: 0x001AE691
		public override int Read(Span<byte> buffer)
		{
			return this._unmanagedStream.Read(buffer);
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x001AF49F File Offset: 0x001AE69F
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x001AF4AC File Offset: 0x001AE6AC
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x001AF4BC File Offset: 0x001AE6BC
		public override byte[] ToArray()
		{
			byte[] array = new byte[this._unmanagedStream.Length];
			this._unmanagedStream.Read(array, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x001AF4F6 File Offset: 0x001AE6F6
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x001AF506 File Offset: 0x001AE706
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._unmanagedStream.Write(buffer);
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x001AF514 File Offset: 0x001AE714
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x001AF524 File Offset: 0x001AE724
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", SR.ArgumentNull_Stream);
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x001AF556 File Offset: 0x001AE756
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x001AF560 File Offset: 0x001AE760
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_StreamClosed);
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", SR.ObjectDisposed_StreamClosed);
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.NotSupported_UnreadableStream);
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(SR.NotSupported_UnwritableStream);
			}
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x001AF5FF File Offset: 0x001AE7FF
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x001AF60D File Offset: 0x001AE80D
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x001AF61F File Offset: 0x001AE81F
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x001AF62E File Offset: 0x001AE82E
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x001AF640 File Offset: 0x001AE840
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x0400193C RID: 6460
		private readonly UnmanagedMemoryStream _unmanagedStream;
	}
}
