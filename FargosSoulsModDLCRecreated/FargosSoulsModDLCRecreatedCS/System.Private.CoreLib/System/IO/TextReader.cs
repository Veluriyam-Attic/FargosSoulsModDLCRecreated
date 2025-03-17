using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006B0 RID: 1712
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		// Token: 0x06005703 RID: 22275 RVA: 0x001AC309 File Offset: 0x001AB509
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x001AC309 File Offset: 0x001AB509
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x000AB30B File Offset: 0x000AA50B
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public virtual int Peek()
		{
			return -1;
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0011DE1A File Offset: 0x0011D01A
		public virtual int Read()
		{
			return -1;
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x001AC318 File Offset: 0x001AB518
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
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.Read();
				if (num == -1)
				{
					break;
				}
				buffer[index + i] = (char)num;
			}
			return i;
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x001AC394 File Offset: 0x001AB594
		[NullableContext(0)]
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if (num > buffer.Length)
				{
					throw new IOException(SR.IO_InvalidReadLength);
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x001AC40C File Offset: 0x001AB60C
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int charCount;
			while ((charCount = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, charCount);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x001AC450 File Offset: 0x001AB650
		public virtual int ReadBlock(char[] buffer, int index, int count)
		{
			int num = 0;
			int num2;
			do
			{
				num += (num2 = this.Read(buffer, index + num, count - num));
			}
			while (num2 > 0 && num < count);
			return num;
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x001AC47C File Offset: 0x001AB67C
		[NullableContext(0)]
		public virtual int ReadBlock(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.ReadBlock(array, 0, buffer.Length);
				if (num > buffer.Length)
				{
					throw new IOException(SR.IO_InvalidReadLength);
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x001AC4F4 File Offset: 0x001AB6F4
		[NullableContext(2)]
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_43;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_43:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x001AC555 File Offset: 0x001AB755
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual Task<string> ReadLineAsync()
		{
			return Task<string>.Factory.StartNew((object state) => ((TextReader)state).ReadLine(), this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x001AC58C File Offset: 0x001AB78C
		public virtual Task<string> ReadToEndAsync()
		{
			TextReader.<ReadToEndAsync>d__14 <ReadToEndAsync>d__;
			<ReadToEndAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadToEndAsync>d__.<>4__this = this;
			<ReadToEndAsync>d__.<>1__state = -1;
			<ReadToEndAsync>d__.<>t__builder.Start<TextReader.<ReadToEndAsync>d__14>(ref <ReadToEndAsync>d__);
			return <ReadToEndAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x001AC5D0 File Offset: 0x001AB7D0
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
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
			return this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x001AC648 File Offset: 0x001AB848
		[NullableContext(0)]
		public virtual ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.Read(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x001AC6C0 File Offset: 0x001AB8C0
		internal virtual ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			Tuple<TextReader, Memory<char>> state2 = new Tuple<TextReader, Memory<char>>(this, buffer);
			return new ValueTask<int>(Task<int>.Factory.StartNew(delegate(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}, state2, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x001AC70C File Offset: 0x001AB90C
		public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
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
			return this.ReadBlockAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x001AC784 File Offset: 0x001AB984
		[NullableContext(0)]
		public virtual ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.ReadBlock(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadBlockAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x001AC7FC File Offset: 0x001AB9FC
		internal ValueTask<int> ReadBlockAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			TextReader.<ReadBlockAsyncInternal>d__20 <ReadBlockAsyncInternal>d__;
			<ReadBlockAsyncInternal>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadBlockAsyncInternal>d__.<>4__this = this;
			<ReadBlockAsyncInternal>d__.buffer = buffer;
			<ReadBlockAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadBlockAsyncInternal>d__.<>1__state = -1;
			<ReadBlockAsyncInternal>d__.<>t__builder.Start<TextReader.<ReadBlockAsyncInternal>d__20>(ref <ReadBlockAsyncInternal>d__);
			return <ReadBlockAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x001AC84F File Offset: 0x001ABA4F
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (!(reader is TextReader.SyncTextReader))
			{
				return new TextReader.SyncTextReader(reader);
			}
			return reader;
		}

		// Token: 0x040018FA RID: 6394
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x020006B1 RID: 1713
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x06005719 RID: 22297 RVA: 0x000AC09B File Offset: 0x000AB29B
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x0600571A RID: 22298 RVA: 0x000C26FD File Offset: 0x000C18FD
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x020006B2 RID: 1714
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x0600571B RID: 22299 RVA: 0x001AC883 File Offset: 0x001ABA83
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x0600571C RID: 22300 RVA: 0x001AC892 File Offset: 0x001ABA92
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x0600571D RID: 22301 RVA: 0x001AC89F File Offset: 0x001ABA9F
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x0600571E RID: 22302 RVA: 0x001AC8AF File Offset: 0x001ABAAF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x0600571F RID: 22303 RVA: 0x001AC8BC File Offset: 0x001ABABC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x06005720 RID: 22304 RVA: 0x001AC8C9 File Offset: 0x001ABAC9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read(char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x06005721 RID: 22305 RVA: 0x001AC8D9 File Offset: 0x001ABAD9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int ReadBlock(char[] buffer, int index, int count)
			{
				return this._in.ReadBlock(buffer, index, count);
			}

			// Token: 0x06005722 RID: 22306 RVA: 0x001AC8E9 File Offset: 0x001ABAE9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x06005723 RID: 22307 RVA: 0x001AC8F6 File Offset: 0x001ABAF6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x06005724 RID: 22308 RVA: 0x001ABDB6 File Offset: 0x001AAFB6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadLineAsync()
			{
				return Task.FromResult<string>(this.ReadLine());
			}

			// Token: 0x06005725 RID: 22309 RVA: 0x001ABDC3 File Offset: 0x001AAFC3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadToEndAsync()
			{
				return Task.FromResult<string>(this.ReadToEnd());
			}

			// Token: 0x06005726 RID: 22310 RVA: 0x001ABDD0 File Offset: 0x001AAFD0
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
			}

			// Token: 0x06005727 RID: 22311 RVA: 0x001ABE58 File Offset: 0x001AB058
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x040018FB RID: 6395
			internal readonly TextReader _in;
		}
	}
}
