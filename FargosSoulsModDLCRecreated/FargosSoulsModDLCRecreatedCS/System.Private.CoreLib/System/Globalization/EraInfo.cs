using System;

namespace System.Globalization
{
	// Token: 0x02000204 RID: 516
	internal class EraInfo
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x0012C1A4 File Offset: 0x0012B3A4
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0012C1F0 File Offset: 0x0012B3F0
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x0400081D RID: 2077
		internal int era;

		// Token: 0x0400081E RID: 2078
		internal long ticks;

		// Token: 0x0400081F RID: 2079
		internal int yearOffset;

		// Token: 0x04000820 RID: 2080
		internal int minEraYear;

		// Token: 0x04000821 RID: 2081
		internal int maxEraYear;

		// Token: 0x04000822 RID: 2082
		internal string eraName;

		// Token: 0x04000823 RID: 2083
		internal string abbrevEraName;

		// Token: 0x04000824 RID: 2084
		internal string englishEraName;
	}
}
