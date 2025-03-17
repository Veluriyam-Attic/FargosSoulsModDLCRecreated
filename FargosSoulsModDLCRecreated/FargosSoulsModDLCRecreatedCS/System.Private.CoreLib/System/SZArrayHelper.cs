using System;
using System.Collections.Generic;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000054 RID: 84
	internal sealed class SZArrayHelper
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000AE118 File Offset: 0x000AD318
		internal IEnumerator<T> GetEnumerator<T>()
		{
			T[] array = Unsafe.As<T[]>(this);
			if (array.Length != 0)
			{
				return new SZGenericArrayEnumerator<T>(array);
			}
			return SZGenericArrayEnumerator<T>.Empty;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000AE13C File Offset: 0x000AD33C
		private void CopyTo<T>(T[] array, int index)
		{
			T[] array2 = Unsafe.As<T[]>(this);
			Array.Copy(array2, 0, array, index, array2.Length);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000AE15C File Offset: 0x000AD35C
		internal int get_Count<T>()
		{
			T[] array = Unsafe.As<T[]>(this);
			return array.Length;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000AE174 File Offset: 0x000AD374
		internal T get_Item<T>(int index)
		{
			T[] array = Unsafe.As<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return array[index];
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000AE19C File Offset: 0x000AD39C
		internal void set_Item<T>(int index, T value)
		{
			T[] array = Unsafe.As<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			array[index] = value;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		private void Add<T>(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000AE1C4 File Offset: 0x000AD3C4
		private bool Contains<T>(T value)
		{
			T[] array = Unsafe.As<T[]>(this);
			return Array.IndexOf<T>(array, value, 0, array.Length) >= 0;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000AC09E File Offset: 0x000AB29E
		private bool get_IsReadOnly<T>()
		{
			return true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000AE1E9 File Offset: 0x000AD3E9
		private void Clear<T>()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000AE1F4 File Offset: 0x000AD3F4
		private int IndexOf<T>(T value)
		{
			T[] array = Unsafe.As<T[]>(this);
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		private void Insert<T>(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000AC0B4 File Offset: 0x000AB2B4
		private bool Remove<T>(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000AC0F1 File Offset: 0x000AB2F1
		private void RemoveAt<T>(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_FixedSizeCollection);
		}
	}
}
