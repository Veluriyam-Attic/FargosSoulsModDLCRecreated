using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D8 RID: 2008
	[TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class EqualityComparer<[Nullable(2)] T> : IEqualityComparer, IEqualityComparer<T>
	{
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x001CE3BE File Offset: 0x001CD5BE
		public static EqualityComparer<T> Default { [Intrinsic] get; } = (EqualityComparer<T>)ComparerHelpers.CreateDefaultEqualityComparer(typeof(T));

		// Token: 0x06006094 RID: 24724 RVA: 0x001CE3C8 File Offset: 0x001CD5C8
		internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001CE3FC File Offset: 0x001CD5FC
		internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006096 RID: 24726
		[NullableContext(2)]
		public abstract bool Equals(T x, T y);

		// Token: 0x06006097 RID: 24727
		public abstract int GetHashCode([DisallowNull] T obj);

		// Token: 0x06006098 RID: 24728 RVA: 0x001CE42F File Offset: 0x001CD62F
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is T)
			{
				return this.GetHashCode((T)((object)obj));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return 0;
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x001CE453 File Offset: 0x001CD653
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is T && y is T)
			{
				return this.Equals((T)((object)x), (T)((object)y));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}
	}
}
