using System;
using System.Globalization;

namespace System
{
	// Token: 0x0200011E RID: 286
	internal struct ParsingInfo
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x000D77EB File Offset: 0x000D69EB
		internal void Init()
		{
			this.dayOfWeek = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
		}

		// Token: 0x040003A1 RID: 929
		internal Calendar calendar;

		// Token: 0x040003A2 RID: 930
		internal int dayOfWeek;

		// Token: 0x040003A3 RID: 931
		internal DateTimeParse.TM timeMark;

		// Token: 0x040003A4 RID: 932
		internal bool fUseHour12;

		// Token: 0x040003A5 RID: 933
		internal bool fUseTwoDigitYear;

		// Token: 0x040003A6 RID: 934
		internal bool fAllowInnerWhite;

		// Token: 0x040003A7 RID: 935
		internal bool fAllowTrailingWhite;

		// Token: 0x040003A8 RID: 936
		internal bool fCustomNumberParser;

		// Token: 0x040003A9 RID: 937
		internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;
	}
}
