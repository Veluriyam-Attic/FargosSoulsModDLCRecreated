using System;

namespace System.Globalization
{
	// Token: 0x020001FE RID: 510
	internal readonly struct DaylightTimeStruct
	{
		// Token: 0x0600208C RID: 8332 RVA: 0x0012AB0D File Offset: 0x00129D0D
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x0400080D RID: 2061
		public readonly DateTime Start;

		// Token: 0x0400080E RID: 2062
		public readonly DateTime End;

		// Token: 0x0400080F RID: 2063
		public readonly TimeSpan Delta;
	}
}
