using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200051A RID: 1306
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConditionalWeakTable<TKey, [Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class where TValue : class
	{
		// Token: 0x060046D1 RID: 18129 RVA: 0x0017C23C File Offset: 0x0017B43C
		public ConditionalWeakTable()
		{
			this._lock = new object();
			this._container = new ConditionalWeakTable<TKey, TValue>.Container(this);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x0017C25D File Offset: 0x0017B45D
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return this._container.TryGetValueWorker(key, out value);
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x0017C27C File Offset: 0x0017B47C
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			lock (@lock)
			{
				object obj;
				int num = this._container.FindEntry(key, out obj);
				if (num != -1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
				}
				this.CreateEntry(key, value);
			}
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x0017C2E8 File Offset: 0x0017B4E8
		public void AddOrUpdate(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			lock (@lock)
			{
				object obj;
				int num = this._container.FindEntry(key, out obj);
				if (num != -1)
				{
					this._container.UpdateValue(num, value);
				}
				else
				{
					this.CreateEntry(key, value);
				}
			}
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x0017C360 File Offset: 0x0017B560
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				result = this._container.Remove(key);
			}
			return result;
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x0017C3B8 File Offset: 0x0017B5B8
		public void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._activeEnumeratorRefCount > 0)
				{
					this._container.RemoveAllKeys();
				}
				else
				{
					this._container = new ConditionalWeakTable<TKey, TValue>.Container(this);
				}
			}
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x0017C418 File Offset: 0x0017B618
		public TValue GetValue(TKey key, [Nullable(new byte[]
		{
			1,
			0,
			0
		})] ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
		{
			if (createValueCallback == null)
			{
				throw new ArgumentNullException("createValueCallback");
			}
			TValue result;
			if (!this.TryGetValue(key, out result))
			{
				return this.GetValueLocked(key, createValueCallback);
			}
			return result;
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x0017C448 File Offset: 0x0017B648
		private TValue GetValueLocked(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
		{
			TValue tvalue = createValueCallback(key);
			object @lock = this._lock;
			TValue result;
			lock (@lock)
			{
				TValue tvalue2;
				if (this._container.TryGetValueWorker(key, out tvalue2))
				{
					result = tvalue2;
				}
				else
				{
					this.CreateEntry(key, tvalue);
					result = tvalue;
				}
			}
			return result;
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x0017C4B0 File Offset: 0x0017B6B0
		public TValue GetOrCreateValue(TKey key)
		{
			return this.GetValue(key, (TKey _) => Activator.CreateInstance<TValue>());
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x0017C4D8 File Offset: 0x0017B6D8
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			object @lock = this._lock;
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
			lock (@lock)
			{
				ConditionalWeakTable<TKey, TValue>.Container container = this._container;
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator2;
				if (container != null && container.FirstFreeEntry != 0)
				{
					enumerator = new ConditionalWeakTable<TKey, TValue>.Enumerator(this);
					enumerator2 = enumerator;
				}
				else
				{
					enumerator2 = ((IEnumerable<KeyValuePair<!0, !1>>)Array.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();
				}
				enumerator = enumerator2;
			}
			return enumerator;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x0017C53C File Offset: 0x0017B73C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<!0, !1>>)this).GetEnumerator();
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x0017C544 File Offset: 0x0017B744
		private void CreateEntry(TKey key, TValue value)
		{
			ConditionalWeakTable<TKey, TValue>.Container container = this._container;
			if (!container.HasCapacity)
			{
				container = (this._container = container.Resize());
			}
			container.CreateEntryNoResize(key, value);
		}

		// Token: 0x040010F4 RID: 4340
		private readonly object _lock;

		// Token: 0x040010F5 RID: 4341
		private volatile ConditionalWeakTable<TKey, TValue>.Container _container;

		// Token: 0x040010F6 RID: 4342
		private int _activeEnumeratorRefCount;

		// Token: 0x0200051B RID: 1307
		// (Invoke) Token: 0x060046DE RID: 18142
		[NullableContext(0)]
		public delegate TValue CreateValueCallback(TKey key);

		// Token: 0x0200051C RID: 1308
		private sealed class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x060046E1 RID: 18145 RVA: 0x0017C57A File Offset: 0x0017B77A
			public Enumerator(ConditionalWeakTable<TKey, TValue> table)
			{
				this._table = table;
				table._activeEnumeratorRefCount++;
				this._maxIndexInclusive = table._container.FirstFreeEntry - 1;
				this._currentIndex = -1;
			}

			// Token: 0x060046E2 RID: 18146 RVA: 0x0017C5B4 File Offset: 0x0017B7B4
			~Enumerator()
			{
				this.Dispose();
			}

			// Token: 0x060046E3 RID: 18147 RVA: 0x0017C5E0 File Offset: 0x0017B7E0
			public void Dispose()
			{
				ConditionalWeakTable<TKey, TValue> conditionalWeakTable = Interlocked.Exchange<ConditionalWeakTable<TKey, TValue>>(ref this._table, null);
				if (conditionalWeakTable != null)
				{
					this._current = default(KeyValuePair<TKey, TValue>);
					object @lock = conditionalWeakTable._lock;
					lock (@lock)
					{
						conditionalWeakTable._activeEnumeratorRefCount--;
					}
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x060046E4 RID: 18148 RVA: 0x0017C64C File Offset: 0x0017B84C
			public bool MoveNext()
			{
				ConditionalWeakTable<TKey, TValue> table = this._table;
				if (table != null)
				{
					object @lock = table._lock;
					lock (@lock)
					{
						ConditionalWeakTable<TKey, TValue>.Container container = table._container;
						if (container != null)
						{
							while (this._currentIndex < this._maxIndexInclusive)
							{
								this._currentIndex++;
								TKey key;
								TValue value;
								if (container.TryGetEntry(this._currentIndex, out key, out value))
								{
									this._current = new KeyValuePair<TKey, TValue>(key, value);
									return true;
								}
							}
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x060046E5 RID: 18149 RVA: 0x0017C6E4 File Offset: 0x0017B8E4
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._currentIndex < 0)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current;
				}
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x060046E6 RID: 18150 RVA: 0x0017C6FA File Offset: 0x0017B8FA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060046E7 RID: 18151 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Reset()
			{
			}

			// Token: 0x040010F7 RID: 4343
			private ConditionalWeakTable<TKey, TValue> _table;

			// Token: 0x040010F8 RID: 4344
			private readonly int _maxIndexInclusive;

			// Token: 0x040010F9 RID: 4345
			private int _currentIndex;

			// Token: 0x040010FA RID: 4346
			private KeyValuePair<TKey, TValue> _current;
		}

		// Token: 0x0200051D RID: 1309
		private struct Entry
		{
			// Token: 0x040010FB RID: 4347
			public DependentHandle depHnd;

			// Token: 0x040010FC RID: 4348
			public int HashCode;

			// Token: 0x040010FD RID: 4349
			public int Next;
		}

		// Token: 0x0200051E RID: 1310
		private sealed class Container
		{
			// Token: 0x060046E8 RID: 18152 RVA: 0x0017C708 File Offset: 0x0017B908
			internal Container(ConditionalWeakTable<TKey, TValue> parent)
			{
				this._buckets = new int[8];
				for (int i = 0; i < this._buckets.Length; i++)
				{
					this._buckets[i] = -1;
				}
				this._entries = new ConditionalWeakTable<TKey, TValue>.Entry[8];
				this._parent = parent;
			}

			// Token: 0x060046E9 RID: 18153 RVA: 0x0017C756 File Offset: 0x0017B956
			private Container(ConditionalWeakTable<TKey, TValue> parent, int[] buckets, ConditionalWeakTable<TKey, TValue>.Entry[] entries, int firstFreeEntry)
			{
				this._parent = parent;
				this._buckets = buckets;
				this._entries = entries;
				this._firstFreeEntry = firstFreeEntry;
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x060046EA RID: 18154 RVA: 0x0017C77B File Offset: 0x0017B97B
			internal bool HasCapacity
			{
				get
				{
					return this._firstFreeEntry < this._entries.Length;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x060046EB RID: 18155 RVA: 0x0017C78D File Offset: 0x0017B98D
			internal int FirstFreeEntry
			{
				get
				{
					return this._firstFreeEntry;
				}
			}

			// Token: 0x060046EC RID: 18156 RVA: 0x0017C798 File Offset: 0x0017B998
			internal void CreateEntryNoResize(TKey key, TValue value)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
				int firstFreeEntry = this._firstFreeEntry;
				this._firstFreeEntry = firstFreeEntry + 1;
				int num2 = firstFreeEntry;
				this._entries[num2].HashCode = num;
				this._entries[num2].depHnd = new DependentHandle(key, value);
				int num3 = num & this._buckets.Length - 1;
				this._entries[num2].Next = this._buckets[num3];
				Volatile.Write(ref this._buckets[num3], num2);
				this._invalid = false;
			}

			// Token: 0x060046ED RID: 18157 RVA: 0x0017C84C File Offset: 0x0017BA4C
			internal bool TryGetValueWorker(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				object value2;
				int num = this.FindEntry(key, out value2);
				value = Unsafe.As<TValue>(value2);
				return num != -1;
			}

			// Token: 0x060046EE RID: 18158 RVA: 0x0017C878 File Offset: 0x0017BA78
			internal int FindEntry(TKey key, out object value)
			{
				int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
				int num2 = num & this._buckets.Length - 1;
				for (int num3 = Volatile.Read(ref this._buckets[num2]); num3 != -1; num3 = this._entries[num3].Next)
				{
					if (this._entries[num3].HashCode == num && this._entries[num3].depHnd.GetPrimaryAndSecondary(out value) == key)
					{
						GC.KeepAlive(this);
						return num3;
					}
				}
				GC.KeepAlive(this);
				value = null;
				return -1;
			}

			// Token: 0x060046EF RID: 18159 RVA: 0x0017C914 File Offset: 0x0017BB14
			internal bool TryGetEntry(int index, [NotNullWhen(true)] out TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				if (index < this._entries.Length)
				{
					object value2;
					object primaryAndSecondary = this._entries[index].depHnd.GetPrimaryAndSecondary(out value2);
					GC.KeepAlive(this);
					if (primaryAndSecondary != null)
					{
						key = Unsafe.As<TKey>(primaryAndSecondary);
						value = Unsafe.As<TValue>(value2);
						return true;
					}
				}
				key = default(TKey);
				value = default(TValue);
				return false;
			}

			// Token: 0x060046F0 RID: 18160 RVA: 0x0017C978 File Offset: 0x0017BB78
			internal void RemoveAllKeys()
			{
				for (int i = 0; i < this._firstFreeEntry; i++)
				{
					this.RemoveIndex(i);
				}
			}

			// Token: 0x060046F1 RID: 18161 RVA: 0x0017C9A0 File Offset: 0x0017BBA0
			internal bool Remove(TKey key)
			{
				this.VerifyIntegrity();
				object obj;
				int num = this.FindEntry(key, out obj);
				if (num != -1)
				{
					this.RemoveIndex(num);
					return true;
				}
				return false;
			}

			// Token: 0x060046F2 RID: 18162 RVA: 0x0017C9CC File Offset: 0x0017BBCC
			private void RemoveIndex(int entryIndex)
			{
				ref ConditionalWeakTable<TKey, TValue>.Entry ptr = ref this._entries[entryIndex];
				Volatile.Write(ref ptr.HashCode, -1);
				ptr.depHnd.SetPrimary(null);
			}

			// Token: 0x060046F3 RID: 18163 RVA: 0x0017C9FE File Offset: 0x0017BBFE
			internal void UpdateValue(int entryIndex, TValue newValue)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				this._entries[entryIndex].depHnd.SetSecondary(newValue);
				this._invalid = false;
			}

			// Token: 0x060046F4 RID: 18164 RVA: 0x0017CA30 File Offset: 0x0017BC30
			internal ConditionalWeakTable<TKey, TValue>.Container Resize()
			{
				bool flag = false;
				int newSize = this._buckets.Length;
				if (this._parent == null || this._parent._activeEnumeratorRefCount == 0)
				{
					for (int i = 0; i < this._entries.Length; i++)
					{
						ref ConditionalWeakTable<TKey, TValue>.Entry ptr = ref this._entries[i];
						if (ptr.HashCode == -1)
						{
							flag = true;
							break;
						}
						if (ptr.depHnd.IsAllocated && ptr.depHnd.GetPrimary() == null)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					newSize = this._buckets.Length * 2;
				}
				return this.Resize(newSize);
			}

			// Token: 0x060046F5 RID: 18165 RVA: 0x0017CAC0 File Offset: 0x0017BCC0
			internal ConditionalWeakTable<TKey, TValue>.Container Resize(int newSize)
			{
				int[] array = new int[newSize];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = -1;
				}
				ConditionalWeakTable<TKey, TValue>.Entry[] array2 = new ConditionalWeakTable<TKey, TValue>.Entry[newSize];
				int j = 0;
				bool flag = this._parent != null && this._parent._activeEnumeratorRefCount > 0;
				if (flag)
				{
					while (j < this._entries.Length)
					{
						ref ConditionalWeakTable<TKey, TValue>.Entry ptr = ref this._entries[j];
						ref ConditionalWeakTable<TKey, TValue>.Entry ptr2 = ref array2[j];
						int hashCode = ptr.HashCode;
						ptr2.HashCode = hashCode;
						ptr2.depHnd = ptr.depHnd;
						int num = hashCode & array.Length - 1;
						ptr2.Next = array[num];
						array[num] = j;
						j++;
					}
				}
				else
				{
					for (int k = 0; k < this._entries.Length; k++)
					{
						ref ConditionalWeakTable<TKey, TValue>.Entry ptr3 = ref this._entries[k];
						int hashCode2 = ptr3.HashCode;
						DependentHandle depHnd = ptr3.depHnd;
						if (hashCode2 != -1 && depHnd.IsAllocated)
						{
							if (depHnd.GetPrimary() != null)
							{
								ref ConditionalWeakTable<TKey, TValue>.Entry ptr4 = ref array2[j];
								ptr4.HashCode = hashCode2;
								ptr4.depHnd = depHnd;
								int num2 = hashCode2 & array.Length - 1;
								ptr4.Next = array[num2];
								array[num2] = j;
								j++;
							}
							else
							{
								Volatile.Write(ref ptr3.HashCode, -1);
							}
						}
					}
				}
				ConditionalWeakTable<TKey, TValue>.Container container = new ConditionalWeakTable<TKey, TValue>.Container(this._parent, array, array2, j);
				if (flag)
				{
					GC.SuppressFinalize(this);
				}
				this._oldKeepAlive = container;
				GC.KeepAlive(this);
				return container;
			}

			// Token: 0x060046F6 RID: 18166 RVA: 0x0017CC40 File Offset: 0x0017BE40
			private void VerifyIntegrity()
			{
				if (this._invalid)
				{
					throw new InvalidOperationException(SR.InvalidOperation_CollectionCorrupted);
				}
			}

			// Token: 0x060046F7 RID: 18167 RVA: 0x0017CC58 File Offset: 0x0017BE58
			protected override void Finalize()
			{
				try
				{
					if (!this._invalid && this._parent != null)
					{
						if (!this._finalized)
						{
							this._finalized = true;
							object @lock = this._parent._lock;
							lock (@lock)
							{
								if (this._parent._container == this)
								{
									this._parent._container = null;
								}
							}
							GC.ReRegisterForFinalize(this);
						}
						else
						{
							ConditionalWeakTable<TKey, TValue>.Entry[] entries = this._entries;
							this._invalid = true;
							this._entries = null;
							this._buckets = null;
							if (entries != null)
							{
								for (int i = 0; i < entries.Length; i++)
								{
									if (this._oldKeepAlive == null || entries[i].HashCode == -1)
									{
										entries[i].depHnd.Free();
									}
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x040010FE RID: 4350
			private readonly ConditionalWeakTable<TKey, TValue> _parent;

			// Token: 0x040010FF RID: 4351
			private int[] _buckets;

			// Token: 0x04001100 RID: 4352
			private ConditionalWeakTable<TKey, TValue>.Entry[] _entries;

			// Token: 0x04001101 RID: 4353
			private int _firstFreeEntry;

			// Token: 0x04001102 RID: 4354
			private bool _invalid;

			// Token: 0x04001103 RID: 4355
			private bool _finalized;

			// Token: 0x04001104 RID: 4356
			private volatile object _oldKeepAlive;
		}
	}
}
