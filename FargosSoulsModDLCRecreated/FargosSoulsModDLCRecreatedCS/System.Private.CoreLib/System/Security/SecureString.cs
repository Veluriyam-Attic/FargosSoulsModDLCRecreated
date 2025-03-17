using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;

namespace System.Security
{
	// Token: 0x020003B9 RID: 953
	public sealed class SecureString : IDisposable
	{
		// Token: 0x0600313B RID: 12603 RVA: 0x001686FB File Offset: 0x001678FB
		public SecureString()
		{
			this.Initialize(ReadOnlySpan<char>.Empty);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0016871C File Offset: 0x0016791C
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Length);
			}
			this.Initialize(new ReadOnlySpan<char>((void*)value, length));
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00168784 File Offset: 0x00167984
		private void Initialize(ReadOnlySpan<char> value)
		{
			this._buffer = SecureString.UnmanagedBuffer.Allocate(SecureString.GetAlignedByteSize(value.Length));
			this._decryptedLength = value.Length;
			SafeBuffer safeBuffer = null;
			try
			{
				Span<char> destination = this.AcquireSpan(ref safeBuffer);
				value.CopyTo(destination);
			}
			finally
			{
				this.ProtectMemory();
				if (safeBuffer != null)
				{
					safeBuffer.DangerousRelease();
				}
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x001687EC File Offset: 0x001679EC
		private SecureString(SecureString str)
		{
			this._buffer = SecureString.UnmanagedBuffer.Allocate((int)str._buffer.ByteLength);
			SecureString.UnmanagedBuffer.Copy(str._buffer, this._buffer, str._buffer.ByteLength);
			this._decryptedLength = str._decryptedLength;
			this._encrypted = str._encrypted;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x00168855 File Offset: 0x00167A55
		public int Length
		{
			get
			{
				this.EnsureNotDisposed();
				return Volatile.Read(ref this._decryptedLength);
			}
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x00168868 File Offset: 0x00167A68
		private void EnsureCapacity(int capacity)
		{
			if (capacity > 65536)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_Capacity);
			}
			if ((ulong)(capacity * 2) <= this._buffer.ByteLength)
			{
				return;
			}
			SecureString.UnmanagedBuffer buffer = this._buffer;
			SecureString.UnmanagedBuffer unmanagedBuffer = SecureString.UnmanagedBuffer.Allocate(SecureString.GetAlignedByteSize(capacity));
			SecureString.UnmanagedBuffer.Copy(buffer, unmanagedBuffer, (ulong)(this._decryptedLength * 2));
			this._buffer = unmanagedBuffer;
			buffer.Dispose();
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x001688D0 File Offset: 0x00167AD0
		public unsafe void AppendChar(char c)
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				this.EnsureNotDisposed();
				this.EnsureNotReadOnly();
				SafeBuffer safeBuffer = null;
				try
				{
					this.UnprotectMemory();
					this.EnsureCapacity(this._decryptedLength + 1);
					*this.AcquireSpan(ref safeBuffer)[this._decryptedLength] = c;
					this._decryptedLength++;
				}
				finally
				{
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x00168970 File Offset: 0x00167B70
		public void Clear()
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				this.EnsureNotDisposed();
				this.EnsureNotReadOnly();
				this._decryptedLength = 0;
				SafeBuffer safeBuffer = null;
				try
				{
					this.AcquireSpan(ref safeBuffer).Clear();
				}
				finally
				{
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x001689E8 File Offset: 0x00167BE8
		[NullableContext(1)]
		public SecureString Copy()
		{
			object methodLock = this._methodLock;
			SecureString result;
			lock (methodLock)
			{
				this.EnsureNotDisposed();
				result = new SecureString(this);
			}
			return result;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00168A30 File Offset: 0x00167C30
		public void Dispose()
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				if (this._buffer != null)
				{
					this._buffer.Dispose();
					this._buffer = null;
				}
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x00168A84 File Offset: 0x00167C84
		public unsafe void InsertAt(int index, char c)
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				if (index < 0 || index > this._decryptedLength)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_IndexString);
				}
				this.EnsureNotDisposed();
				this.EnsureNotReadOnly();
				SafeBuffer safeBuffer = null;
				try
				{
					this.UnprotectMemory();
					this.EnsureCapacity(this._decryptedLength + 1);
					Span<char> span = this.AcquireSpan(ref safeBuffer);
					span.Slice(index, this._decryptedLength - index).CopyTo(span.Slice(index + 1));
					*span[index] = c;
					this._decryptedLength++;
				}
				finally
				{
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00168B60 File Offset: 0x00167D60
		public bool IsReadOnly()
		{
			this.EnsureNotDisposed();
			return Volatile.Read(ref this._readOnly);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00168B73 File Offset: 0x00167D73
		public void MakeReadOnly()
		{
			this.EnsureNotDisposed();
			Volatile.Write(ref this._readOnly, true);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00168B88 File Offset: 0x00167D88
		public void RemoveAt(int index)
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				if (index < 0 || index >= this._decryptedLength)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_IndexString);
				}
				this.EnsureNotDisposed();
				this.EnsureNotReadOnly();
				SafeBuffer safeBuffer = null;
				try
				{
					this.UnprotectMemory();
					Span<char> span = this.AcquireSpan(ref safeBuffer);
					span.Slice(index + 1, this._decryptedLength - (index + 1)).CopyTo(span.Slice(index));
					this._decryptedLength--;
				}
				finally
				{
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x00168C4C File Offset: 0x00167E4C
		public unsafe void SetAt(int index, char c)
		{
			object methodLock = this._methodLock;
			lock (methodLock)
			{
				if (index < 0 || index >= this._decryptedLength)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_IndexString);
				}
				this.EnsureNotDisposed();
				this.EnsureNotReadOnly();
				SafeBuffer safeBuffer = null;
				try
				{
					this.UnprotectMemory();
					*this.AcquireSpan(ref safeBuffer)[index] = c;
				}
				finally
				{
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x00168CE8 File Offset: 0x00167EE8
		private unsafe Span<char> AcquireSpan(ref SafeBuffer bufferToRelease)
		{
			SafeBuffer buffer = this._buffer;
			bool flag = false;
			buffer.DangerousAddRef(ref flag);
			bufferToRelease = buffer;
			return new Span<char>((void*)buffer.DangerousGetHandle(), (int)(buffer.ByteLength / 2UL));
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x00168D23 File Offset: 0x00167F23
		private void EnsureNotReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x00168D38 File Offset: 0x00167F38
		private void EnsureNotDisposed()
		{
			if (this._buffer == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00168D54 File Offset: 0x00167F54
		internal unsafe IntPtr MarshalToBSTR()
		{
			object methodLock = this._methodLock;
			IntPtr result;
			lock (methodLock)
			{
				this.EnsureNotDisposed();
				this.UnprotectMemory();
				SafeBuffer safeBuffer = null;
				IntPtr intPtr = IntPtr.Zero;
				int length = 0;
				try
				{
					Span<char> span = this.AcquireSpan(ref safeBuffer);
					length = this._decryptedLength;
					intPtr = Marshal.AllocBSTR(length);
					span.Slice(0, length).CopyTo(new Span<char>((void*)intPtr, length));
					IntPtr intPtr2 = intPtr;
					intPtr = IntPtr.Zero;
					result = intPtr2;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						new Span<char>((void*)intPtr, length).Clear();
						Marshal.FreeBSTR(intPtr);
					}
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
			return result;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00168E34 File Offset: 0x00168034
		internal unsafe IntPtr MarshalToString(bool globalAlloc, bool unicode)
		{
			object methodLock = this._methodLock;
			IntPtr result;
			lock (methodLock)
			{
				this.EnsureNotDisposed();
				this.UnprotectMemory();
				SafeBuffer safeBuffer = null;
				IntPtr intPtr = IntPtr.Zero;
				int num = 0;
				try
				{
					Span<char> span = this.AcquireSpan(ref safeBuffer).Slice(0, this._decryptedLength);
					if (unicode)
					{
						num = (span.Length + 1) * 2;
					}
					else
					{
						num = Marshal.GetAnsiStringByteCount(span);
					}
					if (globalAlloc)
					{
						intPtr = Marshal.AllocHGlobal(num);
					}
					else
					{
						intPtr = Marshal.AllocCoTaskMem(num);
					}
					if (unicode)
					{
						Span<char> destination = new Span<char>((void*)intPtr, num / 2);
						span.CopyTo(destination);
						*destination[destination.Length - 1] = '\0';
					}
					else
					{
						Marshal.GetAnsiStringBytes(span, new Span<byte>((void*)intPtr, num));
					}
					IntPtr intPtr2 = intPtr;
					intPtr = IntPtr.Zero;
					result = intPtr2;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						new Span<byte>((void*)intPtr, num).Clear();
						if (globalAlloc)
						{
							Marshal.FreeHGlobal(intPtr);
						}
						else
						{
							Marshal.FreeCoTaskMem(intPtr);
						}
					}
					this.ProtectMemory();
					if (safeBuffer != null)
					{
						safeBuffer.DangerousRelease();
					}
				}
			}
			return result;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x00168F98 File Offset: 0x00168198
		private static int GetAlignedByteSize(int length)
		{
			int num = Math.Max(length, 1) * 2;
			return (num + 15) / 16 * 16;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00168FB9 File Offset: 0x001681B9
		private void ProtectMemory()
		{
			if (this._decryptedLength != 0 && !this._encrypted && !Interop.Crypt32.CryptProtectMemory(this._buffer, (uint)this._buffer.ByteLength, 0U))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			this._encrypted = true;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00168FF7 File Offset: 0x001681F7
		private void UnprotectMemory()
		{
			if (this._decryptedLength != 0 && this._encrypted && !Interop.Crypt32.CryptUnprotectMemory(this._buffer, (uint)this._buffer.ByteLength, 0U))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			this._encrypted = false;
		}

		// Token: 0x04000D7E RID: 3454
		private readonly object _methodLock = new object();

		// Token: 0x04000D7F RID: 3455
		private SecureString.UnmanagedBuffer _buffer;

		// Token: 0x04000D80 RID: 3456
		private int _decryptedLength;

		// Token: 0x04000D81 RID: 3457
		private bool _encrypted;

		// Token: 0x04000D82 RID: 3458
		private bool _readOnly;

		// Token: 0x020003BA RID: 954
		private sealed class UnmanagedBuffer : SafeBuffer
		{
			// Token: 0x06003152 RID: 12626 RVA: 0x00169035 File Offset: 0x00168235
			private UnmanagedBuffer() : base(true)
			{
			}

			// Token: 0x06003153 RID: 12627 RVA: 0x00169040 File Offset: 0x00168240
			public static SecureString.UnmanagedBuffer Allocate(int byteLength)
			{
				SecureString.UnmanagedBuffer unmanagedBuffer = new SecureString.UnmanagedBuffer();
				unmanagedBuffer.SetHandle(Marshal.AllocHGlobal(byteLength));
				unmanagedBuffer.Initialize((ulong)((long)byteLength));
				unmanagedBuffer._byteLength = byteLength;
				return unmanagedBuffer;
			}

			// Token: 0x06003154 RID: 12628 RVA: 0x00169070 File Offset: 0x00168270
			internal unsafe static void Copy(SecureString.UnmanagedBuffer source, SecureString.UnmanagedBuffer destination, ulong bytesLength)
			{
				if (bytesLength == 0UL)
				{
					return;
				}
				byte* ptr = null;
				byte* ptr2 = null;
				try
				{
					source.AcquirePointer(ref ptr);
					destination.AcquirePointer(ref ptr2);
					Buffer.MemoryCopy((void*)ptr, (void*)ptr2, destination.ByteLength, bytesLength);
				}
				finally
				{
					if (ptr2 != null)
					{
						destination.ReleasePointer();
					}
					if (ptr != null)
					{
						source.ReleasePointer();
					}
				}
			}

			// Token: 0x06003155 RID: 12629 RVA: 0x001690D0 File Offset: 0x001682D0
			protected unsafe override bool ReleaseHandle()
			{
				new Span<byte>((void*)this.handle, this._byteLength).Clear();
				Marshal.FreeHGlobal(this.handle);
				return true;
			}

			// Token: 0x04000D83 RID: 3459
			private int _byteLength;
		}
	}
}
