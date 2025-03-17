using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020001EB RID: 491
	internal class CultureData
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00122B84 File Offset: 0x00121D84
		private static Dictionary<string, string> RegionNames
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = CultureData.s_regionNames) == null)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(257, StringComparer.OrdinalIgnoreCase);
					dictionary.Add("001", "en-001");
					dictionary.Add("029", "en-029");
					dictionary.Add("150", "en-150");
					dictionary.Add("419", "es-419");
					dictionary.Add("AD", "ca-AD");
					dictionary.Add("AE", "ar-AE");
					dictionary.Add("AF", "prs-AF");
					dictionary.Add("AG", "en-AG");
					dictionary.Add("AI", "en-AI");
					dictionary.Add("AL", "sq-AL");
					dictionary.Add("AM", "hy-AM");
					dictionary.Add("AO", "pt-AO");
					dictionary.Add("AQ", "en-A");
					dictionary.Add("AR", "es-AR");
					dictionary.Add("AS", "en-AS");
					dictionary.Add("AT", "de-AT");
					dictionary.Add("AU", "en-AU");
					dictionary.Add("AW", "nl-AW");
					dictionary.Add("AX", "sv-AX");
					dictionary.Add("AZ", "az-Cyrl-AZ");
					dictionary.Add("BA", "bs-Latn-BA");
					dictionary.Add("BB", "en-BB");
					dictionary.Add("BD", "bn-BD");
					dictionary.Add("BE", "nl-BE");
					dictionary.Add("BF", "fr-BF");
					dictionary.Add("BG", "bg-BG");
					dictionary.Add("BH", "ar-BH");
					dictionary.Add("BI", "rn-BI");
					dictionary.Add("BJ", "fr-BJ");
					dictionary.Add("BL", "fr-BL");
					dictionary.Add("BM", "en-BM");
					dictionary.Add("BN", "ms-BN");
					dictionary.Add("BO", "es-BO");
					dictionary.Add("BQ", "nl-BQ");
					dictionary.Add("BR", "pt-BR");
					dictionary.Add("BS", "en-BS");
					dictionary.Add("BT", "dz-BT");
					dictionary.Add("BV", "nb-B");
					dictionary.Add("BW", "en-BW");
					dictionary.Add("BY", "be-BY");
					dictionary.Add("BZ", "en-BZ");
					dictionary.Add("CA", "en-CA");
					dictionary.Add("CC", "en-CC");
					dictionary.Add("CD", "fr-CD");
					dictionary.Add("CF", "sg-CF");
					dictionary.Add("CG", "fr-CG");
					dictionary.Add("CH", "it-CH");
					dictionary.Add("CI", "fr-CI");
					dictionary.Add("CK", "en-CK");
					dictionary.Add("CL", "es-CL");
					dictionary.Add("CM", "fr-C");
					dictionary.Add("CN", "zh-CN");
					dictionary.Add("CO", "es-CO");
					dictionary.Add("CR", "es-CR");
					dictionary.Add("CS", "sr-Cyrl-CS");
					dictionary.Add("CU", "es-CU");
					dictionary.Add("CV", "pt-CV");
					dictionary.Add("CW", "nl-CW");
					dictionary.Add("CX", "en-CX");
					dictionary.Add("CY", "el-CY");
					dictionary.Add("CZ", "cs-CZ");
					dictionary.Add("DE", "de-DE");
					dictionary.Add("DJ", "fr-DJ");
					dictionary.Add("DK", "da-DK");
					dictionary.Add("DM", "en-DM");
					dictionary.Add("DO", "es-DO");
					dictionary.Add("DZ", "ar-DZ");
					dictionary.Add("EC", "es-EC");
					dictionary.Add("EE", "et-EE");
					dictionary.Add("EG", "ar-EG");
					dictionary.Add("ER", "tig-ER");
					dictionary.Add("ES", "es-ES");
					dictionary.Add("ET", "am-ET");
					dictionary.Add("FI", "fi-FI");
					dictionary.Add("FJ", "en-FJ");
					dictionary.Add("FK", "en-FK");
					dictionary.Add("FM", "en-FM");
					dictionary.Add("FO", "fo-FO");
					dictionary.Add("FR", "fr-FR");
					dictionary.Add("GA", "fr-GA");
					dictionary.Add("GB", "en-GB");
					dictionary.Add("GD", "en-GD");
					dictionary.Add("GE", "ka-GE");
					dictionary.Add("GF", "fr-GF");
					dictionary.Add("GG", "en-GG");
					dictionary.Add("GH", "en-GH");
					dictionary.Add("GI", "en-GI");
					dictionary.Add("GL", "kl-GL");
					dictionary.Add("GM", "en-GM");
					dictionary.Add("GN", "fr-GN");
					dictionary.Add("GP", "fr-GP");
					dictionary.Add("GQ", "es-GQ");
					dictionary.Add("GR", "el-GR");
					dictionary.Add("GS", "en-G");
					dictionary.Add("GT", "es-GT");
					dictionary.Add("GU", "en-GU");
					dictionary.Add("GW", "pt-GW");
					dictionary.Add("GY", "en-GY");
					dictionary.Add("HK", "zh-HK");
					dictionary.Add("HM", "en-H");
					dictionary.Add("HN", "es-HN");
					dictionary.Add("HR", "hr-HR");
					dictionary.Add("HT", "fr-HT");
					dictionary.Add("HU", "hu-HU");
					dictionary.Add("ID", "id-ID");
					dictionary.Add("IE", "en-IE");
					dictionary.Add("IL", "he-IL");
					dictionary.Add("IM", "gv-IM");
					dictionary.Add("IN", "hi-IN");
					dictionary.Add("IO", "en-IO");
					dictionary.Add("IQ", "ar-IQ");
					dictionary.Add("IR", "fa-IR");
					dictionary.Add("IS", "is-IS");
					dictionary.Add("IT", "it-IT");
					dictionary.Add("IV", "");
					dictionary.Add("JE", "en-JE");
					dictionary.Add("JM", "en-JM");
					dictionary.Add("JO", "ar-JO");
					dictionary.Add("JP", "ja-JP");
					dictionary.Add("KE", "sw-KE");
					dictionary.Add("KG", "ky-KG");
					dictionary.Add("KH", "km-KH");
					dictionary.Add("KI", "en-KI");
					dictionary.Add("KM", "ar-KM");
					dictionary.Add("KN", "en-KN");
					dictionary.Add("KP", "ko-KP");
					dictionary.Add("KR", "ko-KR");
					dictionary.Add("KW", "ar-KW");
					dictionary.Add("KY", "en-KY");
					dictionary.Add("KZ", "kk-KZ");
					dictionary.Add("LA", "lo-LA");
					dictionary.Add("LB", "ar-LB");
					dictionary.Add("LC", "en-LC");
					dictionary.Add("LI", "de-LI");
					dictionary.Add("LK", "si-LK");
					dictionary.Add("LR", "en-LR");
					dictionary.Add("LS", "st-LS");
					dictionary.Add("LT", "lt-LT");
					dictionary.Add("LU", "lb-LU");
					dictionary.Add("LV", "lv-LV");
					dictionary.Add("LY", "ar-LY");
					dictionary.Add("MA", "ar-MA");
					dictionary.Add("MC", "fr-MC");
					dictionary.Add("MD", "ro-MD");
					dictionary.Add("ME", "sr-Latn-ME");
					dictionary.Add("MF", "fr-MF");
					dictionary.Add("MG", "mg-MG");
					dictionary.Add("MH", "en-MH");
					dictionary.Add("MK", "mk-MK");
					dictionary.Add("ML", "fr-ML");
					dictionary.Add("MM", "my-MM");
					dictionary.Add("MN", "mn-MN");
					dictionary.Add("MO", "zh-MO");
					dictionary.Add("MP", "en-MP");
					dictionary.Add("MQ", "fr-MQ");
					dictionary.Add("MR", "ar-MR");
					dictionary.Add("MS", "en-MS");
					dictionary.Add("MT", "mt-MT");
					dictionary.Add("MU", "en-MU");
					dictionary.Add("MV", "dv-MV");
					dictionary.Add("MW", "en-MW");
					dictionary.Add("MX", "es-MX");
					dictionary.Add("MY", "ms-MY");
					dictionary.Add("MZ", "pt-MZ");
					dictionary.Add("NA", "en-NA");
					dictionary.Add("NC", "fr-NC");
					dictionary.Add("NE", "fr-NE");
					dictionary.Add("NF", "en-NF");
					dictionary.Add("NG", "ig-NG");
					dictionary.Add("NI", "es-NI");
					dictionary.Add("NL", "nl-NL");
					dictionary.Add("NO", "nn-NO");
					dictionary.Add("NP", "ne-NP");
					dictionary.Add("NR", "en-NR");
					dictionary.Add("NU", "en-NU");
					dictionary.Add("NZ", "en-NZ");
					dictionary.Add("OM", "ar-OM");
					dictionary.Add("PA", "es-PA");
					dictionary.Add("PE", "es-PE");
					dictionary.Add("PF", "fr-PF");
					dictionary.Add("PG", "en-PG");
					dictionary.Add("PH", "en-PH");
					dictionary.Add("PK", "ur-PK");
					dictionary.Add("PL", "pl-PL");
					dictionary.Add("PM", "fr-PM");
					dictionary.Add("PN", "en-PN");
					dictionary.Add("PR", "es-PR");
					dictionary.Add("PS", "ar-PS");
					dictionary.Add("PT", "pt-PT");
					dictionary.Add("PW", "en-PW");
					dictionary.Add("PY", "es-PY");
					dictionary.Add("QA", "ar-QA");
					dictionary.Add("RE", "fr-RE");
					dictionary.Add("RO", "ro-RO");
					dictionary.Add("RS", "sr-Latn-RS");
					dictionary.Add("RU", "ru-RU");
					dictionary.Add("RW", "rw-RW");
					dictionary.Add("SA", "ar-SA");
					dictionary.Add("SB", "en-SB");
					dictionary.Add("SC", "fr-SC");
					dictionary.Add("SD", "ar-SD");
					dictionary.Add("SE", "sv-SE");
					dictionary.Add("SG", "zh-SG");
					dictionary.Add("SH", "en-SH");
					dictionary.Add("SI", "sl-SI");
					dictionary.Add("SJ", "nb-SJ");
					dictionary.Add("SK", "sk-SK");
					dictionary.Add("SL", "en-SL");
					dictionary.Add("SM", "it-SM");
					dictionary.Add("SN", "wo-SN");
					dictionary.Add("SO", "so-SO");
					dictionary.Add("SR", "nl-SR");
					dictionary.Add("SS", "en-SS");
					dictionary.Add("ST", "pt-ST");
					dictionary.Add("SV", "es-SV");
					dictionary.Add("SX", "nl-SX");
					dictionary.Add("SY", "ar-SY");
					dictionary.Add("SZ", "ss-SZ");
					dictionary.Add("TC", "en-TC");
					dictionary.Add("TD", "fr-TD");
					dictionary.Add("TF", "fr-T");
					dictionary.Add("TG", "fr-TG");
					dictionary.Add("TH", "th-TH");
					dictionary.Add("TJ", "tg-Cyrl-TJ");
					dictionary.Add("TK", "en-TK");
					dictionary.Add("TL", "pt-TL");
					dictionary.Add("TM", "tk-TM");
					dictionary.Add("TN", "ar-TN");
					dictionary.Add("TO", "to-TO");
					dictionary.Add("TR", "tr-TR");
					dictionary.Add("TT", "en-TT");
					dictionary.Add("TV", "en-TV");
					dictionary.Add("TW", "zh-TW");
					dictionary.Add("TZ", "sw-TZ");
					dictionary.Add("UA", "uk-UA");
					dictionary.Add("UG", "sw-UG");
					dictionary.Add("UM", "en-UM");
					dictionary.Add("US", "en-US");
					dictionary.Add("UY", "es-UY");
					dictionary.Add("UZ", "uz-Cyrl-UZ");
					dictionary.Add("VA", "it-VA");
					dictionary.Add("VC", "en-VC");
					dictionary.Add("VE", "es-VE");
					dictionary.Add("VG", "en-VG");
					dictionary.Add("VI", "en-VI");
					dictionary.Add("VN", "vi-VN");
					dictionary.Add("VU", "fr-VU");
					dictionary.Add("WF", "fr-WF");
					dictionary.Add("WS", "en-WS");
					dictionary.Add("XK", "sq-XK");
					dictionary.Add("YE", "ar-YE");
					dictionary.Add("YT", "fr-YT");
					dictionary.Add("ZA", "af-ZA");
					dictionary.Add("ZM", "en-ZM");
					dictionary.Add("ZW", "en-ZW");
					result = dictionary;
					CultureData.s_regionNames = dictionary;
				}
				return result;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00123BA8 File Offset: 0x00122DA8
		internal static CultureData GetCultureDataForRegion(string cultureName, bool useUserOverride)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = CultureData.GetCultureData(cultureName, useUserOverride);
			if (cultureData != null && !cultureData.IsNeutralCulture)
			{
				return cultureData;
			}
			CultureData cultureData2 = cultureData;
			string key = CultureData.AnsiToLower(useUserOverride ? cultureName : (cultureName + "*"));
			Dictionary<string, CultureData> dictionary = CultureData.s_cachedRegions;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, CultureData>();
			}
			else
			{
				object obj = CultureData.s_lock;
				lock (obj)
				{
					dictionary.TryGetValue(key, out cultureData);
				}
				if (cultureData != null)
				{
					return cultureData;
				}
			}
			string cultureName2;
			if ((cultureData == null || cultureData.IsNeutralCulture) && CultureData.RegionNames.TryGetValue(cultureName, out cultureName2))
			{
				cultureData = CultureData.GetCultureData(cultureName2, useUserOverride);
			}
			if (!GlobalizationMode.Invariant && (cultureData == null || cultureData.IsNeutralCulture))
			{
				cultureData = (GlobalizationMode.UseNls ? CultureData.NlsGetCultureDataFromRegionName(cultureName) : CultureData.IcuGetCultureDataFromRegionName(cultureName));
			}
			if (cultureData != null && !cultureData.IsNeutralCulture)
			{
				object obj2 = CultureData.s_lock;
				lock (obj2)
				{
					dictionary[key] = cultureData;
				}
				CultureData.s_cachedRegions = dictionary;
			}
			else
			{
				cultureData = cultureData2;
			}
			return cultureData;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00123CDC File Offset: 0x00122EDC
		internal static void ClearCachedData()
		{
			CultureData.s_cachedCultures = null;
			CultureData.s_cachedRegions = null;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00123CF0 File Offset: 0x00122EF0
		internal static CultureInfo[] GetCultures(CultureTypes types)
		{
			if (types <= (CultureTypes)0 || (types & ~(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures | CultureTypes.InstalledWin32Cultures | CultureTypes.UserCustomCulture | CultureTypes.ReplacementCultures | CultureTypes.WindowsOnlyCultures | CultureTypes.FrameworkCultures)) != (CultureTypes)0)
			{
				throw new ArgumentOutOfRangeException("types", SR.Format(SR.ArgumentOutOfRange_Range, CultureTypes.NeutralCultures, CultureTypes.FrameworkCultures));
			}
			if ((types & CultureTypes.WindowsOnlyCultures) != (CultureTypes)0)
			{
				types &= ~CultureTypes.WindowsOnlyCultures;
			}
			if (GlobalizationMode.Invariant)
			{
				return new CultureInfo[]
				{
					new CultureInfo("")
				};
			}
			if (!GlobalizationMode.UseNls)
			{
				return CultureData.IcuEnumCultures(types);
			}
			return CultureData.NlsEnumCultures(types);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00123D64 File Offset: 0x00122F64
		private static CultureData CreateCultureWithInvariantData()
		{
			CultureData cultureData = new CultureData();
			cultureData._bUseOverrides = false;
			cultureData._bUseOverridesUserSetting = false;
			cultureData._sRealName = "";
			cultureData._sWindowsName = "";
			cultureData._sName = "";
			cultureData._sParent = "";
			cultureData._bNeutral = false;
			cultureData._sEnglishDisplayName = "Invariant Language (Invariant Country)";
			cultureData._sNativeDisplayName = "Invariant Language (Invariant Country)";
			cultureData._sSpecificCulture = "";
			cultureData._sISO639Language = "iv";
			cultureData._sISO639Language2 = "ivl";
			cultureData._sLocalizedLanguage = "Invariant Language";
			cultureData._sEnglishLanguage = "Invariant Language";
			cultureData._sNativeLanguage = "Invariant Language";
			cultureData._sAbbrevLang = "IVL";
			cultureData._sConsoleFallbackName = "";
			cultureData._iInputLanguageHandle = 127;
			cultureData._sRegionName = "IV";
			cultureData._sEnglishCountry = "Invariant Country";
			cultureData._sNativeCountry = "Invariant Country";
			cultureData._sISO3166CountryName = "IV";
			cultureData._sISO3166CountryName2 = "ivc";
			cultureData._iGeoId = 244;
			cultureData._sPositiveSign = "+";
			cultureData._sNegativeSign = "-";
			cultureData._iDigits = 2;
			cultureData._iNegativeNumber = 1;
			cultureData._waGrouping = new int[]
			{
				3
			};
			cultureData._sDecimalSeparator = ".";
			cultureData._sThousandSeparator = ",";
			cultureData._sNaN = "NaN";
			cultureData._sPositiveInfinity = "Infinity";
			cultureData._sNegativeInfinity = "-Infinity";
			cultureData._iNegativePercent = 0;
			cultureData._iPositivePercent = 0;
			cultureData._sPercent = "%";
			cultureData._sPerMille = "‰";
			cultureData._sCurrency = "¤";
			cultureData._sIntlMonetarySymbol = "XDR";
			cultureData._sEnglishCurrency = "International Monetary Fund";
			cultureData._sNativeCurrency = "International Monetary Fund";
			cultureData._iCurrencyDigits = 2;
			cultureData._iCurrency = 0;
			cultureData._iNegativeCurrency = 0;
			cultureData._waMonetaryGrouping = new int[]
			{
				3
			};
			cultureData._sMonetaryDecimal = ".";
			cultureData._sMonetaryThousand = ",";
			cultureData._iMeasure = 0;
			cultureData._sListSeparator = ",";
			cultureData._sTimeSeparator = ":";
			cultureData._sAM1159 = "AM";
			cultureData._sPM2359 = "PM";
			cultureData._saLongTimes = new string[]
			{
				"HH:mm:ss"
			};
			cultureData._saShortTimes = new string[]
			{
				"HH:mm",
				"hh:mm tt",
				"H:mm",
				"h:mm tt"
			};
			cultureData._saDurationFormats = new string[]
			{
				"HH:mm:ss"
			};
			cultureData._iFirstDayOfWeek = 0;
			cultureData._iFirstWeekOfYear = 0;
			cultureData._waCalendars = new CalendarId[]
			{
				CalendarId.GREGORIAN
			};
			if (!GlobalizationMode.Invariant)
			{
				cultureData._calendars = new CalendarData[23];
				cultureData._calendars[0] = CalendarData.Invariant;
			}
			cultureData._iReadingLayout = 0;
			cultureData._iLanguage = 127;
			cultureData._iDefaultAnsiCodePage = 1252;
			cultureData._iDefaultOemCodePage = 437;
			cultureData._iDefaultMacCodePage = 10000;
			cultureData._iDefaultEbcdicCodePage = 37;
			if (GlobalizationMode.Invariant)
			{
				cultureData._sLocalizedDisplayName = cultureData._sNativeDisplayName;
				cultureData._sLocalizedCountry = cultureData._sNativeCountry;
			}
			return cultureData;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0012408C File Offset: 0x0012328C
		internal static CultureData Invariant
		{
			get
			{
				CultureData result;
				if ((result = CultureData.s_Invariant) == null)
				{
					result = (CultureData.s_Invariant = CultureData.CreateCultureWithInvariantData());
				}
				return result;
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x001240A8 File Offset: 0x001232A8
		internal static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			string key = CultureData.AnsiToLower(useUserOverride ? cultureName : (cultureName + "*"));
			Dictionary<string, CultureData> dictionary = CultureData.s_cachedCultures;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, CultureData>();
			}
			else
			{
				object obj = CultureData.s_lock;
				CultureData cultureData;
				bool flag2;
				lock (obj)
				{
					flag2 = dictionary.TryGetValue(key, out cultureData);
				}
				if (flag2 && cultureData != null)
				{
					return cultureData;
				}
			}
			CultureData cultureData2 = CultureData.CreateCultureData(cultureName, useUserOverride);
			if (cultureData2 == null)
			{
				return null;
			}
			object obj2 = CultureData.s_lock;
			lock (obj2)
			{
				dictionary[key] = cultureData2;
			}
			CultureData.s_cachedCultures = dictionary;
			return cultureData2;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00124180 File Offset: 0x00123380
		private unsafe static string NormalizeCultureName(string name, out bool isNeutralName)
		{
			isNeutralName = true;
			int i = 0;
			if (name.Length > 85)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidId, "name"));
			}
			int length = name.Length;
			Span<char> span = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)length) * 2)], length);
			Span<char> span2 = span;
			bool flag = false;
			while (i < name.Length && name[i] != '-' && name[i] != '_')
			{
				if (name[i] >= 'A' && name[i] <= 'Z')
				{
					*span2[i] = name[i] + ' ';
					flag = true;
				}
				else
				{
					*span2[i] = name[i];
				}
				i++;
			}
			if (i < name.Length)
			{
				isNeutralName = false;
			}
			while (i < name.Length)
			{
				if (name[i] >= 'a' && name[i] <= 'z')
				{
					*span2[i] = name[i] - ' ';
					flag = true;
				}
				else
				{
					*span2[i] = name[i];
				}
				i++;
			}
			if (flag)
			{
				return new string(span2);
			}
			return name;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00124298 File Offset: 0x00123498
		private static CultureData CreateCultureData(string cultureName, bool useUserOverride)
		{
			if (GlobalizationMode.Invariant)
			{
				if (cultureName.Length > 85 || !CultureInfo.VerifyCultureName(cultureName, false))
				{
					return null;
				}
				CultureData cultureData = CultureData.CreateCultureWithInvariantData();
				cultureData._sName = CultureData.NormalizeCultureName(cultureName, out cultureData._bNeutral);
				cultureData._bUseOverridesUserSetting = useUserOverride;
				cultureData._sRealName = cultureData._sName;
				cultureData._sWindowsName = cultureData._sName;
				cultureData._iLanguage = 4096;
				return cultureData;
			}
			else
			{
				if (cultureName.Length == 1 && (cultureName[0] == 'C' || cultureName[0] == 'c'))
				{
					return CultureData.Invariant;
				}
				CultureData cultureData2 = new CultureData();
				cultureData2._sRealName = cultureName;
				cultureData2._bUseOverridesUserSetting = useUserOverride;
				if (!cultureData2.InitCultureDataCore() && !cultureData2.InitCompatibilityCultureData())
				{
					return null;
				}
				cultureData2.InitUserOverride(useUserOverride);
				return cultureData2;
			}
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00124358 File Offset: 0x00123558
		private bool InitCompatibilityCultureData()
		{
			string sRealName = this._sRealName;
			string a = CultureData.AnsiToLower(sRealName);
			string text;
			string sName;
			if (!(a == "zh-chs"))
			{
				if (!(a == "zh-cht"))
				{
					return false;
				}
				text = "zh-Hant";
				sName = "zh-CHT";
			}
			else
			{
				text = "zh-Hans";
				sName = "zh-CHS";
			}
			this._sRealName = text;
			if (!this.InitCultureDataCore())
			{
				return false;
			}
			this._sName = sName;
			this._sParent = text;
			return true;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x001243D0 File Offset: 0x001235D0
		internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
		{
			CultureData cultureData = null;
			if (culture == 127)
			{
				return CultureData.Invariant;
			}
			if (GlobalizationMode.Invariant)
			{
				throw new CultureNotFoundException("culture", culture, SR.Argument_CultureNotSupported);
			}
			string text = CultureData.LCIDToLocaleName(culture);
			if (!string.IsNullOrEmpty(text))
			{
				cultureData = CultureData.GetCultureData(text, bUseUserOverride);
			}
			if (cultureData == null)
			{
				throw new CultureNotFoundException("culture", culture, SR.Argument_CultureNotSupported);
			}
			return cultureData;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00124430 File Offset: 0x00123630
		internal string CultureName
		{
			get
			{
				string sName = this._sName;
				if (sName == "zh-CHS" || sName == "zh-CHT")
				{
					return this._sName;
				}
				return this._sRealName;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0012446B File Offset: 0x0012366B
		internal bool UseUserOverride
		{
			get
			{
				return this._bUseOverridesUserSetting;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00124473 File Offset: 0x00123673
		internal string Name
		{
			get
			{
				return this._sName ?? string.Empty;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00124484 File Offset: 0x00123684
		internal string ParentName
		{
			get
			{
				string result;
				if ((result = this._sParent) == null)
				{
					result = (this._sParent = this.GetLocaleInfoCore(this._sRealName, CultureData.LocaleStringData.ParentName));
				}
				return result;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x001244B4 File Offset: 0x001236B4
		internal string DisplayName
		{
			get
			{
				string text = this._sLocalizedDisplayName;
				if (text == null && !GlobalizationMode.Invariant)
				{
					if (this.IsSupplementalCustomCulture)
					{
						if (this.IsNeutralCulture)
						{
							text = this.NativeLanguageName;
						}
						else
						{
							text = this.NativeName;
						}
					}
					else
					{
						try
						{
							if (this.Name.Equals("zh-CHT", StringComparison.OrdinalIgnoreCase))
							{
								text = this.GetLanguageDisplayNameCore("zh-Hant");
							}
							else if (this.Name.Equals("zh-CHS", StringComparison.OrdinalIgnoreCase))
							{
								text = this.GetLanguageDisplayNameCore("zh-Hans");
							}
							else
							{
								text = this.GetLanguageDisplayNameCore(this.Name);
							}
						}
						catch
						{
						}
					}
					if (string.IsNullOrEmpty(text))
					{
						CultureInfo userDefaultCulture;
						if (this.IsNeutralCulture)
						{
							text = this.LocalizedLanguageName;
						}
						else if (CultureInfo.DefaultThreadCurrentUICulture != null && (userDefaultCulture = CultureInfo.GetUserDefaultCulture()) != null && !CultureInfo.DefaultThreadCurrentUICulture.Name.Equals(userDefaultCulture.Name))
						{
							text = this.NativeName;
						}
						else
						{
							text = this.GetLocaleInfoCore(CultureData.LocaleStringData.LocalizedDisplayName);
						}
					}
					this._sLocalizedDisplayName = text;
				}
				return text;
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x001245B8 File Offset: 0x001237B8
		private string GetLanguageDisplayNameCore(string cultureName)
		{
			if (!GlobalizationMode.UseNls)
			{
				return CultureData.IcuGetLanguageDisplayName(cultureName);
			}
			return this.NlsGetLanguageDisplayName(cultureName);
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x001245D0 File Offset: 0x001237D0
		internal string EnglishName
		{
			get
			{
				string text = this._sEnglishDisplayName;
				if (text == null && !GlobalizationMode.Invariant)
				{
					if (this.IsNeutralCulture)
					{
						text = this.EnglishLanguageName;
						string sName = this._sName;
						if (sName == "zh-CHS" || sName == "zh-CHT")
						{
							text += " Legacy";
						}
					}
					else
					{
						text = this.GetLocaleInfoCore(CultureData.LocaleStringData.EnglishDisplayName);
						if (string.IsNullOrEmpty(text))
						{
							string englishLanguageName = this.EnglishLanguageName;
							int index = englishLanguageName.Length - 1;
							if (englishLanguageName[index] == ')')
							{
								text = this.EnglishLanguageName.AsSpan(0, this._sEnglishLanguage.Length - 1) + ", " + this.EnglishCountryName + ")";
							}
							else
							{
								text = this.EnglishLanguageName + " (" + this.EnglishCountryName + ")";
							}
						}
					}
					this._sEnglishDisplayName = text;
				}
				return text;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x001246C8 File Offset: 0x001238C8
		internal string NativeName
		{
			get
			{
				string text = this._sNativeDisplayName;
				if (text == null && !GlobalizationMode.Invariant)
				{
					if (this.IsNeutralCulture)
					{
						text = this.NativeLanguageName;
						string sName = this._sName;
						if (!(sName == "zh-CHS"))
						{
							if (sName == "zh-CHT")
							{
								text += " 舊版";
							}
						}
						else
						{
							text += " 旧版";
						}
					}
					else
					{
						text = this.GetLocaleInfoCore(CultureData.LocaleStringData.NativeDisplayName);
						if (string.IsNullOrEmpty(text))
						{
							text = this.NativeLanguageName + " (" + this.NativeCountryName + ")";
						}
					}
					this._sNativeDisplayName = text;
				}
				return text;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0012476F File Offset: 0x0012396F
		internal string SpecificCultureName
		{
			get
			{
				return this._sSpecificCulture;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00124778 File Offset: 0x00123978
		internal string TwoLetterISOLanguageName
		{
			get
			{
				string result;
				if ((result = this._sISO639Language) == null)
				{
					result = (this._sISO639Language = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso639LanguageTwoLetterName));
				}
				return result;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x001247A0 File Offset: 0x001239A0
		internal string ThreeLetterISOLanguageName
		{
			get
			{
				string result;
				if ((result = this._sISO639Language2) == null)
				{
					result = (this._sISO639Language2 = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso639LanguageThreeLetterName));
				}
				return result;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x001247C8 File Offset: 0x001239C8
		internal string ThreeLetterWindowsLanguageName
		{
			get
			{
				string result;
				if ((result = this._sAbbrevLang) == null)
				{
					result = (this._sAbbrevLang = (GlobalizationMode.UseNls ? this.NlsGetThreeLetterWindowsLanguageName(this._sRealName) : CultureData.IcuGetThreeLetterWindowsLanguageName(this._sRealName)));
				}
				return result;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00124808 File Offset: 0x00123A08
		private string LocalizedLanguageName
		{
			get
			{
				if (this._sLocalizedLanguage == null && !GlobalizationMode.Invariant)
				{
					CultureInfo userDefaultCulture;
					if (CultureInfo.DefaultThreadCurrentUICulture != null && (userDefaultCulture = CultureInfo.GetUserDefaultCulture()) != null && !CultureInfo.DefaultThreadCurrentUICulture.Name.Equals(userDefaultCulture.Name))
					{
						this._sLocalizedLanguage = this.NativeLanguageName;
					}
					else
					{
						this._sLocalizedLanguage = this.GetLocaleInfoCore(CultureData.LocaleStringData.LocalizedLanguageName);
					}
				}
				return this._sLocalizedLanguage;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00124870 File Offset: 0x00123A70
		private string EnglishLanguageName
		{
			get
			{
				string result;
				if ((result = this._sEnglishLanguage) == null)
				{
					result = (this._sEnglishLanguage = this.GetLocaleInfoCore(CultureData.LocaleStringData.EnglishLanguageName));
				}
				return result;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0012489C File Offset: 0x00123A9C
		private string NativeLanguageName
		{
			get
			{
				string result;
				if ((result = this._sNativeLanguage) == null)
				{
					result = (this._sNativeLanguage = this.GetLocaleInfoCore(CultureData.LocaleStringData.NativeLanguageName));
				}
				return result;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x001248C4 File Offset: 0x00123AC4
		internal string RegionName
		{
			get
			{
				string result;
				if ((result = this._sRegionName) == null)
				{
					result = (this._sRegionName = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso3166CountryName));
				}
				return result;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x001248EC File Offset: 0x00123AEC
		internal int GeoId
		{
			get
			{
				if (this._iGeoId == -1 && !GlobalizationMode.Invariant)
				{
					this._iGeoId = (GlobalizationMode.UseNls ? this.NlsGetLocaleInfo(CultureData.LocaleNumberData.GeoId) : CultureData.IcuGetGeoId(this._sRealName));
				}
				return this._iGeoId;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x00124928 File Offset: 0x00123B28
		internal string LocalizedCountryName
		{
			get
			{
				string text = this._sLocalizedCountry;
				if (text == null && !GlobalizationMode.Invariant)
				{
					try
					{
						text = (GlobalizationMode.UseNls ? this.NlsGetRegionDisplayName() : CultureData.IcuGetRegionDisplayName());
					}
					catch
					{
					}
					if (text == null)
					{
						text = this.NativeCountryName;
					}
					this._sLocalizedCountry = text;
				}
				return text;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00124984 File Offset: 0x00123B84
		internal string EnglishCountryName
		{
			get
			{
				string result;
				if ((result = this._sEnglishCountry) == null)
				{
					result = (this._sEnglishCountry = this.GetLocaleInfoCore(CultureData.LocaleStringData.EnglishCountryName));
				}
				return result;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x001249B0 File Offset: 0x00123BB0
		internal string NativeCountryName
		{
			get
			{
				string result;
				if ((result = this._sNativeCountry) == null)
				{
					result = (this._sNativeCountry = this.GetLocaleInfoCore(CultureData.LocaleStringData.NativeCountryName));
				}
				return result;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x001249D8 File Offset: 0x00123BD8
		internal string TwoLetterISOCountryName
		{
			get
			{
				string result;
				if ((result = this._sISO3166CountryName) == null)
				{
					result = (this._sISO3166CountryName = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso3166CountryName));
				}
				return result;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00124A00 File Offset: 0x00123C00
		internal string ThreeLetterISOCountryName
		{
			get
			{
				string result;
				if ((result = this._sISO3166CountryName2) == null)
				{
					result = (this._sISO3166CountryName2 = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso3166CountryName2));
				}
				return result;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x00124A28 File Offset: 0x00123C28
		internal int KeyboardLayoutId
		{
			get
			{
				if (this._iInputLanguageHandle == -1)
				{
					if (this.IsSupplementalCustomCulture)
					{
						this._iInputLanguageHandle = 1033;
					}
					else
					{
						this._iInputLanguageHandle = this.LCID;
					}
				}
				return this._iInputLanguageHandle;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00124A5C File Offset: 0x00123C5C
		internal string SCONSOLEFALLBACKNAME
		{
			get
			{
				string result;
				if ((result = this._sConsoleFallbackName) == null)
				{
					result = (this._sConsoleFallbackName = (GlobalizationMode.UseNls ? this.NlsGetConsoleFallbackName(this._sRealName) : CultureData.IcuGetConsoleFallbackName(this._sRealName)));
				}
				return result;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x00124A9C File Offset: 0x00123C9C
		internal int[] NumberGroupSizes
		{
			get
			{
				int[] result;
				if ((result = this._waGrouping) == null)
				{
					result = (this._waGrouping = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleGroupingData.Digit));
				}
				return result;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x00124AC4 File Offset: 0x00123CC4
		private string NaNSymbol
		{
			get
			{
				string result;
				if ((result = this._sNaN) == null)
				{
					result = (this._sNaN = this.GetLocaleInfoCore(CultureData.LocaleStringData.NaNSymbol));
				}
				return result;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00124AEC File Offset: 0x00123CEC
		private string PositiveInfinitySymbol
		{
			get
			{
				string result;
				if ((result = this._sPositiveInfinity) == null)
				{
					result = (this._sPositiveInfinity = this.GetLocaleInfoCore(CultureData.LocaleStringData.PositiveInfinitySymbol));
				}
				return result;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x00124B14 File Offset: 0x00123D14
		private string NegativeInfinitySymbol
		{
			get
			{
				string result;
				if ((result = this._sNegativeInfinity) == null)
				{
					result = (this._sNegativeInfinity = this.GetLocaleInfoCore(CultureData.LocaleStringData.NegativeInfinitySymbol));
				}
				return result;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00124B3C File Offset: 0x00123D3C
		private int PercentNegativePattern
		{
			get
			{
				if (this._iNegativePercent == -1)
				{
					this._iNegativePercent = this.GetLocaleInfoCore(CultureData.LocaleNumberData.NegativePercentFormat);
				}
				return this._iNegativePercent;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00124B5B File Offset: 0x00123D5B
		private int PercentPositivePattern
		{
			get
			{
				if (this._iPositivePercent == -1)
				{
					this._iPositivePercent = this.GetLocaleInfoCore(CultureData.LocaleNumberData.PositivePercentFormat);
				}
				return this._iPositivePercent;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00124B7C File Offset: 0x00123D7C
		private string PercentSymbol
		{
			get
			{
				string result;
				if ((result = this._sPercent) == null)
				{
					result = (this._sPercent = this.GetLocaleInfoCore(CultureData.LocaleStringData.PercentSymbol));
				}
				return result;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x00124BA4 File Offset: 0x00123DA4
		private string PerMilleSymbol
		{
			get
			{
				string result;
				if ((result = this._sPerMille) == null)
				{
					result = (this._sPerMille = this.GetLocaleInfoCore(CultureData.LocaleStringData.PerMilleSymbol));
				}
				return result;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00124BCC File Offset: 0x00123DCC
		internal string CurrencySymbol
		{
			get
			{
				string result;
				if ((result = this._sCurrency) == null)
				{
					result = (this._sCurrency = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.MonetarySymbol));
				}
				return result;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00124BF4 File Offset: 0x00123DF4
		internal string ISOCurrencySymbol
		{
			get
			{
				string result;
				if ((result = this._sIntlMonetarySymbol) == null)
				{
					result = (this._sIntlMonetarySymbol = this.GetLocaleInfoCore(CultureData.LocaleStringData.Iso4217MonetarySymbol));
				}
				return result;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00124C1C File Offset: 0x00123E1C
		internal string CurrencyEnglishName
		{
			get
			{
				string result;
				if ((result = this._sEnglishCurrency) == null)
				{
					result = (this._sEnglishCurrency = this.GetLocaleInfoCore(CultureData.LocaleStringData.CurrencyEnglishName));
				}
				return result;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x00124C48 File Offset: 0x00123E48
		internal string CurrencyNativeName
		{
			get
			{
				string result;
				if ((result = this._sNativeCurrency) == null)
				{
					result = (this._sNativeCurrency = this.GetLocaleInfoCore(CultureData.LocaleStringData.CurrencyNativeName));
				}
				return result;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00124C74 File Offset: 0x00123E74
		internal int[] CurrencyGroupSizes
		{
			get
			{
				int[] result;
				if ((result = this._waMonetaryGrouping) == null)
				{
					result = (this._waMonetaryGrouping = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleGroupingData.Monetary));
				}
				return result;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00124C9C File Offset: 0x00123E9C
		internal int MeasurementSystem
		{
			get
			{
				if (this._iMeasure == -1)
				{
					this._iMeasure = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.MeasurementSystem);
				}
				return this._iMeasure;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00124CBC File Offset: 0x00123EBC
		internal string ListSeparator
		{
			get
			{
				string result;
				if ((result = this._sListSeparator) == null)
				{
					result = (this._sListSeparator = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.ListSeparator));
				}
				return result;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00124CE4 File Offset: 0x00123EE4
		internal string AMDesignator
		{
			get
			{
				string result;
				if ((result = this._sAM1159) == null)
				{
					result = (this._sAM1159 = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.AMDesignator));
				}
				return result;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00124D0C File Offset: 0x00123F0C
		internal string PMDesignator
		{
			get
			{
				string result;
				if ((result = this._sPM2359) == null)
				{
					result = (this._sPM2359 = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.PMDesignator));
				}
				return result;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00124D34 File Offset: 0x00123F34
		internal string[] LongTimes
		{
			get
			{
				if (this._saLongTimes == null && !GlobalizationMode.Invariant)
				{
					string[] timeFormatsCore = this.GetTimeFormatsCore(false);
					if (timeFormatsCore == null || timeFormatsCore.Length == 0)
					{
						this._saLongTimes = CultureData.Invariant._saLongTimes;
					}
					else
					{
						this._saLongTimes = timeFormatsCore;
					}
				}
				return this._saLongTimes;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00124D88 File Offset: 0x00123F88
		internal string[] ShortTimes
		{
			get
			{
				if (this._saShortTimes == null && !GlobalizationMode.Invariant)
				{
					string[] array = this.GetTimeFormatsCore(true);
					if (array == null || array.Length == 0)
					{
						array = this.DeriveShortTimesFromLong();
					}
					this._saShortTimes = array;
				}
				return this._saShortTimes;
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00124DD0 File Offset: 0x00123FD0
		private string[] DeriveShortTimesFromLong()
		{
			string[] longTimes = this.LongTimes;
			string[] array = new string[longTimes.Length];
			for (int i = 0; i < longTimes.Length; i++)
			{
				array[i] = CultureData.StripSecondsFromPattern(longTimes[i]);
			}
			return array;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00124E08 File Offset: 0x00124008
		private static string StripSecondsFromPattern(string time)
		{
			bool flag = false;
			int num = -1;
			for (int i = 0; i < time.Length; i++)
			{
				if (time[i] == '\'')
				{
					flag = !flag;
				}
				else if (time[i] == '\\')
				{
					i++;
				}
				else if (!flag)
				{
					char c = time[i];
					if (c <= 'h')
					{
						if (c != 'H' && c != 'h')
						{
							goto IL_D4;
						}
					}
					else if (c != 'm')
					{
						if (c == 's')
						{
							if (i - num <= 4 && i - num > 1 && time[num + 1] != '\'' && time[i - 1] != '\'' && num >= 0)
							{
								i = num + 1;
							}
							bool flag2;
							int indexOfNextTokenAfterSeconds = CultureData.GetIndexOfNextTokenAfterSeconds(time, i, out flag2);
							string value;
							if (flag2)
							{
								value = " ";
							}
							else
							{
								value = "";
							}
							time = time.AsSpan(0, i) + value + time.AsSpan(indexOfNextTokenAfterSeconds);
							goto IL_D4;
						}
						goto IL_D4;
					}
					num = i;
				}
				IL_D4:;
			}
			return time;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00124EFC File Offset: 0x001240FC
		private static int GetIndexOfNextTokenAfterSeconds(string time, int index, out bool containsSpace)
		{
			bool flag = false;
			containsSpace = false;
			while (index < time.Length)
			{
				char c = time[index];
				if (c <= 'H')
				{
					if (c != ' ')
					{
						if (c != '\'')
						{
							if (c == 'H')
							{
								goto IL_63;
							}
						}
						else
						{
							flag = !flag;
						}
					}
					else
					{
						containsSpace = true;
					}
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						if (c == 'h')
						{
							goto IL_63;
						}
					}
					else
					{
						index++;
						if (time[index] == ' ')
						{
							containsSpace = true;
						}
					}
				}
				else if (c == 'm' || c == 't')
				{
					goto IL_63;
				}
				IL_68:
				index++;
				continue;
				IL_63:
				if (!flag)
				{
					return index;
				}
				goto IL_68;
			}
			containsSpace = false;
			return index;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00124F83 File Offset: 0x00124183
		internal int FirstDayOfWeek
		{
			get
			{
				if (this._iFirstDayOfWeek == -1 && !GlobalizationMode.Invariant)
				{
					this._iFirstDayOfWeek = (this.ShouldUseUserOverrideNlsData ? this.NlsGetFirstDayOfWeek() : this.IcuGetLocaleInfo(CultureData.LocaleNumberData.FirstDayOfWeek));
				}
				return this._iFirstDayOfWeek;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00124FBC File Offset: 0x001241BC
		internal int CalendarWeekRule
		{
			get
			{
				if (this._iFirstWeekOfYear == -1)
				{
					this._iFirstWeekOfYear = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.FirstWeekOfYear);
				}
				return this._iFirstWeekOfYear;
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00124FDE File Offset: 0x001241DE
		internal string[] ShortDates(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00124FEC File Offset: 0x001241EC
		internal string[] LongDates(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00124FFA File Offset: 0x001241FA
		internal string[] YearMonths(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00125008 File Offset: 0x00124208
		internal string[] DayNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00125016 File Offset: 0x00124216
		internal string[] AbbreviatedDayNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00125024 File Offset: 0x00124224
		internal string[] SuperShortDayNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saSuperShortDayNames;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00125032 File Offset: 0x00124232
		internal string[] MonthNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00125040 File Offset: 0x00124240
		internal string[] GenitiveMonthNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0012504E File Offset: 0x0012424E
		internal string[] AbbreviatedMonthNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0012505C File Offset: 0x0012425C
		internal string[] AbbreviatedGenitiveMonthNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0012506A File Offset: 0x0012426A
		internal string[] LeapYearMonthNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00125078 File Offset: 0x00124278
		internal string MonthDay(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00125088 File Offset: 0x00124288
		internal CalendarId[] CalendarIds
		{
			get
			{
				if (this._waCalendars == null && !GlobalizationMode.Invariant)
				{
					CalendarId[] array = new CalendarId[23];
					int num = CalendarData.GetCalendarsCore(this._sWindowsName, this._bUseOverrides, array);
					if (num == 0)
					{
						this._waCalendars = CultureData.Invariant._waCalendars;
					}
					else
					{
						if (this._sWindowsName == "zh-TW")
						{
							bool flag = false;
							for (int i = 0; i < num; i++)
							{
								if (array[i] == CalendarId.TAIWAN)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								num++;
								Array.Copy(array, 1, array, 2, 21);
								array[1] = CalendarId.TAIWAN;
							}
						}
						CalendarId[] array2 = new CalendarId[num];
						Array.Copy(array, array2, num);
						this._waCalendars = array2;
					}
				}
				return this._waCalendars;
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00125145 File Offset: 0x00124345
		internal string CalendarName(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).sNativeName;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00125154 File Offset: 0x00124354
		internal CalendarData GetCalendar(CalendarId calendarId)
		{
			if (GlobalizationMode.Invariant)
			{
				return CalendarData.Invariant;
			}
			int num = (int)(calendarId - CalendarId.GREGORIAN);
			if (this._calendars == null)
			{
				this._calendars = new CalendarData[23];
			}
			CalendarData calendarData = this._calendars[num];
			if (calendarData == null)
			{
				calendarData = new CalendarData(this._sWindowsName, calendarId, this._bUseOverrides);
				this._calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x001251B0 File Offset: 0x001243B0
		internal bool IsRightToLeft
		{
			get
			{
				return this.ReadingLayout == 1;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x001251BB File Offset: 0x001243BB
		private int ReadingLayout
		{
			get
			{
				if (this._iReadingLayout == -1 && !GlobalizationMode.Invariant)
				{
					this._iReadingLayout = this.GetLocaleInfoCore(CultureData.LocaleNumberData.ReadingLayout);
				}
				return this._iReadingLayout;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x001251E1 File Offset: 0x001243E1
		internal string TextInfoName
		{
			get
			{
				return this._sRealName;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x001251E1 File Offset: 0x001243E1
		internal string SortName
		{
			get
			{
				return this._sRealName;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x001251E9 File Offset: 0x001243E9
		internal bool IsSupplementalCustomCulture
		{
			get
			{
				return CultureData.IsCustomCultureId(this.LCID);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x001251F6 File Offset: 0x001243F6
		internal int ANSICodePage
		{
			get
			{
				if (this._iDefaultAnsiCodePage == -1 && !GlobalizationMode.Invariant)
				{
					this._iDefaultAnsiCodePage = this.GetAnsiCodePage(this._sRealName);
				}
				return this._iDefaultAnsiCodePage;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00125220 File Offset: 0x00124420
		internal int OEMCodePage
		{
			get
			{
				if (this._iDefaultOemCodePage == -1 && !GlobalizationMode.Invariant)
				{
					this._iDefaultOemCodePage = this.GetOemCodePage(this._sRealName);
				}
				return this._iDefaultOemCodePage;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0012524A File Offset: 0x0012444A
		internal int MacCodePage
		{
			get
			{
				if (this._iDefaultMacCodePage == -1 && !GlobalizationMode.Invariant)
				{
					this._iDefaultMacCodePage = this.GetMacCodePage(this._sRealName);
				}
				return this._iDefaultMacCodePage;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00125274 File Offset: 0x00124474
		internal int EBCDICCodePage
		{
			get
			{
				if (this._iDefaultEbcdicCodePage == -1 && !GlobalizationMode.Invariant)
				{
					this._iDefaultEbcdicCodePage = this.GetEbcdicCodePage(this._sRealName);
				}
				return this._iDefaultEbcdicCodePage;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0012529E File Offset: 0x0012449E
		internal int LCID
		{
			get
			{
				if (this._iLanguage == 0 && !GlobalizationMode.Invariant)
				{
					this._iLanguage = (GlobalizationMode.UseNls ? CultureData.NlsLocaleNameToLCID(this._sRealName) : CultureData.IcuLocaleNameToLCID(this._sRealName));
				}
				return this._iLanguage;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x001252DA File Offset: 0x001244DA
		internal bool IsNeutralCulture
		{
			get
			{
				return this._bNeutral;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x001252E2 File Offset: 0x001244E2
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x001252EF File Offset: 0x001244EF
		internal bool IsReplacementCulture
		{
			get
			{
				return GlobalizationMode.UseNls && this.NlsIsReplacementCulture;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00125300 File Offset: 0x00124500
		internal Calendar DefaultCalendar
		{
			get
			{
				if (GlobalizationMode.Invariant)
				{
					return new GregorianCalendar();
				}
				CalendarId calendarId = (CalendarId)this.GetLocaleInfoCore(CultureData.LocaleNumberData.CalendarType);
				if (calendarId == CalendarId.UNINITIALIZED_VALUE)
				{
					calendarId = this.CalendarIds[0];
				}
				return CultureInfo.GetCalendarInstance(calendarId);
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00125339 File Offset: 0x00124539
		internal string[] EraNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00125347 File Offset: 0x00124547
		internal string[] AbbrevEraNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00125355 File Offset: 0x00124555
		internal string[] AbbreviatedEnglishEraNames(CalendarId calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00125364 File Offset: 0x00124564
		internal string TimeSeparator
		{
			get
			{
				if (this._sTimeSeparator == null && !GlobalizationMode.Invariant)
				{
					string text = this.ShouldUseUserOverrideNlsData ? this.NlsGetTimeFormatString() : this.IcuGetTimeFormatString();
					if (string.IsNullOrEmpty(text))
					{
						text = this.LongTimes[0];
					}
					this._sTimeSeparator = CultureData.GetTimeSeparator(text);
				}
				return this._sTimeSeparator;
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x001253BA File Offset: 0x001245BA
		internal string DateSeparator(CalendarId calendarId)
		{
			if (GlobalizationMode.Invariant)
			{
				return "/";
			}
			if (calendarId == CalendarId.JAPAN && !LocalAppContextSwitches.EnforceLegacyJapaneseDateParsing)
			{
				return "/";
			}
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x001253E8 File Offset: 0x001245E8
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0012548E File Offset: 0x0012468E
		private static string GetTimeSeparator(string format)
		{
			return CultureData.GetSeparator(format, "Hhms");
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0012549B File Offset: 0x0012469B
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x001254A8 File Offset: 0x001246A8
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0012550C File Offset: 0x0012470C
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.Contains(format[i]))
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						char c2 = format[i];
						if (c2 != '\'' && c2 != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0012557F File Offset: 0x0012477F
		internal static bool IsCustomCultureId(int cultureId)
		{
			return cultureId == 3072 || cultureId == 4096;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00125594 File Offset: 0x00124794
		internal void GetNFIValues(NumberFormatInfo nfi)
		{
			if (GlobalizationMode.Invariant || this.IsInvariantCulture)
			{
				nfi._positiveSign = this._sPositiveSign;
				nfi._negativeSign = this._sNegativeSign;
				nfi._numberGroupSeparator = this._sThousandSeparator;
				nfi._numberDecimalSeparator = this._sDecimalSeparator;
				nfi._numberDecimalDigits = this._iDigits;
				nfi._numberNegativePattern = this._iNegativeNumber;
				nfi._currencySymbol = this._sCurrency;
				nfi._currencyGroupSeparator = this._sMonetaryThousand;
				nfi._currencyDecimalSeparator = this._sMonetaryDecimal;
				nfi._currencyDecimalDigits = this._iCurrencyDigits;
				nfi._currencyNegativePattern = this._iNegativeCurrency;
				nfi._currencyPositivePattern = this._iCurrency;
			}
			else
			{
				nfi._positiveSign = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.PositiveSign);
				nfi._negativeSign = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.NegativeSign);
				nfi._numberDecimalSeparator = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.DecimalSeparator);
				nfi._numberGroupSeparator = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.ThousandSeparator);
				nfi._currencyGroupSeparator = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.MonetaryThousandSeparator);
				nfi._currencyDecimalSeparator = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.MonetaryDecimalSeparator);
				nfi._currencySymbol = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.MonetarySymbol);
				nfi._numberDecimalDigits = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.FractionalDigitsCount);
				nfi._currencyDecimalDigits = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.MonetaryFractionalDigitsCount);
				nfi._currencyPositivePattern = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.PositiveMonetaryNumberFormat);
				nfi._currencyNegativePattern = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.NegativeMonetaryNumberFormat);
				nfi._numberNegativePattern = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData.NegativeNumberFormat);
				string localeInfoCoreUserOverride = this.GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData.Digits);
				nfi._nativeDigits = new string[10];
				for (int i = 0; i < nfi._nativeDigits.Length; i++)
				{
					nfi._nativeDigits[i] = char.ToString(localeInfoCoreUserOverride[i]);
				}
				nfi._digitSubstitution = (this.ShouldUseUserOverrideNlsData ? this.NlsGetLocaleInfo(CultureData.LocaleNumberData.DigitSubstitution) : CultureData.IcuGetDigitSubstitution(this._sRealName));
			}
			nfi._numberGroupSizes = this.NumberGroupSizes;
			nfi._currencyGroupSizes = this.CurrencyGroupSizes;
			nfi._percentNegativePattern = this.PercentNegativePattern;
			nfi._percentPositivePattern = this.PercentPositivePattern;
			nfi._percentSymbol = this.PercentSymbol;
			nfi._perMilleSymbol = this.PerMilleSymbol;
			nfi._negativeInfinitySymbol = this.NegativeInfinitySymbol;
			nfi._positiveInfinitySymbol = this.PositiveInfinitySymbol;
			nfi._nanSymbol = this.NaNSymbol;
			nfi._percentDecimalDigits = nfi._numberDecimalDigits;
			nfi._percentDecimalSeparator = nfi._numberDecimalSeparator;
			nfi._percentGroupSizes = nfi._numberGroupSizes;
			nfi._percentGroupSeparator = nfi._numberGroupSeparator;
			if (string.IsNullOrEmpty(nfi._positiveSign))
			{
				nfi._positiveSign = "+";
			}
			if (string.IsNullOrEmpty(nfi._currencyDecimalSeparator))
			{
				nfi._currencyDecimalSeparator = nfi._numberDecimalSeparator;
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00125823 File Offset: 0x00124A23
		internal static string AnsiToLower(string testString)
		{
			return TextInfo.ToLowerAsciiInvariant(testString);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0012582B File Offset: 0x00124A2B
		private int GetLocaleInfoCore(CultureData.LocaleNumberData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return 0;
			}
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetLocaleInfo(type);
			}
			return this.NlsGetLocaleInfo(type);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0012584C File Offset: 0x00124A4C
		private int GetLocaleInfoCoreUserOverride(CultureData.LocaleNumberData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return 0;
			}
			if (!this.ShouldUseUserOverrideNlsData)
			{
				return this.IcuGetLocaleInfo(type);
			}
			return this.NlsGetLocaleInfo(type);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0012586E File Offset: 0x00124A6E
		private string GetLocaleInfoCoreUserOverride(CultureData.LocaleStringData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return null;
			}
			if (!this.ShouldUseUserOverrideNlsData)
			{
				return this.IcuGetLocaleInfo(type);
			}
			return this.NlsGetLocaleInfo(type);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00125890 File Offset: 0x00124A90
		private string GetLocaleInfoCore(CultureData.LocaleStringData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return null;
			}
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetLocaleInfo(type);
			}
			return this.NlsGetLocaleInfo(type);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x001258B1 File Offset: 0x00124AB1
		private string GetLocaleInfoCore(string localeName, CultureData.LocaleStringData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return null;
			}
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetLocaleInfo(localeName, type);
			}
			return this.NlsGetLocaleInfo(localeName, type);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x001258D4 File Offset: 0x00124AD4
		private int[] GetLocaleInfoCoreUserOverride(CultureData.LocaleGroupingData type)
		{
			if (GlobalizationMode.Invariant)
			{
				return null;
			}
			if (!this.ShouldUseUserOverrideNlsData)
			{
				return this.IcuGetLocaleInfo(type);
			}
			return this.NlsGetLocaleInfo(type);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x001258F6 File Offset: 0x00124AF6
		private string IcuGetLocaleInfo(CultureData.LocaleStringData type)
		{
			return this.IcuGetLocaleInfo(this._sWindowsName, type);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00125908 File Offset: 0x00124B08
		private unsafe string IcuGetLocaleInfo(string localeName, CultureData.LocaleStringData type)
		{
			if (type == CultureData.LocaleStringData.NegativeInfinitySymbol)
			{
				return this.IcuGetLocaleInfo(localeName, CultureData.LocaleStringData.NegativeSign) + this.IcuGetLocaleInfo(localeName, CultureData.LocaleStringData.PositiveInfinitySymbol);
			}
			char* value = stackalloc char[(UIntPtr)200];
			if (!Interop.Globalization.GetLocaleInfoString(localeName, (uint)type, value, 100))
			{
				return string.Empty;
			}
			return new string(value);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00125958 File Offset: 0x00124B58
		private int IcuGetLocaleInfo(CultureData.LocaleNumberData type)
		{
			if (type == CultureData.LocaleNumberData.CalendarType)
			{
				return 0;
			}
			int result = 0;
			bool localeInfoInt = Interop.Globalization.GetLocaleInfoInt(this._sWindowsName, (uint)type, ref result);
			return result;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00125984 File Offset: 0x00124B84
		private int[] IcuGetLocaleInfo(CultureData.LocaleGroupingData type)
		{
			int num = 0;
			int num2 = 0;
			bool localeInfoGroupingSizes = Interop.Globalization.GetLocaleInfoGroupingSizes(this._sWindowsName, (uint)type, ref num, ref num2);
			if (num2 == 0)
			{
				return new int[]
				{
					num
				};
			}
			return new int[]
			{
				num,
				num2
			};
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x001259C4 File Offset: 0x00124BC4
		private string IcuGetTimeFormatString()
		{
			return this.IcuGetTimeFormatString(false);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x001259D0 File Offset: 0x00124BD0
		private unsafe string IcuGetTimeFormatString(bool shortFormat)
		{
			char* ptr = stackalloc char[(UIntPtr)200];
			if (!Interop.Globalization.GetLocaleTimeFormat(this._sWindowsName, shortFormat, ptr, 100))
			{
				return string.Empty;
			}
			ReadOnlySpan<char> span = new ReadOnlySpan<char>((void*)ptr, 100);
			return CultureData.ConvertIcuTimeFormatString(span.Slice(0, span.IndexOf('\0')));
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000C26FD File Offset: 0x000C18FD
		private static CultureData IcuGetCultureDataFromRegionName(string regionName)
		{
			return null;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00125A1D File Offset: 0x00124C1D
		private static string IcuGetLanguageDisplayName(string cultureName)
		{
			return new CultureInfo(cultureName)._cultureData.IcuGetLocaleInfo(cultureName, CultureData.LocaleStringData.LocalizedDisplayName);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000C26FD File Offset: 0x000C18FD
		private static string IcuGetRegionDisplayName()
		{
			return null;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00125A34 File Offset: 0x00124C34
		private unsafe static string ConvertIcuTimeFormatString(ReadOnlySpan<char> icuFormatString)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)314], 157);
			Span<char> span2 = span;
			bool flag = false;
			int length = 0;
			int i = 0;
			while (i < icuFormatString.Length)
			{
				char c = (char)(*icuFormatString[i]);
				if (c <= 'H')
				{
					if (c <= '\'')
					{
						if (c == ' ')
						{
							goto IL_10C;
						}
						if (c == '\'')
						{
							*span2[length++] = (char)(*icuFormatString[i++]);
							while (i < icuFormatString.Length)
							{
								char c2 = (char)(*icuFormatString[i]);
								*span2[length++] = c2;
								if (c2 == '\'')
								{
									break;
								}
								i++;
							}
						}
					}
					else if (c == '.' || c == ':' || c == 'H')
					{
						goto IL_F3;
					}
				}
				else if (c <= 'h')
				{
					if (c != 'a')
					{
						if (c == 'h')
						{
							goto IL_F3;
						}
					}
					else if (!flag)
					{
						flag = true;
						*span2[length++] = 't';
						*span2[length++] = 't';
					}
				}
				else
				{
					if (c == 'm' || c == 's')
					{
						goto IL_F3;
					}
					if (c == '\u00a0')
					{
						goto IL_10C;
					}
				}
				IL_140:
				i++;
				continue;
				IL_F3:
				*span2[length++] = (char)(*icuFormatString[i]);
				goto IL_140;
				IL_10C:
				*span2[length++] = ' ';
				goto IL_140;
			}
			return span2.Slice(0, length).ToString();
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00125BAC File Offset: 0x00124DAC
		private static int IcuLocaleNameToLCID(string cultureName)
		{
			int localeDataNumericPart = IcuLocaleData.GetLocaleDataNumericPart(cultureName, IcuLocaleDataParts.Lcid);
			if (localeDataNumericPart != -1)
			{
				return localeDataNumericPart;
			}
			return 4096;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00125BCC File Offset: 0x00124DCC
		private static int IcuGetGeoId(string cultureName)
		{
			int localeDataNumericPart = IcuLocaleData.GetLocaleDataNumericPart(cultureName, IcuLocaleDataParts.GeoId);
			if (localeDataNumericPart != -1)
			{
				return localeDataNumericPart;
			}
			return CultureData.Invariant.GeoId;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00125BF4 File Offset: 0x00124DF4
		private static int IcuGetDigitSubstitution(string cultureName)
		{
			int localeDataNumericPart = IcuLocaleData.GetLocaleDataNumericPart(cultureName, IcuLocaleDataParts.DigitSubstitution);
			if (localeDataNumericPart != -1)
			{
				return localeDataNumericPart;
			}
			return 1;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00125C10 File Offset: 0x00124E10
		private static string IcuGetThreeLetterWindowsLanguageName(string cultureName)
		{
			return IcuLocaleData.GetThreeLetterWindowsLanguageName(cultureName) ?? "ZZZ";
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00125C24 File Offset: 0x00124E24
		private static CultureInfo[] IcuEnumCultures(CultureTypes types)
		{
			if ((types & (CultureTypes.NeutralCultures | CultureTypes.SpecificCultures)) == (CultureTypes)0)
			{
				return Array.Empty<CultureInfo>();
			}
			int locales = Interop.Globalization.GetLocales(null, 0);
			if (locales <= 0)
			{
				return Array.Empty<CultureInfo>();
			}
			char[] array = new char[locales];
			locales = Interop.Globalization.GetLocales(array, locales);
			if (locales <= 0)
			{
				return Array.Empty<CultureInfo>();
			}
			bool flag = (types & CultureTypes.NeutralCultures) > (CultureTypes)0;
			bool flag2 = (types & CultureTypes.SpecificCultures) > (CultureTypes)0;
			List<CultureInfo> list = new List<CultureInfo>();
			if (flag)
			{
				list.Add(CultureInfo.InvariantCulture);
			}
			int num;
			for (int i = 0; i < locales; i += num)
			{
				num = (int)array[i++];
				if (i + num <= locales)
				{
					CultureInfo cultureInfo = CultureInfo.GetCultureInfo(new string(array, i, num));
					if ((flag && cultureInfo.IsNeutralCulture) || (flag2 && !cultureInfo.IsNeutralCulture))
					{
						list.Add(cultureInfo);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00125CE8 File Offset: 0x00124EE8
		private static string IcuGetConsoleFallbackName(string cultureName)
		{
			return IcuLocaleData.GetConsoleUICulture(cultureName);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00125CF0 File Offset: 0x00124EF0
		internal unsafe static string GetLocaleInfoEx(string localeName, uint field)
		{
			char* ptr = stackalloc char[(UIntPtr)1060];
			int localeInfoEx = CultureData.GetLocaleInfoEx(localeName, field, ptr, 530);
			if (localeInfoEx > 0)
			{
				return new string(ptr);
			}
			return null;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00125D20 File Offset: 0x00124F20
		internal unsafe static int GetLocaleInfoExInt(string localeName, uint field)
		{
			field |= 536870912U;
			int result = 0;
			CultureData.GetLocaleInfoEx(localeName, field, (char*)(&result), 4);
			return result;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00125D45 File Offset: 0x00124F45
		internal unsafe static int GetLocaleInfoEx(string lpLocaleName, uint lcType, char* lpLCData, int cchData)
		{
			return Interop.Kernel32.GetLocaleInfoEx(lpLocaleName, lcType, (void*)lpLCData, cchData);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00125D50 File Offset: 0x00124F50
		private string NlsGetLocaleInfo(CultureData.LocaleStringData type)
		{
			return this.NlsGetLocaleInfo(this._sWindowsName, type);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00125D60 File Offset: 0x00124F60
		private string NlsGetLocaleInfo(string localeName, CultureData.LocaleStringData type)
		{
			return CultureData.GetLocaleInfoFromLCType(localeName, (uint)type, this._bUseOverrides);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00125D7C File Offset: 0x00124F7C
		private int NlsGetLocaleInfo(CultureData.LocaleNumberData type)
		{
			uint num = (uint)type;
			if (!this._bUseOverrides)
			{
				num |= 2147483648U;
			}
			return CultureData.GetLocaleInfoExInt(this._sWindowsName, num);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00125DA7 File Offset: 0x00124FA7
		private int[] NlsGetLocaleInfo(CultureData.LocaleGroupingData type)
		{
			return CultureData.ConvertWin32GroupString(CultureData.GetLocaleInfoFromLCType(this._sWindowsName, (uint)type, this._bUseOverrides));
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00125DC0 File Offset: 0x00124FC0
		private string NlsGetTimeFormatString()
		{
			return CultureData.ReescapeWin32String(CultureData.GetLocaleInfoFromLCType(this._sWindowsName, 4099U, this._bUseOverrides));
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00125DE0 File Offset: 0x00124FE0
		private int NlsGetFirstDayOfWeek()
		{
			int localeInfoExInt = CultureData.GetLocaleInfoExInt(this._sWindowsName, 4108U | ((!this._bUseOverrides) ? 2147483648U : 0U));
			return CultureData.ConvertFirstDayOfWeekMonToSun(localeInfoExInt);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00125E18 File Offset: 0x00125018
		private static CultureData NlsGetCultureDataFromRegionName(string regionName)
		{
			CultureData.EnumLocaleData enumLocaleData;
			enumLocaleData.cultureName = null;
			enumLocaleData.regionName = regionName;
			Interop.Kernel32.EnumSystemLocalesEx(ldftn(EnumSystemLocalesProc), '"', Unsafe.AsPointer<CultureData.EnumLocaleData>(ref enumLocaleData), IntPtr.Zero);
			if (enumLocaleData.cultureName != null)
			{
				return CultureData.GetCultureData(enumLocaleData.cultureName, true);
			}
			return null;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00125E68 File Offset: 0x00125068
		private string NlsGetLanguageDisplayName(string cultureName)
		{
			CultureInfo userDefaultCulture;
			if (CultureInfo.DefaultThreadCurrentUICulture != null && (userDefaultCulture = CultureInfo.GetUserDefaultCulture()) != null && !CultureInfo.DefaultThreadCurrentUICulture.Name.Equals(userDefaultCulture.Name))
			{
				return this.NativeName;
			}
			return this.NlsGetLocaleInfo(cultureName, CultureData.LocaleStringData.LocalizedDisplayName);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00125EAB File Offset: 0x001250AB
		private string NlsGetRegionDisplayName()
		{
			if (CultureInfo.CurrentUICulture.Name.Equals(CultureInfo.UserDefaultUICulture.Name))
			{
				return this.NlsGetLocaleInfo(CultureData.LocaleStringData.LocalizedCountryName);
			}
			return this.NativeCountryName;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00125ED6 File Offset: 0x001250D6
		private static string GetLocaleInfoFromLCType(string localeName, uint lctype, bool useUserOverride)
		{
			if (!useUserOverride)
			{
				lctype |= 2147483648U;
			}
			return CultureData.GetLocaleInfoEx(localeName, lctype) ?? string.Empty;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00125EF4 File Offset: 0x001250F4
		[return: NotNullIfNotNull("str")]
		internal static string ReescapeWin32String(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			bool flag = false;
			int i = 0;
			while (i < str.Length)
			{
				if (str[i] == '\'')
				{
					if (!flag)
					{
						flag = true;
						goto IL_91;
					}
					if (i + 1 >= str.Length || str[i + 1] != '\'')
					{
						flag = false;
						goto IL_91;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(str, 0, i, str.Length * 2);
					}
					stringBuilder.Append("\\'");
					i++;
				}
				else
				{
					if (str[i] != '\\')
					{
						goto IL_91;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(str, 0, i, str.Length * 2);
					}
					stringBuilder.Append("\\\\");
				}
				IL_A2:
				i++;
				continue;
				IL_91:
				if (stringBuilder != null)
				{
					stringBuilder.Append(str[i]);
					goto IL_A2;
				}
				goto IL_A2;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00125FC0 File Offset: 0x001251C0
		[return: NotNullIfNotNull("array")]
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CultureData.ReescapeWin32String(array[i]);
				}
			}
			return array;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00125FEC File Offset: 0x001251EC
		private static int[] ConvertWin32GroupString(string win32Str)
		{
			if (string.IsNullOrEmpty(win32Str))
			{
				return new int[]
				{
					3
				};
			}
			if (win32Str[0] == '0')
			{
				return new int[1];
			}
			int index = win32Str.Length - 1;
			int[] array;
			if (win32Str[index] == '0')
			{
				array = new int[win32Str.Length / 2];
			}
			else
			{
				array = new int[win32Str.Length / 2 + 2];
				int[] array2 = array;
				array2[array2.Length - 1] = 0;
			}
			int num = 0;
			int num2 = 0;
			while (num < win32Str.Length && num2 < array.Length)
			{
				if (win32Str[num] < '1' || win32Str[num] > '9')
				{
					return new int[]
					{
						3
					};
				}
				array[num2] = (int)(win32Str[num] - '0');
				num += 2;
				num2++;
			}
			return array;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x001260A6 File Offset: 0x001252A6
		private static int ConvertFirstDayOfWeekMonToSun(int iTemp)
		{
			iTemp++;
			if (iTemp > 6)
			{
				iTemp = 0;
			}
			return iTemp;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x001260B8 File Offset: 0x001252B8
		[UnmanagedCallersOnly]
		private unsafe static Interop.BOOL EnumSystemLocalesProc(char* lpLocaleString, uint flags, void* contextHandle)
		{
			ref CultureData.EnumLocaleData ptr = ref Unsafe.As<byte, CultureData.EnumLocaleData>(ref *(byte*)contextHandle);
			Interop.BOOL result;
			try
			{
				string text = new string(lpLocaleString);
				string localeInfoEx = CultureData.GetLocaleInfoEx(text, 90U);
				if (localeInfoEx != null && localeInfoEx.Equals(ptr.regionName, StringComparison.OrdinalIgnoreCase))
				{
					ptr.cultureName = text;
					result = Interop.BOOL.FALSE;
				}
				else
				{
					result = Interop.BOOL.TRUE;
				}
			}
			catch (Exception)
			{
				result = Interop.BOOL.FALSE;
			}
			return result;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00126114 File Offset: 0x00125314
		[UnmanagedCallersOnly]
		private unsafe static Interop.BOOL EnumAllSystemLocalesProc(char* lpLocaleString, uint flags, void* contextHandle)
		{
			ref CultureData.EnumData ptr = ref Unsafe.As<byte, CultureData.EnumData>(ref *(byte*)contextHandle);
			Interop.BOOL result;
			try
			{
				ptr.strings.Add(new string(lpLocaleString));
				result = Interop.BOOL.TRUE;
			}
			catch (Exception)
			{
				result = Interop.BOOL.FALSE;
			}
			return result;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00126154 File Offset: 0x00125354
		[UnmanagedCallersOnly]
		private unsafe static Interop.BOOL EnumTimeCallback(char* lpTimeFormatString, void* lParam)
		{
			ref CultureData.EnumData ptr = ref Unsafe.As<byte, CultureData.EnumData>(ref *(byte*)lParam);
			Interop.BOOL result;
			try
			{
				ptr.strings.Add(new string(lpTimeFormatString));
				result = Interop.BOOL.TRUE;
			}
			catch (Exception)
			{
				result = Interop.BOOL.FALSE;
			}
			return result;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00126194 File Offset: 0x00125394
		private unsafe static string[] nativeEnumTimeFormats(string localeName, uint dwFlags, bool useUserOverride)
		{
			CultureData.EnumData enumData = default(CultureData.EnumData);
			enumData.strings = new List<string>();
			Interop.Kernel32.EnumTimeFormatsEx(ldftn(EnumTimeCallback), localeName != null, (Interop.BOOL)dwFlags, (char*)Unsafe.AsPointer<CultureData.EnumData>(ref enumData));
			if (enumData.strings.Count > 0)
			{
				string[] array = enumData.strings.ToArray();
				if (!useUserOverride && enumData.strings.Count > 1)
				{
					uint lctype = (dwFlags == 2U) ? 121U : 4099U;
					string localeInfoFromLCType = CultureData.GetLocaleInfoFromLCType(localeName, lctype, useUserOverride);
					if (localeInfoFromLCType != "")
					{
						string text = array[0];
						if (localeInfoFromLCType != text)
						{
							array[0] = array[1];
							array[1] = text;
						}
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00126236 File Offset: 0x00125436
		private static int NlsLocaleNameToLCID(string cultureName)
		{
			return Interop.Kernel32.LocaleNameToLCID(cultureName, 134217728U);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00126243 File Offset: 0x00125443
		private string NlsGetThreeLetterWindowsLanguageName(string cultureName)
		{
			return this.NlsGetLocaleInfo(cultureName, CultureData.LocaleStringData.AbbreviatedWindowsLanguageName);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00126250 File Offset: 0x00125450
		private static CultureInfo[] NlsEnumCultures(CultureTypes types)
		{
			uint num = 0U;
			if ((types & (CultureTypes.InstalledWin32Cultures | CultureTypes.ReplacementCultures | CultureTypes.FrameworkCultures)) != (CultureTypes)0)
			{
				num |= 48U;
			}
			if ((types & CultureTypes.NeutralCultures) != (CultureTypes)0)
			{
				num |= 16U;
			}
			if ((types & CultureTypes.SpecificCultures) != (CultureTypes)0)
			{
				num |= 32U;
			}
			if ((types & CultureTypes.UserCustomCulture) != (CultureTypes)0)
			{
				num |= 2U;
			}
			if ((types & CultureTypes.ReplacementCultures) != (CultureTypes)0)
			{
				num |= 2U;
			}
			CultureData.EnumData enumData = default(CultureData.EnumData);
			enumData.strings = new List<string>();
			Interop.Kernel32.EnumSystemLocalesEx(ldftn(EnumAllSystemLocalesProc), (char)num, Unsafe.AsPointer<CultureData.EnumData>(ref enumData), IntPtr.Zero);
			CultureInfo[] array = new CultureInfo[enumData.strings.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new CultureInfo(enumData.strings[i]);
			}
			return array;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x001262F2 File Offset: 0x001254F2
		private string NlsGetConsoleFallbackName(string cultureName)
		{
			return this.NlsGetLocaleInfo(cultureName, CultureData.LocaleStringData.ConsoleFallbackName);
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00126300 File Offset: 0x00125500
		internal bool NlsIsReplacementCulture
		{
			get
			{
				CultureData.EnumData enumData = default(CultureData.EnumData);
				enumData.strings = new List<string>();
				Interop.Kernel32.EnumSystemLocalesEx(ldftn(EnumAllSystemLocalesProc), '\b', Unsafe.AsPointer<CultureData.EnumData>(ref enumData), IntPtr.Zero);
				for (int i = 0; i < enumData.strings.Count; i++)
				{
					if (string.Equals(enumData.strings[i], this._sWindowsName, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00126370 File Offset: 0x00125570
		private unsafe bool InitCultureDataCore()
		{
			string sRealName = this._sRealName;
			char* ptr = stackalloc char[(UIntPtr)170];
			int num = CultureData.GetLocaleInfoEx(sRealName, 92U, ptr, 85);
			if (num == 0)
			{
				return false;
			}
			this._sRealName = new string(ptr, 0, num - 1);
			sRealName = this._sRealName;
			if (CultureData.GetLocaleInfoEx(sRealName, 536871025U, ptr, 2) == 0)
			{
				return false;
			}
			this._bNeutral = (*(uint*)ptr > 0U);
			this._sWindowsName = sRealName;
			if (this._bNeutral)
			{
				this._sName = sRealName;
				num = Interop.Kernel32.ResolveLocaleName(sRealName, ptr, 85);
				if (num < 1)
				{
					return false;
				}
				this._sSpecificCulture = new string(ptr, 0, num - 1);
			}
			else
			{
				this._sSpecificCulture = sRealName;
				this._sName = sRealName;
				if (CultureData.GetLocaleInfoEx(sRealName, 536870913U, ptr, 2) == 0)
				{
					return false;
				}
				this._iLanguage = *(int*)ptr;
				if (!CultureData.IsCustomCultureId(this._iLanguage))
				{
					int num2 = sRealName.IndexOf('_');
					if (num2 > 0)
					{
						this._sName = sRealName.Substring(0, num2);
					}
				}
			}
			return true;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0012645D File Offset: 0x0012565D
		private void InitUserOverride(bool useUserOverride)
		{
			this._bUseOverrides = (useUserOverride && this._sWindowsName == CultureInfo.UserDefaultLocaleName);
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x000AC09E File Offset: 0x000AB29E
		internal static bool IsWin32Installed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0012647C File Offset: 0x0012567C
		internal unsafe static CultureData GetCurrentRegionData()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)20], 10);
			Span<char> span2 = span;
			int userGeoID = Interop.Kernel32.GetUserGeoID(16);
			if (userGeoID != -1)
			{
				int num;
				fixed (char* pinnableReference = span2.GetPinnableReference())
				{
					char* lpGeoData = pinnableReference;
					num = Interop.Kernel32.GetGeoInfo(userGeoID, 4, lpGeoData, span2.Length, 0);
				}
				if (num != 0)
				{
					num -= ((*span2[num - 1] == '\0') ? 1 : 0);
					CultureData cultureDataForRegion = CultureData.GetCultureDataForRegion(span2.Slice(0, num).ToString(), true);
					if (cultureDataForRegion != null)
					{
						return cultureDataForRegion;
					}
				}
			}
			return CultureInfo.CurrentCulture._cultureData;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00126510 File Offset: 0x00125710
		private unsafe static string LCIDToLocaleName(int culture)
		{
			char* ptr = stackalloc char[(UIntPtr)172];
			int num = Interop.Kernel32.LCIDToLocaleName(culture, ptr, 86, 134217728U);
			if (num > 0)
			{
				return new string(ptr);
			}
			return null;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00126544 File Offset: 0x00125744
		private string[] GetTimeFormatsCore(bool shortFormat)
		{
			if (GlobalizationMode.UseNls)
			{
				return CultureData.ReescapeWin32Strings(CultureData.nativeEnumTimeFormats(this._sWindowsName, shortFormat ? 2U : 0U, this._bUseOverrides));
			}
			string text = this.IcuGetTimeFormatString(shortFormat);
			if (!this._bUseOverrides)
			{
				return new string[]
				{
					text
				};
			}
			string localeInfoFromLCType = CultureData.GetLocaleInfoFromLCType(this._sWindowsName, shortFormat ? 121U : 4099U, true);
			if (!(localeInfoFromLCType != text))
			{
				return new string[]
				{
					localeInfoFromLCType
				};
			}
			return new string[]
			{
				localeInfoFromLCType,
				text
			};
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x001265CC File Offset: 0x001257CC
		private int GetAnsiCodePage(string cultureName)
		{
			return this.NlsGetLocaleInfo(CultureData.LocaleNumberData.AnsiCodePage);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x001265D9 File Offset: 0x001257D9
		private int GetOemCodePage(string cultureName)
		{
			return this.NlsGetLocaleInfo(CultureData.LocaleNumberData.OemCodePage);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x001265E3 File Offset: 0x001257E3
		private int GetMacCodePage(string cultureName)
		{
			return this.NlsGetLocaleInfo(CultureData.LocaleNumberData.MacCodePage);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x001265F0 File Offset: 0x001257F0
		private int GetEbcdicCodePage(string cultureName)
		{
			return this.NlsGetLocaleInfo(CultureData.LocaleNumberData.EbcdicCodePage);
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x001265FD File Offset: 0x001257FD
		private bool ShouldUseUserOverrideNlsData
		{
			get
			{
				return GlobalizationMode.UseNls || this._bUseOverrides;
			}
		}

		// Token: 0x040006E3 RID: 1763
		private string _sRealName;

		// Token: 0x040006E4 RID: 1764
		private string _sWindowsName;

		// Token: 0x040006E5 RID: 1765
		private string _sName;

		// Token: 0x040006E6 RID: 1766
		private string _sParent;

		// Token: 0x040006E7 RID: 1767
		private string _sLocalizedDisplayName;

		// Token: 0x040006E8 RID: 1768
		private string _sEnglishDisplayName;

		// Token: 0x040006E9 RID: 1769
		private string _sNativeDisplayName;

		// Token: 0x040006EA RID: 1770
		private string _sSpecificCulture;

		// Token: 0x040006EB RID: 1771
		private string _sISO639Language;

		// Token: 0x040006EC RID: 1772
		private string _sISO639Language2;

		// Token: 0x040006ED RID: 1773
		private string _sLocalizedLanguage;

		// Token: 0x040006EE RID: 1774
		private string _sEnglishLanguage;

		// Token: 0x040006EF RID: 1775
		private string _sNativeLanguage;

		// Token: 0x040006F0 RID: 1776
		private string _sAbbrevLang;

		// Token: 0x040006F1 RID: 1777
		private string _sConsoleFallbackName;

		// Token: 0x040006F2 RID: 1778
		private int _iInputLanguageHandle = -1;

		// Token: 0x040006F3 RID: 1779
		private string _sRegionName;

		// Token: 0x040006F4 RID: 1780
		private string _sLocalizedCountry;

		// Token: 0x040006F5 RID: 1781
		private string _sEnglishCountry;

		// Token: 0x040006F6 RID: 1782
		private string _sNativeCountry;

		// Token: 0x040006F7 RID: 1783
		private string _sISO3166CountryName;

		// Token: 0x040006F8 RID: 1784
		private string _sISO3166CountryName2;

		// Token: 0x040006F9 RID: 1785
		private int _iGeoId = -1;

		// Token: 0x040006FA RID: 1786
		private string _sPositiveSign;

		// Token: 0x040006FB RID: 1787
		private string _sNegativeSign;

		// Token: 0x040006FC RID: 1788
		private int _iDigits;

		// Token: 0x040006FD RID: 1789
		private int _iNegativeNumber;

		// Token: 0x040006FE RID: 1790
		private int[] _waGrouping;

		// Token: 0x040006FF RID: 1791
		private string _sDecimalSeparator;

		// Token: 0x04000700 RID: 1792
		private string _sThousandSeparator;

		// Token: 0x04000701 RID: 1793
		private string _sNaN;

		// Token: 0x04000702 RID: 1794
		private string _sPositiveInfinity;

		// Token: 0x04000703 RID: 1795
		private string _sNegativeInfinity;

		// Token: 0x04000704 RID: 1796
		private int _iNegativePercent = -1;

		// Token: 0x04000705 RID: 1797
		private int _iPositivePercent = -1;

		// Token: 0x04000706 RID: 1798
		private string _sPercent;

		// Token: 0x04000707 RID: 1799
		private string _sPerMille;

		// Token: 0x04000708 RID: 1800
		private string _sCurrency;

		// Token: 0x04000709 RID: 1801
		private string _sIntlMonetarySymbol;

		// Token: 0x0400070A RID: 1802
		private string _sEnglishCurrency;

		// Token: 0x0400070B RID: 1803
		private string _sNativeCurrency;

		// Token: 0x0400070C RID: 1804
		private int _iCurrencyDigits;

		// Token: 0x0400070D RID: 1805
		private int _iCurrency;

		// Token: 0x0400070E RID: 1806
		private int _iNegativeCurrency;

		// Token: 0x0400070F RID: 1807
		private int[] _waMonetaryGrouping;

		// Token: 0x04000710 RID: 1808
		private string _sMonetaryDecimal;

		// Token: 0x04000711 RID: 1809
		private string _sMonetaryThousand;

		// Token: 0x04000712 RID: 1810
		private int _iMeasure = -1;

		// Token: 0x04000713 RID: 1811
		private string _sListSeparator;

		// Token: 0x04000714 RID: 1812
		private string _sAM1159;

		// Token: 0x04000715 RID: 1813
		private string _sPM2359;

		// Token: 0x04000716 RID: 1814
		private string _sTimeSeparator;

		// Token: 0x04000717 RID: 1815
		private volatile string[] _saLongTimes;

		// Token: 0x04000718 RID: 1816
		private volatile string[] _saShortTimes;

		// Token: 0x04000719 RID: 1817
		private volatile string[] _saDurationFormats;

		// Token: 0x0400071A RID: 1818
		private int _iFirstDayOfWeek = -1;

		// Token: 0x0400071B RID: 1819
		private int _iFirstWeekOfYear = -1;

		// Token: 0x0400071C RID: 1820
		private volatile CalendarId[] _waCalendars;

		// Token: 0x0400071D RID: 1821
		private CalendarData[] _calendars;

		// Token: 0x0400071E RID: 1822
		private int _iReadingLayout = -1;

		// Token: 0x0400071F RID: 1823
		private int _iDefaultAnsiCodePage = -1;

		// Token: 0x04000720 RID: 1824
		private int _iDefaultOemCodePage = -1;

		// Token: 0x04000721 RID: 1825
		private int _iDefaultMacCodePage = -1;

		// Token: 0x04000722 RID: 1826
		private int _iDefaultEbcdicCodePage = -1;

		// Token: 0x04000723 RID: 1827
		private int _iLanguage;

		// Token: 0x04000724 RID: 1828
		private bool _bUseOverrides;

		// Token: 0x04000725 RID: 1829
		private bool _bUseOverridesUserSetting;

		// Token: 0x04000726 RID: 1830
		private bool _bNeutral;

		// Token: 0x04000727 RID: 1831
		private static volatile Dictionary<string, CultureData> s_cachedRegions;

		// Token: 0x04000728 RID: 1832
		private static volatile Dictionary<string, string> s_regionNames;

		// Token: 0x04000729 RID: 1833
		private static volatile CultureData s_Invariant;

		// Token: 0x0400072A RID: 1834
		private static volatile Dictionary<string, CultureData> s_cachedCultures;

		// Token: 0x0400072B RID: 1835
		private static readonly object s_lock = new object();

		// Token: 0x020001EC RID: 492
		private enum LocaleStringData : uint
		{
			// Token: 0x0400072D RID: 1837
			LocalizedDisplayName = 2U,
			// Token: 0x0400072E RID: 1838
			EnglishDisplayName = 114U,
			// Token: 0x0400072F RID: 1839
			NativeDisplayName,
			// Token: 0x04000730 RID: 1840
			LocalizedLanguageName = 111U,
			// Token: 0x04000731 RID: 1841
			EnglishLanguageName = 4097U,
			// Token: 0x04000732 RID: 1842
			NativeLanguageName = 4U,
			// Token: 0x04000733 RID: 1843
			LocalizedCountryName = 6U,
			// Token: 0x04000734 RID: 1844
			EnglishCountryName = 4098U,
			// Token: 0x04000735 RID: 1845
			NativeCountryName = 8U,
			// Token: 0x04000736 RID: 1846
			AbbreviatedWindowsLanguageName = 3U,
			// Token: 0x04000737 RID: 1847
			ListSeparator = 12U,
			// Token: 0x04000738 RID: 1848
			DecimalSeparator = 14U,
			// Token: 0x04000739 RID: 1849
			ThousandSeparator,
			// Token: 0x0400073A RID: 1850
			Digits = 19U,
			// Token: 0x0400073B RID: 1851
			MonetarySymbol,
			// Token: 0x0400073C RID: 1852
			CurrencyEnglishName = 4103U,
			// Token: 0x0400073D RID: 1853
			CurrencyNativeName,
			// Token: 0x0400073E RID: 1854
			Iso4217MonetarySymbol = 21U,
			// Token: 0x0400073F RID: 1855
			MonetaryDecimalSeparator,
			// Token: 0x04000740 RID: 1856
			MonetaryThousandSeparator,
			// Token: 0x04000741 RID: 1857
			AMDesignator = 40U,
			// Token: 0x04000742 RID: 1858
			PMDesignator,
			// Token: 0x04000743 RID: 1859
			PositiveSign = 80U,
			// Token: 0x04000744 RID: 1860
			NegativeSign,
			// Token: 0x04000745 RID: 1861
			Iso639LanguageTwoLetterName = 89U,
			// Token: 0x04000746 RID: 1862
			Iso639LanguageThreeLetterName = 103U,
			// Token: 0x04000747 RID: 1863
			Iso639LanguageName = 89U,
			// Token: 0x04000748 RID: 1864
			Iso3166CountryName,
			// Token: 0x04000749 RID: 1865
			Iso3166CountryName2 = 104U,
			// Token: 0x0400074A RID: 1866
			NaNSymbol,
			// Token: 0x0400074B RID: 1867
			PositiveInfinitySymbol,
			// Token: 0x0400074C RID: 1868
			NegativeInfinitySymbol,
			// Token: 0x0400074D RID: 1869
			ParentName = 109U,
			// Token: 0x0400074E RID: 1870
			ConsoleFallbackName,
			// Token: 0x0400074F RID: 1871
			PercentSymbol = 118U,
			// Token: 0x04000750 RID: 1872
			PerMilleSymbol
		}

		// Token: 0x020001ED RID: 493
		private enum LocaleGroupingData : uint
		{
			// Token: 0x04000752 RID: 1874
			Digit = 16U,
			// Token: 0x04000753 RID: 1875
			Monetary = 24U
		}

		// Token: 0x020001EE RID: 494
		private enum LocaleNumberData : uint
		{
			// Token: 0x04000755 RID: 1877
			LanguageId = 1U,
			// Token: 0x04000756 RID: 1878
			GeoId = 91U,
			// Token: 0x04000757 RID: 1879
			DigitSubstitution = 4116U,
			// Token: 0x04000758 RID: 1880
			MeasurementSystem = 13U,
			// Token: 0x04000759 RID: 1881
			FractionalDigitsCount = 17U,
			// Token: 0x0400075A RID: 1882
			NegativeNumberFormat = 4112U,
			// Token: 0x0400075B RID: 1883
			MonetaryFractionalDigitsCount = 25U,
			// Token: 0x0400075C RID: 1884
			PositiveMonetaryNumberFormat = 27U,
			// Token: 0x0400075D RID: 1885
			NegativeMonetaryNumberFormat,
			// Token: 0x0400075E RID: 1886
			CalendarType = 4105U,
			// Token: 0x0400075F RID: 1887
			FirstDayOfWeek = 4108U,
			// Token: 0x04000760 RID: 1888
			FirstWeekOfYear,
			// Token: 0x04000761 RID: 1889
			ReadingLayout = 112U,
			// Token: 0x04000762 RID: 1890
			NegativePercentFormat = 116U,
			// Token: 0x04000763 RID: 1891
			PositivePercentFormat,
			// Token: 0x04000764 RID: 1892
			OemCodePage = 11U,
			// Token: 0x04000765 RID: 1893
			AnsiCodePage = 4100U,
			// Token: 0x04000766 RID: 1894
			MacCodePage = 4113U,
			// Token: 0x04000767 RID: 1895
			EbcdicCodePage
		}

		// Token: 0x020001EF RID: 495
		private struct EnumLocaleData
		{
			// Token: 0x04000768 RID: 1896
			public string regionName;

			// Token: 0x04000769 RID: 1897
			public string cultureName;
		}

		// Token: 0x020001F0 RID: 496
		private struct EnumData
		{
			// Token: 0x0400076A RID: 1898
			public List<string> strings;
		}
	}
}
