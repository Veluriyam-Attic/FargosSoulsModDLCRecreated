using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000184 RID: 388
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000F2497 File Offset: 0x000F1697
		public static StringComparer InvariantCulture
		{
			get
			{
				return CultureAwareComparer.InvariantCaseSensitiveInstance;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000F249E File Offset: 0x000F169E
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return CultureAwareComparer.InvariantIgnoreCaseInstance;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000F24A5 File Offset: 0x000F16A5
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.None);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x000F24B2 File Offset: 0x000F16B2
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x000F24BF File Offset: 0x000F16BF
		public static StringComparer Ordinal
		{
			get
			{
				return OrdinalCaseSensitiveComparer.Instance;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x000F24C6 File Offset: 0x000F16C6
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return OrdinalIgnoreCaseComparer.Instance;
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000F24D0 File Offset: 0x000F16D0
		public static StringComparer FromComparison(StringComparison comparisonType)
		{
			StringComparer result;
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				result = StringComparer.CurrentCulture;
				break;
			case StringComparison.CurrentCultureIgnoreCase:
				result = StringComparer.CurrentCultureIgnoreCase;
				break;
			case StringComparison.InvariantCulture:
				result = StringComparer.InvariantCulture;
				break;
			case StringComparison.InvariantCultureIgnoreCase:
				result = StringComparer.InvariantCultureIgnoreCase;
				break;
			case StringComparison.Ordinal:
				result = StringComparer.Ordinal;
				break;
			case StringComparison.OrdinalIgnoreCase:
				result = StringComparer.OrdinalIgnoreCase;
				break;
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
			return result;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000F253E File Offset: 0x000F173E
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000F255B File Offset: 0x000F175B
		public static StringComparer Create(CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, options);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000F2574 File Offset: 0x000F1774
		[NullableContext(2)]
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException(SR.Argument_ImplementIComparable);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000F25CC File Offset: 0x000F17CC
		[NullableContext(2)]
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000F260C File Offset: 0x000F180C
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		// Token: 0x060017E4 RID: 6116
		[NullableContext(2)]
		public abstract int Compare(string x, string y);

		// Token: 0x060017E5 RID: 6117
		[NullableContext(2)]
		public abstract bool Equals(string x, string y);

		// Token: 0x060017E6 RID: 6118
		public abstract int GetHashCode(string obj);
	}
}
