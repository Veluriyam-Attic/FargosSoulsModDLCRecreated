using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000220 RID: 544
	[NullableContext(1)]
	[Nullable(0)]
	public class RegionInfo
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x00133850 File Offset: 0x00132A50
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_NoRegionInvariantCulture, "name");
			}
			CultureData cultureDataForRegion = CultureData.GetCultureDataForRegion(name, true);
			if (cultureDataForRegion == null)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidCultureName, name), "name");
			}
			this._cultureData = cultureDataForRegion;
			if (this._cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidNeutralRegionName, name), "name");
			}
			this._name = this._cultureData.RegionName;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x001338E4 File Offset: 0x00132AE4
		public RegionInfo(int culture)
		{
			if (culture == 127)
			{
				throw new ArgumentException(SR.Argument_NoRegionInvariantCulture);
			}
			if (culture == 0)
			{
				throw new ArgumentException(SR.Format(SR.Argument_CultureIsNeutral, culture), "culture");
			}
			if (culture == 3072)
			{
				throw new ArgumentException(SR.Format(SR.Argument_CustomCultureCannotBePassedByNumber, culture), "culture");
			}
			this._cultureData = CultureData.GetCultureData(culture, true);
			this._name = this._cultureData.RegionName;
			if (this._cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(SR.Format(SR.Argument_CultureIsNeutral, culture), "culture");
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0013398E File Offset: 0x00132B8E
		internal RegionInfo(CultureData cultureData)
		{
			this._cultureData = cultureData;
			this._name = this._cultureData.RegionName;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x001339B0 File Offset: 0x00132BB0
		public static RegionInfo CurrentRegion
		{
			get
			{
				RegionInfo regionInfo = RegionInfo.s_currentRegionInfo;
				if (regionInfo == null)
				{
					regionInfo = new RegionInfo(CultureData.GetCurrentRegionData());
					regionInfo._name = regionInfo._cultureData.RegionName;
					RegionInfo.s_currentRegionInfo = regionInfo;
				}
				return regionInfo;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x001339ED File Offset: 0x00132BED
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x001339F5 File Offset: 0x00132BF5
		public virtual string EnglishName
		{
			get
			{
				return this._cultureData.EnglishCountryName;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00133A02 File Offset: 0x00132C02
		public virtual string DisplayName
		{
			get
			{
				return this._cultureData.LocalizedCountryName;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x00133A0F File Offset: 0x00132C0F
		public virtual string NativeName
		{
			get
			{
				return this._cultureData.NativeCountryName;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00133A1C File Offset: 0x00132C1C
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this._cultureData.TwoLetterISOCountryName;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00133A29 File Offset: 0x00132C29
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this._cultureData.ThreeLetterISOCountryName;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00133A36 File Offset: 0x00132C36
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.ThreeLetterISORegionName;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x00133A3E File Offset: 0x00132C3E
		public virtual bool IsMetric
		{
			get
			{
				return this._cultureData.MeasurementSystem == 0;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x00133A4E File Offset: 0x00132C4E
		public virtual int GeoId
		{
			get
			{
				return this._cultureData.GeoId;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x00133A5B File Offset: 0x00132C5B
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this._cultureData.CurrencyEnglishName;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x00133A68 File Offset: 0x00132C68
		public virtual string CurrencyNativeName
		{
			get
			{
				return this._cultureData.CurrencyNativeName;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00133A75 File Offset: 0x00132C75
		public virtual string CurrencySymbol
		{
			get
			{
				return this._cultureData.CurrencySymbol;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x00133A82 File Offset: 0x00132C82
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this._cultureData.ISOCurrencySymbol;
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00133A90 File Offset: 0x00132C90
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name.Equals(regionInfo.Name);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00133ABA File Offset: 0x00132CBA
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00133AC7 File Offset: 0x00132CC7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040008C8 RID: 2248
		private string _name;

		// Token: 0x040008C9 RID: 2249
		private readonly CultureData _cultureData;

		// Token: 0x040008CA RID: 2250
		internal static volatile RegionInfo s_currentRegionInfo;
	}
}
