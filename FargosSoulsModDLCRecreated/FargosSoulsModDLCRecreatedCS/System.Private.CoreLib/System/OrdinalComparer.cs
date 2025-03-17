using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000186 RID: 390
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class OrdinalComparer : StringComparer
	{
		// Token: 0x060017F2 RID: 6130 RVA: 0x000F281B File Offset: 0x000F1A1B
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000F282A File Offset: 0x000F1A2A
		public override int Compare(string x, string y)
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
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000F2854 File Offset: 0x000F1A54
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && System.Globalization.Ordinal.EqualsIgnoreCase(x.GetRawStringData(), y.GetRawStringData(), x.Length);
			}
			return x.Equals(y);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000F28A6 File Offset: 0x000F1AA6
		[NullableContext(1)]
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			if (this._ignoreCase)
			{
				return obj.GetHashCodeOrdinalIgnoreCase();
			}
			return obj.GetHashCode();
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000F28C8 File Offset: 0x000F1AC8
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000F28F0 File Offset: 0x000F1AF0
		public override int GetHashCode()
		{
			int hashCode = "OrdinalComparer".GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x040004A5 RID: 1189
		private readonly bool _ignoreCase;
	}
}
