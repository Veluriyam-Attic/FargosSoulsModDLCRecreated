using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CC RID: 204
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct ArraySegment<[Nullable(2)] T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x000C8DA2 File Offset: 0x000C7FA2
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static ArraySegment<T> Empty { [return: Nullable(new byte[]
		{
			0,
			1
		})] get; } = new ArraySegment<T>(new T[0]);

		// Token: 0x06000A5E RID: 2654 RVA: 0x000C8DA9 File Offset: 0x000C7FA9
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000C8DCB File Offset: 0x000C7FCB
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null || offset > array.Length || count > array.Length - offset)
			{
				ThrowHelper.ThrowArraySegmentCtorValidationFailedExceptions(array, offset, count);
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x000C8DFB File Offset: 0x000C7FFB
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public T[] Array
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._array;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x000C8E03 File Offset: 0x000C8003
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x000C8E0B File Offset: 0x000C800B
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000113 RID: 275
		public T this[int index]
		{
			get
			{
				if (index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
			set
			{
				if (index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000C8E5A File Offset: 0x000C805A
		[NullableContext(0)]
		public ArraySegment<T>.Enumerator GetEnumerator()
		{
			this.ThrowInvalidOperationIfDefault();
			return new ArraySegment<T>.Enumerator(this);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000C8E6D File Offset: 0x000C806D
		public override int GetHashCode()
		{
			if (this._array != null)
			{
				return HashCode.Combine<int, int, int>(this._offset, this._count, this._array.GetHashCode());
			}
			return 0;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000C8E95 File Offset: 0x000C8095
		public void CopyTo(T[] destination)
		{
			this.CopyTo(destination, 0);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000C8E9F File Offset: 0x000C809F
		public void CopyTo(T[] destination, int destinationIndex)
		{
			this.ThrowInvalidOperationIfDefault();
			System.Array.Copy(this._array, this._offset, destination, destinationIndex, this._count);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000C8EC0 File Offset: 0x000C80C0
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> destination)
		{
			this.ThrowInvalidOperationIfDefault();
			destination.ThrowInvalidOperationIfDefault();
			if (this._count > destination._count)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			System.Array.Copy(this._array, this._offset, destination._array, destination._offset, this._count);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000C8F10 File Offset: 0x000C8110
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000C8F28 File Offset: 0x000C8128
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000C8F56 File Offset: 0x000C8156
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ArraySegment<T> Slice(int index)
		{
			this.ThrowInvalidOperationIfDefault();
			if (index > this._count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return new ArraySegment<T>(this._array, this._offset + index, this._count - index);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000C8F87 File Offset: 0x000C8187
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ArraySegment<T> Slice(int index, int count)
		{
			this.ThrowInvalidOperationIfDefault();
			if (index > this._count || count > this._count - index)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return new ArraySegment<T>(this._array, this._offset + index, count);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000C8FBC File Offset: 0x000C81BC
		public T[] ToArray()
		{
			this.ThrowInvalidOperationIfDefault();
			if (this._count == 0)
			{
				return ArraySegment<T>.Empty._array;
			}
			T[] array = new T[this._count];
			System.Array.Copy(this._array, this._offset, array, 0, this._count);
			return array;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000C9008 File Offset: 0x000C8208
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> a, [Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000C9012 File Offset: 0x000C8212
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> a, [Nullable(new byte[]
		{
			0,
			1
		})] ArraySegment<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000C9020 File Offset: 0x000C8220
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static implicit operator ArraySegment<T>(T[] array)
		{
			if (array == null)
			{
				return default(ArraySegment<T>);
			}
			return new ArraySegment<T>(array);
		}

		// Token: 0x17000114 RID: 276
		T IList<!0>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
			set
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000C909C File Offset: 0x000C829C
		int IList<!0>.IndexOf(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000C90D6 File Offset: 0x000C82D6
		void IList<!0>.Insert(int index, T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000C90D6 File Offset: 0x000C82D6
		void IList<!0>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x17000115 RID: 277
		T IReadOnlyList<!0>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x000AC09E File Offset: 0x000AB29E
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000C90D6 File Offset: 0x000C82D6
		void ICollection<!0>.Add(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000C90D6 File Offset: 0x000C82D6
		void ICollection<!0>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000C90E0 File Offset: 0x000C82E0
		bool ICollection<!0>.Contains(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			return num >= 0;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000C9113 File Offset: 0x000C8313
		bool ICollection<!0>.Remove(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
			return false;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000C911B File Offset: 0x000C831B
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000C911B File Offset: 0x000C831B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000C9128 File Offset: 0x000C8328
		private void ThrowInvalidOperationIfDefault()
		{
			if (this._array == null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NullArray);
			}
		}

		// Token: 0x04000282 RID: 642
		private readonly T[] _array;

		// Token: 0x04000283 RID: 643
		private readonly int _offset;

		// Token: 0x04000284 RID: 644
		private readonly int _count;

		// Token: 0x020000CD RID: 205
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000A81 RID: 2689 RVA: 0x000C914B File Offset: 0x000C834B
			internal Enumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment.Array;
				this._start = arraySegment.Offset;
				this._end = arraySegment.Offset + arraySegment.Count;
				this._current = arraySegment.Offset - 1;
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x000C918B File Offset: 0x000C838B
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000C91B9 File Offset: 0x000C83B9
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					if (this._current < this._start)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
					}
					if (this._current >= this._end)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
					}
					return this._array[this._current];
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000C91F2 File Offset: 0x000C83F2
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A85 RID: 2693 RVA: 0x000C91FF File Offset: 0x000C83FF
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Dispose()
			{
			}

			// Token: 0x04000285 RID: 645
			private readonly T[] _array;

			// Token: 0x04000286 RID: 646
			private readonly int _start;

			// Token: 0x04000287 RID: 647
			private readonly int _end;

			// Token: 0x04000288 RID: 648
			private int _current;
		}
	}
}
