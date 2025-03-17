using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020001F1 RID: 497
	[NullableContext(1)]
	[Nullable(0)]
	public class CultureInfo : IFormatProvider, ICloneable
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x00126683 File Offset: 0x00125883
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			CultureInfo.s_currentThreadCulture = args.CurrentValue;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00126691 File Offset: 0x00125891
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			CultureInfo.s_currentThreadUICulture = args.CurrentValue;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0012669F File Offset: 0x0012589F
		private static CultureInfo InitializeUserDefaultCulture()
		{
			Interlocked.CompareExchange<CultureInfo>(ref CultureInfo.s_userDefaultCulture, CultureInfo.GetUserDefaultCulture(), null);
			return CultureInfo.s_userDefaultCulture;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x001266B9 File Offset: 0x001258B9
		private static CultureInfo InitializeUserDefaultUICulture()
		{
			Interlocked.CompareExchange<CultureInfo>(ref CultureInfo.s_userDefaultUICulture, CultureInfo.GetUserDefaultUICulture(), null);
			return CultureInfo.s_userDefaultUICulture;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x001266D3 File Offset: 0x001258D3
		public CultureInfo(string name) : this(name, true)
		{
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x001266E0 File Offset: 0x001258E0
		public CultureInfo(string name, bool useUserOverride)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CultureData cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (cultureData == null)
			{
				throw new CultureNotFoundException("name", name, SR.Argument_CultureNotSupported);
			}
			this._cultureData = cultureData;
			this._name = this._cultureData.CultureName;
			this._isInherited = (base.GetType() != typeof(CultureInfo));
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00126750 File Offset: 0x00125950
		private CultureInfo(CultureData cultureData, bool isReadOnly = false)
		{
			this._cultureData = cultureData;
			this._name = cultureData.CultureName;
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00126774 File Offset: 0x00125974
		private static CultureInfo CreateCultureInfoNoThrow(string name, bool useUserOverride)
		{
			CultureData cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (cultureData == null)
			{
				return null;
			}
			return new CultureInfo(cultureData, false);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00126795 File Offset: 0x00125995
		public CultureInfo(int culture) : this(culture, true)
		{
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x001267A0 File Offset: 0x001259A0
		public CultureInfo(int culture, bool useUserOverride)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (culture <= 1024)
			{
				if (culture != 0 && culture != 1024)
				{
					goto IL_58;
				}
			}
			else if (culture != 2048 && culture != 3072 && culture != 4096)
			{
				goto IL_58;
			}
			throw new CultureNotFoundException("culture", culture, SR.Argument_CultureNotSupported);
			IL_58:
			this._cultureData = CultureData.GetCultureData(culture, useUserOverride);
			this._isInherited = (base.GetType() != typeof(CultureInfo));
			this._name = this._cultureData.CultureName;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00126840 File Offset: 0x00125A40
		internal CultureInfo(string cultureName, string textAndCompareCultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName", SR.ArgumentNull_String);
			}
			CultureData cultureData = CultureData.GetCultureData(cultureName, false);
			if (cultureData == null)
			{
				throw new CultureNotFoundException("cultureName", cultureName, SR.Argument_CultureNotSupported);
			}
			CultureData cultureData2 = cultureData;
			this._cultureData = cultureData2;
			this._name = this._cultureData.CultureName;
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
			this._compareInfo = cultureInfo.CompareInfo;
			this._textInfo = cultureInfo.TextInfo;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x001268BC File Offset: 0x00125ABC
		private static CultureInfo GetCultureByName(string name)
		{
			CultureInfo result;
			try
			{
				result = new CultureInfo(name)
				{
					_isReadOnly = true
				};
			}
			catch (ArgumentException)
			{
				result = CultureInfo.InvariantCulture;
			}
			return result;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x001268F4 File Offset: 0x00125AF4
		public static CultureInfo CreateSpecificCulture(string name)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo(name);
			}
			catch (ArgumentException)
			{
				cultureInfo = null;
				for (int i = 0; i < name.Length; i++)
				{
					if ('-' == name[i])
					{
						try
						{
							cultureInfo = new CultureInfo(name.Substring(0, i));
							break;
						}
						catch (ArgumentException)
						{
							throw;
						}
					}
				}
				if (cultureInfo == null)
				{
					throw;
				}
			}
			if (!cultureInfo.IsNeutralCulture)
			{
				return cultureInfo;
			}
			return new CultureInfo(cultureInfo._cultureData.SpecificCultureName);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0012697C File Offset: 0x00125B7C
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(SR.Format(SR.Argument_InvalidResourceCultureName, cultureName));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x001269CB File Offset: 0x00125BCB
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture._isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x001269E3 File Offset: 0x00125BE3
		// (set) Token: 0x06001FA7 RID: 8103 RVA: 0x00126A09 File Offset: 0x00125C09
		public static CultureInfo CurrentCulture
		{
			get
			{
				CultureInfo result;
				if ((result = CultureInfo.s_currentThreadCulture) == null && (result = CultureInfo.s_DefaultThreadCurrentCulture) == null)
				{
					result = (CultureInfo.s_userDefaultCulture ?? CultureInfo.InitializeUserDefaultCulture());
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (CultureInfo.s_asyncLocalCurrentCulture == null)
				{
					Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref CultureInfo.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(CultureInfo.AsyncLocalSetCurrentCulture)), null);
				}
				CultureInfo.s_asyncLocalCurrentCulture.Value = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x00126A48 File Offset: 0x00125C48
		// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x00126A64 File Offset: 0x00125C64
		public static CultureInfo CurrentUICulture
		{
			get
			{
				CultureInfo result;
				if ((result = CultureInfo.s_currentThreadUICulture) == null)
				{
					result = (CultureInfo.s_DefaultThreadCurrentUICulture ?? CultureInfo.UserDefaultUICulture);
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (CultureInfo.s_asyncLocalCurrentUICulture == null)
				{
					Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref CultureInfo.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(CultureInfo.AsyncLocalSetCurrentUICulture)), null);
				}
				CultureInfo.s_asyncLocalCurrentUICulture.Value = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00126AB6 File Offset: 0x00125CB6
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				return CultureInfo.s_userDefaultUICulture ?? CultureInfo.InitializeUserDefaultUICulture();
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00126AC8 File Offset: 0x00125CC8
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.s_userDefaultCulture ?? CultureInfo.InitializeUserDefaultCulture();
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x00126ADA File Offset: 0x00125CDA
		// (set) Token: 0x06001FAD RID: 8109 RVA: 0x00126AE3 File Offset: 0x00125CE3
		[Nullable(2)]
		public static CultureInfo DefaultThreadCurrentCulture
		{
			[NullableContext(2)]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			[NullableContext(2)]
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00126AED File Offset: 0x00125CED
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00126AF6 File Offset: 0x00125CF6
		[Nullable(2)]
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			[NullableContext(2)]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			[NullableContext(2)]
			set
			{
				if (value != null)
				{
					CultureInfo.VerifyCultureName(value, true);
				}
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x00126B0B File Offset: 0x00125D0B
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.s_InvariantCultureInfo;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00126B14 File Offset: 0x00125D14
		public virtual CultureInfo Parent
		{
			get
			{
				if (this._parent == null)
				{
					string parentName = this._cultureData.ParentName;
					CultureInfo value;
					if (string.IsNullOrEmpty(parentName))
					{
						value = CultureInfo.InvariantCulture;
					}
					else
					{
						value = (CultureInfo.CreateCultureInfoNoThrow(parentName, this._cultureData.UseUserOverride) ?? CultureInfo.InvariantCulture);
					}
					Interlocked.CompareExchange<CultureInfo>(ref this._parent, value, null);
				}
				return this._parent;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x00126B74 File Offset: 0x00125D74
		public virtual int LCID
		{
			get
			{
				return this._cultureData.LCID;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x00126B81 File Offset: 0x00125D81
		public virtual int KeyboardLayoutId
		{
			get
			{
				return this._cultureData.KeyboardLayoutId;
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00126B8E File Offset: 0x00125D8E
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
			{
				types |= CultureTypes.ReplacementCultures;
			}
			return CultureData.GetCultures(types);
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x00126BA4 File Offset: 0x00125DA4
		public virtual string Name
		{
			get
			{
				string result;
				if ((result = this._nonSortName) == null)
				{
					result = (this._nonSortName = (this._cultureData.Name ?? string.Empty));
				}
				return result;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00126BD8 File Offset: 0x00125DD8
		internal string SortName
		{
			get
			{
				string result;
				if ((result = this._sortName) == null)
				{
					result = (this._sortName = this._cultureData.SortName);
				}
				return result;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00126C04 File Offset: 0x00125E04
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				string result;
				if (!(name == "zh-CHT"))
				{
					if (!(name == "zh-CHS"))
					{
						result = this.Name;
					}
					else
					{
						result = "zh-Hans";
					}
				}
				else
				{
					result = "zh-Hant";
				}
				return result;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00126C4C File Offset: 0x00125E4C
		public virtual string DisplayName
		{
			get
			{
				return this._cultureData.DisplayName;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00126C59 File Offset: 0x00125E59
		public virtual string NativeName
		{
			get
			{
				return this._cultureData.NativeName;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x00126C66 File Offset: 0x00125E66
		public virtual string EnglishName
		{
			get
			{
				return this._cultureData.EnglishName;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x00126C73 File Offset: 0x00125E73
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				return this._cultureData.TwoLetterISOLanguageName;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x00126C80 File Offset: 0x00125E80
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				return this._cultureData.ThreeLetterISOLanguageName;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x00126C8D File Offset: 0x00125E8D
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				return this._cultureData.ThreeLetterWindowsLanguageName;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x00126C9C File Offset: 0x00125E9C
		public virtual CompareInfo CompareInfo
		{
			get
			{
				CompareInfo result;
				if ((result = this._compareInfo) == null)
				{
					result = (this._compareInfo = (this.UseUserOverride ? CultureInfo.GetCultureInfo(this._name).CompareInfo : new CompareInfo(this)));
				}
				return result;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00126CDC File Offset: 0x00125EDC
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this._textInfo == null)
				{
					TextInfo textInfo = new TextInfo(this._cultureData);
					textInfo.SetReadOnlyState(this._isReadOnly);
					this._textInfo = textInfo;
				}
				return this._textInfo;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00126D18 File Offset: 0x00125F18
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && this.Name.Equals(cultureInfo.Name) && this.CompareInfo.Equals(cultureInfo.CompareInfo);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00126D5D File Offset: 0x00125F5D
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00126D76 File Offset: 0x00125F76
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00126D7E File Offset: 0x00125F7E
		[NullableContext(2)]
		public virtual object GetFormat(Type formatType)
		{
			if (formatType == typeof(NumberFormatInfo))
			{
				return this.NumberFormat;
			}
			if (formatType == typeof(DateTimeFormatInfo))
			{
				return this.DateTimeFormat;
			}
			return null;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00126DB3 File Offset: 0x00125FB3
		public virtual bool IsNeutralCulture
		{
			get
			{
				return this._cultureData.IsNeutralCulture;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x00126DC0 File Offset: 0x00125FC0
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = this._cultureData.IsNeutralCulture ? CultureTypes.NeutralCultures : CultureTypes.SpecificCultures;
				bool isWin32Installed = CultureData.IsWin32Installed;
				cultureTypes |= CultureTypes.InstalledWin32Cultures;
				if (this._cultureData.IsSupplementalCustomCulture)
				{
					cultureTypes |= CultureTypes.UserCustomCulture;
				}
				if (this._cultureData.IsReplacementCulture)
				{
					cultureTypes |= CultureTypes.ReplacementCultures;
				}
				return cultureTypes;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00126E10 File Offset: 0x00126010
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00126E51 File Offset: 0x00126051
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (this._numInfo == null)
				{
					Interlocked.CompareExchange<NumberFormatInfo>(ref this._numInfo, new NumberFormatInfo(this._cultureData)
					{
						_isReadOnly = this._isReadOnly
					}, null);
				}
				return this._numInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._numInfo = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x00126E70 File Offset: 0x00126070
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00126EB7 File Offset: 0x001260B7
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (this._dateTimeInfo == null)
				{
					Interlocked.CompareExchange<DateTimeFormatInfo>(ref this._dateTimeInfo, new DateTimeFormatInfo(this._cultureData, this.Calendar)
					{
						_isReadOnly = this._isReadOnly
					}, null);
				}
				return this._dateTimeInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._dateTimeInfo = value;
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00126ED4 File Offset: 0x001260D4
		public void ClearCachedData()
		{
			CultureInfo.UserDefaultLocaleName = CultureInfo.GetUserDefaultLocaleName();
			CultureInfo.s_userDefaultCulture = CultureInfo.GetUserDefaultCulture();
			CultureInfo.s_userDefaultUICulture = CultureInfo.GetUserDefaultUICulture();
			RegionInfo.s_currentRegionInfo = null;
			TimeZone.ResetTimeZone();
			TimeZoneInfo.ClearCachedData();
			CultureInfo.s_cachedCulturesByLcid = null;
			CultureInfo.s_cachedCulturesByName = null;
			CultureData.ClearCachedData();
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00126F2A File Offset: 0x0012612A
		internal static Calendar GetCalendarInstance(CalendarId calType)
		{
			if (calType == CalendarId.GREGORIAN)
			{
				return new GregorianCalendar();
			}
			return CultureInfo.GetCalendarInstanceRare(calType);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00126F3C File Offset: 0x0012613C
		internal static Calendar GetCalendarInstanceRare(CalendarId calType)
		{
			switch (calType)
			{
			case CalendarId.GREGORIAN_US:
			case CalendarId.GREGORIAN_ME_FRENCH:
			case CalendarId.GREGORIAN_ARABIC:
			case CalendarId.GREGORIAN_XLIT_ENGLISH:
			case CalendarId.GREGORIAN_XLIT_FRENCH:
				return new GregorianCalendar((GregorianCalendarTypes)calType);
			case CalendarId.JAPAN:
				return new JapaneseCalendar();
			case CalendarId.TAIWAN:
				return new TaiwanCalendar();
			case CalendarId.KOREA:
				return new KoreanCalendar();
			case CalendarId.HIJRI:
				return new HijriCalendar();
			case CalendarId.THAI:
				return new ThaiBuddhistCalendar();
			case CalendarId.HEBREW:
				return new HebrewCalendar();
			case CalendarId.PERSIAN:
				return new PersianCalendar();
			case CalendarId.UMALQURA:
				return new UmAlQuraCalendar();
			}
			return new GregorianCalendar();
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x00126FE8 File Offset: 0x001261E8
		public virtual Calendar Calendar
		{
			get
			{
				if (this._calendar == null)
				{
					Calendar defaultCalendar = this._cultureData.DefaultCalendar;
					Interlocked.MemoryBarrier();
					defaultCalendar.SetReadOnlyState(this._isReadOnly);
					this._calendar = defaultCalendar;
				}
				return this._calendar;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00127028 File Offset: 0x00126228
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				if (GlobalizationMode.Invariant)
				{
					return new GregorianCalendar[]
					{
						new GregorianCalendar()
					};
				}
				CalendarId[] calendarIds = this._cultureData.CalendarIds;
				Calendar[] array = new Calendar[calendarIds.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CultureInfo.GetCalendarInstance(calendarIds[i]);
				}
				return array;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0012707C File Offset: 0x0012627C
		public bool UseUserOverride
		{
			get
			{
				return this._cultureData.UseUserOverride;
			}
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0012708C File Offset: 0x0012628C
		public CultureInfo GetConsoleFallbackUICulture()
		{
			CultureInfo cultureInfo = this._consoleFallbackCulture;
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CreateSpecificCulture(this._cultureData.SCONSOLEFALLBACKNAME);
				cultureInfo._isReadOnly = true;
				this._consoleFallbackCulture = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x001270C4 File Offset: 0x001262C4
		public virtual object Clone()
		{
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo._isReadOnly = false;
			if (!this._isInherited)
			{
				if (this._dateTimeInfo != null)
				{
					cultureInfo._dateTimeInfo = (DateTimeFormatInfo)this._dateTimeInfo.Clone();
				}
				if (this._numInfo != null)
				{
					cultureInfo._numInfo = (NumberFormatInfo)this._numInfo.Clone();
				}
			}
			else
			{
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
			}
			if (this._textInfo != null)
			{
				cultureInfo._textInfo = (TextInfo)this._textInfo.Clone();
			}
			if (this._dateTimeInfo != null && this._dateTimeInfo.Calendar == this._calendar)
			{
				cultureInfo._calendar = cultureInfo.DateTimeFormat.Calendar;
			}
			else if (this._calendar != null)
			{
				cultureInfo._calendar = (Calendar)this._calendar.Clone();
			}
			return cultureInfo;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x001271C4 File Offset: 0x001263C4
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.IsReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.MemberwiseClone();
			if (!ci.IsNeutralCulture)
			{
				if (!ci._isInherited)
				{
					if (ci._dateTimeInfo != null)
					{
						cultureInfo._dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci._dateTimeInfo);
					}
					if (ci._numInfo != null)
					{
						cultureInfo._numInfo = NumberFormatInfo.ReadOnly(ci._numInfo);
					}
				}
				else
				{
					cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
					cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
				}
			}
			if (ci._textInfo != null)
			{
				cultureInfo._textInfo = TextInfo.ReadOnly(ci._textInfo);
			}
			if (ci._calendar != null)
			{
				cultureInfo._calendar = Calendar.ReadOnly(ci._calendar);
			}
			cultureInfo._isReadOnly = true;
			return cultureInfo;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00127295 File Offset: 0x00126495
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0012729D File Offset: 0x0012649D
		private void VerifyWritable()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x001272B2 File Offset: 0x001264B2
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x001272CC File Offset: 0x001264CC
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture <= 0)
			{
				throw new ArgumentOutOfRangeException("culture", SR.ArgumentOutOfRange_NeedPosNum);
			}
			Dictionary<int, CultureInfo> cachedCulturesByLcid = CultureInfo.CachedCulturesByLcid;
			Dictionary<int, CultureInfo> obj = cachedCulturesByLcid;
			CultureInfo cultureInfo;
			lock (obj)
			{
				if (cachedCulturesByLcid.TryGetValue(culture, out cultureInfo))
				{
					return cultureInfo;
				}
			}
			try
			{
				cultureInfo = new CultureInfo(culture, false)
				{
					_isReadOnly = true
				};
			}
			catch (ArgumentException)
			{
				throw new CultureNotFoundException("culture", culture, SR.Argument_CultureNotSupported);
			}
			Dictionary<int, CultureInfo> obj2 = cachedCulturesByLcid;
			lock (obj2)
			{
				cachedCulturesByLcid[culture] = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00127390 File Offset: 0x00126590
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			name = CultureData.AnsiToLower(name);
			Dictionary<string, CultureInfo> cachedCulturesByName = CultureInfo.CachedCulturesByName;
			Dictionary<string, CultureInfo> obj = cachedCulturesByName;
			CultureInfo cultureInfo;
			lock (obj)
			{
				if (cachedCulturesByName.TryGetValue(name, out cultureInfo))
				{
					return cultureInfo;
				}
			}
			CultureInfo cultureInfo2 = CultureInfo.CreateCultureInfoNoThrow(name, false);
			if (cultureInfo2 == null)
			{
				throw new CultureNotFoundException("name", name, SR.Argument_CultureNotSupported);
			}
			cultureInfo = cultureInfo2;
			cultureInfo._isReadOnly = true;
			name = CultureData.AnsiToLower(cultureInfo._name);
			Dictionary<string, CultureInfo> obj2 = cachedCulturesByName;
			lock (obj2)
			{
				cachedCulturesByName[name] = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00127458 File Offset: 0x00126658
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("altName");
			}
			name = CultureData.AnsiToLower(name);
			altName = CultureData.AnsiToLower(altName);
			string key = name + "�" + altName;
			Dictionary<string, CultureInfo> cachedCulturesByName = CultureInfo.CachedCulturesByName;
			Dictionary<string, CultureInfo> obj = cachedCulturesByName;
			CultureInfo cultureInfo;
			lock (obj)
			{
				if (cachedCulturesByName.TryGetValue(key, out cultureInfo))
				{
					return cultureInfo;
				}
			}
			try
			{
				cultureInfo = new CultureInfo(name, altName)
				{
					_isReadOnly = true
				};
				cultureInfo.TextInfo.SetReadOnlyState(true);
			}
			catch (ArgumentException)
			{
				throw new CultureNotFoundException("name/altName", SR.Format(SR.Argument_OneOfCulturesNotSupported, name, altName));
			}
			Dictionary<string, CultureInfo> obj2 = cachedCulturesByName;
			lock (obj2)
			{
				cachedCulturesByName[key] = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00127558 File Offset: 0x00126758
		public static CultureInfo GetCultureInfo(string name, bool predefinedOnly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!predefinedOnly || GlobalizationMode.Invariant)
			{
				return CultureInfo.GetCultureInfo(name);
			}
			if (!GlobalizationMode.UseNls)
			{
				return CultureInfo.IcuGetPredefinedCultureInfo(name);
			}
			return CultureInfo.NlsGetPredefinedCultureInfo(name);
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00127590 File Offset: 0x00126790
		private static Dictionary<string, CultureInfo> CachedCulturesByName
		{
			get
			{
				Dictionary<string, CultureInfo> dictionary = CultureInfo.s_cachedCulturesByName;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, CultureInfo>();
					dictionary = (Interlocked.CompareExchange<Dictionary<string, CultureInfo>>(ref CultureInfo.s_cachedCulturesByName, dictionary, null) ?? dictionary);
				}
				return dictionary;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x001275C4 File Offset: 0x001267C4
		private static Dictionary<int, CultureInfo> CachedCulturesByLcid
		{
			get
			{
				Dictionary<int, CultureInfo> dictionary = CultureInfo.s_cachedCulturesByLcid;
				if (dictionary == null)
				{
					dictionary = new Dictionary<int, CultureInfo>();
					dictionary = (Interlocked.CompareExchange<Dictionary<int, CultureInfo>>(ref CultureInfo.s_cachedCulturesByLcid, dictionary, null) ?? dictionary);
				}
				return dictionary;
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x001275F8 File Offset: 0x001267F8
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-CHT" || name == "zh-CHS")
			{
				throw new CultureNotFoundException("name", SR.Format(SR.Argument_CultureIetfNotSupported, name));
			}
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
			if (cultureInfo.LCID > 65535 || cultureInfo.LCID == 1034)
			{
				throw new CultureNotFoundException("name", SR.Format(SR.Argument_CultureIetfNotSupported, name));
			}
			return cultureInfo;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0012766D File Offset: 0x0012686D
		private static CultureInfo IcuGetPredefinedCultureInfo(string name)
		{
			if (!Interop.Globalization.IsPredefinedLocale(name))
			{
				throw new CultureNotFoundException("name", SR.Format(SR.Argument_InvalidPredefinedCultureName, name));
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00127693 File Offset: 0x00126893
		private static CultureInfo NlsGetPredefinedCultureInfo(string name)
		{
			if (CultureData.GetLocaleInfoExInt(name, 125U) == 1)
			{
				throw new CultureNotFoundException("name", SR.Format(SR.Argument_InvalidPredefinedCultureName, name));
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x001276BC File Offset: 0x001268BC
		internal static CultureInfo GetUserDefaultCulture()
		{
			if (GlobalizationMode.Invariant)
			{
				return CultureInfo.InvariantCulture;
			}
			string userDefaultLocaleName = CultureInfo.UserDefaultLocaleName;
			if (userDefaultLocaleName == null)
			{
				return CultureInfo.InvariantCulture;
			}
			return CultureInfo.GetCultureByName(userDefaultLocaleName);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x001276EC File Offset: 0x001268EC
		private unsafe static CultureInfo GetUserDefaultUICulture()
		{
			if (GlobalizationMode.Invariant)
			{
				return CultureInfo.InvariantCulture;
			}
			uint num = 0U;
			uint num2 = 0U;
			if (Interop.Kernel32.GetUserPreferredUILanguages(8U, &num, null, &num2) != Interop.BOOL.FALSE)
			{
				Span<char> span2;
				if (num2 <= 256U)
				{
					int num3 = (int)num2;
					Span<char> span = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num3) * 2)], num3);
					span2 = span;
				}
				else
				{
					span2 = new char[num2];
				}
				Span<char> span3 = span2;
				fixed (char* pinnableReference = span3.GetPinnableReference())
				{
					char* pwszLanguagesBuffer = pinnableReference;
					if (Interop.Kernel32.GetUserPreferredUILanguages(8U, &num, pwszLanguagesBuffer, &num2) != Interop.BOOL.FALSE)
					{
						return CultureInfo.GetCultureByName(span3.ToString());
					}
				}
			}
			return CultureInfo.InitializeUserDefaultCulture();
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00127782 File Offset: 0x00126982
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00127789 File Offset: 0x00126989
		[Nullable(2)]
		internal static string UserDefaultLocaleName { get; set; } = CultureInfo.GetUserDefaultLocaleName();

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00127791 File Offset: 0x00126991
		private static string GetUserDefaultLocaleName()
		{
			string result;
			if (!GlobalizationMode.Invariant)
			{
				if ((result = CultureData.GetLocaleInfoEx(null, 92U)) == null)
				{
					return CultureData.GetLocaleInfoEx("!x-sys-default-locale", 92U);
				}
			}
			else
			{
				result = CultureInfo.InvariantCulture.Name;
			}
			return result;
		}

		// Token: 0x0400076B RID: 1899
		private bool _isReadOnly;

		// Token: 0x0400076C RID: 1900
		private CompareInfo _compareInfo;

		// Token: 0x0400076D RID: 1901
		private TextInfo _textInfo;

		// Token: 0x0400076E RID: 1902
		internal NumberFormatInfo _numInfo;

		// Token: 0x0400076F RID: 1903
		internal DateTimeFormatInfo _dateTimeInfo;

		// Token: 0x04000770 RID: 1904
		private Calendar _calendar;

		// Token: 0x04000771 RID: 1905
		internal CultureData _cultureData;

		// Token: 0x04000772 RID: 1906
		internal bool _isInherited;

		// Token: 0x04000773 RID: 1907
		private CultureInfo _consoleFallbackCulture;

		// Token: 0x04000774 RID: 1908
		internal string _name;

		// Token: 0x04000775 RID: 1909
		private string _nonSortName;

		// Token: 0x04000776 RID: 1910
		private string _sortName;

		// Token: 0x04000777 RID: 1911
		private static volatile CultureInfo s_userDefaultCulture;

		// Token: 0x04000778 RID: 1912
		private static volatile CultureInfo s_userDefaultUICulture;

		// Token: 0x04000779 RID: 1913
		private static readonly CultureInfo s_InvariantCultureInfo = new CultureInfo(CultureData.Invariant, true);

		// Token: 0x0400077A RID: 1914
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x0400077B RID: 1915
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x0400077C RID: 1916
		[ThreadStatic]
		private static CultureInfo s_currentThreadCulture;

		// Token: 0x0400077D RID: 1917
		[ThreadStatic]
		private static CultureInfo s_currentThreadUICulture;

		// Token: 0x0400077E RID: 1918
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x0400077F RID: 1919
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x04000780 RID: 1920
		private static volatile Dictionary<string, CultureInfo> s_cachedCulturesByName;

		// Token: 0x04000781 RID: 1921
		private static volatile Dictionary<int, CultureInfo> s_cachedCulturesByLcid;

		// Token: 0x04000782 RID: 1922
		private CultureInfo _parent;

		// Token: 0x04000783 RID: 1923
		internal const int LOCALE_NEUTRAL = 0;

		// Token: 0x04000784 RID: 1924
		private const int LOCALE_USER_DEFAULT = 1024;

		// Token: 0x04000785 RID: 1925
		private const int LOCALE_SYSTEM_DEFAULT = 2048;

		// Token: 0x04000786 RID: 1926
		internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;

		// Token: 0x04000787 RID: 1927
		internal const int LOCALE_CUSTOM_DEFAULT = 3072;

		// Token: 0x04000788 RID: 1928
		internal const int LOCALE_INVARIANT = 127;
	}
}
