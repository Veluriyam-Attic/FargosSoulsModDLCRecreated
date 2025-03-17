using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000214 RID: 532
	public static class ISOWeek
	{
		// Token: 0x06002190 RID: 8592 RVA: 0x0012F870 File Offset: 0x0012EA70
		public static int GetWeekOfYear(DateTime date)
		{
			int weekNumber = ISOWeek.GetWeekNumber(date);
			if (weekNumber < 1)
			{
				return ISOWeek.GetWeeksInYear(date.Year - 1);
			}
			if (weekNumber > ISOWeek.GetWeeksInYear(date.Year))
			{
				return 1;
			}
			return weekNumber;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0012F8AC File Offset: 0x0012EAAC
		public static int GetYear(DateTime date)
		{
			int weekNumber = ISOWeek.GetWeekNumber(date);
			if (weekNumber < 1)
			{
				return date.Year - 1;
			}
			if (weekNumber > ISOWeek.GetWeeksInYear(date.Year))
			{
				return date.Year + 1;
			}
			return date.Year;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0012F8EE File Offset: 0x0012EAEE
		public static DateTime GetYearStart(int year)
		{
			return ISOWeek.ToDateTime(year, 1, DayOfWeek.Monday);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0012F8F8 File Offset: 0x0012EAF8
		public static DateTime GetYearEnd(int year)
		{
			return ISOWeek.ToDateTime(year, ISOWeek.GetWeeksInYear(year), DayOfWeek.Sunday);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0012F907 File Offset: 0x0012EB07
		public static int GetWeeksInYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.ArgumentOutOfRange_Year);
			}
			if (ISOWeek.<GetWeeksInYear>g__P|8_0(year) == 4 || ISOWeek.<GetWeeksInYear>g__P|8_0(year - 1) == 3)
			{
				return 53;
			}
			return 52;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0012F940 File Offset: 0x0012EB40
		public static DateTime ToDateTime(int year, int week, DayOfWeek dayOfWeek)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.ArgumentOutOfRange_Year);
			}
			if (week < 1 || week > 53)
			{
				throw new ArgumentOutOfRangeException("week", SR.ArgumentOutOfRange_Week_ISO);
			}
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > (DayOfWeek)7)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", SR.ArgumentOutOfRange_DayOfWeek);
			}
			DateTime dateTime = new DateTime(year, 1, 4);
			int num = ISOWeek.GetWeekday(dateTime.DayOfWeek) + 3;
			int num2 = week * 7 + ISOWeek.GetWeekday(dayOfWeek) - num;
			return new DateTime(year, 1, 1).AddDays((double)(num2 - 1));
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0012F9D4 File Offset: 0x0012EBD4
		private static int GetWeekNumber(DateTime date)
		{
			return (date.DayOfYear - ISOWeek.GetWeekday(date.DayOfWeek) + 10) / 7;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0012F9EF File Offset: 0x0012EBEF
		private static int GetWeekday(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek != DayOfWeek.Sunday)
			{
				return (int)dayOfWeek;
			}
			return 7;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0012F9F7 File Offset: 0x0012EBF7
		[CompilerGenerated]
		internal static int <GetWeeksInYear>g__P|8_0(int y)
		{
			return (y + y / 4 - y / 100 + y / 400) % 7;
		}
	}
}
