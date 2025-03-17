using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections
{
	// Token: 0x020007AE RID: 1966
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005F28 RID: 24360 RVA: 0x001C8B00 File Offset: 0x001C7D00
		// (set) Token: 0x06005F29 RID: 24361 RVA: 0x001C8B34 File Offset: 0x001C7D34
		[Nullable(2)]
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			[NullableContext(2)]
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).HashCodeProvider;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(SR.Arg_CannotMixComparisonInfrastructure);
			}
			[NullableContext(2)]
			set
			{
				CompatibleComparer compatibleComparer = this._keycomparer as CompatibleComparer;
				if (compatibleComparer != null)
				{
					this._keycomparer = new CompatibleComparer(value, compatibleComparer.Comparer);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(value, null);
					return;
				}
				throw new ArgumentException(SR.Arg_CannotMixComparisonInfrastructure);
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005F2A RID: 24362 RVA: 0x001C8B83 File Offset: 0x001C7D83
		// (set) Token: 0x06005F2B RID: 24363 RVA: 0x001C8BB8 File Offset: 0x001C7DB8
		[Nullable(2)]
		[Obsolete("Please use KeyComparer properties.")]
		protected IComparer comparer
		{
			[NullableContext(2)]
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).Comparer;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(SR.Arg_CannotMixComparisonInfrastructure);
			}
			[NullableContext(2)]
			set
			{
				CompatibleComparer compatibleComparer = this._keycomparer as CompatibleComparer;
				if (compatibleComparer != null)
				{
					this._keycomparer = new CompatibleComparer(compatibleComparer.HashCodeProvider, value);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(null, value);
					return;
				}
				throw new ArgumentException(SR.Arg_CannotMixComparisonInfrastructure);
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x001C8C07 File Offset: 0x001C7E07
		[Nullable(2)]
		protected IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._keycomparer;
			}
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x000ABD27 File Offset: 0x000AAF27
		internal Hashtable(bool trash)
		{
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001C8C0F File Offset: 0x001C7E0F
		public Hashtable() : this(0, 1f)
		{
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x001C8C1D File Offset: 0x001C7E1D
		public Hashtable(int capacity) : this(capacity, 1f)
		{
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x001C8C2C File Offset: 0x001C7E2C
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", SR.Format(SR.ArgumentOutOfRange_HashtableLoadFactor, 0.1, 1.0));
			}
			this._loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this._loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException(SR.Arg_HTCapacityOverflow, "capacity");
			}
			int num2 = (num > 3.0) ? HashHelpers.GetPrime((int)num) : 3;
			this._buckets = new Hashtable.bucket[num2];
			this._loadsize = (int)(this._loadFactor * (float)num2);
			this._isWriterInProgress = false;
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001C8D04 File Offset: 0x001C7F04
		[NullableContext(2)]
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer) : this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x001C8D15 File Offset: 0x001C7F15
		[NullableContext(2)]
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer) : this(0, 1f, hcp, comparer)
		{
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x001C8D25 File Offset: 0x001C7F25
		[NullableContext(2)]
		public Hashtable(IEqualityComparer equalityComparer) : this(0, 1f, equalityComparer)
		{
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x001C8D34 File Offset: 0x001C7F34
		[NullableContext(2)]
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer) : this(capacity, 1f, hcp, comparer)
		{
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x001C8D44 File Offset: 0x001C7F44
		[NullableContext(2)]
		public Hashtable(int capacity, IEqualityComparer equalityComparer) : this(capacity, 1f, equalityComparer)
		{
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x001C8D53 File Offset: 0x001C7F53
		public Hashtable(IDictionary d) : this(d, 1f)
		{
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x001C8D61 File Offset: 0x001C7F61
		public Hashtable(IDictionary d, float loadFactor) : this(d, loadFactor, null)
		{
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x001C8D6C File Offset: 0x001C7F6C
		[NullableContext(2)]
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable([Nullable(1)] IDictionary d, IHashCodeProvider hcp, IComparer comparer) : this(d, 1f, hcp, comparer)
		{
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x001C8D7C File Offset: 0x001C7F7C
		public Hashtable(IDictionary d, [Nullable(2)] IEqualityComparer equalityComparer) : this(d, 1f, equalityComparer)
		{
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x001C8D8B File Offset: 0x001C7F8B
		[NullableContext(2)]
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this(capacity, loadFactor)
		{
			if (hcp != null || comparer != null)
			{
				this._keycomparer = new CompatibleComparer(hcp, comparer);
			}
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x001C8DAC File Offset: 0x001C7FAC
		[NullableContext(2)]
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable([Nullable(1)] IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", SR.ArgumentNull_Dictionary);
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001C8E08 File Offset: 0x001C8008
		public Hashtable(IDictionary d, float loadFactor, [Nullable(2)] IEqualityComparer equalityComparer) : this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", SR.ArgumentNull_Dictionary);
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001C8E5F File Offset: 0x001C805F
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x001C8E74 File Offset: 0x001C8074
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x001C8EA1 File Offset: 0x001C80A1
		public virtual void Add(object key, [Nullable(2)] object value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x001C8EAC File Offset: 0x001C80AC
		public virtual void Clear()
		{
			if (this._count == 0 && this._occupancy == 0)
			{
				return;
			}
			this._isWriterInProgress = true;
			for (int i = 0; i < this._buckets.Length; i++)
			{
				this._buckets[i].hash_coll = 0;
				this._buckets[i].key = null;
				this._buckets[i].val = null;
			}
			this._count = 0;
			this._occupancy = 0;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x001C8F3C File Offset: 0x001C813C
		public virtual object Clone()
		{
			Hashtable.bucket[] buckets = this._buckets;
			Hashtable hashtable = new Hashtable(this._count, this._keycomparer);
			hashtable._version = this._version;
			hashtable._loadFactor = this._loadFactor;
			hashtable._count = 0;
			int i = buckets.Length;
			while (i > 0)
			{
				i--;
				object key = buckets[i].key;
				if (key != null && key != buckets)
				{
					hashtable[key] = buckets[i].val;
				}
			}
			return hashtable;
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x001C8FBB File Offset: 0x001C81BB
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x001C8FC4 File Offset: 0x001C81C4
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			Hashtable.bucket[] buckets = this._buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = buckets[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x001C9064 File Offset: 0x001C8264
		[NullableContext(2)]
		public virtual bool ContainsValue(object value)
		{
			if (value == null)
			{
				int num = this._buckets.Length;
				while (--num >= 0)
				{
					if (this._buckets[num].key != null && this._buckets[num].key != this._buckets && this._buckets[num].val == null)
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this._buckets.Length;
				while (--num2 >= 0)
				{
					object val = this._buckets[num2].val;
					if (val != null && val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x001C9100 File Offset: 0x001C8300
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x001C9148 File Offset: 0x001C8348
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, buckets[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x001C91AC File Offset: 0x001C83AC
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.ArgumentNull_Array);
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(SR.Arg_ArrayPlusOffTooSmall);
			}
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x001C921C File Offset: 0x001C841C
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this._count];
			int num = 0;
			Hashtable.bucket[] buckets = this._buckets;
			int num2 = buckets.Length;
			while (--num2 >= 0)
			{
				object key = buckets[num2].key;
				if (key != null && key != this._buckets)
				{
					array[num++] = new KeyValuePairs(key, buckets[num2].val);
				}
			}
			return array;
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001C9284 File Offset: 0x001C8484
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(buckets[num].val, arrayIndex++);
				}
			}
		}

		// Token: 0x17000F90 RID: 3984
		[Nullable(2)]
		public virtual object this[object key]
		{
			[return: Nullable(2)]
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				Hashtable.bucket[] buckets = this._buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, buckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)buckets.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int version = this._version;
						bucket = buckets[num5];
						if (!this._isWriterInProgress && version == this._version)
						{
							break;
						}
						spinWait.SpinOnce();
					}
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_5;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
					{
						goto IL_CA;
					}
				}
				return null;
				Block_5:
				return bucket.val;
				IL_CA:
				return null;
			}
			[param: Nullable(2)]
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x001C93BC File Offset: 0x001C85BC
		private void expand()
		{
			int newsize = HashHelpers.ExpandPrime(this._buckets.Length);
			this.rehash(newsize);
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x001C93DE File Offset: 0x001C85DE
		private void rehash()
		{
			this.rehash(this._buckets.Length);
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x001C93EE File Offset: 0x001C85EE
		private void UpdateVersion()
		{
			this._version++;
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001C9404 File Offset: 0x001C8604
		private void rehash(int newsize)
		{
			this._occupancy = 0;
			Hashtable.bucket[] array = new Hashtable.bucket[newsize];
			for (int i = 0; i < this._buckets.Length; i++)
			{
				Hashtable.bucket bucket = this._buckets[i];
				if (bucket.key != null && bucket.key != this._buckets)
				{
					int hashcode = bucket.hash_coll & int.MaxValue;
					this.putEntry(array, bucket.key, bucket.val, hashcode);
				}
			}
			this._isWriterInProgress = true;
			this._buckets = array;
			this._loadsize = (int)(this._loadFactor * (float)newsize);
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x001C94A5 File Offset: 0x001C86A5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x001C94A5 File Offset: 0x001C86A5
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x001C94AE File Offset: 0x001C86AE
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x001C94CB File Offset: 0x001C86CB
		protected virtual bool KeyEquals([Nullable(2)] object item, object key)
		{
			if (this._buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005F57 RID: 24407 RVA: 0x001C9500 File Offset: 0x001C8700
		public virtual ICollection Keys
		{
			get
			{
				ICollection result;
				if ((result = this._keys) == null)
				{
					result = (this._keys = new Hashtable.KeyCollection(this));
				}
				return result;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x001C9528 File Offset: 0x001C8728
		public virtual ICollection Values
		{
			get
			{
				ICollection result;
				if ((result = this._values) == null)
				{
					result = (this._values = new Hashtable.ValueCollection(this));
				}
				return result;
			}
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x001C9550 File Offset: 0x001C8750
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			if (this._count >= this._loadsize)
			{
				this.expand();
			}
			else if (this._occupancy > this._loadsize && this._count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this._buckets[num6].key == this._buckets && this._buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this._buckets[num6].key == null || (this._buckets[num6].key == this._buckets && ((long)this._buckets[num6].hash_coll & (long)((ulong)-2147483648)) == 0L))
				{
					break;
				}
				if ((long)(this._buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this._buckets[num6].key, key))
				{
					goto Block_12;
				}
				if (num5 == -1 && this._buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] buckets = this._buckets;
					int num7 = num6;
					buckets[num7].hash_coll = (buckets[num7].hash_coll | int.MinValue);
					this._occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (++num4 >= this._buckets.Length)
				{
					goto Block_16;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this._buckets[num6].key = key;
			Hashtable.bucket[] buckets2 = this._buckets;
			int num8 = num6;
			buckets2[num8].hash_coll = (buckets2[num8].hash_coll | (int)num);
			this._count++;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_12:
			if (add)
			{
				throw new ArgumentException(SR.Format(SR.Argument_AddingDuplicate__, this._buckets[num6].key, key));
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_16:
			if (num5 != -1)
			{
				this._isWriterInProgress = true;
				this._buckets[num5].val = nvalue;
				this._buckets[num5].key = key;
				Hashtable.bucket[] buckets3 = this._buckets;
				int num9 = num5;
				buckets3[num9].hash_coll = (buckets3[num9].hash_coll | (int)num);
				this._count++;
				this.UpdateVersion();
				this._isWriterInProgress = false;
				return;
			}
			throw new InvalidOperationException(SR.InvalidOperation_HashInsertFailed);
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x001C9820 File Offset: 0x001C8A20
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this._buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = (newBuckets[num3].hash_coll | int.MinValue);
					this._occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = (newBuckets[num4].hash_coll | hashcode);
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x001C98D4 File Offset: 0x001C8AD4
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this._buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this._buckets.Length)
				{
					return;
				}
			}
			this._isWriterInProgress = true;
			Hashtable.bucket[] buckets = this._buckets;
			int num6 = num5;
			buckets[num6].hash_coll = (buckets[num6].hash_coll & int.MinValue);
			if (this._buckets[num5].hash_coll != 0)
			{
				this._buckets[num5].key = this._buckets;
			}
			else
			{
				this._buckets[num5].key = null;
			}
			this._buckets[num5].val = null;
			this._count--;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x000AC098 File Offset: 0x000AB298
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005F5D RID: 24413 RVA: 0x001C9A12 File Offset: 0x001C8C12
		public virtual int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x001C9A1A File Offset: 0x001C8C1A
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001C9A30 File Offset: 0x001C8C30
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int version = this._version;
				info.AddValue("LoadFactor", this._loadFactor);
				info.AddValue("Version", this._version);
				IEqualityComparer keycomparer = this._keycomparer;
				if (keycomparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = keycomparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", keycomparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this._buckets.Length);
				object[] array = new object[this._count];
				object[] array2 = new object[this._count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this._version != version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
			}
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001C9BCC File Offset: 0x001C8DCC
		[NullableContext(2)]
		public virtual void OnDeserialization(object sender)
		{
			if (this._buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException(SR.Serialization_InvalidOnDeser);
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this._loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this._loadsize = (int)(this._loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(hashCodeProvider, comparer);
			}
			this._buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException(SR.Serialization_MissingKeys);
			}
			if (array2 == null)
			{
				throw new SerializationException(SR.Serialization_MissingValues);
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException(SR.Serialization_KeyValueDifferentSizes);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException(SR.Serialization_NullKey);
				}
				this.Insert(array[i], array2[i], true);
			}
			this._version = serializationInfo.GetInt32("Version");
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x04001CBB RID: 7355
		private Hashtable.bucket[] _buckets;

		// Token: 0x04001CBC RID: 7356
		private int _count;

		// Token: 0x04001CBD RID: 7357
		private int _occupancy;

		// Token: 0x04001CBE RID: 7358
		private int _loadsize;

		// Token: 0x04001CBF RID: 7359
		private float _loadFactor;

		// Token: 0x04001CC0 RID: 7360
		private volatile int _version;

		// Token: 0x04001CC1 RID: 7361
		private volatile bool _isWriterInProgress;

		// Token: 0x04001CC2 RID: 7362
		private ICollection _keys;

		// Token: 0x04001CC3 RID: 7363
		private ICollection _values;

		// Token: 0x04001CC4 RID: 7364
		private IEqualityComparer _keycomparer;

		// Token: 0x020007AF RID: 1967
		private struct bucket
		{
			// Token: 0x04001CC5 RID: 7365
			public object key;

			// Token: 0x04001CC6 RID: 7366
			public object val;

			// Token: 0x04001CC7 RID: 7367
			public int hash_coll;
		}

		// Token: 0x020007B0 RID: 1968
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x06005F61 RID: 24417 RVA: 0x001C9EB2 File Offset: 0x001C90B2
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005F62 RID: 24418 RVA: 0x001C9EC4 File Offset: 0x001C90C4
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException(SR.Arg_ArrayPlusOffTooSmall);
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x06005F63 RID: 24419 RVA: 0x001C9F39 File Offset: 0x001C9139
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x17000F98 RID: 3992
			// (get) Token: 0x06005F64 RID: 24420 RVA: 0x001C9F47 File Offset: 0x001C9147
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17000F99 RID: 3993
			// (get) Token: 0x06005F65 RID: 24421 RVA: 0x001C9F54 File Offset: 0x001C9154
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17000F9A RID: 3994
			// (get) Token: 0x06005F66 RID: 24422 RVA: 0x001C9F61 File Offset: 0x001C9161
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04001CC8 RID: 7368
			private readonly Hashtable _hashtable;
		}

		// Token: 0x020007B1 RID: 1969
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005F67 RID: 24423 RVA: 0x001C9F6E File Offset: 0x001C916E
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005F68 RID: 24424 RVA: 0x001C9F80 File Offset: 0x001C9180
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException(SR.Arg_ArrayPlusOffTooSmall);
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x06005F69 RID: 24425 RVA: 0x001C9FF5 File Offset: 0x001C91F5
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x17000F9B RID: 3995
			// (get) Token: 0x06005F6A RID: 24426 RVA: 0x001CA003 File Offset: 0x001C9203
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (get) Token: 0x06005F6B RID: 24427 RVA: 0x001CA010 File Offset: 0x001C9210
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17000F9D RID: 3997
			// (get) Token: 0x06005F6C RID: 24428 RVA: 0x001CA01D File Offset: 0x001C921D
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04001CC9 RID: 7369
			private readonly Hashtable _hashtable;
		}

		// Token: 0x020007B2 RID: 1970
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x06005F6D RID: 24429 RVA: 0x001CA02A File Offset: 0x001C922A
			internal SyncHashtable(Hashtable table) : base(false)
			{
				this._table = table;
			}

			// Token: 0x06005F6E RID: 24430 RVA: 0x000B3617 File Offset: 0x000B2817
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x17000F9E RID: 3998
			// (get) Token: 0x06005F6F RID: 24431 RVA: 0x001CA03A File Offset: 0x001C923A
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x17000F9F RID: 3999
			// (get) Token: 0x06005F70 RID: 24432 RVA: 0x001CA047 File Offset: 0x001C9247
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x17000FA0 RID: 4000
			// (get) Token: 0x06005F71 RID: 24433 RVA: 0x001CA054 File Offset: 0x001C9254
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x17000FA1 RID: 4001
			// (get) Token: 0x06005F72 RID: 24434 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FA2 RID: 4002
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x17000FA3 RID: 4003
			// (get) Token: 0x06005F75 RID: 24437 RVA: 0x001CA0BC File Offset: 0x001C92BC
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x06005F76 RID: 24438 RVA: 0x001CA0CC File Offset: 0x001C92CC
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x06005F77 RID: 24439 RVA: 0x001CA118 File Offset: 0x001C9318
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x06005F78 RID: 24440 RVA: 0x001CA164 File Offset: 0x001C9364
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x06005F79 RID: 24441 RVA: 0x001CA172 File Offset: 0x001C9372
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x06005F7A RID: 24442 RVA: 0x001CA194 File Offset: 0x001C9394
			public override bool ContainsValue(object key)
			{
				object syncRoot = this._table.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this._table.ContainsValue(key);
				}
				return result;
			}

			// Token: 0x06005F7B RID: 24443 RVA: 0x001CA1E4 File Offset: 0x001C93E4
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005F7C RID: 24444 RVA: 0x001CA230 File Offset: 0x001C9430
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return result;
			}

			// Token: 0x06005F7D RID: 24445 RVA: 0x001CA288 File Offset: 0x001C9488
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x06005F7E RID: 24446 RVA: 0x001CA288 File Offset: 0x001C9488
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x17000FA4 RID: 4004
			// (get) Token: 0x06005F7F RID: 24447 RVA: 0x001CA298 File Offset: 0x001C9498
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x17000FA5 RID: 4005
			// (get) Token: 0x06005F80 RID: 24448 RVA: 0x001CA2E4 File Offset: 0x001C94E4
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x06005F81 RID: 24449 RVA: 0x001CA330 File Offset: 0x001C9530
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x06005F82 RID: 24450 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x06005F83 RID: 24451 RVA: 0x001CA37C File Offset: 0x001C957C
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._table.ToKeyValuePairsArray();
			}

			// Token: 0x04001CCA RID: 7370
			protected Hashtable _table;
		}

		// Token: 0x020007B3 RID: 1971
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06005F84 RID: 24452 RVA: 0x001CA389 File Offset: 0x001C9589
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this._hashtable = hashtable;
				this._bucket = hashtable._buckets.Length;
				this._version = hashtable._version;
				this._current = false;
				this._getObjectRetType = getObjRetType;
			}

			// Token: 0x06005F85 RID: 24453 RVA: 0x000AC0FA File Offset: 0x000AB2FA
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000FA6 RID: 4006
			// (get) Token: 0x06005F86 RID: 24454 RVA: 0x001CA3C2 File Offset: 0x001C95C2
			public virtual object Key
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					return this._currentKey;
				}
			}

			// Token: 0x06005F87 RID: 24455 RVA: 0x001CA3E0 File Offset: 0x001C95E0
			public virtual bool MoveNext()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				while (this._bucket > 0)
				{
					this._bucket--;
					object key = this._hashtable._buckets[this._bucket].key;
					if (key != null && key != this._hashtable._buckets)
					{
						this._currentKey = key;
						this._currentValue = this._hashtable._buckets[this._bucket].val;
						this._current = true;
						return true;
					}
				}
				this._current = false;
				return false;
			}

			// Token: 0x17000FA7 RID: 4007
			// (get) Token: 0x06005F88 RID: 24456 RVA: 0x001CA48A File Offset: 0x001C968A
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x06005F89 RID: 24457 RVA: 0x001CA4B0 File Offset: 0x001C96B0
			public virtual object Current
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					if (this._getObjectRetType == 1)
					{
						return this._currentKey;
					}
					if (this._getObjectRetType == 2)
					{
						return this._currentValue;
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x06005F8A RID: 24458 RVA: 0x001CA506 File Offset: 0x001C9706
			public virtual object Value
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					return this._currentValue;
				}
			}

			// Token: 0x06005F8B RID: 24459 RVA: 0x001CA524 File Offset: 0x001C9724
			public virtual void Reset()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				this._current = false;
				this._bucket = this._hashtable._buckets.Length;
				this._currentKey = null;
				this._currentValue = null;
			}

			// Token: 0x04001CCB RID: 7371
			private readonly Hashtable _hashtable;

			// Token: 0x04001CCC RID: 7372
			private int _bucket;

			// Token: 0x04001CCD RID: 7373
			private readonly int _version;

			// Token: 0x04001CCE RID: 7374
			private bool _current;

			// Token: 0x04001CCF RID: 7375
			private readonly int _getObjectRetType;

			// Token: 0x04001CD0 RID: 7376
			private object _currentKey;

			// Token: 0x04001CD1 RID: 7377
			private object _currentValue;
		}

		// Token: 0x020007B4 RID: 1972
		internal class HashtableDebugView
		{
			// Token: 0x06005F8C RID: 24460 RVA: 0x001CA579 File Offset: 0x001C9779
			public HashtableDebugView(Hashtable hashtable)
			{
				if (hashtable == null)
				{
					throw new ArgumentNullException("hashtable");
				}
				this._hashtable = hashtable;
			}

			// Token: 0x17000FAA RID: 4010
			// (get) Token: 0x06005F8D RID: 24461 RVA: 0x001CA596 File Offset: 0x001C9796
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this._hashtable.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04001CD2 RID: 7378
			private readonly Hashtable _hashtable;
		}
	}
}
