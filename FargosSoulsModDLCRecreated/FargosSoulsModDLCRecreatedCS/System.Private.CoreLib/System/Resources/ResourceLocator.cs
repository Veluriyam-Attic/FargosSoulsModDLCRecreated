using System;

namespace System.Resources
{
	// Token: 0x02000580 RID: 1408
	internal struct ResourceLocator
	{
		// Token: 0x06004854 RID: 18516 RVA: 0x00181742 File Offset: 0x00180942
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x00181752 File Offset: 0x00180952
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x0018175A File Offset: 0x0018095A
		// (set) Token: 0x06004857 RID: 18519 RVA: 0x00181762 File Offset: 0x00180962
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0018176B File Offset: 0x0018096B
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04001194 RID: 4500
		internal object _value;

		// Token: 0x04001195 RID: 4501
		internal int _dataPos;
	}
}
