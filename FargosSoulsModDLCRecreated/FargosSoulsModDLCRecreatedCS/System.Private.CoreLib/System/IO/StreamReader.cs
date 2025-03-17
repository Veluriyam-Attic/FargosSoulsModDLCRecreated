using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006A2 RID: 1698
	[NullableContext(1)]
	[Nullable(0)]
	public class StreamReader : TextReader
	{
		// Token: 0x06005652 RID: 22098 RVA: 0x001A7E74 File Offset: 0x001A7074
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncReadTask.IsCompleted)
			{
				StreamReader.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x001A7E88 File Offset: 0x001A7088
		[DoesNotReturn]
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException(SR.InvalidOperation_AsyncIOInProgress);
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x001A7E94 File Offset: 0x001A7094
		private StreamReader()
		{
			this._stream = Stream.Null;
			this._closable = true;
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x001A7EB9 File Offset: 0x001A70B9
		public StreamReader(Stream stream) : this(stream, true)
		{
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x001A7EC3 File Offset: 0x001A70C3
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x001A7ED8 File Offset: 0x001A70D8
		public StreamReader(Stream stream, Encoding encoding) : this(stream, encoding, true, 1024, false)
		{
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x001A7EE9 File Offset: 0x001A70E9
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(stream, encoding, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x001A7EFA File Offset: 0x001A70FA
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x001A7F08 File Offset: 0x001A7108
		public StreamReader(Stream stream, [Nullable(2)] Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(SR.Argument_StreamNotReadable);
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
			this._decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this._charBuffer = new char[this._maxCharsPerBuffer];
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._checkPreamble = (encoding.Preamble.Length > 0);
			this._closable = !leaveOpen;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x001A7FF3 File Offset: 0x001A71F3
		public StreamReader(string path) : this(path, true)
		{
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x001A7FFD File Offset: 0x001A71FD
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks) : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x001A8011 File Offset: 0x001A7211
		public StreamReader(string path, Encoding encoding) : this(path, encoding, true, 1024)
		{
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x001A8021 File Offset: 0x001A7221
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(path, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x001A8031 File Offset: 0x001A7231
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : this(StreamReader.ValidateArgsAndOpenPath(path, encoding, bufferSize), encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x001A8048 File Offset: 0x001A7248
		private static Stream ValidateArgsAndOpenPath(string path, Encoding encoding, int bufferSize)
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
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x001A80AB File Offset: 0x001A72AB
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x001A80B4 File Offset: 0x001A72B4
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			if (this._closable)
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
					this._charPos = 0;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06005663 RID: 22115 RVA: 0x001A8110 File Offset: 0x001A7310
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x001A8118 File Offset: 0x001A7318
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x001A8120 File Offset: 0x001A7320
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this._byteLen = 0;
			this._charLen = 0;
			this._charPos = 0;
			if (this._encoding != null)
			{
				this._decoder = this._encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x001A8160 File Offset: 0x001A7360
		public bool EndOfStream
		{
			get
			{
				this.ThrowIfDisposed();
				this.CheckAsyncTaskInProgress();
				if (this._charPos < this._charLen)
				{
					return false;
				}
				int num = this.ReadBuffer();
				return num == 0;
			}
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x001A8194 File Offset: 0x001A7394
		public override int Peek()
		{
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this._charBuffer[this._charPos];
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x001A81D0 File Offset: 0x001A73D0
		public override int Read()
		{
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int result = (int)this._charBuffer[this._charPos];
			this._charPos++;
			return result;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x001A8220 File Offset: 0x001A7420
		public override int Read(char[] buffer, int index, int count)
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
			return this.ReadSpan(new Span<char>(buffer, index, count));
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x001A8284 File Offset: 0x001A7484
		[NullableContext(0)]
		public override int Read(Span<char> buffer)
		{
			if (!(base.GetType() == typeof(StreamReader)))
			{
				return base.Read(buffer);
			}
			return this.ReadSpan(buffer);
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x001A82AC File Offset: 0x001A74AC
		private int ReadSpan(Span<char> buffer)
		{
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			int i = buffer.Length;
			while (i > 0)
			{
				int num2 = this._charLen - this._charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer.Slice(num), out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				if (!flag)
				{
					new Span<char>(this._charBuffer, this._charPos, num2).CopyTo(buffer.Slice(num));
					this._charPos += num2;
				}
				num += num2;
				i -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x001A8348 File Offset: 0x001A7548
		public override string ReadToEnd()
		{
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this._charLen - this._charPos);
			do
			{
				stringBuilder.Append(this._charBuffer, this._charPos, this._charLen - this._charPos);
				this._charPos = this._charLen;
				this.ReadBuffer();
			}
			while (this._charLen > 0);
			return stringBuilder.ToString();
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x001A83B8 File Offset: 0x001A75B8
		public override int ReadBlock(char[] buffer, int index, int count)
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
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x001A8424 File Offset: 0x001A7624
		[NullableContext(0)]
		public override int ReadBlock(Span<char> buffer)
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlock(buffer);
			}
			int num = 0;
			int num2;
			do
			{
				num2 = this.ReadSpan(buffer.Slice(num));
				num += num2;
			}
			while (num2 > 0 && num < buffer.Length);
			return num;
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x001A8474 File Offset: 0x001A7674
		private void CompressBuffer(int n)
		{
			Buffer.BlockCopy(this._byteBuffer, n, this._byteBuffer, 0, this._byteLen - n);
			this._byteLen -= n;
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x001A84A0 File Offset: 0x001A76A0
		private void DetectEncoding()
		{
			if (this._byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this._byteBuffer[0] == 254 && this._byteBuffer[1] == 255)
			{
				this._encoding = Encoding.BigEndianUnicode;
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this._byteBuffer[0] == 255 && this._byteBuffer[1] == 254)
			{
				if (this._byteLen < 4 || this._byteBuffer[2] != 0 || this._byteBuffer[3] != 0)
				{
					this._encoding = Encoding.Unicode;
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this._encoding = Encoding.UTF32;
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this._byteLen >= 3 && this._byteBuffer[0] == 239 && this._byteBuffer[1] == 187 && this._byteBuffer[2] == 191)
			{
				this._encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this._byteLen >= 4 && this._byteBuffer[0] == 0 && this._byteBuffer[1] == 0 && this._byteBuffer[2] == 254 && this._byteBuffer[3] == 255)
			{
				this._encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this._byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this._decoder = this._encoding.GetDecoder();
				int maxCharCount = this._encoding.GetMaxCharCount(this._byteBuffer.Length);
				if (maxCharCount > this._maxCharsPerBuffer)
				{
					this._charBuffer = new char[maxCharCount];
				}
				this._maxCharsPerBuffer = maxCharCount;
			}
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x001A8658 File Offset: 0x001A7858
		private unsafe bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			ReadOnlySpan<byte> preamble = this._encoding.Preamble;
			int num = (this._byteLen >= preamble.Length) ? (preamble.Length - this._bytePos) : (this._byteLen - this._bytePos);
			int i = 0;
			while (i < num)
			{
				if (this._byteBuffer[this._bytePos] != *preamble[this._bytePos])
				{
					this._bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this._bytePos++;
			}
			if (this._checkPreamble && this._bytePos == preamble.Length)
			{
				this.CompressBuffer(preamble.Length);
				this._bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x001A8734 File Offset: 0x001A7934
		internal virtual int ReadBuffer()
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = (this._byteLen < this._byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = (this._byteLen = 0);
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x001A889C File Offset: 0x001A7A9C
		private int ReadBuffer(Span<char> userBuffer, out bool readToUserBuffer)
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = (userBuffer.Length >= this._maxCharsPerBuffer);
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num2 == 0)
					{
						break;
					}
					this._byteLen += num2;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto IL_1CD;
					}
				}
				this._isBlocked = (this._byteLen < this._byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = (userBuffer.Length >= this._maxCharsPerBuffer);
					}
					this._charPos = 0;
					if (readToUserBuffer)
					{
						num += this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
						this._charLen = 0;
					}
					else
					{
						num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
						this._charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_1CD;
				}
			}
			if (this._byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
					this._charLen = 0;
				}
				else
				{
					num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
					this._charLen += num;
				}
			}
			return num;
			IL_1CD:
			this._isBlocked &= (num < userBuffer.Length);
			return num;
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x001A8A90 File Offset: 0x001A7C90
		[NullableContext(2)]
		public override string ReadLine()
		{
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this._charPos;
				do
				{
					c = this._charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_43;
					}
					num++;
				}
				while (num < this._charLen);
				num = this._charLen - this._charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this._charBuffer, this._charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_10;
				}
			}
			IL_43:
			string result;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this._charBuffer, this._charPos, num - this._charPos);
				result = stringBuilder.ToString();
			}
			else
			{
				result = new string(this._charBuffer, this._charPos, num - this._charPos);
			}
			this._charPos = num + 1;
			if (c == '\r' && (this._charPos < this._charLen || this.ReadBuffer() > 0) && this._charBuffer[this._charPos] == '\n')
			{
				this._charPos++;
			}
			return result;
			Block_10:
			return stringBuilder.ToString();
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x001A8BB8 File Offset: 0x001A7DB8
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x001A8C00 File Offset: 0x001A7E00
		private Task<string> ReadLineAsyncInternal()
		{
			StreamReader.<ReadLineAsyncInternal>d__59 <ReadLineAsyncInternal>d__;
			<ReadLineAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadLineAsyncInternal>d__.<>4__this = this;
			<ReadLineAsyncInternal>d__.<>1__state = -1;
			<ReadLineAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadLineAsyncInternal>d__59>(ref <ReadLineAsyncInternal>d__);
			return <ReadLineAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x001A8C44 File Offset: 0x001A7E44
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x001A8C8C File Offset: 0x001A7E8C
		private Task<string> ReadToEndAsyncInternal()
		{
			StreamReader.<ReadToEndAsyncInternal>d__61 <ReadToEndAsyncInternal>d__;
			<ReadToEndAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadToEndAsyncInternal>d__.<>4__this = this;
			<ReadToEndAsyncInternal>d__.<>1__state = -1;
			<ReadToEndAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadToEndAsyncInternal>d__61>(ref <ReadToEndAsyncInternal>d__);
			return <ReadToEndAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x001A8CD0 File Offset: 0x001A7ED0
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
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(new Memory<char>(buffer, index, count), CancellationToken.None).AsTask();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x001A8D78 File Offset: 0x001A7F78
		[NullableContext(0)]
		public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, cancellationToken);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			return this.ReadAsyncInternal(buffer, cancellationToken);
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x001A8DCC File Offset: 0x001A7FCC
		internal override ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			StreamReader.<ReadAsyncInternal>d__64 <ReadAsyncInternal>d__;
			<ReadAsyncInternal>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadAsyncInternal>d__.<>4__this = this;
			<ReadAsyncInternal>d__.buffer = buffer;
			<ReadAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadAsyncInternal>d__.<>1__state = -1;
			<ReadAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadAsyncInternal>d__64>(ref <ReadAsyncInternal>d__);
			return <ReadAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x001A8E20 File Offset: 0x001A8020
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
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x001A8EB8 File Offset: 0x001A80B8
		[NullableContext(0)]
		public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, cancellationToken);
			}
			this.ThrowIfDisposed();
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			ValueTask<int> result = base.ReadBlockAsyncInternal(buffer, cancellationToken);
			if (result.IsCompletedSuccessfully)
			{
				return result;
			}
			Task<int> task = result.AsTask();
			this._asyncReadTask = task;
			return new ValueTask<int>(task);
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001A8F2C File Offset: 0x001A812C
		private ValueTask<int> ReadBufferAsync(CancellationToken cancellationToken)
		{
			StreamReader.<ReadBufferAsync>d__67 <ReadBufferAsync>d__;
			<ReadBufferAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadBufferAsync>d__.<>4__this = this;
			<ReadBufferAsync>d__.cancellationToken = cancellationToken;
			<ReadBufferAsync>d__.<>1__state = -1;
			<ReadBufferAsync>d__.<>t__builder.Start<StreamReader.<ReadBufferAsync>d__67>(ref <ReadBufferAsync>d__);
			return <ReadBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x001A8F77 File Offset: 0x001A8177
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				this.<ThrowIfDisposed>g__ThrowObjectDisposedException|68_0();
			}
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x001A8F93 File Offset: 0x001A8193
		[CompilerGenerated]
		private void <ThrowIfDisposed>g__ThrowObjectDisposedException|68_0()
		{
			throw new ObjectDisposedException(base.GetType().Name, SR.ObjectDisposed_ReaderClosed);
		}

		// Token: 0x0400187D RID: 6269
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x0400187E RID: 6270
		private readonly Stream _stream;

		// Token: 0x0400187F RID: 6271
		private Encoding _encoding;

		// Token: 0x04001880 RID: 6272
		private Decoder _decoder;

		// Token: 0x04001881 RID: 6273
		private readonly byte[] _byteBuffer;

		// Token: 0x04001882 RID: 6274
		private char[] _charBuffer;

		// Token: 0x04001883 RID: 6275
		private int _charPos;

		// Token: 0x04001884 RID: 6276
		private int _charLen;

		// Token: 0x04001885 RID: 6277
		private int _byteLen;

		// Token: 0x04001886 RID: 6278
		private int _bytePos;

		// Token: 0x04001887 RID: 6279
		private int _maxCharsPerBuffer;

		// Token: 0x04001888 RID: 6280
		private bool _disposed;

		// Token: 0x04001889 RID: 6281
		private bool _detectEncoding;

		// Token: 0x0400188A RID: 6282
		private bool _checkPreamble;

		// Token: 0x0400188B RID: 6283
		private bool _isBlocked;

		// Token: 0x0400188C RID: 6284
		private readonly bool _closable;

		// Token: 0x0400188D RID: 6285
		private Task _asyncReadTask = Task.CompletedTask;

		// Token: 0x020006A3 RID: 1699
		private sealed class NullStreamReader : StreamReader
		{
			// Token: 0x17000E3D RID: 3645
			// (get) Token: 0x06005682 RID: 22146 RVA: 0x001A8FAA File Offset: 0x001A81AA
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06005683 RID: 22147 RVA: 0x000AB30B File Offset: 0x000AA50B
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06005684 RID: 22148 RVA: 0x0011DE1A File Offset: 0x0011D01A
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06005685 RID: 22149 RVA: 0x0011DE1A File Offset: 0x0011D01A
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06005686 RID: 22150 RVA: 0x000AC09B File Offset: 0x000AB29B
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06005687 RID: 22151 RVA: 0x000C26FD File Offset: 0x000C18FD
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06005688 RID: 22152 RVA: 0x000CE629 File Offset: 0x000CD829
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06005689 RID: 22153 RVA: 0x000AC09B File Offset: 0x000AB29B
			internal override int ReadBuffer()
			{
				return 0;
			}
		}
	}
}
