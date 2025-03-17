using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000050 RID: 80
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000AB340 File Offset: 0x000AA540
		public unsafe static Array CreateInstance(Type elementType, int length)
		{
			if (elementType == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementType);
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_MustBeType, ExceptionArgument.elementType);
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 1, &length, null);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000AB39C File Offset: 0x000AA59C
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2)
		{
			if (elementType == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementType);
			}
			if (length1 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length1, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length2 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length2, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_MustBeType, ExceptionArgument.elementType);
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = length1;
			ptr[1] = length2;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 2, ptr, null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000AB414 File Offset: 0x000AA614
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			if (elementType == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementType);
			}
			if (length1 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length1, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length2 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length2, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (length3 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length3, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_MustBeType, ExceptionArgument.elementType);
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = length1;
			ptr[1] = length2;
			ptr[2] = length3;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 3, ptr, null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000AB4A4 File Offset: 0x000AA6A4
		public unsafe static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementType);
			}
			if (lengths == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.lengths);
			}
			if (lengths.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NeedAtLeast1Rank);
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_MustBeType, ExceptionArgument.elementType);
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.lengths, i, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
			}
			fixed (int* ptr = &lengths[0])
			{
				int* pLengths = ptr;
				return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, pLengths, null);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000AB538 File Offset: 0x000AA738
		public unsafe static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementType);
			}
			if (lengths == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.lengths);
			}
			if (lowerBounds == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.lowerBounds);
			}
			if (lengths.Length != lowerBounds.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RanksAndBounds);
			}
			if (lengths.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NeedAtLeast1Rank);
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_MustBeType, ExceptionArgument.elementType);
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.lengths, i, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
			}
			fixed (int* ptr = &lengths[0])
			{
				int* pLengths = ptr;
				fixed (int* ptr2 = &lowerBounds[0])
				{
					int* pLowerBounds = ptr2;
					return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, pLengths, pLowerBounds);
				}
			}
		}

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Array InternalCreate(void* elementType, int rank, int* pLengths, int* pLowerBounds);

		// Token: 0x060000F3 RID: 243 RVA: 0x000AB5F8 File Offset: 0x000AA7F8
		public unsafe static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.sourceArray);
			}
			if (destinationArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.destinationArray);
			}
			MethodTable* methodTable = RuntimeHelpers.GetMethodTable(sourceArray);
			if (methodTable != RuntimeHelpers.GetMethodTable(destinationArray) || methodTable->IsMultiDimensionalArray || (UIntPtr)length > (UIntPtr)sourceArray.LongLength || (UIntPtr)length > (UIntPtr)destinationArray.LongLength)
			{
				Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length, false);
				return;
			}
			UIntPtr uintPtr = (UIntPtr)length * (UIntPtr)methodTable->ComponentSize;
			ref byte source = ref Unsafe.As<RawArrayData>(sourceArray).Data;
			ref byte destination = ref Unsafe.As<RawArrayData>(destinationArray).Data;
			if (methodTable->ContainsGCPointers)
			{
				Buffer.BulkMoveWithWriteBarrier(ref destination, ref source, uintPtr);
				return;
			}
			Buffer.Memmove<byte>(ref destination, ref source, uintPtr);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000AB69C File Offset: 0x000AA89C
		public unsafe static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			if (sourceArray != null && destinationArray != null)
			{
				MethodTable* methodTable = RuntimeHelpers.GetMethodTable(sourceArray);
				if (methodTable == RuntimeHelpers.GetMethodTable(destinationArray) && !methodTable->IsMultiDimensionalArray && length >= 0 && sourceIndex >= 0 && destinationIndex >= 0 && (UIntPtr)(sourceIndex + length) <= (UIntPtr)sourceArray.LongLength && (UIntPtr)(destinationIndex + length) <= (UIntPtr)destinationArray.LongLength)
				{
					UIntPtr uintPtr = (UIntPtr)methodTable->ComponentSize;
					UIntPtr uintPtr2 = (UIntPtr)length * uintPtr;
					ref byte source = ref Unsafe.AddByteOffset<byte>(ref Unsafe.As<RawArrayData>(sourceArray).Data, (UIntPtr)sourceIndex * uintPtr);
					ref byte destination = ref Unsafe.AddByteOffset<byte>(ref Unsafe.As<RawArrayData>(destinationArray).Data, (UIntPtr)destinationIndex * uintPtr);
					if (methodTable->ContainsGCPointers)
					{
						Buffer.BulkMoveWithWriteBarrier(ref destination, ref source, uintPtr2);
						return;
					}
					Buffer.Memmove<byte>(ref destination, ref source, uintPtr2);
					return;
				}
			}
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, false);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000AB75C File Offset: 0x000AA95C
		private unsafe static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length, bool reliable)
		{
			if (sourceArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.sourceArray);
			}
			if (destinationArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.destinationArray);
			}
			if (sourceArray.GetType() != destinationArray.GetType() && sourceArray.Rank != destinationArray.Rank)
			{
				throw new RankException(SR.Rank_MustMatch);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int lowerBound = sourceArray.GetLowerBound(0);
			if (sourceIndex < lowerBound || sourceIndex - lowerBound < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			sourceIndex -= lowerBound;
			int lowerBound2 = destinationArray.GetLowerBound(0);
			if (destinationIndex < lowerBound2 || destinationIndex - lowerBound2 < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			destinationIndex -= lowerBound2;
			if ((UIntPtr)(sourceIndex + length) > (UIntPtr)sourceArray.LongLength)
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcArray, "sourceArray");
			}
			if ((UIntPtr)(destinationIndex + length) > (UIntPtr)destinationArray.LongLength)
			{
				throw new ArgumentException(SR.Arg_LongerThanDestArray, "destinationArray");
			}
			if (sourceArray.GetType() == destinationArray.GetType() || Array.IsSimpleCopy(sourceArray, destinationArray))
			{
				MethodTable* methodTable = RuntimeHelpers.GetMethodTable(sourceArray);
				UIntPtr uintPtr = (UIntPtr)methodTable->ComponentSize;
				UIntPtr uintPtr2 = (UIntPtr)length * uintPtr;
				ref byte source = ref Unsafe.AddByteOffset<byte>(sourceArray.GetRawArrayData(), (UIntPtr)sourceIndex * uintPtr);
				ref byte destination = ref Unsafe.AddByteOffset<byte>(destinationArray.GetRawArrayData(), (UIntPtr)destinationIndex * uintPtr);
				if (methodTable->ContainsGCPointers)
				{
					Buffer.BulkMoveWithWriteBarrier(ref destination, ref source, uintPtr2);
					return;
				}
				Buffer.Memmove<byte>(ref destination, ref source, uintPtr2);
				return;
			}
			else
			{
				if (reliable)
				{
					throw new ArrayTypeMismatchException(SR.ArrayTypeMismatch_ConstrainedCopy);
				}
				Array.CopySlow(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
				return;
			}
		}

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSimpleCopy(Array sourceArray, Array destinationArray);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopySlow(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length);

		// Token: 0x060000F8 RID: 248 RVA: 0x000AB8D6 File Offset: 0x000AAAD6
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, true);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000AB8E4 File Offset: 0x000AAAE4
		public unsafe static void Clear(Array array, int index, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			ref byte source = ref Unsafe.As<RawArrayData>(array).Data;
			int num = 0;
			MethodTable* methodTable = RuntimeHelpers.GetMethodTable(array);
			if (methodTable->IsMultiDimensionalArray)
			{
				int multiDimensionalArrayRank = methodTable->MultiDimensionalArrayRank;
				num = *Unsafe.Add<int>(Unsafe.As<byte, int>(ref source), multiDimensionalArrayRank);
				source = Unsafe.Add<byte>(ref source, 8 * multiDimensionalArrayRank);
			}
			int num2 = index - num;
			if (index < num || num2 < 0 || length < 0 || (UIntPtr)(num2 + length) > (UIntPtr)array.LongLength)
			{
				ThrowHelper.ThrowIndexOutOfRangeException();
			}
			UIntPtr uintPtr = (UIntPtr)methodTable->ComponentSize;
			ref byte ptr = ref Unsafe.AddByteOffset<byte>(ref source, (UIntPtr)num2 * uintPtr);
			UIntPtr uintPtr2 = (UIntPtr)length * uintPtr;
			if (methodTable->ContainsGCPointers)
			{
				SpanHelpers.ClearWithReferences(Unsafe.As<byte, IntPtr>(ref ptr), uintPtr2 / (UIntPtr)sizeof(IntPtr));
				return;
			}
			SpanHelpers.ClearWithoutReferences(ref ptr, uintPtr2);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000AB9A0 File Offset: 0x000AABA0
		[return: Nullable(2)]
		public unsafe object GetValue(params int[] indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.indices);
			}
			if (this.Rank != indices.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankIndices);
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int* ptr = &indices[0])
			{
				int* pIndices = ptr;
				this.InternalGetReference((void*)(&typedReference), indices.Length, pIndices);
			}
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000AB9F4 File Offset: 0x000AABF4
		[NullableContext(2)]
		public unsafe object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need1DArray);
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000ABA30 File Offset: 0x000AAC30
		[NullableContext(2)]
		public unsafe object GetValue(int index1, int index2)
		{
			if (this.Rank != 2)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need2DArray);
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000ABA78 File Offset: 0x000AAC78
		[NullableContext(2)]
		public unsafe object GetValue(int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need3DArray);
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000ABAC8 File Offset: 0x000AACC8
		[NullableContext(2)]
		public unsafe void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need1DArray);
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000ABB04 File Offset: 0x000AAD04
		[NullableContext(2)]
		public unsafe void SetValue(object value, int index1, int index2)
		{
			if (this.Rank != 2)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need2DArray);
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000ABB4C File Offset: 0x000AAD4C
		[NullableContext(2)]
		public unsafe void SetValue(object value, int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_Need3DArray);
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000ABB9C File Offset: 0x000AAD9C
		public unsafe void SetValue([Nullable(2)] object value, params int[] indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.indices);
			}
			if (this.Rank != indices.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankIndices);
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int* ptr = &indices[0])
			{
				int* pIndices = ptr;
				this.InternalGetReference((void*)(&typedReference), indices.Length, pIndices);
			}
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void InternalGetReference(void* elemRef, int rank, int* pIndices);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalSetValue(void* target, object value);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000ABBF1 File Offset: 0x000AADF1
		public int Length
		{
			get
			{
				return checked((int)Unsafe.As<RawArrayData>(this).Length);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000ABBFF File Offset: 0x000AADFF
		public long LongLength
		{
			get
			{
				return (long)((ulong)Unsafe.As<RawArrayData>(this).Length);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000ABC10 File Offset: 0x000AAE10
		public int Rank
		{
			get
			{
				int multiDimensionalArrayRank = RuntimeHelpers.GetMultiDimensionalArrayRank(this);
				if (multiDimensionalArrayRank == 0)
				{
					return 1;
				}
				return multiDimensionalArrayRank;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000ABC2C File Offset: 0x000AAE2C
		public unsafe int GetLength(int dimension)
		{
			int multiDimensionalArrayRank = RuntimeHelpers.GetMultiDimensionalArrayRank(this);
			if (multiDimensionalArrayRank == 0 && dimension == 0)
			{
				return this.Length;
			}
			if (dimension >= multiDimensionalArrayRank)
			{
				throw new IndexOutOfRangeException(SR.IndexOutOfRange_ArrayRankIndex);
			}
			return *Unsafe.Add<int>(RuntimeHelpers.GetMultiDimensionalArrayBounds(this), dimension);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000ABC6C File Offset: 0x000AAE6C
		public unsafe int GetUpperBound(int dimension)
		{
			int multiDimensionalArrayRank = RuntimeHelpers.GetMultiDimensionalArrayRank(this);
			if (multiDimensionalArrayRank == 0 && dimension == 0)
			{
				return this.Length - 1;
			}
			if (dimension >= multiDimensionalArrayRank)
			{
				throw new IndexOutOfRangeException(SR.IndexOutOfRange_ArrayRankIndex);
			}
			ref int multiDimensionalArrayBounds = ref RuntimeHelpers.GetMultiDimensionalArrayBounds(this);
			return *Unsafe.Add<int>(ref multiDimensionalArrayBounds, dimension) + *Unsafe.Add<int>(ref multiDimensionalArrayBounds, multiDimensionalArrayRank + dimension) - 1;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000ABCBC File Offset: 0x000AAEBC
		public unsafe int GetLowerBound(int dimension)
		{
			int multiDimensionalArrayRank = RuntimeHelpers.GetMultiDimensionalArrayRank(this);
			if (multiDimensionalArrayRank == 0 && dimension == 0)
			{
				return 0;
			}
			if (dimension >= multiDimensionalArrayRank)
			{
				throw new IndexOutOfRangeException(SR.IndexOutOfRange_ArrayRankIndex);
			}
			return *Unsafe.Add<int>(RuntimeHelpers.GetMultiDimensionalArrayBounds(this), multiDimensionalArrayRank + dimension);
		}

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern CorElementType GetCorElementTypeOfElementType();

		// Token: 0x0600010B RID: 267 RVA: 0x000ABCF8 File Offset: 0x000AAEF8
		private unsafe bool IsValueOfElementType(object value)
		{
			MethodTable* methodTable = RuntimeHelpers.GetMethodTable(this);
			return (IntPtr)methodTable->ElementType == (IntPtr)((void*)RuntimeHelpers.GetMethodTable(value));
		}

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Initialize();

		// Token: 0x0600010D RID: 269 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private protected Array()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000ABD2F File Offset: 0x000AAF2F
		public static ReadOnlyCollection<T> AsReadOnly<[Nullable(2)] T>(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return new ReadOnlyCollection<T>(array);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000ABD40 File Offset: 0x000AAF40
		[NullableContext(2)]
		public static void Resize<T>([Nullable(new byte[]
		{
			2,
			1
		})] [NotNull] ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.newSize, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Buffer.Memmove<T>(MemoryMarshal.GetArrayDataReference<T>(array3), MemoryMarshal.GetArrayDataReference<T>(array2), (UIntPtr)Math.Min(newSize, array2.Length));
				array = array3;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000ABD94 File Offset: 0x000AAF94
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.lengths);
			}
			if (lengths.Length == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NeedAtLeast1Rank);
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				int num2 = (int)num;
				if (num != (long)num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.len, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
				}
				array[i] = num2;
			}
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000ABDF0 File Offset: 0x000AAFF0
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			int num = (int)length;
			if (length != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			Array.Copy(sourceArray, destinationArray, num);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000ABE18 File Offset: 0x000AB018
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			int num = (int)sourceIndex;
			int num2 = (int)destinationIndex;
			int num3 = (int)length;
			if (sourceIndex != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceIndex, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (destinationIndex != (long)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.destinationIndex, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (length != (long)num3)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			Array.Copy(sourceArray, num, destinationArray, num2, num3);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000ABE64 File Offset: 0x000AB064
		[NullableContext(2)]
		public object GetValue(long index)
		{
			int num = (int)index;
			if (index != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			return this.GetValue(num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000ABE8C File Offset: 0x000AB08C
		[NullableContext(2)]
		public object GetValue(long index1, long index2)
		{
			int num = (int)index1;
			int num2 = (int)index2;
			if (index1 != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index1, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index2 != (long)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index2, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			return this.GetValue(num, num2);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000ABEC4 File Offset: 0x000AB0C4
		[NullableContext(2)]
		public object GetValue(long index1, long index2, long index3)
		{
			int num = (int)index1;
			int num2 = (int)index2;
			int num3 = (int)index3;
			if (index1 != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index1, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index2 != (long)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index2, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index3 != (long)num3)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index3, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			return this.GetValue(num, num2, num3);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000ABF10 File Offset: 0x000AB110
		[return: Nullable(2)]
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.indices);
			}
			if (this.Rank != indices.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankIndices);
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				int num2 = (int)num;
				if (num != (long)num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
				}
				array[i] = num2;
			}
			return this.GetValue(array);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000ABF70 File Offset: 0x000AB170
		[NullableContext(2)]
		public void SetValue(object value, long index)
		{
			int num = (int)index;
			if (index != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			this.SetValue(value, num);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000ABF98 File Offset: 0x000AB198
		[NullableContext(2)]
		public void SetValue(object value, long index1, long index2)
		{
			int num = (int)index1;
			int num2 = (int)index2;
			if (index1 != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index1, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index2 != (long)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index2, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			this.SetValue(value, num, num2);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000ABFD0 File Offset: 0x000AB1D0
		[NullableContext(2)]
		public void SetValue(object value, long index1, long index2, long index3)
		{
			int num = (int)index1;
			int num2 = (int)index2;
			int num3 = (int)index3;
			if (index1 != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index1, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index2 != (long)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index2, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			if (index3 != (long)num3)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index3, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			this.SetValue(value, num, num2, num3);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000AC01C File Offset: 0x000AB21C
		public void SetValue([Nullable(2)] object value, params long[] indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.indices);
			}
			if (this.Rank != indices.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankIndices);
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				int num2 = (int)num;
				if (num != (long)num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
				}
				array[i] = num2;
			}
			this.SetValue(value, array);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000AC07D File Offset: 0x000AB27D
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000AC086 File Offset: 0x000AB286
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000AC090 File Offset: 0x000AB290
		int ICollection.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000AC098 File Offset: 0x000AB298
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
			set
			{
				this.SetValue(value, index);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000AC0B4 File Offset: 0x000AB2B4
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
			return 0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000AC0BE File Offset: 0x000AB2BE
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= this.GetLowerBound(0);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000AC0D3 File Offset: 0x000AB2D3
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000AC0E8 File Offset: 0x000AB2E8
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000AC0FA File Offset: 0x000AB2FA
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000AC104 File Offset: 0x000AB304
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentException_OtherNotArrayOfCorrectLength, ExceptionArgument.other);
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000AC16C File Offset: 0x000AB36C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000AC1CC File Offset: 0x000AB3CC
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparer);
			}
			HashCode hashCode = default(HashCode);
			for (int i = (this.Length >= 8) ? (this.Length - 8) : 0; i < this.Length; i++)
			{
				hashCode.Add<int>(comparer.GetHashCode(this.GetValue(i)));
			}
			return hashCode.ToHashCode();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000AC22A File Offset: 0x000AB42A
		public static int BinarySearch(Array array, [Nullable(2)] object value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, null);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000AC24A File Offset: 0x000AB44A
		public static int BinarySearch(Array array, int index, int length, [Nullable(2)] object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000AC256 File Offset: 0x000AB456
		[NullableContext(2)]
		public static int BinarySearch([Nullable(1)] Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, comparer);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000AC278 File Offset: 0x000AB478
		[NullableContext(2)]
		public static int BinarySearch([Nullable(1)] Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - (index - lowerBound) < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowRankException(ExceptionResource.Rank_MultiDimNotSupported);
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num)
				{
					int median = Array.GetMedian(i, num);
					int num2;
					try
					{
						num2 = comparer.Compare(array2[median], value);
					}
					catch (Exception e)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
						return 0;
					}
					if (num2 == 0)
					{
						return median;
					}
					if (num2 < 0)
					{
						i = median + 1;
					}
					else
					{
						num = median - 1;
					}
				}
				return ~i;
			}
			if (comparer == Comparer.Default)
			{
				CorElementType corElementTypeOfElementType = array.GetCorElementTypeOfElementType();
				if (corElementTypeOfElementType.IsPrimitiveType())
				{
					if (value == null)
					{
						return ~index;
					}
					if (array.IsValueOfElementType(value))
					{
						int adjustedIndex = index - lowerBound;
						int num3 = -1;
						switch (corElementTypeOfElementType)
						{
						case CorElementType.ELEMENT_TYPE_BOOLEAN:
						case CorElementType.ELEMENT_TYPE_U1:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<byte>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_CHAR:
						case CorElementType.ELEMENT_TYPE_U2:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<ushort>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_I1:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<sbyte>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_I2:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<short>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_I4:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<int>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_U4:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<uint>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_I8:
						case CorElementType.ELEMENT_TYPE_I:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<long>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_U8:
						case CorElementType.ELEMENT_TYPE_U:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<ulong>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_R4:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<float>(array, adjustedIndex, length, value);
							break;
						case CorElementType.ELEMENT_TYPE_R8:
							num3 = Array.<BinarySearch>g__GenericBinarySearch|81_0<double>(array, adjustedIndex, length, value);
							break;
						}
						if (num3 < 0)
						{
							return ~(index + ~num3);
						}
						return index + num3;
					}
				}
			}
			while (i <= num)
			{
				int median2 = Array.GetMedian(i, num);
				int num4;
				try
				{
					num4 = comparer.Compare(array.GetValue(median2), value);
				}
				catch (Exception e2)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e2);
					return 0;
				}
				if (num4 == 0)
				{
					return median2;
				}
				if (num4 < 0)
				{
					i = median2 + 1;
				}
				else
				{
					num = median2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000AC4E4 File Offset: 0x000AB6E4
		public static int BinarySearch<[Nullable(2)] T>(T[] array, T value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000AC4FB File Offset: 0x000AB6FB
		public static int BinarySearch<[Nullable(2)] T>(T[] array, T value, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000AC512 File Offset: 0x000AB712
		public static int BinarySearch<[Nullable(2)] T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000AC51E File Offset: 0x000AB71E
		public static int BinarySearch<[Nullable(2)] T>(T[] array, int index, int length, T value, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000AC55C File Offset: 0x000AB75C
		public static TOutput[] ConvertAll<[Nullable(2)] TInput, [Nullable(2)] TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000AC5A8 File Offset: 0x000AB7A8
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000AC5D4 File Offset: 0x000AB7D4
		public void CopyTo(Array array, long index)
		{
			int num = (int)index;
			if (index != (long)num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported);
			}
			this.CopyTo(array, num);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000AC5FA File Offset: 0x000AB7FA
		public static T[] Empty<[Nullable(2)] T>()
		{
			return Array.EmptyArray<T>.Value;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000AC601 File Offset: 0x000AB801
		public static bool Exists<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000AC610 File Offset: 0x000AB810
		public static void Fill<[Nullable(2)] T>(T[] array, T value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000AC63C File Offset: 0x000AB83C
		public static void Fill<[Nullable(2)] T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000AC688 File Offset: 0x000AB888
		[return: Nullable(2)]
		public static T Find<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000AC6D8 File Offset: 0x000AB8D8
		public static T[] FindAll<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					list.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000AC72E File Offset: 0x000AB92E
		public static int FindIndex<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000AC744 File Offset: 0x000AB944
		public static int FindIndex<[Nullable(2)] T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000AC75C File Offset: 0x000AB95C
		public static int FindIndex<[Nullable(2)] T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000AC7C0 File Offset: 0x000AB9C0
		[return: Nullable(2)]
		public static T FindLast<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000AC810 File Offset: 0x000ABA10
		public static int FindLastIndex<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000AC82A File Offset: 0x000ABA2A
		public static int FindLastIndex<[Nullable(2)] T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000AC840 File Offset: 0x000ABA40
		public static int FindLastIndex<[Nullable(2)] T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000AC8B4 File Offset: 0x000ABAB4
		public static void ForEach<[Nullable(2)] T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000AC8EF File Offset: 0x000ABAEF
		public static int IndexOf(Array array, [Nullable(2)] object value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.IndexOf(array, value, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000AC910 File Offset: 0x000ABB10
		public static int IndexOf(Array array, [Nullable(2)] object value, int startIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000AC940 File Offset: 0x000ABB40
		public static int IndexOf(Array array, [Nullable(2)] object value, int startIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowRankException(ExceptionResource.Rank_MultiDimNotSupported);
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex + count;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
				return -1;
			}
			CorElementType corElementTypeOfElementType = array.GetCorElementTypeOfElementType();
			if (corElementTypeOfElementType.IsPrimitiveType())
			{
				if (value == null)
				{
					return lowerBound - 1;
				}
				if (array.IsValueOfElementType(value))
				{
					int adjustedIndex = startIndex - lowerBound;
					int num2 = -1;
					switch (corElementTypeOfElementType)
					{
					case CorElementType.ELEMENT_TYPE_BOOLEAN:
					case CorElementType.ELEMENT_TYPE_I1:
					case CorElementType.ELEMENT_TYPE_U1:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<byte>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_CHAR:
					case CorElementType.ELEMENT_TYPE_I2:
					case CorElementType.ELEMENT_TYPE_U2:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<char>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_I4:
					case CorElementType.ELEMENT_TYPE_U4:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<int>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_I8:
					case CorElementType.ELEMENT_TYPE_U8:
					case CorElementType.ELEMENT_TYPE_I:
					case CorElementType.ELEMENT_TYPE_U:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<long>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_R4:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<float>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_R8:
						num2 = Array.<IndexOf>g__GenericIndexOf|106_0<double>(array, value, adjustedIndex, count);
						break;
					}
					return ((num2 >= 0) ? startIndex : lowerBound) + num2;
				}
			}
			for (int k = startIndex; k < num; k++)
			{
				object value2 = array.GetValue(k);
				if (value2 == null)
				{
					if (value == null)
					{
						return k;
					}
				}
				else if (value2.Equals(value))
				{
					return k;
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000ACB17 File Offset: 0x000ABD17
		public static int IndexOf<[Nullable(2)] T>(T[] array, T value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000ACB2D File Offset: 0x000ABD2D
		public static int IndexOf<[Nullable(2)] T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000ACB48 File Offset: 0x000ABD48
		public unsafe static int IndexOf<[Nullable(2)] T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (startIndex > array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count > array.Length - startIndex)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					int num = SpanHelpers.IndexOf(Unsafe.Add<byte>(MemoryMarshal.GetArrayDataReference<byte>(Unsafe.As<byte[]>(array)), startIndex), *Unsafe.As<T, byte>(ref value), count);
					return ((num >= 0) ? startIndex : 0) + num;
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					int num2 = SpanHelpers.IndexOf(Unsafe.Add<char>(MemoryMarshal.GetArrayDataReference<char>(Unsafe.As<char[]>(array)), startIndex), *Unsafe.As<T, char>(ref value), count);
					return ((num2 >= 0) ? startIndex : 0) + num2;
				}
				if (Unsafe.SizeOf<T>() == 4)
				{
					int num3 = SpanHelpers.IndexOf<int>(Unsafe.Add<int>(MemoryMarshal.GetArrayDataReference<int>(Unsafe.As<int[]>(array)), startIndex), *Unsafe.As<T, int>(ref value), count);
					return ((num3 >= 0) ? startIndex : 0) + num3;
				}
				if (Unsafe.SizeOf<T>() == 8)
				{
					int num4 = SpanHelpers.IndexOf<long>(Unsafe.Add<long>(MemoryMarshal.GetArrayDataReference<long>(Unsafe.As<long[]>(array)), startIndex), *Unsafe.As<T, long>(ref value), count);
					return ((num4 >= 0) ? startIndex : 0) + num4;
				}
			}
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000ACC5C File Offset: 0x000ABE5C
		public static int LastIndexOf(Array array, [Nullable(2)] object value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, array.Length - 1 + lowerBound, array.Length);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000ACC94 File Offset: 0x000ABE94
		public static int LastIndexOf(Array array, [Nullable(2)] object value, int startIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1 - lowerBound);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000ACCC0 File Offset: 0x000ABEC0
		public static int LastIndexOf(Array array, [Nullable(2)] object value, int startIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			if (array.Length == 0)
			{
				return lowerBound - 1;
			}
			if (startIndex < lowerBound || startIndex >= array.Length + lowerBound)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (count > startIndex - lowerBound + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.endIndex, ExceptionResource.ArgumentOutOfRange_EndIndexStartIndex);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowRankException(ExceptionResource.Rank_MultiDimNotSupported);
			}
			int num = startIndex - count + 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
				return -1;
			}
			CorElementType corElementTypeOfElementType = array.GetCorElementTypeOfElementType();
			if (corElementTypeOfElementType.IsPrimitiveType())
			{
				if (value == null)
				{
					return lowerBound - 1;
				}
				if (array.IsValueOfElementType(value))
				{
					int adjustedIndex = num - lowerBound;
					int num2 = -1;
					switch (corElementTypeOfElementType)
					{
					case CorElementType.ELEMENT_TYPE_BOOLEAN:
					case CorElementType.ELEMENT_TYPE_I1:
					case CorElementType.ELEMENT_TYPE_U1:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<byte>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_CHAR:
					case CorElementType.ELEMENT_TYPE_I2:
					case CorElementType.ELEMENT_TYPE_U2:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<char>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_I4:
					case CorElementType.ELEMENT_TYPE_U4:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<int>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_I8:
					case CorElementType.ELEMENT_TYPE_U8:
					case CorElementType.ELEMENT_TYPE_I:
					case CorElementType.ELEMENT_TYPE_U:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<long>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_R4:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<float>(array, value, adjustedIndex, count);
						break;
					case CorElementType.ELEMENT_TYPE_R8:
						num2 = Array.<LastIndexOf>g__GenericLastIndexOf|112_0<double>(array, value, adjustedIndex, count);
						break;
					}
					return ((num2 >= 0) ? num : lowerBound) + num2;
				}
			}
			for (int k = startIndex; k >= num; k--)
			{
				object value2 = array.GetValue(k);
				if (value2 == null)
				{
					if (value == null)
					{
						return k;
					}
				}
				else if (value2.Equals(value))
				{
					return k;
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000ACEA9 File Offset: 0x000AC0A9
		public static int LastIndexOf<[Nullable(2)] T>(T[] array, T value)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000ACEC3 File Offset: 0x000AC0C3
		public static int LastIndexOf<[Nullable(2)] T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000ACEE0 File Offset: 0x000AC0E0
		public unsafe static int LastIndexOf<[Nullable(2)] T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
				if (count != 0)
				{
					ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
				}
				return -1;
			}
			if (startIndex >= array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					int num = startIndex - count + 1;
					int num2 = SpanHelpers.LastIndexOf(Unsafe.Add<byte>(MemoryMarshal.GetArrayDataReference<byte>(Unsafe.As<byte[]>(array)), num), *Unsafe.As<T, byte>(ref value), count);
					return ((num2 >= 0) ? num : 0) + num2;
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					int num3 = startIndex - count + 1;
					int num4 = SpanHelpers.LastIndexOf(Unsafe.Add<char>(MemoryMarshal.GetArrayDataReference<char>(Unsafe.As<char[]>(array)), num3), *Unsafe.As<T, char>(ref value), count);
					return ((num4 >= 0) ? num3 : 0) + num4;
				}
				if (Unsafe.SizeOf<T>() == 4)
				{
					int num5 = startIndex - count + 1;
					int num6 = SpanHelpers.LastIndexOf<int>(Unsafe.Add<int>(MemoryMarshal.GetArrayDataReference<int>(Unsafe.As<int[]>(array)), num5), *Unsafe.As<T, int>(ref value), count);
					return ((num6 >= 0) ? num5 : 0) + num6;
				}
				if (Unsafe.SizeOf<T>() == 8)
				{
					int num7 = startIndex - count + 1;
					int num8 = SpanHelpers.LastIndexOf<long>(Unsafe.Add<long>(MemoryMarshal.GetArrayDataReference<long>(Unsafe.As<long[]>(array)), num7), *Unsafe.As<T, long>(ref value), count);
					return ((num8 >= 0) ? num7 : 0) + num8;
				}
			}
			return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000AD034 File Offset: 0x000AC234
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000AD054 File Offset: 0x000AC254
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - (index - lowerBound) < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowRankException(ExceptionResource.Rank_MultiDimNotSupported);
			}
			if (length <= 1)
			{
				return;
			}
			int adjustedIndex = index - lowerBound;
			switch (array.GetCorElementTypeOfElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
			case CorElementType.ELEMENT_TYPE_I1:
			case CorElementType.ELEMENT_TYPE_U1:
				Array.UnsafeArrayAsSpan<byte>(array, adjustedIndex, length).Reverse<byte>();
				return;
			case CorElementType.ELEMENT_TYPE_CHAR:
			case CorElementType.ELEMENT_TYPE_I2:
			case CorElementType.ELEMENT_TYPE_U2:
				Array.UnsafeArrayAsSpan<short>(array, adjustedIndex, length).Reverse<short>();
				return;
			case CorElementType.ELEMENT_TYPE_I4:
			case CorElementType.ELEMENT_TYPE_U4:
			case CorElementType.ELEMENT_TYPE_R4:
				Array.UnsafeArrayAsSpan<int>(array, adjustedIndex, length).Reverse<int>();
				return;
			case CorElementType.ELEMENT_TYPE_I8:
			case CorElementType.ELEMENT_TYPE_U8:
			case CorElementType.ELEMENT_TYPE_R8:
			case CorElementType.ELEMENT_TYPE_I:
			case CorElementType.ELEMENT_TYPE_U:
				Array.UnsafeArrayAsSpan<long>(array, adjustedIndex, length).Reverse<long>();
				return;
			case CorElementType.ELEMENT_TYPE_ARRAY:
			case CorElementType.ELEMENT_TYPE_OBJECT:
			case CorElementType.ELEMENT_TYPE_SZARRAY:
				Array.UnsafeArrayAsSpan<object>(array, adjustedIndex, length).Reverse<object>();
				return;
			}
			int i = index;
			int num = index + length - 1;
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000AD1B0 File Offset: 0x000AC3B0
		public static void Reverse<[Nullable(2)] T>(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			Array.Reverse<T>(array, 0, array.Length);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000AD1C8 File Offset: 0x000AC3C8
		public static void Reverse<[Nullable(2)] T>(T[] array, int index, int length)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length <= 1)
			{
				return;
			}
			ref T ptr = ref Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), index);
			ref T ptr2 = ref Unsafe.Add<T>(Unsafe.Add<T>(ref ptr, length), -1);
			do
			{
				T t = ptr;
				ptr = ptr2;
				ptr2 = t;
				ptr = Unsafe.Add<T>(ref ptr, 1);
				ptr2 = Unsafe.Add<T>(ref ptr2, -1);
			}
			while (Unsafe.IsAddressLessThan<T>(ref ptr, ref ptr2));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000AD252 File Offset: 0x000AC452
		public static void Sort(Array array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000AD272 File Offset: 0x000AC472
		public static void Sort(Array keys, [Nullable(2)] Array items)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000AD293 File Offset: 0x000AC493
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000AD29F File Offset: 0x000AC49F
		public static void Sort(Array keys, [Nullable(2)] Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000AD2AB File Offset: 0x000AC4AB
		public static void Sort(Array array, [Nullable(2)] IComparer comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000AD2CB File Offset: 0x000AC4CB
		[NullableContext(2)]
		public static void Sort([Nullable(1)] Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000AD2EC File Offset: 0x000AC4EC
		public static void Sort(Array array, int index, int length, [Nullable(2)] IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000AD2F8 File Offset: 0x000AC4F8
		[NullableContext(2)]
		public static void Sort([Nullable(1)] Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				ThrowHelper.ThrowRankException(ExceptionResource.Rank_MultiDimNotSupported);
			}
			int lowerBound = keys.GetLowerBound(0);
			if (items != null && lowerBound != items.GetLowerBound(0))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_LowerBoundsMustMatch);
			}
			if (index < lowerBound)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (keys.Length - (index - lowerBound) < length || (items != null && index - lowerBound > items.Length - length))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length <= 1)
			{
				return;
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			object[] array = keys as object[];
			if (array != null)
			{
				object[] array2 = items as object[];
				if (items == null || array2 != null)
				{
					new Array.SorterObjectArray(array, array2, comparer).Sort(index, length);
					return;
				}
			}
			if (comparer == Comparer.Default)
			{
				CorElementType corElementTypeOfElementType = keys.GetCorElementTypeOfElementType();
				if (items == null || items.GetCorElementTypeOfElementType() == corElementTypeOfElementType)
				{
					int adjustedIndex = index - lowerBound;
					switch (corElementTypeOfElementType)
					{
					case CorElementType.ELEMENT_TYPE_BOOLEAN:
					case CorElementType.ELEMENT_TYPE_U1:
						Array.<Sort>g__GenericSort|127_0<byte>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_CHAR:
					case CorElementType.ELEMENT_TYPE_U2:
						Array.<Sort>g__GenericSort|127_0<ushort>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_I1:
						Array.<Sort>g__GenericSort|127_0<sbyte>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_I2:
						Array.<Sort>g__GenericSort|127_0<short>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_I4:
						Array.<Sort>g__GenericSort|127_0<int>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_U4:
						Array.<Sort>g__GenericSort|127_0<uint>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_I8:
					case CorElementType.ELEMENT_TYPE_I:
						Array.<Sort>g__GenericSort|127_0<long>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_U8:
					case CorElementType.ELEMENT_TYPE_U:
						Array.<Sort>g__GenericSort|127_0<ulong>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_R4:
						Array.<Sort>g__GenericSort|127_0<float>(keys, items, adjustedIndex, length);
						return;
					case CorElementType.ELEMENT_TYPE_R8:
						Array.<Sort>g__GenericSort|127_0<double>(keys, items, adjustedIndex, length);
						return;
					}
				}
			}
			new Array.SorterGenericArray(keys, items, comparer).Sort(index, length);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000AD4D0 File Offset: 0x000AC6D0
		public static void Sort<[Nullable(2)] T>(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Length > 1)
			{
				Span<T> keys = new Span<T>(MemoryMarshal.GetArrayDataReference<T>(array), array.Length);
				ArraySortHelper<T>.Default.Sort(keys, null);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000AD507 File Offset: 0x000AC707
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(1)] TKey[] keys, [Nullable(new byte[]
		{
			2,
			1
		})] TValue[] items)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000AD51F File Offset: 0x000AC71F
		public static void Sort<[Nullable(2)] T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000AD52A File Offset: 0x000AC72A
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(1)] TKey[] keys, [Nullable(new byte[]
		{
			2,
			1
		})] TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000AD536 File Offset: 0x000AC736
		public static void Sort<[Nullable(2)] T>(T[] array, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000AD54C File Offset: 0x000AC74C
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(1)] TKey[] keys, [Nullable(new byte[]
		{
			2,
			1
		})] TValue[] items, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000AD564 File Offset: 0x000AC764
		public static void Sort<[Nullable(2)] T>(T[] array, int index, int length, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length > 1)
			{
				Span<T> keys = new Span<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), index), length);
				ArraySortHelper<T>.Default.Sort(keys, comparer);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000AD5C0 File Offset: 0x000AC7C0
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(1)] TKey[] keys, [Nullable(new byte[]
		{
			2,
			1
		})] TValue[] items, int index, int length, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keys);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length > 1)
			{
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				Span<TKey> keys2 = new Span<TKey>(Unsafe.Add<TKey>(MemoryMarshal.GetArrayDataReference<TKey>(keys), index), length);
				Span<TValue> values = new Span<TValue>(Unsafe.Add<TValue>(MemoryMarshal.GetArrayDataReference<TValue>(items), index), length);
				ArraySortHelper<TKey, TValue>.Default.Sort(keys2, values, comparer);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000AD64C File Offset: 0x000AC84C
		public static void Sort<[Nullable(2)] T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			Span<T> keys = new Span<T>(MemoryMarshal.GetArrayDataReference<T>(array), array.Length);
			ArraySortHelper<T>.Sort(keys, comparison);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000AD684 File Offset: 0x000AC884
		public static bool TrueForAll<[Nullable(2)] T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000AD6C4 File Offset: 0x000AC8C4
		private static Span<T> UnsafeArrayAsSpan<T>(Array array, int adjustedIndex, int length)
		{
			return new Span<T>(Unsafe.As<byte, T>(array.GetRawArrayData()), array.Length).Slice(adjustedIndex, length);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000AD6F4 File Offset: 0x000AC8F4
		public IEnumerator GetEnumerator()
		{
			int lowerBound = this.GetLowerBound(0);
			if (this.Rank == 1 && lowerBound == 0)
			{
				return new SZArrayEnumerator(this);
			}
			return new ArrayEnumerator(this, lowerBound, this.Length);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000AD729 File Offset: 0x000AC929
		[CompilerGenerated]
		internal unsafe static int <BinarySearch>g__GenericBinarySearch|81_0<T>(Array array, int adjustedIndex, int length, object value) where T : struct, IComparable<T>
		{
			return Array.UnsafeArrayAsSpan<T>(array, adjustedIndex, length).BinarySearch(*Unsafe.As<byte, T>(value.GetRawData()));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000AD748 File Offset: 0x000AC948
		[CompilerGenerated]
		internal unsafe static int <IndexOf>g__GenericIndexOf|106_0<T>(Array array, object value, int adjustedIndex, int length) where T : struct, IEquatable<T>
		{
			return Array.UnsafeArrayAsSpan<T>(array, adjustedIndex, length).IndexOf(*Unsafe.As<byte, T>(value.GetRawData()));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000AD767 File Offset: 0x000AC967
		[CompilerGenerated]
		internal unsafe static int <LastIndexOf>g__GenericLastIndexOf|112_0<T>(Array array, object value, int adjustedIndex, int length) where T : struct, IEquatable<T>
		{
			return Array.UnsafeArrayAsSpan<T>(array, adjustedIndex, length).LastIndexOf(*Unsafe.As<byte, T>(value.GetRawData()));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000AD788 File Offset: 0x000AC988
		[CompilerGenerated]
		internal static void <Sort>g__GenericSort|127_0<T>(Array keys, Array items, int adjustedIndex, int length) where T : struct
		{
			Span<T> span = Array.UnsafeArrayAsSpan<T>(keys, adjustedIndex, length);
			if (items != null)
			{
				span.Sort(Array.UnsafeArrayAsSpan<T>(items, adjustedIndex, length));
				return;
			}
			span.Sort<T>();
		}

		// Token: 0x02000051 RID: 81
		private static class EmptyArray<T>
		{
			// Token: 0x040000D2 RID: 210
			internal static readonly T[] Value = new T[0];
		}

		// Token: 0x02000052 RID: 82
		private readonly struct SorterObjectArray
		{
			// Token: 0x06000171 RID: 369 RVA: 0x000AD7C3 File Offset: 0x000AC9C3
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x000AD7DC File Offset: 0x000AC9DC
			internal void SwapIfGreater(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x06000173 RID: 371 RVA: 0x000AD858 File Offset: 0x000ACA58
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x06000174 RID: 372 RVA: 0x000AD8B1 File Offset: 0x000ACAB1
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x06000175 RID: 373 RVA: 0x000AD8BC File Offset: 0x000ACABC
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * (BitOperations.Log2((uint)length) + 1));
				}
				catch (IndexOutOfRangeException)
				{
					ThrowHelper.ThrowArgumentException_BadComparer(this.comparer);
				}
				catch (Exception e)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
				}
			}

			// Token: 0x06000176 RID: 374 RVA: 0x000AD91C File Offset: 0x000ACB1C
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 2)
						{
							this.SwapIfGreater(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreater(lo, hi - 1);
							this.SwapIfGreater(lo, hi);
							this.SwapIfGreater(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x06000177 RID: 375 RVA: 0x000AD99C File Offset: 0x000ACB9C
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreater(lo, num);
				this.SwapIfGreater(lo, hi);
				this.SwapIfGreater(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				if (i != hi - 1)
				{
					this.Swap(i, hi - 1);
				}
				return i;
			}

			// Token: 0x06000178 RID: 376 RVA: 0x000ADA3C File Offset: 0x000ACC3C
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x000ADA8C File Offset: 0x000ACC8C
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object[] array = this.items;
				object obj2 = (array != null) ? array[lo + i - 1] : null;
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x0600017A RID: 378 RVA: 0x000ADB70 File Offset: 0x000ACD70
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object[] array = this.items;
					object obj2 = (array != null) ? array[i + 1] : null;
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x040000D3 RID: 211
			private readonly object[] keys;

			// Token: 0x040000D4 RID: 212
			private readonly object[] items;

			// Token: 0x040000D5 RID: 213
			private readonly IComparer comparer;
		}

		// Token: 0x02000053 RID: 83
		private readonly struct SorterGenericArray
		{
			// Token: 0x0600017B RID: 379 RVA: 0x000ADC19 File Offset: 0x000ACE19
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x0600017C RID: 380 RVA: 0x000ADC30 File Offset: 0x000ACE30
			internal void SwapIfGreater(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x0600017D RID: 381 RVA: 0x000ADCD8 File Offset: 0x000ACED8
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x0600017E RID: 382 RVA: 0x000ADD51 File Offset: 0x000ACF51
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x0600017F RID: 383 RVA: 0x000ADD5C File Offset: 0x000ACF5C
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * (BitOperations.Log2((uint)length) + 1));
				}
				catch (IndexOutOfRangeException)
				{
					ThrowHelper.ThrowArgumentException_BadComparer(this.comparer);
				}
				catch (Exception e)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
				}
			}

			// Token: 0x06000180 RID: 384 RVA: 0x000ADDBC File Offset: 0x000ACFBC
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 2)
						{
							this.SwapIfGreater(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreater(lo, hi - 1);
							this.SwapIfGreater(lo, hi);
							this.SwapIfGreater(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x06000181 RID: 385 RVA: 0x000ADE3C File Offset: 0x000AD03C
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreater(lo, num);
				this.SwapIfGreater(lo, hi);
				this.SwapIfGreater(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				if (i != hi - 1)
				{
					this.Swap(i, hi - 1);
				}
				return i;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x000ADEE8 File Offset: 0x000AD0E8
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x06000183 RID: 387 RVA: 0x000ADF38 File Offset: 0x000AD138
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				Array array = this.items;
				object value2 = (array != null) ? array.GetValue(lo + i - 1) : null;
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(value2, lo + i - 1);
				}
			}

			// Token: 0x06000184 RID: 388 RVA: 0x000AE048 File Offset: 0x000AD248
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					Array array = this.items;
					object value2 = (array != null) ? array.GetValue(i + 1) : null;
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(value2, num + 1);
					}
				}
			}

			// Token: 0x040000D6 RID: 214
			private readonly Array keys;

			// Token: 0x040000D7 RID: 215
			private readonly Array items;

			// Token: 0x040000D8 RID: 216
			private readonly IComparer comparer;
		}
	}
}
