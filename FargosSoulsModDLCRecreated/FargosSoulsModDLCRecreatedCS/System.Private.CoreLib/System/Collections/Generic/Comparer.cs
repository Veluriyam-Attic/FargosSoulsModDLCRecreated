using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D5 RID: 2005
	[NullableContext(1)]
	[Nullable(0)]
	[TypeDependency("System.Collections.Generic.ObjectComparer`1")]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class Comparer<[Nullable(2)] T> : IComparer, IComparer<T>
	{
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x001CE090 File Offset: 0x001CD290
		public static Comparer<T> Default { get; } = (Comparer<T>)ComparerHelpers.CreateDefaultComparer(typeof(T));

		// Token: 0x06006082 RID: 24706 RVA: 0x001CE097 File Offset: 0x001CD297
		public static Comparer<T> Create(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new ComparisonComparer<T>(comparison);
		}

		// Token: 0x06006083 RID: 24707
		[NullableContext(2)]
		public abstract int Compare(T x, T y);

		// Token: 0x06006084 RID: 24708 RVA: 0x001CE0AD File Offset: 0x001CD2AD
		int IComparer.Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				if (x is T && y is T)
				{
					return this.Compare((T)((object)x), (T)((object)y));
				}
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
				return 0;
			}
		}
	}
}
