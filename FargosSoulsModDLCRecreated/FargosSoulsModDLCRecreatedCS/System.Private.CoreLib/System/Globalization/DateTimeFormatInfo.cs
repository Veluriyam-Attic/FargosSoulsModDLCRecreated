using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020001F6 RID: 502
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DateTimeFormatInfo : IFormatProvider, ICloneable
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x001279AC File Offset: 0x00126BAC
		private string CultureName
		{
			get
			{
				string result;
				if ((result = this._name) == null)
				{
					result = (this._name = this._cultureData.CultureName);
				}
				return result;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x001279D8 File Offset: 0x00126BD8
		private CultureInfo Culture
		{
			get
			{
				CultureInfo result;
				if ((result = this._cultureInfo) == null)
				{
					result = (this._cultureInfo = CultureInfo.GetCultureInfo(this.CultureName));
				}
				return result;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00127A04 File Offset: 0x00126C04
		private string LanguageName
		{
			get
			{
				string result;
				if ((result = this._langName) == null)
				{
					result = (this._langName = this._cultureData.TwoLetterISOLanguageName);
				}
				return result;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00127A2F File Offset: 0x00126C2F
		private string[] InternalGetAbbreviatedDayOfWeekNames()
		{
			return this.abbreviatedDayNames ?? this.InternalGetAbbreviatedDayOfWeekNamesCore();
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00127A41 File Offset: 0x00126C41
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] InternalGetAbbreviatedDayOfWeekNamesCore()
		{
			this.abbreviatedDayNames = this._cultureData.AbbreviatedDayNames(this.Calendar.ID);
			return this.abbreviatedDayNames;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00127A65 File Offset: 0x00126C65
		private string[] InternalGetSuperShortDayNames()
		{
			return this.m_superShortDayNames ?? this.InternalGetSuperShortDayNamesCore();
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00127A77 File Offset: 0x00126C77
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] InternalGetSuperShortDayNamesCore()
		{
			this.m_superShortDayNames = this._cultureData.SuperShortDayNames(this.Calendar.ID);
			return this.m_superShortDayNames;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00127A9B File Offset: 0x00126C9B
		private string[] InternalGetDayOfWeekNames()
		{
			return this.dayNames ?? this.InternalGetDayOfWeekNamesCore();
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00127AAD File Offset: 0x00126CAD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] InternalGetDayOfWeekNamesCore()
		{
			this.dayNames = this._cultureData.DayNames(this.Calendar.ID);
			return this.dayNames;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00127AD1 File Offset: 0x00126CD1
		private string[] InternalGetAbbreviatedMonthNames()
		{
			return this.abbreviatedMonthNames ?? this.InternalGetAbbreviatedMonthNamesCore();
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00127AE3 File Offset: 0x00126CE3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] InternalGetAbbreviatedMonthNamesCore()
		{
			this.abbreviatedMonthNames = this._cultureData.AbbreviatedMonthNames(this.Calendar.ID);
			return this.abbreviatedMonthNames;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00127B07 File Offset: 0x00126D07
		private string[] InternalGetMonthNames()
		{
			return this.monthNames ?? this.internalGetMonthNamesCore();
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00127B19 File Offset: 0x00126D19
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetMonthNamesCore()
		{
			this.monthNames = this._cultureData.MonthNames(this.Calendar.ID);
			return this.monthNames;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00127B3D File Offset: 0x00126D3D
		public DateTimeFormatInfo() : this(CultureInfo.InvariantCulture._cultureData, GregorianCalendar.GetDefaultInstance())
		{
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00127B54 File Offset: 0x00126D54
		internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
		{
			this._cultureData = cultureData;
			this.calendar = cal;
			this.InitializeOverridableProperties(cultureData, this.calendar.ID);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00127B94 File Offset: 0x00126D94
		private void InitializeOverridableProperties(CultureData cultureData, CalendarId calendarId)
		{
			if (this.firstDayOfWeek == -1)
			{
				this.firstDayOfWeek = cultureData.FirstDayOfWeek;
			}
			if (this.calendarWeekRule == -1)
			{
				this.calendarWeekRule = cultureData.CalendarWeekRule;
			}
			if (this.amDesignator == null)
			{
				this.amDesignator = cultureData.AMDesignator;
			}
			if (this.pmDesignator == null)
			{
				this.pmDesignator = cultureData.PMDesignator;
			}
			if (this.timeSeparator == null)
			{
				this.timeSeparator = cultureData.TimeSeparator;
			}
			if (this.dateSeparator == null)
			{
				this.dateSeparator = cultureData.DateSeparator(calendarId);
			}
			this.allLongTimePatterns = this._cultureData.LongTimes;
			this.allShortTimePatterns = this._cultureData.ShortTimes;
			this.allLongDatePatterns = cultureData.LongDates(calendarId);
			this.allShortDatePatterns = cultureData.ShortDates(calendarId);
			this.allYearMonthPatterns = cultureData.YearMonths(calendarId);
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x00127C68 File Offset: 0x00126E68
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.s_invariantInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
					dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
					dateTimeFormatInfo._isReadOnly = true;
					DateTimeFormatInfo.s_invariantInfo = dateTimeFormatInfo;
				}
				return DateTimeFormatInfo.s_invariantInfo;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x00127CA8 File Offset: 0x00126EA8
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (!currentCulture._isInherited)
				{
					DateTimeFormatInfo dateTimeInfo = currentCulture._dateTimeInfo;
					if (dateTimeInfo != null)
					{
						return dateTimeInfo;
					}
				}
				return (DateTimeFormatInfo)currentCulture.GetFormat(typeof(DateTimeFormatInfo));
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00127CE4 File Offset: 0x00126EE4
		public static DateTimeFormatInfo GetInstance([Nullable(2)] IFormatProvider provider)
		{
			if (provider == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo._isInherited)
			{
				return cultureInfo.DateTimeFormat;
			}
			DateTimeFormatInfo dateTimeFormatInfo = provider as DateTimeFormatInfo;
			if (dateTimeFormatInfo != null)
			{
				return dateTimeFormatInfo;
			}
			DateTimeFormatInfo dateTimeFormatInfo2 = provider.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo;
			if (dateTimeFormatInfo2 == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			return dateTimeFormatInfo2;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00127D3F File Offset: 0x00126F3F
		[NullableContext(2)]
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(DateTimeFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00127D58 File Offset: 0x00126F58
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.calendar = (Calendar)this.Calendar.Clone();
			dateTimeFormatInfo._isReadOnly = false;
			return dateTimeFormatInfo;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00127D8F File Offset: 0x00126F8F
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x00127DB0 File Offset: 0x00126FB0
		public string AMDesignator
		{
			get
			{
				if (this.amDesignator == null)
				{
					this.amDesignator = this._cultureData.AMDesignator;
				}
				return this.amDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.ClearTokenHashTable();
				this.amDesignator = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00127DE0 File Offset: 0x00126FE0
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x00127DE8 File Offset: 0x00126FE8
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			[MemberNotNull("calendar")]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == this.calendar)
				{
					return;
				}
				for (int i = 0; i < this.OptionalCalendars.Length; i++)
				{
					if (this.OptionalCalendars[i] == value.ID)
					{
						if (this.calendar != null)
						{
							this.m_eraNames = null;
							this.m_abbrevEraNames = null;
							this.m_abbrevEnglishEraNames = null;
							this.monthDayPattern = null;
							this.dayNames = null;
							this.abbreviatedDayNames = null;
							this.m_superShortDayNames = null;
							this.monthNames = null;
							this.abbreviatedMonthNames = null;
							this.genitiveMonthNames = null;
							this.m_genitiveAbbreviatedMonthNames = null;
							this.leapYearMonthNames = null;
							this.formatFlags = DateTimeFormatFlags.NotInitialized;
							this.allShortDatePatterns = null;
							this.allLongDatePatterns = null;
							this.allYearMonthPatterns = null;
							this.dateTimeOffsetPattern = null;
							this.longDatePattern = null;
							this.shortDatePattern = null;
							this.yearMonthPattern = null;
							this.fullDateTimePattern = null;
							this.generalShortTimePattern = null;
							this.generalLongTimePattern = null;
							this.dateSeparator = null;
							this.ClearTokenHashTable();
						}
						this.calendar = value;
						this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value", value, SR.Argument_InvalidCalendar);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x00127F34 File Offset: 0x00127134
		private CalendarId[] OptionalCalendars
		{
			get
			{
				CalendarId[] result;
				if ((result = this.optionalCalendars) == null)
				{
					result = (this.optionalCalendars = this._cultureData.CalendarIds);
				}
				return result;
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00127F60 File Offset: 0x00127160
		public int GetEra(string eraName)
		{
			if (eraName == null)
			{
				throw new ArgumentNullException("eraName");
			}
			if (eraName.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.EraNames.Length; i++)
			{
				if (this.m_eraNames[i].Length > 0 && this.Culture.CompareInfo.Compare(eraName, this.m_eraNames[i], CompareOptions.IgnoreCase) == 0)
				{
					return i + 1;
				}
			}
			for (int j = 0; j < this.AbbreviatedEraNames.Length; j++)
			{
				if (this.Culture.CompareInfo.Compare(eraName, this.m_abbrevEraNames[j], CompareOptions.IgnoreCase) == 0)
				{
					return j + 1;
				}
			}
			for (int k = 0; k < this.AbbreviatedEnglishEraNames.Length; k++)
			{
				if (CompareInfo.Invariant.Compare(eraName, this.m_abbrevEnglishEraNames[k], CompareOptions.IgnoreCase) == 0)
				{
					return k + 1;
				}
			}
			return -1;
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0012802C File Offset: 0x0012722C
		internal string[] EraNames
		{
			get
			{
				string[] result;
				if ((result = this.m_eraNames) == null)
				{
					result = (this.m_eraNames = this._cultureData.EraNames(this.Calendar.ID));
				}
				return result;
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00128064 File Offset: 0x00127264
		public string GetEraName(int era)
		{
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.EraNames.Length && era >= 0)
			{
				return this.m_eraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x001280B4 File Offset: 0x001272B4
		internal string[] AbbreviatedEraNames
		{
			get
			{
				string[] result;
				if ((result = this.m_abbrevEraNames) == null)
				{
					result = (this.m_abbrevEraNames = this._cultureData.AbbrevEraNames(this.Calendar.ID));
				}
				return result;
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x001280EC File Offset: 0x001272EC
		public string GetAbbreviatedEraName(int era)
		{
			if (this.AbbreviatedEraNames.Length == 0)
			{
				return this.GetEraName(era);
			}
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.m_abbrevEraNames.Length && era >= 0)
			{
				return this.m_abbrevEraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0012814C File Offset: 0x0012734C
		internal string[] AbbreviatedEnglishEraNames
		{
			get
			{
				if (this.m_abbrevEnglishEraNames == null)
				{
					this.m_abbrevEnglishEraNames = this._cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEnglishEraNames;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x00128178 File Offset: 0x00127378
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x001281A4 File Offset: 0x001273A4
		public string DateSeparator
		{
			get
			{
				if (this.dateSeparator == null)
				{
					this.dateSeparator = this._cultureData.DateSeparator(this.Calendar.ID);
				}
				return this.dateSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.ClearTokenHashTable();
				this.dateSeparator = value;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x001281D4 File Offset: 0x001273D4
		// (set) Token: 0x06002017 RID: 8215 RVA: 0x001281F8 File Offset: 0x001273F8
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				if (this.firstDayOfWeek == -1)
				{
					this.firstDayOfWeek = this._cultureData.FirstDayOfWeek;
				}
				return (DayOfWeek)this.firstDayOfWeek;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value < DayOfWeek.Sunday || value > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
				}
				this.firstDayOfWeek = (int)value;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0012824E File Offset: 0x0012744E
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x00128270 File Offset: 0x00127470
		public CalendarWeekRule CalendarWeekRule
		{
			get
			{
				if (this.calendarWeekRule == -1)
				{
					this.calendarWeekRule = this._cultureData.CalendarWeekRule;
				}
				return (CalendarWeekRule)this.calendarWeekRule;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value < CalendarWeekRule.FirstDay || value > CalendarWeekRule.FirstFourDayWeek)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, CalendarWeekRule.FirstDay, CalendarWeekRule.FirstFourDayWeek));
				}
				this.calendarWeekRule = (int)value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x001282C8 File Offset: 0x001274C8
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x001282FE File Offset: 0x001274FE
		public string FullDateTimePattern
		{
			get
			{
				string result;
				if ((result = this.fullDateTimePattern) == null)
				{
					result = (this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.fullDateTimePattern = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x00128328 File Offset: 0x00127528
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x00128350 File Offset: 0x00127550
		public string LongDatePattern
		{
			get
			{
				string result;
				if ((result = this.longDatePattern) == null)
				{
					result = (this.longDatePattern = this.UnclonedLongDatePatterns[0]);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.longDatePattern = value;
				this.OnLongDatePatternChanged();
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00128380 File Offset: 0x00127580
		private void OnLongDatePatternChanged()
		{
			this.ClearTokenHashTable();
			this.fullDateTimePattern = null;
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00128390 File Offset: 0x00127590
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x001283B8 File Offset: 0x001275B8
		public string LongTimePattern
		{
			get
			{
				string result;
				if ((result = this.longTimePattern) == null)
				{
					result = (this.longTimePattern = this.UnclonedLongTimePatterns[0]);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.longTimePattern = value;
				this.OnLongTimePatternChanged();
			}
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x001283E8 File Offset: 0x001275E8
		private void OnLongTimePatternChanged()
		{
			this.ClearTokenHashTable();
			this.fullDateTimePattern = null;
			this.generalLongTimePattern = null;
			this.dateTimeOffsetPattern = null;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00128405 File Offset: 0x00127605
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x00128431 File Offset: 0x00127631
		public string MonthDayPattern
		{
			get
			{
				if (this.monthDayPattern == null)
				{
					this.monthDayPattern = this._cultureData.MonthDay(this.Calendar.ID);
				}
				return this.monthDayPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.monthDayPattern = value;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0012845B File Offset: 0x0012765B
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x0012847C File Offset: 0x0012767C
		public string PMDesignator
		{
			get
			{
				if (this.pmDesignator == null)
				{
					this.pmDesignator = this._cultureData.PMDesignator;
				}
				return this.pmDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.ClearTokenHashTable();
				this.pmDesignator = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x001284AC File Offset: 0x001276AC
		public string RFC1123Pattern
		{
			get
			{
				return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x001284B4 File Offset: 0x001276B4
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x001284DC File Offset: 0x001276DC
		public string ShortDatePattern
		{
			get
			{
				string result;
				if ((result = this.shortDatePattern) == null)
				{
					result = (this.shortDatePattern = this.UnclonedShortDatePatterns[0]);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.shortDatePattern = value;
				this.OnShortDatePatternChanged();
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0012850C File Offset: 0x0012770C
		private void OnShortDatePatternChanged()
		{
			this.ClearTokenHashTable();
			this.generalLongTimePattern = null;
			this.generalShortTimePattern = null;
			this.dateTimeOffsetPattern = null;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0012852C File Offset: 0x0012772C
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x00128554 File Offset: 0x00127754
		public string ShortTimePattern
		{
			get
			{
				string result;
				if ((result = this.shortTimePattern) == null)
				{
					result = (this.shortTimePattern = this.UnclonedShortTimePatterns[0]);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.shortTimePattern = value;
				this.OnShortTimePatternChanged();
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00128584 File Offset: 0x00127784
		private void OnShortTimePatternChanged()
		{
			this.ClearTokenHashTable();
			this.generalShortTimePattern = null;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x00128593 File Offset: 0x00127793
		public string SortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0012859C File Offset: 0x0012779C
		internal string GeneralShortTimePattern
		{
			get
			{
				string result;
				if ((result = this.generalShortTimePattern) == null)
				{
					result = (this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern);
				}
				return result;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x001285D4 File Offset: 0x001277D4
		internal string GeneralLongTimePattern
		{
			get
			{
				string result;
				if ((result = this.generalLongTimePattern) == null)
				{
					result = (this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern);
				}
				return result;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0012860C File Offset: 0x0012780C
		internal string DateTimeOffsetPattern
		{
			get
			{
				if (this.dateTimeOffsetPattern == null)
				{
					bool flag = false;
					bool flag2 = false;
					char c = '\'';
					int num = 0;
					while (!flag && num < this.LongTimePattern.Length)
					{
						char c2 = this.LongTimePattern[num];
						if (c2 <= '%')
						{
							if (c2 == '"')
							{
								goto IL_51;
							}
							if (c2 == '%')
							{
								goto IL_7B;
							}
						}
						else
						{
							if (c2 == '\'')
							{
								goto IL_51;
							}
							if (c2 == '\\')
							{
								goto IL_7B;
							}
							if (c2 == 'z')
							{
								flag = !flag2;
							}
						}
						IL_7F:
						num++;
						continue;
						IL_51:
						if (flag2 && c == this.LongTimePattern[num])
						{
							flag2 = false;
							goto IL_7F;
						}
						if (!flag2)
						{
							c = this.LongTimePattern[num];
							flag2 = true;
							goto IL_7F;
						}
						goto IL_7F;
						IL_7B:
						num++;
						goto IL_7F;
					}
					this.dateTimeOffsetPattern = (flag ? (this.ShortDatePattern + " " + this.LongTimePattern) : (this.ShortDatePattern + " " + this.LongTimePattern + " zzz"));
				}
				return this.dateTimeOffsetPattern;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x001286EF File Offset: 0x001278EF
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x00128710 File Offset: 0x00127910
		public string TimeSeparator
		{
			get
			{
				if (this.timeSeparator == null)
				{
					this.timeSeparator = this._cultureData.TimeSeparator;
				}
				return this.timeSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.ClearTokenHashTable();
				this.timeSeparator = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00128740 File Offset: 0x00127940
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x00128748 File Offset: 0x00127948
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x00128770 File Offset: 0x00127970
		public string YearMonthPattern
		{
			get
			{
				string result;
				if ((result = this.yearMonthPattern) == null)
				{
					result = (this.yearMonthPattern = this.UnclonedYearMonthPatterns[0]);
				}
				return result;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.yearMonthPattern = value;
				this.OnYearMonthPatternChanged();
			}
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x001287A0 File Offset: 0x001279A0
		private void OnYearMonthPatternChanged()
		{
			this.ClearTokenHashTable();
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x001287A8 File Offset: 0x001279A8
		private static void CheckNullValue(string[] values, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (values[i] == null)
				{
					throw new ArgumentNullException("value", SR.ArgumentNull_ArrayValue);
				}
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x001287D6 File Offset: 0x001279D6
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x001287E8 File Offset: 0x001279E8
		public string[] AbbreviatedDayNames
		{
			get
			{
				return (string[])this.InternalGetAbbreviatedDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.abbreviatedDayNames = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0012884D File Offset: 0x00127A4D
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x00128860 File Offset: 0x00127A60
		public string[] ShortestDayNames
		{
			get
			{
				return (string[])this.InternalGetSuperShortDayNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.m_superShortDayNames = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x001288BF File Offset: 0x00127ABF
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x001288D4 File Offset: 0x00127AD4
		public string[] DayNames
		{
			get
			{
				return (string[])this.InternalGetDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.dayNames = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x00128939 File Offset: 0x00127B39
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x0012894C File Offset: 0x00127B4C
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return (string[])this.InternalGetAbbreviatedMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.abbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x001289B5 File Offset: 0x00127BB5
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x001289C8 File Offset: 0x00127BC8
		public string[] MonthNames
		{
			get
			{
				return (string[])this.InternalGetMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.monthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x00128A31 File Offset: 0x00127C31
		internal bool HasSpacesInMonthNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00128A3E File Offset: 0x00127C3E
		internal bool HasSpacesInDayNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00128A4C File Offset: 0x00127C4C
		internal string InternalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
		{
			string[] array;
			if (style != MonthNameStyles.Genitive)
			{
				if (style != MonthNameStyles.LeapYear)
				{
					array = (abbreviated ? this.InternalGetAbbreviatedMonthNames() : this.InternalGetMonthNames());
				}
				else
				{
					array = this.InternalGetLeapYearMonthNames();
				}
			}
			else
			{
				array = this.InternalGetGenitiveMonthNames(abbreviated);
			}
			string[] array2 = array;
			if (month < 1 || month > array2.Length)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, array2.Length));
			}
			return array2[month - 1];
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00128AC4 File Offset: 0x00127CC4
		private string[] InternalGetGenitiveMonthNames(bool abbreviated)
		{
			if (abbreviated)
			{
				if (this.m_genitiveAbbreviatedMonthNames == null)
				{
					this.m_genitiveAbbreviatedMonthNames = this._cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
				}
				return this.m_genitiveAbbreviatedMonthNames;
			}
			if (this.genitiveMonthNames == null)
			{
				this.genitiveMonthNames = this._cultureData.GenitiveMonthNames(this.Calendar.ID);
			}
			return this.genitiveMonthNames;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00128B29 File Offset: 0x00127D29
		internal string[] InternalGetLeapYearMonthNames()
		{
			if (this.leapYearMonthNames == null)
			{
				this.leapYearMonthNames = this._cultureData.LeapYearMonthNames(this.Calendar.ID);
			}
			return this.leapYearMonthNames;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00128B55 File Offset: 0x00127D55
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", dayofweek, SR.Format(SR.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.InternalGetAbbreviatedDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00128B8E File Offset: 0x00127D8E
		public string GetShortestDayName(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", dayOfWeek, SR.Format(SR.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.InternalGetSuperShortDayNames()[(int)dayOfWeek];
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00128BC8 File Offset: 0x00127DC8
		private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
		{
			string[] array = new string[patterns1.Length * patterns2.Length];
			int num = 0;
			for (int i = 0; i < patterns1.Length; i++)
			{
				for (int j = 0; j < patterns2.Length; j++)
				{
					array[num++] = patterns1[i] + connectString + patterns2[j];
				}
			}
			return array;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00128C14 File Offset: 0x00127E14
		public string[] GetAllDateTimePatterns()
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimePatterns = this.GetAllDateTimePatterns(DateTimeFormat.allStandardFormats[i]);
				for (int j = 0; j < allDateTimePatterns.Length; j++)
				{
					list.Add(allDateTimePatterns[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00128C6C File Offset: 0x00127E6C
		public string[] GetAllDateTimePatterns(char format)
		{
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
					return this.AllLongDatePatterns;
				case 'E':
					goto IL_1AD;
				case 'F':
					break;
				case 'G':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
				default:
					switch (format)
					{
					case 'M':
						goto IL_13B;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_1AD;
					case 'O':
						goto IL_14D;
					case 'R':
						goto IL_15E;
					case 'T':
						return this.AllLongTimePatterns;
					case 'U':
						break;
					default:
						goto IL_1AD;
					}
					break;
				}
				return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
			}
			if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
					return this.AllShortDatePatterns;
				case 'e':
					goto IL_1AD;
				case 'f':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
				case 'g':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
				default:
					switch (format)
					{
					case 'm':
						goto IL_13B;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_1AD;
					case 'o':
						goto IL_14D;
					case 'r':
						goto IL_15E;
					case 's':
						return new string[]
						{
							"yyyy'-'MM'-'dd'T'HH':'mm':'ss"
						};
					case 't':
						return this.AllShortTimePatterns;
					case 'u':
						return new string[]
						{
							this.UniversalSortableDateTimePattern
						};
					case 'y':
						break;
					default:
						goto IL_1AD;
					}
					break;
				}
			}
			return this.AllYearMonthPatterns;
			IL_13B:
			return new string[]
			{
				this.MonthDayPattern
			};
			IL_14D:
			return new string[]
			{
				"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"
			};
			IL_15E:
			return new string[]
			{
				"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'"
			};
			IL_1AD:
			throw new ArgumentException(SR.Format(SR.Format_BadFormatSpecifier, format), "format");
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00128E42 File Offset: 0x00128042
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", dayofweek, SR.Format(SR.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.InternalGetDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00128E7B File Offset: 0x0012807B
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 13));
			}
			return this.InternalGetAbbreviatedMonthNames()[month - 1];
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00128EB8 File Offset: 0x001280B8
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 13));
			}
			return this.InternalGetMonthNames()[month - 1];
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00128EF8 File Offset: 0x001280F8
		private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
		{
			if (defaultPattern == patterns[0])
			{
				return (string[])patterns.Clone();
			}
			int num = 0;
			while (num < patterns.Length && !(defaultPattern == patterns[num]))
			{
				num++;
			}
			string[] array;
			if (num < patterns.Length)
			{
				array = (string[])patterns.Clone();
				array[num] = array[0];
			}
			else
			{
				array = new string[patterns.Length + 1];
				Array.Copy(patterns, 0, array, 1, patterns.Length);
			}
			array[0] = defaultPattern;
			return array;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x00128F6B File Offset: 0x0012816B
		private string[] AllYearMonthPatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00128F7E File Offset: 0x0012817E
		private string[] AllShortDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x00128F91 File Offset: 0x00128191
		private string[] AllShortTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00128FA4 File Offset: 0x001281A4
		private string[] AllLongDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00128FB7 File Offset: 0x001281B7
		private string[] AllLongTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00128FCA File Offset: 0x001281CA
		private string[] UnclonedYearMonthPatterns
		{
			get
			{
				if (this.allYearMonthPatterns == null)
				{
					this.allYearMonthPatterns = this._cultureData.YearMonths(this.Calendar.ID);
				}
				return this.allYearMonthPatterns;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00128FF6 File Offset: 0x001281F6
		private string[] UnclonedShortDatePatterns
		{
			get
			{
				if (this.allShortDatePatterns == null)
				{
					this.allShortDatePatterns = this._cultureData.ShortDates(this.Calendar.ID);
				}
				return this.allShortDatePatterns;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00129022 File Offset: 0x00128222
		private string[] UnclonedLongDatePatterns
		{
			get
			{
				if (this.allLongDatePatterns == null)
				{
					this.allLongDatePatterns = this._cultureData.LongDates(this.Calendar.ID);
				}
				return this.allLongDatePatterns;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0012904E File Offset: 0x0012824E
		private string[] UnclonedShortTimePatterns
		{
			get
			{
				if (this.allShortTimePatterns == null)
				{
					this.allShortTimePatterns = this._cultureData.ShortTimes;
				}
				return this.allShortTimePatterns;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0012906F File Offset: 0x0012826F
		private string[] UnclonedLongTimePatterns
		{
			get
			{
				if (this.allLongTimePatterns == null)
				{
					this.allLongTimePatterns = this._cultureData.LongTimes;
				}
				return this.allLongTimePatterns;
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00129090 File Offset: 0x00128290
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			if (dtfi == null)
			{
				throw new ArgumentNullException("dtfi");
			}
			if (dtfi.IsReadOnly)
			{
				return dtfi;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.MemberwiseClone();
			dateTimeFormatInfo.calendar = Calendar.ReadOnly(dtfi.Calendar);
			dateTimeFormatInfo._isReadOnly = true;
			return dateTimeFormatInfo;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x001290DA File Offset: 0x001282DA
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x001290E2 File Offset: 0x001282E2
		public string NativeCalendarName
		{
			get
			{
				return this._cultureData.CalendarName(this.Calendar.ID);
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x001290FC File Offset: 0x001282FC
		public void SetAllDateTimePatterns(string[] patterns, char format)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns");
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException(SR.Arg_ArrayZeroError, "patterns");
			}
			for (int i = 0; i < patterns.Length; i++)
			{
				if (patterns[i] == null)
				{
					throw new ArgumentNullException("patterns[" + i.ToString() + "]", SR.ArgumentNull_ArrayValue);
				}
			}
			if (format <= 'Y')
			{
				if (format == 'D')
				{
					this.allLongDatePatterns = patterns;
					this.longDatePattern = this.allLongDatePatterns[0];
					this.OnLongDatePatternChanged();
					return;
				}
				if (format == 'T')
				{
					this.allLongTimePatterns = patterns;
					this.longTimePattern = this.allLongTimePatterns[0];
					this.OnLongTimePatternChanged();
					return;
				}
				if (format != 'Y')
				{
					goto IL_125;
				}
			}
			else
			{
				if (format == 'd')
				{
					this.allShortDatePatterns = patterns;
					this.shortDatePattern = this.allShortDatePatterns[0];
					this.OnShortDatePatternChanged();
					return;
				}
				if (format == 't')
				{
					this.allShortTimePatterns = patterns;
					this.shortTimePattern = this.allShortTimePatterns[0];
					this.OnShortTimePatternChanged();
					return;
				}
				if (format != 'y')
				{
					goto IL_125;
				}
			}
			this.allYearMonthPatterns = patterns;
			this.yearMonthPattern = this.allYearMonthPatterns[0];
			this.OnYearMonthPatternChanged();
			return;
			IL_125:
			throw new ArgumentException(SR.Format(SR.Format_BadFormatSpecifier, format), "format");
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00129248 File Offset: 0x00128448
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x0012925C File Offset: 0x0012845C
		public string[] AbbreviatedMonthGenitiveNames
		{
			get
			{
				return (string[])this.InternalGetGenitiveMonthNames(true).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.m_genitiveAbbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x001292C5 File Offset: 0x001284C5
		// (set) Token: 0x06002061 RID: 8289 RVA: 0x001292D8 File Offset: 0x001284D8
		public string[] MonthGenitiveNames
		{
			get
			{
				return (string[])this.InternalGetGenitiveMonthNames(false).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidArrayLength, 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.genitiveMonthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x00129344 File Offset: 0x00128544
		internal string DecimalSeparator
		{
			get
			{
				string result;
				if ((result = this._decimalSeparator) == null)
				{
					result = (this._decimalSeparator = new NumberFormatInfo(this._cultureData.UseUserOverride ? CultureData.GetCultureData(this._cultureData.CultureName, false) : this._cultureData).NumberDecimalSeparator);
				}
				return result;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00129394 File Offset: 0x00128594
		internal string FullTimeSpanPositivePattern
		{
			get
			{
				string result;
				if ((result = this._fullTimeSpanPositivePattern) == null)
				{
					result = (this._fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + this.DecimalSeparator + "'FFFFFFF");
				}
				return result;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x001293CC File Offset: 0x001285CC
		internal string FullTimeSpanNegativePattern
		{
			get
			{
				string result;
				if ((result = this._fullTimeSpanNegativePattern) == null)
				{
					result = (this._fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern);
				}
				return result;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x001293FC File Offset: 0x001285FC
		internal CompareInfo CompareInfo
		{
			get
			{
				CompareInfo result;
				if ((result = this._compareInfo) == null)
				{
					result = (this._compareInfo = CompareInfo.GetCompareInfo(this._cultureData.SortName));
				}
				return result;
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0012942C File Offset: 0x0012862C
		internal static void ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_InvalidDateTimeStyles, parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_ConflictingDateTimeStyles, parameterName);
			}
			if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
			{
				throw new ArgumentException(SR.Argument_ConflictingDateTimeRoundtripStyles, parameterName);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00129481 File Offset: 0x00128681
		internal DateTimeFormatFlags FormatFlags
		{
			get
			{
				if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
				{
					return this.InitializeFormatFlags();
				}
				return this.formatFlags;
			}
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0012949C File Offset: 0x0012869C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DateTimeFormatFlags InitializeFormatFlags()
		{
			this.formatFlags = (DateTimeFormatFlags)(DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.InternalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.InternalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.InternalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.InternalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames) | DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar((int)this.Calendar.ID));
			return this.formatFlags;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x00129518 File Offset: 0x00128718
		internal bool HasForceTwoDigitYears
		{
			get
			{
				CalendarId id = this.calendar.ID;
				return id - CalendarId.JAPAN <= 1;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0012953A File Offset: 0x0012873A
		internal bool HasYearMonthAdjustment
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00129548 File Offset: 0x00128748
		internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
		{
			if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
			{
				if (year < 1000)
				{
					year += 5000;
				}
				if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
				{
					return false;
				}
				if (parsedMonthName && !this.Calendar.IsLeapYear(year))
				{
					if (month >= 8)
					{
						month--;
					}
					else if (month == 7)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x001295D0 File Offset: 0x001287D0
		internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_jajpDTFI;
			if (dateTimeFormat == null)
			{
				dateTimeFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
				dateTimeFormat.Calendar = JapaneseCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_jajpDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00129610 File Offset: 0x00128810
		internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_zhtwDTFI;
			if (dateTimeFormat == null)
			{
				dateTimeFormat = new CultureInfo("zh-TW", false).DateTimeFormat;
				dateTimeFormat.Calendar = TaiwanCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0012964D File Offset: 0x0012884D
		private void ClearTokenHashTable()
		{
			this._dtfiTokenHash = null;
			this.formatFlags = DateTimeFormatFlags.NotInitialized;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00129660 File Offset: 0x00128860
		internal DateTimeFormatInfo.TokenHashValue[] CreateTokenHashTable()
		{
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = new DateTimeFormatInfo.TokenHashValue[199];
				string b = this.TimeSeparator.Trim();
				if ("," != b)
				{
					this.InsertHash(array, ",", TokenType.IgnorableSymbol, 0);
				}
				if ("." != b)
				{
					this.InsertHash(array, ".", TokenType.IgnorableSymbol, 0);
				}
				if ("시" != b && "時" != b && "时" != b)
				{
					this.InsertHash(array, this.TimeSeparator, TokenType.SEP_Time, 0);
				}
				this.InsertHash(array, this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, this.PMDesignator, (TokenType)1284, 1);
				if (this.LanguageName.Equals("sq"))
				{
					this.InsertHash(array, "." + this.AMDesignator, (TokenType)1027, 0);
					this.InsertHash(array, "." + this.PMDesignator, (TokenType)1284, 1);
				}
				this.InsertHash(array, "年", TokenType.SEP_YearSuff, 0);
				this.InsertHash(array, "년", TokenType.SEP_YearSuff, 0);
				this.InsertHash(array, "月", TokenType.SEP_MonthSuff, 0);
				this.InsertHash(array, "월", TokenType.SEP_MonthSuff, 0);
				this.InsertHash(array, "日", TokenType.SEP_DaySuff, 0);
				this.InsertHash(array, "일", TokenType.SEP_DaySuff, 0);
				this.InsertHash(array, "時", TokenType.SEP_HourSuff, 0);
				this.InsertHash(array, "时", TokenType.SEP_HourSuff, 0);
				this.InsertHash(array, "分", TokenType.SEP_MinuteSuff, 0);
				this.InsertHash(array, "秒", TokenType.SEP_SecondSuff, 0);
				if (!LocalAppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == CalendarId.JAPAN)
				{
					this.InsertHash(array, "元", TokenType.YearNumberToken, 1);
					this.InsertHash(array, "(", TokenType.IgnorableSymbol, 0);
					this.InsertHash(array, ")", TokenType.IgnorableSymbol, 0);
				}
				if (this.LanguageName.Equals("ko"))
				{
					this.InsertHash(array, "시", TokenType.SEP_HourSuff, 0);
					this.InsertHash(array, "분", TokenType.SEP_MinuteSuff, 0);
					this.InsertHash(array, "초", TokenType.SEP_SecondSuff, 0);
				}
				if (this.LanguageName.Equals("ky"))
				{
					this.InsertHash(array, "-", TokenType.IgnorableSymbol, 0);
				}
				else
				{
					this.InsertHash(array, "-", TokenType.SEP_DateOrOffset, 0);
				}
				DateTimeFormatInfoScanner dateTimeFormatInfoScanner = new DateTimeFormatInfoScanner();
				string[] dateWordsOfDTFI = dateTimeFormatInfoScanner.GetDateWordsOfDTFI(this);
				DateTimeFormatFlags dateTimeFormatFlags = this.FormatFlags;
				bool flag = false;
				if (dateWordsOfDTFI != null)
				{
					for (int i = 0; i < dateWordsOfDTFI.Length; i++)
					{
						char c = dateWordsOfDTFI[i][0];
						if (c != '')
						{
							if (c != '')
							{
								this.InsertHash(array, dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
								if (this.LanguageName.Equals("eu"))
								{
									this.InsertHash(array, "." + dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
								}
							}
							else
							{
								string text = dateWordsOfDTFI[i].Substring(1);
								this.InsertHash(array, text, TokenType.IgnorableSymbol, 0);
								if (this.DateSeparator.Trim(null).Equals(text))
								{
									flag = true;
								}
							}
						}
						else
						{
							ReadOnlySpan<char> monthPostfix = dateWordsOfDTFI[i].AsSpan(1);
							this.AddMonthNames(array, monthPostfix);
						}
					}
				}
				if (!flag)
				{
					this.InsertHash(array, this.DateSeparator, TokenType.SEP_Date, 0);
				}
				this.AddMonthNames(array, default(ReadOnlySpan<char>));
				for (int j = 1; j <= 13; j++)
				{
					this.InsertHash(array, this.GetAbbreviatedMonthName(j), TokenType.MonthToken, j);
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					string[] array2 = this.InternalGetGenitiveMonthNames(false);
					string[] array3 = this.InternalGetGenitiveMonthNames(true);
					for (int k = 1; k <= 13; k++)
					{
						this.InsertHash(array, array2[k - 1], TokenType.MonthToken, k);
						this.InsertHash(array, array3[k - 1], TokenType.MonthToken, k);
					}
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					for (int l = 1; l <= 13; l++)
					{
						string str = this.InternalGetMonthName(l, MonthNameStyles.LeapYear, false);
						this.InsertHash(array, str, TokenType.MonthToken, l);
					}
				}
				for (int m = 0; m < 7; m++)
				{
					string str2 = this.GetDayName((DayOfWeek)m);
					this.InsertHash(array, str2, TokenType.DayOfWeekToken, m);
					str2 = this.GetAbbreviatedDayName((DayOfWeek)m);
					this.InsertHash(array, str2, TokenType.DayOfWeekToken, m);
				}
				int[] eras = this.calendar.Eras;
				for (int n = 1; n <= eras.Length; n++)
				{
					this.InsertHash(array, this.GetEraName(n), TokenType.EraToken, n);
					this.InsertHash(array, this.GetAbbreviatedEraName(n), TokenType.EraToken, n);
				}
				if (!GlobalizationMode.Invariant)
				{
					if (this.LanguageName.Equals("ja"))
					{
						for (int num = 0; num < 7; num++)
						{
							string str3 = "(" + this.GetAbbreviatedDayName((DayOfWeek)num) + ")";
							this.InsertHash(array, str3, TokenType.DayOfWeekToken, num);
						}
						if (this.Calendar.GetType() != typeof(JapaneseCalendar))
						{
							DateTimeFormatInfo japaneseCalendarDTFI = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
							for (int num2 = 1; num2 <= japaneseCalendarDTFI.Calendar.Eras.Length; num2++)
							{
								this.InsertHash(array, japaneseCalendarDTFI.GetEraName(num2), TokenType.JapaneseEraToken, num2);
								this.InsertHash(array, japaneseCalendarDTFI.GetAbbreviatedEraName(num2), TokenType.JapaneseEraToken, num2);
								this.InsertHash(array, japaneseCalendarDTFI.AbbreviatedEnglishEraNames[num2 - 1], TokenType.JapaneseEraToken, num2);
							}
						}
					}
					else if (this.CultureName.Equals("zh-TW"))
					{
						DateTimeFormatInfo taiwanCalendarDTFI = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
						for (int num3 = 1; num3 <= taiwanCalendarDTFI.Calendar.Eras.Length; num3++)
						{
							if (taiwanCalendarDTFI.GetEraName(num3).Length > 0)
							{
								this.InsertHash(array, taiwanCalendarDTFI.GetEraName(num3), TokenType.TEraToken, num3);
							}
						}
					}
				}
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.PMDesignator, (TokenType)1284, 1);
				for (int num4 = 1; num4 <= 12; num4++)
				{
					string str4 = DateTimeFormatInfo.InvariantInfo.GetMonthName(num4);
					this.InsertHash(array, str4, TokenType.MonthToken, num4);
					str4 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(num4);
					this.InsertHash(array, str4, TokenType.MonthToken, num4);
				}
				for (int num5 = 0; num5 < 7; num5++)
				{
					string str5 = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek)num5);
					this.InsertHash(array, str5, TokenType.DayOfWeekToken, num5);
					str5 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)num5);
					this.InsertHash(array, str5, TokenType.DayOfWeekToken, num5);
				}
				for (int num6 = 0; num6 < this.AbbreviatedEnglishEraNames.Length; num6++)
				{
					this.InsertHash(array, this.AbbreviatedEnglishEraNames[num6], TokenType.EraToken, num6 + 1);
				}
				this.InsertHash(array, "T", TokenType.SEP_LocalTimeMark, 0);
				this.InsertHash(array, "GMT", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "Z", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "/", TokenType.SEP_Date, 0);
				this.InsertHash(array, ":", TokenType.SEP_Time, 0);
				this._dtfiTokenHash = array;
			}
			return array;
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00129D80 File Offset: 0x00128F80
		private void AddMonthNames(DateTimeFormatInfo.TokenHashValue[] temp, ReadOnlySpan<char> monthPostfix = default(ReadOnlySpan<char>))
		{
			for (int i = 1; i <= 13; i++)
			{
				string text = this.GetMonthName(i);
				if (text.Length > 0)
				{
					if (!monthPostfix.IsEmpty)
					{
						this.InsertHash(temp, text + monthPostfix, TokenType.MonthToken, i);
					}
					else
					{
						this.InsertHash(temp, text, TokenType.MonthToken, i);
					}
				}
				text = this.GetAbbreviatedMonthName(i);
				this.InsertHash(temp, text, TokenType.MonthToken, i);
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00129DE8 File Offset: 0x00128FE8
		private unsafe static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
		{
			number = -1;
			badFormat = false;
			int index = str.Index;
			if (!HebrewNumber.IsDigit((char)(*str.Value[index])))
			{
				return false;
			}
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState;
			for (;;)
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar((char)(*str.Value[index++]), ref hebrewNumberParsingContext);
				if (hebrewNumberParsingState <= HebrewNumberParsingState.NotHebrewDigit)
				{
					break;
				}
				if (index >= str.Value.Length || hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
				{
					goto IL_5C;
				}
			}
			return false;
			IL_5C:
			if (hebrewNumberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				return false;
			}
			str.Advance(index - str.Index);
			number = hebrewNumberParsingContext.result;
			return true;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00129E6F File Offset: 0x0012906F
		private static bool IsHebrewChar(char ch)
		{
			return ch >= '֐' && ch <= '׿';
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00129E88 File Offset: 0x00129088
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsAllowedJapaneseTokenFollowedByNonSpaceLetter(string tokenString, char nextCh)
		{
			return !LocalAppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == CalendarId.JAPAN && (nextCh == "元"[0] || (tokenString == "元" && nextCh == "年"[0]));
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00129ED8 File Offset: 0x001290D8
		internal unsafe bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
		{
			tokenType = TokenType.UnknownToken;
			tokenValue = 0;
			char c = str.m_current;
			bool flag = char.IsLetter(c);
			if (flag)
			{
				c = this.Culture.TextInfo.ToLower(c);
				bool flag2;
				if (DateTimeFormatInfo.IsHebrewChar(c) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out flag2, out tokenValue))
				{
					if (flag2)
					{
						tokenType = TokenType.UnknownToken;
						return false;
					}
					tokenType = TokenType.HebrewNumber;
					return true;
				}
			}
			int num = (int)(c % 'Ç');
			int num2 = (int)('\u0001' + c % 'Å');
			int num3 = str.Length - str.Index;
			int num4 = 0;
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash ?? this.CreateTokenHashTable();
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			int count;
			for (;;)
			{
				tokenHashValue = array[num];
				if (tokenHashValue == null)
				{
					return false;
				}
				if ((tokenHashValue.tokenType & TokenMask) > (TokenType)0 && tokenHashValue.tokenString.Length <= num3)
				{
					bool flag3 = true;
					if (flag)
					{
						int num5 = str.Index + tokenHashValue.tokenString.Length;
						if (num5 > str.Length)
						{
							flag3 = false;
						}
						else if (num5 < str.Length)
						{
							char c2 = (char)(*str.Value[num5]);
							flag3 = (!char.IsLetter(c2) || this.IsAllowedJapaneseTokenFollowedByNonSpaceLetter(tokenHashValue.tokenString, c2));
						}
					}
					if (flag3 && ((tokenHashValue.tokenString.Length == 1 && *str.Value[str.Index] == (ushort)tokenHashValue.tokenString[0]) || this.Culture.CompareInfo.Compare(str.Value.Slice(str.Index, tokenHashValue.tokenString.Length), tokenHashValue.tokenString, CompareOptions.IgnoreCase) == 0))
					{
						break;
					}
					if ((tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames) || (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames))
					{
						count = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref count))
						{
							goto Block_18;
						}
					}
				}
				num4++;
				num += num2;
				if (num >= 199)
				{
					num -= 199;
				}
				if (num4 >= 199)
				{
					return false;
				}
			}
			tokenType = (tokenHashValue.tokenType & TokenMask);
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(tokenHashValue.tokenString.Length);
			return true;
			Block_18:
			tokenType = (tokenHashValue.tokenType & TokenMask);
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(count);
			return true;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0012A11C File Offset: 0x0012931C
		private void InsertAtCurrentHashNode(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
		{
			DateTimeFormatInfo.TokenHashValue tokenHashValue = hashTable[hashcode];
			hashTable[hashcode] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			while (++pos < 199)
			{
				hashcode += hashProbe;
				if (hashcode >= 199)
				{
					hashcode -= 199;
				}
				DateTimeFormatInfo.TokenHashValue tokenHashValue2 = hashTable[hashcode];
				if (tokenHashValue2 == null || this.Culture.TextInfo.ToLower(tokenHashValue2.tokenString[0]) == ch)
				{
					hashTable[hashcode] = tokenHashValue;
					if (tokenHashValue2 == null)
					{
						return;
					}
					tokenHashValue = tokenHashValue2;
				}
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0012A198 File Offset: 0x00129398
		private void InsertHash(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
		{
			if (string.IsNullOrEmpty(str))
			{
				return;
			}
			int num = 0;
			if (!char.IsWhiteSpace(str[0]))
			{
				string text = str;
				int index = text.Length - 1;
				if (!char.IsWhiteSpace(text[index]))
				{
					goto IL_44;
				}
			}
			str = str.Trim(null);
			if (str.Length == 0)
			{
				return;
			}
			IL_44:
			char c = this.Culture.TextInfo.ToLower(str[0]);
			int num2 = (int)(c % 'Ç');
			int num3 = (int)('\u0001' + c % 'Å');
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			for (;;)
			{
				tokenHashValue = hashTable[num2];
				if (tokenHashValue == null)
				{
					break;
				}
				if (str.Length >= tokenHashValue.tokenString.Length && this.CompareStringIgnoreCaseOptimized(str, 0, tokenHashValue.tokenString.Length, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length))
				{
					goto Block_6;
				}
				num++;
				num2 += num3;
				if (num2 >= 199)
				{
					num2 -= 199;
				}
				if (num >= 199)
				{
					return;
				}
			}
			hashTable[num2] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			return;
			Block_6:
			if (str.Length > tokenHashValue.tokenString.Length)
			{
				this.InsertAtCurrentHashNode(hashTable, str, c, tokenType, tokenValue, num, num2, num3);
				return;
			}
			int tokenType2 = (int)tokenHashValue.tokenType;
			if (((tokenType2 & 255) == 0 && (tokenType & TokenType.RegularTokenMask) != (TokenType)0) || ((tokenType2 & 65280) == 0 && (tokenType & TokenType.SeparatorTokenMask) != (TokenType)0))
			{
				tokenHashValue.tokenType |= tokenType;
				if (tokenValue != 0)
				{
					tokenHashValue.tokenValue = tokenValue;
				}
			}
			return;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0012A2FB File Offset: 0x001294FB
		private bool CompareStringIgnoreCaseOptimized(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return (length1 == 1 && length2 == 1 && string1[offset1] == string2[offset2]) || this.Culture.CompareInfo.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x040007A2 RID: 1954
		private static volatile DateTimeFormatInfo s_invariantInfo;

		// Token: 0x040007A3 RID: 1955
		private readonly CultureData _cultureData;

		// Token: 0x040007A4 RID: 1956
		private string _name;

		// Token: 0x040007A5 RID: 1957
		private string _langName;

		// Token: 0x040007A6 RID: 1958
		private CompareInfo _compareInfo;

		// Token: 0x040007A7 RID: 1959
		private CultureInfo _cultureInfo;

		// Token: 0x040007A8 RID: 1960
		private string amDesignator;

		// Token: 0x040007A9 RID: 1961
		private string pmDesignator;

		// Token: 0x040007AA RID: 1962
		private string dateSeparator;

		// Token: 0x040007AB RID: 1963
		private string generalShortTimePattern;

		// Token: 0x040007AC RID: 1964
		private string generalLongTimePattern;

		// Token: 0x040007AD RID: 1965
		private string timeSeparator;

		// Token: 0x040007AE RID: 1966
		private string monthDayPattern;

		// Token: 0x040007AF RID: 1967
		private string dateTimeOffsetPattern;

		// Token: 0x040007B0 RID: 1968
		private Calendar calendar;

		// Token: 0x040007B1 RID: 1969
		private int firstDayOfWeek = -1;

		// Token: 0x040007B2 RID: 1970
		private int calendarWeekRule = -1;

		// Token: 0x040007B3 RID: 1971
		private string fullDateTimePattern;

		// Token: 0x040007B4 RID: 1972
		private string[] abbreviatedDayNames;

		// Token: 0x040007B5 RID: 1973
		private string[] m_superShortDayNames;

		// Token: 0x040007B6 RID: 1974
		private string[] dayNames;

		// Token: 0x040007B7 RID: 1975
		private string[] abbreviatedMonthNames;

		// Token: 0x040007B8 RID: 1976
		private string[] monthNames;

		// Token: 0x040007B9 RID: 1977
		private string[] genitiveMonthNames;

		// Token: 0x040007BA RID: 1978
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x040007BB RID: 1979
		private string[] leapYearMonthNames;

		// Token: 0x040007BC RID: 1980
		private string longDatePattern;

		// Token: 0x040007BD RID: 1981
		private string shortDatePattern;

		// Token: 0x040007BE RID: 1982
		private string yearMonthPattern;

		// Token: 0x040007BF RID: 1983
		private string longTimePattern;

		// Token: 0x040007C0 RID: 1984
		private string shortTimePattern;

		// Token: 0x040007C1 RID: 1985
		private string[] allYearMonthPatterns;

		// Token: 0x040007C2 RID: 1986
		private string[] allShortDatePatterns;

		// Token: 0x040007C3 RID: 1987
		private string[] allLongDatePatterns;

		// Token: 0x040007C4 RID: 1988
		private string[] allShortTimePatterns;

		// Token: 0x040007C5 RID: 1989
		private string[] allLongTimePatterns;

		// Token: 0x040007C6 RID: 1990
		private string[] m_eraNames;

		// Token: 0x040007C7 RID: 1991
		private string[] m_abbrevEraNames;

		// Token: 0x040007C8 RID: 1992
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x040007C9 RID: 1993
		private CalendarId[] optionalCalendars;

		// Token: 0x040007CA RID: 1994
		internal bool _isReadOnly;

		// Token: 0x040007CB RID: 1995
		private DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;

		// Token: 0x040007CC RID: 1996
		private string _decimalSeparator;

		// Token: 0x040007CD RID: 1997
		private string _fullTimeSpanPositivePattern;

		// Token: 0x040007CE RID: 1998
		private string _fullTimeSpanNegativePattern;

		// Token: 0x040007CF RID: 1999
		private DateTimeFormatInfo.TokenHashValue[] _dtfiTokenHash;

		// Token: 0x040007D0 RID: 2000
		private static volatile DateTimeFormatInfo s_jajpDTFI;

		// Token: 0x040007D1 RID: 2001
		private static volatile DateTimeFormatInfo s_zhtwDTFI;

		// Token: 0x020001F7 RID: 503
		internal class TokenHashValue
		{
			// Token: 0x06002078 RID: 8312 RVA: 0x0012A337 File Offset: 0x00129537
			internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
			{
				this.tokenString = tokenString;
				this.tokenType = tokenType;
				this.tokenValue = tokenValue;
			}

			// Token: 0x040007D2 RID: 2002
			internal string tokenString;

			// Token: 0x040007D3 RID: 2003
			internal TokenType tokenType;

			// Token: 0x040007D4 RID: 2004
			internal int tokenValue;
		}
	}
}
