using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public readonly struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable, ISpanFormattable
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x000F3600 File Offset: 0x000F2800
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000F3609 File Offset: 0x000F2809
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000F3619 File Offset: 0x000F2819
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000F3628 File Offset: 0x000F2828
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, SR.Overflow_TimeSpanTooLong);
			}
			this._ticks = num * 10000L;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000F3695 File Offset: 0x000F2895
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x000F369D File Offset: 0x000F289D
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x000F36B0 File Offset: 0x000F28B0
		public int Hours
		{
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000F36C7 File Offset: 0x000F28C7
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks / 10000L % 1000L);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000F36DE File Offset: 0x000F28DE
		public int Minutes
		{
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000F36F2 File Offset: 0x000F28F2
		public int Seconds
		{
			get
			{
				return (int)(this._ticks / 10000000L % 60L);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x000F3706 File Offset: 0x000F2906
		public double TotalDays
		{
			get
			{
				return (double)this._ticks / 864000000000.0;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x000F3719 File Offset: 0x000F2919
		public double TotalHours
		{
			get
			{
				return (double)this._ticks / 36000000000.0;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000F372C File Offset: 0x000F292C
		public double TotalMilliseconds
		{
			get
			{
				double num = (double)this._ticks / 10000.0;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x000F3778 File Offset: 0x000F2978
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks / 600000000.0;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000F378B File Offset: 0x000F298B
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks / 10000000.0;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x000F37A0 File Offset: 0x000F29A0
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(SR.Overflow_TimeSpanTooLong);
			}
			return new TimeSpan(num);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000F37EF File Offset: 0x000F29EF
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000F3814 File Offset: 0x000F2A14
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException(SR.Arg_MustBeTimeSpan);
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000F385C File Offset: 0x000F2A5C
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000F3887 File Offset: 0x000F2A87
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 864000000000.0);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000F3898 File Offset: 0x000F2A98
		public TimeSpan Duration()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(SR.Overflow_Duration);
			}
			return new TimeSpan((this._ticks >= 0L) ? this._ticks : (-this._ticks));
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000F38D5 File Offset: 0x000F2AD5
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000F38F4 File Offset: 0x000F2AF4
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000F38F4 File Offset: 0x000F2AF4
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000F3904 File Offset: 0x000F2B04
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000F3918 File Offset: 0x000F2B18
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 36000000000.0);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000F392C File Offset: 0x000F2B2C
		private static TimeSpan Interval(double value, double scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException(SR.Arg_CannotBeNaN);
			}
			double ticks = value * scale;
			return TimeSpan.IntervalFromDoubleTicks(ticks);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000F3958 File Offset: 0x000F2B58
		private static TimeSpan IntervalFromDoubleTicks(double ticks)
		{
			if (ticks > 9.223372036854776E+18 || ticks < -9.223372036854776E+18 || double.IsNaN(ticks))
			{
				throw new OverflowException(SR.Overflow_TimeSpanTooLong);
			}
			if (ticks == 9.223372036854776E+18)
			{
				return TimeSpan.MaxValue;
			}
			return new TimeSpan((long)ticks);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000F39A9 File Offset: 0x000F2BA9
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 10000.0);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000F39BA File Offset: 0x000F2BBA
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 600000000.0);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000F39CB File Offset: 0x000F2BCB
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(SR.Overflow_NegateTwosCompNum);
			}
			return new TimeSpan(-this._ticks);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000F39F6 File Offset: 0x000F2BF6
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 10000000.0);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000F3A08 File Offset: 0x000F2C08
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(SR.Overflow_TimeSpanTooLong);
			}
			return new TimeSpan(num);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000F3A57 File Offset: 0x000F2C57
		public TimeSpan Multiply(double factor)
		{
			return this * factor;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000F3A65 File Offset: 0x000F2C65
		public TimeSpan Divide(double divisor)
		{
			return this / divisor;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000F3A73 File Offset: 0x000F2C73
		public double Divide(TimeSpan ts)
		{
			return this / ts;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000F3A81 File Offset: 0x000F2C81
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000F3A8C File Offset: 0x000F2C8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				ThrowHelper.ThrowArgumentOutOfRange_TimeSpanTooLong();
			}
			return num * 10000000L;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000F3AD2 File Offset: 0x000F2CD2
		private static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(SR.Argument_InvalidTimeSpanStyles, parameterName);
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000F3AE7 File Offset: 0x000F2CE7
		[NullableContext(1)]
		public static TimeSpan Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(s, null);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000F3AFF File Offset: 0x000F2CFF
		[NullableContext(1)]
		public static TimeSpan Parse(string input, [Nullable(2)] IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(input, formatProvider);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000F3B17 File Offset: 0x000F2D17
		public static TimeSpan Parse(ReadOnlySpan<char> input, [Nullable(2)] IFormatProvider formatProvider = null)
		{
			return TimeSpanParse.Parse(input, formatProvider);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000F3B20 File Offset: 0x000F2D20
		[NullableContext(1)]
		public static TimeSpan ParseExact(string input, string format, [Nullable(2)] IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000F3B49 File Offset: 0x000F2D49
		[NullableContext(1)]
		public static TimeSpan ParseExact(string input, string[] formats, [Nullable(2)] IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000F3B63 File Offset: 0x000F2D63
		[NullableContext(1)]
		public static TimeSpan ParseExact(string input, string format, [Nullable(2)] IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000F3B97 File Offset: 0x000F2D97
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000F3BAD File Offset: 0x000F2DAD
		[NullableContext(1)]
		public static TimeSpan ParseExact(string input, string[] formats, [Nullable(2)] IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000F3BD2 File Offset: 0x000F2DD2
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, [Nullable(1)] string[] formats, [Nullable(2)] IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000F3BE8 File Offset: 0x000F2DE8
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out TimeSpan result)
		{
			if (s == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(s, null, out result);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000F3C03 File Offset: 0x000F2E03
		public static bool TryParse(ReadOnlySpan<char> s, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(s, null, out result);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000F3C0D File Offset: 0x000F2E0D
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000F3C28 File Offset: 0x000F2E28
		public static bool TryParse(ReadOnlySpan<char> input, [Nullable(2)] IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000F3C32 File Offset: 0x000F2E32
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string format, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000F3C57 File Offset: 0x000F2E57
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000F3C63 File Offset: 0x000F2E63
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000F3C80 File Offset: 0x000F2E80
		[NullableContext(2)]
		public static bool TryParseExact([Nullable(0)] ReadOnlySpan<char> input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000F3C8C File Offset: 0x000F2E8C
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000F3CBE File Offset: 0x000F2EBE
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, [Nullable(2)] IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000F3CD6 File Offset: 0x000F2ED6
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000F3D00 File Offset: 0x000F2F00
		[NullableContext(2)]
		public static bool TryParseExact([Nullable(0)] ReadOnlySpan<char> input, [NotNullWhen(true)] string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000F3D18 File Offset: 0x000F2F18
		[NullableContext(1)]
		public override string ToString()
		{
			return TimeSpanFormat.FormatC(this);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000F3D25 File Offset: 0x000F2F25
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return TimeSpanFormat.Format(this, format, null);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000F3D34 File Offset: 0x000F2F34
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000F3D43 File Offset: 0x000F2F43
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider formatProvider = null)
		{
			return TimeSpanFormat.TryFormat(this, destination, out charsWritten, format, formatProvider);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000F3D55 File Offset: 0x000F2F55
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException(SR.Overflow_NegateTwosCompNum);
			}
			return new TimeSpan(-t._ticks);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000F3D80 File Offset: 0x000F2F80
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000AC098 File Offset: 0x000AB298
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000F3D8A File Offset: 0x000F2F8A
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000F3D94 File Offset: 0x000F2F94
		public static TimeSpan operator *(TimeSpan timeSpan, double factor)
		{
			if (double.IsNaN(factor))
			{
				throw new ArgumentException(SR.Arg_CannotBeNaN, "factor");
			}
			double ticks = Math.Round((double)timeSpan.Ticks * factor);
			return TimeSpan.IntervalFromDoubleTicks(ticks);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000F3DCF File Offset: 0x000F2FCF
		public static TimeSpan operator *(double factor, TimeSpan timeSpan)
		{
			return timeSpan * factor;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000F3DD8 File Offset: 0x000F2FD8
		public static TimeSpan operator /(TimeSpan timeSpan, double divisor)
		{
			if (double.IsNaN(divisor))
			{
				throw new ArgumentException(SR.Arg_CannotBeNaN, "divisor");
			}
			double ticks = Math.Round((double)timeSpan.Ticks / divisor);
			return TimeSpan.IntervalFromDoubleTicks(ticks);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000F3E13 File Offset: 0x000F3013
		public static double operator /(TimeSpan t1, TimeSpan t2)
		{
			return (double)t1.Ticks / (double)t2.Ticks;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000F38F4 File Offset: 0x000F2AF4
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000F3E26 File Offset: 0x000F3026
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000F3E39 File Offset: 0x000F3039
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000F3E49 File Offset: 0x000F3049
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000F3E5C File Offset: 0x000F305C
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000F3E6C File Offset: 0x000F306C
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x04000552 RID: 1362
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04000553 RID: 1363
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04000554 RID: 1364
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000555 RID: 1365
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000556 RID: 1366
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04000557 RID: 1367
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		// Token: 0x04000558 RID: 1368
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		// Token: 0x04000559 RID: 1369
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x0400055A RID: 1370
		internal readonly long _ticks;
	}
}
