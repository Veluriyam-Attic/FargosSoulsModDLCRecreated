using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006A8 RID: 1704
	[NullableContext(1)]
	[Nullable(0)]
	public class StreamWriter : TextWriter
	{
		// Token: 0x06005693 RID: 22163 RVA: 0x001A9CD6 File Offset: 0x001A8ED6
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncWriteTask.IsCompleted)
			{
				StreamWriter.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x001A7E88 File Offset: 0x001A7088
		[DoesNotReturn]
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException(SR.InvalidOperation_AsyncIOInProgress);
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06005695 RID: 22165 RVA: 0x001A9CEA File Offset: 0x001A8EEA
		private static Encoding UTF8NoBOM
		{
			get
			{
				return EncodingCache.UTF8NoBOM;
			}
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x001A9CF1 File Offset: 0x001A8EF1
		public StreamWriter(Stream stream) : this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x001A9D05 File Offset: 0x001A8F05
		public StreamWriter(Stream stream, Encoding encoding) : this(stream, encoding, 1024, false)
		{
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x001A9D15 File Offset: 0x001A8F15
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize) : this(stream, encoding, bufferSize, false)
		{
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x001A9D24 File Offset: 0x001A8F24
		public StreamWriter(Stream stream, [Nullable(2)] Encoding encoding = null, int bufferSize = -1, bool leaveOpen = false) : base(null)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				encoding = StreamWriter.UTF8NoBOM;
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(SR.Argument_StreamNotWritable);
			}
			if (bufferSize == -1)
			{
				bufferSize = 1024;
			}
			else if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.ArgumentOutOfRange_NeedPosNum);
			}
			this._stream = stream;
			this._encoding = encoding;
			this._encoder = this._encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._charBuffer = new char[bufferSize];
			this._byteBuffer = new byte[this._encoding.GetMaxByteCount(bufferSize)];
			this._charLen = bufferSize;
			if (this._stream.CanSeek && this._stream.Position > 0L)
			{
				this._haveWrittenPreamble = true;
			}
			this._closable = !leaveOpen;
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x001A9E15 File Offset: 0x001A9015
		public StreamWriter(string path) : this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x001A9E29 File Offset: 0x001A9029
		public StreamWriter(string path, bool append) : this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x001A9E3D File Offset: 0x001A903D
		public StreamWriter(string path, bool append, Encoding encoding) : this(path, append, encoding, 1024)
		{
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x001A9E4D File Offset: 0x001A904D
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize) : this(StreamWriter.ValidateArgsAndOpenPath(path, append, encoding, bufferSize), encoding, bufferSize, false)
		{
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x001A9E64 File Offset: 0x001A9064
		private static Stream ValidateArgsAndOpenPath(string path, bool append, Encoding encoding, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyPath);
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.ArgumentOutOfRange_NeedPosNum);
			}
			return new FileStream(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan);
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x001A9ECD File Offset: 0x001A90CD
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x001A9EDC File Offset: 0x001A90DC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed && disposing)
				{
					this.CheckAsyncTaskInProgress();
					this.Flush(true, true);
				}
			}
			finally
			{
				this.CloseStreamFromDispose(disposing);
			}
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x001A9F20 File Offset: 0x001A9120
		private void CloseStreamFromDispose(bool disposing)
		{
			if (this._closable && !this._disposed)
			{
				try
				{
					if (disposing)
					{
						this._stream.Close();
					}
				}
				finally
				{
					this._disposed = true;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x001A9F74 File Offset: 0x001A9174
		public override ValueTask DisposeAsync()
		{
			if (!(base.GetType() != typeof(StreamWriter)))
			{
				return this.DisposeAsyncCore();
			}
			return base.DisposeAsync();
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x001A9F9C File Offset: 0x001A919C
		private ValueTask DisposeAsyncCore()
		{
			StreamWriter.<DisposeAsyncCore>d__33 <DisposeAsyncCore>d__;
			<DisposeAsyncCore>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<DisposeAsyncCore>d__.<>4__this = this;
			<DisposeAsyncCore>d__.<>1__state = -1;
			<DisposeAsyncCore>d__.<>t__builder.Start<StreamWriter.<DisposeAsyncCore>d__33>(ref <DisposeAsyncCore>d__);
			return <DisposeAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x001A9FDF File Offset: 0x001A91DF
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x001A9FF0 File Offset: 0x001A91F0
		private void Flush(bool flushStream, bool flushEncoder)
		{
			this.ThrowIfDisposed();
			if (this._charPos == 0 && !flushStream && !flushEncoder)
			{
				return;
			}
			if (!this._haveWrittenPreamble)
			{
				this._haveWrittenPreamble = true;
				ReadOnlySpan<byte> preamble = this._encoding.Preamble;
				if (preamble.Length > 0)
				{
					this._stream.Write(preamble);
				}
			}
			int bytes = this._encoder.GetBytes(this._charBuffer, 0, this._charPos, this._byteBuffer, 0, flushEncoder);
			this._charPos = 0;
			if (bytes > 0)
			{
				this._stream.Write(this._byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x001AA090 File Offset: 0x001A9290
		// (set) Token: 0x060056A7 RID: 22183 RVA: 0x001AA098 File Offset: 0x001A9298
		public virtual bool AutoFlush
		{
			get
			{
				return this._autoFlush;
			}
			set
			{
				this.CheckAsyncTaskInProgress();
				this._autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x001AA0B2 File Offset: 0x001A92B2
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060056A9 RID: 22185 RVA: 0x001AA0BA File Offset: 0x001A92BA
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x001AA0C4 File Offset: 0x001A92C4
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen)
			{
				this.Flush(false, false);
			}
			this._charBuffer[this._charPos] = value;
			this._charPos++;
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001AA119 File Offset: 0x001A9319
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer)
		{
			this.WriteSpan(buffer, false);
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x001AA128 File Offset: 0x001A9328
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer, int index, int count)
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
			this.WriteSpan(buffer.AsSpan(index, count), false);
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001AA197 File Offset: 0x001A9397
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(ReadOnlySpan<char> buffer)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteSpan(buffer, false);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x001AA1C0 File Offset: 0x001A93C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteSpan(ReadOnlySpan<char> buffer, bool appendNewLine)
		{
			this.CheckAsyncTaskInProgress();
			if (buffer.Length <= 4 && buffer.Length <= this._charLen - this._charPos)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					char[] charBuffer = this._charBuffer;
					int charPos = this._charPos;
					this._charPos = charPos + 1;
					charBuffer[charPos] = *buffer[i];
				}
			}
			else
			{
				this.ThrowIfDisposed();
				char[] charBuffer2 = this._charBuffer;
				fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
				{
					char* ptr = reference;
					fixed (char* ptr2 = &charBuffer2[0])
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						int j = buffer.Length;
						int num = this._charPos;
						while (j > 0)
						{
							if (num == charBuffer2.Length)
							{
								this.Flush(false, false);
								num = 0;
							}
							int num2 = Math.Min(charBuffer2.Length - num, j);
							int num3 = num2 * 2;
							Buffer.MemoryCopy((void*)ptr4, (void*)(ptr3 + num), (long)num3, (long)num3);
							this._charPos += num2;
							num += num2;
							ptr4 += num2;
							j -= num2;
						}
					}
				}
			}
			if (appendNewLine)
			{
				char[] coreNewLine = this.CoreNewLine;
				for (int k = 0; k < coreNewLine.Length; k++)
				{
					if (this._charPos == this._charLen)
					{
						this.Flush(false, false);
					}
					this._charBuffer[this._charPos] = coreNewLine[k];
					this._charPos++;
				}
			}
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001AA33C File Offset: 0x001A953C
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(string value)
		{
			this.WriteSpan(value, false);
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x001AA34B File Offset: 0x001A954B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(string value)
		{
			this.CheckAsyncTaskInProgress();
			this.WriteSpan(value, true);
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x001AA360 File Offset: 0x001A9560
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(ReadOnlySpan<char> value)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.CheckAsyncTaskInProgress();
				this.WriteSpan(value, true);
				return;
			}
			base.WriteLine(value);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x001AA390 File Offset: 0x001A9590
		private void WriteFormatHelper(string format, ParamsArray args, bool appendNewLine)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(((format != null) ? format.Length : 0) + args.Length * 8).AppendFormatHelper(null, format, args);
			StringBuilder.ChunkEnumerator chunks = stringBuilder.GetChunks();
			bool flag = chunks.MoveNext();
			while (flag)
			{
				ReadOnlyMemory<char> readOnlyMemory = chunks.Current;
				ReadOnlySpan<char> span = readOnlyMemory.Span;
				flag = chunks.MoveNext();
				this.WriteSpan(span, !flag && appendNewLine);
			}
			StringBuilderCache.Release(stringBuilder);
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x001AA402 File Offset: 0x001A9602
		public override void Write(string format, [Nullable(2)] object arg0)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0), false);
				return;
			}
			base.Write(format, arg0);
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x001AA432 File Offset: 0x001A9632
		[NullableContext(2)]
		public override void Write([Nullable(1)] string format, object arg0, object arg1)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0, arg1), false);
				return;
			}
			base.Write(format, arg0, arg1);
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x001AA464 File Offset: 0x001A9664
		[NullableContext(2)]
		public override void Write([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0, arg1, arg2), false);
				return;
			}
			base.Write(format, arg0, arg1, arg2);
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x001AA49C File Offset: 0x001A969C
		public override void Write(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			if (!(base.GetType() == typeof(StreamWriter)))
			{
				base.Write(format, arg);
				return;
			}
			if (arg == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "arg");
			}
			this.WriteFormatHelper(format, new ParamsArray(arg), false);
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x001AA4EF File Offset: 0x001A96EF
		public override void WriteLine(string format, [Nullable(2)] object arg0)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0), true);
				return;
			}
			base.WriteLine(format, arg0);
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x001AA51F File Offset: 0x001A971F
		[NullableContext(2)]
		public override void WriteLine([Nullable(1)] string format, object arg0, object arg1)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0, arg1), true);
				return;
			}
			base.WriteLine(format, arg0, arg1);
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x001AA551 File Offset: 0x001A9751
		[NullableContext(2)]
		public override void WriteLine([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteFormatHelper(format, new ParamsArray(arg0, arg1, arg2), true);
				return;
			}
			base.WriteLine(format, arg0, arg1, arg2);
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x001AA587 File Offset: 0x001A9787
		public override void WriteLine(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			if (!(base.GetType() == typeof(StreamWriter)))
			{
				base.WriteLine(format, arg);
				return;
			}
			if (arg == null)
			{
				throw new ArgumentNullException("arg");
			}
			this.WriteFormatHelper(format, new ParamsArray(arg), true);
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x001AA5C8 File Offset: 0x001A97C8
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x001AA630 File Offset: 0x001A9830
		private static Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			StreamWriter.<WriteAsyncInternal>d__61 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.value = value;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__61>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x001AA6B0 File Offset: 0x001A98B0
		public override Task WriteAsync([Nullable(2)] string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value != null)
			{
				this.ThrowIfDisposed();
				this.CheckAsyncTaskInProgress();
				Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
				this._asyncWriteTask = task;
				return task;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x001AA720 File Offset: 0x001A9920
		private static Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			StreamWriter.<WriteAsyncInternal>d__63 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.value = value;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__63>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x001AA7A0 File Offset: 0x001A99A0
		public override Task WriteAsync(char[] buffer, int index, int count)
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
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x001AA868 File Offset: 0x001A9A68
		[NullableContext(0)]
		[return: Nullable(1)]
		public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x001AA8E4 File Offset: 0x001A9AE4
		private static Task WriteAsyncInternal(StreamWriter _this, ReadOnlyMemory<char> source, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine, CancellationToken cancellationToken)
		{
			StreamWriter.<WriteAsyncInternal>d__66 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.source = source;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.cancellationToken = cancellationToken;
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__66>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x001AA96C File Offset: 0x001A9B6C
		public override Task WriteLineAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync();
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, ReadOnlyMemory<char>.Empty, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x001AA9E0 File Offset: 0x001A9BE0
		public override Task WriteLineAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C4 RID: 22212 RVA: 0x001AAA48 File Offset: 0x001A9C48
		public override Task WriteLineAsync([Nullable(2)] string value)
		{
			if (value == null)
			{
				return this.WriteLineAsync();
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x001AAABC File Offset: 0x001A9CBC
		public override Task WriteLineAsync(char[] buffer, int index, int count)
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
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, index, count);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x001AAB84 File Offset: 0x001A9D84
		[NullableContext(0)]
		[return: Nullable(1)]
		public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, cancellationToken);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x001AAC00 File Offset: 0x001A9E00
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this._charBuffer, this._charPos, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x001AAC60 File Offset: 0x001A9E60
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task result = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this._haveWrittenPreamble, this._encoding, this._encoder, this._byteBuffer, this._stream, cancellationToken);
			this._charPos = 0;
			return result;
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x001AACC4 File Offset: 0x001A9EC4
		private static Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream, CancellationToken cancellationToken)
		{
			StreamWriter.<FlushAsyncInternal>d__74 <FlushAsyncInternal>d__;
			<FlushAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsyncInternal>d__._this = _this;
			<FlushAsyncInternal>d__.flushStream = flushStream;
			<FlushAsyncInternal>d__.flushEncoder = flushEncoder;
			<FlushAsyncInternal>d__.charBuffer = charBuffer;
			<FlushAsyncInternal>d__.charPos = charPos;
			<FlushAsyncInternal>d__.haveWrittenPreamble = haveWrittenPreamble;
			<FlushAsyncInternal>d__.encoding = encoding;
			<FlushAsyncInternal>d__.encoder = encoder;
			<FlushAsyncInternal>d__.byteBuffer = byteBuffer;
			<FlushAsyncInternal>d__.stream = stream;
			<FlushAsyncInternal>d__.cancellationToken = cancellationToken;
			<FlushAsyncInternal>d__.<>1__state = -1;
			<FlushAsyncInternal>d__.<>t__builder.Start<StreamWriter.<FlushAsyncInternal>d__74>(ref <FlushAsyncInternal>d__);
			return <FlushAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x001AAD5E File Offset: 0x001A9F5E
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				this.<ThrowIfDisposed>g__ThrowObjectDisposedException|75_0();
			}
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x001AAD8A File Offset: 0x001A9F8A
		[CompilerGenerated]
		private void <ThrowIfDisposed>g__ThrowObjectDisposedException|75_0()
		{
			throw new ObjectDisposedException(base.GetType().Name, SR.ObjectDisposed_WriterClosed);
		}

		// Token: 0x040018AC RID: 6316
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, StreamWriter.UTF8NoBOM, 128, true);

		// Token: 0x040018AD RID: 6317
		private readonly Stream _stream;

		// Token: 0x040018AE RID: 6318
		private readonly Encoding _encoding;

		// Token: 0x040018AF RID: 6319
		private readonly Encoder _encoder;

		// Token: 0x040018B0 RID: 6320
		private readonly byte[] _byteBuffer;

		// Token: 0x040018B1 RID: 6321
		private readonly char[] _charBuffer;

		// Token: 0x040018B2 RID: 6322
		private int _charPos;

		// Token: 0x040018B3 RID: 6323
		private int _charLen;

		// Token: 0x040018B4 RID: 6324
		private bool _autoFlush;

		// Token: 0x040018B5 RID: 6325
		private bool _haveWrittenPreamble;

		// Token: 0x040018B6 RID: 6326
		private readonly bool _closable;

		// Token: 0x040018B7 RID: 6327
		private bool _disposed;

		// Token: 0x040018B8 RID: 6328
		private Task _asyncWriteTask = Task.CompletedTask;
	}
}
