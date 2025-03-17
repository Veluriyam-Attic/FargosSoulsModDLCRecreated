using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020007CE RID: 1998
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ReadOnlyCollection<[Nullable(2)] T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection, IReadOnlyList<!0>, IReadOnlyCollection<T>
	{
		// Token: 0x06006029 RID: 24617 RVA: 0x001CC0F5 File Offset: 0x001CB2F5
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.list = list;
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x001CC10E File Offset: 0x001CB30E
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000FDD RID: 4061
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x001CC129 File Offset: 0x001CB329
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x001CC137 File Offset: 0x001CB337
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x001CC146 File Offset: 0x001CB346
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x001CC153 File Offset: 0x001CB353
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x001CC161 File Offset: 0x001CB361
		protected IList<T> Items
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x000AC09E File Offset: 0x000AB29E
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		T IList<!0>.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void ICollection<!0>.Add(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void ICollection<!0>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList<!0>.Insert(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x001CC169 File Offset: 0x001CB369
		bool ICollection<!0>.Remove(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList<!0>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x001CC172 File Offset: 0x001CB372
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x001CC180 File Offset: 0x001CB380
		object ICollection.SyncRoot
		{
			get
			{
				ICollection collection = this.list as ICollection;
				if (collection == null)
				{
					return this;
				}
				return collection.SyncRoot;
			}
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x001CC1A4 File Offset: 0x001CB3A4
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
				this.list.CopyTo(array2, index);
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
			int count = this.list.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.list[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x000AC09E File Offset: 0x000AB29E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x000AC09E File Offset: 0x000AB29E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x001CC2B3 File Offset: 0x001CB4B3
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return -1;
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x001CC0C8 File Offset: 0x001CB2C8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x001CC2BC File Offset: 0x001CB4BC
		bool IList.Contains(object value)
		{
			return ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x001CC2D4 File Offset: 0x001CB4D4
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x04001CFF RID: 7423
		private readonly IList<T> list;
	}
}
