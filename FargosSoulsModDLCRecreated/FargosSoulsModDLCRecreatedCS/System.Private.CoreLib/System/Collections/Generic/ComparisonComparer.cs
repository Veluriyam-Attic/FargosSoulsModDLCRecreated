using System;

namespace System.Collections.Generic
{
	// Token: 0x020007DF RID: 2015
	internal sealed class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x060060C0 RID: 24768 RVA: 0x001CEA60 File Offset: 0x001CDC60
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x001CEA6F File Offset: 0x001CDC6F
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04001D04 RID: 7428
		private readonly Comparison<T> _comparison;
	}
}
