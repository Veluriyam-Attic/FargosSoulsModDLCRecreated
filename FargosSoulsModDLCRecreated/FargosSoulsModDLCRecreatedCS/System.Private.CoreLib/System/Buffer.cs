using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000057 RID: 87
	[Nullable(0)]
	[NullableContext(1)]
	public static class Buffer
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000AF2D4 File Offset: 0x000AE4D4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal unsafe static void _ZeroMemory(ref byte b, [NativeInteger] UIntPtr byteLength)
		{
			fixed (byte* ptr = &b)
			{
				byte* b2 = ptr;
				Buffer.__ZeroMemory((void*)b2, byteLength);
			}
		}

		// Token: 0x060001D5 RID: 469
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void __ZeroMemory(void* b, [NativeInteger] UIntPtr byteLength);

		// Token: 0x060001D6 RID: 470 RVA: 0x000AF2F0 File Offset: 0x000AE4F0
		internal static void BulkMoveWithWriteBarrier(ref byte destination, ref byte source, [NativeInteger] UIntPtr byteCount)
		{
			if (byteCount <= (UIntPtr)((IntPtr)16384))
			{
				Buffer.__BulkMoveWithWriteBarrier(ref destination, ref source, byteCount);
				return;
			}
			Buffer._BulkMoveWithWriteBarrier(ref destination, ref source, byteCount);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000AF30C File Offset: 0x000AE50C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void _BulkMoveWithWriteBarrier(ref byte destination, ref byte source, [NativeInteger] UIntPtr byteCount)
		{
			if (Unsafe.AreSame<byte>(ref source, ref destination))
			{
				return;
			}
			if ((UIntPtr)Unsafe.ByteOffset<byte>(ref source, ref destination) >= byteCount)
			{
				do
				{
					byteCount -= (UIntPtr)((IntPtr)16384);
					Buffer.__BulkMoveWithWriteBarrier(ref destination, ref source, (UIntPtr)((IntPtr)16384));
					destination = Unsafe.AddByteOffset<byte>(ref destination, (UIntPtr)((IntPtr)16384));
					source = Unsafe.AddByteOffset<byte>(ref source, (UIntPtr)((IntPtr)16384));
				}
				while (byteCount > (UIntPtr)((IntPtr)16384));
			}
			else
			{
				do
				{
					byteCount -= (UIntPtr)((IntPtr)16384);
					Buffer.__BulkMoveWithWriteBarrier(Unsafe.AddByteOffset<byte>(ref destination, byteCount), Unsafe.AddByteOffset<byte>(ref source, byteCount), (UIntPtr)((IntPtr)16384));
				}
				while (byteCount > (UIntPtr)((IntPtr)16384));
			}
			Buffer.__BulkMoveWithWriteBarrier(ref destination, ref source, byteCount);
		}

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void __BulkMoveWithWriteBarrier(ref byte destination, ref byte source, [NativeInteger] UIntPtr byteCount);

		// Token: 0x060001D9 RID: 473
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void __Memmove(byte* dest, byte* src, [NativeInteger] UIntPtr len);

		// Token: 0x060001DA RID: 474 RVA: 0x000AF3A0 File Offset: 0x000AE5A0
		internal unsafe static void Memcpy(byte* dest, byte* src, int len)
		{
			Buffer.Memmove(dest, src, (UIntPtr)((IntPtr)len));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000AF3AC File Offset: 0x000AE5AC
		internal unsafe static void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = src)
			{
				byte* ptr;
				if (src == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(pDest + destIndex, ptr + srcIndex, len);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000AF3E5 File Offset: 0x000AE5E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Memmove<T>(ref T destination, ref T source, [NativeInteger] UIntPtr elementCount)
		{
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Buffer.Memmove(Unsafe.As<T, byte>(ref destination), Unsafe.As<T, byte>(ref source), elementCount * (UIntPtr)((IntPtr)Unsafe.SizeOf<T>()));
				return;
			}
			Buffer.BulkMoveWithWriteBarrier(Unsafe.As<T, byte>(ref destination), Unsafe.As<T, byte>(ref source), elementCount * (UIntPtr)((IntPtr)Unsafe.SizeOf<T>()));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000AF424 File Offset: 0x000AE624
		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (dst == null)
			{
				throw new ArgumentNullException("dst");
			}
			UIntPtr uintPtr = (UIntPtr)src.LongLength;
			if (src.GetType() != typeof(byte[]))
			{
				if (!src.GetCorElementTypeOfElementType().IsPrimitiveType())
				{
					throw new ArgumentException(SR.Arg_MustBePrimArray, "src");
				}
				uintPtr *= (UIntPtr)src.GetElementSize();
			}
			UIntPtr uintPtr2 = uintPtr;
			if (src != dst)
			{
				uintPtr2 = (UIntPtr)dst.LongLength;
				if (dst.GetType() != typeof(byte[]))
				{
					if (!dst.GetCorElementTypeOfElementType().IsPrimitiveType())
					{
						throw new ArgumentException(SR.Arg_MustBePrimArray, "dst");
					}
					uintPtr2 *= (UIntPtr)dst.GetElementSize();
				}
			}
			if (srcOffset < 0)
			{
				throw new ArgumentOutOfRangeException("srcOffset", SR.ArgumentOutOfRange_MustBeNonNegInt32);
			}
			if (dstOffset < 0)
			{
				throw new ArgumentOutOfRangeException("dstOffset", SR.ArgumentOutOfRange_MustBeNonNegInt32);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_MustBeNonNegInt32);
			}
			UIntPtr uintPtr3 = (UIntPtr)((IntPtr)count);
			UIntPtr uintPtr4 = (UIntPtr)((IntPtr)srcOffset);
			UIntPtr uintPtr5 = (UIntPtr)((IntPtr)dstOffset);
			if (uintPtr < uintPtr4 + uintPtr3 || uintPtr2 < uintPtr5 + uintPtr3)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			Buffer.Memmove(Unsafe.AddByteOffset<byte>(dst.GetRawArrayData(), uintPtr5), Unsafe.AddByteOffset<byte>(src.GetRawArrayData(), uintPtr4), uintPtr3);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000AF560 File Offset: 0x000AE760
		public static int ByteLength(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!array.GetCorElementTypeOfElementType().IsPrimitiveType())
			{
				throw new ArgumentException(SR.Arg_MustBePrimArray, "array");
			}
			UIntPtr uintPtr = (UIntPtr)array.LongLength * (UIntPtr)array.GetElementSize();
			return checked((int)uintPtr);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000AF5AA File Offset: 0x000AE7AA
		public unsafe static byte GetByte(Array array, int index)
		{
			if (index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return *Unsafe.Add<byte>(array.GetRawArrayData(), index);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000AF5CD File Offset: 0x000AE7CD
		public unsafe static void SetByte(Array array, int index, byte value)
		{
			if (index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			*Unsafe.Add<byte>(array.GetRawArrayData(), index) = value;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000AF5F1 File Offset: 0x000AE7F1
		internal unsafe static void ZeroMemory(byte* dest, [NativeInteger] UIntPtr len)
		{
			SpanHelpers.ClearWithoutReferences(ref *dest, len);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000AF5FA File Offset: 0x000AE7FA
		[NullableContext(0)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			Buffer.Memmove((byte*)destination, (byte*)source, checked((UIntPtr)sourceBytesToCopy));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000AF610 File Offset: 0x000AE810
		[NullableContext(0)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void MemoryCopy(void* source, void* destination, ulong destinationSizeInBytes, ulong sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			Buffer.Memmove((byte*)destination, (byte*)source, checked((UIntPtr)sourceBytesToCopy));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000AF628 File Offset: 0x000AE828
		internal unsafe static void Memmove(byte* dest, byte* src, [NativeInteger] UIntPtr len)
		{
			if (dest - src >= (IntPtr)len && src - dest >= (IntPtr)len)
			{
				byte* ptr = src + (ulong)len;
				byte* ptr2 = dest + (ulong)len;
				if (len > (UIntPtr)((IntPtr)16))
				{
					if (len > (UIntPtr)((IntPtr)64))
					{
						if (len > (UIntPtr)((IntPtr)2048))
						{
							goto IL_10A;
						}
						UIntPtr uintPtr = len >> 6;
						do
						{
							*(Buffer.Block64*)dest = *(Buffer.Block64*)src;
							dest += 64;
							src += 64;
							uintPtr--;
						}
						while (uintPtr != 0);
						len %= (UIntPtr)((IntPtr)64);
						if (len <= (UIntPtr)((IntPtr)16))
						{
							*(Buffer.Block16*)(ptr2 - 16) = *(Buffer.Block16*)(ptr - 16);
							return;
						}
					}
					*(Buffer.Block16*)dest = *(Buffer.Block16*)src;
					if (len > (UIntPtr)((IntPtr)32))
					{
						*(Buffer.Block16*)(dest + 16) = *(Buffer.Block16*)(src + 16);
						if (len > (UIntPtr)((IntPtr)48))
						{
							*(Buffer.Block16*)(dest + 32) = *(Buffer.Block16*)(src + 32);
						}
					}
					*(Buffer.Block16*)(ptr2 - 16) = *(Buffer.Block16*)(ptr - 16);
					return;
				}
				if ((len & (UIntPtr)((IntPtr)24)) != 0)
				{
					*(long*)dest = *(long*)src;
					*(long*)(ptr2 - 8) = *(long*)(ptr - 8);
					return;
				}
				if ((len & (UIntPtr)((IntPtr)4)) != 0)
				{
					*(int*)dest = *(int*)src;
					*(int*)(ptr2 - 4) = *(int*)(ptr - 4);
					return;
				}
				if (len == 0)
				{
					return;
				}
				*dest = *src;
				if ((len & (UIntPtr)((IntPtr)2)) == 0)
				{
					return;
				}
				*(short*)(ptr2 - 2) = *(short*)(ptr - 2);
				return;
			}
			IL_10A:
			Buffer._Memmove(dest, src, len);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000AF748 File Offset: 0x000AE948
		private unsafe static void Memmove(ref byte dest, ref byte src, [NativeInteger] UIntPtr len)
		{
			if ((UIntPtr)Unsafe.ByteOffset<byte>(ref src, ref dest) >= len && (UIntPtr)Unsafe.ByteOffset<byte>(ref dest, ref src) >= len)
			{
				ref byte source = ref Unsafe.Add<byte>(ref src, (IntPtr)len);
				ref byte source2 = ref Unsafe.Add<byte>(ref dest, (IntPtr)len);
				if (len > (UIntPtr)((IntPtr)16))
				{
					if (len > (UIntPtr)((IntPtr)64))
					{
						if (len > (UIntPtr)((IntPtr)2048))
						{
							goto IL_1DB;
						}
						UIntPtr uintPtr = len >> 6;
						do
						{
							*Unsafe.As<byte, Buffer.Block64>(ref dest) = *Unsafe.As<byte, Buffer.Block64>(ref src);
							dest = Unsafe.Add<byte>(ref dest, 64);
							src = Unsafe.Add<byte>(ref src, 64);
							uintPtr--;
						}
						while (uintPtr != 0);
						len %= (UIntPtr)((IntPtr)64);
						if (len <= (UIntPtr)((IntPtr)16))
						{
							*Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref source2, -16)) = *Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref source, -16));
							return;
						}
					}
					*Unsafe.As<byte, Buffer.Block16>(ref dest) = *Unsafe.As<byte, Buffer.Block16>(ref src);
					if (len > (UIntPtr)((IntPtr)32))
					{
						*Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref dest, 16)) = *Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref src, 16));
						if (len > (UIntPtr)((IntPtr)48))
						{
							*Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref dest, 32)) = *Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref src, 32));
						}
					}
					*Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref source2, -16)) = *Unsafe.As<byte, Buffer.Block16>(Unsafe.Add<byte>(ref source, -16));
					return;
				}
				if ((len & (UIntPtr)((IntPtr)24)) != 0)
				{
					*Unsafe.As<byte, long>(ref dest) = *Unsafe.As<byte, long>(ref src);
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref source2, -8)) = *Unsafe.As<byte, long>(Unsafe.Add<byte>(ref source, -8));
					return;
				}
				if ((len & (UIntPtr)((IntPtr)4)) != 0)
				{
					*Unsafe.As<byte, int>(ref dest) = *Unsafe.As<byte, int>(ref src);
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref source2, -4)) = *Unsafe.As<byte, int>(Unsafe.Add<byte>(ref source, -4));
					return;
				}
				if (len == 0)
				{
					return;
				}
				dest = src;
				if ((len & (UIntPtr)((IntPtr)2)) == 0)
				{
					return;
				}
				*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref source2, -2)) = *Unsafe.As<byte, short>(Unsafe.Add<byte>(ref source, -2));
				return;
			}
			else if (Unsafe.AreSame<byte>(ref dest, ref src))
			{
				return;
			}
			IL_1DB:
			Buffer._Memmove(ref dest, ref src, len);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000AF938 File Offset: 0x000AEB38
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void _Memmove(byte* dest, byte* src, [NativeInteger] UIntPtr len)
		{
			Buffer.__Memmove(dest, src, len);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000AF944 File Offset: 0x000AEB44
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void _Memmove(ref byte dest, ref byte src, [NativeInteger] UIntPtr len)
		{
			fixed (byte* ptr = &dest)
			{
				byte* dest2 = ptr;
				fixed (byte* ptr2 = &src)
				{
					byte* src2 = ptr2;
					Buffer.__Memmove(dest2, src2, len);
				}
			}
		}

		// Token: 0x02000058 RID: 88
		private struct Block16
		{
		}

		// Token: 0x02000059 RID: 89
		private struct Block64
		{
		}
	}
}
