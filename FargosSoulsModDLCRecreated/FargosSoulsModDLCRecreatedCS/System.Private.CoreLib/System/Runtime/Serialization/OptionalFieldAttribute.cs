using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E7 RID: 999
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x0016A655 File Offset: 0x00169855
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x0016A65D File Offset: 0x0016985D
		public int VersionAdded
		{
			get
			{
				return this._versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(SR.Serialization_OptionalFieldVersionValue);
				}
				this._versionAdded = value;
			}
		}

		// Token: 0x04000DF3 RID: 3571
		private int _versionAdded = 1;
	}
}
