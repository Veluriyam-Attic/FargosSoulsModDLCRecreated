using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x0200079D RID: 1949
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06005DCF RID: 24015 RVA: 0x001C515B File Offset: 0x001C435B
		public ArrayList()
		{
			this._items = Array.Empty<object>();
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x001C5170 File Offset: 0x001C4370
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format(SR.ArgumentOutOfRange_MustBeNonNegNum, "capacity"));
			}
			if (capacity == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[capacity];
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x001C51BC File Offset: 0x001C43BC
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", SR.ArgumentNull_Collection);
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x001C520B File Offset: 0x001C440B
		// (set) Token: 0x06005DD3 RID: 24019 RVA: 0x001C5218 File Offset: 0x001C4418
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, array, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x001C5283 File Offset: 0x001C4483
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x000AC098 File Offset: 0x000AB298
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F4E RID: 3918
		[Nullable(2)]
		public virtual object this[int index]
		{
			[NullableContext(2)]
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				return this._items[index];
			}
			[NullableContext(2)]
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x001C52E8 File Offset: 0x001C44E8
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x001C5300 File Offset: 0x001C4500
		[NullableContext(2)]
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x001C5358 File Offset: 0x001C4558
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001C5368 File Offset: 0x001C4568
		[NullableContext(2)]
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001C53C3 File Offset: 0x001C45C3
		[NullableContext(2)]
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x001C53D4 File Offset: 0x001C45D4
		[NullableContext(2)]
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x001C53E5 File Offset: 0x001C45E5
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x001C5418 File Offset: 0x001C4618
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, arrayList._items, this._size);
			return arrayList;
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x001C5464 File Offset: 0x001C4664
		[NullableContext(2)]
		public virtual bool Contains(object item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x001C54C1 File Offset: 0x001C46C1
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x001C54CB File Offset: 0x001C46CB
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x001C5500 File Offset: 0x001C4700
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x001C5550 File Offset: 0x001C4750
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

		// Token: 0x06005DE8 RID: 24040 RVA: 0x001C559A File Offset: 0x001C479A
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x001C55B0 File Offset: 0x001C47B0
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x001C55C6 File Offset: 0x001C47C6
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x001C55D0 File Offset: 0x001C47D0
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x001C5623 File Offset: 0x001C4823
		[NullableContext(2)]
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x001C5638 File Offset: 0x001C4838
		[NullableContext(2)]
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x001C5668 File Offset: 0x001C4868
		[NullableContext(2)]
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x001C56BC File Offset: 0x001C48BC
		[NullableContext(2)]
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x001C5750 File Offset: 0x001C4950
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", SR.ArgumentNull_Collection);
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x001C5804 File Offset: 0x001C4A04
		[NullableContext(2)]
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x001C581B File Offset: 0x001C4A1B
		[NullableContext(2)]
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x001C5844 File Offset: 0x001C4A44
		[NullableContext(2)]
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", SR.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001C58C3 File Offset: 0x001C4AC3
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x001C58D9 File Offset: 0x001C4AD9
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x001C58F0 File Offset: 0x001C4AF0
		[NullableContext(2)]
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x001C5910 File Offset: 0x001C4B10
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x001C598C File Offset: 0x001C4B8C
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x001C5A3C File Offset: 0x001C4C3C
		public static ArrayList Repeat([Nullable(2)] object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x001C5A80 File Offset: 0x001C4C80
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x001C5A90 File Offset: 0x001C4C90
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			Array.Reverse<object>(this._items, index, count);
			this._version++;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x001C5AF8 File Offset: 0x001C4CF8
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", SR.ArgumentNull_Collection);
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x001C5B60 File Offset: 0x001C4D60
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			return new ArrayList.Range(this, index, count);
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x001C5BAE File Offset: 0x001C4DAE
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x001C5BC2 File Offset: 0x001C4DC2
		[NullableContext(2)]
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x001C5BD4 File Offset: 0x001C4DD4
		[NullableContext(2)]
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x001C5C3B File Offset: 0x001C4E3B
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x001C5C51 File Offset: 0x001C4E51
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x001C5C68 File Offset: 0x001C4E68
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			Array.Copy(this._items, array, this._size);
			return array;
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x001C5CA4 File Offset: 0x001C4EA4
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, this._size);
			Array.Copy(this._items, array, this._size);
			return array;
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x001C5CE5 File Offset: 0x001C4EE5
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04001C90 RID: 7312
		private object[] _items;

		// Token: 0x04001C91 RID: 7313
		private int _size;

		// Token: 0x04001C92 RID: 7314
		private int _version;

		// Token: 0x0200079E RID: 1950
		private class IListWrapper : ArrayList
		{
			// Token: 0x06005E06 RID: 24070 RVA: 0x001C5CF3 File Offset: 0x001C4EF3
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x17000F4F RID: 3919
			// (get) Token: 0x06005E07 RID: 24071 RVA: 0x001C5D09 File Offset: 0x001C4F09
			// (set) Token: 0x06005E08 RID: 24072 RVA: 0x001C5D16 File Offset: 0x001C4F16
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
					}
				}
			}

			// Token: 0x17000F50 RID: 3920
			// (get) Token: 0x06005E09 RID: 24073 RVA: 0x001C5D09 File Offset: 0x001C4F09
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000F51 RID: 3921
			// (get) Token: 0x06005E0A RID: 24074 RVA: 0x001C5D31 File Offset: 0x001C4F31
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000F52 RID: 3922
			// (get) Token: 0x06005E0B RID: 24075 RVA: 0x001C5D3E File Offset: 0x001C4F3E
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000F53 RID: 3923
			// (get) Token: 0x06005E0C RID: 24076 RVA: 0x001C5D4B File Offset: 0x001C4F4B
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000F54 RID: 3924
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x17000F55 RID: 3925
			// (get) Token: 0x06005E0F RID: 24079 RVA: 0x001C5D83 File Offset: 0x001C4F83
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005E10 RID: 24080 RVA: 0x001C5D90 File Offset: 0x001C4F90
			public override int Add(object obj)
			{
				int result = this._list.Add(obj);
				this._version++;
				return result;
			}

			// Token: 0x06005E11 RID: 24081 RVA: 0x001C5DB9 File Offset: 0x001C4FB9
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x06005E12 RID: 24082 RVA: 0x001C5DC8 File Offset: 0x001C4FC8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x06005E13 RID: 24083 RVA: 0x001C5E57 File Offset: 0x001C5057
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06005E14 RID: 24084 RVA: 0x001C5E8A File Offset: 0x001C508A
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06005E15 RID: 24085 RVA: 0x001C5E97 File Offset: 0x001C5097
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005E16 RID: 24086 RVA: 0x001C5EA5 File Offset: 0x001C50A5
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005E17 RID: 24087 RVA: 0x001C5EB4 File Offset: 0x001C50B4
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06005E18 RID: 24088 RVA: 0x001C5F7A File Offset: 0x001C517A
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005E19 RID: 24089 RVA: 0x001C5F88 File Offset: 0x001C5188
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06005E1A RID: 24090 RVA: 0x001C5FDB File Offset: 0x001C51DB
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005E1B RID: 24091 RVA: 0x001C5FE9 File Offset: 0x001C51E9
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06005E1C RID: 24092 RVA: 0x001C6000 File Offset: 0x001C5200
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005E1D RID: 24093 RVA: 0x001C609F File Offset: 0x001C529F
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06005E1E RID: 24094 RVA: 0x001C60BC File Offset: 0x001C52BC
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", SR.ArgumentNull_Collection);
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object value in c)
						{
							this._list.Insert(index++, value);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06005E1F RID: 24095 RVA: 0x001C6151 File Offset: 0x001C5351
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x06005E20 RID: 24096 RVA: 0x001C6172 File Offset: 0x001C5372
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005E21 RID: 24097 RVA: 0x001C6180 File Offset: 0x001C5380
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005E22 RID: 24098 RVA: 0x001C58F0 File Offset: 0x001C4AF0
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06005E23 RID: 24099 RVA: 0x001C6230 File Offset: 0x001C5430
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06005E24 RID: 24100 RVA: 0x001C624C File Offset: 0x001C544C
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06005E25 RID: 24101 RVA: 0x001C62C0 File Offset: 0x001C54C0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object value = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = value;
				}
				this._version++;
			}

			// Token: 0x06005E26 RID: 24102 RVA: 0x001C6364 File Offset: 0x001C5564
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", SR.ArgumentNull_Collection);
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				if (c.Count > 0)
				{
					foreach (object value in c)
					{
						this._list[index++] = value;
					}
					this._version++;
				}
			}

			// Token: 0x06005E27 RID: 24103 RVA: 0x001C63EC File Offset: 0x001C55EC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005E28 RID: 24104 RVA: 0x001C6440 File Offset: 0x001C5640
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06005E29 RID: 24105 RVA: 0x001C64D0 File Offset: 0x001C56D0
			public override object[] ToArray()
			{
				if (this.Count == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005E2A RID: 24106 RVA: 0x001C6508 File Offset: 0x001C5708
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.CreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005E2B RID: 24107 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void TrimToSize()
			{
			}

			// Token: 0x04001C93 RID: 7315
			private readonly IList _list;

			// Token: 0x0200079F RID: 1951
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x06005E2C RID: 24108 RVA: 0x001C654C File Offset: 0x001C574C
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x06005E2D RID: 24109 RVA: 0x000ABD27 File Offset: 0x000AAF27
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x06005E2E RID: 24110 RVA: 0x001C65A0 File Offset: 0x001C57A0
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x06005E2F RID: 24111 RVA: 0x001C6600 File Offset: 0x001C5800
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					bool flag = this._en.MoveNext();
					if (flag)
					{
						int remaining = this._remaining;
						this._remaining = remaining - 1;
						return remaining > 0;
					}
					return false;
				}

				// Token: 0x17000F56 RID: 3926
				// (get) Token: 0x06005E30 RID: 24112 RVA: 0x001C666E File Offset: 0x001C586E
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
						}
						return this._en.Current;
					}
				}

				// Token: 0x06005E31 RID: 24113 RVA: 0x001C66A4 File Offset: 0x001C58A4
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x04001C94 RID: 7316
				private IEnumerator _en;

				// Token: 0x04001C95 RID: 7317
				private int _remaining;

				// Token: 0x04001C96 RID: 7318
				private int _initialStartIndex;

				// Token: 0x04001C97 RID: 7319
				private int _initialCount;

				// Token: 0x04001C98 RID: 7320
				private bool _firstCall;
			}
		}

		// Token: 0x020007A0 RID: 1952
		private class SyncArrayList : ArrayList
		{
			// Token: 0x06005E32 RID: 24114 RVA: 0x001C66EB File Offset: 0x001C58EB
			internal SyncArrayList(ArrayList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17000F57 RID: 3927
			// (get) Token: 0x06005E33 RID: 24115 RVA: 0x001C6708 File Offset: 0x001C5908
			// (set) Token: 0x06005E34 RID: 24116 RVA: 0x001C6750 File Offset: 0x001C5950
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x17000F58 RID: 3928
			// (get) Token: 0x06005E35 RID: 24117 RVA: 0x001C6798 File Offset: 0x001C5998
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17000F59 RID: 3929
			// (get) Token: 0x06005E36 RID: 24118 RVA: 0x001C67E0 File Offset: 0x001C59E0
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000F5A RID: 3930
			// (get) Token: 0x06005E37 RID: 24119 RVA: 0x001C67ED File Offset: 0x001C59ED
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000F5B RID: 3931
			// (get) Token: 0x06005E38 RID: 24120 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F5C RID: 3932
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17000F5D RID: 3933
			// (get) Token: 0x06005E3B RID: 24123 RVA: 0x001C688C File Offset: 0x001C5A8C
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005E3C RID: 24124 RVA: 0x001C6894 File Offset: 0x001C5A94
			public override int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06005E3D RID: 24125 RVA: 0x001C68DC File Offset: 0x001C5ADC
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06005E3E RID: 24126 RVA: 0x001C6924 File Offset: 0x001C5B24
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value);
				}
				return result;
			}

			// Token: 0x06005E3F RID: 24127 RVA: 0x001C696C File Offset: 0x001C5B6C
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(value, comparer);
				}
				return result;
			}

			// Token: 0x06005E40 RID: 24128 RVA: 0x001C69B8 File Offset: 0x001C5BB8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.BinarySearch(index, count, value, comparer);
				}
				return result;
			}

			// Token: 0x06005E41 RID: 24129 RVA: 0x001C6A04 File Offset: 0x001C5C04
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005E42 RID: 24130 RVA: 0x001C6A4C File Offset: 0x001C5C4C
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return result;
			}

			// Token: 0x06005E43 RID: 24131 RVA: 0x001C6AA0 File Offset: 0x001C5CA0
			public override bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06005E44 RID: 24132 RVA: 0x001C6AE8 File Offset: 0x001C5CE8
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06005E45 RID: 24133 RVA: 0x001C6B30 File Offset: 0x001C5D30
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005E46 RID: 24134 RVA: 0x001C6B78 File Offset: 0x001C5D78
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06005E47 RID: 24135 RVA: 0x001C6BC4 File Offset: 0x001C5DC4
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005E48 RID: 24136 RVA: 0x001C6C0C File Offset: 0x001C5E0C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06005E49 RID: 24137 RVA: 0x001C6C58 File Offset: 0x001C5E58
			public override int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06005E4A RID: 24138 RVA: 0x001C6CA0 File Offset: 0x001C5EA0
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06005E4B RID: 24139 RVA: 0x001C6CEC File Offset: 0x001C5EEC
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06005E4C RID: 24140 RVA: 0x001C6D38 File Offset: 0x001C5F38
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005E4D RID: 24141 RVA: 0x001C6D80 File Offset: 0x001C5F80
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06005E4E RID: 24142 RVA: 0x001C6DC8 File Offset: 0x001C5FC8
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value);
				}
				return result;
			}

			// Token: 0x06005E4F RID: 24143 RVA: 0x001C6E10 File Offset: 0x001C6010
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06005E50 RID: 24144 RVA: 0x001C6E5C File Offset: 0x001C605C
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.LastIndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06005E51 RID: 24145 RVA: 0x001C6EA8 File Offset: 0x001C60A8
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005E52 RID: 24146 RVA: 0x001C6EF0 File Offset: 0x001C60F0
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06005E53 RID: 24147 RVA: 0x001C6F38 File Offset: 0x001C6138
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06005E54 RID: 24148 RVA: 0x001C6F80 File Offset: 0x001C6180
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06005E55 RID: 24149 RVA: 0x001C6FC8 File Offset: 0x001C61C8
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06005E56 RID: 24150 RVA: 0x001C7010 File Offset: 0x001C6210
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06005E57 RID: 24151 RVA: 0x001C705C File Offset: 0x001C625C
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06005E58 RID: 24152 RVA: 0x001C70A4 File Offset: 0x001C62A4
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06005E59 RID: 24153 RVA: 0x001C70EC File Offset: 0x001C62EC
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06005E5A RID: 24154 RVA: 0x001C7134 File Offset: 0x001C6334
			public override object[] ToArray()
			{
				object root = this._root;
				object[] result;
				lock (root)
				{
					result = this._list.ToArray();
				}
				return result;
			}

			// Token: 0x06005E5B RID: 24155 RVA: 0x001C717C File Offset: 0x001C637C
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array result;
				lock (root)
				{
					result = this._list.ToArray(type);
				}
				return result;
			}

			// Token: 0x06005E5C RID: 24156 RVA: 0x001C71C4 File Offset: 0x001C63C4
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04001C99 RID: 7321
			private readonly ArrayList _list;

			// Token: 0x04001C9A RID: 7322
			private readonly object _root;
		}

		// Token: 0x020007A1 RID: 1953
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005E5D RID: 24157 RVA: 0x001C720C File Offset: 0x001C640C
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17000F5E RID: 3934
			// (get) Token: 0x06005E5E RID: 24158 RVA: 0x001C7228 File Offset: 0x001C6428
			public virtual int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17000F5F RID: 3935
			// (get) Token: 0x06005E5F RID: 24159 RVA: 0x001C7270 File Offset: 0x001C6470
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000F60 RID: 3936
			// (get) Token: 0x06005E60 RID: 24160 RVA: 0x001C727D File Offset: 0x001C647D
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x06005E61 RID: 24161 RVA: 0x000AC09E File Offset: 0x000AB29E
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F62 RID: 3938
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object result;
					lock (root)
					{
						result = this._list[index];
					}
					return result;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17000F63 RID: 3939
			// (get) Token: 0x06005E64 RID: 24164 RVA: 0x001C731C File Offset: 0x001C651C
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005E65 RID: 24165 RVA: 0x001C7324 File Offset: 0x001C6524
			public virtual int Add(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.Add(value);
				}
				return result;
			}

			// Token: 0x06005E66 RID: 24166 RVA: 0x001C736C File Offset: 0x001C656C
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005E67 RID: 24167 RVA: 0x001C73B4 File Offset: 0x001C65B4
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._list.Contains(item);
				}
				return result;
			}

			// Token: 0x06005E68 RID: 24168 RVA: 0x001C73FC File Offset: 0x001C65FC
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005E69 RID: 24169 RVA: 0x001C7444 File Offset: 0x001C6644
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005E6A RID: 24170 RVA: 0x001C748C File Offset: 0x001C668C
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int result;
				lock (root)
				{
					result = this._list.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06005E6B RID: 24171 RVA: 0x001C74D4 File Offset: 0x001C66D4
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005E6C RID: 24172 RVA: 0x001C751C File Offset: 0x001C671C
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005E6D RID: 24173 RVA: 0x001C7564 File Offset: 0x001C6764
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04001C9B RID: 7323
			private readonly IList _list;

			// Token: 0x04001C9C RID: 7324
			private readonly object _root;
		}

		// Token: 0x020007A2 RID: 1954
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005E6E RID: 24174 RVA: 0x001C75AC File Offset: 0x001C67AC
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17000F64 RID: 3940
			// (get) Token: 0x06005E6F RID: 24175 RVA: 0x001C75BB File Offset: 0x001C67BB
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000F65 RID: 3941
			// (get) Token: 0x06005E70 RID: 24176 RVA: 0x001C75C8 File Offset: 0x001C67C8
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000F66 RID: 3942
			// (get) Token: 0x06005E71 RID: 24177 RVA: 0x000AC09E File Offset: 0x000AB29E
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F67 RID: 3943
			// (get) Token: 0x06005E72 RID: 24178 RVA: 0x001C75D5 File Offset: 0x001C67D5
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000F68 RID: 3944
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x06005E75 RID: 24181 RVA: 0x001C75FF File Offset: 0x001C67FF
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005E76 RID: 24182 RVA: 0x001C760C File Offset: 0x001C680C
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E77 RID: 24183 RVA: 0x001C760C File Offset: 0x001C680C
			public virtual void Clear()
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E78 RID: 24184 RVA: 0x001C7618 File Offset: 0x001C6818
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005E79 RID: 24185 RVA: 0x001C7626 File Offset: 0x001C6826
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005E7A RID: 24186 RVA: 0x001C7635 File Offset: 0x001C6835
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005E7B RID: 24187 RVA: 0x001C7642 File Offset: 0x001C6842
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005E7C RID: 24188 RVA: 0x001C760C File Offset: 0x001C680C
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E7D RID: 24189 RVA: 0x001C760C File Offset: 0x001C680C
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E7E RID: 24190 RVA: 0x001C760C File Offset: 0x001C680C
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x04001C9D RID: 7325
			private readonly IList _list;
		}

		// Token: 0x020007A3 RID: 1955
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x06005E7F RID: 24191 RVA: 0x001C7650 File Offset: 0x001C6850
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17000F6A RID: 3946
			// (get) Token: 0x06005E80 RID: 24192 RVA: 0x001C7670 File Offset: 0x001C6870
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000F6B RID: 3947
			// (get) Token: 0x06005E81 RID: 24193 RVA: 0x001C767D File Offset: 0x001C687D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000F6C RID: 3948
			// (get) Token: 0x06005E82 RID: 24194 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F6D RID: 3949
			// (get) Token: 0x06005E83 RID: 24195 RVA: 0x001C768A File Offset: 0x001C688A
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000F6E RID: 3950
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x17000F6F RID: 3951
			// (get) Token: 0x06005E86 RID: 24198 RVA: 0x001C76C5 File Offset: 0x001C68C5
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005E87 RID: 24199 RVA: 0x001C760C File Offset: 0x001C680C
			public override int Add(object obj)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E88 RID: 24200 RVA: 0x001C760C File Offset: 0x001C680C
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E89 RID: 24201 RVA: 0x001C76D2 File Offset: 0x001C68D2
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17000F70 RID: 3952
			// (get) Token: 0x06005E8A RID: 24202 RVA: 0x001C76E4 File Offset: 0x001C68E4
			// (set) Token: 0x06005E8B RID: 24203 RVA: 0x001C760C File Offset: 0x001C680C
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
				}
			}

			// Token: 0x06005E8C RID: 24204 RVA: 0x001C760C File Offset: 0x001C680C
			public override void Clear()
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E8D RID: 24205 RVA: 0x001C76F4 File Offset: 0x001C68F4
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005E8E RID: 24206 RVA: 0x001C7724 File Offset: 0x001C6924
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005E8F RID: 24207 RVA: 0x001C7732 File Offset: 0x001C6932
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005E90 RID: 24208 RVA: 0x001C7741 File Offset: 0x001C6941
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005E91 RID: 24209 RVA: 0x001C7753 File Offset: 0x001C6953
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005E92 RID: 24210 RVA: 0x001C7760 File Offset: 0x001C6960
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005E93 RID: 24211 RVA: 0x001C776F File Offset: 0x001C696F
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005E94 RID: 24212 RVA: 0x001C777D File Offset: 0x001C697D
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005E95 RID: 24213 RVA: 0x001C778C File Offset: 0x001C698C
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005E96 RID: 24214 RVA: 0x001C760C File Offset: 0x001C680C
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E97 RID: 24215 RVA: 0x001C760C File Offset: 0x001C680C
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E98 RID: 24216 RVA: 0x001C779C File Offset: 0x001C699C
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005E99 RID: 24217 RVA: 0x001C77AA File Offset: 0x001C69AA
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005E9A RID: 24218 RVA: 0x001C77B9 File Offset: 0x001C69B9
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005E9B RID: 24219 RVA: 0x001C760C File Offset: 0x001C680C
			public override void Remove(object value)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E9C RID: 24220 RVA: 0x001C760C File Offset: 0x001C680C
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E9D RID: 24221 RVA: 0x001C760C File Offset: 0x001C680C
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x06005E9E RID: 24222 RVA: 0x001C77C9 File Offset: 0x001C69C9
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x06005E9F RID: 24223 RVA: 0x001C77EC File Offset: 0x001C69EC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005EA0 RID: 24224 RVA: 0x001C783A File Offset: 0x001C6A3A
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x06005EA1 RID: 24225 RVA: 0x001C785A File Offset: 0x001C6A5A
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x06005EA2 RID: 24226 RVA: 0x001C787B File Offset: 0x001C6A7B
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005EA3 RID: 24227 RVA: 0x001C7888 File Offset: 0x001C6A88
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005EA4 RID: 24228 RVA: 0x001C760C File Offset: 0x001C680C
			public override void TrimToSize()
			{
				throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
			}

			// Token: 0x04001C9E RID: 7326
			private ArrayList _list;
		}

		// Token: 0x020007A4 RID: 1956
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005EA5 RID: 24229 RVA: 0x001C7896 File Offset: 0x001C6A96
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17000F71 RID: 3953
			// (get) Token: 0x06005EA6 RID: 24230 RVA: 0x001C78A5 File Offset: 0x001C6AA5
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000F72 RID: 3954
			// (get) Token: 0x06005EA7 RID: 24231 RVA: 0x000AC09E File Offset: 0x000AB29E
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F73 RID: 3955
			// (get) Token: 0x06005EA8 RID: 24232 RVA: 0x000AC09E File Offset: 0x000AB29E
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F74 RID: 3956
			// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x001C78B2 File Offset: 0x001C6AB2
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000F75 RID: 3957
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
				}
			}

			// Token: 0x17000F76 RID: 3958
			// (get) Token: 0x06005EAC RID: 24236 RVA: 0x001C78D9 File Offset: 0x001C6AD9
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005EAD RID: 24237 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EAE RID: 24238 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public virtual void Clear()
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EAF RID: 24239 RVA: 0x001C78E6 File Offset: 0x001C6AE6
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005EB0 RID: 24240 RVA: 0x001C78F4 File Offset: 0x001C6AF4
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005EB1 RID: 24241 RVA: 0x001C7903 File Offset: 0x001C6B03
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005EB2 RID: 24242 RVA: 0x001C7910 File Offset: 0x001C6B10
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005EB3 RID: 24243 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EB4 RID: 24244 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EB5 RID: 24245 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x04001C9F RID: 7327
			private readonly IList _list;
		}

		// Token: 0x020007A5 RID: 1957
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x06005EB6 RID: 24246 RVA: 0x001C791E File Offset: 0x001C6B1E
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x17000F77 RID: 3959
			// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x001C792D File Offset: 0x001C6B2D
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x06005EB8 RID: 24248 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F79 RID: 3961
			// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F7A RID: 3962
			// (get) Token: 0x06005EBA RID: 24250 RVA: 0x001C793A File Offset: 0x001C6B3A
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000F7B RID: 3963
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
				}
			}

			// Token: 0x17000F7C RID: 3964
			// (get) Token: 0x06005EBD RID: 24253 RVA: 0x001C7955 File Offset: 0x001C6B55
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005EBE RID: 24254 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override int Add(object obj)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EBF RID: 24255 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EC0 RID: 24256 RVA: 0x001C7962 File Offset: 0x001C6B62
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17000F7D RID: 3965
			// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x001C7974 File Offset: 0x001C6B74
			// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
				}
			}

			// Token: 0x06005EC3 RID: 24259 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void Clear()
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005EC4 RID: 24260 RVA: 0x001C7984 File Offset: 0x001C6B84
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005EC5 RID: 24261 RVA: 0x001C79B4 File Offset: 0x001C6BB4
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005EC6 RID: 24262 RVA: 0x001C79C2 File Offset: 0x001C6BC2
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005EC7 RID: 24263 RVA: 0x001C79D1 File Offset: 0x001C6BD1
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005EC8 RID: 24264 RVA: 0x001C79E3 File Offset: 0x001C6BE3
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005EC9 RID: 24265 RVA: 0x001C79F0 File Offset: 0x001C6BF0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005ECA RID: 24266 RVA: 0x001C79FF File Offset: 0x001C6BFF
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005ECB RID: 24267 RVA: 0x001C7A0D File Offset: 0x001C6C0D
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005ECC RID: 24268 RVA: 0x001C7A1C File Offset: 0x001C6C1C
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005ECD RID: 24269 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ECE RID: 24270 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ECF RID: 24271 RVA: 0x001C7A2C File Offset: 0x001C6C2C
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005ED0 RID: 24272 RVA: 0x001C7A3A File Offset: 0x001C6C3A
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005ED1 RID: 24273 RVA: 0x001C7A49 File Offset: 0x001C6C49
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005ED2 RID: 24274 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void Remove(object value)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED3 RID: 24275 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED4 RID: 24276 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED5 RID: 24277 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED6 RID: 24278 RVA: 0x001C77EC File Offset: 0x001C69EC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005ED7 RID: 24279 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED8 RID: 24280 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005ED9 RID: 24281 RVA: 0x001C7A59 File Offset: 0x001C6C59
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005EDA RID: 24282 RVA: 0x001C7A66 File Offset: 0x001C6C66
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005EDB RID: 24283 RVA: 0x001C78CD File Offset: 0x001C6ACD
			public override void TrimToSize()
			{
				throw new NotSupportedException(SR.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x04001CA0 RID: 7328
			private ArrayList _list;
		}

		// Token: 0x020007A6 RID: 1958
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005EDC RID: 24284 RVA: 0x001C7A74 File Offset: 0x001C6C74
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this._list = list;
				this._startIndex = index;
				this._index = index - 1;
				this._endIndex = this._index + count;
				this._version = list._version;
				this._currentElement = null;
			}

			// Token: 0x06005EDD RID: 24285 RVA: 0x000AC0FA File Offset: 0x000AB2FA
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005EDE RID: 24286 RVA: 0x001C7AB4 File Offset: 0x001C6CB4
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				if (this._index < this._endIndex)
				{
					ArrayList list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._currentElement = list[index];
					return true;
				}
				this._index = this._endIndex + 1;
				return false;
			}

			// Token: 0x17000F7E RID: 3966
			// (get) Token: 0x06005EDF RID: 24287 RVA: 0x001C7B20 File Offset: 0x001C6D20
			public object Current
			{
				get
				{
					if (this._index < this._startIndex)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					if (this._index > this._endIndex)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
					}
					return this._currentElement;
				}
			}

			// Token: 0x06005EE0 RID: 24288 RVA: 0x001C7B5A File Offset: 0x001C6D5A
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				this._index = this._startIndex - 1;
			}

			// Token: 0x04001CA1 RID: 7329
			private readonly ArrayList _list;

			// Token: 0x04001CA2 RID: 7330
			private int _index;

			// Token: 0x04001CA3 RID: 7331
			private readonly int _endIndex;

			// Token: 0x04001CA4 RID: 7332
			private readonly int _version;

			// Token: 0x04001CA5 RID: 7333
			private object _currentElement;

			// Token: 0x04001CA6 RID: 7334
			private readonly int _startIndex;
		}

		// Token: 0x020007A7 RID: 1959
		private class Range : ArrayList
		{
			// Token: 0x06005EE1 RID: 24289 RVA: 0x001C7B88 File Offset: 0x001C6D88
			internal Range(ArrayList list, int index, int count)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x06005EE2 RID: 24290 RVA: 0x001C7BBD File Offset: 0x001C6DBD
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_UnderlyingArrayListChanged);
				}
			}

			// Token: 0x06005EE3 RID: 24291 RVA: 0x001C7BDD File Offset: 0x001C6DDD
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06005EE4 RID: 24292 RVA: 0x001C7BFC File Offset: 0x001C6DFC
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06005EE5 RID: 24293 RVA: 0x001C7C40 File Offset: 0x001C6E40
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06005EE6 RID: 24294 RVA: 0x001C7C9C File Offset: 0x001C6E9C
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x06005EE7 RID: 24295 RVA: 0x001C7D15 File Offset: 0x001C6F15
			// (set) Token: 0x06005EE8 RID: 24296 RVA: 0x001C5D16 File Offset: 0x001C4F16
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
					}
				}
			}

			// Token: 0x06005EE9 RID: 24297 RVA: 0x001C7D22 File Offset: 0x001C6F22
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06005EEA RID: 24298 RVA: 0x001C7D58 File Offset: 0x001C6F58
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06005EEB RID: 24299 RVA: 0x001C7D9C File Offset: 0x001C6F9C
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06005EEC RID: 24300 RVA: 0x001C7E20 File Offset: 0x001C7020
			public override void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06005EED RID: 24301 RVA: 0x001C7EA4 File Offset: 0x001C70A4
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x06005EEE RID: 24302 RVA: 0x001C7F47 File Offset: 0x001C7147
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x06005EEF RID: 24303 RVA: 0x001C7F55 File Offset: 0x001C7155
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x17000F82 RID: 3970
			// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x001C7F62 File Offset: 0x001C7162
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x001C7F6F File Offset: 0x001C716F
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x06005EF2 RID: 24306 RVA: 0x001C7F7C File Offset: 0x001C717C
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x06005EF3 RID: 24307 RVA: 0x001C7F8C File Offset: 0x001C718C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06005EF4 RID: 24308 RVA: 0x001C7FEC File Offset: 0x001C71EC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x06005EF5 RID: 24309 RVA: 0x001C8040 File Offset: 0x001C7240
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06005EF6 RID: 24310 RVA: 0x001C8050 File Offset: 0x001C7250
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005EF7 RID: 24311 RVA: 0x001C808C File Offset: 0x001C728C
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005EF8 RID: 24312 RVA: 0x001C80F8 File Offset: 0x001C72F8
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005EF9 RID: 24313 RVA: 0x001C816C File Offset: 0x001C736C
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06005EFA RID: 24314 RVA: 0x001C81C4 File Offset: 0x001C73C4
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005EFB RID: 24315 RVA: 0x001C8238 File Offset: 0x001C7438
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005EFC RID: 24316 RVA: 0x001C6172 File Offset: 0x001C5372
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005EFD RID: 24317 RVA: 0x001C827C File Offset: 0x001C747C
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005EFE RID: 24318 RVA: 0x001C82EC File Offset: 0x001C74EC
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x06005EFF RID: 24319 RVA: 0x001C8344 File Offset: 0x001C7544
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x06005F00 RID: 24320 RVA: 0x001C83BC File Offset: 0x001C75BC
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x06005F01 RID: 24321 RVA: 0x001C8424 File Offset: 0x001C7624
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005F02 RID: 24322 RVA: 0x001C8478 File Offset: 0x001C7678
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(SR.Argument_InvalidOffLen);
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x17000F85 RID: 3973
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005F05 RID: 24325 RVA: 0x001C8558 File Offset: 0x001C7758
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005F06 RID: 24326 RVA: 0x001C85A4 File Offset: 0x001C77A4
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.CreateInstance(type, this._baseSize);
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005F07 RID: 24327 RVA: 0x001C85F2 File Offset: 0x001C77F2
			public override void TrimToSize()
			{
				throw new NotSupportedException(SR.NotSupported_RangeCollection);
			}

			// Token: 0x04001CA7 RID: 7335
			private ArrayList _baseList;

			// Token: 0x04001CA8 RID: 7336
			private readonly int _baseIndex;

			// Token: 0x04001CA9 RID: 7337
			private int _baseSize;

			// Token: 0x04001CAA RID: 7338
			private int _baseVersion;
		}

		// Token: 0x020007A8 RID: 1960
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06005F08 RID: 24328 RVA: 0x001C8600 File Offset: 0x001C7800
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this._list = list;
				this._index = -1;
				this._version = list._version;
				this._isArrayList = (list.GetType() == typeof(ArrayList));
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
			}

			// Token: 0x06005F09 RID: 24329 RVA: 0x000AC0FA File Offset: 0x000AB2FA
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005F0A RID: 24330 RVA: 0x001C8654 File Offset: 0x001C7854
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				if (this._isArrayList)
				{
					if (this._index < this._list._size - 1)
					{
						object[] items = this._list._items;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = items[num];
						return true;
					}
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					this._index = this._list._size;
					return false;
				}
				else
				{
					if (this._index < this._list.Count - 1)
					{
						ArrayList list = this._list;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = list[num];
						return true;
					}
					this._index = this._list.Count;
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					return false;
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x06005F0B RID: 24331 RVA: 0x001C8738 File Offset: 0x001C7938
			public object Current
			{
				get
				{
					object currentElement = this._currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.s_dummyObject != currentElement)
					{
						return currentElement;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
				}
			}

			// Token: 0x06005F0C RID: 24332 RVA: 0x001C8774 File Offset: 0x001C7974
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
				this._index = -1;
			}

			// Token: 0x04001CAB RID: 7339
			private readonly ArrayList _list;

			// Token: 0x04001CAC RID: 7340
			private int _index;

			// Token: 0x04001CAD RID: 7341
			private readonly int _version;

			// Token: 0x04001CAE RID: 7342
			private object _currentElement;

			// Token: 0x04001CAF RID: 7343
			private readonly bool _isArrayList;

			// Token: 0x04001CB0 RID: 7344
			private static readonly object s_dummyObject = new object();
		}

		// Token: 0x020007A9 RID: 1961
		internal class ArrayListDebugView
		{
			// Token: 0x06005F0E RID: 24334 RVA: 0x001C87B2 File Offset: 0x001C79B2
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this._arrayList = arrayList;
			}

			// Token: 0x17000F87 RID: 3975
			// (get) Token: 0x06005F0F RID: 24335 RVA: 0x001C87CF File Offset: 0x001C79CF
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._arrayList.ToArray();
				}
			}

			// Token: 0x04001CB1 RID: 7345
			private readonly ArrayList _arrayList;
		}
	}
}
