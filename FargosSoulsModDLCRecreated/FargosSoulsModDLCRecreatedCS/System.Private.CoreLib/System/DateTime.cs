using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A5 RID: 165
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTime : IComparable, IFormattable, IConvertible, IComparable<DateTime>, IEquatable<DateTime>, ISerializable, ISpanFormattable
	{
		// Token: 0x060008AB RID: 2219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool ValidateSystemTime(Interop.Kernel32.SYSTEMTIME* time, bool localTime);

		// Token: 0x060008AC RID: 2220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool FileTimeToSystemTime(long fileTime, DateTime.FullSystemTime* time);

		// Token: 0x060008AD RID: 2221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetSystemTimeWithLeapSecondsHandling(DateTime.FullSystemTime* time);

		// Token: 0x060008AE RID: 2222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SystemTimeToFileTime(Interop.Kernel32.SYSTEMTIME* time, long* fileTime);

		// Token: 0x060008AF RID: 2223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetSystemTimeAsFileTime();

		// Token: 0x060008B0 RID: 2224 RVA: 0x000C5314 File Offset: 0x000C4514
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", SR.ArgumentOutOfRange_DateTimeBadTicks);
			}
			this._dateData = (ulong)ticks;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000C533E File Offset: 0x000C453E
		private DateTime(ulong dateData)
		{
			this._dateData = dateData;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000C5348 File Offset: 0x000C4548
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", SR.ArgumentOutOfRange_DateTimeBadTicks);
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeKind, "kind");
			}
			this._dateData = (ulong)(ticks | (long)kind << 62);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000C539C File Offset: 0x000C459C
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", SR.ArgumentOutOfRange_DateTimeBadTicks);
			}
			this._dateData = (ulong)(ticks | (isAmbiguousDst ? -4611686018427387904L : long.MinValue));
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000C53E9 File Offset: 0x000C45E9
		public DateTime(int year, int month, int day)
		{
			this._dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000C53F9 File Offset: 0x000C45F9
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000C5409 File Offset: 0x000C4609
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			this._dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000C5448 File Offset: 0x000C4648
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeKind, "kind");
			}
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this._dateData = (ulong)(num | (long)kind << 62);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000C54B8 File Offset: 0x000C46B8
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			this._dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this._dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
				}
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000C5554 File Offset: 0x000C4754
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Arg_DateTimeRange);
			}
			this._dateData = (ulong)num;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000C55FC File Offset: 0x000C47FC
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeKind, "kind");
			}
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Arg_DateTimeRange);
			}
			this._dateData = (ulong)(num | (long)kind << 62);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000C56C8 File Offset: 0x000C48C8
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Arg_DateTimeRange);
			}
			this._dateData = (ulong)num2;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this._dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
				}
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000C57C0 File Offset: 0x000C49C0
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeKind, "kind");
			}
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Arg_DateTimeRange);
			}
			this._dateData = (ulong)(num2 | (long)kind << 62);
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this._dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, kind))
				{
					throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
				}
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000C58DC File Offset: 0x000C4ADC
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long dateData = 0L;
			ulong dateData2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						dateData2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					dateData = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this._dateData = dateData2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException(SR.Serialization_MissingDateTimeData);
				}
				this._dateData = (ulong)dateData;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException(SR.Serialization_DateTimeTicksOutOfRange);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000C59AE File Offset: 0x000C4BAE
		internal long InternalTicks
		{
			get
			{
				return (long)(this._dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000C59C0 File Offset: 0x000C4BC0
		private ulong InternalKind
		{
			get
			{
				return this._dateData & 13835058055282163712UL;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000C59D2 File Offset: 0x000C4BD2
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000C59E0 File Offset: 0x000C4BE0
		private DateTime Add(double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5);
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_AddValue);
			}
			return this.AddTicks((long)num * 10000L);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000C5A4A File Offset: 0x000C4C4A
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000C5A58 File Offset: 0x000C4C58
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000C5A66 File Offset: 0x000C4C66
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000C5A70 File Offset: 0x000C4C70
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000C5A80 File Offset: 0x000C4C80
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", SR.ArgumentOutOfRange_DateTimeBadMonths);
			}
			int num;
			int num2;
			int num3;
			this.GetDate(out num, out num2, out num3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", SR.ArgumentOutOfRange_DateArithmetic);
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)(DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L | (long)this.InternalKind));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000C5B39 File Offset: 0x000C4D39
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000C5B48 File Offset: 0x000C4D48
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_DateArithmetic);
			}
			return new DateTime((ulong)(internalTicks + value | (long)this.InternalKind));
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000C5B90 File Offset: 0x000C4D90
		internal bool TryAddTicks(long value, out DateTime result)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				result = default(DateTime);
				return false;
			}
			result = new DateTime((ulong)(internalTicks + value | (long)this.InternalKind));
			return true;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000C5BD8 File Offset: 0x000C4DD8
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_DateTimeBadYears);
			}
			return this.AddMonths(value * 12);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000C5C04 File Offset: 0x000C4E04
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000C5C2E File Offset: 0x000C4E2E
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException(SR.Arg_MustBeDateTime);
			}
			return DateTime.Compare(this, (DateTime)value);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000C5C59 File Offset: 0x000C4E59
		public int CompareTo(DateTime value)
		{
			return DateTime.Compare(this, value);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000C5C68 File Offset: 0x000C4E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static long DateToTicks(int year, int month, int day)
		{
			if (year < 1 || year > 9999 || month < 1 || month > 12 || day < 1)
			{
				ThrowHelper.ThrowArgumentOutOfRange_BadYearMonthDay();
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			if (day > array[month] - array[month - 1])
			{
				ThrowHelper.ThrowArgumentOutOfRange_BadYearMonthDay();
			}
			int num = year - 1;
			int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
			return (long)num2 * 864000000000L;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000C5CEC File Offset: 0x000C4EEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 24 || minute >= 60 || second >= 60)
			{
				ThrowHelper.ThrowArgumentOutOfRange_BadHourMinuteSecond();
			}
			return TimeSpan.TimeToTicks(hour, minute, second);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000C5D0C File Offset: 0x000C4F0C
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", SR.ArgumentOutOfRange_Month);
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000C5D50 File Offset: 0x000C4F50
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(SR.Arg_OleAutDateInvalid);
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : -0.5));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException(SR.Arg_OleAutDateScale);
			}
			return num * 10000L;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000C5DF4 File Offset: 0x000C4FF4
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000C5E21 File Offset: 0x000C5021
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000C5E32 File Offset: 0x000C5032
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000C5E44 File Offset: 0x000C5044
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool isAmbiguousDst = false;
			long ticks;
			if (num < 0L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime time = new DateTime(num, DateTimeKind.Utc);
				bool flag;
				ticks = TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			}
			num += ticks;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Argument_DateTimeBadBinaryData, "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000C5F2C File Offset: 0x000C512C
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(SR.Argument_DateTimeBadBinaryData, "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000C5F6C File Offset: 0x000C516C
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000C5F88 File Offset: 0x000C5188
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", SR.ArgumentOutOfRange_FileTimeInvalid);
			}
			if (DateTime.s_systemSupportsLeapSeconds)
			{
				return DateTime.FromFileTimeLeapSecondsAware(fileTime);
			}
			long ticks = fileTime + 504911232000000000L;
			return new DateTime(ticks, DateTimeKind.Utc);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000C5FD7 File Offset: 0x000C51D7
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000C5FE5 File Offset: 0x000C51E5
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this._dateData);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000C6017 File Offset: 0x000C5217
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000C6035 File Offset: 0x000C5235
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000C6044 File Offset: 0x000C5244
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long ticks = this.Ticks;
				long num = ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this._dateData;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000C609C File Offset: 0x000C529C
		public DateTime Date
		{
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)(internalTicks - internalTicks % 864000000000L | (long)this.InternalKind));
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000C60CC File Offset: 0x000C52CC
		private int GetDatePart(int part)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000C61BC File Offset: 0x000C53BC
		internal void GetDate(out int year, out int month, out int day)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000C62A0 File Offset: 0x000C54A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void GetTime(out int hour, out int minute, out int second)
		{
			long num = this.InternalTicks / 10000000L;
			long num2;
			num = Math.DivRem(num, 60L, out num2);
			second = (int)num2;
			num = Math.DivRem(num, 60L, out num2);
			minute = (int)num2;
			hour = (int)(num % 24L);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000C62E4 File Offset: 0x000C54E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void GetTime(out int hour, out int minute, out int second, out int millisecond)
		{
			long num = this.InternalTicks / 10000L;
			long num2;
			num = Math.DivRem(num, 1000L, out num2);
			millisecond = (int)num2;
			num = Math.DivRem(num, 60L, out num2);
			second = (int)num2;
			num = Math.DivRem(num, 60L, out num2);
			minute = (int)num2;
			hour = (int)(num % 24L);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000C633C File Offset: 0x000C553C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void GetTimePrecise(out int hour, out int minute, out int second, out int tick)
		{
			long num2;
			long num = Math.DivRem(this.InternalTicks, 10000000L, out num2);
			tick = (int)num2;
			num = Math.DivRem(num, 60L, out num2);
			second = (int)num2;
			num = Math.DivRem(num, 60L, out num2);
			minute = (int)num2;
			hour = (int)(num % 24L);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000C638A File Offset: 0x000C558A
		public int Day
		{
			get
			{
				return this.GetDatePart(3);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000C6393 File Offset: 0x000C5593
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000C63AC File Offset: 0x000C55AC
		public int DayOfYear
		{
			get
			{
				return this.GetDatePart(1);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000C63B8 File Offset: 0x000C55B8
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000C63D4 File Offset: 0x000C55D4
		public int Hour
		{
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000C63EB File Offset: 0x000C55EB
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000C6400 File Offset: 0x000C5600
		public DateTimeKind Kind
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				ulong internalKind = this.InternalKind;
				DateTimeKind result;
				if (internalKind != 0UL)
				{
					if (internalKind != 4611686018427387904UL)
					{
						result = DateTimeKind.Local;
					}
					else
					{
						result = DateTimeKind.Utc;
					}
				}
				else
				{
					result = DateTimeKind.Unspecified;
				}
				return result;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000C6430 File Offset: 0x000C5630
		public int Millisecond
		{
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000C6447 File Offset: 0x000C5647
		public int Minute
		{
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000C645B File Offset: 0x000C565B
		public int Month
		{
			get
			{
				return this.GetDatePart(2);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000C6464 File Offset: 0x000C5664
		public static DateTime Now
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool isAmbiguousDst;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out isAmbiguousDst).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000C64C5 File Offset: 0x000C56C5
		public int Second
		{
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x000C64D9 File Offset: 0x000C56D9
		public long Ticks
		{
			get
			{
				return this.InternalTicks;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000C64E1 File Offset: 0x000C56E1
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000C64F8 File Offset: 0x000C56F8
		public static DateTime Today
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000C6512 File Offset: 0x000C5712
		public int Year
		{
			get
			{
				return this.GetDatePart(0);
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000C651B File Offset: 0x000C571B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				ThrowHelper.ThrowArgumentOutOfRange_Year();
			}
			return (year & 3) == 0 && ((year & 15) == 0 || year % 25 != 0);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000C6544 File Offset: 0x000C5744
		public static DateTime Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000C6561 File Offset: 0x000C5761
		public static DateTime Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000C657F File Offset: 0x000C577F
		public static DateTime Parse(string s, [Nullable(2)] IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000C65A8 File Offset: 0x000C57A8
		[NullableContext(0)]
		public static DateTime Parse(ReadOnlySpan<char> s, [Nullable(2)] IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000C65C2 File Offset: 0x000C57C2
		public static DateTime ParseExact(string s, string format, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000C65F0 File Offset: 0x000C57F0
		public static DateTime ParseExact(string s, string format, [Nullable(2)] IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000C6629 File Offset: 0x000C5829
		[NullableContext(0)]
		public static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000C6644 File Offset: 0x000C5844
		public static DateTime ParseExact(string s, string[] formats, [Nullable(2)] IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000C666E File Offset: 0x000C586E
		[NullableContext(0)]
		public static DateTime ParseExact(ReadOnlySpan<char> s, [Nullable(1)] string[] formats, [Nullable(2)] IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000C6689 File Offset: 0x000C5889
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000C66A0 File Offset: 0x000C58A0
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_DateArithmetic);
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)this.InternalKind));
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000C66EC File Offset: 0x000C58EC
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(SR.Arg_OleAutDateInvalid);
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000C6774 File Offset: 0x000C5974
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000C6784 File Offset: 0x000C5984
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000C67A0 File Offset: 0x000C59A0
		public long ToFileTimeUtc()
		{
			long num = ((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks;
			if (DateTime.s_systemSupportsLeapSeconds)
			{
				return DateTime.ToFileTimeLeapSecondsAware(num);
			}
			num -= 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_FileTimeInvalid);
			}
			return num;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000C6802 File Offset: 0x000C5A02
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000C680C File Offset: 0x000C5A0C
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag;
			bool isAmbiguousDst;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException(SR.Arg_ArgumentOutOfRangeException);
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException(SR.Arg_ArgumentOutOfRangeException);
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000C689E File Offset: 0x000C5A9E
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", null);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000C68B1 File Offset: 0x000C5AB1
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", null);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000C68C4 File Offset: 0x000C5AC4
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", null);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000C68D7 File Offset: 0x000C5AD7
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", null);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000C68EA File Offset: 0x000C5AEA
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, null);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000C68F9 File Offset: 0x000C5AF9
		public string ToString([Nullable(2)] string format)
		{
			return DateTimeFormat.Format(this, format, null);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000C6908 File Offset: 0x000C5B08
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, provider);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000C6917 File Offset: 0x000C5B17
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, provider);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000C6926 File Offset: 0x000C5B26
		[NullableContext(0)]
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return DateTimeFormat.TryFormat(this, destination, out charsWritten, format, provider);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000C6938 File Offset: 0x000C5B38
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000C6946 File Offset: 0x000C5B46
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out DateTime result)
		{
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000C6966 File Offset: 0x000C5B66
		[NullableContext(0)]
		public static bool TryParse(ReadOnlySpan<char> s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000C6975 File Offset: 0x000C5B75
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000C69A1 File Offset: 0x000C5BA1
		[NullableContext(0)]
		public static bool TryParse(ReadOnlySpan<char> s, [Nullable(2)] IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000C69BC File Offset: 0x000C5BBC
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string s, [NotNullWhen(true)] string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null || format == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000C69F3 File Offset: 0x000C5BF3
		[NullableContext(0)]
		public static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000C6A10 File Offset: 0x000C5C10
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string s, [NotNullWhen(true)] string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000C6A3F File Offset: 0x000C5C3F
		[NullableContext(2)]
		public static bool TryParseExact([Nullable(0)] ReadOnlySpan<char> s, [NotNullWhen(true)] string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000C6A5C File Offset: 0x000C5C5C
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", SR.ArgumentOutOfRange_DateArithmetic);
			}
			return new DateTime((ulong)(internalTicks + ticks | (long)d.InternalKind));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000C6AB0 File Offset: 0x000C5CB0
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", SR.ArgumentOutOfRange_DateArithmetic);
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)d.InternalKind));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000C6AFE File Offset: 0x000C5CFE
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000C5E32 File Offset: 0x000C5032
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000C6B14 File Offset: 0x000C5D14
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000C6B29 File Offset: 0x000C5D29
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000C6B3B File Offset: 0x000C5D3B
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000C6B50 File Offset: 0x000C5D50
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000C6B62 File Offset: 0x000C5D62
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000C6B77 File Offset: 0x000C5D77
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000C6B84 File Offset: 0x000C5D84
		public string[] GetDateTimeFormats([Nullable(2)] IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000C6B97 File Offset: 0x000C5D97
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000C6BA5 File Offset: 0x000C5DA5
		public string[] GetDateTimeFormats(char format, [Nullable(2)] IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000C6BB9 File Offset: 0x000C5DB9
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000C6BBD File Offset: 0x000C5DBD
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Boolean"));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000C6BD8 File Offset: 0x000C5DD8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Char"));
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000C6BF3 File Offset: 0x000C5DF3
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "SByte"));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000C6C0E File Offset: 0x000C5E0E
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Byte"));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000C6C29 File Offset: 0x000C5E29
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Int16"));
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000C6C44 File Offset: 0x000C5E44
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "UInt16"));
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000C6C5F File Offset: 0x000C5E5F
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Int32"));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000C6C7A File Offset: 0x000C5E7A
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "UInt32"));
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000C6C95 File Offset: 0x000C5E95
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Int64"));
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000C6CB0 File Offset: 0x000C5EB0
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "UInt64"));
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000C6CCB File Offset: 0x000C5ECB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Single"));
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000C6CE6 File Offset: 0x000C5EE6
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Double"));
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000C6D01 File Offset: 0x000C5F01
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "DateTime", "Decimal"));
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000C6D1C File Offset: 0x000C5F1C
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000C6D24 File Offset: 0x000C5F24
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000C6D38 File Offset: 0x000C5F38
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second > 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			if (second == 60)
			{
				if (!DateTime.s_systemSupportsLeapSeconds || !DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
				{
					return false;
				}
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000C6E28 File Offset: 0x000C6028
		private unsafe static bool SystemSupportsLeapSeconds()
		{
			Interop.NtDll.SYSTEM_LEAP_SECOND_INFORMATION system_LEAP_SECOND_INFORMATION;
			return Interop.NtDll.NtQuerySystemInformation(206, (void*)(&system_LEAP_SECOND_INFORMATION), (uint)sizeof(Interop.NtDll.SYSTEM_LEAP_SECOND_INFORMATION), null) == 0U && system_LEAP_SECOND_INFORMATION.Enabled > Interop.BOOLEAN.FALSE;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000C6E58 File Offset: 0x000C6058
		public unsafe static DateTime UtcNow
		{
			get
			{
				if (DateTime.s_systemSupportsLeapSeconds)
				{
					DateTime.FullSystemTime fullSystemTime;
					DateTime.GetSystemTimeWithLeapSecondsHandling(&fullSystemTime);
					return DateTime.CreateDateTimeFromSystemTime(fullSystemTime);
				}
				return new DateTime((ulong)(DateTime.GetSystemTimeAsFileTime() + 504911232000000000L | 4611686018427387904L));
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000C6E9C File Offset: 0x000C609C
		internal unsafe static bool IsValidTimeWithLeapSeconds(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			DateTime dateTime = new DateTime(year, month, day);
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(year, month, dateTime.DayOfWeek, day, hour, minute, second);
			bool result;
			if (kind != DateTimeKind.Utc)
			{
				if (kind == DateTimeKind.Local)
				{
					result = DateTime.ValidateSystemTime(&fullSystemTime.systemTime, true);
				}
				else
				{
					result = (DateTime.ValidateSystemTime(&fullSystemTime.systemTime, true) || DateTime.ValidateSystemTime(&fullSystemTime.systemTime, false));
				}
			}
			else
			{
				result = DateTime.ValidateSystemTime(&fullSystemTime.systemTime, false);
			}
			return result;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000C6F18 File Offset: 0x000C6118
		private unsafe static DateTime FromFileTimeLeapSecondsAware(long fileTime)
		{
			DateTime.FullSystemTime fullSystemTime;
			if (DateTime.FileTimeToSystemTime(fileTime, &fullSystemTime))
			{
				return DateTime.CreateDateTimeFromSystemTime(fullSystemTime);
			}
			throw new ArgumentOutOfRangeException("fileTime", SR.ArgumentOutOfRange_DateTimeBadTicks);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000C6F48 File Offset: 0x000C6148
		private unsafe static long ToFileTimeLeapSecondsAware(long ticks)
		{
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(ticks);
			long num;
			if (DateTime.SystemTimeToFileTime(&fullSystemTime.systemTime, &num))
			{
				return num + ticks % 10000L;
			}
			throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_FileTimeInvalid);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000C6F88 File Offset: 0x000C6188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static DateTime CreateDateTimeFromSystemTime(in DateTime.FullSystemTime time)
		{
			long num = DateTime.DateToTicks((int)time.systemTime.Year, (int)time.systemTime.Month, (int)time.systemTime.Day);
			num += DateTime.TimeToTicks((int)time.systemTime.Hour, (int)time.systemTime.Minute, (int)time.systemTime.Second);
			num += (long)((ulong)time.systemTime.Milliseconds * 10000UL);
			num += time.hundredNanoSecond;
			return new DateTime((ulong)(num | 4611686018427387904L));
		}

		// Token: 0x04000253 RID: 595
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};

		// Token: 0x04000254 RID: 596
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};

		// Token: 0x04000255 RID: 597
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		// Token: 0x04000256 RID: 598
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x04000257 RID: 599
		public static readonly DateTime UnixEpoch = new DateTime(621355968000000000L, DateTimeKind.Utc);

		// Token: 0x04000258 RID: 600
		private readonly ulong _dateData;

		// Token: 0x04000259 RID: 601
		internal static readonly bool s_systemSupportsLeapSeconds = DateTime.SystemSupportsLeapSeconds();

		// Token: 0x020000A6 RID: 166
		private struct FullSystemTime
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x000C7090 File Offset: 0x000C6290
			internal FullSystemTime(int year, int month, DayOfWeek dayOfWeek, int day, int hour, int minute, int second)
			{
				this.systemTime.Year = (ushort)year;
				this.systemTime.Month = (ushort)month;
				this.systemTime.DayOfWeek = (ushort)dayOfWeek;
				this.systemTime.Day = (ushort)day;
				this.systemTime.Hour = (ushort)hour;
				this.systemTime.Minute = (ushort)minute;
				this.systemTime.Second = (ushort)second;
				this.systemTime.Milliseconds = 0;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x000C7110 File Offset: 0x000C6310
			internal FullSystemTime(long ticks)
			{
				DateTime dateTime = new DateTime(ticks);
				int num;
				int num2;
				int num3;
				dateTime.GetDate(out num, out num2, out num3);
				int num4;
				int num5;
				int num6;
				int num7;
				dateTime.GetTime(out num4, out num5, out num6, out num7);
				this.systemTime.Year = (ushort)num;
				this.systemTime.Month = (ushort)num2;
				this.systemTime.DayOfWeek = (ushort)dateTime.DayOfWeek;
				this.systemTime.Day = (ushort)num3;
				this.systemTime.Hour = (ushort)num4;
				this.systemTime.Minute = (ushort)num5;
				this.systemTime.Second = (ushort)num6;
				this.systemTime.Milliseconds = (ushort)num7;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x0400025A RID: 602
			internal Interop.Kernel32.SYSTEMTIME systemTime;

			// Token: 0x0400025B RID: 603
			internal long hundredNanoSecond;
		}
	}
}
