using System;
using System.Runtime.CompilerServices;
using Internal.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049D RID: 1181
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060044BB RID: 17595 RVA: 0x001794D5 File Offset: 0x001786D5
		protected SafeBuffer(bool ownsHandle) : base(ownsHandle)
		{
			this._numBytes = SafeBuffer.Uninitialized;
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x001794E9 File Offset: 0x001786E9
		[NativeInteger]
		private static UIntPtr Uninitialized
		{
			[return: NativeInteger]
			get
			{
				return UIntPtr.MaxValue;
			}
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x001794F0 File Offset: 0x001786F0
		[CLSCompliant(false)]
		public void Initialize(ulong numBytes)
		{
			if (IntPtr.Size == 4)
			{
			}
			if (numBytes >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.ArgumentOutOfRange_UIntPtrMax);
			}
			this._numBytes = (UIntPtr)numBytes;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0017951B File Offset: 0x0017871B
		[CLSCompliant(false)]
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			this.Initialize((ulong)numElements * (ulong)sizeOfEachElement);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x00179528 File Offset: 0x00178728
		[CLSCompliant(false)]
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, SafeBuffer.AlignedSizeOf<T>());
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x00179538 File Offset: 0x00178738
		[CLSCompliant(false)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			bool flag = false;
			base.DangerousAddRef(ref flag);
			pointer = (void*)this.handle;
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x00179573 File Offset: 0x00178773
		public void ReleasePointer()
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x00179590 File Offset: 0x00178790
		[CLSCompliant(false)]
		public unsafe T Read<T>(ulong byteOffset) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (UIntPtr)num);
			T result = default(T);
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				try
				{
					fixed (byte* ptr2 = Unsafe.As<T, byte>(ref result))
					{
						byte* dest = ptr2;
						Buffer.Memmove(dest, ptr, (UIntPtr)num);
					}
				}
				finally
				{
					byte* ptr2 = null;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x00179628 File Offset: 0x00178828
		[CLSCompliant(false)]
		public unsafe void ReadArray<T>(ulong byteOffset, [Nullable(new byte[]
		{
			1,
			0
		})] T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.ArgumentNull_Buffer);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			uint num2 = SafeBuffer.AlignedSizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked((UIntPtr)(unchecked((ulong)num2) * (ulong)(unchecked((long)count)))));
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				if (count > 0)
				{
					try
					{
						fixed (byte* ptr2 = Unsafe.As<T, byte>(ref array[index]))
						{
							byte* ptr3 = ptr2;
							for (int i = 0; i < count; i++)
							{
								Buffer.Memmove(ptr3 + (ulong)num * (ulong)((long)i), ptr + (ulong)num2 * (ulong)((long)i), (UIntPtr)num);
							}
						}
					}
					finally
					{
						byte* ptr2 = null;
					}
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x00179740 File Offset: 0x00178940
		[CLSCompliant(false)]
		public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (UIntPtr)num);
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				try
				{
					fixed (byte* ptr2 = Unsafe.As<T, byte>(ref value))
					{
						byte* src = ptr2;
						Buffer.Memmove(ptr, src, (UIntPtr)num);
					}
				}
				finally
				{
					byte* ptr2 = null;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x001797CC File Offset: 0x001789CC
		[CLSCompliant(false)]
		public unsafe void WriteArray<T>(ulong byteOffset, [Nullable(new byte[]
		{
			1,
			0
		})] T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.ArgumentNull_Buffer);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			uint num2 = SafeBuffer.AlignedSizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked((UIntPtr)(unchecked((ulong)num2) * (ulong)(unchecked((long)count)))));
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				if (count > 0)
				{
					try
					{
						fixed (byte* ptr2 = Unsafe.As<T, byte>(ref array[index]))
						{
							byte* ptr3 = ptr2;
							for (int i = 0; i < count; i++)
							{
								Buffer.Memmove(ptr + (ulong)num2 * (ulong)((long)i), ptr3 + (ulong)num * (ulong)((long)i), (UIntPtr)num);
							}
						}
					}
					finally
					{
						byte* ptr2 = null;
					}
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x001798E4 File Offset: 0x00178AE4
		[CLSCompliant(false)]
		public ulong ByteLength
		{
			get
			{
				if (this._numBytes == SafeBuffer.Uninitialized)
				{
					throw SafeBuffer.NotInitialized();
				}
				return (ulong)this._numBytes;
			}
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x00179900 File Offset: 0x00178B00
		private unsafe void SpaceCheck(byte* ptr, [NativeInteger] UIntPtr sizeInBytes)
		{
			if (this._numBytes < sizeInBytes)
			{
				SafeBuffer.NotEnoughRoom();
			}
			if ((long)((byte*)ptr - (byte*)((void*)this.handle)) > (long)((ulong)(this._numBytes - sizeInBytes)))
			{
				SafeBuffer.NotEnoughRoom();
			}
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x00179930 File Offset: 0x00178B30
		private static void NotEnoughRoom()
		{
			throw new ArgumentException(SR.Arg_BufferTooSmall);
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x0017993C File Offset: 0x00178B3C
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException(SR.InvalidOperation_MustCallInitialize);
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x00179948 File Offset: 0x00178B48
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = SafeBuffer.SizeOf<T>();
			if (num == 1U || num == 2U)
			{
				return num;
			}
			return (uint)((ulong)(num + 3U) & 18446744073709551612UL);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0017996E File Offset: 0x00178B6E
		internal static uint SizeOf<T>() where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException(SR.Argument_NeedStructWithNoRefs);
			}
			return (uint)Unsafe.SizeOf<T>();
		}

		// Token: 0x04000F61 RID: 3937
		[NativeInteger]
		private UIntPtr _numBytes;
	}
}
