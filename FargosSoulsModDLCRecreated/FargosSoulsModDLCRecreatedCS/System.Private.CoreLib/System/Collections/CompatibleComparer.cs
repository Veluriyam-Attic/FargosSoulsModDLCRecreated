using System;

namespace System.Collections
{
	// Token: 0x020007AB RID: 1963
	internal sealed class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06005F15 RID: 24341 RVA: 0x001C88E6 File Offset: 0x001C7AE6
		internal CompatibleComparer(IHashCodeProvider hashCodeProvider, IComparer comparer)
		{
			this._hcp = hashCodeProvider;
			this._comparer = comparer;
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005F16 RID: 24342 RVA: 0x001C88FC File Offset: 0x001C7AFC
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x001C8904 File Offset: 0x001C7B04
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x001C890C File Offset: 0x001C7B0C
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001C891C File Offset: 0x001C7B1C
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException(SR.Argument_ImplementIComparable);
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x001C896B File Offset: 0x001C7B6B
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp == null)
			{
				return obj.GetHashCode();
			}
			return this._hcp.GetHashCode(obj);
		}

		// Token: 0x04001CB5 RID: 7349
		private readonly IHashCodeProvider _hcp;

		// Token: 0x04001CB6 RID: 7350
		private readonly IComparer _comparer;
	}
}
