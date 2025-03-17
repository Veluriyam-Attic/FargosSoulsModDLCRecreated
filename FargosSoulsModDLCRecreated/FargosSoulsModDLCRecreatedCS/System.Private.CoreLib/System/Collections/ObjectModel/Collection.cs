using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020007CD RID: 1997
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Collection<[Nullable(2)] T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x06006007 RID: 24583 RVA: 0x001CBBEF File Offset: 0x001CADEF
		public Collection()
		{
			this.items = new List<T>();
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x001CBC02 File Offset: 0x001CAE02
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.items = list;
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x001CBC1B File Offset: 0x001CAE1B
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x001CBC28 File Offset: 0x001CAE28
		protected IList<T> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (this.items.IsReadOnly)
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				}
				if (index >= this.items.Count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this.SetItem(index, value);
			}
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x001CBC70 File Offset: 0x001CAE70
		public void Add(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int count = this.items.Count;
			this.InsertItem(count, item);
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x001CBCA4 File Offset: 0x001CAEA4
		public void Clear()
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			this.ClearItems();
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x001CBCBF File Offset: 0x001CAEBF
		public void CopyTo(T[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x001CBCCE File Offset: 0x001CAECE
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x001CBCDC File Offset: 0x001CAEDC
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x001CBCE9 File Offset: 0x001CAEE9
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x001CBCF7 File Offset: 0x001CAEF7
		public void Insert(int index, T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index > this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.InsertItem(index, item);
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x001CBD28 File Offset: 0x001CAF28
		public bool Remove(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int num = this.items.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x001CBD63 File Offset: 0x001CAF63
		public void RemoveAt(int index)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index >= this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.RemoveItem(index);
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x001CBD92 File Offset: 0x001CAF92
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x001CBD9F File Offset: 0x001CAF9F
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x001CBDAE File Offset: 0x001CAFAE
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x001CBDBC File Offset: 0x001CAFBC
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600601A RID: 24602 RVA: 0x001CBDCB File Offset: 0x001CAFCB
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x001CBDD8 File Offset: 0x001CAFD8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600601C RID: 24604 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x001CBDE8 File Offset: 0x001CAFE8
		object ICollection.SyncRoot
		{
			get
			{
				ICollection collection = this.items as ICollection;
				if (collection == null)
				{
					return this;
				}
				return collection.SyncRoot;
			}
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x001CBE0C File Offset: 0x001CB00C
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
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.items.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			int count = this.items.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.items[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x17000FD9 RID: 4057
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				T value2 = default(T);
				try
				{
					value2 = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(T));
				}
				this[index] = value2;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x001CBDCB File Offset: 0x001CAFCB
		bool IList.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06006022 RID: 24610 RVA: 0x001CBF6C File Offset: 0x001CB16C
		bool IList.IsFixedSize
		{
			get
			{
				IList list = this.items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x001CBF9C File Offset: 0x001CB19C
		int IList.Add(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			T item = default(T);
			try
			{
				item = (T)((object)value);
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(T));
			}
			this.Add(item);
			return this.Count - 1;
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x001CC008 File Offset: 0x001CB208
		bool IList.Contains(object value)
		{
			return Collection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x001CC020 File Offset: 0x001CB220
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x001CC038 File Offset: 0x001CB238
		void IList.Insert(int index, object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			T item = default(T);
			try
			{
				item = (T)((object)value);
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(T));
			}
			this.Insert(index, item);
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x001CC09C File Offset: 0x001CB29C
		void IList.Remove(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (Collection<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x001CC0C8 File Offset: 0x001CB2C8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x04001CFE RID: 7422
		private readonly IList<T> items;
	}
}
