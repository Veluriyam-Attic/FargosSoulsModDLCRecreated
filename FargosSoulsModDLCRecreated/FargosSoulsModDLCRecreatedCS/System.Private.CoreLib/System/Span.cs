using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200017D RID: 381
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[NonVersionable]
	public readonly ref struct Span<[Nullable(2)] T>
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x000E8F44 File Offset: 0x000E8144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._pointer = new ByReference<T>(MemoryMarshal.GetArrayDataReference<T>(array));
			this._length = array.Length;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000E8FA4 File Offset: 0x000E81A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span([Nullable(new byte[]
		{
			2,
			1
		})] T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Span<T>);
				return;
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if ((ulong)start + (ulong)length > (ulong)array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), start));
			this._length = length;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000E9022 File Offset: 0x000E8222
		[NullableContext(0)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(ref *(byte*)pointer));
			this._length = length;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000E905B File Offset: 0x000E825B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x170001AA RID: 426
		public T this[int index]
		{
			[Intrinsic]
			[NonVersionable]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return Unsafe.Add<T>(this._pointer.Value, index);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x000E9091 File Offset: 0x000E8291
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000E9099 File Offset: 0x000E8299
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return 0 >= this._length;
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000E90A7 File Offset: 0x000E82A7
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000E85CE File Offset: 0x000E77CE
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000E85DA File Offset: 0x000E77DA
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000E90B3 File Offset: 0x000E82B3
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator Span<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000E90BB File Offset: 0x000E82BB
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator Span<T>([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x000E90D8 File Offset: 0x000E82D8
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> Empty
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000E90EE File Offset: 0x000E82EE
		[NullableContext(0)]
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000E90FC File Offset: 0x000E82FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ref T GetPinnableReference()
		{
			ref T result = ref Unsafe.NullRef<T>();
			if (this._length != 0)
			{
				result = this._pointer.Value;
			}
			return ref result;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000E9124 File Offset: 0x000E8324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				SpanHelpers.ClearWithReferences(Unsafe.As<T, IntPtr>(this._pointer.Value), (UIntPtr)((IntPtr)this._length * (IntPtr)(Unsafe.SizeOf<T>() / sizeof(UIntPtr))));
				return;
			}
			SpanHelpers.ClearWithoutReferences(Unsafe.As<T, byte>(this._pointer.Value), (UIntPtr)((IntPtr)this._length * (IntPtr)Unsafe.SizeOf<T>()));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000E9188 File Offset: 0x000E8388
		public unsafe void Fill(T value)
		{
			if (Unsafe.SizeOf<T>() == 1)
			{
				uint length = (uint)this._length;
				if (length == 0U)
				{
					return;
				}
				T t = value;
				Unsafe.InitBlockUnaligned(Unsafe.As<T, byte>(this._pointer.Value), *Unsafe.As<T, byte>(ref t), length);
				return;
			}
			else
			{
				UIntPtr uintPtr = (UIntPtr)this._length;
				if (uintPtr == 0)
				{
					return;
				}
				ref T value2 = ref this._pointer.Value;
				UIntPtr uintPtr2 = (UIntPtr)Unsafe.SizeOf<T>();
				UIntPtr uintPtr3;
				for (uintPtr3 = (UIntPtr)((IntPtr)0); uintPtr3 < (uintPtr & (UIntPtr)(~(UIntPtr)((IntPtr)7))); uintPtr3 += (UIntPtr)((IntPtr)8))
				{
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)0)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)1)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)2)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)3)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)4)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)5)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)6)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)7)) * uintPtr2) = value;
				}
				if (uintPtr3 < (uintPtr & (UIntPtr)(~(UIntPtr)((IntPtr)3))))
				{
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)0)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)1)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)2)) * uintPtr2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (uintPtr3 + (UIntPtr)((IntPtr)3)) * uintPtr2) = value;
					uintPtr3 += (UIntPtr)((IntPtr)4);
				}
				while (uintPtr3 < uintPtr)
				{
					*Unsafe.AddByteOffset<T>(ref value2, uintPtr3 * uintPtr2) = value;
					uintPtr3++;
				}
				return;
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000E9323 File Offset: 0x000E8523
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (UIntPtr)((IntPtr)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000E9360 File Offset: 0x000E8560
		public bool TryCopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> destination)
		{
			bool result = false;
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (UIntPtr)((IntPtr)this._length));
				result = true;
			}
			return result;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000E93A4 File Offset: 0x000E85A4
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000E93D3 File Offset: 0x000E85D3
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ReadOnlySpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pointer.Value, span._length);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000E93EC File Offset: 0x000E85EC
		public override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				return new string(new ReadOnlySpan<char>(Unsafe.As<T, char>(this._pointer.Value), this._length));
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000E9459 File Offset: 0x000E8659
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000E9487 File Offset: 0x000E8687
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public Span<T> Slice(int start, int length)
		{
			if ((ulong)start + (ulong)length > (ulong)this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000E94B4 File Offset: 0x000E86B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._length];
			Buffer.Memmove<T>(MemoryMarshal.GetArrayDataReference<T>(array), this._pointer.Value, (UIntPtr)((IntPtr)this._length));
			return array;
		}

		// Token: 0x04000494 RID: 1172
		internal readonly ByReference<T> _pointer;

		// Token: 0x04000495 RID: 1173
		private readonly int _length;

		// Token: 0x0200017E RID: 382
		[Nullable(0)]
		public ref struct Enumerator
		{
			// Token: 0x0600134A RID: 4938 RVA: 0x000E94F9 File Offset: 0x000E86F9
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x0600134B RID: 4939 RVA: 0x000E950C File Offset: 0x000E870C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x0600134C RID: 4940 RVA: 0x000E953A File Offset: 0x000E873A
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04000496 RID: 1174
			private readonly Span<T> _span;

			// Token: 0x04000497 RID: 1175
			private int _index;
		}
	}
}
