using System;

namespace System.Globalization
{
	// Token: 0x020001E3 RID: 483
	internal static class CalendricalCalculationsHelper
	{
		// Token: 0x06001E18 RID: 7704 RVA: 0x0011EB79 File Offset: 0x0011DD79
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.141592653589793 / 180.0;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0011EB90 File Offset: 0x0011DD90
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0011EB9D File Offset: 0x0011DD9D
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0011EBAA File Offset: 0x0011DDAA
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0011EBB7 File Offset: 0x0011DDB7
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0011EBD4 File Offset: 0x0011DDD4
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficients, julianCenturies);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0011EBE1 File Offset: 0x0011DDE1
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0011EBF4 File Offset: 0x0011DDF4
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0011EC30 File Offset: 0x0011DE30
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0011EC4B File Offset: 0x0011DE4B
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0011EC78 File Offset: 0x0011DE78
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0011EC88 File Offset: 0x0011DE88
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0011ECC0 File Offset: 0x0011DEC0
		private static double CenturiesFrom1900(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1));
			return (double)(numberOfDays - CalendricalCalculationsHelper.s_startOf1900Century) / 36525.0;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0011ECF0 File Offset: 0x0011DEF0
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1));
			double num = (double)(numberOfDays - CalendricalCalculationsHelper.s_startOf1810);
			double x = 0.5 + num;
			return (Math.Pow(x, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0011ED4D File Offset: 0x0011DF4D
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0011ED64 File Offset: 0x0011DF64
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficients1900to1987, indeterminate);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0011ED84 File Offset: 0x0011DF84
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficients1800to1899, indeterminate);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0011EDA4 File Offset: 0x0011DFA4
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficients1700to1799, indeterminate) / 86400.0;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0011EDD0 File Offset: 0x0011DFD0
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficients1620to1699, indeterminate) / 86400.0;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0011EDFC File Offset: 0x0011DFFC
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] array = CalendricalCalculationsHelper.s_ephemerisCorrectionTable;
			int i = 0;
			while (i < array.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = array[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_7F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_7F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0011EE90 File Offset: 0x0011E090
		public static double JulianCenturies(double moment)
		{
			double num = moment + CalendricalCalculationsHelper.EphemerisCorrection(moment);
			return (num - 730120.5) / 36525.0;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0011EEBB File Offset: 0x0011E0BB
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0011EEC6 File Offset: 0x0011E0C6
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0011EEDC File Offset: 0x0011E0DC
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_lambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_anomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_eccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.Obliquity(num);
			double num6 = CalendricalCalculationsHelper.TanOfDegree(num5 / 2.0);
			double num7 = num6 * num6;
			double num8 = num7 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num7 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num7, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num9 = num8 / 6.283185307179586;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num9), 0.5), num9);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0011F008 File Offset: 0x0011E208
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double time = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(time);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0011F026 File Offset: 0x0011E226
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0011F040 File Offset: 0x0011E240
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0011F05C File Offset: 0x0011E25C
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0011F072 File Offset: 0x0011E272
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0011F084 File Offset: 0x0011E284
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			double num = 0.0;
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536);
			return num + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0011F65C File Offset: 0x0011E85C
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0011F68C File Offset: 0x0011E88C
		private static double Nutation(double julianCenturies)
		{
			double degree = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficientsA, julianCenturies);
			double degree2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.s_coefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(degree) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(degree2);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0011F6D4 File Offset: 0x0011E8D4
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = 282.7771834 + 36000.76953744 * num + 5.729577951308232E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num);
			double longitude = num2 + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num);
			return CalendricalCalculationsHelper.InitLongitude(longitude);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0011F725 File Offset: 0x0011E925
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0011F740 File Offset: 0x0011E940
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0011F790 File Offset: 0x0011E990
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double date = (double)numberOfDays;
			double d = CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(date));
			long num = (long)Math.Floor(d) - 1L;
			long num2 = num + 3L;
			long num3;
			for (num3 = num; num3 != num2; num3 += 1L)
			{
				double time = CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num3);
				double num4 = CalendricalCalculationsHelper.Compute(time);
				if (0.0 <= num4 && num4 <= 2.0)
				{
					break;
				}
			}
			return num3 - 1L;
		}

		// Token: 0x040006B7 RID: 1719
		private static readonly long s_startOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x040006B8 RID: 1720
		private static readonly long s_startOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x040006B9 RID: 1721
		private static readonly double[] s_coefficients1900to1987 = new double[]
		{
			-2E-05,
			0.000297,
			0.025184,
			-0.181133,
			0.55304,
			-0.861938,
			0.677066,
			-0.212591
		};

		// Token: 0x040006BA RID: 1722
		private static readonly double[] s_coefficients1800to1899 = new double[]
		{
			-9E-06,
			0.003844,
			0.083563,
			0.865736,
			4.867575,
			15.845535,
			31.332267,
			38.291999,
			28.316289,
			11.636204,
			2.043794
		};

		// Token: 0x040006BB RID: 1723
		private static readonly double[] s_coefficients1700to1799 = new double[]
		{
			8.118780842,
			-0.005092142,
			0.003336121,
			-2.66484E-05
		};

		// Token: 0x040006BC RID: 1724
		private static readonly double[] s_coefficients1620to1699 = new double[]
		{
			196.58333,
			-4.0675,
			0.0219167
		};

		// Token: 0x040006BD RID: 1725
		private static readonly double[] s_lambdaCoefficients = new double[]
		{
			280.46645,
			36000.76983,
			0.0003032
		};

		// Token: 0x040006BE RID: 1726
		private static readonly double[] s_anomalyCoefficients = new double[]
		{
			357.5291,
			35999.0503,
			-0.0001559,
			-4.8E-07
		};

		// Token: 0x040006BF RID: 1727
		private static readonly double[] s_eccentricityCoefficients = new double[]
		{
			0.016708617,
			-4.2037E-05,
			-1.236E-07
		};

		// Token: 0x040006C0 RID: 1728
		private static readonly double[] s_coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x040006C1 RID: 1729
		private static readonly double[] s_coefficientsA = new double[]
		{
			124.9,
			-1934.134,
			0.002063
		};

		// Token: 0x040006C2 RID: 1730
		private static readonly double[] s_coefficientsB = new double[]
		{
			201.11,
			72001.5377,
			0.00057
		};

		// Token: 0x040006C3 RID: 1731
		private static readonly CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] s_ephemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x020001E4 RID: 484
		private enum CorrectionAlgorithm
		{
			// Token: 0x040006C5 RID: 1733
			Default,
			// Token: 0x040006C6 RID: 1734
			Year1988to2019,
			// Token: 0x040006C7 RID: 1735
			Year1900to1987,
			// Token: 0x040006C8 RID: 1736
			Year1800to1899,
			// Token: 0x040006C9 RID: 1737
			Year1700to1799,
			// Token: 0x040006CA RID: 1738
			Year1620to1699
		}

		// Token: 0x020001E5 RID: 485
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x06001E3D RID: 7741 RVA: 0x0011F9EA File Offset: 0x0011EBEA
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x040006CB RID: 1739
			internal int _lowestYear;

			// Token: 0x040006CC RID: 1740
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
