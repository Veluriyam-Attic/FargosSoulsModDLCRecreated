using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Internal.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007E3 RID: 2019
	[DebuggerDisplay("Count = {Count}")]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Dictionary<TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		// Token: 0x060060CE RID: 24782 RVA: 0x001CEB07 File Offset: 0x001CDD07
		public Dictionary() : this(0, null)
		{
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x001CEB11 File Offset: 0x001CDD11
		public Dictionary(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x001CEB1B File Offset: 0x001CDD1B
		public Dictionary([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer) : this(0, comparer)
		{
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x001CEB28 File Offset: 0x001CDD28
		public Dictionary(int capacity, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			if (comparer != null && comparer != EqualityComparer<TKey>.Default)
			{
				this._comparer = comparer;
			}
			if (typeof(TKey) == typeof(string))
			{
				if (this._comparer == null)
				{
					this._comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.WrappedAroundDefaultComparer;
					return;
				}
				if (this._comparer == StringComparer.Ordinal)
				{
					this._comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.WrappedAroundStringComparerOrdinal;
					return;
				}
				if (this._comparer == StringComparer.OrdinalIgnoreCase)
				{
					this._comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.WrappedAroundStringComparerOrdinalIgnoreCase;
				}
			}
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001CEBD3 File Offset: 0x001CDDD3
		public Dictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x001CEBE0 File Offset: 0x001CDDE0
		public Dictionary(IDictionary<TKey, TValue> dictionary, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			if (dictionary.GetType() == typeof(Dictionary<TKey, TValue>))
			{
				Dictionary<TKey, TValue> dictionary2 = (Dictionary<TKey, TValue>)dictionary;
				int count = dictionary2._count;
				Dictionary<TKey, TValue>.Entry[] entries = dictionary2._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].next >= -1)
					{
						this.Add(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x001CECC0 File Offset: 0x001CDEC0
		public Dictionary([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> collection) : this(collection, null)
		{
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x001CECCC File Offset: 0x001CDECC
		public Dictionary([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> collection, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			ICollection<KeyValuePair<TKey, TValue>> collection2 = collection as ICollection<KeyValuePair<TKey, TValue>>;
			this..ctor((collection2 != null) ? collection2.Count : 0, comparer);
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x001C8E5F File Offset: 0x001C805F
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060060D7 RID: 24791 RVA: 0x001CED44 File Offset: 0x001CDF44
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				if (typeof(TKey) == typeof(string))
				{
					return (IEqualityComparer<TKey>)IInternalStringEqualityComparer.GetUnderlyingEqualityComparer((IEqualityComparer<string>)this._comparer);
				}
				return this._comparer ?? EqualityComparer<TKey>.Default;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x001CED91 File Offset: 0x001CDF91
		public int Count
		{
			get
			{
				return this._count - this._freeCount;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060060D9 RID: 24793 RVA: 0x001CEDA0 File Offset: 0x001CDFA0
		[Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			get
			{
				Dictionary<TKey, TValue>.KeyCollection result;
				if ((result = this._keys) == null)
				{
					result = (this._keys = new Dictionary<TKey, TValue>.KeyCollection(this));
				}
				return result;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x001CEDC6 File Offset: 0x001CDFC6
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x001CEDC6 File Offset: 0x001CDFC6
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060060DC RID: 24796 RVA: 0x001CEDD0 File Offset: 0x001CDFD0
		[Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			get
			{
				Dictionary<TKey, TValue>.ValueCollection result;
				if ((result = this._values) == null)
				{
					result = (this._values = new Dictionary<TKey, TValue>.ValueCollection(this));
				}
				return result;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060060DD RID: 24797 RVA: 0x001CEDF6 File Offset: 0x001CDFF6
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060060DE RID: 24798 RVA: 0x001CEDF6 File Offset: 0x001CDFF6
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		public TValue this[TKey key]
		{
			get
			{
				ref TValue ptr = ref this.FindValue(key);
				if (!Unsafe.IsNullRef<TValue>(ref ptr))
				{
					return ptr;
				}
				ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				return default(TValue);
			}
			set
			{
				bool flag = this.TryInsert(key, value, InsertionBehavior.OverwriteExisting);
			}
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x001CEE4C File Offset: 0x001CE04C
		public void Add(TKey key, TValue value)
		{
			bool flag = this.TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x001CEE63 File Offset: 0x001CE063
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x001CEE7C File Offset: 0x001CE07C
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			ref TValue ptr = ref this.FindValue(keyValuePair.Key);
			return !Unsafe.IsNullRef<TValue>(ref ptr) && EqualityComparer<TValue>.Default.Equals(ptr, keyValuePair.Value);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x001CEEBC File Offset: 0x001CE0BC
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			ref TValue ptr = ref this.FindValue(keyValuePair.Key);
			if (!Unsafe.IsNullRef<TValue>(ref ptr) && EqualityComparer<TValue>.Default.Equals(ptr, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x001CEF0C File Offset: 0x001CE10C
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

		// Token: 0x060060E6 RID: 24806 RVA: 0x001CEF5A File Offset: 0x001CE15A
		public bool ContainsKey(TKey key)
		{
			return !Unsafe.IsNullRef<TValue>(this.FindValue(key));
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x001CEF6C File Offset: 0x001CE16C
		public bool ContainsValue(TValue value)
		{
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			if (value == null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].next >= -1 && entries[i].value == null)
					{
						return true;
					}
				}
			}
			else if (typeof(TValue).IsValueType)
			{
				for (int j = 0; j < this._count; j++)
				{
					if (entries[j].next >= -1 && EqualityComparer<TValue>.Default.Equals(entries[j].value, value))
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int k = 0; k < this._count; k++)
				{
					if (entries[k].next >= -1 && @default.Equals(entries[k].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x001CF054 File Offset: 0x001CE254
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int count = this._count;
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			for (int i = 0; i < count; i++)
			{
				if (entries[i].next >= -1)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
				}
			}
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x001CF0DA File Offset: 0x001CE2DA
		[NullableContext(0)]
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x001CF0E3 File Offset: 0x001CE2E3
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x001CF0F4 File Offset: 0x001CE2F4
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this.Comparer, typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001CF188 File Offset: 0x001CE388
		private unsafe ref TValue FindValue(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ref Dictionary<TKey, TValue>.Entry ptr = ref Unsafe.NullRef<Dictionary<TKey, TValue>.Entry>();
			if (this._buckets == null)
			{
				goto IL_17F;
			}
			IEqualityComparer<TKey> comparer = this._comparer;
			if (comparer != null)
			{
				uint hashCode = (uint)comparer.GetHashCode(key);
				int i = *this.GetBucket(hashCode);
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				uint num = 0U;
				i--;
				while (i < entries.Length)
				{
					ptr = ref entries[i];
					if (ptr.hashCode == hashCode && comparer.Equals(ptr.key, key))
					{
						goto IL_176;
					}
					i = ptr.next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						goto IL_171;
					}
				}
				goto IL_17F;
			}
			uint hashCode2 = (uint)key.GetHashCode();
			int j = *this.GetBucket(hashCode2);
			Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
			uint num2 = 0U;
			if (typeof(TKey).IsValueType)
			{
				j--;
				while (j < entries2.Length)
				{
					ptr = ref entries2[j];
					if (ptr.hashCode == hashCode2 && EqualityComparer<TKey>.Default.Equals(ptr.key, key))
					{
						goto IL_176;
					}
					j = ptr.next;
					num2 += 1U;
					if (num2 > (uint)entries2.Length)
					{
						goto IL_171;
					}
				}
				goto IL_17F;
			}
			EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
			j--;
			while (j < entries2.Length)
			{
				ptr = ref entries2[j];
				if (ptr.hashCode == hashCode2 && @default.Equals(ptr.key, key))
				{
					goto IL_176;
				}
				j = ptr.next;
				num2 += 1U;
				if (num2 > (uint)entries2.Length)
				{
					goto IL_171;
				}
			}
			goto IL_17F;
			IL_171:
			ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
			IL_176:
			return ref ptr.value;
			IL_17F:
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x001CF31C File Offset: 0x001CE51C
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			int[] buckets = new int[prime];
			Dictionary<TKey, TValue>.Entry[] entries = new Dictionary<TKey, TValue>.Entry[prime];
			this._freeList = -1;
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)prime);
			this._buckets = buckets;
			this._entries = entries;
			return prime;
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x001CF360 File Offset: 0x001CE560
		private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			IEqualityComparer<TKey> comparer = this._comparer;
			uint num = (uint)((comparer == null) ? key.GetHashCode() : comparer.GetHashCode(key));
			uint num2 = 0U;
			ref int bucket = ref this.GetBucket(num);
			int i = bucket - 1;
			if (comparer == null)
			{
				if (typeof(TKey).IsValueType)
				{
					while (i < entries.Length)
					{
						if (entries[i].hashCode == num && EqualityComparer<TKey>.Default.Equals(entries[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								entries[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException<TKey>(key);
							}
							return false;
						}
						else
						{
							i = entries[i].next;
							num2 += 1U;
							if (num2 > (uint)entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
						}
					}
				}
				else
				{
					EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
					while (i < entries.Length)
					{
						if (entries[i].hashCode == num && @default.Equals(entries[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								entries[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException<TKey>(key);
							}
							return false;
						}
						else
						{
							i = entries[i].next;
							num2 += 1U;
							if (num2 > (uint)entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
						}
					}
				}
			}
			else
			{
				while (i < entries.Length)
				{
					if (entries[i].hashCode == num && comparer.Equals(entries[i].key, key))
					{
						if (behavior == InsertionBehavior.OverwriteExisting)
						{
							entries[i].value = value;
							return true;
						}
						if (behavior == InsertionBehavior.ThrowOnExisting)
						{
							ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException<TKey>(key);
						}
						return false;
					}
					else
					{
						i = entries[i].next;
						num2 += 1U;
						if (num2 > (uint)entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
					}
				}
			}
			int num3;
			if (this._freeCount > 0)
			{
				num3 = this._freeList;
				this._freeList = -3 - entries[this._freeList].next;
				this._freeCount--;
			}
			else
			{
				int count = this._count;
				if (count == entries.Length)
				{
					this.Resize();
					bucket = this.GetBucket(num);
				}
				num3 = count;
				this._count = count + 1;
				entries = this._entries;
			}
			ref Dictionary<TKey, TValue>.Entry ptr = ref entries[num3];
			ptr.hashCode = num;
			ptr.next = bucket - 1;
			ptr.key = key;
			ptr.value = value;
			bucket = num3 + 1;
			this._version++;
			if (!typeof(TKey).IsValueType && num2 > 100U && comparer is NonRandomizedStringEqualityComparer)
			{
				this.Resize(entries.Length, true);
			}
			return true;
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x001CF610 File Offset: 0x001CE810
		[NullableContext(2)]
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this._comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.Initialize(int2);
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Key == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
					}
					this.Add(array[i].Key, array[i].Value);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = @int;
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x001CF700 File Offset: 0x001CE900
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this._count), false);
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x001CF714 File Offset: 0x001CE914
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			Dictionary<TKey, TValue>.Entry[] array = new Dictionary<TKey, TValue>.Entry[newSize];
			int count = this._count;
			Array.Copy(this._entries, array, count);
			if (!typeof(TKey).IsValueType && forceNewHashCodes)
			{
				this._comparer = (IEqualityComparer<TKey>)((NonRandomizedStringEqualityComparer)this._comparer).GetRandomizedEqualityComparer();
				for (int i = 0; i < count; i++)
				{
					if (array[i].next >= -1)
					{
						array[i].hashCode = (uint)this._comparer.GetHashCode(array[i].key);
					}
				}
				if (this._comparer == EqualityComparer<TKey>.Default)
				{
					this._comparer = null;
				}
			}
			this._buckets = new int[newSize];
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)newSize);
			for (int j = 0; j < count; j++)
			{
				if (array[j].next >= -1)
				{
					ref int bucket = ref this.GetBucket(array[j].hashCode);
					array[j].next = bucket - 1;
					bucket = j + 1;
				}
			}
			this._entries = array;
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x001CF824 File Offset: 0x001CEA24
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				uint num = 0U;
				IEqualityComparer<TKey> comparer = this._comparer;
				uint num2 = (uint)((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode());
				ref int bucket = ref this.GetBucket(num2);
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				int num3 = -1;
				int i = bucket - 1;
				while (i >= 0)
				{
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries[i];
					if (ptr.hashCode == num2)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								bucket = ptr.next + 1;
							}
							else
							{
								entries[num3].next = ptr.next;
							}
							ptr.next = -3 - this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							return true;
						}
					}
					num3 = i;
					i = ptr.next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			return false;
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x001CF968 File Offset: 0x001CEB68
		public bool Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				uint num = 0U;
				IEqualityComparer<TKey> comparer = this._comparer;
				uint num2 = (uint)((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode());
				ref int bucket = ref this.GetBucket(num2);
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				int num3 = -1;
				int i = bucket - 1;
				while (i >= 0)
				{
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries[i];
					if (ptr.hashCode == num2)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								bucket = ptr.next + 1;
							}
							else
							{
								entries[num3].next = ptr.next;
							}
							value = ptr.value;
							ptr.next = -3 - this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							return true;
						}
					}
					num3 = i;
					i = ptr.next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x001CFAC4 File Offset: 0x001CECC4
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ref TValue ptr = ref this.FindValue(key);
			if (!Unsafe.IsNullRef<TValue>(ref ptr))
			{
				value = ptr;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x001CFAF7 File Offset: 0x001CECF7
		public bool TryAdd(TKey key, TValue value)
		{
			return this.TryInsert(key, value, InsertionBehavior.None);
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x001CFB02 File Offset: 0x001CED02
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x001CFB0C File Offset: 0x001CED0C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].next >= -1)
					{
						array3[index++] = new DictionaryEntry(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			try
			{
				int count = this._count;
				Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
				for (int j = 0; j < count; j++)
				{
					if (entries2[j].next >= -1)
					{
						array4[index++] = new KeyValuePair<TKey, TValue>(entries2[j].key, entries2[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x001CF0E3 File Offset: 0x001CE2E3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x001CFC6C File Offset: 0x001CEE6C
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
			this._version++;
			if (this._buckets == null)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.Resize(prime, false);
			return prime;
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x001CFCCC File Offset: 0x001CEECC
		public void TrimExcess()
		{
			this.TrimExcess(this.Count);
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x001CFCDC File Offset: 0x001CEEDC
		public void TrimExcess(int capacity)
		{
			if (capacity < this.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			int num = (entries == null) ? 0 : entries.Length;
			if (prime >= num)
			{
				return;
			}
			int count = this._count;
			this._version++;
			this.Initialize(prime);
			Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				uint hashCode = entries[i].hashCode;
				if (entries[i].next >= -1)
				{
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries2[num2];
					ptr = entries[i];
					ref int bucket = ref this.GetBucket(hashCode);
					ptr.next = bucket - 1;
					bucket = num2 + 1;
					num2++;
				}
			}
			this._count = num2;
			this._freeCount = 0;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060060FD RID: 24829 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x000AC098 File Offset: 0x000AB298
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060060FF RID: 24831 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06006101 RID: 24833 RVA: 0x001CEDC6 File Offset: 0x001CDFC6
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06006102 RID: 24834 RVA: 0x001CEDF6 File Offset: 0x001CDFF6
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000FFA RID: 4090
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					ref TValue ptr = ref this.FindValue((TKey)((object)key));
					if (!Unsafe.IsNullRef<TValue>(ref ptr))
					{
						return ptr;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey key2 = (TKey)((object)key);
					try
					{
						this[key2] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException<object>(key, typeof(TKey));
				}
			}
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x001CFE6C File Offset: 0x001CF06C
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x001CFE80 File Offset: 0x001CF080
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey key2 = (TKey)((object)key);
				try
				{
					this.Add(key2, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException<object>(key, typeof(TKey));
			}
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x001CFEF8 File Offset: 0x001CF0F8
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001CFF10 File Offset: 0x001CF110
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x001CFF1E File Offset: 0x001CF11E
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x001CFF38 File Offset: 0x001CF138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref int GetBucket(uint hashCode)
		{
			int[] buckets = this._buckets;
			return ref buckets[(int)HashHelpers.FastMod(hashCode, (uint)buckets.Length, this._fastModMultiplier)];
		}

		// Token: 0x04001D05 RID: 7429
		private int[] _buckets;

		// Token: 0x04001D06 RID: 7430
		private Dictionary<TKey, TValue>.Entry[] _entries;

		// Token: 0x04001D07 RID: 7431
		private ulong _fastModMultiplier;

		// Token: 0x04001D08 RID: 7432
		private int _count;

		// Token: 0x04001D09 RID: 7433
		private int _freeList;

		// Token: 0x04001D0A RID: 7434
		private int _freeCount;

		// Token: 0x04001D0B RID: 7435
		private int _version;

		// Token: 0x04001D0C RID: 7436
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04001D0D RID: 7437
		private Dictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04001D0E RID: 7438
		private Dictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x020007E4 RID: 2020
		private struct Entry
		{
			// Token: 0x04001D0F RID: 7439
			public uint hashCode;

			// Token: 0x04001D10 RID: 7440
			public int next;

			// Token: 0x04001D11 RID: 7441
			public TKey key;

			// Token: 0x04001D12 RID: 7442
			public TValue value;
		}

		// Token: 0x020007E5 RID: 2021
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600610B RID: 24843 RVA: 0x001CFF61 File Offset: 0x001CF161
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._dictionary = dictionary;
				this._version = dictionary._version;
				this._index = 0;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x0600610C RID: 24844 RVA: 0x001CFF90 File Offset: 0x001CF190
			public bool MoveNext()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				while (this._index < this._dictionary._count)
				{
					Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
					int index = this._index;
					this._index = index + 1;
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
					if (ptr.next >= -1)
					{
						this._current = new KeyValuePair<TKey, TValue>(ptr.key, ptr.value);
						return true;
					}
				}
				this._index = this._dictionary._count + 1;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			// Token: 0x17000FFB RID: 4091
			// (get) Token: 0x0600610D RID: 24845 RVA: 0x001D002E File Offset: 0x001CF22E
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					return this._current;
				}
			}

			// Token: 0x0600610E RID: 24846 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Dispose()
			{
			}

			// Token: 0x17000FFC RID: 4092
			// (get) Token: 0x0600610F RID: 24847 RVA: 0x001D0038 File Offset: 0x001CF238
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					if (this._getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this._current.Key, this._current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x06006110 RID: 24848 RVA: 0x001D00BB File Offset: 0x001CF2BB
			void IEnumerator.Reset()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x17000FFD RID: 4093
			// (get) Token: 0x06006111 RID: 24849 RVA: 0x001D00E8 File Offset: 0x001CF2E8
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return new DictionaryEntry(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x06006112 RID: 24850 RVA: 0x001D013C File Offset: 0x001CF33C
			[Nullable(1)]
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Key;
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (get) Token: 0x06006113 RID: 24851 RVA: 0x001D0170 File Offset: 0x001CF370
			[Nullable(2)]
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Value;
				}
			}

			// Token: 0x04001D13 RID: 7443
			private readonly Dictionary<TKey, TValue> _dictionary;

			// Token: 0x04001D14 RID: 7444
			private readonly int _version;

			// Token: 0x04001D15 RID: 7445
			private int _index;

			// Token: 0x04001D16 RID: 7446
			private KeyValuePair<TKey, TValue> _current;

			// Token: 0x04001D17 RID: 7447
			private readonly int _getEnumeratorRetType;
		}

		// Token: 0x020007E6 RID: 2022
		[NullableContext(0)]
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class KeyCollection : ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06006114 RID: 24852 RVA: 0x001D01A4 File Offset: 0x001CF3A4
			[NullableContext(1)]
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x06006115 RID: 24853 RVA: 0x001D01BC File Offset: 0x001CF3BC
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06006116 RID: 24854 RVA: 0x001D01CC File Offset: 0x001CF3CC
			[NullableContext(1)]
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].next >= -1)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			// Token: 0x17001000 RID: 4096
			// (get) Token: 0x06006117 RID: 24855 RVA: 0x001D0254 File Offset: 0x001CF454
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001001 RID: 4097
			// (get) Token: 0x06006118 RID: 24856 RVA: 0x000AC09E File Offset: 0x000AB29E
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006119 RID: 24857 RVA: 0x001D0261 File Offset: 0x001CF461
			void ICollection<!0>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x0600611A RID: 24858 RVA: 0x001D0261 File Offset: 0x001CF461
			void ICollection<!0>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x0600611B RID: 24859 RVA: 0x001D026A File Offset: 0x001CF46A
			bool ICollection<!0>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x0600611C RID: 24860 RVA: 0x001D0278 File Offset: 0x001CF478
			bool ICollection<!0>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x0600611D RID: 24861 RVA: 0x001D0282 File Offset: 0x001CF482
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x0600611E RID: 24862 RVA: 0x001D0282 File Offset: 0x001CF482
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x0600611F RID: 24863 RVA: 0x001D0294 File Offset: 0x001CF494
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].next >= -1)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x17001002 RID: 4098
			// (get) Token: 0x06006120 RID: 24864 RVA: 0x000AC09B File Offset: 0x000AB29B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x06006121 RID: 24865 RVA: 0x001D0380 File Offset: 0x001CF580
			[Nullable(1)]
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04001D18 RID: 7448
			private readonly Dictionary<TKey, TValue> _dictionary;

			// Token: 0x020007E7 RID: 2023
			public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
			{
				// Token: 0x06006122 RID: 24866 RVA: 0x001D038D File Offset: 0x001CF58D
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x06006123 RID: 24867 RVA: 0x000AB30B File Offset: 0x000AA50B
				public void Dispose()
				{
				}

				// Token: 0x06006124 RID: 24868 RVA: 0x001D03B8 File Offset: 0x001CF5B8
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.next >= -1)
						{
							this._currentKey = ptr.key;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentKey = default(TKey);
					return false;
				}

				// Token: 0x17001004 RID: 4100
				// (get) Token: 0x06006125 RID: 24869 RVA: 0x001D044B File Offset: 0x001CF64B
				[Nullable(1)]
				public TKey Current
				{
					[NullableContext(1)]
					get
					{
						return this._currentKey;
					}
				}

				// Token: 0x17001005 RID: 4101
				// (get) Token: 0x06006126 RID: 24870 RVA: 0x001D0453 File Offset: 0x001CF653
				[Nullable(2)]
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentKey;
					}
				}

				// Token: 0x06006127 RID: 24871 RVA: 0x001D0482 File Offset: 0x001CF682
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x04001D19 RID: 7449
				private readonly Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04001D1A RID: 7450
				private int _index;

				// Token: 0x04001D1B RID: 7451
				private readonly int _version;

				// Token: 0x04001D1C RID: 7452
				private TKey _currentKey;
			}
		}

		// Token: 0x020007E8 RID: 2024
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[NullableContext(0)]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006128 RID: 24872 RVA: 0x001D04AF File Offset: 0x001CF6AF
			[NullableContext(1)]
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x06006129 RID: 24873 RVA: 0x001D04C7 File Offset: 0x001CF6C7
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x0600612A RID: 24874 RVA: 0x001D04D4 File Offset: 0x001CF6D4
			[NullableContext(1)]
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if ((ulong)index > (ulong)((long)array.Length))
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].next >= -1)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			// Token: 0x17001006 RID: 4102
			// (get) Token: 0x0600612B RID: 24875 RVA: 0x001D055A File Offset: 0x001CF75A
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001007 RID: 4103
			// (get) Token: 0x0600612C RID: 24876 RVA: 0x000AC09E File Offset: 0x000AB29E
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600612D RID: 24877 RVA: 0x001D0567 File Offset: 0x001CF767
			void ICollection<!1>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x0600612E RID: 24878 RVA: 0x001D0570 File Offset: 0x001CF770
			bool ICollection<!1>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x0600612F RID: 24879 RVA: 0x001D0567 File Offset: 0x001CF767
			void ICollection<!1>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006130 RID: 24880 RVA: 0x001D057A File Offset: 0x001CF77A
			bool ICollection<!1>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06006131 RID: 24881 RVA: 0x001D0588 File Offset: 0x001CF788
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06006132 RID: 24882 RVA: 0x001D0588 File Offset: 0x001CF788
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06006133 RID: 24883 RVA: 0x001D059C File Offset: 0x001CF79C
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].next >= -1)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x17001008 RID: 4104
			// (get) Token: 0x06006134 RID: 24884 RVA: 0x000AC09B File Offset: 0x000AB29B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001009 RID: 4105
			// (get) Token: 0x06006135 RID: 24885 RVA: 0x001D0688 File Offset: 0x001CF888
			[Nullable(1)]
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04001D1D RID: 7453
			private readonly Dictionary<TKey, TValue> _dictionary;

			// Token: 0x020007E9 RID: 2025
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06006136 RID: 24886 RVA: 0x001D0695 File Offset: 0x001CF895
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x06006137 RID: 24887 RVA: 0x000AB30B File Offset: 0x000AA50B
				public void Dispose()
				{
				}

				// Token: 0x06006138 RID: 24888 RVA: 0x001D06C0 File Offset: 0x001CF8C0
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.next >= -1)
						{
							this._currentValue = ptr.value;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentValue = default(TValue);
					return false;
				}

				// Token: 0x1700100A RID: 4106
				// (get) Token: 0x06006139 RID: 24889 RVA: 0x001D0753 File Offset: 0x001CF953
				[Nullable(1)]
				public TValue Current
				{
					[NullableContext(1)]
					get
					{
						return this._currentValue;
					}
				}

				// Token: 0x1700100B RID: 4107
				// (get) Token: 0x0600613A RID: 24890 RVA: 0x001D075B File Offset: 0x001CF95B
				[Nullable(2)]
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentValue;
					}
				}

				// Token: 0x0600613B RID: 24891 RVA: 0x001D078A File Offset: 0x001CF98A
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x04001D1E RID: 7454
				private readonly Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04001D1F RID: 7455
				private int _index;

				// Token: 0x04001D20 RID: 7456
				private readonly int _version;

				// Token: 0x04001D21 RID: 7457
				private TValue _currentValue;
			}
		}
	}
}
