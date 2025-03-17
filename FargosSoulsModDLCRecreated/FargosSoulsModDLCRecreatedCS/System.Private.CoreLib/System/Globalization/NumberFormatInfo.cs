using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200021B RID: 539
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NumberFormatInfo : IFormatProvider, ICloneable
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x00131360 File Offset: 0x00130560
		public NumberFormatInfo()
		{
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x001314C5 File Offset: 0x001306C5
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName);
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyDecString, propertyName);
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x001314E5 File Offset: 0x001306E5
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName);
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x001314F4 File Offset: 0x001306F4
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, SR.ArgumentNull_Array);
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(SR.Argument_InvalidNativeDigitCount, propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, SR.ArgumentNull_ArrayValue);
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(SR.Argument_InvalidNativeDigitValue, propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(SR.Argument_InvalidNativeDigitValue, propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(SR.Argument_InvalidNativeDigitValue, propertyName);
				}
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x001315B4 File Offset: 0x001307B4
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(SR.Argument_InvalidDigitSubstitution, propertyName);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x001315C6 File Offset: 0x001307C6
		internal bool HasInvariantNumberSigns
		{
			get
			{
				return this._hasInvariantNumberSigns;
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x001315CE File Offset: 0x001307CE
		private void UpdateHasInvariantNumberSigns()
		{
			this._hasInvariantNumberSigns = (this._positiveSign == "+" && this._negativeSign == "-");
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x001315FC File Offset: 0x001307FC
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				this.UpdateHasInvariantNumberSigns();
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00131771 File Offset: 0x00130971
		private void VerifyWritable()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00131786 File Offset: 0x00130986
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				NumberFormatInfo result;
				if ((result = NumberFormatInfo.s_invariantInfo) == null)
				{
					NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
					numberFormatInfo._isReadOnly = true;
					result = numberFormatInfo;
					NumberFormatInfo.s_invariantInfo = numberFormatInfo;
				}
				return result;
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x001317A7 File Offset: 0x001309A7
		public static NumberFormatInfo GetInstance([Nullable(2)] IFormatProvider formatProvider)
		{
			if (formatProvider != null)
			{
				return NumberFormatInfo.<GetInstance>g__GetProviderNonNull|42_0(formatProvider);
			}
			return NumberFormatInfo.CurrentInfo;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x001317B8 File Offset: 0x001309B8
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo._isReadOnly = false;
			return numberFormatInfo;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x001317D9 File Offset: 0x001309D9
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x001317E1 File Offset: 0x001309E1
		public int CurrencyDecimalDigits
		{
			get
			{
				return this._currencyDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 99));
				}
				this.VerifyWritable();
				this._currencyDecimalDigits = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00131821 File Offset: 0x00130A21
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x00131829 File Offset: 0x00130A29
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this._currencyDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "value");
				this._currencyDecimalSeparator = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x00131843 File Offset: 0x00130A43
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0013184C File Offset: 0x00130A4C
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(SR.Argument_InvalidGroupSize, propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(SR.Argument_InvalidGroupSize, propName);
					}
					i++;
				}
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0013189A File Offset: 0x00130A9A
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x001318AC File Offset: 0x00130AAC
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this._currencyGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("value", array);
				this._currencyGroupSizes = array;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x001318EB File Offset: 0x00130AEB
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x00131900 File Offset: 0x00130B00
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this._numberGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("value", array);
				this._numberGroupSizes = array;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0013193F File Offset: 0x00130B3F
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x00131954 File Offset: 0x00130B54
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this._percentGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("value", array);
				this._percentGroupSizes = array;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x00131993 File Offset: 0x00130B93
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x0013199B File Offset: 0x00130B9B
		public string CurrencyGroupSeparator
		{
			get
			{
				return this._currencyGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "value");
				this._currencyGroupSeparator = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x001319B5 File Offset: 0x00130BB5
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x001319BD File Offset: 0x00130BBD
		public string CurrencySymbol
		{
			get
			{
				return this._currencySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._currencySymbol = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x001319DC File Offset: 0x00130BDC
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (!currentCulture._isInherited)
				{
					NumberFormatInfo numInfo = currentCulture._numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x00131A18 File Offset: 0x00130C18
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x00131A20 File Offset: 0x00130C20
		public string NaNSymbol
		{
			get
			{
				return this._nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._nanSymbol = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x00131A3D File Offset: 0x00130C3D
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x00131A45 File Offset: 0x00130C45
		public int CurrencyNegativePattern
		{
			get
			{
				return this._currencyNegativePattern;
			}
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 15));
				}
				this.VerifyWritable();
				this._currencyNegativePattern = value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x00131A85 File Offset: 0x00130C85
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x00131A8D File Offset: 0x00130C8D
		public int NumberNegativePattern
		{
			get
			{
				return this._numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 4));
				}
				this.VerifyWritable();
				this._numberNegativePattern = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00131ACB File Offset: 0x00130CCB
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x00131AD3 File Offset: 0x00130CD3
		public int PercentPositivePattern
		{
			get
			{
				return this._percentPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 3));
				}
				this.VerifyWritable();
				this._percentPositivePattern = value;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x00131B11 File Offset: 0x00130D11
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x00131B19 File Offset: 0x00130D19
		public int PercentNegativePattern
		{
			get
			{
				return this._percentNegativePattern;
			}
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 11));
				}
				this.VerifyWritable();
				this._percentNegativePattern = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x00131B59 File Offset: 0x00130D59
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x00131B61 File Offset: 0x00130D61
		public string NegativeInfinitySymbol
		{
			get
			{
				return this._negativeInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._negativeInfinitySymbol = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00131B7E File Offset: 0x00130D7E
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x00131B86 File Offset: 0x00130D86
		public string NegativeSign
		{
			get
			{
				return this._negativeSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._negativeSign = value;
				this.UpdateHasInvariantNumberSigns();
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x00131BA9 File Offset: 0x00130DA9
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x00131BB1 File Offset: 0x00130DB1
		public int NumberDecimalDigits
		{
			get
			{
				return this._numberDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 99));
				}
				this.VerifyWritable();
				this._numberDecimalDigits = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00131BF1 File Offset: 0x00130DF1
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x00131BF9 File Offset: 0x00130DF9
		public string NumberDecimalSeparator
		{
			get
			{
				return this._numberDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "value");
				this._numberDecimalSeparator = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00131C13 File Offset: 0x00130E13
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x00131C1B File Offset: 0x00130E1B
		public string NumberGroupSeparator
		{
			get
			{
				return this._numberGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "value");
				this._numberGroupSeparator = value;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x00131C35 File Offset: 0x00130E35
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x00131C3D File Offset: 0x00130E3D
		public int CurrencyPositivePattern
		{
			get
			{
				return this._currencyPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 3));
				}
				this.VerifyWritable();
				this._currencyPositivePattern = value;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x00131C7B File Offset: 0x00130E7B
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x00131C83 File Offset: 0x00130E83
		public string PositiveInfinitySymbol
		{
			get
			{
				return this._positiveInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._positiveInfinitySymbol = value;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x00131CA0 File Offset: 0x00130EA0
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x00131CA8 File Offset: 0x00130EA8
		public string PositiveSign
		{
			get
			{
				return this._positiveSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._positiveSign = value;
				this.UpdateHasInvariantNumberSigns();
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x00131CCB File Offset: 0x00130ECB
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x00131CD3 File Offset: 0x00130ED3
		public int PercentDecimalDigits
		{
			get
			{
				return this._percentDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 0, 99));
				}
				this.VerifyWritable();
				this._percentDecimalDigits = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x00131D13 File Offset: 0x00130F13
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x00131D1B File Offset: 0x00130F1B
		public string PercentDecimalSeparator
		{
			get
			{
				return this._percentDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "value");
				this._percentDecimalSeparator = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x00131D35 File Offset: 0x00130F35
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x00131D3D File Offset: 0x00130F3D
		public string PercentGroupSeparator
		{
			get
			{
				return this._percentGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "value");
				this._percentGroupSeparator = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00131D57 File Offset: 0x00130F57
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x00131D5F File Offset: 0x00130F5F
		public string PercentSymbol
		{
			get
			{
				return this._percentSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._percentSymbol = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x00131D7C File Offset: 0x00130F7C
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x00131D84 File Offset: 0x00130F84
		public string PerMilleSymbol
		{
			get
			{
				return this._perMilleSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._perMilleSymbol = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00131DA1 File Offset: 0x00130FA1
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x00131DB3 File Offset: 0x00130FB3
		public string[] NativeDigits
		{
			get
			{
				return (string[])this._nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "value");
				this._nativeDigits = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x00131DCD File Offset: 0x00130FCD
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x00131DD5 File Offset: 0x00130FD5
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this._digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "value");
				this._digitSubstitution = (int)value;
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00131DEF File Offset: 0x00130FEF
		[NullableContext(2)]
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00131E08 File Offset: 0x00131008
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo._isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00131E41 File Offset: 0x00131041
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol)) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				NumberFormatInfo.<ValidateParseStyleInteger>g__throwInvalid|133_0(style);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00131E5B File Offset: 0x0013105B
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol)) != NumberStyles.None)
			{
				NumberFormatInfo.<ValidateParseStyleFloatingPoint>g__throwInvalid|134_0(style);
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00131E6C File Offset: 0x0013106C
		[CompilerGenerated]
		internal static NumberFormatInfo <GetInstance>g__GetProviderNonNull|42_0(IFormatProvider provider)
		{
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo._isInherited)
			{
				return cultureInfo._numInfo ?? cultureInfo.NumberFormat;
			}
			NumberFormatInfo result;
			if ((result = (provider as NumberFormatInfo)) == null)
			{
				result = ((provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo) ?? NumberFormatInfo.CurrentInfo);
			}
			return result;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00131EC4 File Offset: 0x001310C4
		[CompilerGenerated]
		internal static void <ValidateParseStyleInteger>g__throwInvalid|133_0(NumberStyles value)
		{
			if ((value & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(SR.Argument_InvalidNumberStyles, "style");
			}
			throw new ArgumentException(SR.Arg_InvalidHexStyle);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00131EE9 File Offset: 0x001310E9
		[CompilerGenerated]
		internal static void <ValidateParseStyleFloatingPoint>g__throwInvalid|134_0(NumberStyles value)
		{
			if ((value & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(SR.Argument_InvalidNumberStyles, "style");
			}
			throw new ArgumentException(SR.Arg_HexStyleNotSupported);
		}

		// Token: 0x04000890 RID: 2192
		private static volatile NumberFormatInfo s_invariantInfo;

		// Token: 0x04000891 RID: 2193
		internal int[] _numberGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04000892 RID: 2194
		internal int[] _currencyGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04000893 RID: 2195
		internal int[] _percentGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04000894 RID: 2196
		internal string _positiveSign = "+";

		// Token: 0x04000895 RID: 2197
		internal string _negativeSign = "-";

		// Token: 0x04000896 RID: 2198
		internal string _numberDecimalSeparator = ".";

		// Token: 0x04000897 RID: 2199
		internal string _numberGroupSeparator = ",";

		// Token: 0x04000898 RID: 2200
		internal string _currencyGroupSeparator = ",";

		// Token: 0x04000899 RID: 2201
		internal string _currencyDecimalSeparator = ".";

		// Token: 0x0400089A RID: 2202
		internal string _currencySymbol = "¤";

		// Token: 0x0400089B RID: 2203
		internal string _nanSymbol = "NaN";

		// Token: 0x0400089C RID: 2204
		internal string _positiveInfinitySymbol = "Infinity";

		// Token: 0x0400089D RID: 2205
		internal string _negativeInfinitySymbol = "-Infinity";

		// Token: 0x0400089E RID: 2206
		internal string _percentDecimalSeparator = ".";

		// Token: 0x0400089F RID: 2207
		internal string _percentGroupSeparator = ",";

		// Token: 0x040008A0 RID: 2208
		internal string _percentSymbol = "%";

		// Token: 0x040008A1 RID: 2209
		internal string _perMilleSymbol = "‰";

		// Token: 0x040008A2 RID: 2210
		internal string[] _nativeDigits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};

		// Token: 0x040008A3 RID: 2211
		internal int _numberDecimalDigits = 2;

		// Token: 0x040008A4 RID: 2212
		internal int _currencyDecimalDigits = 2;

		// Token: 0x040008A5 RID: 2213
		internal int _currencyPositivePattern;

		// Token: 0x040008A6 RID: 2214
		internal int _currencyNegativePattern;

		// Token: 0x040008A7 RID: 2215
		internal int _numberNegativePattern = 1;

		// Token: 0x040008A8 RID: 2216
		internal int _percentPositivePattern;

		// Token: 0x040008A9 RID: 2217
		internal int _percentNegativePattern;

		// Token: 0x040008AA RID: 2218
		internal int _percentDecimalDigits = 2;

		// Token: 0x040008AB RID: 2219
		internal int _digitSubstitution = 1;

		// Token: 0x040008AC RID: 2220
		internal bool _isReadOnly;

		// Token: 0x040008AD RID: 2221
		private bool _hasInvariantNumberSigns = true;
	}
}
