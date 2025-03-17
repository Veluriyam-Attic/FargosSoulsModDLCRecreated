using System;

namespace System
{
	// Token: 0x0200011F RID: 287
	internal enum TokenType
	{
		// Token: 0x040003AB RID: 939
		NumberToken = 1,
		// Token: 0x040003AC RID: 940
		YearNumberToken,
		// Token: 0x040003AD RID: 941
		Am,
		// Token: 0x040003AE RID: 942
		Pm,
		// Token: 0x040003AF RID: 943
		MonthToken,
		// Token: 0x040003B0 RID: 944
		EndOfString,
		// Token: 0x040003B1 RID: 945
		DayOfWeekToken,
		// Token: 0x040003B2 RID: 946
		TimeZoneToken,
		// Token: 0x040003B3 RID: 947
		EraToken,
		// Token: 0x040003B4 RID: 948
		DateWordToken,
		// Token: 0x040003B5 RID: 949
		UnknownToken,
		// Token: 0x040003B6 RID: 950
		HebrewNumber,
		// Token: 0x040003B7 RID: 951
		JapaneseEraToken,
		// Token: 0x040003B8 RID: 952
		TEraToken,
		// Token: 0x040003B9 RID: 953
		IgnorableSymbol,
		// Token: 0x040003BA RID: 954
		SEP_Unk = 256,
		// Token: 0x040003BB RID: 955
		SEP_End = 512,
		// Token: 0x040003BC RID: 956
		SEP_Space = 768,
		// Token: 0x040003BD RID: 957
		SEP_Am = 1024,
		// Token: 0x040003BE RID: 958
		SEP_Pm = 1280,
		// Token: 0x040003BF RID: 959
		SEP_Date = 1536,
		// Token: 0x040003C0 RID: 960
		SEP_Time = 1792,
		// Token: 0x040003C1 RID: 961
		SEP_YearSuff = 2048,
		// Token: 0x040003C2 RID: 962
		SEP_MonthSuff = 2304,
		// Token: 0x040003C3 RID: 963
		SEP_DaySuff = 2560,
		// Token: 0x040003C4 RID: 964
		SEP_HourSuff = 2816,
		// Token: 0x040003C5 RID: 965
		SEP_MinuteSuff = 3072,
		// Token: 0x040003C6 RID: 966
		SEP_SecondSuff = 3328,
		// Token: 0x040003C7 RID: 967
		SEP_LocalTimeMark = 3584,
		// Token: 0x040003C8 RID: 968
		SEP_DateOrOffset = 3840,
		// Token: 0x040003C9 RID: 969
		RegularTokenMask = 255,
		// Token: 0x040003CA RID: 970
		SeparatorTokenMask = 65280
	}
}
