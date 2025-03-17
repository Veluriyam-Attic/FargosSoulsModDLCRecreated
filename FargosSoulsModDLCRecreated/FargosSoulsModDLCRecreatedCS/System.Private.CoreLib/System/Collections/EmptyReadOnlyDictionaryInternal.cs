using System;

namespace System.Collections
{
	// Token: 0x0200079B RID: 1947
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005DB8 RID: 23992 RVA: 0x001C504C File Offset: 0x001C424C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x001C5054 File Offset: 0x001C4254
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
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x000AC09B File Offset: 0x000AB29B
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x000AC098 File Offset: 0x000AB298
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F3F RID: 3903
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.ArgumentNull_Key);
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(SR.Argument_NotSerializable, "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(SR.Argument_NotSerializable, "value");
				}
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x0018C06F File Offset: 0x0018B26F
		public ICollection Keys
		{
			get
			{
				return Array.Empty<object>();
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x0018C06F File Offset: 0x0018B26F
		public ICollection Values
		{
			get
			{
				return Array.Empty<object>();
			}
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x001C50D0 File Offset: 0x001C42D0
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.ArgumentNull_Key);
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(SR.Argument_NotSerializable, "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(SR.Argument_NotSerializable, "value");
			}
			throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x001C5137 File Offset: 0x001C4337
		public void Clear()
		{
			throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x001C504C File Offset: 0x001C424C
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x001C5137 File Offset: 0x001C4337
		public void Remove(object key)
		{
			throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
		}

		// Token: 0x0200079C RID: 1948
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005DC9 RID: 24009 RVA: 0x000AC09B File Offset: 0x000AB29B
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x17000F44 RID: 3908
			// (get) Token: 0x06005DCA RID: 24010 RVA: 0x000F2C71 File Offset: 0x000F1E71
			public object Current
			{
				get
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
			}

			// Token: 0x06005DCB RID: 24011 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void Reset()
			{
			}

			// Token: 0x17000F45 RID: 3909
			// (get) Token: 0x06005DCC RID: 24012 RVA: 0x000F2C71 File Offset: 0x000F1E71
			public object Key
			{
				get
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
			}

			// Token: 0x17000F46 RID: 3910
			// (get) Token: 0x06005DCD RID: 24013 RVA: 0x000F2C71 File Offset: 0x000F1E71
			public object Value
			{
				get
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
			}

			// Token: 0x17000F47 RID: 3911
			// (get) Token: 0x06005DCE RID: 24014 RVA: 0x001C5144 File Offset: 0x001C4344
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
				}
			}
		}
	}
}
