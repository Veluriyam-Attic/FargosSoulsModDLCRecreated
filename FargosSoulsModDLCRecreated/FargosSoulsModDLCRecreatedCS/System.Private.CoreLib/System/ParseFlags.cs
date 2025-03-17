using System;

namespace System
{
	// Token: 0x0200011C RID: 284
	[Flags]
	internal enum ParseFlags
	{
		// Token: 0x04000380 RID: 896
		HaveYear = 1,
		// Token: 0x04000381 RID: 897
		HaveMonth = 2,
		// Token: 0x04000382 RID: 898
		HaveDay = 4,
		// Token: 0x04000383 RID: 899
		HaveHour = 8,
		// Token: 0x04000384 RID: 900
		HaveMinute = 16,
		// Token: 0x04000385 RID: 901
		HaveSecond = 32,
		// Token: 0x04000386 RID: 902
		HaveTime = 64,
		// Token: 0x04000387 RID: 903
		HaveDate = 128,
		// Token: 0x04000388 RID: 904
		TimeZoneUsed = 256,
		// Token: 0x04000389 RID: 905
		TimeZoneUtc = 512,
		// Token: 0x0400038A RID: 906
		ParsedMonthName = 1024,
		// Token: 0x0400038B RID: 907
		CaptureOffset = 2048,
		// Token: 0x0400038C RID: 908
		YearDefault = 4096,
		// Token: 0x0400038D RID: 909
		Rfc1123Pattern = 8192,
		// Token: 0x0400038E RID: 910
		UtcSortPattern = 16384
	}
}
