using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text
{
	// Token: 0x02000380 RID: 896
	internal sealed class TranscodingStream : Stream
	{
		// Token: 0x06002F63 RID: 12131 RVA: 0x00160368 File Offset: 0x0015F568
		internal TranscodingStream(Stream innerStream, Encoding innerEncoding, Encoding thisEncoding, bool leaveOpen)
		{
			this._innerStream = innerStream;
			this._leaveOpen = leaveOpen;
			this._innerEncoding = innerEncoding;
			this._thisEncoding = thisEncoding;
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x0016038D File Offset: 0x0015F58D
		public override bool CanRead
		{
			get
			{
				Stream innerStream = this._innerStream;
				return innerStream != null && innerStream.CanRead;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x001603A0 File Offset: 0x0015F5A0
		public override bool CanWrite
		{
			get
			{
				Stream innerStream = this._innerStream;
				return innerStream != null && innerStream.CanWrite;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x001603B3 File Offset: 0x0015F5B3
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_UnseekableStream);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x001603B3 File Offset: 0x0015F5B3
		// (set) Token: 0x06002F69 RID: 12137 RVA: 0x001603B3 File Offset: 0x0015F5B3
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_UnseekableStream);
			}
			set
			{
				throw new NotSupportedException(SR.NotSupported_UnseekableStream);
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x001603BF File Offset: 0x0015F5BF
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x001603D8 File Offset: 0x0015F5D8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x001603F4 File Offset: 0x0015F5F4
		protected override void Dispose(bool disposing)
		{
			if (this._innerStream == null)
			{
				return;
			}
			ArraySegment<byte> segment = this.FinalFlushWriteBuffers();
			if (segment.Count != 0)
			{
				this._innerStream.Write(segment);
			}
			Stream innerStream = this._innerStream;
			this._innerStream = null;
			if (!this._leaveOpen)
			{
				innerStream.Dispose();
			}
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x00160448 File Offset: 0x0015F648
		public override ValueTask DisposeAsync()
		{
			if (this._innerStream == null)
			{
				return default(ValueTask);
			}
			ArraySegment<byte> pendingData = this.FinalFlushWriteBuffers();
			if (pendingData.Count != 0)
			{
				return this.<DisposeAsync>g__DisposeAsyncCore|30_0(pendingData);
			}
			Stream innerStream = this._innerStream;
			this._innerStream = null;
			if (!this._leaveOpen)
			{
				return innerStream.DisposeAsync();
			}
			return default(ValueTask);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x001604A5 File Offset: 0x0015F6A5
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x001604AD File Offset: 0x0015F6AD
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x001604B5 File Offset: 0x0015F6B5
		[MemberNotNull(new string[]
		{
			"_innerDecoder",
			"_thisEncoder",
			"_readBuffer"
		})]
		private void EnsurePreReadConditions()
		{
			this.ThrowIfDisposed();
			if (this._innerDecoder == null)
			{
				this.<EnsurePreReadConditions>g__InitializeReadDataStructures|33_0();
			}
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x001604CB File Offset: 0x0015F6CB
		[MemberNotNull(new string[]
		{
			"_thisDecoder",
			"_innerEncoder"
		})]
		private void EnsurePreWriteConditions()
		{
			this.ThrowIfDisposed();
			if (this._innerEncoder == null)
			{
				this.<EnsurePreWriteConditions>g__InitializeReadDataStructures|34_0();
			}
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x001604E4 File Offset: 0x0015F6E4
		private ArraySegment<byte> FinalFlushWriteBuffers()
		{
			if (this._thisDecoder == null || this._innerEncoder == null)
			{
				return default(ArraySegment<byte>);
			}
			char[] chars = Array.Empty<char>();
			int num = this._thisDecoder.GetCharCount(Array.Empty<byte>(), 0, 0, true);
			if (num > 0)
			{
				chars = new char[num];
				num = this._thisDecoder.GetChars(Array.Empty<byte>(), 0, 0, chars, 0, true);
			}
			byte[] array = Array.Empty<byte>();
			int num2 = this._innerEncoder.GetByteCount(chars, 0, num, true);
			if (num2 > 0)
			{
				array = new byte[num2];
				num2 = this._innerEncoder.GetBytes(chars, 0, num, array, 0, true);
			}
			return new ArraySegment<byte>(array, 0, num2);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x00160582 File Offset: 0x0015F782
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._innerStream.Flush();
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00160595 File Offset: 0x0015F795
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x001605A9 File Offset: 0x0015F7A9
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Read(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x001605C8 File Offset: 0x0015F7C8
		public override int Read(Span<byte> buffer)
		{
			this.EnsurePreReadConditions();
			if (this._readBufferCount == 0)
			{
				byte[] array = ArrayPool<byte>.Shared.Rent(4096);
				char[] array2 = ArrayPool<char>.Shared.Rent(this._readCharBufferMaxSize);
				try
				{
					int num = this._innerStream.Read(array, 0, 4096);
					bool flush = num == 0;
					int chars = this._innerDecoder.GetChars(array, 0, num, array2, 0, flush);
					int bytes = this._thisEncoder.GetBytes(array2, 0, chars, this._readBuffer, 0, flush);
					this._readBufferOffset = 0;
					this._readBufferCount = bytes;
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(array, false);
					ArrayPool<char>.Shared.Return(array2, false);
				}
			}
			int num2 = Math.Min(this._readBufferCount, buffer.Length);
			this._readBuffer.AsSpan(this._readBufferOffset, num2).CopyTo(buffer);
			this._readBufferOffset += num2;
			this._readBufferCount -= num2;
			return num2;
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x001606D4 File Offset: 0x0015F8D4
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.ReadAsync(new Memory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x00160707 File Offset: 0x0015F907
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			this.EnsurePreReadConditions();
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			return this.<ReadAsync>g__ReadAsyncCore|41_0(buffer, cancellationToken);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x00160728 File Offset: 0x0015F928
		public unsafe override int ReadByte()
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)1], 1);
			Span<byte> buffer = span;
			int num = this.Read(buffer);
			if (num != 0)
			{
				return (int)(*buffer[0]);
			}
			return -1;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x001603B3 File Offset: 0x0015F5B3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.NotSupported_UnseekableStream);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x001603B3 File Offset: 0x0015F5B3
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.NotSupported_UnseekableStream);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x00160758 File Offset: 0x0015F958
		[StackTraceHidden]
		private void ThrowIfDisposed()
		{
			if (this._innerStream == null)
			{
				this.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x00160768 File Offset: 0x0015F968
		[DoesNotReturn]
		[StackTraceHidden]
		private void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(base.GetType().Name, SR.ObjectDisposed_StreamClosed);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0016077F File Offset: 0x0015F97F
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Write(new ReadOnlySpan<byte>(buffer, offset, count));
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x001607A0 File Offset: 0x0015F9A0
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.EnsurePreWriteConditions();
			int minimumLength = Math.Clamp(buffer.Length, 4096, 1048576);
			char[] array = ArrayPool<char>.Shared.Rent(minimumLength);
			byte[] array2 = ArrayPool<byte>.Shared.Rent(minimumLength);
			try
			{
				bool flag;
				do
				{
					int num;
					int value;
					this._thisDecoder.Convert(buffer, array, false, out num, out value, out flag);
					ReadOnlySpan<byte> readOnlySpan = buffer;
					int length = readOnlySpan.Length;
					int num2 = num;
					int num3 = length - num2;
					buffer = readOnlySpan.Slice(num2, num3);
					Span<char> span = array.AsSpan(Range.EndAt(value));
					bool flag2;
					do
					{
						int num4;
						int count;
						this._innerEncoder.Convert(span, array2, false, out num4, out count, out flag2);
						Span<char> span2 = span;
						int length2 = span2.Length;
						num3 = num4;
						num2 = length2 - num3;
						span = span2.Slice(num3, num2);
						this._innerStream.Write(array2, 0, count);
					}
					while (!flag2);
				}
				while (!flag);
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
				ArrayPool<byte>.Shared.Return(array2, false);
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x001608B4 File Offset: 0x0015FAB4
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x001608E7 File Offset: 0x0015FAE7
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			this.EnsurePreWriteConditions();
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled(cancellationToken);
			}
			return this.<WriteAsync>g__WriteAsyncCore|50_0(buffer, cancellationToken);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x00160907 File Offset: 0x0015FB07
		public override void WriteByte(byte value)
		{
			this.Write(MemoryMarshal.CreateReadOnlySpan<byte>(ref value, 1));
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x00160918 File Offset: 0x0015FB18
		[CompilerGenerated]
		private ValueTask <DisposeAsync>g__DisposeAsyncCore|30_0(ArraySegment<byte> pendingData)
		{
			TranscodingStream.<<DisposeAsync>g__DisposeAsyncCore|30_0>d <<DisposeAsync>g__DisposeAsyncCore|30_0>d;
			<<DisposeAsync>g__DisposeAsyncCore|30_0>d.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<<DisposeAsync>g__DisposeAsyncCore|30_0>d.<>4__this = this;
			<<DisposeAsync>g__DisposeAsyncCore|30_0>d.pendingData = pendingData;
			<<DisposeAsync>g__DisposeAsyncCore|30_0>d.<>1__state = -1;
			<<DisposeAsync>g__DisposeAsyncCore|30_0>d.<>t__builder.Start<TranscodingStream.<<DisposeAsync>g__DisposeAsyncCore|30_0>d>(ref <<DisposeAsync>g__DisposeAsyncCore|30_0>d);
			return <<DisposeAsync>g__DisposeAsyncCore|30_0>d.<>t__builder.Task;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x00160964 File Offset: 0x0015FB64
		[CompilerGenerated]
		private void <EnsurePreReadConditions>g__InitializeReadDataStructures|33_0()
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			this._innerDecoder = this._innerEncoding.GetDecoder();
			this._thisEncoder = this._thisEncoding.GetEncoder();
			this._readCharBufferMaxSize = this._innerEncoding.GetMaxCharCount(4096);
			this._readBuffer = GC.AllocateUninitializedArray<byte>(this._thisEncoding.GetMaxByteCount(this._readCharBufferMaxSize), false);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x001609D4 File Offset: 0x0015FBD4
		[CompilerGenerated]
		private void <EnsurePreWriteConditions>g__InitializeReadDataStructures|34_0()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			this._innerEncoder = this._innerEncoding.GetEncoder();
			this._thisDecoder = this._thisEncoding.GetDecoder();
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x00160A08 File Offset: 0x0015FC08
		[CompilerGenerated]
		private ValueTask<int> <ReadAsync>g__ReadAsyncCore|41_0(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			TranscodingStream.<<ReadAsync>g__ReadAsyncCore|41_0>d <<ReadAsync>g__ReadAsyncCore|41_0>d;
			<<ReadAsync>g__ReadAsyncCore|41_0>d.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<<ReadAsync>g__ReadAsyncCore|41_0>d.<>4__this = this;
			<<ReadAsync>g__ReadAsyncCore|41_0>d.buffer = buffer;
			<<ReadAsync>g__ReadAsyncCore|41_0>d.cancellationToken = cancellationToken;
			<<ReadAsync>g__ReadAsyncCore|41_0>d.<>1__state = -1;
			<<ReadAsync>g__ReadAsyncCore|41_0>d.<>t__builder.Start<TranscodingStream.<<ReadAsync>g__ReadAsyncCore|41_0>d>(ref <<ReadAsync>g__ReadAsyncCore|41_0>d);
			return <<ReadAsync>g__ReadAsyncCore|41_0>d.<>t__builder.Task;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00160A5C File Offset: 0x0015FC5C
		[CompilerGenerated]
		private ValueTask <WriteAsync>g__WriteAsyncCore|50_0(ReadOnlyMemory<byte> remainingOuterEncodedBytes, CancellationToken cancellationToken)
		{
			TranscodingStream.<<WriteAsync>g__WriteAsyncCore|50_0>d <<WriteAsync>g__WriteAsyncCore|50_0>d;
			<<WriteAsync>g__WriteAsyncCore|50_0>d.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<<WriteAsync>g__WriteAsyncCore|50_0>d.<>4__this = this;
			<<WriteAsync>g__WriteAsyncCore|50_0>d.remainingOuterEncodedBytes = remainingOuterEncodedBytes;
			<<WriteAsync>g__WriteAsyncCore|50_0>d.cancellationToken = cancellationToken;
			<<WriteAsync>g__WriteAsyncCore|50_0>d.<>1__state = -1;
			<<WriteAsync>g__WriteAsyncCore|50_0>d.<>t__builder.Start<TranscodingStream.<<WriteAsync>g__WriteAsyncCore|50_0>d>(ref <<WriteAsync>g__WriteAsyncCore|50_0>d);
			return <<WriteAsync>g__WriteAsyncCore|50_0>d.<>t__builder.Task;
		}

		// Token: 0x04000CF9 RID: 3321
		private readonly Encoding _innerEncoding;

		// Token: 0x04000CFA RID: 3322
		private readonly Encoding _thisEncoding;

		// Token: 0x04000CFB RID: 3323
		private Stream _innerStream;

		// Token: 0x04000CFC RID: 3324
		private readonly bool _leaveOpen;

		// Token: 0x04000CFD RID: 3325
		private Encoder _innerEncoder;

		// Token: 0x04000CFE RID: 3326
		private Decoder _thisDecoder;

		// Token: 0x04000CFF RID: 3327
		private Encoder _thisEncoder;

		// Token: 0x04000D00 RID: 3328
		private Decoder _innerDecoder;

		// Token: 0x04000D01 RID: 3329
		private int _readCharBufferMaxSize;

		// Token: 0x04000D02 RID: 3330
		private byte[] _readBuffer;

		// Token: 0x04000D03 RID: 3331
		private int _readBufferOffset;

		// Token: 0x04000D04 RID: 3332
		private int _readBufferCount;
	}
}
