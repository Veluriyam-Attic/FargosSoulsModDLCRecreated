using System;

namespace System.Globalization
{
	// Token: 0x02000209 RID: 521
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x06002134 RID: 8500 RVA: 0x0012D6A2 File Offset: 0x0012C8A2
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x04000839 RID: 2105
		internal HebrewNumber.HS state;

		// Token: 0x0400083A RID: 2106
		internal int result;
	}
}
