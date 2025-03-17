using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000805 RID: 2053
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class List<[Nullable(2)] T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x060061C6 RID: 25030 RVA: 0x001D23AB File Offset: 0x001D15AB
		public List()
		{
			this._items = List<T>.s_emptyArray;
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x001D23BE File Offset: 0x001D15BE
		public List(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = List<T>.s_emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x001D23F0 File Offset: 0x001D15F0
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 == null)
			{
				this._items = List<T>.s_emptyArray;
				foreach (!0 item in collection)
				{
					this.Add(item);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = List<T>.s_emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060061C9 RID: 25033 RVA: 0x001D2494 File Offset: 0x001D1694
		// (set) Token: 0x060061CA RID: 25034 RVA: 0x001D24A0 File Offset: 0x001D16A0
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, array, this._size);
						}
						this._items = array;
						return;
					}
					this._items = List<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x001D2502 File Offset: 0x001D1702
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060061CC RID: 25036 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060061CE RID: 25038 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060061CF RID: 25039 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x000AC098 File Offset: 0x000AB298
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700102B RID: 4139
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x001CC0C8 File Offset: 0x001CB2C8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x1700102C RID: 4140
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(T));
				}
			}
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x001D25A8 File Offset: 0x001D17A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			this._version++;
			T[] items = this._items;
			int size = this._size;
			if (size < items.Length)
			{
				this._size = size + 1;
				items[size] = item;
				return;
			}
			this.AddWithResize(item);
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x001D25F0 File Offset: 0x001D17F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithResize(T item)
		{
			int size = this._size;
			this.EnsureCapacity(size + 1);
			this._size = size + 1;
			this._items[size] = item;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x001D2624 File Offset: 0x001D1824
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(item, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x001D2674 File Offset: 0x001D1874
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x001D2683 File Offset: 0x001D1883
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x001D268B File Offset: 0x001D188B
		public int BinarySearch(int index, int count, T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return Array.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x001D26C5 File Offset: 0x001D18C5
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x001D26D6 File Offset: 0x001D18D6
		public int BinarySearch(T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x001D26E8 File Offset: 0x001D18E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			this._version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				int size = this._size;
				this._size = 0;
				if (size > 0)
				{
					Array.Clear(this._items, 0, size);
					return;
				}
			}
			else
			{
				this._size = 0;
			}
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x001D2731 File Offset: 0x001D1931
		public bool Contains(T item)
		{
			return this._size != 0 && this.IndexOf(item) != -1;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x001D274A File Offset: 0x001D194A
		bool IList.Contains(object item)
		{
			return List<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x001D2764 File Offset: 0x001D1964
		public List<TOutput> ConvertAll<[Nullable(2)] TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			List<TOutput> list = new List<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				list._items[i] = converter(this._items[i]);
			}
			list._size = this._size;
			return list;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x001D27C3 File Offset: 0x001D19C3
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x001D27D0 File Offset: 0x001D19D0
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				Array.Copy(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x001D2820 File Offset: 0x001D1A20
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x001D2845 File Offset: 0x001D1A45
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x001D285C File Offset: 0x001D1A5C
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = (this._items.Length == 0) ? 4 : (this._items.Length * 2);
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x001D28A6 File Offset: 0x001D1AA6
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x001D28B8 File Offset: 0x001D1AB8
		[return: Nullable(2)]
		public T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x001D290C File Offset: 0x001D1B0C
		public List<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					list.Add(this._items[i]);
				}
			}
			return list;
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x001D2961 File Offset: 0x001D1B61
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x001D2971 File Offset: 0x001D1B71
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x001D2984 File Offset: 0x001D1B84
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex > this._size - count)
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
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x001D29E4 File Offset: 0x001D1BE4
		[return: Nullable(2)]
		public T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x001D2A38 File Offset: 0x001D1C38
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x001D2A4F File Offset: 0x001D1C4F
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x001D2A5C File Offset: 0x001D1C5C
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
			}
			else if (startIndex >= this._size)
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
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x001D2ACC File Offset: 0x001D1CCC
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && version == this._version)
			{
				action(this._items[num]);
				num++;
			}
			if (version != this._version)
			{
				ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
			}
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x001D2B24 File Offset: 0x001D1D24
		[NullableContext(0)]
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x001D2B2C File Offset: 0x001D1D2C
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x001D2B2C File Offset: 0x001D1D2C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x001D2B3C File Offset: 0x001D1D3C
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			List<T> list = new List<T>(count);
			Array.Copy(this._items, index, list._items, 0, count);
			list._size = count;
			return list;
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x001D2B94 File Offset: 0x001D1D94
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x001D2BA9 File Offset: 0x001D1DA9
		int IList.IndexOf(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x001D2BC1 File Offset: 0x001D1DC1
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x001D2BE6 File Offset: 0x001D1DE6
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			return Array.IndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x001D2C18 File Offset: 0x001D1E18
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x001D2CA4 File Offset: 0x001D1EA4
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(item, typeof(T));
			}
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x001D2CEC File Offset: 0x001D1EEC
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						collection2.CopyTo(this._items, index);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (!0 item in collection)
				{
					this.Insert(index++, item);
				}
			}
			this._version++;
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x001D2E04 File Offset: 0x001D2004
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x001D2E25 File Offset: 0x001D2025
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x001D2E40 File Offset: 0x001D2040
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x001D2EAC File Offset: 0x001D20AC
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x001D2ECF File Offset: 0x001D20CF
		void IList.Remove(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x001D2EE8 File Offset: 0x001D20E8
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._items, num, this._size - num);
			}
			int result = this._size - num;
			this._size = num;
			this._version++;
			return result;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x001D2FC4 File Offset: 0x001D21C4
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._items[this._size] = default(T);
			}
			this._version++;
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x001D3044 File Offset: 0x001D2244
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				this._version++;
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					Array.Clear(this._items, this._size, count);
				}
			}
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x001D30D8 File Offset: 0x001D22D8
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x001D30E8 File Offset: 0x001D22E8
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				Array.Reverse<T>(this._items, index, count);
			}
			this._version++;
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x001D313C File Offset: 0x001D233C
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x001D314C File Offset: 0x001D234C
		public void Sort([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x001D315C File Offset: 0x001D235C
		public void Sort(int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				Array.Sort<T>(this._items, index, count, comparer);
			}
			this._version++;
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x001D31B1 File Offset: 0x001D23B1
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (this._size > 1)
			{
				ArraySortHelper<T>.Sort(new Span<T>(this._items, 0, this._size), comparison);
			}
			this._version++;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x001D31EC File Offset: 0x001D23EC
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return List<T>.s_emptyArray;
			}
			T[] array = new T[this._size];
			Array.Copy(this._items, array, this._size);
			return array;
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x001D3228 File Offset: 0x001D2428
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x001D3260 File Offset: 0x001D2460
		public bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match(this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001D3C RID: 7484
		internal T[] _items;

		// Token: 0x04001D3D RID: 7485
		internal int _size;

		// Token: 0x04001D3E RID: 7486
		private int _version;

		// Token: 0x04001D3F RID: 7487
		private static readonly T[] s_emptyArray = new T[0];

		// Token: 0x02000806 RID: 2054
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x0600620F RID: 25103 RVA: 0x001D32AC File Offset: 0x001D24AC
			internal Enumerator(List<T> list)
			{
				this._list = list;
				this._index = 0;
				this._version = list._version;
				this._current = default(T);
			}

			// Token: 0x06006210 RID: 25104 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Dispose()
			{
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x001D32D4 File Offset: 0x001D24D4
			public bool MoveNext()
			{
				List<T> list = this._list;
				if (this._version == list._version && this._index < list._size)
				{
					this._current = list._items[this._index];
					this._index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006212 RID: 25106 RVA: 0x001D3331 File Offset: 0x001D2531
			private bool MoveNextRare()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = this._list._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x1700102D RID: 4141
			// (get) Token: 0x06006213 RID: 25107 RVA: 0x001D336B File Offset: 0x001D256B
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700102E RID: 4142
			// (get) Token: 0x06006214 RID: 25108 RVA: 0x001D3373 File Offset: 0x001D2573
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this.Current;
				}
			}

			// Token: 0x06006215 RID: 25109 RVA: 0x001D33A2 File Offset: 0x001D25A2
			void IEnumerator.Reset()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04001D40 RID: 7488
			private readonly List<T> _list;

			// Token: 0x04001D41 RID: 7489
			private int _index;

			// Token: 0x04001D42 RID: 7490
			private readonly int _version;

			// Token: 0x04001D43 RID: 7491
			private T _current;
		}
	}
}
