using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001D5 RID: 469
	[Intrinsic]
	public struct Vector<T> : IEquatable<Vector<T>>, IFormattable where T : struct
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0010AD5E File Offset: 0x00109F5E
		public static int Count
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return Unsafe.SizeOf<Vector<T>>() / Unsafe.SizeOf<T>();
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0010AD70 File Offset: 0x00109F70
		public static Vector<T> Zero
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return default(Vector<T>);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0010AD8B File Offset: 0x00109F8B
		public static Vector<T> One
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return new Vector<T>(Vector<T>.GetOneValue());
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0010AD9C File Offset: 0x00109F9C
		internal static Vector<T> AllBitsSet
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return new Vector<T>(Vector<T>.GetAllBitsSetValue());
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0010ADB0 File Offset: 0x00109FB0
		[Intrinsic]
		public unsafe Vector(T value)
		{
			this = default(Vector<T>);
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					fixed (byte* ptr = &this.register.byte_0)
					{
						byte* ptr2 = ptr;
						for (IntPtr intPtr = (IntPtr)0; intPtr < (IntPtr)Vector<T>.Count; intPtr++)
						{
							ptr2[(long)intPtr] = (byte)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(sbyte))
				{
					fixed (sbyte* ptr3 = &this.register.sbyte_0)
					{
						sbyte* ptr4 = ptr3;
						for (IntPtr intPtr2 = (IntPtr)0; intPtr2 < (IntPtr)Vector<T>.Count; intPtr2++)
						{
							ptr4[(long)intPtr2] = (sbyte)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(ushort))
				{
					fixed (ushort* ptr5 = &this.register.uint16_0)
					{
						ushort* ptr6 = ptr5;
						for (IntPtr intPtr3 = (IntPtr)0; intPtr3 < (IntPtr)Vector<T>.Count; intPtr3++)
						{
							ptr6[(long)intPtr3 * 2L / 2L] = (ushort)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(short))
				{
					fixed (short* ptr7 = &this.register.int16_0)
					{
						short* ptr8 = ptr7;
						for (IntPtr intPtr4 = (IntPtr)0; intPtr4 < (IntPtr)Vector<T>.Count; intPtr4++)
						{
							ptr8[(long)intPtr4 * 2L / 2L] = (short)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(uint))
				{
					fixed (uint* ptr9 = &this.register.uint32_0)
					{
						uint* ptr10 = ptr9;
						for (IntPtr intPtr5 = (IntPtr)0; intPtr5 < (IntPtr)Vector<T>.Count; intPtr5++)
						{
							ptr10[(long)intPtr5 * 4L / 4L] = (uint)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(int))
				{
					fixed (int* ptr11 = &this.register.int32_0)
					{
						int* ptr12 = ptr11;
						for (IntPtr intPtr6 = (IntPtr)0; intPtr6 < (IntPtr)Vector<T>.Count; intPtr6++)
						{
							ptr12[(long)intPtr6 * 4L / 4L] = (int)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(ulong))
				{
					fixed (ulong* ptr13 = &this.register.uint64_0)
					{
						ulong* ptr14 = ptr13;
						for (IntPtr intPtr7 = (IntPtr)0; intPtr7 < (IntPtr)Vector<T>.Count; intPtr7++)
						{
							ptr14[(long)intPtr7 * 8L / 8L] = (ulong)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(long))
				{
					fixed (long* ptr15 = &this.register.int64_0)
					{
						long* ptr16 = ptr15;
						for (IntPtr intPtr8 = (IntPtr)0; intPtr8 < (IntPtr)Vector<T>.Count; intPtr8++)
						{
							ptr16[(long)intPtr8 * 8L / 8L] = (long)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(float))
				{
					fixed (float* ptr17 = &this.register.single_0)
					{
						float* ptr18 = ptr17;
						for (IntPtr intPtr9 = (IntPtr)0; intPtr9 < (IntPtr)Vector<T>.Count; intPtr9++)
						{
							ptr18[(long)intPtr9 * 4L / 4L] = (float)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(double))
				{
					fixed (double* ptr19 = &this.register.double_0)
					{
						double* ptr20 = ptr19;
						for (IntPtr intPtr10 = (IntPtr)0; intPtr10 < (IntPtr)Vector<T>.Count; intPtr10++)
						{
							ptr20[(long)intPtr10 * 8L / 8L] = (double)((object)value);
						}
					}
					return;
				}
			}
			else
			{
				if (typeof(T) == typeof(byte))
				{
					this.register.byte_0 = (byte)((object)value);
					this.register.byte_1 = (byte)((object)value);
					this.register.byte_2 = (byte)((object)value);
					this.register.byte_3 = (byte)((object)value);
					this.register.byte_4 = (byte)((object)value);
					this.register.byte_5 = (byte)((object)value);
					this.register.byte_6 = (byte)((object)value);
					this.register.byte_7 = (byte)((object)value);
					this.register.byte_8 = (byte)((object)value);
					this.register.byte_9 = (byte)((object)value);
					this.register.byte_10 = (byte)((object)value);
					this.register.byte_11 = (byte)((object)value);
					this.register.byte_12 = (byte)((object)value);
					this.register.byte_13 = (byte)((object)value);
					this.register.byte_14 = (byte)((object)value);
					this.register.byte_15 = (byte)((object)value);
					return;
				}
				if (typeof(T) == typeof(sbyte))
				{
					this.register.sbyte_0 = (sbyte)((object)value);
					this.register.sbyte_1 = (sbyte)((object)value);
					this.register.sbyte_2 = (sbyte)((object)value);
					this.register.sbyte_3 = (sbyte)((object)value);
					this.register.sbyte_4 = (sbyte)((object)value);
					this.register.sbyte_5 = (sbyte)((object)value);
					this.register.sbyte_6 = (sbyte)((object)value);
					this.register.sbyte_7 = (sbyte)((object)value);
					this.register.sbyte_8 = (sbyte)((object)value);
					this.register.sbyte_9 = (sbyte)((object)value);
					this.register.sbyte_10 = (sbyte)((object)value);
					this.register.sbyte_11 = (sbyte)((object)value);
					this.register.sbyte_12 = (sbyte)((object)value);
					this.register.sbyte_13 = (sbyte)((object)value);
					this.register.sbyte_14 = (sbyte)((object)value);
					this.register.sbyte_15 = (sbyte)((object)value);
					return;
				}
				if (typeof(T) == typeof(ushort))
				{
					this.register.uint16_0 = (ushort)((object)value);
					this.register.uint16_1 = (ushort)((object)value);
					this.register.uint16_2 = (ushort)((object)value);
					this.register.uint16_3 = (ushort)((object)value);
					this.register.uint16_4 = (ushort)((object)value);
					this.register.uint16_5 = (ushort)((object)value);
					this.register.uint16_6 = (ushort)((object)value);
					this.register.uint16_7 = (ushort)((object)value);
					return;
				}
				if (typeof(T) == typeof(short))
				{
					this.register.int16_0 = (short)((object)value);
					this.register.int16_1 = (short)((object)value);
					this.register.int16_2 = (short)((object)value);
					this.register.int16_3 = (short)((object)value);
					this.register.int16_4 = (short)((object)value);
					this.register.int16_5 = (short)((object)value);
					this.register.int16_6 = (short)((object)value);
					this.register.int16_7 = (short)((object)value);
					return;
				}
				if (typeof(T) == typeof(uint))
				{
					this.register.uint32_0 = (uint)((object)value);
					this.register.uint32_1 = (uint)((object)value);
					this.register.uint32_2 = (uint)((object)value);
					this.register.uint32_3 = (uint)((object)value);
					return;
				}
				if (typeof(T) == typeof(int))
				{
					this.register.int32_0 = (int)((object)value);
					this.register.int32_1 = (int)((object)value);
					this.register.int32_2 = (int)((object)value);
					this.register.int32_3 = (int)((object)value);
					return;
				}
				if (typeof(T) == typeof(ulong))
				{
					this.register.uint64_0 = (ulong)((object)value);
					this.register.uint64_1 = (ulong)((object)value);
					return;
				}
				if (typeof(T) == typeof(long))
				{
					this.register.int64_0 = (long)((object)value);
					this.register.int64_1 = (long)((object)value);
					return;
				}
				if (typeof(T) == typeof(float))
				{
					this.register.single_0 = (float)((object)value);
					this.register.single_1 = (float)((object)value);
					this.register.single_2 = (float)((object)value);
					this.register.single_3 = (float)((object)value);
					return;
				}
				if (typeof(T) == typeof(double))
				{
					this.register.double_0 = (double)((object)value);
					this.register.double_1 = (double)((object)value);
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0010B83F File Offset: 0x0010AA3F
		[Intrinsic]
		public Vector([Nullable(new byte[]
		{
			1,
			0
		})] T[] values)
		{
			this = new Vector<T>(values, 0);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0010B84C File Offset: 0x0010AA4C
		[Intrinsic]
		public Vector([Nullable(new byte[]
		{
			1,
			0
		})] T[] values, int index)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (values == null)
			{
				throw new NullReferenceException(SR.Arg_NullArgumentNullRef);
			}
			if (index < 0 || values.Length - index < Vector<T>.Count)
			{
				Vector.ThrowInsufficientNumberOfElementsException(Vector<T>.Count);
			}
			this = Unsafe.ReadUnaligned<Vector<T>>(Unsafe.As<T, byte>(ref values[index]));
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0010B89D File Offset: 0x0010AA9D
		internal unsafe Vector(void* dataPointer)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			this = Unsafe.ReadUnaligned<Vector<T>>(dataPointer);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0010B8B0 File Offset: 0x0010AAB0
		private Vector(ref Register existingRegister)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			this.register = existingRegister;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0010B8C3 File Offset: 0x0010AAC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector(ReadOnlySpan<byte> values)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (values.Length < Vector<byte>.Count)
			{
				Vector.ThrowInsufficientNumberOfElementsException(Vector<byte>.Count);
			}
			this = Unsafe.ReadUnaligned<Vector<T>>(MemoryMarshal.GetReference<byte>(values));
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0010B8F3 File Offset: 0x0010AAF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector(ReadOnlySpan<T> values)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (values.Length < Vector<T>.Count)
			{
				Vector.ThrowInsufficientNumberOfElementsException(Vector<T>.Count);
			}
			this = Unsafe.ReadUnaligned<Vector<T>>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)));
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0010B928 File Offset: 0x0010AB28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector(Span<T> values)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (values.Length < Vector<T>.Count)
			{
				Vector.ThrowInsufficientNumberOfElementsException(Vector<T>.Count);
			}
			this = Unsafe.ReadUnaligned<Vector<T>>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)));
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0010B95D File Offset: 0x0010AB5D
		public readonly void CopyTo(Span<byte> destination)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (destination.Length < Vector<byte>.Count)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			Unsafe.WriteUnaligned<Vector<T>>(MemoryMarshal.GetReference<byte>(destination), this);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0010B988 File Offset: 0x0010AB88
		public readonly void CopyTo(Span<T> destination)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (destination.Length < Vector<T>.Count)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			Unsafe.WriteUnaligned<Vector<T>>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(destination)), this);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0010B9B8 File Offset: 0x0010ABB8
		[Intrinsic]
		public readonly void CopyTo([Nullable(new byte[]
		{
			1,
			0
		})] T[] destination)
		{
			this.CopyTo(destination, 0);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0010B9C4 File Offset: 0x0010ABC4
		[Intrinsic]
		public readonly void CopyTo([Nullable(new byte[]
		{
			1,
			0
		})] T[] destination, int startIndex)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (destination == null)
			{
				throw new NullReferenceException(SR.Arg_NullArgumentNullRef);
			}
			if (startIndex >= destination.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.Format(SR.Arg_ArgumentOutOfRangeException, startIndex));
			}
			if (destination.Length - startIndex < Vector<T>.Count)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, startIndex));
			}
			Unsafe.WriteUnaligned<Vector<T>>(Unsafe.As<T, byte>(ref destination[startIndex]), this);
		}

		// Token: 0x17000679 RID: 1657
		public unsafe readonly T this[int index]
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				if (index >= Vector<T>.Count)
				{
					throw new IndexOutOfRangeException(SR.Format(SR.Arg_ArgumentOutOfRangeException, index));
				}
				return *Unsafe.Add<T>(Unsafe.As<Vector<T>, T>(Unsafe.AsRef<Vector<T>>(this)), index);
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0010BA7C File Offset: 0x0010AC7C
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Vector<T>)
			{
				Vector<T> other = (Vector<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0010BAA1 File Offset: 0x0010ACA1
		[Intrinsic]
		public readonly bool Equals(Vector<T> other)
		{
			return this == other;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0010BAB0 File Offset: 0x0010ACB0
		public unsafe override readonly int GetHashCode()
		{
			HashCode hashCode = default(HashCode);
			if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
			{
				for (IntPtr intPtr = (IntPtr)0; intPtr < (IntPtr)Vector<int>.Count; intPtr++)
				{
					hashCode.Add<int>(*Unsafe.Add<int>(Unsafe.As<Vector<T>, int>(Unsafe.AsRef<Vector<T>>(this)), intPtr));
				}
			}
			else if (typeof(T) == typeof(float))
			{
				for (IntPtr intPtr2 = (IntPtr)0; intPtr2 < (IntPtr)Vector<T>.Count; intPtr2++)
				{
					hashCode.Add<float>(*Unsafe.Add<float>(Unsafe.As<Vector<T>, float>(Unsafe.AsRef<Vector<T>>(this)), intPtr2));
				}
			}
			else
			{
				if (!(typeof(T) == typeof(double)))
				{
					throw new NotSupportedException(SR.Arg_TypeNotSupported);
				}
				for (IntPtr intPtr3 = (IntPtr)0; intPtr3 < (IntPtr)Vector<T>.Count; intPtr3++)
				{
					hashCode.Add<double>(*Unsafe.Add<double>(Unsafe.As<Vector<T>, double>(Unsafe.AsRef<Vector<T>>(this)), intPtr3));
				}
			}
			return hashCode.ToHashCode();
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0010BC78 File Offset: 0x0010AE78
		[NullableContext(1)]
		public override readonly string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0010BC8A File Offset: 0x0010AE8A
		[NullableContext(1)]
		public readonly string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0010BC98 File Offset: 0x0010AE98
		[NullableContext(2)]
		[return: Nullable(1)]
		public readonly string ToString(string format, IFormatProvider formatProvider)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			for (int i = 0; i < Vector<T>.Count - 1; i++)
			{
				stringBuilder.Append(((IFormattable)((object)this[i])).ToString(format, formatProvider));
				stringBuilder.Append(numberGroupSeparator);
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(((IFormattable)((object)this[Vector<T>.Count - 1])).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0010BD3B File Offset: 0x0010AF3B
		public readonly bool TryCopyTo(Span<byte> destination)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (destination.Length < Vector<byte>.Count)
			{
				return false;
			}
			Unsafe.WriteUnaligned<Vector<T>>(MemoryMarshal.GetReference<byte>(destination), this);
			return true;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0010BD64 File Offset: 0x0010AF64
		public readonly bool TryCopyTo(Span<T> destination)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (destination.Length < Vector<T>.Count)
			{
				return false;
			}
			Unsafe.WriteUnaligned<Vector<T>>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(destination)), this);
			return true;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0010BD94 File Offset: 0x0010AF94
		[Intrinsic]
		public unsafe static Vector<T>operator +(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (!Vector.IsHardwareAccelerated)
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = left.register.byte_0 + right.register.byte_0;
					result.register.byte_1 = left.register.byte_1 + right.register.byte_1;
					result.register.byte_2 = left.register.byte_2 + right.register.byte_2;
					result.register.byte_3 = left.register.byte_3 + right.register.byte_3;
					result.register.byte_4 = left.register.byte_4 + right.register.byte_4;
					result.register.byte_5 = left.register.byte_5 + right.register.byte_5;
					result.register.byte_6 = left.register.byte_6 + right.register.byte_6;
					result.register.byte_7 = left.register.byte_7 + right.register.byte_7;
					result.register.byte_8 = left.register.byte_8 + right.register.byte_8;
					result.register.byte_9 = left.register.byte_9 + right.register.byte_9;
					result.register.byte_10 = left.register.byte_10 + right.register.byte_10;
					result.register.byte_11 = left.register.byte_11 + right.register.byte_11;
					result.register.byte_12 = left.register.byte_12 + right.register.byte_12;
					result.register.byte_13 = left.register.byte_13 + right.register.byte_13;
					result.register.byte_14 = left.register.byte_14 + right.register.byte_14;
					result.register.byte_15 = left.register.byte_15 + right.register.byte_15;
				}
				else if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = left.register.sbyte_0 + right.register.sbyte_0;
					result.register.sbyte_1 = left.register.sbyte_1 + right.register.sbyte_1;
					result.register.sbyte_2 = left.register.sbyte_2 + right.register.sbyte_2;
					result.register.sbyte_3 = left.register.sbyte_3 + right.register.sbyte_3;
					result.register.sbyte_4 = left.register.sbyte_4 + right.register.sbyte_4;
					result.register.sbyte_5 = left.register.sbyte_5 + right.register.sbyte_5;
					result.register.sbyte_6 = left.register.sbyte_6 + right.register.sbyte_6;
					result.register.sbyte_7 = left.register.sbyte_7 + right.register.sbyte_7;
					result.register.sbyte_8 = left.register.sbyte_8 + right.register.sbyte_8;
					result.register.sbyte_9 = left.register.sbyte_9 + right.register.sbyte_9;
					result.register.sbyte_10 = left.register.sbyte_10 + right.register.sbyte_10;
					result.register.sbyte_11 = left.register.sbyte_11 + right.register.sbyte_11;
					result.register.sbyte_12 = left.register.sbyte_12 + right.register.sbyte_12;
					result.register.sbyte_13 = left.register.sbyte_13 + right.register.sbyte_13;
					result.register.sbyte_14 = left.register.sbyte_14 + right.register.sbyte_14;
					result.register.sbyte_15 = left.register.sbyte_15 + right.register.sbyte_15;
				}
				else if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = left.register.uint16_0 + right.register.uint16_0;
					result.register.uint16_1 = left.register.uint16_1 + right.register.uint16_1;
					result.register.uint16_2 = left.register.uint16_2 + right.register.uint16_2;
					result.register.uint16_3 = left.register.uint16_3 + right.register.uint16_3;
					result.register.uint16_4 = left.register.uint16_4 + right.register.uint16_4;
					result.register.uint16_5 = left.register.uint16_5 + right.register.uint16_5;
					result.register.uint16_6 = left.register.uint16_6 + right.register.uint16_6;
					result.register.uint16_7 = left.register.uint16_7 + right.register.uint16_7;
				}
				else if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = left.register.int16_0 + right.register.int16_0;
					result.register.int16_1 = left.register.int16_1 + right.register.int16_1;
					result.register.int16_2 = left.register.int16_2 + right.register.int16_2;
					result.register.int16_3 = left.register.int16_3 + right.register.int16_3;
					result.register.int16_4 = left.register.int16_4 + right.register.int16_4;
					result.register.int16_5 = left.register.int16_5 + right.register.int16_5;
					result.register.int16_6 = left.register.int16_6 + right.register.int16_6;
					result.register.int16_7 = left.register.int16_7 + right.register.int16_7;
				}
				else if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = left.register.uint32_0 + right.register.uint32_0;
					result.register.uint32_1 = left.register.uint32_1 + right.register.uint32_1;
					result.register.uint32_2 = left.register.uint32_2 + right.register.uint32_2;
					result.register.uint32_3 = left.register.uint32_3 + right.register.uint32_3;
				}
				else if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = left.register.int32_0 + right.register.int32_0;
					result.register.int32_1 = left.register.int32_1 + right.register.int32_1;
					result.register.int32_2 = left.register.int32_2 + right.register.int32_2;
					result.register.int32_3 = left.register.int32_3 + right.register.int32_3;
				}
				else if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = left.register.uint64_0 + right.register.uint64_0;
					result.register.uint64_1 = left.register.uint64_1 + right.register.uint64_1;
				}
				else if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = left.register.int64_0 + right.register.int64_0;
					result.register.int64_1 = left.register.int64_1 + right.register.int64_1;
				}
				else if (typeof(T) == typeof(float))
				{
					result.register.single_0 = left.register.single_0 + right.register.single_0;
					result.register.single_1 = left.register.single_1 + right.register.single_1;
					result.register.single_2 = left.register.single_2 + right.register.single_2;
					result.register.single_3 = left.register.single_3 + right.register.single_3;
				}
				else if (typeof(T) == typeof(double))
				{
					result.register.double_0 = left.register.double_0 + right.register.double_0;
					result.register.double_1 = left.register.double_1 + right.register.double_1;
				}
				return result;
			}
			if (typeof(T) == typeof(byte))
			{
				byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					ptr[i] = (byte)((object)Vector<T>.ScalarAdd(left[i], right[i]));
				}
				return new Vector<T>((void*)ptr);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
				for (int j = 0; j < Vector<T>.Count; j++)
				{
					ptr2[j] = (sbyte)((object)Vector<T>.ScalarAdd(left[j], right[j]));
				}
				return new Vector<T>((void*)ptr2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int k = 0; k < Vector<T>.Count; k++)
				{
					ptr3[k] = (ushort)((object)Vector<T>.ScalarAdd(left[k], right[k]));
				}
				return new Vector<T>((void*)ptr3);
			}
			if (typeof(T) == typeof(short))
			{
				short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int l = 0; l < Vector<T>.Count; l++)
				{
					ptr4[l] = (short)((object)Vector<T>.ScalarAdd(left[l], right[l]));
				}
				return new Vector<T>((void*)ptr4);
			}
			if (typeof(T) == typeof(uint))
			{
				uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int m = 0; m < Vector<T>.Count; m++)
				{
					ptr5[m] = (uint)((object)Vector<T>.ScalarAdd(left[m], right[m]));
				}
				return new Vector<T>((void*)ptr5);
			}
			if (typeof(T) == typeof(int))
			{
				int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int n = 0; n < Vector<T>.Count; n++)
				{
					ptr6[n] = (int)((object)Vector<T>.ScalarAdd(left[n], right[n]));
				}
				return new Vector<T>((void*)ptr6);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num = 0; num < Vector<T>.Count; num++)
				{
					ptr7[num] = (ulong)((object)Vector<T>.ScalarAdd(left[num], right[num]));
				}
				return new Vector<T>((void*)ptr7);
			}
			if (typeof(T) == typeof(long))
			{
				long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num2 = 0; num2 < Vector<T>.Count; num2++)
				{
					ptr8[num2] = (long)((object)Vector<T>.ScalarAdd(left[num2], right[num2]));
				}
				return new Vector<T>((void*)ptr8);
			}
			if (typeof(T) == typeof(float))
			{
				float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int num3 = 0; num3 < Vector<T>.Count; num3++)
				{
					ptr9[num3] = (float)((object)Vector<T>.ScalarAdd(left[num3], right[num3]));
				}
				return new Vector<T>((void*)ptr9);
			}
			if (typeof(T) == typeof(double))
			{
				double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num4 = 0; num4 < Vector<T>.Count; num4++)
				{
					ptr10[num4] = (double)((object)Vector<T>.ScalarAdd(left[num4], right[num4]));
				}
				return new Vector<T>((void*)ptr10);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0010CC74 File Offset: 0x0010BE74
		[Intrinsic]
		public unsafe static Vector<T>operator -(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (!Vector.IsHardwareAccelerated)
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = left.register.byte_0 - right.register.byte_0;
					result.register.byte_1 = left.register.byte_1 - right.register.byte_1;
					result.register.byte_2 = left.register.byte_2 - right.register.byte_2;
					result.register.byte_3 = left.register.byte_3 - right.register.byte_3;
					result.register.byte_4 = left.register.byte_4 - right.register.byte_4;
					result.register.byte_5 = left.register.byte_5 - right.register.byte_5;
					result.register.byte_6 = left.register.byte_6 - right.register.byte_6;
					result.register.byte_7 = left.register.byte_7 - right.register.byte_7;
					result.register.byte_8 = left.register.byte_8 - right.register.byte_8;
					result.register.byte_9 = left.register.byte_9 - right.register.byte_9;
					result.register.byte_10 = left.register.byte_10 - right.register.byte_10;
					result.register.byte_11 = left.register.byte_11 - right.register.byte_11;
					result.register.byte_12 = left.register.byte_12 - right.register.byte_12;
					result.register.byte_13 = left.register.byte_13 - right.register.byte_13;
					result.register.byte_14 = left.register.byte_14 - right.register.byte_14;
					result.register.byte_15 = left.register.byte_15 - right.register.byte_15;
				}
				else if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = left.register.sbyte_0 - right.register.sbyte_0;
					result.register.sbyte_1 = left.register.sbyte_1 - right.register.sbyte_1;
					result.register.sbyte_2 = left.register.sbyte_2 - right.register.sbyte_2;
					result.register.sbyte_3 = left.register.sbyte_3 - right.register.sbyte_3;
					result.register.sbyte_4 = left.register.sbyte_4 - right.register.sbyte_4;
					result.register.sbyte_5 = left.register.sbyte_5 - right.register.sbyte_5;
					result.register.sbyte_6 = left.register.sbyte_6 - right.register.sbyte_6;
					result.register.sbyte_7 = left.register.sbyte_7 - right.register.sbyte_7;
					result.register.sbyte_8 = left.register.sbyte_8 - right.register.sbyte_8;
					result.register.sbyte_9 = left.register.sbyte_9 - right.register.sbyte_9;
					result.register.sbyte_10 = left.register.sbyte_10 - right.register.sbyte_10;
					result.register.sbyte_11 = left.register.sbyte_11 - right.register.sbyte_11;
					result.register.sbyte_12 = left.register.sbyte_12 - right.register.sbyte_12;
					result.register.sbyte_13 = left.register.sbyte_13 - right.register.sbyte_13;
					result.register.sbyte_14 = left.register.sbyte_14 - right.register.sbyte_14;
					result.register.sbyte_15 = left.register.sbyte_15 - right.register.sbyte_15;
				}
				else if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = left.register.uint16_0 - right.register.uint16_0;
					result.register.uint16_1 = left.register.uint16_1 - right.register.uint16_1;
					result.register.uint16_2 = left.register.uint16_2 - right.register.uint16_2;
					result.register.uint16_3 = left.register.uint16_3 - right.register.uint16_3;
					result.register.uint16_4 = left.register.uint16_4 - right.register.uint16_4;
					result.register.uint16_5 = left.register.uint16_5 - right.register.uint16_5;
					result.register.uint16_6 = left.register.uint16_6 - right.register.uint16_6;
					result.register.uint16_7 = left.register.uint16_7 - right.register.uint16_7;
				}
				else if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = left.register.int16_0 - right.register.int16_0;
					result.register.int16_1 = left.register.int16_1 - right.register.int16_1;
					result.register.int16_2 = left.register.int16_2 - right.register.int16_2;
					result.register.int16_3 = left.register.int16_3 - right.register.int16_3;
					result.register.int16_4 = left.register.int16_4 - right.register.int16_4;
					result.register.int16_5 = left.register.int16_5 - right.register.int16_5;
					result.register.int16_6 = left.register.int16_6 - right.register.int16_6;
					result.register.int16_7 = left.register.int16_7 - right.register.int16_7;
				}
				else if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = left.register.uint32_0 - right.register.uint32_0;
					result.register.uint32_1 = left.register.uint32_1 - right.register.uint32_1;
					result.register.uint32_2 = left.register.uint32_2 - right.register.uint32_2;
					result.register.uint32_3 = left.register.uint32_3 - right.register.uint32_3;
				}
				else if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = left.register.int32_0 - right.register.int32_0;
					result.register.int32_1 = left.register.int32_1 - right.register.int32_1;
					result.register.int32_2 = left.register.int32_2 - right.register.int32_2;
					result.register.int32_3 = left.register.int32_3 - right.register.int32_3;
				}
				else if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = left.register.uint64_0 - right.register.uint64_0;
					result.register.uint64_1 = left.register.uint64_1 - right.register.uint64_1;
				}
				else if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = left.register.int64_0 - right.register.int64_0;
					result.register.int64_1 = left.register.int64_1 - right.register.int64_1;
				}
				else if (typeof(T) == typeof(float))
				{
					result.register.single_0 = left.register.single_0 - right.register.single_0;
					result.register.single_1 = left.register.single_1 - right.register.single_1;
					result.register.single_2 = left.register.single_2 - right.register.single_2;
					result.register.single_3 = left.register.single_3 - right.register.single_3;
				}
				else if (typeof(T) == typeof(double))
				{
					result.register.double_0 = left.register.double_0 - right.register.double_0;
					result.register.double_1 = left.register.double_1 - right.register.double_1;
				}
				return result;
			}
			if (typeof(T) == typeof(byte))
			{
				byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					ptr[i] = (byte)((object)Vector<T>.ScalarSubtract(left[i], right[i]));
				}
				return new Vector<T>((void*)ptr);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
				for (int j = 0; j < Vector<T>.Count; j++)
				{
					ptr2[j] = (sbyte)((object)Vector<T>.ScalarSubtract(left[j], right[j]));
				}
				return new Vector<T>((void*)ptr2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int k = 0; k < Vector<T>.Count; k++)
				{
					ptr3[k] = (ushort)((object)Vector<T>.ScalarSubtract(left[k], right[k]));
				}
				return new Vector<T>((void*)ptr3);
			}
			if (typeof(T) == typeof(short))
			{
				short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int l = 0; l < Vector<T>.Count; l++)
				{
					ptr4[l] = (short)((object)Vector<T>.ScalarSubtract(left[l], right[l]));
				}
				return new Vector<T>((void*)ptr4);
			}
			if (typeof(T) == typeof(uint))
			{
				uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int m = 0; m < Vector<T>.Count; m++)
				{
					ptr5[m] = (uint)((object)Vector<T>.ScalarSubtract(left[m], right[m]));
				}
				return new Vector<T>((void*)ptr5);
			}
			if (typeof(T) == typeof(int))
			{
				int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int n = 0; n < Vector<T>.Count; n++)
				{
					ptr6[n] = (int)((object)Vector<T>.ScalarSubtract(left[n], right[n]));
				}
				return new Vector<T>((void*)ptr6);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num = 0; num < Vector<T>.Count; num++)
				{
					ptr7[num] = (ulong)((object)Vector<T>.ScalarSubtract(left[num], right[num]));
				}
				return new Vector<T>((void*)ptr7);
			}
			if (typeof(T) == typeof(long))
			{
				long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num2 = 0; num2 < Vector<T>.Count; num2++)
				{
					ptr8[num2] = (long)((object)Vector<T>.ScalarSubtract(left[num2], right[num2]));
				}
				return new Vector<T>((void*)ptr8);
			}
			if (typeof(T) == typeof(float))
			{
				float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int num3 = 0; num3 < Vector<T>.Count; num3++)
				{
					ptr9[num3] = (float)((object)Vector<T>.ScalarSubtract(left[num3], right[num3]));
				}
				return new Vector<T>((void*)ptr9);
			}
			if (typeof(T) == typeof(double))
			{
				double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num4 = 0; num4 < Vector<T>.Count; num4++)
				{
					ptr10[num4] = (double)((object)Vector<T>.ScalarSubtract(left[num4], right[num4]));
				}
				return new Vector<T>((void*)ptr10);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0010DB54 File Offset: 0x0010CD54
		[Intrinsic]
		public unsafe static Vector<T>operator *(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (!Vector.IsHardwareAccelerated)
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = left.register.byte_0 * right.register.byte_0;
					result.register.byte_1 = left.register.byte_1 * right.register.byte_1;
					result.register.byte_2 = left.register.byte_2 * right.register.byte_2;
					result.register.byte_3 = left.register.byte_3 * right.register.byte_3;
					result.register.byte_4 = left.register.byte_4 * right.register.byte_4;
					result.register.byte_5 = left.register.byte_5 * right.register.byte_5;
					result.register.byte_6 = left.register.byte_6 * right.register.byte_6;
					result.register.byte_7 = left.register.byte_7 * right.register.byte_7;
					result.register.byte_8 = left.register.byte_8 * right.register.byte_8;
					result.register.byte_9 = left.register.byte_9 * right.register.byte_9;
					result.register.byte_10 = left.register.byte_10 * right.register.byte_10;
					result.register.byte_11 = left.register.byte_11 * right.register.byte_11;
					result.register.byte_12 = left.register.byte_12 * right.register.byte_12;
					result.register.byte_13 = left.register.byte_13 * right.register.byte_13;
					result.register.byte_14 = left.register.byte_14 * right.register.byte_14;
					result.register.byte_15 = left.register.byte_15 * right.register.byte_15;
				}
				else if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = left.register.sbyte_0 * right.register.sbyte_0;
					result.register.sbyte_1 = left.register.sbyte_1 * right.register.sbyte_1;
					result.register.sbyte_2 = left.register.sbyte_2 * right.register.sbyte_2;
					result.register.sbyte_3 = left.register.sbyte_3 * right.register.sbyte_3;
					result.register.sbyte_4 = left.register.sbyte_4 * right.register.sbyte_4;
					result.register.sbyte_5 = left.register.sbyte_5 * right.register.sbyte_5;
					result.register.sbyte_6 = left.register.sbyte_6 * right.register.sbyte_6;
					result.register.sbyte_7 = left.register.sbyte_7 * right.register.sbyte_7;
					result.register.sbyte_8 = left.register.sbyte_8 * right.register.sbyte_8;
					result.register.sbyte_9 = left.register.sbyte_9 * right.register.sbyte_9;
					result.register.sbyte_10 = left.register.sbyte_10 * right.register.sbyte_10;
					result.register.sbyte_11 = left.register.sbyte_11 * right.register.sbyte_11;
					result.register.sbyte_12 = left.register.sbyte_12 * right.register.sbyte_12;
					result.register.sbyte_13 = left.register.sbyte_13 * right.register.sbyte_13;
					result.register.sbyte_14 = left.register.sbyte_14 * right.register.sbyte_14;
					result.register.sbyte_15 = left.register.sbyte_15 * right.register.sbyte_15;
				}
				else if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = left.register.uint16_0 * right.register.uint16_0;
					result.register.uint16_1 = left.register.uint16_1 * right.register.uint16_1;
					result.register.uint16_2 = left.register.uint16_2 * right.register.uint16_2;
					result.register.uint16_3 = left.register.uint16_3 * right.register.uint16_3;
					result.register.uint16_4 = left.register.uint16_4 * right.register.uint16_4;
					result.register.uint16_5 = left.register.uint16_5 * right.register.uint16_5;
					result.register.uint16_6 = left.register.uint16_6 * right.register.uint16_6;
					result.register.uint16_7 = left.register.uint16_7 * right.register.uint16_7;
				}
				else if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = left.register.int16_0 * right.register.int16_0;
					result.register.int16_1 = left.register.int16_1 * right.register.int16_1;
					result.register.int16_2 = left.register.int16_2 * right.register.int16_2;
					result.register.int16_3 = left.register.int16_3 * right.register.int16_3;
					result.register.int16_4 = left.register.int16_4 * right.register.int16_4;
					result.register.int16_5 = left.register.int16_5 * right.register.int16_5;
					result.register.int16_6 = left.register.int16_6 * right.register.int16_6;
					result.register.int16_7 = left.register.int16_7 * right.register.int16_7;
				}
				else if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = left.register.uint32_0 * right.register.uint32_0;
					result.register.uint32_1 = left.register.uint32_1 * right.register.uint32_1;
					result.register.uint32_2 = left.register.uint32_2 * right.register.uint32_2;
					result.register.uint32_3 = left.register.uint32_3 * right.register.uint32_3;
				}
				else if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = left.register.int32_0 * right.register.int32_0;
					result.register.int32_1 = left.register.int32_1 * right.register.int32_1;
					result.register.int32_2 = left.register.int32_2 * right.register.int32_2;
					result.register.int32_3 = left.register.int32_3 * right.register.int32_3;
				}
				else if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = left.register.uint64_0 * right.register.uint64_0;
					result.register.uint64_1 = left.register.uint64_1 * right.register.uint64_1;
				}
				else if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = left.register.int64_0 * right.register.int64_0;
					result.register.int64_1 = left.register.int64_1 * right.register.int64_1;
				}
				else if (typeof(T) == typeof(float))
				{
					result.register.single_0 = left.register.single_0 * right.register.single_0;
					result.register.single_1 = left.register.single_1 * right.register.single_1;
					result.register.single_2 = left.register.single_2 * right.register.single_2;
					result.register.single_3 = left.register.single_3 * right.register.single_3;
				}
				else if (typeof(T) == typeof(double))
				{
					result.register.double_0 = left.register.double_0 * right.register.double_0;
					result.register.double_1 = left.register.double_1 * right.register.double_1;
				}
				return result;
			}
			if (typeof(T) == typeof(byte))
			{
				byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					ptr[i] = (byte)((object)Vector<T>.ScalarMultiply(left[i], right[i]));
				}
				return new Vector<T>((void*)ptr);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
				for (int j = 0; j < Vector<T>.Count; j++)
				{
					ptr2[j] = (sbyte)((object)Vector<T>.ScalarMultiply(left[j], right[j]));
				}
				return new Vector<T>((void*)ptr2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int k = 0; k < Vector<T>.Count; k++)
				{
					ptr3[k] = (ushort)((object)Vector<T>.ScalarMultiply(left[k], right[k]));
				}
				return new Vector<T>((void*)ptr3);
			}
			if (typeof(T) == typeof(short))
			{
				short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int l = 0; l < Vector<T>.Count; l++)
				{
					ptr4[l] = (short)((object)Vector<T>.ScalarMultiply(left[l], right[l]));
				}
				return new Vector<T>((void*)ptr4);
			}
			if (typeof(T) == typeof(uint))
			{
				uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int m = 0; m < Vector<T>.Count; m++)
				{
					ptr5[m] = (uint)((object)Vector<T>.ScalarMultiply(left[m], right[m]));
				}
				return new Vector<T>((void*)ptr5);
			}
			if (typeof(T) == typeof(int))
			{
				int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int n = 0; n < Vector<T>.Count; n++)
				{
					ptr6[n] = (int)((object)Vector<T>.ScalarMultiply(left[n], right[n]));
				}
				return new Vector<T>((void*)ptr6);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num = 0; num < Vector<T>.Count; num++)
				{
					ptr7[num] = (ulong)((object)Vector<T>.ScalarMultiply(left[num], right[num]));
				}
				return new Vector<T>((void*)ptr7);
			}
			if (typeof(T) == typeof(long))
			{
				long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num2 = 0; num2 < Vector<T>.Count; num2++)
				{
					ptr8[num2] = (long)((object)Vector<T>.ScalarMultiply(left[num2], right[num2]));
				}
				return new Vector<T>((void*)ptr8);
			}
			if (typeof(T) == typeof(float))
			{
				float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int num3 = 0; num3 < Vector<T>.Count; num3++)
				{
					ptr9[num3] = (float)((object)Vector<T>.ScalarMultiply(left[num3], right[num3]));
				}
				return new Vector<T>((void*)ptr9);
			}
			if (typeof(T) == typeof(double))
			{
				double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num4 = 0; num4 < Vector<T>.Count; num4++)
				{
					ptr10[num4] = (double)((object)Vector<T>.ScalarMultiply(left[num4], right[num4]));
				}
				return new Vector<T>((void*)ptr10);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0010EA32 File Offset: 0x0010DC32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T>operator *(Vector<T> value, T factor)
		{
			return new Vector<T>(factor) * value;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0010EA40 File Offset: 0x0010DC40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T>operator *(T factor, Vector<T> value)
		{
			return new Vector<T>(factor) * value;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0010EA50 File Offset: 0x0010DC50
		[Intrinsic]
		public unsafe static Vector<T>operator /(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (!Vector.IsHardwareAccelerated)
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = left.register.byte_0 / right.register.byte_0;
					result.register.byte_1 = left.register.byte_1 / right.register.byte_1;
					result.register.byte_2 = left.register.byte_2 / right.register.byte_2;
					result.register.byte_3 = left.register.byte_3 / right.register.byte_3;
					result.register.byte_4 = left.register.byte_4 / right.register.byte_4;
					result.register.byte_5 = left.register.byte_5 / right.register.byte_5;
					result.register.byte_6 = left.register.byte_6 / right.register.byte_6;
					result.register.byte_7 = left.register.byte_7 / right.register.byte_7;
					result.register.byte_8 = left.register.byte_8 / right.register.byte_8;
					result.register.byte_9 = left.register.byte_9 / right.register.byte_9;
					result.register.byte_10 = left.register.byte_10 / right.register.byte_10;
					result.register.byte_11 = left.register.byte_11 / right.register.byte_11;
					result.register.byte_12 = left.register.byte_12 / right.register.byte_12;
					result.register.byte_13 = left.register.byte_13 / right.register.byte_13;
					result.register.byte_14 = left.register.byte_14 / right.register.byte_14;
					result.register.byte_15 = left.register.byte_15 / right.register.byte_15;
				}
				else if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = left.register.sbyte_0 / right.register.sbyte_0;
					result.register.sbyte_1 = left.register.sbyte_1 / right.register.sbyte_1;
					result.register.sbyte_2 = left.register.sbyte_2 / right.register.sbyte_2;
					result.register.sbyte_3 = left.register.sbyte_3 / right.register.sbyte_3;
					result.register.sbyte_4 = left.register.sbyte_4 / right.register.sbyte_4;
					result.register.sbyte_5 = left.register.sbyte_5 / right.register.sbyte_5;
					result.register.sbyte_6 = left.register.sbyte_6 / right.register.sbyte_6;
					result.register.sbyte_7 = left.register.sbyte_7 / right.register.sbyte_7;
					result.register.sbyte_8 = left.register.sbyte_8 / right.register.sbyte_8;
					result.register.sbyte_9 = left.register.sbyte_9 / right.register.sbyte_9;
					result.register.sbyte_10 = left.register.sbyte_10 / right.register.sbyte_10;
					result.register.sbyte_11 = left.register.sbyte_11 / right.register.sbyte_11;
					result.register.sbyte_12 = left.register.sbyte_12 / right.register.sbyte_12;
					result.register.sbyte_13 = left.register.sbyte_13 / right.register.sbyte_13;
					result.register.sbyte_14 = left.register.sbyte_14 / right.register.sbyte_14;
					result.register.sbyte_15 = left.register.sbyte_15 / right.register.sbyte_15;
				}
				else if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = left.register.uint16_0 / right.register.uint16_0;
					result.register.uint16_1 = left.register.uint16_1 / right.register.uint16_1;
					result.register.uint16_2 = left.register.uint16_2 / right.register.uint16_2;
					result.register.uint16_3 = left.register.uint16_3 / right.register.uint16_3;
					result.register.uint16_4 = left.register.uint16_4 / right.register.uint16_4;
					result.register.uint16_5 = left.register.uint16_5 / right.register.uint16_5;
					result.register.uint16_6 = left.register.uint16_6 / right.register.uint16_6;
					result.register.uint16_7 = left.register.uint16_7 / right.register.uint16_7;
				}
				else if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = left.register.int16_0 / right.register.int16_0;
					result.register.int16_1 = left.register.int16_1 / right.register.int16_1;
					result.register.int16_2 = left.register.int16_2 / right.register.int16_2;
					result.register.int16_3 = left.register.int16_3 / right.register.int16_3;
					result.register.int16_4 = left.register.int16_4 / right.register.int16_4;
					result.register.int16_5 = left.register.int16_5 / right.register.int16_5;
					result.register.int16_6 = left.register.int16_6 / right.register.int16_6;
					result.register.int16_7 = left.register.int16_7 / right.register.int16_7;
				}
				else if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = left.register.uint32_0 / right.register.uint32_0;
					result.register.uint32_1 = left.register.uint32_1 / right.register.uint32_1;
					result.register.uint32_2 = left.register.uint32_2 / right.register.uint32_2;
					result.register.uint32_3 = left.register.uint32_3 / right.register.uint32_3;
				}
				else if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = left.register.int32_0 / right.register.int32_0;
					result.register.int32_1 = left.register.int32_1 / right.register.int32_1;
					result.register.int32_2 = left.register.int32_2 / right.register.int32_2;
					result.register.int32_3 = left.register.int32_3 / right.register.int32_3;
				}
				else if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = left.register.uint64_0 / right.register.uint64_0;
					result.register.uint64_1 = left.register.uint64_1 / right.register.uint64_1;
				}
				else if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = left.register.int64_0 / right.register.int64_0;
					result.register.int64_1 = left.register.int64_1 / right.register.int64_1;
				}
				else if (typeof(T) == typeof(float))
				{
					result.register.single_0 = left.register.single_0 / right.register.single_0;
					result.register.single_1 = left.register.single_1 / right.register.single_1;
					result.register.single_2 = left.register.single_2 / right.register.single_2;
					result.register.single_3 = left.register.single_3 / right.register.single_3;
				}
				else if (typeof(T) == typeof(double))
				{
					result.register.double_0 = left.register.double_0 / right.register.double_0;
					result.register.double_1 = left.register.double_1 / right.register.double_1;
				}
				return result;
			}
			if (typeof(T) == typeof(byte))
			{
				byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					ptr[i] = (byte)((object)Vector<T>.ScalarDivide(left[i], right[i]));
				}
				return new Vector<T>((void*)ptr);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
				for (int j = 0; j < Vector<T>.Count; j++)
				{
					ptr2[j] = (sbyte)((object)Vector<T>.ScalarDivide(left[j], right[j]));
				}
				return new Vector<T>((void*)ptr2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int k = 0; k < Vector<T>.Count; k++)
				{
					ptr3[k] = (ushort)((object)Vector<T>.ScalarDivide(left[k], right[k]));
				}
				return new Vector<T>((void*)ptr3);
			}
			if (typeof(T) == typeof(short))
			{
				short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
				for (int l = 0; l < Vector<T>.Count; l++)
				{
					ptr4[l] = (short)((object)Vector<T>.ScalarDivide(left[l], right[l]));
				}
				return new Vector<T>((void*)ptr4);
			}
			if (typeof(T) == typeof(uint))
			{
				uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int m = 0; m < Vector<T>.Count; m++)
				{
					ptr5[m] = (uint)((object)Vector<T>.ScalarDivide(left[m], right[m]));
				}
				return new Vector<T>((void*)ptr5);
			}
			if (typeof(T) == typeof(int))
			{
				int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int n = 0; n < Vector<T>.Count; n++)
				{
					ptr6[n] = (int)((object)Vector<T>.ScalarDivide(left[n], right[n]));
				}
				return new Vector<T>((void*)ptr6);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num = 0; num < Vector<T>.Count; num++)
				{
					ptr7[num] = (ulong)((object)Vector<T>.ScalarDivide(left[num], right[num]));
				}
				return new Vector<T>((void*)ptr7);
			}
			if (typeof(T) == typeof(long))
			{
				long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num2 = 0; num2 < Vector<T>.Count; num2++)
				{
					ptr8[num2] = (long)((object)Vector<T>.ScalarDivide(left[num2], right[num2]));
				}
				return new Vector<T>((void*)ptr8);
			}
			if (typeof(T) == typeof(float))
			{
				float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
				for (int num3 = 0; num3 < Vector<T>.Count; num3++)
				{
					ptr9[num3] = (float)((object)Vector<T>.ScalarDivide(left[num3], right[num3]));
				}
				return new Vector<T>((void*)ptr9);
			}
			if (typeof(T) == typeof(double))
			{
				double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
				for (int num4 = 0; num4 < Vector<T>.Count; num4++)
				{
					ptr10[num4] = (double)((object)Vector<T>.ScalarDivide(left[num4], right[num4]));
				}
				return new Vector<T>((void*)ptr10);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0010F92E File Offset: 0x0010EB2E
		public static Vector<T>operator -(Vector<T> value)
		{
			return Vector<T>.Zero - value;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0010F93C File Offset: 0x0010EB3C
		[Intrinsic]
		public unsafe static Vector<T>operator &(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector<T> result = default(Vector<T>);
			if (Vector.IsHardwareAccelerated)
			{
				long* ptr = &result.register.int64_0;
				long* ptr2 = &left.register.int64_0;
				long* ptr3 = &right.register.int64_0;
				for (int i = 0; i < Vector<long>.Count; i++)
				{
					ptr[i] = (ptr2[i] & ptr3[i]);
				}
			}
			else
			{
				result.register.int64_0 = (left.register.int64_0 & right.register.int64_0);
				result.register.int64_1 = (left.register.int64_1 & right.register.int64_1);
			}
			return result;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0010FA00 File Offset: 0x0010EC00
		[Intrinsic]
		public unsafe static Vector<T>operator |(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector<T> result = default(Vector<T>);
			if (Vector.IsHardwareAccelerated)
			{
				long* ptr = &result.register.int64_0;
				long* ptr2 = &left.register.int64_0;
				long* ptr3 = &right.register.int64_0;
				for (int i = 0; i < Vector<long>.Count; i++)
				{
					ptr[i] = (ptr2[i] | ptr3[i]);
				}
			}
			else
			{
				result.register.int64_0 = (left.register.int64_0 | right.register.int64_0);
				result.register.int64_1 = (left.register.int64_1 | right.register.int64_1);
			}
			return result;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0010FAC4 File Offset: 0x0010ECC4
		[Intrinsic]
		public unsafe static Vector<T>operator ^(Vector<T> left, Vector<T> right)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector<T> result = default(Vector<T>);
			if (Vector.IsHardwareAccelerated)
			{
				long* ptr = &result.register.int64_0;
				long* ptr2 = &left.register.int64_0;
				long* ptr3 = &right.register.int64_0;
				for (int i = 0; i < Vector<long>.Count; i++)
				{
					ptr[i] = (ptr2[i] ^ ptr3[i]);
				}
			}
			else
			{
				result.register.int64_0 = (left.register.int64_0 ^ right.register.int64_0);
				result.register.int64_1 = (left.register.int64_1 ^ right.register.int64_1);
			}
			return result;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0010FB85 File Offset: 0x0010ED85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T>operator ~(Vector<T> value)
		{
			return Vector<T>.AllBitsSet ^ value;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0010FB94 File Offset: 0x0010ED94
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					if (!Vector<T>.ScalarEquals(left[i], right[i]))
					{
						return false;
					}
				}
				return true;
			}
			if (typeof(T) == typeof(byte))
			{
				return left.register.byte_0 == right.register.byte_0 && left.register.byte_1 == right.register.byte_1 && left.register.byte_2 == right.register.byte_2 && left.register.byte_3 == right.register.byte_3 && left.register.byte_4 == right.register.byte_4 && left.register.byte_5 == right.register.byte_5 && left.register.byte_6 == right.register.byte_6 && left.register.byte_7 == right.register.byte_7 && left.register.byte_8 == right.register.byte_8 && left.register.byte_9 == right.register.byte_9 && left.register.byte_10 == right.register.byte_10 && left.register.byte_11 == right.register.byte_11 && left.register.byte_12 == right.register.byte_12 && left.register.byte_13 == right.register.byte_13 && left.register.byte_14 == right.register.byte_14 && left.register.byte_15 == right.register.byte_15;
			}
			if (typeof(T) == typeof(sbyte))
			{
				return left.register.sbyte_0 == right.register.sbyte_0 && left.register.sbyte_1 == right.register.sbyte_1 && left.register.sbyte_2 == right.register.sbyte_2 && left.register.sbyte_3 == right.register.sbyte_3 && left.register.sbyte_4 == right.register.sbyte_4 && left.register.sbyte_5 == right.register.sbyte_5 && left.register.sbyte_6 == right.register.sbyte_6 && left.register.sbyte_7 == right.register.sbyte_7 && left.register.sbyte_8 == right.register.sbyte_8 && left.register.sbyte_9 == right.register.sbyte_9 && left.register.sbyte_10 == right.register.sbyte_10 && left.register.sbyte_11 == right.register.sbyte_11 && left.register.sbyte_12 == right.register.sbyte_12 && left.register.sbyte_13 == right.register.sbyte_13 && left.register.sbyte_14 == right.register.sbyte_14 && left.register.sbyte_15 == right.register.sbyte_15;
			}
			if (typeof(T) == typeof(ushort))
			{
				return left.register.uint16_0 == right.register.uint16_0 && left.register.uint16_1 == right.register.uint16_1 && left.register.uint16_2 == right.register.uint16_2 && left.register.uint16_3 == right.register.uint16_3 && left.register.uint16_4 == right.register.uint16_4 && left.register.uint16_5 == right.register.uint16_5 && left.register.uint16_6 == right.register.uint16_6 && left.register.uint16_7 == right.register.uint16_7;
			}
			if (typeof(T) == typeof(short))
			{
				return left.register.int16_0 == right.register.int16_0 && left.register.int16_1 == right.register.int16_1 && left.register.int16_2 == right.register.int16_2 && left.register.int16_3 == right.register.int16_3 && left.register.int16_4 == right.register.int16_4 && left.register.int16_5 == right.register.int16_5 && left.register.int16_6 == right.register.int16_6 && left.register.int16_7 == right.register.int16_7;
			}
			if (typeof(T) == typeof(uint))
			{
				return left.register.uint32_0 == right.register.uint32_0 && left.register.uint32_1 == right.register.uint32_1 && left.register.uint32_2 == right.register.uint32_2 && left.register.uint32_3 == right.register.uint32_3;
			}
			if (typeof(T) == typeof(int))
			{
				return left.register.int32_0 == right.register.int32_0 && left.register.int32_1 == right.register.int32_1 && left.register.int32_2 == right.register.int32_2 && left.register.int32_3 == right.register.int32_3;
			}
			if (typeof(T) == typeof(ulong))
			{
				return left.register.uint64_0 == right.register.uint64_0 && left.register.uint64_1 == right.register.uint64_1;
			}
			if (typeof(T) == typeof(long))
			{
				return left.register.int64_0 == right.register.int64_0 && left.register.int64_1 == right.register.int64_1;
			}
			if (typeof(T) == typeof(float))
			{
				return left.register.single_0 == right.register.single_0 && left.register.single_1 == right.register.single_1 && left.register.single_2 == right.register.single_2 && left.register.single_3 == right.register.single_3;
			}
			if (typeof(T) == typeof(double))
			{
				return left.register.double_0 == right.register.double_0 && left.register.double_1 == right.register.double_1;
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0011038D File Offset: 0x0010F58D
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector<T> left, Vector<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00110399 File Offset: 0x0010F599
		[Intrinsic]
		public static explicit operator Vector<byte>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<byte>(ref value.register);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x001103AC File Offset: 0x0010F5AC
		[Intrinsic]
		[CLSCompliant(false)]
		public static explicit operator Vector<sbyte>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<sbyte>(ref value.register);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x001103BF File Offset: 0x0010F5BF
		[CLSCompliant(false)]
		[Intrinsic]
		public static explicit operator Vector<ushort>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<ushort>(ref value.register);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x001103D2 File Offset: 0x0010F5D2
		[Intrinsic]
		public static explicit operator Vector<short>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<short>(ref value.register);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x001103E5 File Offset: 0x0010F5E5
		[Intrinsic]
		[CLSCompliant(false)]
		public static explicit operator Vector<uint>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<uint>(ref value.register);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x001103F8 File Offset: 0x0010F5F8
		[Intrinsic]
		public static explicit operator Vector<int>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<int>(ref value.register);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0011040B File Offset: 0x0010F60B
		[CLSCompliant(false)]
		[Intrinsic]
		public static explicit operator Vector<ulong>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<ulong>(ref value.register);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0011041E File Offset: 0x0010F61E
		[Intrinsic]
		public static explicit operator Vector<long>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<long>(ref value.register);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00110431 File Offset: 0x0010F631
		[Intrinsic]
		public static explicit operator Vector<float>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<float>(ref value.register);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00110444 File Offset: 0x0010F644
		[Intrinsic]
		public static explicit operator Vector<double>(Vector<T> value)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return new Vector<double>(ref value.register);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00110458 File Offset: 0x0010F658
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Vector<T> Equals(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarEquals(left[i], right[i]) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarEquals(left[j], right[j]) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarEquals(left[k], right[k]) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarEquals(left[l], right[l]) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarEquals(left[m], right[m]) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarEquals(left[n], right[n]) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarEquals(left[num], right[num]) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarEquals(left[num2], right[num2]) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarEquals(left[num3], right[num3]) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarEquals(left[num4], right[num4]) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				Register register = default(Register);
				if (typeof(T) == typeof(byte))
				{
					register.byte_0 = ((left.register.byte_0 == right.register.byte_0) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_1 = ((left.register.byte_1 == right.register.byte_1) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_2 = ((left.register.byte_2 == right.register.byte_2) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_3 = ((left.register.byte_3 == right.register.byte_3) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_4 = ((left.register.byte_4 == right.register.byte_4) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_5 = ((left.register.byte_5 == right.register.byte_5) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_6 = ((left.register.byte_6 == right.register.byte_6) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_7 = ((left.register.byte_7 == right.register.byte_7) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_8 = ((left.register.byte_8 == right.register.byte_8) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_9 = ((left.register.byte_9 == right.register.byte_9) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_10 = ((left.register.byte_10 == right.register.byte_10) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_11 = ((left.register.byte_11 == right.register.byte_11) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_12 = ((left.register.byte_12 == right.register.byte_12) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_13 = ((left.register.byte_13 == right.register.byte_13) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_14 = ((left.register.byte_14 == right.register.byte_14) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_15 = ((left.register.byte_15 == right.register.byte_15) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(sbyte))
				{
					register.sbyte_0 = ((left.register.sbyte_0 == right.register.sbyte_0) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_1 = ((left.register.sbyte_1 == right.register.sbyte_1) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_2 = ((left.register.sbyte_2 == right.register.sbyte_2) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_3 = ((left.register.sbyte_3 == right.register.sbyte_3) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_4 = ((left.register.sbyte_4 == right.register.sbyte_4) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_5 = ((left.register.sbyte_5 == right.register.sbyte_5) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_6 = ((left.register.sbyte_6 == right.register.sbyte_6) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_7 = ((left.register.sbyte_7 == right.register.sbyte_7) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_8 = ((left.register.sbyte_8 == right.register.sbyte_8) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_9 = ((left.register.sbyte_9 == right.register.sbyte_9) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_10 = ((left.register.sbyte_10 == right.register.sbyte_10) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_11 = ((left.register.sbyte_11 == right.register.sbyte_11) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_12 = ((left.register.sbyte_12 == right.register.sbyte_12) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_13 = ((left.register.sbyte_13 == right.register.sbyte_13) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_14 = ((left.register.sbyte_14 == right.register.sbyte_14) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_15 = ((left.register.sbyte_15 == right.register.sbyte_15) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ushort))
				{
					register.uint16_0 = ((left.register.uint16_0 == right.register.uint16_0) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_1 = ((left.register.uint16_1 == right.register.uint16_1) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_2 = ((left.register.uint16_2 == right.register.uint16_2) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_3 = ((left.register.uint16_3 == right.register.uint16_3) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_4 = ((left.register.uint16_4 == right.register.uint16_4) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_5 = ((left.register.uint16_5 == right.register.uint16_5) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_6 = ((left.register.uint16_6 == right.register.uint16_6) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_7 = ((left.register.uint16_7 == right.register.uint16_7) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(short))
				{
					register.int16_0 = ((left.register.int16_0 == right.register.int16_0) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_1 = ((left.register.int16_1 == right.register.int16_1) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_2 = ((left.register.int16_2 == right.register.int16_2) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_3 = ((left.register.int16_3 == right.register.int16_3) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_4 = ((left.register.int16_4 == right.register.int16_4) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_5 = ((left.register.int16_5 == right.register.int16_5) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_6 = ((left.register.int16_6 == right.register.int16_6) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_7 = ((left.register.int16_7 == right.register.int16_7) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(uint))
				{
					register.uint32_0 = ((left.register.uint32_0 == right.register.uint32_0) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_1 = ((left.register.uint32_1 == right.register.uint32_1) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_2 = ((left.register.uint32_2 == right.register.uint32_2) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_3 = ((left.register.uint32_3 == right.register.uint32_3) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(int))
				{
					register.int32_0 = ((left.register.int32_0 == right.register.int32_0) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_1 = ((left.register.int32_1 == right.register.int32_1) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_2 = ((left.register.int32_2 == right.register.int32_2) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_3 = ((left.register.int32_3 == right.register.int32_3) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ulong))
				{
					register.uint64_0 = ((left.register.uint64_0 == right.register.uint64_0) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					register.uint64_1 = ((left.register.uint64_1 == right.register.uint64_1) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(long))
				{
					register.int64_0 = ((left.register.int64_0 == right.register.int64_0) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					register.int64_1 = ((left.register.int64_1 == right.register.int64_1) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(float))
				{
					register.single_0 = ((left.register.single_0 == right.register.single_0) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_1 = ((left.register.single_1 == right.register.single_1) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_2 = ((left.register.single_2 == right.register.single_2) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_3 = ((left.register.single_3 == right.register.single_3) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(double))
				{
					register.double_0 = ((left.register.double_0 == right.register.double_0) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					register.double_1 = ((left.register.double_1 == right.register.double_1) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					return new Vector<T>(ref register);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00111464 File Offset: 0x00110664
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Vector<T> LessThan(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarLessThan(left[i], right[i]) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarLessThan(left[j], right[j]) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarLessThan(left[k], right[k]) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarLessThan(left[l], right[l]) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarLessThan(left[m], right[m]) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarLessThan(left[n], right[n]) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarLessThan(left[num], right[num]) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarLessThan(left[num2], right[num2]) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarLessThan(left[num3], right[num3]) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarLessThan(left[num4], right[num4]) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				Register register = default(Register);
				if (typeof(T) == typeof(byte))
				{
					register.byte_0 = ((left.register.byte_0 < right.register.byte_0) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_1 = ((left.register.byte_1 < right.register.byte_1) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_2 = ((left.register.byte_2 < right.register.byte_2) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_3 = ((left.register.byte_3 < right.register.byte_3) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_4 = ((left.register.byte_4 < right.register.byte_4) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_5 = ((left.register.byte_5 < right.register.byte_5) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_6 = ((left.register.byte_6 < right.register.byte_6) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_7 = ((left.register.byte_7 < right.register.byte_7) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_8 = ((left.register.byte_8 < right.register.byte_8) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_9 = ((left.register.byte_9 < right.register.byte_9) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_10 = ((left.register.byte_10 < right.register.byte_10) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_11 = ((left.register.byte_11 < right.register.byte_11) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_12 = ((left.register.byte_12 < right.register.byte_12) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_13 = ((left.register.byte_13 < right.register.byte_13) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_14 = ((left.register.byte_14 < right.register.byte_14) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_15 = ((left.register.byte_15 < right.register.byte_15) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(sbyte))
				{
					register.sbyte_0 = ((left.register.sbyte_0 < right.register.sbyte_0) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_1 = ((left.register.sbyte_1 < right.register.sbyte_1) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_2 = ((left.register.sbyte_2 < right.register.sbyte_2) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_3 = ((left.register.sbyte_3 < right.register.sbyte_3) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_4 = ((left.register.sbyte_4 < right.register.sbyte_4) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_5 = ((left.register.sbyte_5 < right.register.sbyte_5) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_6 = ((left.register.sbyte_6 < right.register.sbyte_6) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_7 = ((left.register.sbyte_7 < right.register.sbyte_7) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_8 = ((left.register.sbyte_8 < right.register.sbyte_8) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_9 = ((left.register.sbyte_9 < right.register.sbyte_9) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_10 = ((left.register.sbyte_10 < right.register.sbyte_10) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_11 = ((left.register.sbyte_11 < right.register.sbyte_11) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_12 = ((left.register.sbyte_12 < right.register.sbyte_12) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_13 = ((left.register.sbyte_13 < right.register.sbyte_13) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_14 = ((left.register.sbyte_14 < right.register.sbyte_14) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_15 = ((left.register.sbyte_15 < right.register.sbyte_15) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ushort))
				{
					register.uint16_0 = ((left.register.uint16_0 < right.register.uint16_0) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_1 = ((left.register.uint16_1 < right.register.uint16_1) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_2 = ((left.register.uint16_2 < right.register.uint16_2) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_3 = ((left.register.uint16_3 < right.register.uint16_3) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_4 = ((left.register.uint16_4 < right.register.uint16_4) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_5 = ((left.register.uint16_5 < right.register.uint16_5) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_6 = ((left.register.uint16_6 < right.register.uint16_6) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_7 = ((left.register.uint16_7 < right.register.uint16_7) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(short))
				{
					register.int16_0 = ((left.register.int16_0 < right.register.int16_0) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_1 = ((left.register.int16_1 < right.register.int16_1) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_2 = ((left.register.int16_2 < right.register.int16_2) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_3 = ((left.register.int16_3 < right.register.int16_3) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_4 = ((left.register.int16_4 < right.register.int16_4) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_5 = ((left.register.int16_5 < right.register.int16_5) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_6 = ((left.register.int16_6 < right.register.int16_6) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_7 = ((left.register.int16_7 < right.register.int16_7) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(uint))
				{
					register.uint32_0 = ((left.register.uint32_0 < right.register.uint32_0) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_1 = ((left.register.uint32_1 < right.register.uint32_1) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_2 = ((left.register.uint32_2 < right.register.uint32_2) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_3 = ((left.register.uint32_3 < right.register.uint32_3) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(int))
				{
					register.int32_0 = ((left.register.int32_0 < right.register.int32_0) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_1 = ((left.register.int32_1 < right.register.int32_1) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_2 = ((left.register.int32_2 < right.register.int32_2) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_3 = ((left.register.int32_3 < right.register.int32_3) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ulong))
				{
					register.uint64_0 = ((left.register.uint64_0 < right.register.uint64_0) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					register.uint64_1 = ((left.register.uint64_1 < right.register.uint64_1) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(long))
				{
					register.int64_0 = ((left.register.int64_0 < right.register.int64_0) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					register.int64_1 = ((left.register.int64_1 < right.register.int64_1) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(float))
				{
					register.single_0 = ((left.register.single_0 < right.register.single_0) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_1 = ((left.register.single_1 < right.register.single_1) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_2 = ((left.register.single_2 < right.register.single_2) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_3 = ((left.register.single_3 < right.register.single_3) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(double))
				{
					register.double_0 = ((left.register.double_0 < right.register.double_0) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					register.double_1 = ((left.register.double_1 < right.register.double_1) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					return new Vector<T>(ref register);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00112470 File Offset: 0x00111670
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Vector<T> GreaterThan(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarGreaterThan(left[i], right[i]) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarGreaterThan(left[j], right[j]) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarGreaterThan(left[k], right[k]) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarGreaterThan(left[l], right[l]) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarGreaterThan(left[m], right[m]) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarGreaterThan(left[n], right[n]) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarGreaterThan(left[num], right[num]) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarGreaterThan(left[num2], right[num2]) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarGreaterThan(left[num3], right[num3]) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarGreaterThan(left[num4], right[num4]) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				Register register = default(Register);
				if (typeof(T) == typeof(byte))
				{
					register.byte_0 = ((left.register.byte_0 > right.register.byte_0) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_1 = ((left.register.byte_1 > right.register.byte_1) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_2 = ((left.register.byte_2 > right.register.byte_2) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_3 = ((left.register.byte_3 > right.register.byte_3) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_4 = ((left.register.byte_4 > right.register.byte_4) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_5 = ((left.register.byte_5 > right.register.byte_5) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_6 = ((left.register.byte_6 > right.register.byte_6) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_7 = ((left.register.byte_7 > right.register.byte_7) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_8 = ((left.register.byte_8 > right.register.byte_8) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_9 = ((left.register.byte_9 > right.register.byte_9) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_10 = ((left.register.byte_10 > right.register.byte_10) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_11 = ((left.register.byte_11 > right.register.byte_11) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_12 = ((left.register.byte_12 > right.register.byte_12) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_13 = ((left.register.byte_13 > right.register.byte_13) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_14 = ((left.register.byte_14 > right.register.byte_14) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_15 = ((left.register.byte_15 > right.register.byte_15) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(sbyte))
				{
					register.sbyte_0 = ((left.register.sbyte_0 > right.register.sbyte_0) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_1 = ((left.register.sbyte_1 > right.register.sbyte_1) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_2 = ((left.register.sbyte_2 > right.register.sbyte_2) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_3 = ((left.register.sbyte_3 > right.register.sbyte_3) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_4 = ((left.register.sbyte_4 > right.register.sbyte_4) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_5 = ((left.register.sbyte_5 > right.register.sbyte_5) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_6 = ((left.register.sbyte_6 > right.register.sbyte_6) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_7 = ((left.register.sbyte_7 > right.register.sbyte_7) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_8 = ((left.register.sbyte_8 > right.register.sbyte_8) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_9 = ((left.register.sbyte_9 > right.register.sbyte_9) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_10 = ((left.register.sbyte_10 > right.register.sbyte_10) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_11 = ((left.register.sbyte_11 > right.register.sbyte_11) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_12 = ((left.register.sbyte_12 > right.register.sbyte_12) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_13 = ((left.register.sbyte_13 > right.register.sbyte_13) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_14 = ((left.register.sbyte_14 > right.register.sbyte_14) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_15 = ((left.register.sbyte_15 > right.register.sbyte_15) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ushort))
				{
					register.uint16_0 = ((left.register.uint16_0 > right.register.uint16_0) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_1 = ((left.register.uint16_1 > right.register.uint16_1) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_2 = ((left.register.uint16_2 > right.register.uint16_2) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_3 = ((left.register.uint16_3 > right.register.uint16_3) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_4 = ((left.register.uint16_4 > right.register.uint16_4) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_5 = ((left.register.uint16_5 > right.register.uint16_5) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_6 = ((left.register.uint16_6 > right.register.uint16_6) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_7 = ((left.register.uint16_7 > right.register.uint16_7) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(short))
				{
					register.int16_0 = ((left.register.int16_0 > right.register.int16_0) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_1 = ((left.register.int16_1 > right.register.int16_1) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_2 = ((left.register.int16_2 > right.register.int16_2) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_3 = ((left.register.int16_3 > right.register.int16_3) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_4 = ((left.register.int16_4 > right.register.int16_4) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_5 = ((left.register.int16_5 > right.register.int16_5) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_6 = ((left.register.int16_6 > right.register.int16_6) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_7 = ((left.register.int16_7 > right.register.int16_7) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(uint))
				{
					register.uint32_0 = ((left.register.uint32_0 > right.register.uint32_0) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_1 = ((left.register.uint32_1 > right.register.uint32_1) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_2 = ((left.register.uint32_2 > right.register.uint32_2) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_3 = ((left.register.uint32_3 > right.register.uint32_3) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(int))
				{
					register.int32_0 = ((left.register.int32_0 > right.register.int32_0) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_1 = ((left.register.int32_1 > right.register.int32_1) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_2 = ((left.register.int32_2 > right.register.int32_2) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_3 = ((left.register.int32_3 > right.register.int32_3) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ulong))
				{
					register.uint64_0 = ((left.register.uint64_0 > right.register.uint64_0) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					register.uint64_1 = ((left.register.uint64_1 > right.register.uint64_1) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(long))
				{
					register.int64_0 = ((left.register.int64_0 > right.register.int64_0) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					register.int64_1 = ((left.register.int64_1 > right.register.int64_1) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(float))
				{
					register.single_0 = ((left.register.single_0 > right.register.single_0) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_1 = ((left.register.single_1 > right.register.single_1) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_2 = ((left.register.single_2 > right.register.single_2) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_3 = ((left.register.single_3 > right.register.single_3) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(double))
				{
					register.double_0 = ((left.register.double_0 > right.register.double_0) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					register.double_1 = ((left.register.double_1 > right.register.double_1) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					return new Vector<T>(ref register);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0011347B File Offset: 0x0011267B
		[Intrinsic]
		internal static Vector<T> GreaterThanOrEqual(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.Equals(left, right) | Vector<T>.GreaterThan(left, right);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00113490 File Offset: 0x00112690
		[Intrinsic]
		internal static Vector<T> LessThanOrEqual(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.Equals(left, right) | Vector<T>.LessThan(left, right);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x001134A5 File Offset: 0x001126A5
		[Intrinsic]
		internal static Vector<T> ConditionalSelect(Vector<T> condition, Vector<T> left, Vector<T> right)
		{
			return (left & condition) | Vector.AndNot<T>(right, condition);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x001134BC File Offset: 0x001126BC
		[Intrinsic]
		internal unsafe static Vector<T> Abs(Vector<T> value)
		{
			if (typeof(T) == typeof(byte))
			{
				return value;
			}
			if (typeof(T) == typeof(ushort))
			{
				return value;
			}
			if (typeof(T) == typeof(uint))
			{
				return value;
			}
			if (typeof(T) == typeof(ulong))
			{
				return value;
			}
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (sbyte)Math.Abs((sbyte)((object)value[i]));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr2 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (short)Math.Abs((short)((object)value[j]));
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr3 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (int)Math.Abs((int)((object)value[k]));
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr4 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (long)Math.Abs((long)((object)value[l]));
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr5 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (float)Math.Abs((float)((object)value[m]));
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr6 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (double)Math.Abs((double)((object)value[n]));
					}
					return new Vector<T>((void*)ptr6);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				if (typeof(T) == typeof(sbyte))
				{
					value.register.sbyte_0 = Math.Abs(value.register.sbyte_0);
					value.register.sbyte_1 = Math.Abs(value.register.sbyte_1);
					value.register.sbyte_2 = Math.Abs(value.register.sbyte_2);
					value.register.sbyte_3 = Math.Abs(value.register.sbyte_3);
					value.register.sbyte_4 = Math.Abs(value.register.sbyte_4);
					value.register.sbyte_5 = Math.Abs(value.register.sbyte_5);
					value.register.sbyte_6 = Math.Abs(value.register.sbyte_6);
					value.register.sbyte_7 = Math.Abs(value.register.sbyte_7);
					value.register.sbyte_8 = Math.Abs(value.register.sbyte_8);
					value.register.sbyte_9 = Math.Abs(value.register.sbyte_9);
					value.register.sbyte_10 = Math.Abs(value.register.sbyte_10);
					value.register.sbyte_11 = Math.Abs(value.register.sbyte_11);
					value.register.sbyte_12 = Math.Abs(value.register.sbyte_12);
					value.register.sbyte_13 = Math.Abs(value.register.sbyte_13);
					value.register.sbyte_14 = Math.Abs(value.register.sbyte_14);
					value.register.sbyte_15 = Math.Abs(value.register.sbyte_15);
					return value;
				}
				if (typeof(T) == typeof(short))
				{
					value.register.int16_0 = Math.Abs(value.register.int16_0);
					value.register.int16_1 = Math.Abs(value.register.int16_1);
					value.register.int16_2 = Math.Abs(value.register.int16_2);
					value.register.int16_3 = Math.Abs(value.register.int16_3);
					value.register.int16_4 = Math.Abs(value.register.int16_4);
					value.register.int16_5 = Math.Abs(value.register.int16_5);
					value.register.int16_6 = Math.Abs(value.register.int16_6);
					value.register.int16_7 = Math.Abs(value.register.int16_7);
					return value;
				}
				if (typeof(T) == typeof(int))
				{
					value.register.int32_0 = Math.Abs(value.register.int32_0);
					value.register.int32_1 = Math.Abs(value.register.int32_1);
					value.register.int32_2 = Math.Abs(value.register.int32_2);
					value.register.int32_3 = Math.Abs(value.register.int32_3);
					return value;
				}
				if (typeof(T) == typeof(long))
				{
					value.register.int64_0 = Math.Abs(value.register.int64_0);
					value.register.int64_1 = Math.Abs(value.register.int64_1);
					return value;
				}
				if (typeof(T) == typeof(float))
				{
					value.register.single_0 = Math.Abs(value.register.single_0);
					value.register.single_1 = Math.Abs(value.register.single_1);
					value.register.single_2 = Math.Abs(value.register.single_2);
					value.register.single_3 = Math.Abs(value.register.single_3);
					return value;
				}
				if (typeof(T) == typeof(double))
				{
					value.register.double_0 = Math.Abs(value.register.double_0);
					value.register.double_1 = Math.Abs(value.register.double_1);
					return value;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00113C84 File Offset: 0x00112E84
		[Intrinsic]
		internal unsafe static Vector<T> Min(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarLessThan(left[i], right[i]) ? ((byte)((object)left[i])) : ((byte)((object)right[i])));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarLessThan(left[j], right[j]) ? ((sbyte)((object)left[j])) : ((sbyte)((object)right[j])));
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarLessThan(left[k], right[k]) ? ((ushort)((object)left[k])) : ((ushort)((object)right[k])));
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarLessThan(left[l], right[l]) ? ((short)((object)left[l])) : ((short)((object)right[l])));
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarLessThan(left[m], right[m]) ? ((uint)((object)left[m])) : ((uint)((object)right[m])));
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarLessThan(left[n], right[n]) ? ((int)((object)left[n])) : ((int)((object)right[n])));
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarLessThan(left[num], right[num]) ? ((ulong)((object)left[num])) : ((ulong)((object)right[num])));
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarLessThan(left[num2], right[num2]) ? ((long)((object)left[num2])) : ((long)((object)right[num2])));
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarLessThan(left[num3], right[num3]) ? ((float)((object)left[num3])) : ((float)((object)right[num3])));
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarLessThan(left[num4], right[num4]) ? ((double)((object)left[num4])) : ((double)((object)right[num4])));
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = ((left.register.byte_0 < right.register.byte_0) ? left.register.byte_0 : right.register.byte_0);
					result.register.byte_1 = ((left.register.byte_1 < right.register.byte_1) ? left.register.byte_1 : right.register.byte_1);
					result.register.byte_2 = ((left.register.byte_2 < right.register.byte_2) ? left.register.byte_2 : right.register.byte_2);
					result.register.byte_3 = ((left.register.byte_3 < right.register.byte_3) ? left.register.byte_3 : right.register.byte_3);
					result.register.byte_4 = ((left.register.byte_4 < right.register.byte_4) ? left.register.byte_4 : right.register.byte_4);
					result.register.byte_5 = ((left.register.byte_5 < right.register.byte_5) ? left.register.byte_5 : right.register.byte_5);
					result.register.byte_6 = ((left.register.byte_6 < right.register.byte_6) ? left.register.byte_6 : right.register.byte_6);
					result.register.byte_7 = ((left.register.byte_7 < right.register.byte_7) ? left.register.byte_7 : right.register.byte_7);
					result.register.byte_8 = ((left.register.byte_8 < right.register.byte_8) ? left.register.byte_8 : right.register.byte_8);
					result.register.byte_9 = ((left.register.byte_9 < right.register.byte_9) ? left.register.byte_9 : right.register.byte_9);
					result.register.byte_10 = ((left.register.byte_10 < right.register.byte_10) ? left.register.byte_10 : right.register.byte_10);
					result.register.byte_11 = ((left.register.byte_11 < right.register.byte_11) ? left.register.byte_11 : right.register.byte_11);
					result.register.byte_12 = ((left.register.byte_12 < right.register.byte_12) ? left.register.byte_12 : right.register.byte_12);
					result.register.byte_13 = ((left.register.byte_13 < right.register.byte_13) ? left.register.byte_13 : right.register.byte_13);
					result.register.byte_14 = ((left.register.byte_14 < right.register.byte_14) ? left.register.byte_14 : right.register.byte_14);
					result.register.byte_15 = ((left.register.byte_15 < right.register.byte_15) ? left.register.byte_15 : right.register.byte_15);
					return result;
				}
				if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = ((left.register.sbyte_0 < right.register.sbyte_0) ? left.register.sbyte_0 : right.register.sbyte_0);
					result.register.sbyte_1 = ((left.register.sbyte_1 < right.register.sbyte_1) ? left.register.sbyte_1 : right.register.sbyte_1);
					result.register.sbyte_2 = ((left.register.sbyte_2 < right.register.sbyte_2) ? left.register.sbyte_2 : right.register.sbyte_2);
					result.register.sbyte_3 = ((left.register.sbyte_3 < right.register.sbyte_3) ? left.register.sbyte_3 : right.register.sbyte_3);
					result.register.sbyte_4 = ((left.register.sbyte_4 < right.register.sbyte_4) ? left.register.sbyte_4 : right.register.sbyte_4);
					result.register.sbyte_5 = ((left.register.sbyte_5 < right.register.sbyte_5) ? left.register.sbyte_5 : right.register.sbyte_5);
					result.register.sbyte_6 = ((left.register.sbyte_6 < right.register.sbyte_6) ? left.register.sbyte_6 : right.register.sbyte_6);
					result.register.sbyte_7 = ((left.register.sbyte_7 < right.register.sbyte_7) ? left.register.sbyte_7 : right.register.sbyte_7);
					result.register.sbyte_8 = ((left.register.sbyte_8 < right.register.sbyte_8) ? left.register.sbyte_8 : right.register.sbyte_8);
					result.register.sbyte_9 = ((left.register.sbyte_9 < right.register.sbyte_9) ? left.register.sbyte_9 : right.register.sbyte_9);
					result.register.sbyte_10 = ((left.register.sbyte_10 < right.register.sbyte_10) ? left.register.sbyte_10 : right.register.sbyte_10);
					result.register.sbyte_11 = ((left.register.sbyte_11 < right.register.sbyte_11) ? left.register.sbyte_11 : right.register.sbyte_11);
					result.register.sbyte_12 = ((left.register.sbyte_12 < right.register.sbyte_12) ? left.register.sbyte_12 : right.register.sbyte_12);
					result.register.sbyte_13 = ((left.register.sbyte_13 < right.register.sbyte_13) ? left.register.sbyte_13 : right.register.sbyte_13);
					result.register.sbyte_14 = ((left.register.sbyte_14 < right.register.sbyte_14) ? left.register.sbyte_14 : right.register.sbyte_14);
					result.register.sbyte_15 = ((left.register.sbyte_15 < right.register.sbyte_15) ? left.register.sbyte_15 : right.register.sbyte_15);
					return result;
				}
				if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = ((left.register.uint16_0 < right.register.uint16_0) ? left.register.uint16_0 : right.register.uint16_0);
					result.register.uint16_1 = ((left.register.uint16_1 < right.register.uint16_1) ? left.register.uint16_1 : right.register.uint16_1);
					result.register.uint16_2 = ((left.register.uint16_2 < right.register.uint16_2) ? left.register.uint16_2 : right.register.uint16_2);
					result.register.uint16_3 = ((left.register.uint16_3 < right.register.uint16_3) ? left.register.uint16_3 : right.register.uint16_3);
					result.register.uint16_4 = ((left.register.uint16_4 < right.register.uint16_4) ? left.register.uint16_4 : right.register.uint16_4);
					result.register.uint16_5 = ((left.register.uint16_5 < right.register.uint16_5) ? left.register.uint16_5 : right.register.uint16_5);
					result.register.uint16_6 = ((left.register.uint16_6 < right.register.uint16_6) ? left.register.uint16_6 : right.register.uint16_6);
					result.register.uint16_7 = ((left.register.uint16_7 < right.register.uint16_7) ? left.register.uint16_7 : right.register.uint16_7);
					return result;
				}
				if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = ((left.register.int16_0 < right.register.int16_0) ? left.register.int16_0 : right.register.int16_0);
					result.register.int16_1 = ((left.register.int16_1 < right.register.int16_1) ? left.register.int16_1 : right.register.int16_1);
					result.register.int16_2 = ((left.register.int16_2 < right.register.int16_2) ? left.register.int16_2 : right.register.int16_2);
					result.register.int16_3 = ((left.register.int16_3 < right.register.int16_3) ? left.register.int16_3 : right.register.int16_3);
					result.register.int16_4 = ((left.register.int16_4 < right.register.int16_4) ? left.register.int16_4 : right.register.int16_4);
					result.register.int16_5 = ((left.register.int16_5 < right.register.int16_5) ? left.register.int16_5 : right.register.int16_5);
					result.register.int16_6 = ((left.register.int16_6 < right.register.int16_6) ? left.register.int16_6 : right.register.int16_6);
					result.register.int16_7 = ((left.register.int16_7 < right.register.int16_7) ? left.register.int16_7 : right.register.int16_7);
					return result;
				}
				if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = ((left.register.uint32_0 < right.register.uint32_0) ? left.register.uint32_0 : right.register.uint32_0);
					result.register.uint32_1 = ((left.register.uint32_1 < right.register.uint32_1) ? left.register.uint32_1 : right.register.uint32_1);
					result.register.uint32_2 = ((left.register.uint32_2 < right.register.uint32_2) ? left.register.uint32_2 : right.register.uint32_2);
					result.register.uint32_3 = ((left.register.uint32_3 < right.register.uint32_3) ? left.register.uint32_3 : right.register.uint32_3);
					return result;
				}
				if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = ((left.register.int32_0 < right.register.int32_0) ? left.register.int32_0 : right.register.int32_0);
					result.register.int32_1 = ((left.register.int32_1 < right.register.int32_1) ? left.register.int32_1 : right.register.int32_1);
					result.register.int32_2 = ((left.register.int32_2 < right.register.int32_2) ? left.register.int32_2 : right.register.int32_2);
					result.register.int32_3 = ((left.register.int32_3 < right.register.int32_3) ? left.register.int32_3 : right.register.int32_3);
					return result;
				}
				if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = ((left.register.uint64_0 < right.register.uint64_0) ? left.register.uint64_0 : right.register.uint64_0);
					result.register.uint64_1 = ((left.register.uint64_1 < right.register.uint64_1) ? left.register.uint64_1 : right.register.uint64_1);
					return result;
				}
				if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = ((left.register.int64_0 < right.register.int64_0) ? left.register.int64_0 : right.register.int64_0);
					result.register.int64_1 = ((left.register.int64_1 < right.register.int64_1) ? left.register.int64_1 : right.register.int64_1);
					return result;
				}
				if (typeof(T) == typeof(float))
				{
					result.register.single_0 = ((left.register.single_0 < right.register.single_0) ? left.register.single_0 : right.register.single_0);
					result.register.single_1 = ((left.register.single_1 < right.register.single_1) ? left.register.single_1 : right.register.single_1);
					result.register.single_2 = ((left.register.single_2 < right.register.single_2) ? left.register.single_2 : right.register.single_2);
					result.register.single_3 = ((left.register.single_3 < right.register.single_3) ? left.register.single_3 : right.register.single_3);
					return result;
				}
				if (typeof(T) == typeof(double))
				{
					result.register.double_0 = ((left.register.double_0 < right.register.double_0) ? left.register.double_0 : right.register.double_0);
					result.register.double_1 = ((left.register.double_1 < right.register.double_1) ? left.register.double_1 : right.register.double_1);
					return result;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x001152D4 File Offset: 0x001144D4
		[Intrinsic]
		internal unsafe static Vector<T> Max(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarGreaterThan(left[i], right[i]) ? ((byte)((object)left[i])) : ((byte)((object)right[i])));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarGreaterThan(left[j], right[j]) ? ((sbyte)((object)left[j])) : ((sbyte)((object)right[j])));
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarGreaterThan(left[k], right[k]) ? ((ushort)((object)left[k])) : ((ushort)((object)right[k])));
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarGreaterThan(left[l], right[l]) ? ((short)((object)left[l])) : ((short)((object)right[l])));
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarGreaterThan(left[m], right[m]) ? ((uint)((object)left[m])) : ((uint)((object)right[m])));
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarGreaterThan(left[n], right[n]) ? ((int)((object)left[n])) : ((int)((object)right[n])));
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarGreaterThan(left[num], right[num]) ? ((ulong)((object)left[num])) : ((ulong)((object)right[num])));
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarGreaterThan(left[num2], right[num2]) ? ((long)((object)left[num2])) : ((long)((object)right[num2])));
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarGreaterThan(left[num3], right[num3]) ? ((float)((object)left[num3])) : ((float)((object)right[num3])));
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarGreaterThan(left[num4], right[num4]) ? ((double)((object)left[num4])) : ((double)((object)right[num4])));
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				Vector<T> result = default(Vector<T>);
				if (typeof(T) == typeof(byte))
				{
					result.register.byte_0 = ((left.register.byte_0 > right.register.byte_0) ? left.register.byte_0 : right.register.byte_0);
					result.register.byte_1 = ((left.register.byte_1 > right.register.byte_1) ? left.register.byte_1 : right.register.byte_1);
					result.register.byte_2 = ((left.register.byte_2 > right.register.byte_2) ? left.register.byte_2 : right.register.byte_2);
					result.register.byte_3 = ((left.register.byte_3 > right.register.byte_3) ? left.register.byte_3 : right.register.byte_3);
					result.register.byte_4 = ((left.register.byte_4 > right.register.byte_4) ? left.register.byte_4 : right.register.byte_4);
					result.register.byte_5 = ((left.register.byte_5 > right.register.byte_5) ? left.register.byte_5 : right.register.byte_5);
					result.register.byte_6 = ((left.register.byte_6 > right.register.byte_6) ? left.register.byte_6 : right.register.byte_6);
					result.register.byte_7 = ((left.register.byte_7 > right.register.byte_7) ? left.register.byte_7 : right.register.byte_7);
					result.register.byte_8 = ((left.register.byte_8 > right.register.byte_8) ? left.register.byte_8 : right.register.byte_8);
					result.register.byte_9 = ((left.register.byte_9 > right.register.byte_9) ? left.register.byte_9 : right.register.byte_9);
					result.register.byte_10 = ((left.register.byte_10 > right.register.byte_10) ? left.register.byte_10 : right.register.byte_10);
					result.register.byte_11 = ((left.register.byte_11 > right.register.byte_11) ? left.register.byte_11 : right.register.byte_11);
					result.register.byte_12 = ((left.register.byte_12 > right.register.byte_12) ? left.register.byte_12 : right.register.byte_12);
					result.register.byte_13 = ((left.register.byte_13 > right.register.byte_13) ? left.register.byte_13 : right.register.byte_13);
					result.register.byte_14 = ((left.register.byte_14 > right.register.byte_14) ? left.register.byte_14 : right.register.byte_14);
					result.register.byte_15 = ((left.register.byte_15 > right.register.byte_15) ? left.register.byte_15 : right.register.byte_15);
					return result;
				}
				if (typeof(T) == typeof(sbyte))
				{
					result.register.sbyte_0 = ((left.register.sbyte_0 > right.register.sbyte_0) ? left.register.sbyte_0 : right.register.sbyte_0);
					result.register.sbyte_1 = ((left.register.sbyte_1 > right.register.sbyte_1) ? left.register.sbyte_1 : right.register.sbyte_1);
					result.register.sbyte_2 = ((left.register.sbyte_2 > right.register.sbyte_2) ? left.register.sbyte_2 : right.register.sbyte_2);
					result.register.sbyte_3 = ((left.register.sbyte_3 > right.register.sbyte_3) ? left.register.sbyte_3 : right.register.sbyte_3);
					result.register.sbyte_4 = ((left.register.sbyte_4 > right.register.sbyte_4) ? left.register.sbyte_4 : right.register.sbyte_4);
					result.register.sbyte_5 = ((left.register.sbyte_5 > right.register.sbyte_5) ? left.register.sbyte_5 : right.register.sbyte_5);
					result.register.sbyte_6 = ((left.register.sbyte_6 > right.register.sbyte_6) ? left.register.sbyte_6 : right.register.sbyte_6);
					result.register.sbyte_7 = ((left.register.sbyte_7 > right.register.sbyte_7) ? left.register.sbyte_7 : right.register.sbyte_7);
					result.register.sbyte_8 = ((left.register.sbyte_8 > right.register.sbyte_8) ? left.register.sbyte_8 : right.register.sbyte_8);
					result.register.sbyte_9 = ((left.register.sbyte_9 > right.register.sbyte_9) ? left.register.sbyte_9 : right.register.sbyte_9);
					result.register.sbyte_10 = ((left.register.sbyte_10 > right.register.sbyte_10) ? left.register.sbyte_10 : right.register.sbyte_10);
					result.register.sbyte_11 = ((left.register.sbyte_11 > right.register.sbyte_11) ? left.register.sbyte_11 : right.register.sbyte_11);
					result.register.sbyte_12 = ((left.register.sbyte_12 > right.register.sbyte_12) ? left.register.sbyte_12 : right.register.sbyte_12);
					result.register.sbyte_13 = ((left.register.sbyte_13 > right.register.sbyte_13) ? left.register.sbyte_13 : right.register.sbyte_13);
					result.register.sbyte_14 = ((left.register.sbyte_14 > right.register.sbyte_14) ? left.register.sbyte_14 : right.register.sbyte_14);
					result.register.sbyte_15 = ((left.register.sbyte_15 > right.register.sbyte_15) ? left.register.sbyte_15 : right.register.sbyte_15);
					return result;
				}
				if (typeof(T) == typeof(ushort))
				{
					result.register.uint16_0 = ((left.register.uint16_0 > right.register.uint16_0) ? left.register.uint16_0 : right.register.uint16_0);
					result.register.uint16_1 = ((left.register.uint16_1 > right.register.uint16_1) ? left.register.uint16_1 : right.register.uint16_1);
					result.register.uint16_2 = ((left.register.uint16_2 > right.register.uint16_2) ? left.register.uint16_2 : right.register.uint16_2);
					result.register.uint16_3 = ((left.register.uint16_3 > right.register.uint16_3) ? left.register.uint16_3 : right.register.uint16_3);
					result.register.uint16_4 = ((left.register.uint16_4 > right.register.uint16_4) ? left.register.uint16_4 : right.register.uint16_4);
					result.register.uint16_5 = ((left.register.uint16_5 > right.register.uint16_5) ? left.register.uint16_5 : right.register.uint16_5);
					result.register.uint16_6 = ((left.register.uint16_6 > right.register.uint16_6) ? left.register.uint16_6 : right.register.uint16_6);
					result.register.uint16_7 = ((left.register.uint16_7 > right.register.uint16_7) ? left.register.uint16_7 : right.register.uint16_7);
					return result;
				}
				if (typeof(T) == typeof(short))
				{
					result.register.int16_0 = ((left.register.int16_0 > right.register.int16_0) ? left.register.int16_0 : right.register.int16_0);
					result.register.int16_1 = ((left.register.int16_1 > right.register.int16_1) ? left.register.int16_1 : right.register.int16_1);
					result.register.int16_2 = ((left.register.int16_2 > right.register.int16_2) ? left.register.int16_2 : right.register.int16_2);
					result.register.int16_3 = ((left.register.int16_3 > right.register.int16_3) ? left.register.int16_3 : right.register.int16_3);
					result.register.int16_4 = ((left.register.int16_4 > right.register.int16_4) ? left.register.int16_4 : right.register.int16_4);
					result.register.int16_5 = ((left.register.int16_5 > right.register.int16_5) ? left.register.int16_5 : right.register.int16_5);
					result.register.int16_6 = ((left.register.int16_6 > right.register.int16_6) ? left.register.int16_6 : right.register.int16_6);
					result.register.int16_7 = ((left.register.int16_7 > right.register.int16_7) ? left.register.int16_7 : right.register.int16_7);
					return result;
				}
				if (typeof(T) == typeof(uint))
				{
					result.register.uint32_0 = ((left.register.uint32_0 > right.register.uint32_0) ? left.register.uint32_0 : right.register.uint32_0);
					result.register.uint32_1 = ((left.register.uint32_1 > right.register.uint32_1) ? left.register.uint32_1 : right.register.uint32_1);
					result.register.uint32_2 = ((left.register.uint32_2 > right.register.uint32_2) ? left.register.uint32_2 : right.register.uint32_2);
					result.register.uint32_3 = ((left.register.uint32_3 > right.register.uint32_3) ? left.register.uint32_3 : right.register.uint32_3);
					return result;
				}
				if (typeof(T) == typeof(int))
				{
					result.register.int32_0 = ((left.register.int32_0 > right.register.int32_0) ? left.register.int32_0 : right.register.int32_0);
					result.register.int32_1 = ((left.register.int32_1 > right.register.int32_1) ? left.register.int32_1 : right.register.int32_1);
					result.register.int32_2 = ((left.register.int32_2 > right.register.int32_2) ? left.register.int32_2 : right.register.int32_2);
					result.register.int32_3 = ((left.register.int32_3 > right.register.int32_3) ? left.register.int32_3 : right.register.int32_3);
					return result;
				}
				if (typeof(T) == typeof(ulong))
				{
					result.register.uint64_0 = ((left.register.uint64_0 > right.register.uint64_0) ? left.register.uint64_0 : right.register.uint64_0);
					result.register.uint64_1 = ((left.register.uint64_1 > right.register.uint64_1) ? left.register.uint64_1 : right.register.uint64_1);
					return result;
				}
				if (typeof(T) == typeof(long))
				{
					result.register.int64_0 = ((left.register.int64_0 > right.register.int64_0) ? left.register.int64_0 : right.register.int64_0);
					result.register.int64_1 = ((left.register.int64_1 > right.register.int64_1) ? left.register.int64_1 : right.register.int64_1);
					return result;
				}
				if (typeof(T) == typeof(float))
				{
					result.register.single_0 = ((left.register.single_0 > right.register.single_0) ? left.register.single_0 : right.register.single_0);
					result.register.single_1 = ((left.register.single_1 > right.register.single_1) ? left.register.single_1 : right.register.single_1);
					result.register.single_2 = ((left.register.single_2 > right.register.single_2) ? left.register.single_2 : right.register.single_2);
					result.register.single_3 = ((left.register.single_3 > right.register.single_3) ? left.register.single_3 : right.register.single_3);
					return result;
				}
				if (typeof(T) == typeof(double))
				{
					result.register.double_0 = ((left.register.double_0 > right.register.double_0) ? left.register.double_0 : right.register.double_0);
					result.register.double_1 = ((left.register.double_1 > right.register.double_1) ? left.register.double_1 : right.register.double_1);
					return result;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00116924 File Offset: 0x00115B24
		[Intrinsic]
		internal static T Dot(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				T t = default(T);
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					t = Vector<T>.ScalarAdd(t, Vector<T>.ScalarMultiply(left[i], right[i]));
				}
				return t;
			}
			if (typeof(T) == typeof(byte))
			{
				byte b = 0;
				b += left.register.byte_0 * right.register.byte_0;
				b += left.register.byte_1 * right.register.byte_1;
				b += left.register.byte_2 * right.register.byte_2;
				b += left.register.byte_3 * right.register.byte_3;
				b += left.register.byte_4 * right.register.byte_4;
				b += left.register.byte_5 * right.register.byte_5;
				b += left.register.byte_6 * right.register.byte_6;
				b += left.register.byte_7 * right.register.byte_7;
				b += left.register.byte_8 * right.register.byte_8;
				b += left.register.byte_9 * right.register.byte_9;
				b += left.register.byte_10 * right.register.byte_10;
				b += left.register.byte_11 * right.register.byte_11;
				b += left.register.byte_12 * right.register.byte_12;
				b += left.register.byte_13 * right.register.byte_13;
				b += left.register.byte_14 * right.register.byte_14;
				b += left.register.byte_15 * right.register.byte_15;
				return (T)((object)b);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte b2 = 0;
				b2 += left.register.sbyte_0 * right.register.sbyte_0;
				b2 += left.register.sbyte_1 * right.register.sbyte_1;
				b2 += left.register.sbyte_2 * right.register.sbyte_2;
				b2 += left.register.sbyte_3 * right.register.sbyte_3;
				b2 += left.register.sbyte_4 * right.register.sbyte_4;
				b2 += left.register.sbyte_5 * right.register.sbyte_5;
				b2 += left.register.sbyte_6 * right.register.sbyte_6;
				b2 += left.register.sbyte_7 * right.register.sbyte_7;
				b2 += left.register.sbyte_8 * right.register.sbyte_8;
				b2 += left.register.sbyte_9 * right.register.sbyte_9;
				b2 += left.register.sbyte_10 * right.register.sbyte_10;
				b2 += left.register.sbyte_11 * right.register.sbyte_11;
				b2 += left.register.sbyte_12 * right.register.sbyte_12;
				b2 += left.register.sbyte_13 * right.register.sbyte_13;
				b2 += left.register.sbyte_14 * right.register.sbyte_14;
				b2 += left.register.sbyte_15 * right.register.sbyte_15;
				return (T)((object)b2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort num = 0;
				num += left.register.uint16_0 * right.register.uint16_0;
				num += left.register.uint16_1 * right.register.uint16_1;
				num += left.register.uint16_2 * right.register.uint16_2;
				num += left.register.uint16_3 * right.register.uint16_3;
				num += left.register.uint16_4 * right.register.uint16_4;
				num += left.register.uint16_5 * right.register.uint16_5;
				num += left.register.uint16_6 * right.register.uint16_6;
				num += left.register.uint16_7 * right.register.uint16_7;
				return (T)((object)num);
			}
			if (typeof(T) == typeof(short))
			{
				short num2 = 0;
				num2 += left.register.int16_0 * right.register.int16_0;
				num2 += left.register.int16_1 * right.register.int16_1;
				num2 += left.register.int16_2 * right.register.int16_2;
				num2 += left.register.int16_3 * right.register.int16_3;
				num2 += left.register.int16_4 * right.register.int16_4;
				num2 += left.register.int16_5 * right.register.int16_5;
				num2 += left.register.int16_6 * right.register.int16_6;
				num2 += left.register.int16_7 * right.register.int16_7;
				return (T)((object)num2);
			}
			if (typeof(T) == typeof(uint))
			{
				uint num3 = 0U;
				num3 += left.register.uint32_0 * right.register.uint32_0;
				num3 += left.register.uint32_1 * right.register.uint32_1;
				num3 += left.register.uint32_2 * right.register.uint32_2;
				num3 += left.register.uint32_3 * right.register.uint32_3;
				return (T)((object)num3);
			}
			if (typeof(T) == typeof(int))
			{
				int num4 = 0;
				num4 += left.register.int32_0 * right.register.int32_0;
				num4 += left.register.int32_1 * right.register.int32_1;
				num4 += left.register.int32_2 * right.register.int32_2;
				num4 += left.register.int32_3 * right.register.int32_3;
				return (T)((object)num4);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong num5 = 0UL;
				num5 += left.register.uint64_0 * right.register.uint64_0;
				num5 += left.register.uint64_1 * right.register.uint64_1;
				return (T)((object)num5);
			}
			if (typeof(T) == typeof(long))
			{
				long num6 = 0L;
				num6 += left.register.int64_0 * right.register.int64_0;
				num6 += left.register.int64_1 * right.register.int64_1;
				return (T)((object)num6);
			}
			if (typeof(T) == typeof(float))
			{
				float num7 = 0f;
				num7 += left.register.single_0 * right.register.single_0;
				num7 += left.register.single_1 * right.register.single_1;
				num7 += left.register.single_2 * right.register.single_2;
				num7 += left.register.single_3 * right.register.single_3;
				return (T)((object)num7);
			}
			if (typeof(T) == typeof(double))
			{
				double num8 = 0.0;
				num8 += left.register.double_0 * right.register.double_0;
				num8 += left.register.double_1 * right.register.double_1;
				return (T)((object)num8);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x001172A4 File Offset: 0x001164A4
		[Intrinsic]
		internal unsafe static Vector<T> SquareRoot(Vector<T> value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (byte)Math.Sqrt((double)((byte)((object)value[i])));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (sbyte)Math.Sqrt((double)((sbyte)((object)value[j])));
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3 = stackalloc ushort[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (ushort)Math.Sqrt((double)((ushort)((object)value[k])));
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4 = stackalloc short[checked(unchecked((UIntPtr)Vector<T>.Count) * 2)];
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (short)Math.Sqrt((double)((short)((object)value[l])));
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5 = stackalloc uint[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (uint)Math.Sqrt((uint)((object)value[m]));
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6 = stackalloc int[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (int)Math.Sqrt((double)((int)((object)value[n])));
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7 = stackalloc ulong[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (ulong)Math.Sqrt((ulong)((object)value[num]));
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8 = stackalloc long[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (long)Math.Sqrt((double)((long)((object)value[num2])));
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9 = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (float)Math.Sqrt((double)((float)((object)value[num3])));
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = Math.Sqrt((double)((object)value[num4]));
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				if (typeof(T) == typeof(byte))
				{
					value.register.byte_0 = (byte)Math.Sqrt((double)value.register.byte_0);
					value.register.byte_1 = (byte)Math.Sqrt((double)value.register.byte_1);
					value.register.byte_2 = (byte)Math.Sqrt((double)value.register.byte_2);
					value.register.byte_3 = (byte)Math.Sqrt((double)value.register.byte_3);
					value.register.byte_4 = (byte)Math.Sqrt((double)value.register.byte_4);
					value.register.byte_5 = (byte)Math.Sqrt((double)value.register.byte_5);
					value.register.byte_6 = (byte)Math.Sqrt((double)value.register.byte_6);
					value.register.byte_7 = (byte)Math.Sqrt((double)value.register.byte_7);
					value.register.byte_8 = (byte)Math.Sqrt((double)value.register.byte_8);
					value.register.byte_9 = (byte)Math.Sqrt((double)value.register.byte_9);
					value.register.byte_10 = (byte)Math.Sqrt((double)value.register.byte_10);
					value.register.byte_11 = (byte)Math.Sqrt((double)value.register.byte_11);
					value.register.byte_12 = (byte)Math.Sqrt((double)value.register.byte_12);
					value.register.byte_13 = (byte)Math.Sqrt((double)value.register.byte_13);
					value.register.byte_14 = (byte)Math.Sqrt((double)value.register.byte_14);
					value.register.byte_15 = (byte)Math.Sqrt((double)value.register.byte_15);
					return value;
				}
				if (typeof(T) == typeof(sbyte))
				{
					value.register.sbyte_0 = (sbyte)Math.Sqrt((double)value.register.sbyte_0);
					value.register.sbyte_1 = (sbyte)Math.Sqrt((double)value.register.sbyte_1);
					value.register.sbyte_2 = (sbyte)Math.Sqrt((double)value.register.sbyte_2);
					value.register.sbyte_3 = (sbyte)Math.Sqrt((double)value.register.sbyte_3);
					value.register.sbyte_4 = (sbyte)Math.Sqrt((double)value.register.sbyte_4);
					value.register.sbyte_5 = (sbyte)Math.Sqrt((double)value.register.sbyte_5);
					value.register.sbyte_6 = (sbyte)Math.Sqrt((double)value.register.sbyte_6);
					value.register.sbyte_7 = (sbyte)Math.Sqrt((double)value.register.sbyte_7);
					value.register.sbyte_8 = (sbyte)Math.Sqrt((double)value.register.sbyte_8);
					value.register.sbyte_9 = (sbyte)Math.Sqrt((double)value.register.sbyte_9);
					value.register.sbyte_10 = (sbyte)Math.Sqrt((double)value.register.sbyte_10);
					value.register.sbyte_11 = (sbyte)Math.Sqrt((double)value.register.sbyte_11);
					value.register.sbyte_12 = (sbyte)Math.Sqrt((double)value.register.sbyte_12);
					value.register.sbyte_13 = (sbyte)Math.Sqrt((double)value.register.sbyte_13);
					value.register.sbyte_14 = (sbyte)Math.Sqrt((double)value.register.sbyte_14);
					value.register.sbyte_15 = (sbyte)Math.Sqrt((double)value.register.sbyte_15);
					return value;
				}
				if (typeof(T) == typeof(ushort))
				{
					value.register.uint16_0 = (ushort)Math.Sqrt((double)value.register.uint16_0);
					value.register.uint16_1 = (ushort)Math.Sqrt((double)value.register.uint16_1);
					value.register.uint16_2 = (ushort)Math.Sqrt((double)value.register.uint16_2);
					value.register.uint16_3 = (ushort)Math.Sqrt((double)value.register.uint16_3);
					value.register.uint16_4 = (ushort)Math.Sqrt((double)value.register.uint16_4);
					value.register.uint16_5 = (ushort)Math.Sqrt((double)value.register.uint16_5);
					value.register.uint16_6 = (ushort)Math.Sqrt((double)value.register.uint16_6);
					value.register.uint16_7 = (ushort)Math.Sqrt((double)value.register.uint16_7);
					return value;
				}
				if (typeof(T) == typeof(short))
				{
					value.register.int16_0 = (short)Math.Sqrt((double)value.register.int16_0);
					value.register.int16_1 = (short)Math.Sqrt((double)value.register.int16_1);
					value.register.int16_2 = (short)Math.Sqrt((double)value.register.int16_2);
					value.register.int16_3 = (short)Math.Sqrt((double)value.register.int16_3);
					value.register.int16_4 = (short)Math.Sqrt((double)value.register.int16_4);
					value.register.int16_5 = (short)Math.Sqrt((double)value.register.int16_5);
					value.register.int16_6 = (short)Math.Sqrt((double)value.register.int16_6);
					value.register.int16_7 = (short)Math.Sqrt((double)value.register.int16_7);
					return value;
				}
				if (typeof(T) == typeof(uint))
				{
					value.register.uint32_0 = (uint)Math.Sqrt(value.register.uint32_0);
					value.register.uint32_1 = (uint)Math.Sqrt(value.register.uint32_1);
					value.register.uint32_2 = (uint)Math.Sqrt(value.register.uint32_2);
					value.register.uint32_3 = (uint)Math.Sqrt(value.register.uint32_3);
					return value;
				}
				if (typeof(T) == typeof(int))
				{
					value.register.int32_0 = (int)Math.Sqrt((double)value.register.int32_0);
					value.register.int32_1 = (int)Math.Sqrt((double)value.register.int32_1);
					value.register.int32_2 = (int)Math.Sqrt((double)value.register.int32_2);
					value.register.int32_3 = (int)Math.Sqrt((double)value.register.int32_3);
					return value;
				}
				if (typeof(T) == typeof(ulong))
				{
					value.register.uint64_0 = (ulong)Math.Sqrt(value.register.uint64_0);
					value.register.uint64_1 = (ulong)Math.Sqrt(value.register.uint64_1);
					return value;
				}
				if (typeof(T) == typeof(long))
				{
					value.register.int64_0 = (long)Math.Sqrt((double)value.register.int64_0);
					value.register.int64_1 = (long)Math.Sqrt((double)value.register.int64_1);
					return value;
				}
				if (typeof(T) == typeof(float))
				{
					value.register.single_0 = (float)Math.Sqrt((double)value.register.single_0);
					value.register.single_1 = (float)Math.Sqrt((double)value.register.single_1);
					value.register.single_2 = (float)Math.Sqrt((double)value.register.single_2);
					value.register.single_3 = (float)Math.Sqrt((double)value.register.single_3);
					return value;
				}
				if (typeof(T) == typeof(double))
				{
					value.register.double_0 = Math.Sqrt(value.register.double_0);
					value.register.double_1 = Math.Sqrt(value.register.double_1);
					return value;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00117FA0 File Offset: 0x001171A0
		[Intrinsic]
		internal unsafe static Vector<T> Ceiling(Vector<T> value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(float))
				{
					float* ptr = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = MathF.Ceiling((float)((object)value[i]));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr2 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = Math.Ceiling((double)((object)value[j]));
					}
					return new Vector<T>((void*)ptr2);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				if (typeof(T) == typeof(float))
				{
					value.register.single_0 = MathF.Ceiling(value.register.single_0);
					value.register.single_1 = MathF.Ceiling(value.register.single_1);
					value.register.single_2 = MathF.Ceiling(value.register.single_2);
					value.register.single_3 = MathF.Ceiling(value.register.single_3);
					return value;
				}
				if (typeof(T) == typeof(double))
				{
					value.register.double_0 = Math.Ceiling(value.register.double_0);
					value.register.double_1 = Math.Ceiling(value.register.double_1);
					return value;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00118164 File Offset: 0x00117364
		[Intrinsic]
		internal unsafe static Vector<T> Floor(Vector<T> value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(float))
				{
					float* ptr = stackalloc float[checked(unchecked((UIntPtr)Vector<T>.Count) * 4)];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = MathF.Floor((float)((object)value[i]));
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr2 = stackalloc double[checked(unchecked((UIntPtr)Vector<T>.Count) * 8)];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = Math.Floor((double)((object)value[j]));
					}
					return new Vector<T>((void*)ptr2);
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
			else
			{
				if (typeof(T) == typeof(float))
				{
					value.register.single_0 = MathF.Floor(value.register.single_0);
					value.register.single_1 = MathF.Floor(value.register.single_1);
					value.register.single_2 = MathF.Floor(value.register.single_2);
					value.register.single_3 = MathF.Floor(value.register.single_3);
					return value;
				}
				if (typeof(T) == typeof(double))
				{
					value.register.double_0 = Math.Floor(value.register.double_0);
					value.register.double_1 = Math.Floor(value.register.double_1);
					return value;
				}
				throw new NotSupportedException(SR.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00118328 File Offset: 0x00117528
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ScalarEquals(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) == (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) == (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) == (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) == (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) == (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) == (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) == (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) == (long)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) == (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) == (double)((object)right);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00118548 File Offset: 0x00117748
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ScalarLessThan(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) < (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) < (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) < (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) < (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) < (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) < (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) < (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) < (long)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) < (double)((object)right);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00118768 File Offset: 0x00117968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ScalarGreaterThan(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) > (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) > (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) > (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) > (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) > (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) > (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) > (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) > (long)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) > (double)((object)right);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00118988 File Offset: 0x00117B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T ScalarAdd(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)((byte)((object)left) + (byte)((object)right)));
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)((sbyte)((object)left) + (sbyte)((object)right)));
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)((ushort)((object)left) + (ushort)((object)right)));
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)((short)((object)left) + (short)((object)right)));
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)((uint)((object)left) + (uint)((object)right)));
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)((int)((object)left) + (int)((object)right)));
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)((ulong)((object)left) + (ulong)((object)right)));
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)((long)((object)left) + (long)((object)right)));
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)((float)((object)left) + (float)((object)right)));
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)((double)((object)left) + (double)((object)right)));
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00118C08 File Offset: 0x00117E08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T ScalarSubtract(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)((byte)((object)left) - (byte)((object)right)));
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)((sbyte)((object)left) - (sbyte)((object)right)));
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)((ushort)((object)left) - (ushort)((object)right)));
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)((short)((object)left) - (short)((object)right)));
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)((uint)((object)left) - (uint)((object)right)));
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)((int)((object)left) - (int)((object)right)));
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)((ulong)((object)left) - (ulong)((object)right)));
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)((long)((object)left) - (long)((object)right)));
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)((float)((object)left) - (float)((object)right)));
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)((double)((object)left) - (double)((object)right)));
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00118E88 File Offset: 0x00118088
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T ScalarMultiply(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)((byte)((object)left) * (byte)((object)right)));
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)((sbyte)((object)left) * (sbyte)((object)right)));
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)((ushort)((object)left) * (ushort)((object)right)));
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)((short)((object)left) * (short)((object)right)));
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)((uint)((object)left) * (uint)((object)right)));
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)((int)((object)left) * (int)((object)right)));
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)((ulong)((object)left) * (ulong)((object)right)));
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)((long)((object)left) * (long)((object)right)));
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)((float)((object)left) * (float)((object)right)));
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)((double)((object)left) * (double)((object)right)));
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00119108 File Offset: 0x00118308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T ScalarDivide(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)((byte)((object)left) / (byte)((object)right)));
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)((sbyte)((object)left) / (sbyte)((object)right)));
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)((ushort)((object)left) / (ushort)((object)right)));
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)((short)((object)left) / (short)((object)right)));
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)((uint)((object)left) / (uint)((object)right)));
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)((int)((object)left) / (int)((object)right)));
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)((ulong)((object)left) / (ulong)((object)right)));
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)((long)((object)left) / (long)((object)right)));
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)((float)((object)left) / (float)((object)right)));
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)((double)((object)left) / (double)((object)right)));
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00119388 File Offset: 0x00118588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetOneValue()
		{
			if (typeof(T) == typeof(byte))
			{
				byte b = 1;
				return (T)((object)b);
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte b2 = 1;
				return (T)((object)b2);
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort num = 1;
				return (T)((object)num);
			}
			if (typeof(T) == typeof(short))
			{
				short num2 = 1;
				return (T)((object)num2);
			}
			if (typeof(T) == typeof(uint))
			{
				uint num3 = 1U;
				return (T)((object)num3);
			}
			if (typeof(T) == typeof(int))
			{
				int num4 = 1;
				return (T)((object)num4);
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong num5 = 1UL;
				return (T)((object)num5);
			}
			if (typeof(T) == typeof(long))
			{
				long num6 = 1L;
				return (T)((object)num6);
			}
			if (typeof(T) == typeof(float))
			{
				float num7 = 1f;
				return (T)((object)num7);
			}
			if (typeof(T) == typeof(double))
			{
				double num8 = 1.0;
				return (T)((object)num8);
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00119554 File Offset: 0x00118754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetAllBitsSetValue()
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)ConstantHelper.GetByteWithAllBitsSet());
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)ConstantHelper.GetSByteWithAllBitsSet());
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)ConstantHelper.GetUInt16WithAllBitsSet());
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)ConstantHelper.GetInt16WithAllBitsSet());
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)ConstantHelper.GetUInt32WithAllBitsSet());
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)ConstantHelper.GetInt32WithAllBitsSet());
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)ConstantHelper.GetUInt64WithAllBitsSet());
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)ConstantHelper.GetInt64WithAllBitsSet());
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)ConstantHelper.GetSingleWithAllBitsSet());
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)ConstantHelper.GetDoubleWithAllBitsSet());
			}
			throw new NotSupportedException(SR.Arg_TypeNotSupported);
		}

		// Token: 0x04000676 RID: 1654
		private Register register;
	}
}
