using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000176 RID: 374
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[NonVersionable]
	public readonly ref struct ReadOnlySpan<[Nullable(2)] T>
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x000E84C3 File Offset: 0x000E76C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlySpan<T>);
				return;
			}
			this._pointer = new ByReference<T>(MemoryMarshal.GetArrayDataReference<T>(array));
			this._length = array.Length;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000E84EC File Offset: 0x000E76EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan([Nullable(new byte[]
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
				this = default(ReadOnlySpan<T>);
				return;
			}
			if ((ulong)start + (ulong)length > (ulong)array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), start));
			this._length = length;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000E853D File Offset: 0x000E773D
		[CLSCompliant(false)]
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
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

		// Token: 0x060012B9 RID: 4793 RVA: 0x000E8576 File Offset: 0x000E7776
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x170001A3 RID: 419
		public T this[int index]
		{
			[NonVersionable]
			[Intrinsic]
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000E85AC File Offset: 0x000E77AC
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000E85B4 File Offset: 0x000E77B4
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return 0 >= this._length;
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000E85C2 File Offset: 0x000E77C2
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000E85CE File Offset: 0x000E77CE
		[NullableContext(2)]
		[Obsolete("Equals() on ReadOnlySpan will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000E85DA File Offset: 0x000E77DA
		[Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000E85E6 File Offset: 0x000E77E6
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ReadOnlySpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000E85EE File Offset: 0x000E77EE
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ReadOnlySpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> segment)
		{
			return new ReadOnlySpan<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000E860C File Offset: 0x000E780C
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlySpan<T> Empty
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000E8622 File Offset: 0x000E7822
		[NullableContext(0)]
		public ReadOnlySpan<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySpan<T>.Enumerator(this);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000E8630 File Offset: 0x000E7830
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ref readonly T GetPinnableReference()
		{
			ref T result = ref Unsafe.NullRef<T>();
			if (this._length != 0)
			{
				result = this._pointer.Value;
			}
			return ref result;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000E8658 File Offset: 0x000E7858
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

		// Token: 0x060012C6 RID: 4806 RVA: 0x000E8694 File Offset: 0x000E7894
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

		// Token: 0x060012C7 RID: 4807 RVA: 0x000E86D8 File Offset: 0x000E78D8
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000E8708 File Offset: 0x000E7908
		public override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				return new string(new ReadOnlySpan<char>(Unsafe.As<T, char>(this._pointer.Value), this._length));
			}
			return string.Format("System.ReadOnlySpan<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000E8775 File Offset: 0x000E7975
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000E87A3 File Offset: 0x000E79A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if ((ulong)start + (ulong)length > (ulong)this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000E87D0 File Offset: 0x000E79D0
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

		// Token: 0x04000484 RID: 1156
		internal readonly ByReference<T> _pointer;

		// Token: 0x04000485 RID: 1157
		private readonly int _length;

		// Token: 0x02000177 RID: 375
		[Nullable(0)]
		public ref struct Enumerator
		{
			// Token: 0x060012CC RID: 4812 RVA: 0x000E8815 File Offset: 0x000E7A15
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(ReadOnlySpan<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x060012CD RID: 4813 RVA: 0x000E8828 File Offset: 0x000E7A28
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

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x000E8856 File Offset: 0x000E7A56
			public ref readonly T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04000486 RID: 1158
			private readonly ReadOnlySpan<T> _span;

			// Token: 0x04000487 RID: 1159
			private int _index;
		}
	}
}
