using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000216 RID: 534
	[Nullable(0)]
	[NullableContext(1)]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x001302A6 File Offset: 0x0012F4A6
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.s_minDate;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x001302AD File Offset: 0x0012F4AD
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.s_maxDate;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0012DB28 File Offset: 0x0012CD28
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x001302B4 File Offset: 0x0012F4B4
		internal override int MinCalendarYear
		{
			get
			{
				return 1960;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x001302BB File Offset: 0x0012F4BB
		internal override int MaxCalendarYear
		{
			get
			{
				return 2049;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x001302A6 File Offset: 0x0012F4A6
		internal override DateTime MinDate
		{
			get
			{
				return JapaneseLunisolarCalendar.s_minDate;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x001302AD File Offset: 0x0012F4AD
		internal override DateTime MaxDate
		{
			get
			{
				return JapaneseLunisolarCalendar.s_maxDate;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x001302C2 File Offset: 0x0012F4C2
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return JapaneseCalendar.GetEraInfo();
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x001302CC File Offset: 0x0012F4CC
		internal override int GetYearInfo(int lunarYear, int index)
		{
			if (lunarYear < 1960 || lunarYear > 2049)
			{
				throw new ArgumentOutOfRangeException("year", lunarYear, SR.Format(SR.ArgumentOutOfRange_Range, 1960, 2049));
			}
			return JapaneseLunisolarCalendar.s_yinfo[lunarYear - 1960, index];
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0013032A File Offset: 0x0012F52A
		internal override int GetYear(int year, DateTime time)
		{
			return this._helper.GetYear(year, time);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00130339 File Offset: 0x0012F539
		internal override int GetGregorianYear(int year, int era)
		{
			return this._helper.GetGregorianYear(year, era);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00130348 File Offset: 0x0012F548
		private static EraInfo[] TrimEras(EraInfo[] baseEras)
		{
			EraInfo[] array = new EraInfo[baseEras.Length];
			int num = 0;
			for (int i = 0; i < baseEras.Length; i++)
			{
				if (baseEras[i].yearOffset + baseEras[i].minEraYear < 2049)
				{
					if (baseEras[i].yearOffset + baseEras[i].maxEraYear < 1960)
					{
						break;
					}
					array[num] = baseEras[i];
					num++;
				}
			}
			if (num == 0)
			{
				return baseEras;
			}
			Array.Resize<EraInfo>(ref array, num);
			return array;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x001303B6 File Offset: 0x0012F5B6
		public JapaneseLunisolarCalendar()
		{
			this._helper = new GregorianCalendarHelper(this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x001303D4 File Offset: 0x0012F5D4
		public override int GetEra(DateTime time)
		{
			return this._helper.GetEra(time);
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000C9D36 File Offset: 0x000C8F36
		internal override CalendarId BaseCalendarID
		{
			get
			{
				return CalendarId.JAPAN;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000CFEC7 File Offset: 0x000CF0C7
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.JAPANESELUNISOLAR;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x001303E2 File Offset: 0x0012F5E2
		public override int[] Eras
		{
			get
			{
				return this._helper.Eras;
			}
		}

		// Token: 0x04000880 RID: 2176
		public const int JapaneseEra = 1;

		// Token: 0x04000881 RID: 2177
		private readonly GregorianCalendarHelper _helper;

		// Token: 0x04000882 RID: 2178
		private static readonly DateTime s_minDate = new DateTime(1960, 1, 28);

		// Token: 0x04000883 RID: 2179
		private static readonly DateTime s_maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x04000884 RID: 2180
		private static readonly int[,] s_yinfo = new int[,]
		{
			{
				6,
				1,
				28,
				44368
			},
			{
				0,
				2,
				15,
				43856
			},
			{
				0,
				2,
				5,
				19296
			},
			{
				4,
				1,
				25,
				42352
			},
			{
				0,
				2,
				13,
				42352
			},
			{
				0,
				2,
				2,
				21104
			},
			{
				3,
				1,
				22,
				26928
			},
			{
				0,
				2,
				9,
				55632
			},
			{
				7,
				1,
				30,
				27304
			},
			{
				0,
				2,
				17,
				22176
			},
			{
				0,
				2,
				6,
				39632
			},
			{
				5,
				1,
				27,
				19176
			},
			{
				0,
				2,
				15,
				19168
			},
			{
				0,
				2,
				3,
				42208
			},
			{
				4,
				1,
				23,
				53864
			},
			{
				0,
				2,
				11,
				53840
			},
			{
				8,
				1,
				31,
				54600
			},
			{
				0,
				2,
				18,
				46400
			},
			{
				0,
				2,
				7,
				54944
			},
			{
				6,
				1,
				28,
				38608
			},
			{
				0,
				2,
				16,
				38320
			},
			{
				0,
				2,
				5,
				18864
			},
			{
				4,
				1,
				25,
				42200
			},
			{
				0,
				2,
				13,
				42160
			},
			{
				10,
				2,
				2,
				45656
			},
			{
				0,
				2,
				20,
				27216
			},
			{
				0,
				2,
				9,
				27968
			},
			{
				6,
				1,
				29,
				46504
			},
			{
				0,
				2,
				18,
				11104
			},
			{
				0,
				2,
				6,
				38320
			},
			{
				5,
				1,
				27,
				18872
			},
			{
				0,
				2,
				15,
				18800
			},
			{
				0,
				2,
				4,
				25776
			},
			{
				3,
				1,
				23,
				27216
			},
			{
				0,
				2,
				10,
				59984
			},
			{
				8,
				1,
				31,
				27976
			},
			{
				0,
				2,
				19,
				23248
			},
			{
				0,
				2,
				8,
				11104
			},
			{
				5,
				1,
				28,
				37744
			},
			{
				0,
				2,
				16,
				37600
			},
			{
				0,
				2,
				5,
				51552
			},
			{
				4,
				1,
				24,
				58536
			},
			{
				0,
				2,
				12,
				54432
			},
			{
				0,
				2,
				1,
				55888
			},
			{
				2,
				1,
				22,
				23208
			},
			{
				0,
				2,
				9,
				22208
			},
			{
				7,
				1,
				29,
				43736
			},
			{
				0,
				2,
				18,
				9680
			},
			{
				0,
				2,
				7,
				37584
			},
			{
				5,
				1,
				26,
				51544
			},
			{
				0,
				2,
				14,
				43344
			},
			{
				0,
				2,
				3,
				46240
			},
			{
				3,
				1,
				23,
				47696
			},
			{
				0,
				2,
				10,
				46416
			},
			{
				9,
				1,
				31,
				21928
			},
			{
				0,
				2,
				19,
				19360
			},
			{
				0,
				2,
				8,
				42416
			},
			{
				5,
				1,
				28,
				21176
			},
			{
				0,
				2,
				16,
				21168
			},
			{
				0,
				2,
				5,
				43344
			},
			{
				4,
				1,
				25,
				46248
			},
			{
				0,
				2,
				12,
				27296
			},
			{
				0,
				2,
				1,
				44368
			},
			{
				2,
				1,
				22,
				21928
			},
			{
				0,
				2,
				10,
				19296
			},
			{
				6,
				1,
				29,
				42352
			},
			{
				0,
				2,
				17,
				42352
			},
			{
				0,
				2,
				7,
				21104
			},
			{
				5,
				1,
				27,
				26928
			},
			{
				0,
				2,
				13,
				55600
			},
			{
				0,
				2,
				3,
				23200
			},
			{
				3,
				1,
				23,
				43856
			},
			{
				0,
				2,
				11,
				38608
			},
			{
				11,
				1,
				31,
				19176
			},
			{
				0,
				2,
				19,
				19168
			},
			{
				0,
				2,
				8,
				42192
			},
			{
				6,
				1,
				28,
				53864
			},
			{
				0,
				2,
				15,
				53840
			},
			{
				0,
				2,
				4,
				54560
			},
			{
				5,
				1,
				24,
				55968
			},
			{
				0,
				2,
				12,
				46752
			},
			{
				0,
				2,
				1,
				38608
			},
			{
				2,
				1,
				22,
				19160
			},
			{
				0,
				2,
				10,
				18864
			},
			{
				7,
				1,
				30,
				42168
			},
			{
				0,
				2,
				17,
				42160
			},
			{
				0,
				2,
				6,
				45648
			},
			{
				5,
				1,
				26,
				46376
			},
			{
				0,
				2,
				14,
				27968
			},
			{
				0,
				2,
				2,
				44448
			}
		};
	}
}
