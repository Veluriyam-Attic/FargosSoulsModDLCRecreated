using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Internal.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007EA RID: 2026
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class HashSet<[Nullable(2)] T> : ICollection<!0>, IEnumerable<!0>, IEnumerable, ISet<T>, IReadOnlyCollection<T>, IReadOnlySet<T>, ISerializable, IDeserializationCallback
	{
		// Token: 0x0600613C RID: 24892 RVA: 0x001D07B7 File Offset: 0x001CF9B7
		public HashSet() : this(null)
		{
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x001D07C0 File Offset: 0x001CF9C0
		public HashSet([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> comparer)
		{
			if (comparer != null && comparer != EqualityComparer<T>.Default)
			{
				this._comparer = comparer;
			}
			if (typeof(T) == typeof(string))
			{
				if (this._comparer == null)
				{
					this._comparer = (IEqualityComparer<T>)NonRandomizedStringEqualityComparer.WrappedAroundDefaultComparer;
					return;
				}
				if (this._comparer == StringComparer.Ordinal)
				{
					this._comparer = (IEqualityComparer<T>)NonRandomizedStringEqualityComparer.WrappedAroundStringComparerOrdinal;
					return;
				}
				if (this._comparer == StringComparer.OrdinalIgnoreCase)
				{
					this._comparer = (IEqualityComparer<T>)NonRandomizedStringEqualityComparer.WrappedAroundStringComparerOrdinalIgnoreCase;
				}
			}
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x001D0854 File Offset: 0x001CFA54
		public HashSet(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x001D085E File Offset: 0x001CFA5E
		public HashSet(IEnumerable<T> collection) : this(collection, null)
		{
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x001D0868 File Offset: 0x001CFA68
		public HashSet(IEnumerable<T> collection, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> comparer) : this(comparer)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			HashSet<T> hashSet = collection as HashSet<T>;
			if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
			{
				this.ConstructFrom(hashSet);
				return;
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.Initialize(count);
				}
			}
			this.UnionWith(collection);
			if (this._count > 0 && this._entries.Length / this._count > 3)
			{
				this.TrimExcess();
			}
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x001D08E6 File Offset: 0x001CFAE6
		public HashSet(int capacity, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> comparer) : this(comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x001D0906 File Offset: 0x001CFB06
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x001D0918 File Offset: 0x001CFB18
		private void ConstructFrom(HashSet<T> source)
		{
			if (source.Count == 0)
			{
				return;
			}
			int num = source._buckets.Length;
			int num2 = HashHelpers.ExpandPrime(source.Count + 1);
			if (num2 >= num)
			{
				this._buckets = (int[])source._buckets.Clone();
				this._entries = (HashSet<T>.Entry[])source._entries.Clone();
				this._freeList = source._freeList;
				this._freeCount = source._freeCount;
				this._count = source._count;
				this._fastModMultiplier = source._fastModMultiplier;
				return;
			}
			this.Initialize(source.Count);
			HashSet<T>.Entry[] entries = source._entries;
			for (int i = 0; i < source._count; i++)
			{
				ref HashSet<T>.Entry ptr = ref entries[i];
				if (ptr.Next >= -1)
				{
					int num3;
					this.AddIfNotPresent(ptr.Value, out num3);
				}
			}
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x001D09F0 File Offset: 0x001CFBF0
		void ICollection<!0>.Add(T item)
		{
			int num;
			this.AddIfNotPresent(item, out num);
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x001D0A08 File Offset: 0x001CFC08
		public void Clear()
		{
			int count = this._count;
			if (count > 0)
			{
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._count = 0;
				this._freeList = -1;
				this._freeCount = 0;
				Array.Clear(this._entries, 0, count);
			}
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x001D0A56 File Offset: 0x001CFC56
		public bool Contains(T item)
		{
			return this.FindItemIndex(item) >= 0;
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x001D0A68 File Offset: 0x001CFC68
		private unsafe int FindItemIndex(T item)
		{
			int[] buckets = this._buckets;
			if (buckets != null)
			{
				HashSet<T>.Entry[] entries = this._entries;
				uint num = 0U;
				IEqualityComparer<T> comparer = this._comparer;
				if (comparer == null)
				{
					int num2 = (item != null) ? item.GetHashCode() : 0;
					if (typeof(T).IsValueType)
					{
						int i = *this.GetBucketRef(num2) - 1;
						while (i >= 0)
						{
							ref HashSet<T>.Entry ptr = ref entries[i];
							if (ptr.HashCode == num2 && EqualityComparer<T>.Default.Equals(ptr.Value, item))
							{
								return i;
							}
							i = ptr.Next;
							num += 1U;
							if (num > (uint)entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
						}
					}
					else
					{
						EqualityComparer<T> @default = EqualityComparer<T>.Default;
						int j = *this.GetBucketRef(num2) - 1;
						while (j >= 0)
						{
							ref HashSet<T>.Entry ptr2 = ref entries[j];
							if (ptr2.HashCode == num2 && @default.Equals(ptr2.Value, item))
							{
								return j;
							}
							j = ptr2.Next;
							num += 1U;
							if (num > (uint)entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
						}
					}
				}
				else
				{
					int num3 = (item != null) ? comparer.GetHashCode(item) : 0;
					int k = *this.GetBucketRef(num3) - 1;
					while (k >= 0)
					{
						ref HashSet<T>.Entry ptr3 = ref entries[k];
						if (ptr3.HashCode == num3 && comparer.Equals(ptr3.Value, item))
						{
							return k;
						}
						k = ptr3.Next;
						num += 1U;
						if (num > (uint)entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x001D0BE8 File Offset: 0x001CFDE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref int GetBucketRef(int hashCode)
		{
			int[] buckets = this._buckets;
			return ref buckets[(int)HashHelpers.FastMod((uint)hashCode, (uint)buckets.Length, this._fastModMultiplier)];
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x001D0C14 File Offset: 0x001CFE14
		public bool Remove(T item)
		{
			if (this._buckets != null)
			{
				HashSet<T>.Entry[] entries = this._entries;
				uint num = 0U;
				int num2 = -1;
				int num3;
				if (item == null)
				{
					num3 = 0;
				}
				else
				{
					IEqualityComparer<T> comparer = this._comparer;
					num3 = ((comparer != null) ? comparer.GetHashCode(item) : item.GetHashCode());
				}
				int num4 = num3;
				ref int bucketRef = ref this.GetBucketRef(num4);
				int i = bucketRef - 1;
				while (i >= 0)
				{
					ref HashSet<T>.Entry ptr = ref entries[i];
					if (ptr.HashCode == num4)
					{
						IEqualityComparer<T> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.Value, item) : EqualityComparer<T>.Default.Equals(ptr.Value, item))
						{
							if (num2 < 0)
							{
								bucketRef = ptr.Next + 1;
							}
							else
							{
								entries[num2].Next = ptr.Next;
							}
							ptr.Next = -3 - this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
							{
								ptr.Value = default(T);
							}
							this._freeList = i;
							this._freeCount++;
							return true;
						}
					}
					num2 = i;
					i = ptr.Next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			return false;
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600614A RID: 24906 RVA: 0x001D0D40 File Offset: 0x001CFF40
		public int Count
		{
			get
			{
				return this._count - this._freeCount;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x001D0D4F File Offset: 0x001CFF4F
		[NullableContext(0)]
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001D0D57 File Offset: 0x001CFF57
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x001D0D57 File Offset: 0x001CFF57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x001D0D64 File Offset: 0x001CFF64
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this.Comparer, typeof(IEqualityComparer<T>));
			info.AddValue("Capacity", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array);
				info.AddValue("Elements", array, typeof(T[]));
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x001D0DF8 File Offset: 0x001CFFF8
		[NullableContext(2)]
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Capacity");
			this._comparer = (IEqualityComparer<T>)this._siInfo.GetValue("Comparer", typeof(IEqualityComparer<T>));
			this._freeList = -1;
			this._freeCount = 0;
			if (@int != 0)
			{
				this._buckets = new int[@int];
				this._entries = new HashSet<T>.Entry[@int];
				this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)@int);
				T[] array = (T[])this._siInfo.GetValue("Elements", typeof(T[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					this.AddIfNotPresent(array[i], out num);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = this._siInfo.GetInt32("Version");
			this._siInfo = null;
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x001D0EE8 File Offset: 0x001D00E8
		public bool Add(T item)
		{
			int num;
			return this.AddIfNotPresent(item, out num);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x001D0F00 File Offset: 0x001D0100
		public bool TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue)
		{
			if (this._buckets != null)
			{
				int num = this.FindItemIndex(equalValue);
				if (num >= 0)
				{
					actualValue = this._entries[num].Value;
					return true;
				}
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x001D0F44 File Offset: 0x001D0144
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			foreach (T value in other)
			{
				int num;
				this.AddIfNotPresent(value, out num);
			}
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x001D0F9C File Offset: 0x001D019C
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0 || other == this)
			{
				return;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					this.Clear();
					return;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
				{
					this.IntersectWithHashSetWithSameComparer(hashSet);
					return;
				}
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x001D0FFC File Offset: 0x001D01FC
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			foreach (T item in other)
			{
				this.Remove(item);
			}
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x001D1064 File Offset: 0x001D0264
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
			{
				this.SymmetricExceptWithUniqueHashSet(hashSet);
				return;
			}
			this.SymmetricExceptWithEnumerable(other);
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x001D10B8 File Offset: 0x001D02B8
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0 || other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
			{
				return this.Count <= hashSet.Count && this.IsSubsetOfHashSetWithSameComparer(hashSet);
			}
			ValueTuple<int, int> valueTuple = this.CheckUniqueAndUnfoundElements(other, false);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			return item == this.Count && item2 >= 0;
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x001D1130 File Offset: 0x001D0330
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return false;
				}
				if (this.Count == 0)
				{
					return collection.Count > 0;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
				{
					return this.Count < hashSet.Count && this.IsSubsetOfHashSetWithSameComparer(hashSet);
				}
			}
			ValueTuple<int, int> valueTuple = this.CheckUniqueAndUnfoundElements(other, false);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			return item == this.Count && item2 > 0;
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x001D11C4 File Offset: 0x001D03C4
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (other == this)
			{
				return true;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet) && hashSet.Count > this.Count)
				{
					return false;
				}
			}
			return this.ContainsAllElements(other);
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x001D1220 File Offset: 0x001D0420
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0 || other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
				{
					return hashSet.Count < this.Count && this.ContainsAllElements(hashSet);
				}
			}
			ValueTuple<int, int> valueTuple = this.CheckUniqueAndUnfoundElements(other, true);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			return item < this.Count && item2 == 0;
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001D12A8 File Offset: 0x001D04A8
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x001D1314 File Offset: 0x001D0514
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.EqualityComparersAreEqual(this, hashSet))
			{
				return this.Count == hashSet.Count && this.ContainsAllElements(hashSet);
			}
			if (this.Count == 0)
			{
				ICollection<T> collection = other as ICollection<!0>;
				if (collection != null && collection.Count > 0)
				{
					return false;
				}
			}
			ValueTuple<int, int> valueTuple = this.CheckUniqueAndUnfoundElements(other, true);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			return item == this.Count && item2 == 0;
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x001D139D File Offset: 0x001D059D
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x001D13AD File Offset: 0x001D05AD
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex, this.Count);
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x001D13C0 File Offset: 0x001D05C0
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (arrayIndex > array.Length || count > array.Length - arrayIndex)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			HashSet<T>.Entry[] entries = this._entries;
			int num = 0;
			while (num < this._count && count != 0)
			{
				ref HashSet<T>.Entry ptr = ref entries[num];
				if (ptr.Next >= -1)
				{
					array[arrayIndex++] = ptr.Value;
					count--;
				}
				num++;
			}
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x001D1464 File Offset: 0x001D0664
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			HashSet<T>.Entry[] entries = this._entries;
			int num = 0;
			for (int i = 0; i < this._count; i++)
			{
				ref HashSet<T>.Entry ptr = ref entries[i];
				if (ptr.Next >= -1)
				{
					T value = ptr.Value;
					if (match(value) && this.Remove(value))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x001D14C8 File Offset: 0x001D06C8
		public IEqualityComparer<T> Comparer
		{
			get
			{
				if (typeof(T) == typeof(string))
				{
					return (IEqualityComparer<T>)IInternalStringEqualityComparer.GetUnderlyingEqualityComparer((IEqualityComparer<string>)this._comparer);
				}
				return this._comparer ?? EqualityComparer<T>.Default;
			}
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x001D1518 File Offset: 0x001D0718
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int num = (this._entries == null) ? 0 : this._entries.Length;
			if (num >= capacity)
			{
				return num;
			}
			if (this._buckets == null)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.Resize(prime, false);
			return prime;
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x001D156A File Offset: 0x001D076A
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this._count), false);
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x001D1580 File Offset: 0x001D0780
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			HashSet<T>.Entry[] array = new HashSet<T>.Entry[newSize];
			int count = this._count;
			Array.Copy(this._entries, array, count);
			if (!typeof(T).IsValueType && forceNewHashCodes)
			{
				this._comparer = (IEqualityComparer<T>)((NonRandomizedStringEqualityComparer)this._comparer).GetRandomizedEqualityComparer();
				for (int i = 0; i < count; i++)
				{
					ref HashSet<T>.Entry ptr = ref array[i];
					if (ptr.Next >= -1)
					{
						ptr.HashCode = ((ptr.Value != null) ? this._comparer.GetHashCode(ptr.Value) : 0);
					}
				}
				if (this._comparer == EqualityComparer<T>.Default)
				{
					this._comparer = null;
				}
			}
			this._buckets = new int[newSize];
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)newSize);
			for (int j = 0; j < count; j++)
			{
				ref HashSet<T>.Entry ptr2 = ref array[j];
				if (ptr2.Next >= -1)
				{
					ref int bucketRef = ref this.GetBucketRef(ptr2.HashCode);
					ptr2.Next = bucketRef - 1;
					bucketRef = j + 1;
				}
			}
			this._entries = array;
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x001D1698 File Offset: 0x001D0898
		public void TrimExcess()
		{
			int count = this.Count;
			int prime = HashHelpers.GetPrime(count);
			HashSet<T>.Entry[] entries = this._entries;
			int num = (entries == null) ? 0 : entries.Length;
			if (prime >= num)
			{
				return;
			}
			int count2 = this._count;
			this._version++;
			this.Initialize(prime);
			HashSet<T>.Entry[] entries2 = this._entries;
			int num2 = 0;
			for (int i = 0; i < count2; i++)
			{
				int hashCode = entries[i].HashCode;
				if (entries[i].Next >= -1)
				{
					ref HashSet<T>.Entry ptr = ref entries2[num2];
					ptr = entries[i];
					ref int bucketRef = ref this.GetBucketRef(hashCode);
					ptr.Next = bucketRef - 1;
					bucketRef = num2 + 1;
					num2++;
				}
			}
			this._count = count;
			this._freeCount = 0;
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x001D176D File Offset: 0x001D096D
		public static IEqualityComparer<HashSet<T>> CreateSetComparer()
		{
			return new HashSetEqualityComparer<T>();
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x001D1774 File Offset: 0x001D0974
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			int[] buckets = new int[prime];
			HashSet<T>.Entry[] entries = new HashSet<T>.Entry[prime];
			this._freeList = -1;
			this._buckets = buckets;
			this._entries = entries;
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)prime);
			return prime;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x001D17B8 File Offset: 0x001D09B8
		private bool AddIfNotPresent(T value, out int location)
		{
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			HashSet<T>.Entry[] entries = this._entries;
			IEqualityComparer<T> comparer = this._comparer;
			uint num = 0U;
			ref int ptr = ref Unsafe.NullRef<int>();
			int num2;
			if (comparer == null)
			{
				num2 = ((value != null) ? value.GetHashCode() : 0);
				ptr = this.GetBucketRef(num2);
				int i = ptr - 1;
				if (typeof(T).IsValueType)
				{
					while (i >= 0)
					{
						ref HashSet<T>.Entry ptr2 = ref entries[i];
						if (ptr2.HashCode == num2 && EqualityComparer<T>.Default.Equals(ptr2.Value, value))
						{
							location = i;
							return false;
						}
						i = ptr2.Next;
						num += 1U;
						if (num > (uint)entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
					}
				}
				else
				{
					EqualityComparer<T> @default = EqualityComparer<T>.Default;
					while (i >= 0)
					{
						ref HashSet<T>.Entry ptr3 = ref entries[i];
						if (ptr3.HashCode == num2 && @default.Equals(ptr3.Value, value))
						{
							location = i;
							return false;
						}
						i = ptr3.Next;
						num += 1U;
						if (num > (uint)entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
					}
				}
			}
			else
			{
				num2 = ((value != null) ? comparer.GetHashCode(value) : 0);
				ptr = this.GetBucketRef(num2);
				int j = ptr - 1;
				while (j >= 0)
				{
					ref HashSet<T>.Entry ptr4 = ref entries[j];
					if (ptr4.HashCode == num2 && comparer.Equals(ptr4.Value, value))
					{
						location = j;
						return false;
					}
					j = ptr4.Next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			int num3;
			if (this._freeCount > 0)
			{
				num3 = this._freeList;
				this._freeCount--;
				this._freeList = -3 - entries[this._freeList].Next;
			}
			else
			{
				int count = this._count;
				if (count == entries.Length)
				{
					this.Resize();
					ptr = this.GetBucketRef(num2);
				}
				num3 = count;
				this._count = count + 1;
				entries = this._entries;
			}
			ref HashSet<T>.Entry ptr5 = ref entries[num3];
			ptr5.HashCode = num2;
			ptr5.Next = ptr - 1;
			ptr5.Value = value;
			ptr = num3 + 1;
			this._version++;
			location = num3;
			if (!typeof(T).IsValueType && num > 100U && comparer is NonRandomizedStringEqualityComparer)
			{
				this.Resize(entries.Length, true);
				location = this.FindItemIndex(value);
			}
			return true;
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001D1A1C File Offset: 0x001D0C1C
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x001D1A70 File Offset: 0x001D0C70
		internal bool IsSubsetOfHashSetWithSameComparer(HashSet<T> other)
		{
			foreach (T item in this)
			{
				if (!other.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x001D1AC8 File Offset: 0x001D0CC8
		private void IntersectWithHashSetWithSameComparer(HashSet<T> other)
		{
			HashSet<T>.Entry[] entries = this._entries;
			for (int i = 0; i < this._count; i++)
			{
				ref HashSet<T>.Entry ptr = ref entries[i];
				if (ptr.Next >= -1)
				{
					T value = ptr.Value;
					if (!other.Contains(value))
					{
						this.Remove(value);
					}
				}
			}
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x001D1B18 File Offset: 0x001D0D18
		private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
		{
			int count = this._count;
			int num = BitHelper.ToIntArrayLength(count);
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)400], 100);
			Span<int> span2 = span;
			BitHelper bitHelper = (num <= 100) ? new BitHelper(span2.Slice(0, num), true) : new BitHelper(new int[num], false);
			foreach (T item in other)
			{
				int num2 = this.FindItemIndex(item);
				if (num2 >= 0)
				{
					bitHelper.MarkBit(num2);
				}
			}
			for (int i = 0; i < count; i++)
			{
				ref HashSet<T>.Entry ptr = ref this._entries[i];
				if (ptr.Next >= -1 && !bitHelper.IsMarked(i))
				{
					this.Remove(ptr.Value);
				}
			}
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x001D1C04 File Offset: 0x001D0E04
		private void SymmetricExceptWithUniqueHashSet(HashSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Remove(t))
				{
					int num;
					this.AddIfNotPresent(t, out num);
				}
			}
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x001D1C60 File Offset: 0x001D0E60
		private unsafe void SymmetricExceptWithEnumerable(IEnumerable<T> other)
		{
			int count = this._count;
			int num = BitHelper.ToIntArrayLength(count);
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)200], 50);
			Span<int> span2 = span;
			BitHelper bitHelper = (num <= 50) ? new BitHelper(span2.Slice(0, num), true) : new BitHelper(new int[num], false);
			span = new Span<int>(stackalloc byte[(UIntPtr)200], 50);
			Span<int> span3 = span;
			BitHelper bitHelper2 = (num <= 50) ? new BitHelper(span3.Slice(0, num), true) : new BitHelper(new int[num], false);
			foreach (T value in other)
			{
				int num2;
				if (this.AddIfNotPresent(value, out num2))
				{
					bitHelper2.MarkBit(num2);
				}
				else if (num2 < count && !bitHelper2.IsMarked(num2))
				{
					bitHelper.MarkBit(num2);
				}
			}
			for (int i = 0; i < count; i++)
			{
				if (bitHelper.IsMarked(i))
				{
					this.Remove(this._entries[i].Value);
				}
			}
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x001D1D94 File Offset: 0x001D0F94
		[return: TupleElementNames(new string[]
		{
			"UniqueCount",
			"UnfoundCount"
		})]
		private unsafe ValueTuple<int, int> CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			if (this._count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				return new ValueTuple<int, int>(0, num);
			}
			int count = this._count;
			int num2 = BitHelper.ToIntArrayLength(count);
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)400], 100);
			Span<int> span2 = span;
			BitHelper bitHelper = (num2 <= 100) ? new BitHelper(span2.Slice(0, num2), true) : new BitHelper(new int[num2], false);
			int num3 = 0;
			int num4 = 0;
			foreach (T item in other)
			{
				int num5 = this.FindItemIndex(item);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			return new ValueTuple<int, int>(num4, num3);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x001D1EBC File Offset: 0x001D10BC
		internal static bool EqualityComparersAreEqual(HashSet<T> set1, HashSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x04001D22 RID: 7458
		private int[] _buckets;

		// Token: 0x04001D23 RID: 7459
		private HashSet<T>.Entry[] _entries;

		// Token: 0x04001D24 RID: 7460
		private ulong _fastModMultiplier;

		// Token: 0x04001D25 RID: 7461
		private int _count;

		// Token: 0x04001D26 RID: 7462
		private int _freeList;

		// Token: 0x04001D27 RID: 7463
		private int _freeCount;

		// Token: 0x04001D28 RID: 7464
		private int _version;

		// Token: 0x04001D29 RID: 7465
		private IEqualityComparer<T> _comparer;

		// Token: 0x04001D2A RID: 7466
		private SerializationInfo _siInfo;

		// Token: 0x020007EB RID: 2027
		private struct Entry
		{
			// Token: 0x04001D2B RID: 7467
			public int HashCode;

			// Token: 0x04001D2C RID: 7468
			public int Next;

			// Token: 0x04001D2D RID: 7469
			public T Value;
		}

		// Token: 0x020007EC RID: 2028
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06006171 RID: 24945 RVA: 0x001D1ECF File Offset: 0x001D10CF
			internal Enumerator(HashSet<T> hashSet)
			{
				this._hashSet = hashSet;
				this._version = hashSet._version;
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x06006172 RID: 24946 RVA: 0x001D1EF8 File Offset: 0x001D10F8
			public bool MoveNext()
			{
				if (this._version != this._hashSet._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				while (this._index < this._hashSet._count)
				{
					HashSet<T>.Entry[] entries = this._hashSet._entries;
					int index = this._index;
					this._index = index + 1;
					ref HashSet<T>.Entry ptr = ref entries[index];
					if (ptr.Next >= -1)
					{
						this._current = ptr.Value;
						return true;
					}
				}
				this._index = this._hashSet._count + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x1700100F RID: 4111
			// (get) Token: 0x06006173 RID: 24947 RVA: 0x001D1F8B File Offset: 0x001D118B
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06006174 RID: 24948 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Dispose()
			{
			}

			// Token: 0x17001010 RID: 4112
			// (get) Token: 0x06006175 RID: 24949 RVA: 0x001D1F93 File Offset: 0x001D1193
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._hashSet._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current;
				}
			}

			// Token: 0x06006176 RID: 24950 RVA: 0x001D1FC2 File Offset: 0x001D11C2
			void IEnumerator.Reset()
			{
				if (this._version != this._hashSet._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04001D2E RID: 7470
			private readonly HashSet<T> _hashSet;

			// Token: 0x04001D2F RID: 7471
			private readonly int _version;

			// Token: 0x04001D30 RID: 7472
			private int _index;

			// Token: 0x04001D31 RID: 7473
			private T _current;
		}
	}
}
