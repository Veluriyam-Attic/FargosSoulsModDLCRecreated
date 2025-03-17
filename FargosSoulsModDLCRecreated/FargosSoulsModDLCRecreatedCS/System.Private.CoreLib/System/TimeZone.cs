using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000195 RID: 405
	[NullableContext(1)]
	[Nullable(0)]
	[Obsolete("System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.")]
	public abstract class TimeZone
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x000F3EB4 File Offset: 0x000F30B4
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, value, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x000F3EE0 File Offset: 0x000F30E0
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000F3F44 File Offset: 0x000F3144
		internal static void ResetTimeZone()
		{
			if (TimeZone.currentTimeZone != null)
			{
				object internalSyncObject = TimeZone.InternalSyncObject;
				lock (internalSyncObject)
				{
					TimeZone.currentTimeZone = null;
				}
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060018AA RID: 6314
		public abstract string StandardName { get; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060018AB RID: 6315
		public abstract string DaylightName { get; }

		// Token: 0x060018AC RID: 6316
		public abstract TimeSpan GetUtcOffset(DateTime time);

		// Token: 0x060018AD RID: 6317 RVA: 0x000F3F90 File Offset: 0x000F3190
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			long num = time.Ticks - this.GetUtcOffset(time).Ticks;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Utc);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Utc);
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000F3FF4 File Offset: 0x000F31F4
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool isAmbiguousDst = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref isAmbiguousDst);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x060018AF RID: 6319
		public abstract DaylightTime GetDaylightChanges(int year);

		// Token: 0x060018B0 RID: 6320 RVA: 0x000F4032 File Offset: 0x000F3232
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000F4047 File Offset: 0x000F3247
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000F405C File Offset: 0x000F325C
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			DateTimeKind kind = time.Kind;
			if (kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime t;
			DateTime t2;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				t = end - daylightTimes.Delta;
				t2 = end;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= t && time < t2)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x0400055B RID: 1371
		private static volatile TimeZone currentTimeZone;

		// Token: 0x0400055C RID: 1372
		private static object s_InternalSyncObject;
	}
}
