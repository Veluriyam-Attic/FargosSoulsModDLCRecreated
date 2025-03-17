using System;

namespace System
{
	// Token: 0x0200005B RID: 91
	internal struct Currency
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000AFA13 File Offset: 0x000AEC13
		public Currency(decimal value)
		{
			this.m_value = decimal.ToOACurrency(value);
		}

		// Token: 0x040000DB RID: 219
		internal long m_value;
	}
}
