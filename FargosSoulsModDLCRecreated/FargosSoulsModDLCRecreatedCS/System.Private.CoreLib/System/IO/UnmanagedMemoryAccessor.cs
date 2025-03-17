using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x020006BC RID: 1724
	public class UnmanagedMemoryAccessor : IDisposable
	{
		// Token: 0x060057C9 RID: 22473 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected UnmanagedMemoryAccessor()
		{
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001ADD02 File Offset: 0x001ACF02
		[NullableContext(1)]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
		{
			this.Initialize(buffer, offset, capacity, FileAccess.Read);
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x001ADD14 File Offset: 0x001ACF14
		[NullableContext(1)]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			this.Initialize(buffer, offset, capacity, access);
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x001ADD28 File Offset: 0x001ACF28
		[NullableContext(1)]
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.ByteLength < (ulong)(offset + capacity))
			{
				throw new ArgumentException(SR.Argument_OffsetAndCapacityOutOfBounds);
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
				if (ptr + offset + capacity < ptr)
				{
					throw new ArgumentException(SR.Argument_UnmanagedMemAccessorWrapAround);
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
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
			this._canRead = ((this._access & FileAccess.Read) > (FileAccess)0);
			this._canWrite = ((this._access & FileAccess.Write) > (FileAccess)0);
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060057CD RID: 22477 RVA: 0x001ADE30 File Offset: 0x001AD030
		public long Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060057CE RID: 22478 RVA: 0x001ADE38 File Offset: 0x001AD038
		public bool CanRead
		{
			get
			{
				return this._isOpen && this._canRead;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060057CF RID: 22479 RVA: 0x001ADE4A File Offset: 0x001AD04A
		public bool CanWrite
		{
			get
			{
				return this._isOpen && this._canWrite;
			}
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001ADE5C File Offset: 0x001AD05C
		protected virtual void Dispose(bool disposing)
		{
			this._isOpen = false;
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001ADE65 File Offset: 0x001AD065
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x001ADE74 File Offset: 0x001AD074
		protected bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x001ADE7C File Offset: 0x001AD07C
		public bool ReadBoolean(long position)
		{
			return this.ReadByte(position) > 0;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x001ADE88 File Offset: 0x001AD088
		public unsafe byte ReadByte(long position)
		{
			this.EnsureSafeToRead(position, 1);
			byte* ptr = null;
			byte result;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				result = (ptr + this._offset)[position];
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return result;
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x001ADEE0 File Offset: 0x001AD0E0
		public char ReadChar(long position)
		{
			return (char)this.ReadInt16(position);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x001ADEEC File Offset: 0x001AD0EC
		public unsafe short ReadInt16(long position)
		{
			this.EnsureSafeToRead(position, 2);
			byte* ptr = null;
			short result;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				result = Unsafe.ReadUnaligned<short>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return result;
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x001ADF48 File Offset: 0x001AD148
		public unsafe int ReadInt32(long position)
		{
			this.EnsureSafeToRead(position, 4);
			byte* ptr = null;
			int result;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				result = Unsafe.ReadUnaligned<int>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return result;
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x001ADFA4 File Offset: 0x001AD1A4
		public unsafe long ReadInt64(long position)
		{
			this.EnsureSafeToRead(position, 8);
			byte* ptr = null;
			long result;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				result = Unsafe.ReadUnaligned<long>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return result;
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001AE000 File Offset: 0x001AD200
		public unsafe decimal ReadDecimal(long position)
		{
			this.EnsureSafeToRead(position, 16);
			byte* ptr = null;
			int lo;
			int mid;
			int hi;
			int num;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				lo = Unsafe.ReadUnaligned<int>((void*)ptr);
				mid = Unsafe.ReadUnaligned<int>((void*)(ptr + 4));
				hi = Unsafe.ReadUnaligned<int>((void*)(ptr + 8));
				num = Unsafe.ReadUnaligned<int>((void*)(ptr + 12));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			if ((num & 2130771967) != 0 || (num & 16711680) > 1835008)
			{
				throw new ArgumentException(SR.Arg_BadDecimal);
			}
			bool isNegative = (num & int.MinValue) != 0;
			byte scale = (byte)(num >> 16);
			return new decimal(lo, mid, hi, isNegative, scale);
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x001AE0C4 File Offset: 0x001AD2C4
		public float ReadSingle(long position)
		{
			return BitConverter.Int32BitsToSingle(this.ReadInt32(position));
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001AE0D2 File Offset: 0x001AD2D2
		public double ReadDouble(long position)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(position));
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x001AE0E0 File Offset: 0x001AD2E0
		[CLSCompliant(false)]
		public sbyte ReadSByte(long position)
		{
			return (sbyte)this.ReadByte(position);
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x001ADEE0 File Offset: 0x001AD0E0
		[CLSCompliant(false)]
		public ushort ReadUInt16(long position)
		{
			return (ushort)this.ReadInt16(position);
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x001AE0EA File Offset: 0x001AD2EA
		[CLSCompliant(false)]
		public uint ReadUInt32(long position)
		{
			return (uint)this.ReadInt32(position);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x001AE0F3 File Offset: 0x001AD2F3
		[CLSCompliant(false)]
		public ulong ReadUInt64(long position)
		{
			return (ulong)this.ReadInt64(position);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x001AE0FC File Offset: 0x001AD2FC
		public void Read<T>(long position, out T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canRead)
			{
				throw new NotSupportedException(SR.NotSupported_Reading);
			}
			uint num = SafeBuffer.SizeOf<T>();
			if (position <= this._capacity - (long)((ulong)num))
			{
				structure = this._buffer.Read<T>((ulong)(this._offset + position));
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			throw new ArgumentException(SR.Format(SR.Argument_NotEnoughBytesToRead, typeof(T)), "position");
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x001AE1AC File Offset: 0x001AD3AC
		public int ReadArray<T>(long position, [Nullable(new byte[]
		{
			1,
			0
		})] T[] array, int offset, int count) where T : struct
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
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canRead)
			{
				throw new NotSupportedException(SR.NotSupported_Reading);
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			uint num = SafeBuffer.AlignedSizeOf<T>();
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			int num2 = count;
			long num3 = this._capacity - position;
			if (num3 < 0L)
			{
				num2 = 0;
			}
			else
			{
				ulong num4 = (ulong)num * (ulong)((long)count);
				if (num3 < (long)num4)
				{
					num2 = (int)(num3 / (long)((ulong)num));
				}
			}
			this._buffer.ReadArray<T>((ulong)(this._offset + position), array, offset, num2);
			return num2;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001AE2A5 File Offset: 0x001AD4A5
		public void Write(long position, bool value)
		{
			this.Write(position, value ? 1 : 0);
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001AE2B8 File Offset: 0x001AD4B8
		public unsafe void Write(long position, byte value)
		{
			this.EnsureSafeToWrite(position, 1);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				(ptr + this._offset)[position] = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x001AE310 File Offset: 0x001AD510
		public void Write(long position, char value)
		{
			this.Write(position, (short)value);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001AE31C File Offset: 0x001AD51C
		public unsafe void Write(long position, short value)
		{
			this.EnsureSafeToWrite(position, 2);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<short>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x001AE378 File Offset: 0x001AD578
		public unsafe void Write(long position, int value)
		{
			this.EnsureSafeToWrite(position, 4);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<int>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x001AE3D4 File Offset: 0x001AD5D4
		public unsafe void Write(long position, long value)
		{
			this.EnsureSafeToWrite(position, 8);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<long>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x001AE430 File Offset: 0x001AD630
		public unsafe void Write(long position, decimal value)
		{
			this.EnsureSafeToWrite(position, 16);
			int* ptr = (int*)(&value);
			int value2 = *ptr;
			int value3 = ptr[1];
			int value4 = ptr[2];
			int value5 = ptr[3];
			byte* ptr2 = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr2);
				ptr2 += this._offset + position;
				Unsafe.WriteUnaligned<int>((void*)ptr2, value4);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 4), value5);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 8), value3);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 12), value2);
			}
			finally
			{
				if (ptr2 != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x001AE4D0 File Offset: 0x001AD6D0
		public void Write(long position, float value)
		{
			this.Write(position, BitConverter.SingleToInt32Bits(value));
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001AE4DF File Offset: 0x001AD6DF
		public void Write(long position, double value)
		{
			this.Write(position, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001AE4EE File Offset: 0x001AD6EE
		[CLSCompliant(false)]
		public void Write(long position, sbyte value)
		{
			this.Write(position, (byte)value);
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x001AE310 File Offset: 0x001AD510
		[CLSCompliant(false)]
		public void Write(long position, ushort value)
		{
			this.Write(position, (short)value);
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001AE4F9 File Offset: 0x001AD6F9
		[CLSCompliant(false)]
		public void Write(long position, uint value)
		{
			this.Write(position, (int)value);
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x001AE503 File Offset: 0x001AD703
		[CLSCompliant(false)]
		public void Write(long position, ulong value)
		{
			this.Write(position, (long)value);
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001AE510 File Offset: 0x001AD710
		public void Write<T>(long position, ref T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException(SR.NotSupported_Writing);
			}
			uint num = SafeBuffer.SizeOf<T>();
			if (position <= this._capacity - (long)((ulong)num))
			{
				this._buffer.Write<T>((ulong)(this._offset + position), structure);
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			throw new ArgumentException(SR.Format(SR.Argument_NotEnoughBytesToWrite, typeof(T)), "position");
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001AE5C0 File Offset: 0x001AD7C0
		public void WriteArray<T>(long position, [Nullable(new byte[]
		{
			1,
			0
		})] T[] array, int offset, int count) where T : struct
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
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (position >= this.Capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException(SR.NotSupported_Writing);
			}
			this._buffer.WriteArray<T>((ulong)(this._offset + position), array, offset, count);
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x001AE690 File Offset: 0x001AD890
		private void EnsureSafeToRead(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canRead)
			{
				throw new NotSupportedException(SR.NotSupported_Reading);
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			throw new ArgumentException(SR.Argument_NotEnoughBytesToRead, "position");
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x001AE714 File Offset: 0x001AD914
		private void EnsureSafeToWrite(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", SR.ObjectDisposed_ViewAccessorClosed);
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException(SR.NotSupported_Writing);
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", SR.ArgumentOutOfRange_PositionLessThanCapacityRequired);
			}
			throw new ArgumentException(SR.Argument_NotEnoughBytesToWrite, "position");
		}

		// Token: 0x0400192C RID: 6444
		private SafeBuffer _buffer;

		// Token: 0x0400192D RID: 6445
		private long _offset;

		// Token: 0x0400192E RID: 6446
		private long _capacity;

		// Token: 0x0400192F RID: 6447
		private FileAccess _access;

		// Token: 0x04001930 RID: 6448
		private bool _isOpen;

		// Token: 0x04001931 RID: 6449
		private bool _canRead;

		// Token: 0x04001932 RID: 6450
		private bool _canWrite;
	}
}
