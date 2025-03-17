using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000672 RID: 1650
	[Nullable(0)]
	[NullableContext(1)]
	public abstract class Stream : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x06005404 RID: 21508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasOverriddenBeginEndRead();

		// Token: 0x06005405 RID: 21509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasOverriddenBeginEndWrite();

		// Token: 0x06005406 RID: 21510 RVA: 0x0019CE2B File Offset: 0x0019C02B
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06005407 RID: 21511
		public abstract bool CanRead { get; }

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06005408 RID: 21512
		public abstract bool CanSeek { get; }

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600540A RID: 21514
		public abstract bool CanWrite { get; }

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600540B RID: 21515
		public abstract long Length { get; }

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600540C RID: 21516
		// (set) Token: 0x0600540D RID: 21517
		public abstract long Position { get; set; }

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600540E RID: 21518 RVA: 0x0019CE57 File Offset: 0x0019C057
		// (set) Token: 0x0600540F RID: 21519 RVA: 0x0019CE57 File Offset: 0x0019C057
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException(SR.InvalidOperation_TimeoutsNotSupported);
			}
			set
			{
				throw new InvalidOperationException(SR.InvalidOperation_TimeoutsNotSupported);
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005410 RID: 21520 RVA: 0x0019CE57 File Offset: 0x0019C057
		// (set) Token: 0x06005411 RID: 21521 RVA: 0x0019CE57 File Offset: 0x0019C057
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException(SR.InvalidOperation_TimeoutsNotSupported);
			}
			set
			{
				throw new InvalidOperationException(SR.InvalidOperation_TimeoutsNotSupported);
			}
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0019CE64 File Offset: 0x0019C064
		public Task CopyToAsync(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize);
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0019CE80 File Offset: 0x0019C080
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0019CE90 File Offset: 0x0019C090
		public Task CopyToAsync(Stream destination, CancellationToken cancellationToken)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize, cancellationToken);
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0019CEAD File Offset: 0x0019C0AD
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0019CEC0 File Offset: 0x0019C0C0
		private Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			Stream.<CopyToAsyncInternal>d__29 <CopyToAsyncInternal>d__;
			<CopyToAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CopyToAsyncInternal>d__.<>4__this = this;
			<CopyToAsyncInternal>d__.destination = destination;
			<CopyToAsyncInternal>d__.bufferSize = bufferSize;
			<CopyToAsyncInternal>d__.cancellationToken = cancellationToken;
			<CopyToAsyncInternal>d__.<>1__state = -1;
			<CopyToAsyncInternal>d__.<>t__builder.Start<Stream.<CopyToAsyncInternal>d__29>(ref <CopyToAsyncInternal>d__);
			return <CopyToAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0019CF1C File Offset: 0x0019C11C
		public void CopyTo(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			this.CopyTo(destination, copyBufferSize);
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0019CF38 File Offset: 0x0019C138
		public virtual void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				int count;
				while ((count = this.Read(array, 0, array.Length)) != 0)
				{
					destination.Write(array, 0, count);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0019CF94 File Offset: 0x0019C194
		private int GetCopyBufferSize()
		{
			int num = 81920;
			if (this.CanSeek)
			{
				long length = this.Length;
				long position = this.Position;
				if (length <= position)
				{
					num = 1;
				}
				else
				{
					long num2 = length - position;
					if (num2 > 0L)
					{
						num = (int)Math.Min((long)num, num2);
					}
				}
			}
			return num;
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0019CFD9 File Offset: 0x0019C1D9
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0019CFE8 File Offset: 0x0019C1E8
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x000AB30B File Offset: 0x000AA50B
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0019CFF0 File Offset: 0x0019C1F0
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				this.Dispose();
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception exception)
			{
				valueTask = ValueTask.FromException(exception);
			}
			return valueTask;
		}

		// Token: 0x0600541E RID: 21534
		public abstract void Flush();

		// Token: 0x0600541F RID: 21535 RVA: 0x0019D02C File Offset: 0x0019C22C
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0019D039 File Offset: 0x0019C239
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0019D06C File Offset: 0x0019C26C
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0019D074 File Offset: 0x0019C274
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0019D088 File Offset: 0x0019C288
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, apm, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					result = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation(readWriteTask2);
					}
					readWriteTask2.ClearBeginState();
				}
				return result;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0019D104 File Offset: 0x0019C304
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask readWriteTask = asyncResult as Stream.ReadWriteTask;
			if (readWriteTask == null)
			{
				throw new ArgumentException(SR.InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple);
			}
			if (readWriteTask._endCalled)
			{
				throw new InvalidOperationException(SR.InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple);
			}
			if (!readWriteTask._isRead)
			{
				throw new ArgumentException(SR.InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple);
			}
			int result;
			try
			{
				result = readWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation(readWriteTask);
			}
			return result;
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0019D188 File Offset: 0x0019C388
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0019D198 File Offset: 0x0019C398
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0019D1B4 File Offset: 0x0019C3B4
		[NullableContext(0)]
		public virtual ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask<int>(this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			return Stream.<ReadAsync>g__FinishReadAsync|46_0(this.ReadAsync(array, 0, buffer.Length, cancellationToken), array, buffer);
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0019D21C File Offset: 0x0019C41C
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndRead())
			{
				return (Task<int>)this.BeginReadInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0019D2A9 File Offset: 0x0019C4A9
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0019D2BC File Offset: 0x0019C4BC
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, apm, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
					result = 0;
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation(readWriteTask2);
					}
					readWriteTask2.ClearBeginState();
				}
				return result;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0019D338 File Offset: 0x0019C538
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask2 = (Stream.ReadWriteTask)state;
				readWriteTask2._stream.RunReadWriteTask(readWriteTask2);
			}, readWriteTask, default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0019D38F File Offset: 0x0019C58F
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0019D3A3 File Offset: 0x0019C5A3
		private void FinishTrackingAsyncOperation(Stream.ReadWriteTask task)
		{
			task._endCalled = true;
			this._asyncActiveSemaphore.Release();
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0019D3B8 File Offset: 0x0019C5B8
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask readWriteTask = asyncResult as Stream.ReadWriteTask;
			if (readWriteTask == null)
			{
				throw new ArgumentException(SR.InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple);
			}
			if (readWriteTask._endCalled)
			{
				throw new InvalidOperationException(SR.InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple);
			}
			if (readWriteTask._isRead)
			{
				throw new ArgumentException(SR.InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple);
			}
			try
			{
				readWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation(readWriteTask);
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0019D438 File Offset: 0x0019C638
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0019D448 File Offset: 0x0019C648
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0019D464 File Offset: 0x0019C664
		[NullableContext(0)]
		public virtual ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask(this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			buffer.Span.CopyTo(array);
			return new ValueTask(this.FinishWriteAsync(this.WriteAsync(array, 0, buffer.Length, cancellationToken), array));
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0019D4E0 File Offset: 0x0019C6E0
		private Task FinishWriteAsync(Task writeTask, byte[] localBuffer)
		{
			Stream.<FinishWriteAsync>d__59 <FinishWriteAsync>d__;
			<FinishWriteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishWriteAsync>d__.writeTask = writeTask;
			<FinishWriteAsync>d__.localBuffer = localBuffer;
			<FinishWriteAsync>d__.<>1__state = -1;
			<FinishWriteAsync>d__.<>t__builder.Start<Stream.<FinishWriteAsync>d__59>(ref <FinishWriteAsync>d__);
			return <FinishWriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0019D52C File Offset: 0x0019C72C
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndWrite())
			{
				return (Task)this.BeginWriteInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		// Token: 0x06005434 RID: 21556
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06005435 RID: 21557
		public abstract void SetLength(long value);

		// Token: 0x06005436 RID: 21558
		public abstract int Read(byte[] buffer, int offset, int count);

		// Token: 0x06005437 RID: 21559 RVA: 0x0019D5BC File Offset: 0x0019C7BC
		[NullableContext(0)]
		public virtual int Read(Span<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if (num > buffer.Length)
				{
					throw new IOException(SR.IO_StreamTooLong);
				}
				new Span<byte>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0019D634 File Offset: 0x0019C834
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06005439 RID: 21561
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x0600543A RID: 21562 RVA: 0x0019D65C File Offset: 0x0019C85C
		[NullableContext(0)]
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
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

		// Token: 0x0600543B RID: 21563 RVA: 0x0019D6B8 File Offset: 0x0019C8B8
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0019D6D9 File Offset: 0x0019C8D9
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0019D6FC File Offset: 0x0019C8FC
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				int bytesRead = this.Read(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(bytesRead, state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0019D748 File Offset: 0x0019C948
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0019D750 File Offset: 0x0019C950
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0019D79C File Offset: 0x0019C99C
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0019D7B0 File Offset: 0x0019C9B0
		[CompilerGenerated]
		internal static ValueTask<int> <ReadAsync>g__FinishReadAsync|46_0(Task<int> readTask, byte[] localBuffer, Memory<byte> localDestination)
		{
			Stream.<<ReadAsync>g__FinishReadAsync|46_0>d <<ReadAsync>g__FinishReadAsync|46_0>d;
			<<ReadAsync>g__FinishReadAsync|46_0>d.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<<ReadAsync>g__FinishReadAsync|46_0>d.readTask = readTask;
			<<ReadAsync>g__FinishReadAsync|46_0>d.localBuffer = localBuffer;
			<<ReadAsync>g__FinishReadAsync|46_0>d.localDestination = localDestination;
			<<ReadAsync>g__FinishReadAsync|46_0>d.<>1__state = -1;
			<<ReadAsync>g__FinishReadAsync|46_0>d.<>t__builder.Start<Stream.<<ReadAsync>g__FinishReadAsync|46_0>d>(ref <<ReadAsync>g__FinishReadAsync|46_0>d);
			return <<ReadAsync>g__FinishReadAsync|46_0>d.<>t__builder.Task;
		}

		// Token: 0x0400177A RID: 6010
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x0400177B RID: 6011
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000673 RID: 1651
		private struct ReadWriteParameters
		{
			// Token: 0x0400177C RID: 6012
			internal byte[] Buffer;

			// Token: 0x0400177D RID: 6013
			internal int Offset;

			// Token: 0x0400177E RID: 6014
			internal int Count;
		}

		// Token: 0x02000674 RID: 1652
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06005445 RID: 21573 RVA: 0x0019D803 File Offset: 0x0019CA03
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06005446 RID: 21574 RVA: 0x0019D814 File Offset: 0x0019CA14
			public ReadWriteTask(bool isRead, bool apm, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback) : base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				this._isRead = isRead;
				this._apm = apm;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture();
					base.AddCompletionAction(this, false);
				}
			}

			// Token: 0x06005447 RID: 21575 RVA: 0x0019D880 File Offset: 0x0019CA80
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06005448 RID: 21576 RVA: 0x0019D8AC File Offset: 0x0019CAAC
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback;
				if ((contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback) == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				ContextCallback callback2 = contextCallback;
				ExecutionContext.RunInternal(context, callback2, this);
			}

			// Token: 0x17000E08 RID: 3592
			// (get) Token: 0x06005449 RID: 21577 RVA: 0x000AC09E File Offset: 0x000AB29E
			bool ITaskCompletionAction.InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400177F RID: 6015
			internal readonly bool _isRead;

			// Token: 0x04001780 RID: 6016
			internal readonly bool _apm;

			// Token: 0x04001781 RID: 6017
			internal bool _endCalled;

			// Token: 0x04001782 RID: 6018
			internal Stream _stream;

			// Token: 0x04001783 RID: 6019
			internal byte[] _buffer;

			// Token: 0x04001784 RID: 6020
			internal readonly int _offset;

			// Token: 0x04001785 RID: 6021
			internal readonly int _count;

			// Token: 0x04001786 RID: 6022
			private AsyncCallback _callback;

			// Token: 0x04001787 RID: 6023
			private ExecutionContext _context;

			// Token: 0x04001788 RID: 6024
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x02000675 RID: 1653
		private sealed class NullStream : Stream
		{
			// Token: 0x0600544A RID: 21578 RVA: 0x0019D904 File Offset: 0x0019CB04
			internal NullStream()
			{
			}

			// Token: 0x17000E09 RID: 3593
			// (get) Token: 0x0600544B RID: 21579 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E0A RID: 3594
			// (get) Token: 0x0600544C RID: 21580 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E0B RID: 3595
			// (get) Token: 0x0600544D RID: 21581 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E0C RID: 3596
			// (get) Token: 0x0600544E RID: 21582 RVA: 0x001876ED File Offset: 0x001868ED
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000E0D RID: 3597
			// (get) Token: 0x0600544F RID: 21583 RVA: 0x001876ED File Offset: 0x001868ED
			// (set) Token: 0x06005450 RID: 21584 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x06005451 RID: 21585 RVA: 0x0019D90C File Offset: 0x0019CB0C
			public override void CopyTo(Stream destination, int bufferSize)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			}

			// Token: 0x06005452 RID: 21586 RVA: 0x0019D916 File Offset: 0x0019CB16
			public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06005453 RID: 21587 RVA: 0x000AB30B File Offset: 0x000AA50B
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06005454 RID: 21588 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Flush()
			{
			}

			// Token: 0x06005455 RID: 21589 RVA: 0x0019D935 File Offset: 0x0019CB35
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06005456 RID: 21590 RVA: 0x0019D94C File Offset: 0x0019CB4C
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					throw Error.GetReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06005457 RID: 21591 RVA: 0x0019D969 File Offset: 0x0019CB69
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x06005458 RID: 21592 RVA: 0x0019D97F File Offset: 0x0019CB7F
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06005459 RID: 21593 RVA: 0x0019D99C File Offset: 0x0019CB9C
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x0600545A RID: 21594 RVA: 0x000AC09B File Offset: 0x000AB29B
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x0600545B RID: 21595 RVA: 0x000AC09B File Offset: 0x000AB29B
			public override int Read(Span<byte> buffer)
			{
				return 0;
			}

			// Token: 0x0600545C RID: 21596 RVA: 0x0019D9B2 File Offset: 0x0019CBB2
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				return Stream.NullStream.s_zeroTask;
			}

			// Token: 0x0600545D RID: 21597 RVA: 0x0019D9B9 File Offset: 0x0019CBB9
			public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ValueTask<int>(0);
			}

			// Token: 0x0600545E RID: 21598 RVA: 0x0011DE1A File Offset: 0x0011D01A
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x0600545F RID: 21599 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06005460 RID: 21600 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Write(ReadOnlySpan<byte> buffer)
			{
			}

			// Token: 0x06005461 RID: 21601 RVA: 0x0019D9C1 File Offset: 0x0019CBC1
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06005462 RID: 21602 RVA: 0x0019D9DC File Offset: 0x0019CBDC
			public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return default(ValueTask);
				}
				return ValueTask.FromCanceled(cancellationToken);
			}

			// Token: 0x06005463 RID: 21603 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x06005464 RID: 21604 RVA: 0x001876ED File Offset: 0x001868ED
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06005465 RID: 21605 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void SetLength(long length)
			{
			}

			// Token: 0x04001789 RID: 6025
			private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);
		}

		// Token: 0x02000676 RID: 1654
		private sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06005467 RID: 21607 RVA: 0x0019DA0F File Offset: 0x0019CC0F
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06005468 RID: 21608 RVA: 0x0019DA25 File Offset: 0x0019CC25
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x06005469 RID: 21609 RVA: 0x0019DA3B File Offset: 0x0019CC3B
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x17000E0E RID: 3598
			// (get) Token: 0x0600546A RID: 21610 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E0F RID: 3599
			// (get) Token: 0x0600546B RID: 21611 RVA: 0x0019DA5D File Offset: 0x0019CC5D
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x17000E10 RID: 3600
			// (get) Token: 0x0600546C RID: 21612 RVA: 0x0019DA89 File Offset: 0x0019CC89
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x17000E11 RID: 3601
			// (get) Token: 0x0600546D RID: 21613 RVA: 0x000AC09E File Offset: 0x000AB29E
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600546E RID: 21614 RVA: 0x0019DA91 File Offset: 0x0019CC91
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x0600546F RID: 21615 RVA: 0x0019DAA8 File Offset: 0x0019CCA8
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException(SR.Arg_WrongAsyncResult);
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException(SR.InvalidOperation_EndReadCalledMultiple);
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x06005470 RID: 21616 RVA: 0x0019DAF8 File Offset: 0x0019CCF8
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException(SR.Arg_WrongAsyncResult);
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException(SR.InvalidOperation_EndWriteCalledMultiple);
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x0400178A RID: 6026
			private readonly object _stateObject;

			// Token: 0x0400178B RID: 6027
			private readonly bool _isWrite;

			// Token: 0x0400178C RID: 6028
			private ManualResetEvent _waitHandle;

			// Token: 0x0400178D RID: 6029
			private readonly ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x0400178E RID: 6030
			private bool _endXxxCalled;

			// Token: 0x0400178F RID: 6031
			private readonly int _bytesRead;
		}

		// Token: 0x02000678 RID: 1656
		private sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x06005474 RID: 21620 RVA: 0x0019DB56 File Offset: 0x0019CD56
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x17000E12 RID: 3602
			// (get) Token: 0x06005475 RID: 21621 RVA: 0x0019DB73 File Offset: 0x0019CD73
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x17000E13 RID: 3603
			// (get) Token: 0x06005476 RID: 21622 RVA: 0x0019DB80 File Offset: 0x0019CD80
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x17000E14 RID: 3604
			// (get) Token: 0x06005477 RID: 21623 RVA: 0x0019DB8D File Offset: 0x0019CD8D
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x17000E15 RID: 3605
			// (get) Token: 0x06005478 RID: 21624 RVA: 0x0019DB9A File Offset: 0x0019CD9A
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x17000E16 RID: 3606
			// (get) Token: 0x06005479 RID: 21625 RVA: 0x0019DBA8 File Offset: 0x0019CDA8
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x17000E17 RID: 3607
			// (get) Token: 0x0600547A RID: 21626 RVA: 0x0019DBF0 File Offset: 0x0019CDF0
			// (set) Token: 0x0600547B RID: 21627 RVA: 0x0019DC38 File Offset: 0x0019CE38
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x17000E18 RID: 3608
			// (get) Token: 0x0600547C RID: 21628 RVA: 0x0019DC80 File Offset: 0x0019CE80
			// (set) Token: 0x0600547D RID: 21629 RVA: 0x0019DC8D File Offset: 0x0019CE8D
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x17000E19 RID: 3609
			// (get) Token: 0x0600547E RID: 21630 RVA: 0x0019DC9B File Offset: 0x0019CE9B
			// (set) Token: 0x0600547F RID: 21631 RVA: 0x0019DCA8 File Offset: 0x0019CEA8
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x06005480 RID: 21632 RVA: 0x0019DCB8 File Offset: 0x0019CEB8
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x06005481 RID: 21633 RVA: 0x0019DD14 File Offset: 0x0019CF14
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x06005482 RID: 21634 RVA: 0x0019DD70 File Offset: 0x0019CF70
			public override ValueTask DisposeAsync()
			{
				Stream stream = this._stream;
				ValueTask result;
				lock (stream)
				{
					result = this._stream.DisposeAsync();
				}
				return result;
			}

			// Token: 0x06005483 RID: 21635 RVA: 0x0019DDB8 File Offset: 0x0019CFB8
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x06005484 RID: 21636 RVA: 0x0019DE00 File Offset: 0x0019D000
			public override int Read(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.Read(bytes, offset, count);
				}
				return result;
			}

			// Token: 0x06005485 RID: 21637 RVA: 0x0019DE4C File Offset: 0x0019D04C
			public override int Read(Span<byte> buffer)
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.Read(buffer);
				}
				return result;
			}

			// Token: 0x06005486 RID: 21638 RVA: 0x0019DE94 File Offset: 0x0019D094
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.ReadByte();
				}
				return result;
			}

			// Token: 0x06005487 RID: 21639 RVA: 0x0019DEDC File Offset: 0x0019D0DC
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndRead();
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (flag ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true, true));
				}
				return result;
			}

			// Token: 0x06005488 RID: 21640 RVA: 0x0019DF50 File Offset: 0x0019D150
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.EndRead(asyncResult);
				}
				return result;
			}

			// Token: 0x06005489 RID: 21641 RVA: 0x0019DFA8 File Offset: 0x0019D1A8
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long result;
				lock (stream)
				{
					result = this._stream.Seek(offset, origin);
				}
				return result;
			}

			// Token: 0x0600548A RID: 21642 RVA: 0x0019DFF4 File Offset: 0x0019D1F4
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x0600548B RID: 21643 RVA: 0x0019E03C File Offset: 0x0019D23C
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x0600548C RID: 21644 RVA: 0x0019E084 File Offset: 0x0019D284
			public override void Write(ReadOnlySpan<byte> buffer)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(buffer);
				}
			}

			// Token: 0x0600548D RID: 21645 RVA: 0x0019E0CC File Offset: 0x0019D2CC
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x0600548E RID: 21646 RVA: 0x0019E114 File Offset: 0x0019D314
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndWrite();
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (flag ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true, true));
				}
				return result;
			}

			// Token: 0x0600548F RID: 21647 RVA: 0x0019E188 File Offset: 0x0019D388
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x04001792 RID: 6034
			private readonly Stream _stream;
		}
	}
}
