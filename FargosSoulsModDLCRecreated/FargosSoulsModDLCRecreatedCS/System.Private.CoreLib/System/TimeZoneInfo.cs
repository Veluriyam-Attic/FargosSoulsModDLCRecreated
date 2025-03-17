using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Internal.Win32;

namespace System
{
	// Token: 0x02000196 RID: 406
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000F413A File Offset: 0x000F333A
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000F4142 File Offset: 0x000F3342
		public string DisplayName
		{
			get
			{
				return this._displayName ?? string.Empty;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000F4153 File Offset: 0x000F3353
		public string StandardName
		{
			get
			{
				return this._standardDisplayName ?? string.Empty;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000F4164 File Offset: 0x000F3364
		public string DaylightName
		{
			get
			{
				return this._daylightDisplayName ?? string.Empty;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000F4175 File Offset: 0x000F3375
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this._baseUtcOffset;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000F417D File Offset: 0x000F337D
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return this._supportsDaylightSavingTime;
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000F4188 File Offset: 0x000F3388
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(SR.Argument_DateTimeOffsetIsNotAmbiguous, "dateTimeOffset");
			}
			DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
			bool flag = false;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime, out ruleIndex);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForAmbiguousOffsets, ruleIndex);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(SR.Argument_DateTimeOffsetIsNotAmbiguous, "dateTimeOffset");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000F4274 File Offset: 0x000F3474
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(SR.Argument_DateTimeIsNotAmbiguous, "dateTime");
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, TimeZoneInfoOptions.None, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			bool flag = false;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime2, out ruleIndex);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForAmbiguousOffsets, ruleIndex);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(SR.Argument_DateTimeIsNotAmbiguous, "dateTime");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000F4398 File Offset: 0x000F3598
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForAmbiguousOffsets(DateTime adjustedTime, out int? ruleIndex)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(adjustedTime, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.NoDaylightTransitions && !adjustmentRuleForTime.HasDaylightSaving)
			{
				return this.GetPreviousAdjustmentRule(adjustmentRuleForTime, ruleIndex);
			}
			return adjustmentRuleForTime;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000F43D0 File Offset: 0x000F35D0
		private TimeZoneInfo.AdjustmentRule GetPreviousAdjustmentRule(TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (ruleIndex != null && 0 < ruleIndex.GetValueOrDefault() && ruleIndex.GetValueOrDefault() < this._adjustmentRules.Length)
			{
				return this._adjustmentRules[ruleIndex.GetValueOrDefault() - 1];
			}
			TimeZoneInfo.AdjustmentRule result = rule;
			for (int i = 1; i < this._adjustmentRules.Length; i++)
			{
				if (rule == this._adjustmentRules[i])
				{
					result = this._adjustmentRules[i - 1];
					break;
				}
			}
			return result;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000F4440 File Offset: 0x000F3640
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000F444F File Offset: 0x000F364F
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000F4460 File Offset: 0x000F3660
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000F4481 File Offset: 0x000F3681
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000F4490 File Offset: 0x000F3690
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					DateTime time = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags);
					return TimeZoneInfo.GetUtcOffsetFromUtc(time, this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this._baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000F44F8 File Offset: 0x000F36F8
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			return this._supportsDaylightSavingTime && this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000F4524 File Offset: 0x000F3724
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000F4530 File Offset: 0x000F3730
		internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (!this._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			DateTime dateTime2 = (dateTime.Kind == DateTimeKind.Local) ? TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData) : ((dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, flags, cachedData) : dateTime);
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, ruleIndex);
				return TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000F45B8 File Offset: 0x000F37B8
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			bool result;
			TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out result);
			return result;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000F45D6 File Offset: 0x000F37D6
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000F45E5 File Offset: 0x000F37E5
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000F45F4 File Offset: 0x000F37F4
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this._supportsDaylightSavingTime || this._adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool result;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out result);
				return result;
			}
			else
			{
				dateTime2 = dateTime;
			}
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, ruleIndex);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000F4684 File Offset: 0x000F3884
		public bool IsInvalidTime(DateTime dateTime)
		{
			bool result = false;
			if (dateTime.Kind == DateTimeKind.Unspecified || (dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local))
			{
				int? ruleIndex;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime, out ruleIndex);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, ruleIndex);
					result = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000F46E7 File Offset: 0x000F38E7
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000F46F3 File Offset: 0x000F38F3
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000F4701 File Offset: 0x000F3901
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000F4710 File Offset: 0x000F3910
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			if (dateTime.Kind == DateTimeKind.Local && string.Equals(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase))
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
			}
			if (dateTime.Kind == DateTimeKind.Utc && string.Equals(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
			}
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000F479C File Offset: 0x000F399C
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
			long num = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
			if (num > DateTimeOffset.MaxValue.Ticks)
			{
				return DateTimeOffset.MaxValue;
			}
			if (num >= DateTimeOffset.MinValue.Ticks)
			{
				return new DateTimeOffset(num, utcOffsetFromUtc);
			}
			return DateTimeOffset.MinValue;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000F4804 File Offset: 0x000F3A04
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Ticks == 0L)
			{
				TimeZoneInfo.ClearCachedData();
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo sourceTimeZone = (dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.s_utcTimeZone : cachedData.Local;
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000F4855 File Offset: 0x000F3A55
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000F4865 File Offset: 0x000F3A65
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000F4878 File Offset: 0x000F3A78
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException(SR.Argument_ConvertMismatch, "sourceTimeZone");
			}
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime, out ruleIndex);
			TimeSpan t = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				t += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = sourceTimeZone.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, ruleIndex);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException(SR.Argument_DateTimeIsInvalid, "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime);
					t += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			long ticks = dateTime.Ticks - t.Ticks;
			bool isAmbiguousDst;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(ticks, destinationTimeZone, out isAmbiguousDst);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, isAmbiguousDst);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000F49AF File Offset: 0x000F3BAF
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000F49C4 File Offset: 0x000F3BC4
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000F49F8 File Offset: 0x000F3BF8
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags, cachedData);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000F4A2A File Offset: 0x000F3C2A
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000F4A3E File Offset: 0x000F3C3E
		[NullableContext(2)]
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Equals(this._id, other._id, StringComparison.OrdinalIgnoreCase) && this.HasSameRules(other);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000F4A60 File Offset: 0x000F3C60
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeZoneInfo);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000F4A6E File Offset: 0x000F3C6E
		public static TimeZoneInfo FromSerializedString(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidSerializedString, source), "source");
			}
			return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000F4AA2 File Offset: 0x000F3CA2
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._id);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000F4AB4 File Offset: 0x000F3CB4
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			lock (obj)
			{
				if (cachedData._readOnlySystemTimeZones == null)
				{
					TimeZoneInfo.PopulateAllSystemTimeZones(cachedData);
					cachedData._allSystemTimeZonesRead = true;
					List<TimeZoneInfo> list;
					if (cachedData._systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData._systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
					{
						int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
						if (num != 0)
						{
							return num;
						}
						return string.CompareOrdinal(x.DisplayName, y.DisplayName);
					});
					cachedData._readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData._readOnlySystemTimeZones;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000F4B5C File Offset: 0x000F3D5C
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._baseUtcOffset != other._baseUtcOffset || this._supportsDaylightSavingTime != other._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this._adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other._adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000F4BEC File Offset: 0x000F3DEC
		public static TimeZoneInfo Local
		{
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000F4BF8 File Offset: 0x000F3DF8
		public string ToSerializedString()
		{
			return TimeZoneInfo.StringSerializer.GetSerializedString(this);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000F4C00 File Offset: 0x000F3E00
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x000F4C08 File Offset: 0x000F3E08
		public static TimeZoneInfo Utc
		{
			get
			{
				return TimeZoneInfo.s_utcTimeZone;
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000F4C10 File Offset: 0x000F3E10
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			this._id = id;
			this._baseUtcOffset = baseUtcOffset;
			this._displayName = displayName;
			this._standardDisplayName = standardDisplayName;
			this._daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this._supportsDaylightSavingTime = (flag && !disableDaylightSavingTime);
			this._adjustmentRules = adjustmentRules;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000F4C73 File Offset: 0x000F3E73
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, [Nullable(2)] string displayName, [Nullable(2)] string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000F4C81 File Offset: 0x000F3E81
		[NullableContext(2)]
		[return: Nullable(1)]
		public static TimeZoneInfo CreateCustomTimeZone([Nullable(1)] string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, [Nullable(new byte[]
		{
			2,
			1
		})] TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000F4C91 File Offset: 0x000F3E91
		[NullableContext(2)]
		[return: Nullable(1)]
		public static TimeZoneInfo CreateCustomTimeZone([Nullable(1)] string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, [Nullable(new byte[]
		{
			2,
			1
		})] TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRules = (TimeZoneInfo.AdjustmentRule[])adjustmentRules.Clone();
			}
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000F4CC0 File Offset: 0x000F3EC0
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out flag);
				if (flag != this._supportsDaylightSavingTime)
				{
					throw new SerializationException(SR.Format(SR.Serialization_CorruptField, "SupportsDaylightSavingTime"));
				}
			}
			catch (ArgumentException innerException)
			{
				throw new SerializationException(SR.Serialization_InvalidData, innerException);
			}
			catch (InvalidTimeZoneException innerException2)
			{
				throw new SerializationException(SR.Serialization_InvalidData, innerException2);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000F4D3C File Offset: 0x000F3F3C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this._id);
			info.AddValue("DisplayName", this._displayName);
			info.AddValue("StandardName", this._standardDisplayName);
			info.AddValue("DaylightName", this._daylightDisplayName);
			info.AddValue("BaseUtcOffset", this._baseUtcOffset);
			info.AddValue("AdjustmentRules", this._adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this._supportsDaylightSavingTime);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000F4DD4 File Offset: 0x000F3FD4
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._id = (string)info.GetValue("Id", typeof(string));
			this._displayName = (string)info.GetValue("DisplayName", typeof(string));
			this._standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this._daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this._baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this._adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this._supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000F4ED8 File Offset: 0x000F40D8
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, out int? ruleIndex)
		{
			return this.GetAdjustmentRuleForTime(dateTime, false, out ruleIndex);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000F4EF0 File Offset: 0x000F40F0
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, bool dateTimeisUtc, out int? ruleIndex)
		{
			if (this._adjustmentRules == null || this._adjustmentRules.Length == 0)
			{
				ruleIndex = null;
				return null;
			}
			DateTime dateOnly = dateTimeisUtc ? (dateTime + this.BaseUtcOffset).Date : dateTime.Date;
			int i = 0;
			int num = this._adjustmentRules.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				TimeZoneInfo.AdjustmentRule adjustmentRule = this._adjustmentRules[num2];
				TimeZoneInfo.AdjustmentRule previousRule = (num2 > 0) ? this._adjustmentRules[num2 - 1] : adjustmentRule;
				int num3 = this.CompareAdjustmentRuleToDateTime(adjustmentRule, previousRule, dateTime, dateOnly, dateTimeisUtc);
				if (num3 == 0)
				{
					ruleIndex = new int?(num2);
					return adjustmentRule;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			ruleIndex = null;
			return null;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000F4FB4 File Offset: 0x000F41B4
		private int CompareAdjustmentRuleToDateTime(TimeZoneInfo.AdjustmentRule rule, TimeZoneInfo.AdjustmentRule previousRule, DateTime dateTime, DateTime dateOnly, bool dateTimeisUtc)
		{
			bool flag;
			if (rule.DateStart.Kind == DateTimeKind.Utc)
			{
				DateTime t = dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, previousRule.DaylightDelta, previousRule.BaseUtcOffsetDelta);
				flag = (t >= rule.DateStart);
			}
			else
			{
				flag = (dateOnly >= rule.DateStart);
			}
			if (!flag)
			{
				return 1;
			}
			bool flag2;
			if (rule.DateEnd.Kind == DateTimeKind.Utc)
			{
				DateTime t2 = dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, rule.DaylightDelta, rule.BaseUtcOffsetDelta);
				flag2 = (t2 <= rule.DateEnd);
			}
			else
			{
				flag2 = (dateOnly <= rule.DateEnd);
			}
			if (!flag2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000F5060 File Offset: 0x000F4260
		private DateTime ConvertToUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, true);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000F506C File Offset: 0x000F426C
		private DateTime ConvertFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, false);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000F5078 File Offset: 0x000F4278
		private DateTime ConvertToFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta, bool convertToUtc)
		{
			TimeSpan timeSpan = this.BaseUtcOffset + daylightDelta + baseUtcOffsetDelta;
			if (convertToUtc)
			{
				timeSpan = timeSpan.Negate();
			}
			long num = dateTime.Ticks + timeSpan.Ticks;
			if (num > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (num >= DateTime.MinValue.Ticks)
			{
				return new DateTime(num);
			}
			return DateTime.MinValue;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000F50E0 File Offset: 0x000F42E0
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			DateTime time = (ticks > DateTime.MaxValue.Ticks) ? DateTime.MaxValue : ((ticks < DateTime.MinValue.Ticks) ? DateTime.MinValue : new DateTime(ticks));
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc(time, destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (ticks >= DateTime.MinValue.Ticks)
			{
				return new DateTime(ticks);
			}
			return DateTime.MinValue;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000F515C File Offset: 0x000F435C
		private DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime start;
			DateTime end;
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				start = this.ConvertFromUtc(rule.DateStart, previousAdjustmentRule.DaylightDelta, previousAdjustmentRule.BaseUtcOffsetDelta);
				end = this.ConvertFromUtc(rule.DateEnd, rule.DaylightDelta, rule.BaseUtcOffsetDelta);
			}
			else
			{
				start = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
				end = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			}
			return new DaylightTimeStruct(start, end, daylightDelta);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000F51D8 File Offset: 0x000F43D8
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime startTime;
			DateTime endTime;
			if (time.Kind == DateTimeKind.Local)
			{
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(startTime, time, endTime, false, rule);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000F5328 File Offset: 0x000F4528
		private TimeSpan GetDaylightSavingsStartOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				return baseUtcOffset + previousAdjustmentRule.BaseUtcOffsetDelta + previousAdjustmentRule.DaylightDelta;
			}
			return baseUtcOffset + rule.BaseUtcOffsetDelta;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000F536A File Offset: 0x000F456A
		private TimeSpan GetDaylightSavingsEndOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule)
		{
			return baseUtcOffset + rule.BaseUtcOffsetDelta + rule.DaylightDelta;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000F5384 File Offset: 0x000F4584
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			DaylightTimeStruct daylightTime = zone.GetDaylightTime(year, rule, ruleIndex);
			bool ignoreYearAdjustment = false;
			TimeSpan daylightSavingsStartOffsetFromUtc = zone.GetDaylightSavingsStartOffsetFromUtc(utc, rule, ruleIndex);
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				int? ruleIndex2;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31), out ruleIndex2);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					DaylightTimeStruct daylightTime2 = zone.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime, ruleIndex2);
					dateTime = daylightTime2.Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - daylightSavingsStartOffsetFromUtc;
				}
			}
			else
			{
				dateTime = daylightTime.Start - daylightSavingsStartOffsetFromUtc;
			}
			TimeSpan daylightSavingsEndOffsetFromUtc = zone.GetDaylightSavingsEndOffsetFromUtc(utc, rule);
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				int? ruleIndex3;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1), out ruleIndex3);
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						DaylightTimeStruct daylightTime3 = zone.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2, ruleIndex3);
						dateTime2 = daylightTime3.End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - daylightSavingsEndOffsetFromUtc;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - daylightSavingsEndOffsetFromUtc;
			}
			DateTime t;
			DateTime t2;
			if (daylightTime.Delta.Ticks > 0L)
			{
				t = dateTime2 - daylightTime.Delta;
				t2 = dateTime2;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightTime.Delta;
			}
			bool flag = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, ignoreYearAdjustment, rule);
			if (flag)
			{
				isAmbiguousLocalDst = (time >= t && time < t2);
				if (!isAmbiguousLocalDst && t.Year != t2.Year)
				{
					try
					{
						DateTime t3 = t.AddYears(1);
						DateTime t4 = t2.AddYears(1);
						isAmbiguousLocalDst = (time >= t3 && time < t4);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							DateTime t3 = t.AddYears(-1);
							DateTime t4 = t2.AddYears(-1);
							isAmbiguousLocalDst = (time >= t3 && time < t4);
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000F5694 File Offset: 0x000F4894
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment, TimeZoneInfo.AdjustmentRule rule)
		{
			if (!ignoreYearAdjustment && !rule.NoDaylightTransitions)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			if (startTime > endTime)
			{
				return time < endTime || time >= startTime;
			}
			if (rule.NoDaylightTransitions)
			{
				return time >= startTime && time <= endTime;
			}
			return time >= startTime && time < endTime;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000F5730 File Offset: 0x000F4930
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t2 && time < t);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t4 && time < t3);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t4 && time < t3);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000F5854 File Offset: 0x000F4A54
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t && time < t2);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t3 && time < t4);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t3 && time < t4);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000F5978 File Offset: 0x000F4B78
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, out ruleIndex);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = zone.GetDaylightTime(time.Year, adjustmentRuleForTime, ruleIndex);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000F59E4 File Offset: 0x000F4BE4
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000F59FC File Offset: 0x000F4BFC
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000F5A14 File Offset: 0x000F4C14
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime;
			int year;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MaxValue, out ruleIndex);
				year = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MinValue, out ruleIndex);
				year = 1;
			}
			else
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, true, out ruleIndex);
				year = (time + timeSpan).Year;
			}
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, zone._baseUtcOffset, adjustmentRuleForTime, ruleIndex, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000F5AD0 File Offset: 0x000F4CD0
		internal static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			TimeSpan timeOfDay = transitionTime.TimeOfDay.TimeOfDay;
			DateTime result;
			if (transitionTime.IsFixedDateRule)
			{
				int num = transitionTime.Day;
				if (num > 28)
				{
					int num2 = DateTime.DaysInMonth(year, transitionTime.Month);
					if (num > num2)
					{
						num = num2;
					}
				}
				result = new DateTime(year, transitionTime.Month, num) + timeOfDay;
			}
			else if (transitionTime.Week <= 4)
			{
				result = new DateTime(year, transitionTime.Month, 1) + timeOfDay;
				int dayOfWeek = (int)result.DayOfWeek;
				int num3 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num3 < 0)
				{
					num3 += 7;
				}
				num3 += 7 * (transitionTime.Week - 1);
				if (num3 > 0)
				{
					result = result.AddDays((double)num3);
				}
			}
			else
			{
				int day = DateTime.DaysInMonth(year, transitionTime.Month);
				result = new DateTime(year, transitionTime.Month, day) + timeOfDay;
				int dayOfWeek2 = (int)result.DayOfWeek;
				int num4 = dayOfWeek2 - (int)transitionTime.DayOfWeek;
				if (num4 < 0)
				{
					num4 += 7;
				}
				if (num4 > 0)
				{
					result = result.AddDays((double)(-(double)num4));
				}
			}
			return result;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000F5BEC File Offset: 0x000F4DEC
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData, bool alwaysFallbackToLocalMachine = false)
		{
			TimeZoneInfo.TimeZoneInfoResult result = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo;
			if (cachedData._systemTimeZones != null && cachedData._systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
				}
				return result;
			}
			if (!cachedData._allSystemTimeZonesRead || alwaysFallbackToLocalMachine)
			{
				result = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, dstDisabled, out value, out e, cachedData);
			}
			else
			{
				result = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return result;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000F5C98 File Offset: 0x000F4E98
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo timeZoneInfo;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, out timeZoneInfo, out e);
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				if (cachedData._systemTimeZones == null)
				{
					cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase)
					{
						{
							"UTC",
							TimeZoneInfo.s_utcTimeZone
						}
					};
				}
				if (!id.Equals("UTC", StringComparison.OrdinalIgnoreCase))
				{
					cachedData._systemTimeZones.Add(id, timeZoneInfo);
				}
				if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
				}
			}
			else
			{
				value = null;
			}
			return timeZoneInfoResult;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000F5D5C File Offset: 0x000F4F5C
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidId, id), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", SR.ArgumentOutOfRange_UtcOffset);
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException(SR.Argument_TimeSpanHasSeconds, "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException(SR.Argument_AdjustmentRulesNoNulls);
					}
					if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(baseUtcOffset, adjustmentRule))
					{
						throw new InvalidTimeZoneException(SR.ArgumentOutOfRange_UtcOffsetAndDaylightDelta);
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException(SR.Argument_AdjustmentRulesOutOfOrder);
					}
				}
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000F5E35 File Offset: 0x000F5035
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset < TimeZoneInfo.MinOffset || offset > TimeZoneInfo.MaxOffset;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000F5E51 File Offset: 0x000F5051
		private static TimeSpan GetUtcOffset(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return baseUtcOffset + adjustmentRule.BaseUtcOffsetDelta + (adjustmentRule.HasDaylightSaving ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000F5E7C File Offset: 0x000F507C
		private static bool IsValidAdjustmentRuleOffest(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			TimeSpan utcOffset = TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule);
			return !TimeZoneInfo.UtcOffsetOutOfRange(utcOffset);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000F5E9A File Offset: 0x000F509A
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this._adjustmentRules == null)
			{
				return Array.Empty<TimeZoneInfo.AdjustmentRule>();
			}
			return (TimeZoneInfo.AdjustmentRule[])this._adjustmentRules.Clone();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000F5EBC File Offset: 0x000F50BC
		private static void PopulateAllSystemTimeZones(TimeZoneInfo.CachedData cachedData)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey != null)
				{
					foreach (string id in registryKey.GetSubKeyNames())
					{
						TimeZoneInfo timeZoneInfo;
						Exception ex;
						TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out ex, cachedData, false);
					}
				}
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000F5F20 File Offset: 0x000F5120
		private TimeZoneInfo(in Interop.Kernel32.TIME_ZONE_INFORMATION zone, bool dstDisabled)
		{
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = zone;
			string standardName = time_ZONE_INFORMATION.GetStandardName();
			if (standardName.Length == 0)
			{
				this._id = "Local";
			}
			else
			{
				this._id = standardName;
			}
			this._baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
			if (!dstDisabled)
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT = new Interop.Kernel32.REG_TZI_FORMAT(ref zone);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
				if (adjustmentRule != null)
				{
					this._adjustmentRules = new TimeZoneInfo.AdjustmentRule[]
					{
						adjustmentRule
					};
				}
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out this._supportsDaylightSavingTime);
			this._displayName = standardName;
			this._standardDisplayName = standardName;
			time_ZONE_INFORMATION = zone;
			this._daylightDisplayName = time_ZONE_INFORMATION.GetDaylightName();
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000F5FF0 File Offset: 0x000F51F0
		private static bool CheckDaylightSavingTimeNotSupported(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone)
		{
			Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
			return daylightDate.Equals(timeZone.StandardDate);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000F6014 File Offset: 0x000F5214
		private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				if (timeZoneInformation.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
			else
			{
				TimeZoneInfo.TransitionTime daylightTransitionStart;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out daylightTransitionStart, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, false))
				{
					return null;
				}
				if (daylightTransitionStart.Equals(transitionTime))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), daylightTransitionStart, transitionTime, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000F60D0 File Offset: 0x000F52D0
		private static string FindIdFromTimeZoneInformation(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, out bool dstDisabled)
		{
			dstDisabled = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey == null)
				{
					return null;
				}
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, text, out dstDisabled))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000F6144 File Offset: 0x000F5344
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION time_DYNAMIC_ZONE_INFORMATION;
			uint dynamicTimeZoneInformation = Interop.Kernel32.GetDynamicTimeZoneInformation(out time_DYNAMIC_ZONE_INFORMATION);
			if (dynamicTimeZoneInformation == 4294967295U)
			{
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}
			string timeZoneKeyName = time_DYNAMIC_ZONE_INFORMATION.GetTimeZoneKeyName();
			TimeZoneInfo result;
			Exception ex;
			if (timeZoneKeyName.Length != 0 && TimeZoneInfo.TryGetTimeZone(timeZoneKeyName, time_DYNAMIC_ZONE_INFORMATION.DynamicDaylightTimeDisabled > 0, out result, out ex, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = new Interop.Kernel32.TIME_ZONE_INFORMATION(ref time_DYNAMIC_ZONE_INFORMATION);
			bool dstDisabled;
			string text = TimeZoneInfo.FindIdFromTimeZoneInformation(time_ZONE_INFORMATION, out dstDisabled);
			TimeZoneInfo result2;
			if (text != null && TimeZoneInfo.TryGetTimeZone(text, dstDisabled, out result2, out ex, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result2;
			}
			return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(time_ZONE_INFORMATION, dstDisabled);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000F61D8 File Offset: 0x000F53D8
		private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZoneInformation, bool dstDisabled)
		{
			try
			{
				return new TimeZoneInfo(ref timeZoneInformation, dstDisabled);
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidTimeZoneException)
			{
			}
			if (!dstDisabled)
			{
				try
				{
					return new TimeZoneInfo(ref timeZoneInformation, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000F6258 File Offset: 0x000F5458
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (string.Equals(id, "UTC", StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.Utc;
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0 || id.Length > 255 || id.Contains('\0'))
			{
				throw new TimeZoneNotFoundException(SR.Format(SR.TimeZoneNotFound_MissingData, id));
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			TimeZoneInfo result;
			Exception ex;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			lock (obj)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZone(id, false, out result, out ex, cachedData, false);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
			{
				throw new InvalidTimeZoneException(SR.Format(SR.InvalidTimeZone_InvalidRegistryData, id), ex);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
			{
				throw new SecurityException(SR.Format(SR.Security_CannotReadRegistryData, id), ex);
			}
			throw new TimeZoneNotFoundException(SR.Format(SR.TimeZoneNotFound_MissingData, id), ex);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000F633C File Offset: 0x000F553C
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			isAmbiguousLocalDst = false;
			int year = time.Year;
			TimeZoneInfo.OffsetAndRule oneYearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
			TimeSpan timeSpan = oneYearLocalFromUtc.Offset;
			if (oneYearLocalFromUtc.Rule != null)
			{
				timeSpan += oneYearLocalFromUtc.Rule.BaseUtcOffsetDelta;
				if (oneYearLocalFromUtc.Rule.HasDaylightSaving)
				{
					bool isDaylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, oneYearLocalFromUtc.Offset, oneYearLocalFromUtc.Rule, null, out isAmbiguousLocalDst, TimeZoneInfo.Local);
					timeSpan += (isDaylightSavingsFromUtc ? oneYearLocalFromUtc.Rule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000F63D0 File Offset: 0x000F55D0
		private static bool TransitionTimeFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day, (DayOfWeek)timeZoneInformation.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day, (DayOfWeek)timeZoneInformation.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000F6590 File Offset: 0x000F5790
		private static bool TryCreateAdjustmentRules(string id, in Interop.Kernel32.REG_TZI_FORMAT defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
		{
			rules = null;
			e = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id + "\\Dynamic DST", false))
				{
					if (registryKey == null)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(defaultTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[]
							{
								adjustmentRule
							};
						}
						return true;
					}
					int num = (int)registryKey.GetValue("FirstEntry", -1);
					int num2 = (int)registryKey.GetValue("LastEntry", -1);
					if (num == -1 || num2 == -1 || num > num2)
					{
						return false;
					}
					Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					if (num == num2)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule2 != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[]
							{
								adjustmentRule2
							};
						}
						return true;
					}
					List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, new DateTime(num, 12, 31), defaultBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						list.Add(adjustmentRule3);
					}
					for (int i = num + 1; i < num2; i++)
					{
						if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, i.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
						{
							return false;
						}
						TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, new DateTime(i, 1, 1), new DateTime(i, 12, 31), defaultBaseUtcOffset);
						if (adjustmentRule4 != null)
						{
							list.Add(adjustmentRule4);
						}
					}
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num2.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, new DateTime(num2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
					if (adjustmentRule5 != null)
					{
						list.Add(adjustmentRule5);
					}
					if (list.Count != 0)
					{
						rules = list.ToArray();
					}
				}
			}
			catch (InvalidCastException ex)
			{
				e = ex;
				return false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				e = ex2;
				return false;
			}
			catch (ArgumentException ex3)
			{
				e = ex3;
				return false;
			}
			return true;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000F6814 File Offset: 0x000F5A14
		private unsafe static bool TryGetTimeZoneEntryFromRegistry(RegistryKey key, string name, out Interop.Kernel32.REG_TZI_FORMAT dtzi)
		{
			byte[] array = key.GetValue(name, null) as byte[];
			if (array == null || array.Length != sizeof(Interop.Kernel32.REG_TZI_FORMAT))
			{
				dtzi = default(Interop.Kernel32.REG_TZI_FORMAT);
				return false;
			}
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				dtzi = *(Interop.Kernel32.REG_TZI_FORMAT*)ptr2;
			}
			return true;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000F6864 File Offset: 0x000F5A64
		private static bool TryCompareStandardDate(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, in Interop.Kernel32.REG_TZI_FORMAT registryTimeZoneInfo)
		{
			if (timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias)
			{
				Interop.Kernel32.SYSTEMTIME standardDate = timeZone.StandardDate;
				return standardDate.Equals(registryTimeZoneInfo.StandardDate);
			}
			return false;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000F68A4 File Offset: 0x000F5AA4
		private static bool TryCompareTimeZoneInformationToRegistry(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, string id, out bool dstDisabled)
		{
			dstDisabled = false;
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				if (registryKey == null)
				{
					result = false;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					result = false;
				}
				else if (!TimeZoneInfo.TryCompareStandardDate(timeZone, reg_TZI_FORMAT))
				{
					result = false;
				}
				else
				{
					bool flag;
					if (!dstDisabled && !TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone))
					{
						if (timeZone.DaylightBias == reg_TZI_FORMAT.DaylightBias)
						{
							Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
							flag = daylightDate.Equals(reg_TZI_FORMAT.DaylightDate);
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = true;
					}
					bool flag2 = flag;
					if (flag2)
					{
						string text = registryKey.GetValue("Std", string.Empty) as string;
						string a = text;
						Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = timeZone;
						flag2 = string.Equals(a, time_ZONE_INFORMATION.GetStandardName(), StringComparison.Ordinal);
					}
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000F6984 File Offset: 0x000F5B84
		private unsafe static string TryGetLocalizedNameByMuiNativeResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			string[] array = resource.Split(',', StringSplitOptions.None);
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string systemDirectory = Environment.SystemDirectory;
			string path = array[0].TrimStart('@');
			string pcwszFilePath;
			try
			{
				pcwszFilePath = Path.Combine(systemDirectory, path);
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			int num;
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return string.Empty;
			}
			num = -num;
			string result;
			try
			{
				char* ptr = stackalloc char[(UIntPtr)520];
				int length = 260;
				int num2 = 0;
				long num3 = 0L;
				result = (Interop.Kernel32.GetFileMUIPath(16U, pcwszFilePath, null, ref num2, ptr, ref length, ref num3) ? TimeZoneInfo.TryGetLocalizedNameByNativeResource(new string(ptr, 0, length), num) : string.Empty);
			}
			catch (EntryPointNotFoundException)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000F6A6C File Offset: 0x000F5C6C
		private unsafe static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Interop.Kernel32.LoadLibraryEx(filePath, IntPtr.Zero, 2);
				if (intPtr != IntPtr.Zero)
				{
					char* ptr = stackalloc char[(UIntPtr)1000];
					int num = Interop.User32.LoadString(intPtr, (uint)resource, ptr, 500);
					if (num != 0)
					{
						return new string(ptr, 0, num);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Interop.Kernel32.FreeLibrary(intPtr);
				}
			}
			return string.Empty;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000F6AEC File Offset: 0x000F5CEC
		private static void GetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
		{
			displayName = string.Empty;
			standardName = string.Empty;
			daylightName = string.Empty;
			string text = key.GetValue("MUI_Display", string.Empty) as string;
			string text2 = key.GetValue("MUI_Std", string.Empty) as string;
			string text3 = key.GetValue("MUI_Dlt", string.Empty) as string;
			if (!string.IsNullOrEmpty(text))
			{
				displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text3);
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = (key.GetValue("Display", string.Empty) as string);
			}
			if (string.IsNullOrEmpty(standardName))
			{
				standardName = (key.GetValue("Std", string.Empty) as string);
			}
			if (string.IsNullOrEmpty(daylightName))
			{
				daylightName = (key.GetValue("Dlt", string.Empty) as string);
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000F6BE0 File Offset: 0x000F5DE0
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, out TimeZoneInfo value, out Exception e)
		{
			e = null;
			TimeZoneInfo.TimeZoneInfoResult result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				TimeZoneInfo.AdjustmentRule[] adjustmentRules;
				if (registryKey == null)
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else if (!TimeZoneInfo.TryCreateAdjustmentRules(id, reg_TZI_FORMAT, out adjustmentRules, out e, reg_TZI_FORMAT.Bias))
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else
				{
					string displayName;
					string standardDisplayName;
					string daylightDisplayName;
					TimeZoneInfo.GetLocalizedNamesByRegistryKey(registryKey, out displayName, out standardDisplayName, out daylightDisplayName);
					try
					{
						value = new TimeZoneInfo(id, new TimeSpan(0, -reg_TZI_FORMAT.Bias, 0), displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
						result = TimeZoneInfo.TimeZoneInfoResult.Success;
					}
					catch (ArgumentException ex)
					{
						value = null;
						e = ex;
						result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
					catch (InvalidTimeZoneException ex2)
					{
						value = null;
						e = ex2;
						result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
				}
			}
			return result;
		}

		// Token: 0x0400055D RID: 1373
		private readonly string _id;

		// Token: 0x0400055E RID: 1374
		private readonly string _displayName;

		// Token: 0x0400055F RID: 1375
		private readonly string _standardDisplayName;

		// Token: 0x04000560 RID: 1376
		private readonly string _daylightDisplayName;

		// Token: 0x04000561 RID: 1377
		private readonly TimeSpan _baseUtcOffset;

		// Token: 0x04000562 RID: 1378
		private readonly bool _supportsDaylightSavingTime;

		// Token: 0x04000563 RID: 1379
		private readonly TimeZoneInfo.AdjustmentRule[] _adjustmentRules;

		// Token: 0x04000564 RID: 1380
		private static readonly TimeZoneInfo s_utcTimeZone = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "(UTC) Coordinated Universal Time", "Coordinated Universal Time");

		// Token: 0x04000565 RID: 1381
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x04000566 RID: 1382
		private static readonly DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x04000567 RID: 1383
		private static readonly DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x04000568 RID: 1384
		private static readonly TimeSpan MaxOffset = TimeSpan.FromHours(14.0);

		// Token: 0x04000569 RID: 1385
		private static readonly TimeSpan MinOffset = -TimeZoneInfo.MaxOffset;

		// Token: 0x02000197 RID: 407
		[NullableContext(0)]
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001916 RID: 6422 RVA: 0x000F6D3F File Offset: 0x000F5F3F
			public DateTime DateStart
			{
				get
				{
					return this._dateStart;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001917 RID: 6423 RVA: 0x000F6D47 File Offset: 0x000F5F47
			public DateTime DateEnd
			{
				get
				{
					return this._dateEnd;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001918 RID: 6424 RVA: 0x000F6D4F File Offset: 0x000F5F4F
			public TimeSpan DaylightDelta
			{
				get
				{
					return this._daylightDelta;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001919 RID: 6425 RVA: 0x000F6D57 File Offset: 0x000F5F57
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this._daylightTransitionStart;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x0600191A RID: 6426 RVA: 0x000F6D5F File Offset: 0x000F5F5F
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this._daylightTransitionEnd;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x0600191B RID: 6427 RVA: 0x000F6D67 File Offset: 0x000F5F67
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this._baseUtcOffsetDelta;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x0600191C RID: 6428 RVA: 0x000F6D6F File Offset: 0x000F5F6F
			internal bool NoDaylightTransitions
			{
				get
				{
					return this._noDaylightTransitions;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x0600191D RID: 6429 RVA: 0x000F6D78 File Offset: 0x000F5F78
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || (this.DaylightTransitionStart != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue) || (this.DaylightTransitionEnd != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0));
				}
			}

			// Token: 0x0600191E RID: 6430 RVA: 0x000F6E08 File Offset: 0x000F6008
			[NullableContext(2)]
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return other != null && this._dateStart == other._dateStart && this._dateEnd == other._dateEnd && this._daylightDelta == other._daylightDelta && this._baseUtcOffsetDelta == other._baseUtcOffsetDelta && this._daylightTransitionEnd.Equals(other._daylightTransitionEnd) && this._daylightTransitionStart.Equals(other._daylightTransitionStart);
			}

			// Token: 0x0600191F RID: 6431 RVA: 0x000F6E8A File Offset: 0x000F608A
			public override int GetHashCode()
			{
				return this._dateStart.GetHashCode();
			}

			// Token: 0x06001920 RID: 6432 RVA: 0x000F6E98 File Offset: 0x000F6098
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, noDaylightTransitions);
				this._dateStart = dateStart;
				this._dateEnd = dateEnd;
				this._daylightDelta = daylightDelta;
				this._daylightTransitionStart = daylightTransitionStart;
				this._daylightTransitionEnd = daylightTransitionEnd;
				this._baseUtcOffsetDelta = baseUtcOffsetDelta;
				this._noDaylightTransitions = noDaylightTransitions;
			}

			// Token: 0x06001921 RID: 6433 RVA: 0x000F6EEE File Offset: 0x000F60EE
			[NullableContext(1)]
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, TimeSpan.Zero, false);
			}

			// Token: 0x06001922 RID: 6434 RVA: 0x000F6F01 File Offset: 0x000F6101
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule.AdjustDaylightDeltaToExpectedRange(ref daylightDelta, ref baseUtcOffsetDelta);
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, baseUtcOffsetDelta, noDaylightTransitions);
			}

			// Token: 0x06001923 RID: 6435 RVA: 0x000F6F1C File Offset: 0x000F611C
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.TimeOfDay.Ticks < 10000000L && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06001924 RID: 6436 RVA: 0x000F6F94 File Offset: 0x000F6194
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.TimeOfDay.Ticks < 10000000L && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06001925 RID: 6437 RVA: 0x000F700C File Offset: 0x000F620C
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, bool noDaylightTransitions)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified && dateStart.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException(SR.Argument_DateTimeKindMustBeUnspecifiedOrUtc, "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified && dateEnd.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException(SR.Argument_DateTimeKindMustBeUnspecifiedOrUtc, "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd) && !noDaylightTransitions)
				{
					throw new ArgumentException(SR.Argument_TransitionTimesAreIdentical, "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException(SR.Argument_OutOfOrderDateTimes, "dateStart");
				}
				if (daylightDelta.TotalHours < -23.0 || daylightDelta.TotalHours > 14.0)
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, SR.ArgumentOutOfRange_UtcOffset);
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException(SR.Argument_TimeSpanHasSeconds, "daylightDelta");
				}
				if (dateStart != DateTime.MinValue && dateStart.Kind == DateTimeKind.Unspecified && dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(SR.Argument_DateTimeHasTimeOfDay, "dateStart");
				}
				if (dateEnd != DateTime.MaxValue && dateEnd.Kind == DateTimeKind.Unspecified && dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(SR.Argument_DateTimeHasTimeOfDay, "dateEnd");
				}
			}

			// Token: 0x06001926 RID: 6438 RVA: 0x000F7164 File Offset: 0x000F6364
			private static void AdjustDaylightDeltaToExpectedRange(ref TimeSpan daylightDelta, ref TimeSpan baseUtcOffsetDelta)
			{
				if (daylightDelta > TimeZoneInfo.AdjustmentRule.MaxDaylightDelta)
				{
					daylightDelta -= TimeZoneInfo.AdjustmentRule.DaylightDeltaAdjustment;
					baseUtcOffsetDelta += TimeZoneInfo.AdjustmentRule.DaylightDeltaAdjustment;
					return;
				}
				if (daylightDelta < -TimeZoneInfo.AdjustmentRule.MaxDaylightDelta)
				{
					daylightDelta += TimeZoneInfo.AdjustmentRule.DaylightDeltaAdjustment;
					baseUtcOffsetDelta -= TimeZoneInfo.AdjustmentRule.DaylightDeltaAdjustment;
				}
			}

			// Token: 0x06001927 RID: 6439 RVA: 0x000F71F4 File Offset: 0x000F63F4
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this._dateStart, this._dateEnd, this._daylightDelta, this._daylightTransitionStart, this._daylightTransitionEnd, this._noDaylightTransitions);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException);
				}
			}

			// Token: 0x06001928 RID: 6440 RVA: 0x000F724C File Offset: 0x000F644C
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this._dateStart);
				info.AddValue("DateEnd", this._dateEnd);
				info.AddValue("DaylightDelta", this._daylightDelta);
				info.AddValue("DaylightTransitionStart", this._daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this._daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this._baseUtcOffsetDelta);
				info.AddValue("NoDaylightTransitions", this._noDaylightTransitions);
			}

			// Token: 0x06001929 RID: 6441 RVA: 0x000F72F4 File Offset: 0x000F64F4
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this._dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this._daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this._daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this._daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (valueNoThrow != null)
				{
					this._baseUtcOffsetDelta = (TimeSpan)valueNoThrow;
				}
				valueNoThrow = info.GetValueNoThrow("NoDaylightTransitions", typeof(bool));
				if (valueNoThrow != null)
				{
					this._noDaylightTransitions = (bool)valueNoThrow;
				}
			}

			// Token: 0x0400056A RID: 1386
			private static readonly TimeSpan DaylightDeltaAdjustment = TimeSpan.FromHours(24.0);

			// Token: 0x0400056B RID: 1387
			private static readonly TimeSpan MaxDaylightDelta = TimeSpan.FromHours(12.0);

			// Token: 0x0400056C RID: 1388
			private readonly DateTime _dateStart;

			// Token: 0x0400056D RID: 1389
			private readonly DateTime _dateEnd;

			// Token: 0x0400056E RID: 1390
			private readonly TimeSpan _daylightDelta;

			// Token: 0x0400056F RID: 1391
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionStart;

			// Token: 0x04000570 RID: 1392
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionEnd;

			// Token: 0x04000571 RID: 1393
			private readonly TimeSpan _baseUtcOffsetDelta;

			// Token: 0x04000572 RID: 1394
			private readonly bool _noDaylightTransitions;
		}

		// Token: 0x02000198 RID: 408
		private enum TimeZoneInfoResult
		{
			// Token: 0x04000574 RID: 1396
			Success,
			// Token: 0x04000575 RID: 1397
			TimeZoneNotFoundException,
			// Token: 0x04000576 RID: 1398
			InvalidTimeZoneException,
			// Token: 0x04000577 RID: 1399
			SecurityException
		}

		// Token: 0x02000199 RID: 409
		private sealed class CachedData
		{
			// Token: 0x0600192B RID: 6443 RVA: 0x000F7428 File Offset: 0x000F6628
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo result;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
						this._localTimeZone = timeZoneInfo;
					}
					result = timeZoneInfo;
				}
				return result;
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x0600192C RID: 6444 RVA: 0x000F74A8 File Offset: 0x000F66A8
			public TimeZoneInfo Local
			{
				get
				{
					return this._localTimeZone ?? this.CreateLocal();
				}
			}

			// Token: 0x0600192D RID: 6445 RVA: 0x000F74BC File Offset: 0x000F66BC
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				if (timeZone == TimeZoneInfo.s_utcTimeZone)
				{
					return DateTimeKind.Utc;
				}
				if (timeZone != this._localTimeZone)
				{
					return DateTimeKind.Unspecified;
				}
				return DateTimeKind.Local;
			}

			// Token: 0x0600192E RID: 6446 RVA: 0x000F74D8 File Offset: 0x000F66D8
			private static TimeZoneInfo GetCurrentOneYearLocal()
			{
				Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION;
				uint timeZoneInformation = Interop.Kernel32.GetTimeZoneInformation(out time_ZONE_INFORMATION);
				if (timeZoneInformation != 4294967295U)
				{
					return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(time_ZONE_INFORMATION, false);
				}
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}

			// Token: 0x0600192F RID: 6447 RVA: 0x000F7514 File Offset: 0x000F6714
			public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
			{
				TimeZoneInfo.OffsetAndRule offsetAndRule = this._oneYearLocalFromUtc;
				if (offsetAndRule == null || offsetAndRule.Year != year)
				{
					TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
					TimeZoneInfo.AdjustmentRule[] adjustmentRules = currentOneYearLocal._adjustmentRules;
					TimeZoneInfo.AdjustmentRule rule = (adjustmentRules != null) ? adjustmentRules[0] : null;
					offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, rule);
					this._oneYearLocalFromUtc = offsetAndRule;
				}
				return offsetAndRule;
			}

			// Token: 0x04000578 RID: 1400
			private volatile TimeZoneInfo _localTimeZone;

			// Token: 0x04000579 RID: 1401
			public Dictionary<string, TimeZoneInfo> _systemTimeZones;

			// Token: 0x0400057A RID: 1402
			public ReadOnlyCollection<TimeZoneInfo> _readOnlySystemTimeZones;

			// Token: 0x0400057B RID: 1403
			public bool _allSystemTimeZonesRead;

			// Token: 0x0400057C RID: 1404
			private volatile TimeZoneInfo.OffsetAndRule _oneYearLocalFromUtc;
		}

		// Token: 0x0200019A RID: 410
		private struct StringSerializer
		{
			// Token: 0x06001931 RID: 6449 RVA: 0x000F7564 File Offset: 0x000F6764
			public unsafe static string GetSerializedString(TimeZoneInfo zone)
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id, ref valueStringBuilder);
				valueStringBuilder.Append(';');
				valueStringBuilder.AppendSpanFormattable<double>(zone.BaseUtcOffset.TotalMinutes, null, CultureInfo.InvariantCulture);
				valueStringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName, ref valueStringBuilder);
				valueStringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName, ref valueStringBuilder);
				valueStringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName, ref valueStringBuilder);
				valueStringBuilder.Append(';');
				TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in adjustmentRules)
				{
					valueStringBuilder.Append('[');
					valueStringBuilder.AppendSpanFormattable<DateTime>(adjustmentRule.DateStart, "MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo);
					valueStringBuilder.Append(';');
					valueStringBuilder.AppendSpanFormattable<DateTime>(adjustmentRule.DateEnd, "MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo);
					valueStringBuilder.Append(';');
					valueStringBuilder.AppendSpanFormattable<double>(adjustmentRule.DaylightDelta.TotalMinutes, null, CultureInfo.InvariantCulture);
					valueStringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, ref valueStringBuilder);
					valueStringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, ref valueStringBuilder);
					valueStringBuilder.Append(';');
					if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
					{
						valueStringBuilder.AppendSpanFormattable<double>(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes, null, CultureInfo.InvariantCulture);
						valueStringBuilder.Append(';');
					}
					if (adjustmentRule.NoDaylightTransitions)
					{
						valueStringBuilder.Append('1');
						valueStringBuilder.Append(';');
					}
					valueStringBuilder.Append(']');
				}
				valueStringBuilder.Append(';');
				return valueStringBuilder.ToString();
			}

			// Token: 0x06001932 RID: 6450 RVA: 0x000F7740 File Offset: 0x000F6940
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue();
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue();
				string nextStringValue2 = stringSerializer.GetNextStringValue();
				string nextStringValue3 = stringSerializer.GetNextStringValue();
				string nextStringValue4 = stringSerializer.GetNextStringValue();
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue();
				TimeZoneInfo result;
				try
				{
					result = new TimeZoneInfo(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue, false);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException);
				}
				catch (InvalidTimeZoneException innerException2)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException2);
				}
				return result;
			}

			// Token: 0x06001933 RID: 6451 RVA: 0x000F77D8 File Offset: 0x000F69D8
			private StringSerializer(string str)
			{
				this._serializedText = str;
				this._currentTokenStartIndex = 0;
				this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x06001934 RID: 6452 RVA: 0x000F77F0 File Offset: 0x000F69F0
			private static void SerializeSubstitute(string text, ref ValueStringBuilder serializedText)
			{
				foreach (char c in text)
				{
					if (c == '\\' || c == '[' || c == ']' || c == ';')
					{
						serializedText.Append('\\');
					}
					serializedText.Append(c);
				}
			}

			// Token: 0x06001935 RID: 6453 RVA: 0x000F783C File Offset: 0x000F6A3C
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, ref ValueStringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append(time.IsFixedDateRule ? '1' : '0');
				serializedText.Append(';');
				serializedText.AppendSpanFormattable<DateTime>(time.TimeOfDay, "HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo);
				serializedText.Append(';');
				serializedText.AppendSpanFormattable<int>(time.Month, null, CultureInfo.InvariantCulture);
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.AppendSpanFormattable<int>(time.Day, null, CultureInfo.InvariantCulture);
					serializedText.Append(';');
				}
				else
				{
					serializedText.AppendSpanFormattable<int>(time.Week, null, CultureInfo.InvariantCulture);
					serializedText.Append(';');
					serializedText.AppendSpanFormattable<int>((int)time.DayOfWeek, null, CultureInfo.InvariantCulture);
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x06001936 RID: 6454 RVA: 0x000F790C File Offset: 0x000F6B0C
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(SR.Format(SR.Serialization_InvalidEscapeSequence, c));
				}
			}

			// Token: 0x06001937 RID: 6455 RVA: 0x000F7938 File Offset: 0x000F6B38
			private void SkipVersionNextDataFields(int depth)
			{
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException(SR.Serialization_InvalidData);
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this._currentTokenStartIndex = i + 1;
								if (this._currentTokenStartIndex >= this._serializedText.Length)
								{
									this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException(SR.Serialization_InvalidData);
			}

			// Token: 0x06001938 RID: 6456 RVA: 0x000F7A28 File Offset: 0x000F6C28
			private unsafe string GetNextStringValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						valueStringBuilder.Append(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException(SR.Serialization_InvalidData);
						}
						if (c == ';')
						{
							this._currentTokenStartIndex = i + 1;
							if (this._currentTokenStartIndex >= this._serializedText.Length)
							{
								this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
							}
							else
							{
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
							}
							return valueStringBuilder.ToString();
						}
						switch (c)
						{
						case '[':
							throw new SerializationException(SR.Serialization_InvalidData);
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							throw new SerializationException(SR.Serialization_InvalidData);
						default:
							valueStringBuilder.Append(this._serializedText[i]);
							break;
						}
					}
				}
				if (state == TimeZoneInfo.StringSerializer.State.Escaped)
				{
					throw new SerializationException(SR.Format(SR.Serialization_InvalidEscapeSequence, string.Empty));
				}
				throw new SerializationException(SR.Serialization_InvalidData);
			}

			// Token: 0x06001939 RID: 6457 RVA: 0x000F7BA4 File Offset: 0x000F6DA4
			private DateTime GetNextDateTimeValue(string format)
			{
				string nextStringValue = this.GetNextStringValue();
				DateTime result;
				if (!DateTime.TryParseExact(nextStringValue, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				return result;
			}

			// Token: 0x0600193A RID: 6458 RVA: 0x000F7BD8 File Offset: 0x000F6DD8
			private TimeSpan GetNextTimeSpanValue()
			{
				int nextInt32Value = this.GetNextInt32Value();
				TimeSpan result;
				try
				{
					result = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException innerException)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException);
				}
				return result;
			}

			// Token: 0x0600193B RID: 6459 RVA: 0x000F7C18 File Offset: 0x000F6E18
			private int GetNextInt32Value()
			{
				string nextStringValue = this.GetNextStringValue();
				int result;
				if (!int.TryParse(nextStringValue, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result))
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				return result;
			}

			// Token: 0x0600193C RID: 6460 RVA: 0x000F7C48 File Offset: 0x000F6E48
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue()
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue(); nextAdjustmentRuleValue != null; nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue())
				{
					list.Add(nextAdjustmentRuleValue);
					num++;
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x0600193D RID: 6461 RVA: 0x000F7CC4 File Offset: 0x000F6EC4
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					return null;
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._serializedText[this._currentTokenStartIndex] == ';')
				{
					return null;
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				this._currentTokenStartIndex++;
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("MM:dd:yyyy");
				DateTime nextDateTimeValue2 = this.GetNextDateTimeValue("MM:dd:yyyy");
				TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue();
				TimeSpan baseUtcOffsetDelta = TimeSpan.Zero;
				int num = 0;
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if ((this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '9') || this._serializedText[this._currentTokenStartIndex] == '-' || this._serializedText[this._currentTokenStartIndex] == '+')
				{
					baseUtcOffsetDelta = this.GetNextTimeSpanValue();
				}
				if (this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '1')
				{
					num = this.GetNextInt32Value();
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				TimeZoneInfo.AdjustmentRule result;
				try
				{
					result = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, baseUtcOffsetDelta, num > 0);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException);
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return result;
			}

			// Token: 0x0600193E RID: 6462 RVA: 0x000F7EE4 File Offset: 0x000F70E4
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ']'))
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				this._currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value();
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value();
				TimeZoneInfo.TransitionTime result;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value();
					try
					{
						result = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_137;
					}
					catch (ArgumentException innerException)
					{
						throw new SerializationException(SR.Serialization_InvalidData, innerException);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value();
				int nextInt32Value5 = this.GetNextInt32Value();
				try
				{
					result = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException innerException2)
				{
					throw new SerializationException(SR.Serialization_InvalidData, innerException2);
				}
				IL_137:
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				bool flag = false;
				if (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ';')
				{
					this._currentTokenStartIndex++;
					flag = true;
				}
				if (!flag)
				{
					throw new SerializationException(SR.Serialization_InvalidData);
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return result;
			}

			// Token: 0x0400057D RID: 1405
			private readonly string _serializedText;

			// Token: 0x0400057E RID: 1406
			private int _currentTokenStartIndex;

			// Token: 0x0400057F RID: 1407
			private TimeZoneInfo.StringSerializer.State _state;

			// Token: 0x0200019B RID: 411
			private enum State
			{
				// Token: 0x04000581 RID: 1409
				Escaped,
				// Token: 0x04000582 RID: 1410
				NotEscaped,
				// Token: 0x04000583 RID: 1411
				StartOfToken,
				// Token: 0x04000584 RID: 1412
				EndOfLine
			}
		}

		// Token: 0x0200019C RID: 412
		[NullableContext(0)]
		[Serializable]
		public readonly struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x0600193F RID: 6463 RVA: 0x000F8108 File Offset: 0x000F7308
			public DateTime TimeOfDay
			{
				get
				{
					return this._timeOfDay;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001940 RID: 6464 RVA: 0x000F8110 File Offset: 0x000F7310
			public int Month
			{
				get
				{
					return (int)this._month;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001941 RID: 6465 RVA: 0x000F8118 File Offset: 0x000F7318
			public int Week
			{
				get
				{
					return (int)this._week;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001942 RID: 6466 RVA: 0x000F8120 File Offset: 0x000F7320
			public int Day
			{
				get
				{
					return (int)this._day;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001943 RID: 6467 RVA: 0x000F8128 File Offset: 0x000F7328
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this._dayOfWeek;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001944 RID: 6468 RVA: 0x000F8130 File Offset: 0x000F7330
			public bool IsFixedDateRule
			{
				get
				{
					return this._isFixedDateRule;
				}
			}

			// Token: 0x06001945 RID: 6469 RVA: 0x000F8138 File Offset: 0x000F7338
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			// Token: 0x06001946 RID: 6470 RVA: 0x000F8150 File Offset: 0x000F7350
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			// Token: 0x06001947 RID: 6471 RVA: 0x000F815A File Offset: 0x000F735A
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			// Token: 0x06001948 RID: 6472 RVA: 0x000F8168 File Offset: 0x000F7368
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				if (this._isFixedDateRule != other._isFixedDateRule || !(this._timeOfDay == other._timeOfDay) || this._month != other._month)
				{
					return false;
				}
				if (!other._isFixedDateRule)
				{
					return this._week == other._week && this._dayOfWeek == other._dayOfWeek;
				}
				return this._day == other._day;
			}

			// Token: 0x06001949 RID: 6473 RVA: 0x000F81DB File Offset: 0x000F73DB
			public override int GetHashCode()
			{
				return (int)this._month ^ (int)this._week << 8;
			}

			// Token: 0x0600194A RID: 6474 RVA: 0x000F81EC File Offset: 0x000F73EC
			private TransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				this._timeOfDay = timeOfDay;
				this._month = (byte)month;
				this._week = (byte)week;
				this._day = (byte)day;
				this._dayOfWeek = dayOfWeek;
				this._isFixedDateRule = isFixedDateRule;
			}

			// Token: 0x0600194B RID: 6475 RVA: 0x000F822A File Offset: 0x000F742A
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			// Token: 0x0600194C RID: 6476 RVA: 0x000F8237 File Offset: 0x000F7437
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x0600194D RID: 6477 RVA: 0x000F8244 File Offset: 0x000F7444
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(SR.Argument_DateTimeKindMustBeUnspecified, "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", SR.ArgumentOutOfRange_MonthParam);
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", SR.ArgumentOutOfRange_DayParam);
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", SR.ArgumentOutOfRange_Week);
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", SR.ArgumentOutOfRange_DayOfWeek);
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDate(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException(SR.Argument_DateTimeHasTicks, "timeOfDay");
				}
			}

			// Token: 0x0600194E RID: 6478 RVA: 0x000F8308 File Offset: 0x000F7508
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				if (this != default(TimeZoneInfo.TransitionTime))
				{
					try
					{
						TimeZoneInfo.TransitionTime.ValidateTransitionTime(this._timeOfDay, (int)this._month, (int)this._week, (int)this._day, this._dayOfWeek);
					}
					catch (ArgumentException innerException)
					{
						throw new SerializationException(SR.Serialization_InvalidData, innerException);
					}
				}
			}

			// Token: 0x0600194F RID: 6479 RVA: 0x000F8370 File Offset: 0x000F7570
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this._timeOfDay);
				info.AddValue("Month", this._month);
				info.AddValue("Week", this._week);
				info.AddValue("Day", this._day);
				info.AddValue("DayOfWeek", this._dayOfWeek);
				info.AddValue("IsFixedDateRule", this._isFixedDateRule);
			}

			// Token: 0x06001950 RID: 6480 RVA: 0x000F83F8 File Offset: 0x000F75F8
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this._month = (byte)info.GetValue("Month", typeof(byte));
				this._week = (byte)info.GetValue("Week", typeof(byte));
				this._day = (byte)info.GetValue("Day", typeof(byte));
				this._dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this._isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x04000585 RID: 1413
			private readonly DateTime _timeOfDay;

			// Token: 0x04000586 RID: 1414
			private readonly byte _month;

			// Token: 0x04000587 RID: 1415
			private readonly byte _week;

			// Token: 0x04000588 RID: 1416
			private readonly byte _day;

			// Token: 0x04000589 RID: 1417
			private readonly DayOfWeek _dayOfWeek;

			// Token: 0x0400058A RID: 1418
			private readonly bool _isFixedDateRule;
		}

		// Token: 0x0200019D RID: 413
		private sealed class OffsetAndRule
		{
			// Token: 0x06001951 RID: 6481 RVA: 0x000F84D3 File Offset: 0x000F76D3
			public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
			{
				this.Year = year;
				this.Offset = offset;
				this.Rule = rule;
			}

			// Token: 0x0400058B RID: 1419
			public readonly int Year;

			// Token: 0x0400058C RID: 1420
			public readonly TimeSpan Offset;

			// Token: 0x0400058D RID: 1421
			public readonly TimeZoneInfo.AdjustmentRule Rule;
		}
	}
}
