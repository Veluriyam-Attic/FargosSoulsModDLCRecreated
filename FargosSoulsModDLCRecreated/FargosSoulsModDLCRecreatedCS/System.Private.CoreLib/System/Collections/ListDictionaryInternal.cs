using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007C1 RID: 1985
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000FBA RID: 4026
		[Nullable(2)]
		public object this[object key]
		{
			[return: Nullable(2)]
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
				}
				return null;
			}
			[param: Nullable(2)]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005FBA RID: 24506 RVA: 0x001CA69B File Offset: 0x001C989B
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005FBB RID: 24507 RVA: 0x001CA6A3 File Offset: 0x001C98A3
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005FBF RID: 24511 RVA: 0x000AC098 File Offset: 0x000AB298
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x001CA6AC File Offset: 0x001C98AC
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x001CA6B8 File Offset: 0x001C98B8
		public void Add(object key, [Nullable(2)] object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(SR.Format(SR.Argument_AddingDuplicate__, next.key, key));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x001CA762 File Offset: 0x001C9962
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x001CA780 File Offset: 0x001C9980
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x001CA7C4 File Offset: 0x001C99C4
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(SR.ArgumentOutOfRange_Index, "index");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x001CA85C File Offset: 0x001C9A5C
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x001CA85C File Offset: 0x001C9A5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x001CA864 File Offset: 0x001C9A64
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04001CD5 RID: 7381
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x04001CD6 RID: 7382
		private int version;

		// Token: 0x04001CD7 RID: 7383
		private int count;

		// Token: 0x020007C2 RID: 1986
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005FC8 RID: 24520 RVA: 0x001CA8EC File Offset: 0x001C9AEC
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x001CA915 File Offset: 0x001C9B15
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x06005FCA RID: 24522 RVA: 0x001CA922 File Offset: 0x001C9B22
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x06005FCB RID: 24523 RVA: 0x001CA952 File Offset: 0x001C9B52
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.key;
				}
			}

			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x06005FCC RID: 24524 RVA: 0x001CA972 File Offset: 0x001C9B72
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.value;
				}
			}

			// Token: 0x06005FCD RID: 24525 RVA: 0x001CA994 File Offset: 0x001C9B94
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06005FCE RID: 24526 RVA: 0x001CAA03 File Offset: 0x001C9C03
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04001CD8 RID: 7384
			private readonly ListDictionaryInternal list;

			// Token: 0x04001CD9 RID: 7385
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04001CDA RID: 7386
			private readonly int version;

			// Token: 0x04001CDB RID: 7387
			private bool start;
		}

		// Token: 0x020007C3 RID: 1987
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005FCF RID: 24527 RVA: 0x001CAA31 File Offset: 0x001C9C31
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06005FD0 RID: 24528 RVA: 0x001CAA48 File Offset: 0x001C9C48
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Arg_RankMultiDimNotSupported);
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException(SR.ArgumentOutOfRange_Index, "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x001CAAEC File Offset: 0x001C9CEC
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x000AC09B File Offset: 0x000AB29B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x001CAB18 File Offset: 0x001C9D18
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06005FD4 RID: 24532 RVA: 0x001CAB25 File Offset: 0x001C9D25
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04001CDC RID: 7388
			private readonly ListDictionaryInternal list;

			// Token: 0x04001CDD RID: 7389
			private readonly bool isKeys;

			// Token: 0x020007C4 RID: 1988
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06005FD5 RID: 24533 RVA: 0x001CAB38 File Offset: 0x001C9D38
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17000FC9 RID: 4041
				// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x001CAB68 File Offset: 0x001C9D68
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06005FD7 RID: 24535 RVA: 0x001CAB9C File Offset: 0x001C9D9C
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06005FD8 RID: 24536 RVA: 0x001CAC0B File Offset: 0x001C9E0B
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04001CDE RID: 7390
				private readonly ListDictionaryInternal list;

				// Token: 0x04001CDF RID: 7391
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04001CE0 RID: 7392
				private readonly int version;

				// Token: 0x04001CE1 RID: 7393
				private readonly bool isKeys;

				// Token: 0x04001CE2 RID: 7394
				private bool start;
			}
		}

		// Token: 0x020007C5 RID: 1989
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04001CE3 RID: 7395
			public object key;

			// Token: 0x04001CE4 RID: 7396
			public object value;

			// Token: 0x04001CE5 RID: 7397
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
