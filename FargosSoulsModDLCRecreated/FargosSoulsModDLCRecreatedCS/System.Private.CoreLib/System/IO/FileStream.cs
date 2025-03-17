using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000690 RID: 1680
	[NullableContext(1)]
	[Nullable(0)]
	public class FileStream : Stream
	{
		// Token: 0x0600553C RID: 21820 RVA: 0x001A1BBE File Offset: 0x001A0DBE
		[Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead.  https://go.microsoft.com/fwlink/?linkid=14202")]
		public FileStream(IntPtr handle, FileAccess access) : this(handle, access, true, 4096, false)
		{
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x001A1BCF File Offset: 0x001A0DCF
		[Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  https://go.microsoft.com/fwlink/?linkid=14202")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle) : this(handle, access, ownsHandle, 4096, false)
		{
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x001A1BE0 File Offset: 0x001A0DE0
		[Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  https://go.microsoft.com/fwlink/?linkid=14202")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize) : this(handle, access, ownsHandle, bufferSize, false)
		{
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x001A1BF0 File Offset: 0x001A0DF0
		[Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  https://go.microsoft.com/fwlink/?linkid=14202")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
		{
			this._activeBufferOperation = Task.CompletedTask;
			base..ctor();
			SafeFileHandle safeFileHandle = new SafeFileHandle(handle, ownsHandle);
			try
			{
				this.ValidateAndInitFromHandle(safeFileHandle, access, bufferSize, isAsync);
			}
			catch
			{
				GC.SuppressFinalize(safeFileHandle);
				throw;
			}
			this._access = access;
			this._useAsyncIO = isAsync;
			this._fileHandle = safeFileHandle;
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x001A1C54 File Offset: 0x001A0E54
		public FileStream(SafeFileHandle handle, FileAccess access) : this(handle, access, 4096)
		{
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x001A1C63 File Offset: 0x001A0E63
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) : this(handle, access, bufferSize, FileStream.GetDefaultIsAsync(handle))
		{
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x001A1C74 File Offset: 0x001A0E74
		private void ValidateAndInitFromHandle(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
		{
			if (handle.IsInvalid)
			{
				throw new ArgumentException(SR.Arg_InvalidHandle, "handle");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", SR.ArgumentOutOfRange_Enum);
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (handle.IsClosed)
			{
				throw new ObjectDisposedException(SR.ObjectDisposed_FileClosed);
			}
			if (handle.IsAsync != null && isAsync != handle.IsAsync.GetValueOrDefault())
			{
				throw new ArgumentException(SR.Arg_HandleNotAsync, "handle");
			}
			this._exposedHandle = true;
			this._bufferLength = bufferSize;
			this.InitFromHandle(handle, access, isAsync);
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x001A1D22 File Offset: 0x001A0F22
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
		{
			this._activeBufferOperation = Task.CompletedTask;
			base..ctor();
			this.ValidateAndInitFromHandle(handle, access, bufferSize, isAsync);
			this._access = access;
			this._useAsyncIO = isAsync;
			this._fileHandle = handle;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x001A1D56 File Offset: 0x001A0F56
		public FileStream(string path, FileMode mode) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, false)
		{
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x001A1D6F File Offset: 0x001A0F6F
		public FileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, FileShare.Read, 4096, false)
		{
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x001A1D81 File Offset: 0x001A0F81
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 4096, false)
		{
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x001A1D94 File Offset: 0x001A0F94
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, false)
		{
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x001A1DA4 File Offset: 0x001A0FA4
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x001A1DC0 File Offset: 0x001A0FC0
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
		{
			this._activeBufferOperation = Task.CompletedTask;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path", SR.ArgumentNull_Path);
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyPath, "path");
			}
			FileShare fileShare = share & ~FileShare.Inheritable;
			string text = null;
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				text = "mode";
			}
			else if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				text = "access";
			}
			else if (fileShare < FileShare.None || fileShare > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				text = "share";
			}
			if (text != null)
			{
				throw new ArgumentOutOfRangeException(text, SR.ArgumentOutOfRange_Enum);
			}
			if (options != FileOptions.None && (options & (FileOptions)67092479) != FileOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", SR.ArgumentOutOfRange_Enum);
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if ((access & FileAccess.Write) == (FileAccess)0 && (mode == FileMode.Truncate || mode == FileMode.CreateNew || mode == FileMode.Create || mode == FileMode.Append))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidFileModeAndAccessCombo, mode, access), "access");
			}
			if ((access & FileAccess.Read) != (FileAccess)0 && mode == FileMode.Append)
			{
				throw new ArgumentException(SR.Argument_InvalidAppendMode, "access");
			}
			string fullPath = Path.GetFullPath(path);
			this._path = fullPath;
			this._access = access;
			this._bufferLength = bufferSize;
			if ((options & FileOptions.Asynchronous) != FileOptions.None)
			{
				this._useAsyncIO = true;
			}
			if ((access & FileAccess.Write) == FileAccess.Write)
			{
				SerializationInfo.ThrowIfDeserializationInProgress("AllowFileWrites", ref FileStream.s_cachedSerializationSwitch);
			}
			this._fileHandle = this.OpenHandle(mode, share, options);
			try
			{
				this.Init(mode, share, path);
			}
			catch
			{
				this._fileHandle.Dispose();
				this._fileHandle = null;
				throw;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x001A1F58 File Offset: 0x001A1158
		[Obsolete("This property has been deprecated.  Please use FileStream's SafeFileHandle property instead.  https://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual IntPtr Handle
		{
			get
			{
				return this.SafeFileHandle.DangerousGetHandle();
			}
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x001A1F68 File Offset: 0x001A1168
		public virtual void Lock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			this.LockInternal(position, length);
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x001A1FB8 File Offset: 0x001A11B8
		public virtual void Unlock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			this.UnlockInternal(position, length);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x001A2006 File Offset: 0x001A1206
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(FileStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			return this.FlushAsyncInternal(cancellationToken);
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x001A2030 File Offset: 0x001A1230
		public override int Read(byte[] array, int offset, int count)
		{
			this.ValidateReadWriteArgs(array, offset, count);
			if (!this._useAsyncIO)
			{
				return this.ReadSpan(new Span<byte>(array, offset, count));
			}
			return this.ReadAsyncTask(array, offset, count, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x001A2078 File Offset: 0x001A1278
		[NullableContext(0)]
		public override int Read(Span<byte> buffer)
		{
			if (!(base.GetType() == typeof(FileStream)) || this._useAsyncIO)
			{
				return base.Read(buffer);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			return this.ReadSpan(buffer);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x001A20C8 File Offset: 0x001A12C8
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
			if (base.GetType() != typeof(FileStream))
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			if (this.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (!this._useAsyncIO)
			{
				return (Task<int>)base.BeginReadInternal(buffer, offset, count, null, null, true, false);
			}
			return this.ReadAsyncTask(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x001A218C File Offset: 0x001A138C
		[NullableContext(0)]
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(FileStream))
			{
				return base.ReadAsync(buffer, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled<int>(cancellationToken);
			}
			if (this.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (!this._useAsyncIO)
			{
				ArraySegment<byte> arraySegment;
				if (!MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					return base.ReadAsync(buffer, cancellationToken);
				}
				return new ValueTask<int>((Task<int>)base.BeginReadInternal(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, null, true, false));
			}
			else
			{
				int result;
				Task<int> task = this.ReadAsyncInternal(buffer, cancellationToken, out result);
				if (task == null)
				{
					return new ValueTask<int>(result);
				}
				return new ValueTask<int>(task);
			}
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x001A223C File Offset: 0x001A143C
		private Task<int> ReadAsyncTask(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			int num;
			Task<int> task = this.ReadAsyncInternal(new Memory<byte>(array, offset, count), cancellationToken, out num);
			if (task == null)
			{
				task = this._lastSynchronouslyCompletedTask;
				if (task == null || task.Result != num)
				{
					task = (this._lastSynchronouslyCompletedTask = Task.FromResult<int>(num));
				}
			}
			return task;
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x001A2284 File Offset: 0x001A1484
		public override void Write(byte[] array, int offset, int count)
		{
			this.ValidateReadWriteArgs(array, offset, count);
			if (this._useAsyncIO)
			{
				this.WriteAsyncInternal(new ReadOnlyMemory<byte>(array, offset, count), CancellationToken.None).AsTask().GetAwaiter().GetResult();
				return;
			}
			this.WriteSpan(new ReadOnlySpan<byte>(array, offset, count));
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x001A22DC File Offset: 0x001A14DC
		[NullableContext(0)]
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (!(base.GetType() == typeof(FileStream)) || this._useAsyncIO)
			{
				base.Write(buffer);
				return;
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			this.WriteSpan(buffer);
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x001A232C File Offset: 0x001A152C
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
			if (base.GetType() != typeof(FileStream))
			{
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (this.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (!this._useAsyncIO)
			{
				return (Task)base.BeginWriteInternal(buffer, offset, count, null, null, true, false);
			}
			return this.WriteAsyncInternal(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x001A23FC File Offset: 0x001A15FC
		[NullableContext(0)]
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(FileStream))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ValueTask.FromCanceled(cancellationToken);
			}
			if (this.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (this._useAsyncIO)
			{
				return this.WriteAsyncInternal(buffer, cancellationToken);
			}
			ArraySegment<byte> arraySegment;
			if (!MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			return new ValueTask((Task)base.BeginWriteInternal(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, null, true, false));
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x001A2494 File Offset: 0x001A1694
		public override void Flush()
		{
			this.Flush(false);
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x001A249D File Offset: 0x001A169D
		public virtual void Flush(bool flushToDisk)
		{
			if (this.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			this.FlushInternalBuffer();
			if (flushToDisk && this.CanWrite)
			{
				this.FlushOSBuffer();
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06005559 RID: 21849 RVA: 0x001A24C4 File Offset: 0x001A16C4
		public override bool CanRead
		{
			get
			{
				return !this._fileHandle.IsClosed && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600555A RID: 21850 RVA: 0x001A24E0 File Offset: 0x001A16E0
		public override bool CanWrite
		{
			get
			{
				return !this._fileHandle.IsClosed && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x001A24FC File Offset: 0x001A16FC
		private void ValidateReadWriteArgs(byte[] array, int offset, int count)
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
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x001A256C File Offset: 0x001A176C
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (!this.CanSeek)
			{
				throw Error.GetSeekNotSupported();
			}
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			this.SetLengthInternal(value);
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x001A25C4 File Offset: 0x001A17C4
		public virtual SafeFileHandle SafeFileHandle
		{
			get
			{
				this.Flush();
				this._exposedHandle = true;
				return this._fileHandle;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x001A25D9 File Offset: 0x001A17D9
		public virtual string Name
		{
			get
			{
				return this._path ?? SR.IO_UnknownFileName;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600555F RID: 21855 RVA: 0x001A25EA File Offset: 0x001A17EA
		public virtual bool IsAsync
		{
			get
			{
				return this._useAsyncIO;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x001A25F2 File Offset: 0x001A17F2
		public override long Length
		{
			get
			{
				if (this._fileHandle.IsClosed)
				{
					throw Error.GetFileNotOpen();
				}
				if (!this.CanSeek)
				{
					throw Error.GetSeekNotSupported();
				}
				return this.GetLengthInternal();
			}
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x001A261C File Offset: 0x001A181C
		private void VerifyOSHandlePosition()
		{
			bool exposedHandle = this._exposedHandle;
			if (exposedHandle && this.CanSeek)
			{
				long filePosition = this._filePosition;
				long num = this.SeekCore(this._fileHandle, 0L, SeekOrigin.Current, false);
				if (filePosition != num)
				{
					this._readPos = (this._readLength = 0);
					if (this._writePos > 0)
					{
						this._writePos = 0;
						throw new IOException(SR.IO_FileStreamHandlePosition);
					}
				}
			}
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x001A2682 File Offset: 0x001A1882
		private void PrepareForReading()
		{
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (this._readLength == 0 && !this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06005563 RID: 21859 RVA: 0x001A26B0 File Offset: 0x001A18B0
		// (set) Token: 0x06005564 RID: 21860 RVA: 0x001A2702 File Offset: 0x001A1902
		public override long Position
		{
			get
			{
				if (this._fileHandle.IsClosed)
				{
					throw Error.GetFileNotOpen();
				}
				if (!this.CanSeek)
				{
					throw Error.GetSeekNotSupported();
				}
				this.VerifyOSHandlePosition();
				return this._filePosition - (long)this._readLength + (long)this._readPos + (long)this._writePos;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06005565 RID: 21861 RVA: 0x001A2722 File Offset: 0x001A1922
		internal virtual bool IsClosed
		{
			get
			{
				return this._fileHandle.IsClosed;
			}
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x001A272F File Offset: 0x001A192F
		private static bool IsIoRelatedException(Exception e)
		{
			return e is IOException || e is UnauthorizedAccessException || e is NotSupportedException || (e is ArgumentException && !(e is ArgumentNullException));
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x001A2761 File Offset: 0x001A1961
		private byte[] GetBuffer()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferLength];
				this.OnBufferAllocated();
			}
			return this._buffer;
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x001A2788 File Offset: 0x001A1988
		private void OnBufferAllocated()
		{
			if (this._useAsyncIO)
			{
				this._preallocatedOverlapped = new PreAllocatedOverlapped(FileStream.s_ioCallback, this, this._buffer);
			}
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x001A27A9 File Offset: 0x001A19A9
		private void FlushInternalBuffer()
		{
			if (this._writePos > 0)
			{
				this.FlushWriteBuffer(false);
				return;
			}
			if (this._readPos < this._readLength && this.CanSeek)
			{
				this.FlushReadBuffer();
			}
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x001A27D8 File Offset: 0x001A19D8
		private void FlushReadBuffer()
		{
			int num = this._readPos - this._readLength;
			if (num != 0)
			{
				this.SeekCore(this._fileHandle, (long)num, SeekOrigin.Current, false);
			}
			this._readPos = (this._readLength = 0);
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x001A2818 File Offset: 0x001A1A18
		public override int ReadByte()
		{
			this.PrepareForReading();
			byte[] buffer = this.GetBuffer();
			if (this._readPos == this._readLength)
			{
				this.FlushWriteBuffer(false);
				this._readLength = this.FillReadBufferForReadByte();
				this._readPos = 0;
				if (this._readLength == 0)
				{
					return -1;
				}
			}
			byte[] array = buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return array[readPos];
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x001A2878 File Offset: 0x001A1A78
		public override void WriteByte(byte value)
		{
			this.PrepareForWriting();
			if (this._writePos == this._bufferLength)
			{
				this.FlushWriteBufferForWriteByte();
			}
			byte[] buffer = this.GetBuffer();
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x001A28B8 File Offset: 0x001A1AB8
		private void PrepareForWriting()
		{
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (this._writePos == 0)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				this.FlushReadBuffer();
			}
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x001A28EC File Offset: 0x001A1AEC
		~FileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x001A291C File Offset: 0x001A1B1C
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (this.IsClosed)
			{
				throw new ObjectDisposedException(SR.ObjectDisposed_FileClosed);
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.NotSupported_UnreadableStream);
			}
			if (!this.IsAsync)
			{
				return base.BeginRead(array, offset, numBytes, callback, state);
			}
			return TaskToApm.Begin(this.ReadAsyncTask(array, offset, numBytes, CancellationToken.None), callback, state);
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x001A29C8 File Offset: 0x001A1BC8
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, [Nullable(2)] AsyncCallback callback, [Nullable(2)] object state)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (this.IsClosed)
			{
				throw new ObjectDisposedException(SR.ObjectDisposed_FileClosed);
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.NotSupported_UnwritableStream);
			}
			if (!this.IsAsync)
			{
				return base.BeginWrite(array, offset, numBytes, callback, state);
			}
			return TaskToApm.Begin(this.WriteAsyncInternal(new ReadOnlyMemory<byte>(array, offset, numBytes), CancellationToken.None).AsTask(), callback, state);
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x001A2A7E File Offset: 0x001A1C7E
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.IsAsync)
			{
				return base.EndRead(asyncResult);
			}
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x001A2AA4 File Offset: 0x001A1CA4
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.IsAsync)
			{
				base.EndWrite(asyncResult);
				return;
			}
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x001A2ACC File Offset: 0x001A1CCC
		private void Init(FileMode mode, FileShare share, string originalPath)
		{
			if (!PathInternal.IsExtended(originalPath))
			{
				int fileType = Interop.Kernel32.GetFileType(this._fileHandle);
				if (fileType != 1)
				{
					int num = (fileType == 0) ? Marshal.GetLastWin32Error() : 0;
					this._fileHandle.Dispose();
					if (num != 0)
					{
						throw Win32Marshal.GetExceptionForWin32Error(num, "");
					}
					throw new NotSupportedException(SR.NotSupported_FileStreamOnNonFiles);
				}
			}
			if (this._useAsyncIO)
			{
				try
				{
					this._fileHandle.ThreadPoolBinding = ThreadPoolBoundHandle.BindHandle(this._fileHandle);
				}
				catch (ArgumentException innerException)
				{
					throw new IOException(SR.IO_BindHandleFailed, innerException);
				}
				finally
				{
					if (this._fileHandle.ThreadPoolBinding == null)
					{
						this._fileHandle.Dispose();
					}
				}
			}
			this._canSeek = true;
			if (mode == FileMode.Append)
			{
				this._appendStart = this.SeekCore(this._fileHandle, 0L, SeekOrigin.End, false);
				return;
			}
			this._appendStart = -1L;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x001A2BB4 File Offset: 0x001A1DB4
		private void InitFromHandle(SafeFileHandle handle, FileAccess access, bool useAsyncIO)
		{
			this.InitFromHandleImpl(handle, useAsyncIO);
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x001A2BC0 File Offset: 0x001A1DC0
		private void InitFromHandleImpl(SafeFileHandle handle, bool useAsyncIO)
		{
			int fileType = Interop.Kernel32.GetFileType(handle);
			this._canSeek = (fileType == 1);
			this._isPipe = (fileType == 3);
			if (useAsyncIO && !handle.IsAsync.GetValueOrDefault())
			{
				try
				{
					handle.ThreadPoolBinding = ThreadPoolBoundHandle.BindHandle(handle);
					goto IL_57;
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(SR.Arg_HandleNotAsync, "handle", innerException);
				}
			}
			if (!useAsyncIO)
			{
				FileStream.VerifyHandleIsSync(handle);
			}
			IL_57:
			if (this._canSeek)
			{
				this.SeekCore(handle, 0L, SeekOrigin.Current, false);
				return;
			}
			this._filePosition = 0L;
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x001A2C54 File Offset: 0x001A1E54
		private static Interop.Kernel32.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES result = default(Interop.Kernel32.SECURITY_ATTRIBUTES);
			if ((share & FileShare.Inheritable) != FileShare.None)
			{
				result = new Interop.Kernel32.SECURITY_ATTRIBUTES
				{
					nLength = (uint)sizeof(Interop.Kernel32.SECURITY_ATTRIBUTES),
					bInheritHandle = Interop.BOOL.TRUE
				};
			}
			return result;
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x001A2C8F File Offset: 0x001A1E8F
		private bool HasActiveBufferOperation
		{
			get
			{
				return !this._activeBufferOperation.IsCompleted;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x001A2C9F File Offset: 0x001A1E9F
		public override bool CanSeek
		{
			get
			{
				return this._canSeek;
			}
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x001A2CA8 File Offset: 0x001A1EA8
		private unsafe long GetLengthInternal()
		{
			Interop.Kernel32.FILE_STANDARD_INFO file_STANDARD_INFO;
			if (!Interop.Kernel32.GetFileInformationByHandleEx(this._fileHandle, 1, (void*)(&file_STANDARD_INFO), (uint)sizeof(Interop.Kernel32.FILE_STANDARD_INFO)))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error(this._path);
			}
			long num = file_STANDARD_INFO.EndOfFile;
			if (this._writePos > 0 && this._filePosition + (long)this._writePos > num)
			{
				num = (long)this._writePos + this._filePosition;
			}
			return num;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x001A2D0C File Offset: 0x001A1F0C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._fileHandle != null && !this._fileHandle.IsClosed && this._writePos > 0)
				{
					try
					{
						this.FlushWriteBuffer(!disposing);
					}
					catch (Exception e) when (FileStream.IsIoRelatedException(e) && !disposing)
					{
					}
				}
			}
			finally
			{
				if (this._fileHandle != null && !this._fileHandle.IsClosed)
				{
					ThreadPoolBoundHandle threadPoolBinding = this._fileHandle.ThreadPoolBinding;
					if (threadPoolBinding != null)
					{
						threadPoolBinding.Dispose();
					}
					this._fileHandle.Dispose();
				}
				PreAllocatedOverlapped preallocatedOverlapped = this._preallocatedOverlapped;
				if (preallocatedOverlapped != null)
				{
					preallocatedOverlapped.Dispose();
				}
				this._canSeek = false;
			}
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x001A2DD4 File Offset: 0x001A1FD4
		public override ValueTask DisposeAsync()
		{
			if (!(base.GetType() == typeof(FileStream)))
			{
				return base.DisposeAsync();
			}
			return this.DisposeAsyncCore();
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x001A2DFC File Offset: 0x001A1FFC
		private ValueTask DisposeAsyncCore()
		{
			FileStream.<DisposeAsyncCore>d__99 <DisposeAsyncCore>d__;
			<DisposeAsyncCore>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<DisposeAsyncCore>d__.<>4__this = this;
			<DisposeAsyncCore>d__.<>1__state = -1;
			<DisposeAsyncCore>d__.<>t__builder.Start<FileStream.<DisposeAsyncCore>d__99>(ref <DisposeAsyncCore>d__);
			return <DisposeAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x001A2E3F File Offset: 0x001A203F
		private void FlushOSBuffer()
		{
			if (!Interop.Kernel32.FlushFileBuffers(this._fileHandle))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error(this._path);
			}
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x001A2E5C File Offset: 0x001A205C
		private Task FlushWriteAsync(CancellationToken cancellationToken)
		{
			if (this._writePos == 0)
			{
				return Task.CompletedTask;
			}
			Task task = this.WriteAsyncInternalCore(new ReadOnlyMemory<byte>(this.GetBuffer(), 0, this._writePos), cancellationToken);
			this._writePos = 0;
			this._activeBufferOperation = (this.HasActiveBufferOperation ? Task.WhenAll(new Task[]
			{
				this._activeBufferOperation,
				task
			}) : task);
			return task;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x001A2EC2 File Offset: 0x001A20C2
		private void FlushWriteBufferForWriteByte()
		{
			this.FlushWriteBuffer(false);
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x001A2ECC File Offset: 0x001A20CC
		private void FlushWriteBuffer(bool calledFromFinalizer = false)
		{
			if (this._writePos == 0)
			{
				return;
			}
			if (this._useAsyncIO)
			{
				Task task = this.FlushWriteAsync(CancellationToken.None);
				if (!calledFromFinalizer)
				{
					task.GetAwaiter().GetResult();
				}
			}
			else
			{
				this.WriteCore(new ReadOnlySpan<byte>(this.GetBuffer(), 0, this._writePos));
			}
			this._writePos = 0;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x001A2F28 File Offset: 0x001A2128
		private void SetLengthInternal(long value)
		{
			if (this._writePos > 0)
			{
				this.FlushWriteBuffer(false);
			}
			else if (this._readPos < this._readLength)
			{
				this.FlushReadBuffer();
			}
			this._readPos = 0;
			this._readLength = 0;
			if (this._appendStart != -1L && value < this._appendStart)
			{
				throw new IOException(SR.IO_SetLengthAppendTruncate);
			}
			this.SetLengthCore(value);
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x001A2F90 File Offset: 0x001A2190
		private void SetLengthCore(long value)
		{
			long filePosition = this._filePosition;
			this.VerifyOSHandlePosition();
			if (this._filePosition != value)
			{
				this.SeekCore(this._fileHandle, value, SeekOrigin.Begin, false);
			}
			if (Interop.Kernel32.SetEndOfFile(this._fileHandle))
			{
				if (filePosition != value)
				{
					if (filePosition < value)
					{
						this.SeekCore(this._fileHandle, filePosition, SeekOrigin.Begin, false);
						return;
					}
					this.SeekCore(this._fileHandle, 0L, SeekOrigin.End, false);
				}
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 87)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_FileLengthTooBig);
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, this._path);
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x001A3022 File Offset: 0x001A2222
		private FileStream.FileStreamCompletionSource CompareExchangeCurrentOverlappedOwner(FileStream.FileStreamCompletionSource newSource, FileStream.FileStreamCompletionSource existingSource)
		{
			return Interlocked.CompareExchange<FileStream.FileStreamCompletionSource>(ref this._currentOverlappedOwner, newSource, existingSource);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x001A3034 File Offset: 0x001A2234
		private int ReadSpan(Span<byte> destination)
		{
			bool flag = false;
			int num = this._readLength - this._readPos;
			if (num == 0)
			{
				if (!this.CanRead)
				{
					throw Error.GetReadNotSupported();
				}
				if (this._writePos > 0)
				{
					this.FlushWriteBuffer(false);
				}
				if (!this.CanSeek || destination.Length >= this._bufferLength)
				{
					num = this.ReadNative(destination);
					this._readPos = 0;
					this._readLength = 0;
					return num;
				}
				num = this.ReadNative(this.GetBuffer());
				if (num == 0)
				{
					return 0;
				}
				flag = (num < this._bufferLength);
				this._readPos = 0;
				this._readLength = num;
			}
			if (num > destination.Length)
			{
				num = destination.Length;
			}
			new ReadOnlySpan<byte>(this.GetBuffer(), this._readPos, num).CopyTo(destination);
			this._readPos += num;
			if (!this._isPipe && num < destination.Length && !flag)
			{
				int num2 = this.ReadNative(destination.Slice(num));
				num += num2;
				this._readPos = 0;
				this._readLength = 0;
			}
			return num;
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x001A3144 File Offset: 0x001A2344
		private int FillReadBufferForReadByte()
		{
			if (!this._useAsyncIO)
			{
				return this.ReadNative(this._buffer);
			}
			return this.ReadNativeAsync(new Memory<byte>(this._buffer), 0, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001A3190 File Offset: 0x001A2390
		private int ReadNative(Span<byte> buffer)
		{
			this.VerifyOSHandlePosition();
			int num2;
			int num = this.ReadFileNative(this._fileHandle, buffer, null, out num2);
			if (num == -1)
			{
				if (num2 == 109)
				{
					num = 0;
				}
				else
				{
					if (num2 == 87)
					{
						throw new ArgumentException(SR.Arg_HandleNotSync, "_fileHandle");
					}
					throw Win32Marshal.GetExceptionForWin32Error(num2, this._path);
				}
			}
			this._filePosition += (long)num;
			return num;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x001A31F4 File Offset: 0x001A23F4
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin < SeekOrigin.Begin || origin > SeekOrigin.End)
			{
				throw new ArgumentException(SR.Argument_InvalidSeekOrigin, "origin");
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			if (!this.CanSeek)
			{
				throw Error.GetSeekNotSupported();
			}
			if (this._writePos > 0)
			{
				this.FlushWriteBuffer(false);
			}
			else if (origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLength - this._readPos);
			}
			this._readPos = (this._readLength = 0);
			this.VerifyOSHandlePosition();
			long num = this._filePosition + (long)(this._readPos - this._readLength);
			long num2 = this.SeekCore(this._fileHandle, offset, origin, false);
			if (this._appendStart != -1L && num2 < this._appendStart)
			{
				this.SeekCore(this._fileHandle, num, SeekOrigin.Begin, false);
				throw new IOException(SR.IO_SeekAppendOverwrite);
			}
			if (this._readLength > 0)
			{
				if (num == num2)
				{
					if (this._readPos > 0)
					{
						Buffer.BlockCopy(this.GetBuffer(), this._readPos, this.GetBuffer(), 0, this._readLength - this._readPos);
						this._readLength -= this._readPos;
						this._readPos = 0;
					}
					if (this._readLength > 0)
					{
						this.SeekCore(this._fileHandle, (long)this._readLength, SeekOrigin.Current, false);
					}
				}
				else if (num - (long)this._readPos < num2 && num2 < num + (long)this._readLength - (long)this._readPos)
				{
					int num3 = (int)(num2 - num);
					Buffer.BlockCopy(this.GetBuffer(), this._readPos + num3, this.GetBuffer(), 0, this._readLength - (this._readPos + num3));
					this._readLength -= this._readPos + num3;
					this._readPos = 0;
					if (this._readLength > 0)
					{
						this.SeekCore(this._fileHandle, (long)this._readLength, SeekOrigin.Current, false);
					}
				}
				else
				{
					this._readPos = 0;
					this._readLength = 0;
				}
			}
			return num2;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x001A33E8 File Offset: 0x001A25E8
		private long SeekCore(SafeFileHandle fileHandle, long offset, SeekOrigin origin, bool closeInvalidHandle = false)
		{
			long num;
			if (Interop.Kernel32.SetFilePointerEx(fileHandle, offset, out num, (uint)origin))
			{
				this._filePosition = num;
				return num;
			}
			if (closeInvalidHandle)
			{
				throw Win32Marshal.GetExceptionForWin32Error(this.GetLastWin32ErrorAndDisposeHandleIfInvalid(), this._path);
			}
			throw Win32Marshal.GetExceptionForLastWin32Error(this._path);
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x001A342C File Offset: 0x001A262C
		private void WriteSpan(ReadOnlySpan<byte> source)
		{
			if (this._writePos == 0)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				if (this._readPos < this._readLength)
				{
					this.FlushReadBuffer();
				}
				this._readPos = 0;
				this._readLength = 0;
			}
			if (this._writePos > 0)
			{
				int num = this._bufferLength - this._writePos;
				if (num > 0)
				{
					if (num >= source.Length)
					{
						source.CopyTo(this.GetBuffer().AsSpan(this._writePos));
						this._writePos += source.Length;
						return;
					}
					source.Slice(0, num).CopyTo(this.GetBuffer().AsSpan(this._writePos));
					this._writePos += num;
					source = source.Slice(num);
				}
				this.WriteCore(new ReadOnlySpan<byte>(this.GetBuffer(), 0, this._writePos));
				this._writePos = 0;
			}
			if (source.Length >= this._bufferLength)
			{
				this.WriteCore(source);
				return;
			}
			if (source.Length == 0)
			{
				return;
			}
			source.CopyTo(this.GetBuffer().AsSpan(this._writePos));
			this._writePos = source.Length;
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x001A3568 File Offset: 0x001A2768
		private void WriteCore(ReadOnlySpan<byte> source)
		{
			this.VerifyOSHandlePosition();
			int num2;
			int num = this.WriteFileNative(this._fileHandle, source, null, out num2);
			if (num == -1)
			{
				if (num2 == 232)
				{
					num = 0;
				}
				else
				{
					if (num2 == 87)
					{
						throw new IOException(SR.IO_FileTooLongOrHandleNotSync);
					}
					throw Win32Marshal.GetExceptionForWin32Error(num2, this._path);
				}
			}
			this._filePosition += (long)num;
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x001A35CC File Offset: 0x001A27CC
		private Task<int> ReadAsyncInternal(Memory<byte> destination, CancellationToken cancellationToken, out int synchronousResult)
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			if (this._isPipe)
			{
				if (this._readPos < this._readLength)
				{
					int num = Math.Min(this._readLength - this._readPos, destination.Length);
					new Span<byte>(this.GetBuffer(), this._readPos, num).CopyTo(destination.Span);
					this._readPos += num;
					synchronousResult = num;
					return null;
				}
				synchronousResult = 0;
				return this.ReadNativeAsync(destination, 0, cancellationToken);
			}
			else
			{
				if (this._writePos > 0)
				{
					this.FlushWriteBuffer(false);
				}
				if (this._readPos == this._readLength)
				{
					if (destination.Length < this._bufferLength)
					{
						Task<int> task = this.ReadNativeAsync(new Memory<byte>(this.GetBuffer()), 0, cancellationToken);
						this._readLength = task.GetAwaiter().GetResult();
						int num2 = Math.Min(this._readLength, destination.Length);
						new Span<byte>(this.GetBuffer(), 0, num2).CopyTo(destination.Span);
						this._readPos = num2;
						synchronousResult = num2;
						return null;
					}
					this._readPos = 0;
					this._readLength = 0;
					synchronousResult = 0;
					return this.ReadNativeAsync(destination, 0, cancellationToken);
				}
				else
				{
					int num3 = Math.Min(this._readLength - this._readPos, destination.Length);
					new Span<byte>(this.GetBuffer(), this._readPos, num3).CopyTo(destination.Span);
					this._readPos += num3;
					if (num3 == destination.Length)
					{
						synchronousResult = num3;
						return null;
					}
					this._readPos = 0;
					this._readLength = 0;
					synchronousResult = 0;
					return this.ReadNativeAsync(destination.Slice(num3), num3, cancellationToken);
				}
			}
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x001A3788 File Offset: 0x001A2988
		private unsafe Task<int> ReadNativeAsync(Memory<byte> destination, int numBufferedBytesRead, CancellationToken cancellationToken)
		{
			FileStream.FileStreamCompletionSource fileStreamCompletionSource = FileStream.FileStreamCompletionSource.Create(this, numBufferedBytesRead, destination);
			NativeOverlapped* overlapped = fileStreamCompletionSource.Overlapped;
			if (this.CanSeek)
			{
				long length = this.Length;
				this.VerifyOSHandlePosition();
				if (this._filePosition + (long)destination.Length > length)
				{
					if (this._filePosition <= length)
					{
						destination = destination.Slice(0, (int)(length - this._filePosition));
					}
					else
					{
						destination = default(Memory<byte>);
					}
				}
				overlapped->OffsetLow = (int)this._filePosition;
				overlapped->OffsetHigh = (int)(this._filePosition >> 32);
				this.SeekCore(this._fileHandle, (long)destination.Length, SeekOrigin.Current, false);
			}
			int num2;
			int num = this.ReadFileNative(this._fileHandle, destination.Span, overlapped, out num2);
			if (num == -1)
			{
				if (num2 == 109)
				{
					overlapped->InternalLow = IntPtr.Zero;
					fileStreamCompletionSource.SetCompletedSynchronously(0);
				}
				else if (num2 != 997)
				{
					if (!this._fileHandle.IsClosed && this.CanSeek)
					{
						this.SeekCore(this._fileHandle, 0L, SeekOrigin.Current, false);
					}
					fileStreamCompletionSource.ReleaseNativeResource();
					if (num2 == 38)
					{
						throw Error.GetEndOfFile();
					}
					throw Win32Marshal.GetExceptionForWin32Error(num2, this._path);
				}
				else if (cancellationToken.CanBeCanceled)
				{
					fileStreamCompletionSource.RegisterForCancellation(cancellationToken);
				}
			}
			return fileStreamCompletionSource.Task;
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x001A38C8 File Offset: 0x001A2AC8
		private ValueTask WriteAsyncInternal(ReadOnlyMemory<byte> source, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			bool flag = false;
			if (!this._isPipe)
			{
				if (this._writePos == 0)
				{
					if (this._readPos < this._readLength)
					{
						this.FlushReadBuffer();
					}
					this._readPos = 0;
					this._readLength = 0;
				}
				int num = this._bufferLength - this._writePos;
				if (source.Length < this._bufferLength && !this.HasActiveBufferOperation && source.Length <= num)
				{
					source.Span.CopyTo(new Span<byte>(this.GetBuffer(), this._writePos, source.Length));
					this._writePos += source.Length;
					flag = true;
					if (source.Length != num)
					{
						return default(ValueTask);
					}
				}
			}
			Task task = null;
			if (this._writePos > 0)
			{
				task = this.FlushWriteAsync(cancellationToken);
				if (flag || task.IsFaulted || task.IsCanceled)
				{
					return new ValueTask(task);
				}
			}
			Task task2 = this.WriteAsyncInternalCore(source, cancellationToken);
			return new ValueTask((task == null || task.Status == TaskStatus.RanToCompletion) ? task2 : ((task2.Status == TaskStatus.RanToCompletion) ? task : Task.WhenAll(new Task[]
			{
				task,
				task2
			})));
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x001A3A08 File Offset: 0x001A2C08
		private unsafe Task WriteAsyncInternalCore(ReadOnlyMemory<byte> source, CancellationToken cancellationToken)
		{
			FileStream.FileStreamCompletionSource fileStreamCompletionSource = FileStream.FileStreamCompletionSource.Create(this, 0, source);
			NativeOverlapped* overlapped = fileStreamCompletionSource.Overlapped;
			if (this.CanSeek)
			{
				long length = this.Length;
				this.VerifyOSHandlePosition();
				if (this._filePosition + (long)source.Length > length)
				{
					this.SetLengthCore(this._filePosition + (long)source.Length);
				}
				overlapped->OffsetLow = (int)this._filePosition;
				overlapped->OffsetHigh = (int)(this._filePosition >> 32);
				this.SeekCore(this._fileHandle, (long)source.Length, SeekOrigin.Current, false);
			}
			int num2;
			int num = this.WriteFileNative(this._fileHandle, source.Span, overlapped, out num2);
			if (num == -1)
			{
				if (num2 == 232)
				{
					fileStreamCompletionSource.SetCompletedSynchronously(0);
					return Task.CompletedTask;
				}
				if (num2 != 997)
				{
					if (!this._fileHandle.IsClosed && this.CanSeek)
					{
						this.SeekCore(this._fileHandle, 0L, SeekOrigin.Current, false);
					}
					fileStreamCompletionSource.ReleaseNativeResource();
					if (num2 == 38)
					{
						throw Error.GetEndOfFile();
					}
					throw Win32Marshal.GetExceptionForWin32Error(num2, this._path);
				}
				else if (cancellationToken.CanBeCanceled)
				{
					fileStreamCompletionSource.RegisterForCancellation(cancellationToken);
				}
			}
			return fileStreamCompletionSource.Task;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x001A3B2C File Offset: 0x001A2D2C
		private unsafe int ReadFileNative(SafeFileHandle handle, Span<byte> bytes, NativeOverlapped* overlapped, out int errorCode)
		{
			int result = 0;
			int num;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* bytes2 = reference;
				num = (this._useAsyncIO ? Interop.Kernel32.ReadFile(handle, bytes2, bytes.Length, IntPtr.Zero, overlapped) : Interop.Kernel32.ReadFile(handle, bytes2, bytes.Length, out result, IntPtr.Zero));
			}
			if (num == 0)
			{
				errorCode = this.GetLastWin32ErrorAndDisposeHandleIfInvalid();
				return -1;
			}
			errorCode = 0;
			return result;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x001A3B90 File Offset: 0x001A2D90
		private unsafe int WriteFileNative(SafeFileHandle handle, ReadOnlySpan<byte> buffer, NativeOverlapped* overlapped, out int errorCode)
		{
			int result = 0;
			int num;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* bytes = reference;
				num = (this._useAsyncIO ? Interop.Kernel32.WriteFile(handle, bytes, buffer.Length, IntPtr.Zero, overlapped) : Interop.Kernel32.WriteFile(handle, bytes, buffer.Length, out result, IntPtr.Zero));
			}
			if (num == 0)
			{
				errorCode = this.GetLastWin32ErrorAndDisposeHandleIfInvalid();
				return -1;
			}
			errorCode = 0;
			return result;
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x001A3BF4 File Offset: 0x001A2DF4
		private int GetLastWin32ErrorAndDisposeHandleIfInvalid()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 6)
			{
				this._fileHandle.Dispose();
			}
			return lastWin32Error;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x001A3C18 File Offset: 0x001A2E18
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (!this._useAsyncIO || base.GetType() != typeof(FileStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			return this.AsyncModeCopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x001A3C84 File Offset: 0x001A2E84
		private Task AsyncModeCopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			FileStream.<AsyncModeCopyToAsync>d__128 <AsyncModeCopyToAsync>d__;
			<AsyncModeCopyToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AsyncModeCopyToAsync>d__.<>4__this = this;
			<AsyncModeCopyToAsync>d__.destination = destination;
			<AsyncModeCopyToAsync>d__.bufferSize = bufferSize;
			<AsyncModeCopyToAsync>d__.cancellationToken = cancellationToken;
			<AsyncModeCopyToAsync>d__.<>1__state = -1;
			<AsyncModeCopyToAsync>d__.<>t__builder.Start<FileStream.<AsyncModeCopyToAsync>d__128>(ref <AsyncModeCopyToAsync>d__);
			return <AsyncModeCopyToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x001A3CE0 File Offset: 0x001A2EE0
		private Task FlushAsyncInternal(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (this._fileHandle.IsClosed)
			{
				throw Error.GetFileNotOpen();
			}
			try
			{
				this.FlushInternalBuffer();
			}
			catch (Exception exception)
			{
				return Task.FromException(exception);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x001A3D3C File Offset: 0x001A2F3C
		private void LockInternal(long position, long length)
		{
			int offsetLow = (int)position;
			int offsetHigh = (int)(position >> 32);
			int countLow = (int)length;
			int countHigh = (int)(length >> 32);
			if (!Interop.Kernel32.LockFile(this._fileHandle, offsetLow, offsetHigh, countLow, countHigh))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error(this._path);
			}
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x001A3D78 File Offset: 0x001A2F78
		private void UnlockInternal(long position, long length)
		{
			int offsetLow = (int)position;
			int offsetHigh = (int)(position >> 32);
			int countLow = (int)length;
			int countHigh = (int)(length >> 32);
			if (!Interop.Kernel32.UnlockFile(this._fileHandle, offsetLow, offsetHigh, countLow, countHigh))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error(this._path);
			}
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x001A3DB4 File Offset: 0x001A2FB4
		private SafeFileHandle ValidateFileHandle(SafeFileHandle fileHandle)
		{
			if (fileHandle.IsInvalid)
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 3 && this._path.Length == PathInternal.GetRootLength(this._path))
				{
					num = 5;
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, this._path);
			}
			fileHandle.IsAsync = new bool?(this._useAsyncIO);
			return fileHandle;
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x001A3E11 File Offset: 0x001A3011
		private SafeFileHandle OpenHandle(FileMode mode, FileShare share, FileOptions options)
		{
			return this.CreateFileOpenHandle(mode, share, options);
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x001A3E1C File Offset: 0x001A301C
		private unsafe SafeFileHandle CreateFileOpenHandle(FileMode mode, FileShare share, FileOptions options)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
			int dwDesiredAccess = (((this._access & FileAccess.Read) == FileAccess.Read) ? int.MinValue : 0) | (((this._access & FileAccess.Write) == FileAccess.Write) ? 1073741824 : 0);
			share &= ~FileShare.Inheritable;
			if (mode == FileMode.Append)
			{
				mode = FileMode.OpenOrCreate;
			}
			int dwFlagsAndAttributes = (int)(options | (FileOptions)1048576);
			SafeFileHandle result;
			using (DisableMediaInsertionPrompt.Create())
			{
				result = this.ValidateFileHandle(Interop.Kernel32.CreateFile(this._path, dwDesiredAccess, share, &secAttrs, mode, dwFlagsAndAttributes, IntPtr.Zero));
			}
			return result;
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x001A3EB8 File Offset: 0x001A30B8
		private static bool GetDefaultIsAsync(SafeFileHandle handle)
		{
			bool? isAsync = handle.IsAsync;
			if (isAsync == null)
			{
				return (!FileStream.IsHandleSynchronous(handle, true)).GetValueOrDefault();
			}
			return isAsync.GetValueOrDefault();
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x001A3F10 File Offset: 0x001A3110
		private unsafe static bool? IsHandleSynchronous(SafeFileHandle fileHandle, bool ignoreInvalid)
		{
			if (fileHandle.IsInvalid)
			{
				return null;
			}
			Interop.NtDll.IO_STATUS_BLOCK io_STATUS_BLOCK;
			uint num2;
			int num = Interop.NtDll.NtQueryInformationFile(fileHandle, out io_STATUS_BLOCK, (void*)(&num2), 4U, 16U);
			if (num != -1073741816)
			{
				if (num != 0)
				{
					return null;
				}
				return new bool?((num2 & 48U) > 0U);
			}
			else
			{
				if (!ignoreInvalid)
				{
					throw Win32Marshal.GetExceptionForWin32Error(6, "");
				}
				return null;
			}
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x001A3F7C File Offset: 0x001A317C
		private static void VerifyHandleIsSync(SafeFileHandle handle)
		{
			if (handle.IsAsync == null)
			{
				return;
			}
			if (!(FileStream.IsHandleSynchronous(handle, false) ?? true))
			{
				throw new ArgumentException(SR.Arg_HandleNotSync, "handle");
			}
		}

		// Token: 0x04001828 RID: 6184
		private byte[] _buffer;

		// Token: 0x04001829 RID: 6185
		private int _bufferLength;

		// Token: 0x0400182A RID: 6186
		private readonly SafeFileHandle _fileHandle;

		// Token: 0x0400182B RID: 6187
		private readonly FileAccess _access;

		// Token: 0x0400182C RID: 6188
		private readonly string _path;

		// Token: 0x0400182D RID: 6189
		private int _readPos;

		// Token: 0x0400182E RID: 6190
		private int _readLength;

		// Token: 0x0400182F RID: 6191
		private int _writePos;

		// Token: 0x04001830 RID: 6192
		private readonly bool _useAsyncIO;

		// Token: 0x04001831 RID: 6193
		private Task<int> _lastSynchronouslyCompletedTask;

		// Token: 0x04001832 RID: 6194
		private long _filePosition;

		// Token: 0x04001833 RID: 6195
		private bool _exposedHandle;

		// Token: 0x04001834 RID: 6196
		private static int s_cachedSerializationSwitch;

		// Token: 0x04001835 RID: 6197
		private bool _canSeek;

		// Token: 0x04001836 RID: 6198
		private bool _isPipe;

		// Token: 0x04001837 RID: 6199
		private long _appendStart;

		// Token: 0x04001838 RID: 6200
		private static readonly IOCompletionCallback s_ioCallback = new IOCompletionCallback(FileStream.FileStreamCompletionSource.IOCallback);

		// Token: 0x04001839 RID: 6201
		private Task _activeBufferOperation;

		// Token: 0x0400183A RID: 6202
		private PreAllocatedOverlapped _preallocatedOverlapped;

		// Token: 0x0400183B RID: 6203
		private FileStream.FileStreamCompletionSource _currentOverlappedOwner;

		// Token: 0x02000691 RID: 1681
		private sealed class AsyncCopyToAwaitable : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17000E2E RID: 3630
			// (get) Token: 0x0600559E RID: 21918 RVA: 0x000AC098 File Offset: 0x000AB298
			internal object CancellationLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600559F RID: 21919 RVA: 0x001A3FDA File Offset: 0x001A31DA
			internal AsyncCopyToAwaitable(FileStream fileStream)
			{
				this._fileStream = fileStream;
			}

			// Token: 0x060055A0 RID: 21920 RVA: 0x001A3FE9 File Offset: 0x001A31E9
			internal void ResetForNextOperation()
			{
				this._continuation = null;
				this._errorCode = 0U;
				this._numBytes = 0U;
			}

			// Token: 0x060055A1 RID: 21921 RVA: 0x001A4000 File Offset: 0x001A3200
			internal unsafe static void IOCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
			{
				FileStream.AsyncCopyToAwaitable asyncCopyToAwaitable = (FileStream.AsyncCopyToAwaitable)ThreadPoolBoundHandle.GetNativeOverlappedState(pOVERLAP);
				asyncCopyToAwaitable._errorCode = errorCode;
				asyncCopyToAwaitable._numBytes = numBytes;
				Action action = asyncCopyToAwaitable._continuation ?? Interlocked.CompareExchange<Action>(ref asyncCopyToAwaitable._continuation, FileStream.AsyncCopyToAwaitable.s_sentinel, null);
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x060055A2 RID: 21922 RVA: 0x001A404C File Offset: 0x001A324C
			internal void MarkCompleted()
			{
				this._continuation = FileStream.AsyncCopyToAwaitable.s_sentinel;
			}

			// Token: 0x060055A3 RID: 21923 RVA: 0x000AC098 File Offset: 0x000AB298
			public FileStream.AsyncCopyToAwaitable GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000E2F RID: 3631
			// (get) Token: 0x060055A4 RID: 21924 RVA: 0x001A4059 File Offset: 0x001A3259
			public bool IsCompleted
			{
				get
				{
					return this._continuation == FileStream.AsyncCopyToAwaitable.s_sentinel;
				}
			}

			// Token: 0x060055A5 RID: 21925 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void GetResult()
			{
			}

			// Token: 0x060055A6 RID: 21926 RVA: 0x001A4068 File Offset: 0x001A3268
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x060055A7 RID: 21927 RVA: 0x001A4071 File Offset: 0x001A3271
			public void UnsafeOnCompleted(Action continuation)
			{
				if (this._continuation == FileStream.AsyncCopyToAwaitable.s_sentinel || Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null) != null)
				{
					Task.Run(continuation);
				}
			}

			// Token: 0x0400183C RID: 6204
			private static readonly Action s_sentinel = delegate()
			{
			};

			// Token: 0x0400183D RID: 6205
			internal static readonly IOCompletionCallback s_callback = new IOCompletionCallback(FileStream.AsyncCopyToAwaitable.IOCallback);

			// Token: 0x0400183E RID: 6206
			internal readonly FileStream _fileStream;

			// Token: 0x0400183F RID: 6207
			internal long _position;

			// Token: 0x04001840 RID: 6208
			internal unsafe NativeOverlapped* _nativeOverlapped;

			// Token: 0x04001841 RID: 6209
			internal Action _continuation;

			// Token: 0x04001842 RID: 6210
			internal uint _errorCode;

			// Token: 0x04001843 RID: 6211
			internal uint _numBytes;
		}

		// Token: 0x02000693 RID: 1683
		private class FileStreamCompletionSource : TaskCompletionSource<int>
		{
			// Token: 0x060055AC RID: 21932 RVA: 0x001A40CC File Offset: 0x001A32CC
			protected FileStreamCompletionSource(FileStream stream, int numBufferedBytes, byte[] bytes) : base(TaskCreationOptions.RunContinuationsAsynchronously)
			{
				this._numBufferedBytes = numBufferedBytes;
				this._stream = stream;
				this._result = 0L;
				this._overlapped = ((bytes != null && this._stream.CompareExchangeCurrentOverlappedOwner(this, null) == null) ? this._stream._fileHandle.ThreadPoolBinding.AllocateNativeOverlapped(this._stream._preallocatedOverlapped) : this._stream._fileHandle.ThreadPoolBinding.AllocateNativeOverlapped(FileStream.s_ioCallback, this, bytes));
			}

			// Token: 0x17000E30 RID: 3632
			// (get) Token: 0x060055AD RID: 21933 RVA: 0x001A414D File Offset: 0x001A334D
			internal unsafe NativeOverlapped* Overlapped
			{
				get
				{
					return this._overlapped;
				}
			}

			// Token: 0x060055AE RID: 21934 RVA: 0x001A4155 File Offset: 0x001A3355
			public void SetCompletedSynchronously(int numBytes)
			{
				this.ReleaseNativeResource();
				base.TrySetResult(numBytes + this._numBufferedBytes);
			}

			// Token: 0x060055AF RID: 21935 RVA: 0x001A416C File Offset: 0x001A336C
			public void RegisterForCancellation(CancellationToken cancellationToken)
			{
				if (this._overlapped != null)
				{
					Action<object> action;
					if ((action = FileStream.FileStreamCompletionSource.s_cancelCallback) == null)
					{
						action = (FileStream.FileStreamCompletionSource.s_cancelCallback = new Action<object>(FileStream.FileStreamCompletionSource.Cancel));
					}
					Action<object> callback = action;
					long num = Interlocked.CompareExchange(ref this._result, 17179869184L, 0L);
					if (num == 0L)
					{
						this._cancellationRegistration = cancellationToken.UnsafeRegister(callback, this);
						num = Interlocked.Exchange(ref this._result, 0L);
					}
					else if (num != 34359738368L)
					{
						num = Interlocked.Exchange(ref this._result, 0L);
					}
					if (num != 0L && num != 34359738368L && num != 17179869184L)
					{
						this.CompleteCallback((ulong)num);
					}
				}
			}

			// Token: 0x060055B0 RID: 21936 RVA: 0x001A4218 File Offset: 0x001A3418
			internal virtual void ReleaseNativeResource()
			{
				this._cancellationRegistration.Dispose();
				if (this._overlapped != null)
				{
					this._stream._fileHandle.ThreadPoolBinding.FreeNativeOverlapped(this._overlapped);
					this._overlapped = null;
				}
				this._stream.CompareExchangeCurrentOverlappedOwner(null, this);
			}

			// Token: 0x060055B1 RID: 21937 RVA: 0x001A426C File Offset: 0x001A346C
			internal unsafe static void IOCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
			{
				object nativeOverlappedState = ThreadPoolBoundHandle.GetNativeOverlappedState(pOverlapped);
				FileStream fileStream = nativeOverlappedState as FileStream;
				FileStream.FileStreamCompletionSource fileStreamCompletionSource = (fileStream != null) ? fileStream._currentOverlappedOwner : ((FileStream.FileStreamCompletionSource)nativeOverlappedState);
				ulong num;
				if (errorCode != 0U && errorCode != 109U && errorCode != 232U)
				{
					num = (8589934592UL | (ulong)errorCode);
				}
				else
				{
					num = (4294967296UL | (ulong)numBytes);
				}
				if (Interlocked.Exchange(ref fileStreamCompletionSource._result, (long)num) == 0L && Interlocked.Exchange(ref fileStreamCompletionSource._result, 34359738368L) != 0L)
				{
					fileStreamCompletionSource.CompleteCallback(num);
				}
			}

			// Token: 0x060055B2 RID: 21938 RVA: 0x001A42F0 File Offset: 0x001A34F0
			private void CompleteCallback(ulong packedResult)
			{
				CancellationToken token = this._cancellationRegistration.Token;
				this.ReleaseNativeResource();
				long num = (long)(packedResult & 18446744069414584320UL);
				if (num != 8589934592L)
				{
					base.TrySetResult((int)(packedResult & (ulong)-1) + this._numBufferedBytes);
					return;
				}
				int num2 = (int)(packedResult & (ulong)-1);
				if (num2 == 995)
				{
					base.TrySetCanceled(token.IsCancellationRequested ? token : new CancellationToken(true));
					return;
				}
				Exception exceptionForWin32Error = Win32Marshal.GetExceptionForWin32Error(num2, "");
				exceptionForWin32Error.SetCurrentStackTrace();
				base.TrySetException(exceptionForWin32Error);
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x001A4380 File Offset: 0x001A3580
			private static void Cancel(object state)
			{
				FileStream.FileStreamCompletionSource fileStreamCompletionSource = (FileStream.FileStreamCompletionSource)state;
				if (!fileStreamCompletionSource._stream._fileHandle.IsInvalid && !Interop.Kernel32.CancelIoEx(fileStreamCompletionSource._stream._fileHandle, fileStreamCompletionSource._overlapped))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 1168)
					{
						throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
					}
				}
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x001A43D8 File Offset: 0x001A35D8
			public static FileStream.FileStreamCompletionSource Create(FileStream stream, int numBufferedBytesRead, ReadOnlyMemory<byte> memory)
			{
				ArraySegment<byte> arraySegment;
				if (!MemoryMarshal.TryGetArray<byte>(memory, out arraySegment) || arraySegment.Array != stream._buffer)
				{
					return new FileStream.MemoryFileStreamCompletionSource(stream, numBufferedBytesRead, memory);
				}
				return new FileStream.FileStreamCompletionSource(stream, numBufferedBytesRead, arraySegment.Array);
			}

			// Token: 0x04001845 RID: 6213
			private static Action<object> s_cancelCallback;

			// Token: 0x04001846 RID: 6214
			private readonly FileStream _stream;

			// Token: 0x04001847 RID: 6215
			private readonly int _numBufferedBytes;

			// Token: 0x04001848 RID: 6216
			private CancellationTokenRegistration _cancellationRegistration;

			// Token: 0x04001849 RID: 6217
			private unsafe NativeOverlapped* _overlapped;

			// Token: 0x0400184A RID: 6218
			private long _result;
		}

		// Token: 0x02000694 RID: 1684
		private sealed class MemoryFileStreamCompletionSource : FileStream.FileStreamCompletionSource
		{
			// Token: 0x060055B5 RID: 21941 RVA: 0x001A4415 File Offset: 0x001A3615
			internal MemoryFileStreamCompletionSource(FileStream stream, int numBufferedBytes, ReadOnlyMemory<byte> memory) : base(stream, numBufferedBytes, null)
			{
				this._handle = memory.Pin();
			}

			// Token: 0x060055B6 RID: 21942 RVA: 0x001A442D File Offset: 0x001A362D
			internal override void ReleaseNativeResource()
			{
				this._handle.Dispose();
				base.ReleaseNativeResource();
			}

			// Token: 0x0400184B RID: 6219
			private MemoryHandle _handle;
		}
	}
}
