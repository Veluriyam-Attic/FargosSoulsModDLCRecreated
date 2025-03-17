using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E5 RID: 229
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTimeOffset : IComparable, IFormattable, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>, ISerializable, IDeserializationCallback, ISpanFormattable
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x000CD45C File Offset: 0x000CC65C
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			DateTime dateTime = new DateTime(ticks);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000CD48C File Offset: 0x000CC68C
		public DateTimeOffset(DateTime dateTime)
		{
			TimeSpan localUtcOffset;
			if (dateTime.Kind != DateTimeKind.Utc)
			{
				localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			else
			{
				localUtcOffset = new TimeSpan(0L);
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(localUtcOffset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, localUtcOffset);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000CD4D0 File Offset: 0x000CC6D0
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
				{
					throw new ArgumentException(SR.Argument_OffsetLocalMismatch, "offset");
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException(SR.Argument_OffsetUtcMismatch, "offset");
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000CD548 File Offset: 0x000CC748
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this._dateTime.Year, this._dateTime.Month, this._dateTime.Day, this._dateTime.Hour, this._dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000CD5E4 File Offset: 0x000CC7E4
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this._dateTime.Year, this._dateTime.Month, this._dateTime.Day, this._dateTime.Hour, this._dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000CD684 File Offset: 0x000CC884
		[NullableContext(1)]
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			int num = second;
			if (second == 60 && DateTime.s_systemSupportsLeapSeconds)
			{
				second = 59;
			}
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
			if (num == 60 && !DateTime.IsValidTimeWithLeapSeconds(this._dateTime.Year, this._dateTime.Month, this._dateTime.Day, this._dateTime.Hour, this._dateTime.Minute, 60, DateTimeKind.Utc))
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000CD723 File Offset: 0x000CC923
		public static DateTimeOffset Now
		{
			get
			{
				return DateTimeOffset.ToLocalTime(DateTime.UtcNow, true);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000CD730 File Offset: 0x000CC930
		public static DateTimeOffset UtcNow
		{
			get
			{
				return new DateTimeOffset(DateTime.UtcNow);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000CD73C File Offset: 0x000CC93C
		public DateTime DateTime
		{
			get
			{
				return this.ClockDateTime;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000CD744 File Offset: 0x000CC944
		public DateTime UtcDateTime
		{
			get
			{
				return DateTime.SpecifyKind(this._dateTime, DateTimeKind.Utc);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000CD754 File Offset: 0x000CC954
		public DateTime LocalDateTime
		{
			get
			{
				return this.UtcDateTime.ToLocalTime();
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000CD770 File Offset: 0x000CC970
		public DateTimeOffset ToOffset(TimeSpan offset)
		{
			return new DateTimeOffset((this._dateTime + offset).Ticks, offset);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000CD798 File Offset: 0x000CC998
		private DateTime ClockDateTime
		{
			get
			{
				return new DateTime((this._dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x000CD7C4 File Offset: 0x000CC9C4
		public DateTime Date
		{
			get
			{
				return this.ClockDateTime.Date;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x000CD7E0 File Offset: 0x000CC9E0
		public int Day
		{
			get
			{
				return this.ClockDateTime.Day;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000CD7FC File Offset: 0x000CC9FC
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.ClockDateTime.DayOfWeek;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000CD818 File Offset: 0x000CCA18
		public int DayOfYear
		{
			get
			{
				return this.ClockDateTime.DayOfYear;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000CD834 File Offset: 0x000CCA34
		public int Hour
		{
			get
			{
				return this.ClockDateTime.Hour;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000CD850 File Offset: 0x000CCA50
		public int Millisecond
		{
			get
			{
				return this.ClockDateTime.Millisecond;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000CD86C File Offset: 0x000CCA6C
		public int Minute
		{
			get
			{
				return this.ClockDateTime.Minute;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000CD888 File Offset: 0x000CCA88
		public int Month
		{
			get
			{
				return this.ClockDateTime.Month;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000CD8A3 File Offset: 0x000CCAA3
		public TimeSpan Offset
		{
			get
			{
				return new TimeSpan(0, (int)this._offsetMinutes, 0);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000CD8B4 File Offset: 0x000CCAB4
		public int Second
		{
			get
			{
				return this.ClockDateTime.Second;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000CD8D0 File Offset: 0x000CCAD0
		public long Ticks
		{
			get
			{
				return this.ClockDateTime.Ticks;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000CD8EC File Offset: 0x000CCAEC
		public long UtcTicks
		{
			get
			{
				return this.UtcDateTime.Ticks;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000CD908 File Offset: 0x000CCB08
		public TimeSpan TimeOfDay
		{
			get
			{
				return this.ClockDateTime.TimeOfDay;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000CD924 File Offset: 0x000CCB24
		public int Year
		{
			get
			{
				return this.ClockDateTime.Year;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000CD940 File Offset: 0x000CCB40
		public DateTimeOffset Add(TimeSpan timeSpan)
		{
			return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000CD968 File Offset: 0x000CCB68
		public DateTimeOffset AddDays(double days)
		{
			return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000CD990 File Offset: 0x000CCB90
		public DateTimeOffset AddHours(double hours)
		{
			return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000CD9B8 File Offset: 0x000CCBB8
		public DateTimeOffset AddMilliseconds(double milliseconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000CD9E0 File Offset: 0x000CCBE0
		public DateTimeOffset AddMinutes(double minutes)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000CDA08 File Offset: 0x000CCC08
		public DateTimeOffset AddMonths(int months)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000CDA30 File Offset: 0x000CCC30
		public DateTimeOffset AddSeconds(double seconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000CDA58 File Offset: 0x000CCC58
		public DateTimeOffset AddTicks(long ticks)
		{
			return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000CDA80 File Offset: 0x000CCC80
		public DateTimeOffset AddYears(int years)
		{
			return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000CDAA7 File Offset: 0x000CCCA7
		public static int Compare(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000CDABC File Offset: 0x000CCCBC
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is DateTimeOffset))
			{
				throw new ArgumentException(SR.Arg_MustBeDateTimeOffset);
			}
			DateTime utcDateTime = ((DateTimeOffset)obj).UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000CDB10 File Offset: 0x000CCD10
		public int CompareTo(DateTimeOffset other)
		{
			DateTime utcDateTime = other.UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000CDB44 File Offset: 0x000CCD44
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000CDB78 File Offset: 0x000CCD78
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime.Equals(other.UtcDateTime);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000CDB9C File Offset: 0x000CCD9C
		public bool EqualsExact(DateTimeOffset other)
		{
			return this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset && this.ClockDateTime.Kind == other.ClockDateTime.Kind;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000CDBF2 File Offset: 0x000CCDF2
		public static bool Equals(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000CDC07 File Offset: 0x000CCE07
		public static DateTimeOffset FromFileTime(long fileTime)
		{
			return DateTimeOffset.ToLocalTime(DateTime.FromFileTimeUtc(fileTime), true);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000CDC18 File Offset: 0x000CCE18
		public static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			if (seconds < -62135596800L || seconds > 253402300799L)
			{
				throw new ArgumentOutOfRangeException("seconds", SR.Format(SR.ArgumentOutOfRange_Range, -62135596800L, 253402300799L));
			}
			long ticks = seconds * 10000000L + 621355968000000000L;
			return new DateTimeOffset(ticks, TimeSpan.Zero);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000CDC8C File Offset: 0x000CCE8C
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", SR.Format(SR.ArgumentOutOfRange_Range, -62135596800000L, 253402300799999L));
			}
			long ticks = milliseconds * 10000L + 621355968000000000L;
			return new DateTimeOffset(ticks, TimeSpan.Zero);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000CDD00 File Offset: 0x000CCF00
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				DateTimeOffset.ValidateOffset(this.Offset);
				DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
			}
			catch (ArgumentException innerException)
			{
				throw new SerializationException(SR.Serialization_InvalidData, innerException);
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000CDD4C File Offset: 0x000CCF4C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("DateTime", this._dateTime);
			info.AddValue("OffsetMinutes", this._offsetMinutes);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000CDD80 File Offset: 0x000CCF80
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			this._offsetMinutes = (short)info.GetValue("OffsetMinutes", typeof(short));
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000CDDDC File Offset: 0x000CCFDC
		public override int GetHashCode()
		{
			return this.UtcDateTime.GetHashCode();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000CDDF8 File Offset: 0x000CCFF8
		[NullableContext(1)]
		public static DateTimeOffset Parse(string input)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out offset).Ticks, offset);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000CDE30 File Offset: 0x000CD030
		[NullableContext(1)]
		public static DateTimeOffset Parse(string input, [Nullable(2)] IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000CDE44 File Offset: 0x000CD044
		[NullableContext(1)]
		public static DateTimeOffset Parse(string input, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000CDE8C File Offset: 0x000CD08C
		public static DateTimeOffset Parse(ReadOnlySpan<char> input, [Nullable(2)] IFormatProvider formatProvider = null, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000CDEC3 File Offset: 0x000CD0C3
		[NullableContext(1)]
		public static DateTimeOffset ParseExact(string input, string format, [Nullable(2)] IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000CDEE4 File Offset: 0x000CD0E4
		[NullableContext(1)]
		public static DateTimeOffset ParseExact(string input, string format, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000CDF3C File Offset: 0x000CD13C
		public static DateTimeOffset ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000CDF74 File Offset: 0x000CD174
		[NullableContext(1)]
		public static DateTimeOffset ParseExact(string input, string[] formats, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000CDFBC File Offset: 0x000CD1BC
		public static DateTimeOffset ParseExact(ReadOnlySpan<char> input, [Nullable(1)] string[] formats, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000CDFF4 File Offset: 0x000CD1F4
		public TimeSpan Subtract(DateTimeOffset value)
		{
			return this.UtcDateTime.Subtract(value.UtcDateTime);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000CE018 File Offset: 0x000CD218
		public DateTimeOffset Subtract(TimeSpan value)
		{
			return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000CE040 File Offset: 0x000CD240
		public long ToFileTime()
		{
			return this.UtcDateTime.ToFileTime();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000CE05C File Offset: 0x000CD25C
		public long ToUnixTimeSeconds()
		{
			long num = this.UtcDateTime.Ticks / 10000000L;
			return num - 62135596800L;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000CE08C File Offset: 0x000CD28C
		public long ToUnixTimeMilliseconds()
		{
			long num = this.UtcDateTime.Ticks / 10000L;
			return num - 62135596800000L;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000CE0BA File Offset: 0x000CD2BA
		public DateTimeOffset ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000CE0C3 File Offset: 0x000CD2C3
		internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
		{
			return DateTimeOffset.ToLocalTime(this.UtcDateTime, throwOnOverflow);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000CE0D4 File Offset: 0x000CD2D4
		private static DateTimeOffset ToLocalTime(DateTime utcDateTime, bool throwOnOverflow)
		{
			TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(utcDateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			long num = utcDateTime.Ticks + localUtcOffset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException(SR.Arg_ArgumentOutOfRangeException);
				}
				num = ((num < 0L) ? 0L : 3155378975999999999L);
			}
			return new DateTimeOffset(num, localUtcOffset);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000CE132 File Offset: 0x000CD332
		[NullableContext(1)]
		public override string ToString()
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, null, this.Offset);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000CE147 File Offset: 0x000CD347
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, null, this.Offset);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000CE15C File Offset: 0x000CD35C
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, formatProvider, this.Offset);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000CE171 File Offset: 0x000CD371
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, formatProvider, this.Offset);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000CE186 File Offset: 0x000CD386
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider formatProvider = null)
		{
			return DateTimeFormat.TryFormat(this.ClockDateTime, destination, out charsWritten, format, formatProvider, this.Offset);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000CE19E File Offset: 0x000CD39E
		public DateTimeOffset ToUniversalTime()
		{
			return new DateTimeOffset(this.UtcDateTime);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000CE1AC File Offset: 0x000CD3AC
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000CE1E4 File Offset: 0x000CD3E4
		public static bool TryParse(ReadOnlySpan<char> input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000CE218 File Offset: 0x000CD418
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000CE26C File Offset: 0x000CD46C
		public static bool TryParse(ReadOnlySpan<char> input, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000CE2AC File Offset: 0x000CD4AC
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000CE308 File Offset: 0x000CD508
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000CE34C File Offset: 0x000CD54C
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000CE3A0 File Offset: 0x000CD5A0
		[NullableContext(2)]
		public static bool TryParseExact([Nullable(0)] ReadOnlySpan<char> input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000CE3E4 File Offset: 0x000CD5E4
		private static short ValidateOffset(TimeSpan offset)
		{
			long ticks = offset.Ticks;
			if (ticks % 600000000L != 0L)
			{
				throw new ArgumentException(SR.Argument_OffsetPrecision, "offset");
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.Argument_OffsetOutOfRange);
			}
			return (short)(offset.Ticks / 600000000L);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000CE44C File Offset: 0x000CD64C
		private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
		{
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.Argument_UTCOutOfRange);
			}
			return new DateTime(num, DateTimeKind.Unspecified);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000CE494 File Offset: 0x000CD694
		private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeStyles, parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_ConflictingDateTimeStyles, parameterName);
			}
			if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_DateTimeOffsetInvalidDateTimeStyles, parameterName);
			}
			style &= ~DateTimeStyles.RoundtripKind;
			style &= ~DateTimeStyles.AssumeLocal;
			return style;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000CE4EF File Offset: 0x000CD6EF
		public static implicit operator DateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000CE4F7 File Offset: 0x000CD6F7
		public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000CE512 File Offset: 0x000CD712
		public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000CE52D File Offset: 0x000CD72D
		public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime - right.UtcDateTime;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000CE542 File Offset: 0x000CD742
		public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime == right.UtcDateTime;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000CE557 File Offset: 0x000CD757
		public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime != right.UtcDateTime;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000CE56C File Offset: 0x000CD76C
		public static bool operator <(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime < right.UtcDateTime;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000CE581 File Offset: 0x000CD781
		public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime <= right.UtcDateTime;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000CE596 File Offset: 0x000CD796
		public static bool operator >(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime > right.UtcDateTime;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000CE5AB File Offset: 0x000CD7AB
		public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime >= right.UtcDateTime;
		}

		// Token: 0x040002BF RID: 703
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);

		// Token: 0x040002C0 RID: 704
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);

		// Token: 0x040002C1 RID: 705
		public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(621355968000000000L, TimeSpan.Zero);

		// Token: 0x040002C2 RID: 706
		private readonly DateTime _dateTime;

		// Token: 0x040002C3 RID: 707
		private readonly short _offsetMinutes;
	}
}
