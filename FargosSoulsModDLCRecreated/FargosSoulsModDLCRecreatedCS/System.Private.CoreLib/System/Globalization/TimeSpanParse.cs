using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000230 RID: 560
	internal static class TimeSpanParse
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x00136088 File Offset: 0x00135288
		internal static long Pow10(int pow)
		{
			long result;
			switch (pow)
			{
			case 0:
				result = 1L;
				break;
			case 1:
				result = 10L;
				break;
			case 2:
				result = 100L;
				break;
			case 3:
				result = 1000L;
				break;
			case 4:
				result = 10000L;
				break;
			case 5:
				result = 100000L;
				break;
			case 6:
				result = 1000000L;
				break;
			case 7:
				result = 10000000L;
				break;
			default:
				result = (long)Math.Pow(10.0, (double)pow);
				break;
			}
			return result;
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00136110 File Offset: 0x00135310
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days._num > 10675199 || hours._num > 23 || minutes._num > 59 || seconds._num > 59 || !fraction.NormalizeAndValidateFraction())
			{
				result = 0L;
				return false;
			}
			long num = ((long)days._num * 3600L * 24L + (long)hours._num * 3600L + (long)minutes._num * 60L + (long)seconds._num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			result = num * 10000L + (long)fraction._num;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x001361DC File Offset: 0x001353DC
		internal static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true, input);
			bool flag = TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00136204 File Offset: 0x00135404
		internal static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false, input);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0013623C File Offset: 0x0013543C
		internal static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true, input);
			bool flag = TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00136264 File Offset: 0x00135464
		internal static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false, input);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x001362A0 File Offset: 0x001354A0
		internal static TimeSpan ParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true, input);
			bool flag = TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x001362C8 File Offset: 0x001354C8
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false, input);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00136304 File Offset: 0x00135504
		private static bool TryParseTimeSpan(ReadOnlySpan<char> input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			input = input.Trim();
			if (input.IsEmpty)
			{
				return result.SetBadTimeSpanFailure();
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken nextToken = timeSpanTokenizer.GetNextToken();
			while (nextToken._ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref nextToken, ref result))
				{
					return result.SetBadTimeSpanFailure();
				}
				nextToken = timeSpanTokenizer.GetNextToken();
			}
			return TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result) || result.SetBadTimeSpanFailure();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00136388 File Offset: 0x00135588
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken._ttt = TimeSpanParse.TTT.Sep;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetBadTimeSpanFailure();
				}
			}
			bool result2;
			switch (raw._numCount)
			{
			case 1:
				result2 = TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
				break;
			case 2:
				result2 = TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
				break;
			case 3:
				result2 = TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
				break;
			case 4:
				result2 = TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
				break;
			case 5:
				result2 = TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
				break;
			default:
				result2 = result.SetBadTimeSpanFailure();
				break;
			}
			return result2;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00136424 File Offset: 0x00135624
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 6)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetBadTimeSpanFailure();
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, raw._numbers4, out num))
			{
				return result.SetOverflowFailure();
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetOverflowFailure();
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00136504 File Offset: 0x00135704
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 5 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (flag)
			{
				if (raw.FullHMSFMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetOverflowFailure();
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetBadTimeSpanFailure();
			}
			return result.SetOverflowFailure();
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x001368C8 File Offset: 0x00135AC8
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetOverflowFailure();
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetBadTimeSpanFailure();
			}
			return result.SetOverflowFailure();
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00136C3C File Offset: 0x00135E3C
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetBadTimeSpanFailure();
			}
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetOverflowFailure();
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetOverflowFailure();
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x00136D20 File Offset: 0x00135F20
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(TimeSpanFormat.PositiveInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(TimeSpanFormat.NegativeInvariantFormatLiterals))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetBadTimeSpanFailure();
			}
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, timeSpanToken, timeSpanToken, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetOverflowFailure();
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetOverflowFailure();
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x00136E00 File Offset: 0x00136000
		private unsafe static bool TryParseExactTimeSpan(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (format.Length == 0)
			{
				return result.SetBadFormatSpecifierFailure(null);
			}
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'T')
				{
					if (c == 'G')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull, formatProvider, ref result);
					}
					if (c != 'T')
					{
						goto IL_6D;
					}
				}
				else if (c != 'c')
				{
					if (c == 'g')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized, formatProvider, ref result);
					}
					if (c != 't')
					{
						goto IL_6D;
					}
				}
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
				IL_6D:
				return result.SetBadFormatSpecifierFailure(new char?((char)(*format[0])));
			}
			return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x00136E9C File Offset: 0x0013609C
		private unsafe static bool TryParseByFormat(ReadOnlySpan<char> input, ReadOnlySpan<char> format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int number = 0;
			int number2 = 0;
			int number3 = 0;
			int number4 = 0;
			int leadingZeroes = 0;
			int number5 = 0;
			int i = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input, -1);
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				int num2;
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_287;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num >= 0 && num != 37)
							{
								num2 = 1;
								goto IL_28E;
							}
							return result.SetInvalidStringFailure();
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_287;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 > 7 || flag5)
						{
							return result.SetInvalidStringFailure();
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num2, num2, out leadingZeroes, out number5);
						flag5 = true;
						goto IL_28E;
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num2))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetBadQuoteFailure(c);
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetInvalidStringFailure();
					}
					StringBuilderCache.Release(stringBuilder);
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3;
							if (num2 > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num2 < 2) ? 1 : num2, (num2 < 2) ? 8 : num2, out num3, out number))
							{
								return result.SetInvalidStringFailure();
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_287;
						case 'f':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num2, num2, out leadingZeroes, out number5))
							{
								return result.SetInvalidStringFailure();
							}
							flag5 = true;
							break;
						case 'h':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num2, out number2))
							{
								return result.SetInvalidStringFailure();
							}
							flag2 = true;
							break;
						default:
							goto IL_287;
						}
					}
					else
					{
						int num = DateTimeFormat.ParseNextChar(format, i);
						if (num < 0 || timeSpanTokenizer.NextChar != (char)num)
						{
							return result.SetInvalidStringFailure();
						}
						num2 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_287;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num2 > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num2, out number4))
					{
						return result.SetInvalidStringFailure();
					}
					flag4 = true;
				}
				else
				{
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num2 > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num2, out number3))
					{
						return result.SetInvalidStringFailure();
					}
					flag3 = true;
				}
				IL_28E:
				i += num2;
				continue;
				IL_287:
				return result.SetInvalidStringFailure();
			}
			if (!timeSpanTokenizer.EOL)
			{
				return result.SetBadTimeSpanFailure();
			}
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			long num4;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(number), new TimeSpanParse.TimeSpanToken(number2), new TimeSpanParse.TimeSpanToken(number3), new TimeSpanParse.TimeSpanToken(number4), new TimeSpanParse.TimeSpanToken(number5, leadingZeroes), out num4))
			{
				if (!flag6)
				{
					num4 = -num4;
				}
				result.parsedTimeSpan = new TimeSpan(num4);
				return true;
			}
			return result.SetOverflowFailure();
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x001371B4 File Offset: 0x001363B4
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			int maxDigitLength = (minDigitLength == 1) ? 2 : minDigitLength;
			int num;
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, maxDigitLength, out num, out result);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x001371D8 File Offset: 0x001363D8
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				num = num * 10 + (int)(nextChar - '0');
				if (num == 0)
				{
					num2++;
				}
			}
			zeroes = num2;
			result = num;
			return i >= minDigitLength;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00137230 File Offset: 0x00136430
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00137260 File Offset: 0x00136460
		private static bool TryParseTimeSpanConstant(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00137280 File Offset: 0x00136480
		private static bool TryParseExactMultipleTimeSpan(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (formats == null)
			{
				return result.SetArgumentNullFailure("formats");
			}
			if (input.Length == 0)
			{
				return result.SetBadTimeSpanFailure();
			}
			if (formats.Length == 0)
			{
				return result.SetNoFormatSpecifierFailure();
			}
			foreach (string value in formats)
			{
				if (string.IsNullOrEmpty(value))
				{
					return result.SetBadFormatSpecifierFailure(null);
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false, input);
				if (TimeSpanParse.TryParseExactTimeSpan(input, value, formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			return result.SetBadTimeSpanFailure();
		}

		// Token: 0x02000231 RID: 561
		[Flags]
		private enum TimeSpanStandardStyles : byte
		{
			// Token: 0x04000903 RID: 2307
			None = 0,
			// Token: 0x04000904 RID: 2308
			Invariant = 1,
			// Token: 0x04000905 RID: 2309
			Localized = 2,
			// Token: 0x04000906 RID: 2310
			RequireFull = 4,
			// Token: 0x04000907 RID: 2311
			Any = 3
		}

		// Token: 0x02000232 RID: 562
		private enum TTT : byte
		{
			// Token: 0x04000909 RID: 2313
			None,
			// Token: 0x0400090A RID: 2314
			End,
			// Token: 0x0400090B RID: 2315
			Num,
			// Token: 0x0400090C RID: 2316
			Sep,
			// Token: 0x0400090D RID: 2317
			NumOverflow
		}

		// Token: 0x02000233 RID: 563
		private ref struct TimeSpanToken
		{
			// Token: 0x06002385 RID: 9093 RVA: 0x00137318 File Offset: 0x00136518
			public TimeSpanToken(TimeSpanParse.TTT type)
			{
				this = new TimeSpanParse.TimeSpanToken(type, 0, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06002386 RID: 9094 RVA: 0x00137338 File Offset: 0x00136538
			public TimeSpanToken(int number)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06002387 RID: 9095 RVA: 0x00137358 File Offset: 0x00136558
			public TimeSpanToken(int number, int leadingZeroes)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, leadingZeroes, default(ReadOnlySpan<char>));
			}

			// Token: 0x06002388 RID: 9096 RVA: 0x00137377 File Offset: 0x00136577
			public TimeSpanToken(TimeSpanParse.TTT type, int number, int leadingZeroes, ReadOnlySpan<char> separator)
			{
				this._ttt = type;
				this._num = number;
				this._zeroes = leadingZeroes;
				this._sep = separator;
			}

			// Token: 0x06002389 RID: 9097 RVA: 0x00137398 File Offset: 0x00136598
			public bool NormalizeAndValidateFraction()
			{
				if (this._num == 0)
				{
					return true;
				}
				if (this._zeroes == 0 && this._num > 9999999)
				{
					return false;
				}
				int num = (int)Math.Floor(Math.Log10((double)this._num)) + 1 + this._zeroes;
				if (num == 7)
				{
					return true;
				}
				if (num < 7)
				{
					this._num *= (int)TimeSpanParse.Pow10(7 - num);
					return true;
				}
				this._num = (int)Math.Round((double)this._num / (double)TimeSpanParse.Pow10(num - 7), MidpointRounding.AwayFromZero);
				return true;
			}

			// Token: 0x0400090E RID: 2318
			internal TimeSpanParse.TTT _ttt;

			// Token: 0x0400090F RID: 2319
			internal int _num;

			// Token: 0x04000910 RID: 2320
			internal int _zeroes;

			// Token: 0x04000911 RID: 2321
			internal ReadOnlySpan<char> _sep;
		}

		// Token: 0x02000234 RID: 564
		private ref struct TimeSpanTokenizer
		{
			// Token: 0x0600238A RID: 9098 RVA: 0x00137423 File Offset: 0x00136623
			internal TimeSpanTokenizer(ReadOnlySpan<char> input)
			{
				this = new TimeSpanParse.TimeSpanTokenizer(input, 0);
			}

			// Token: 0x0600238B RID: 9099 RVA: 0x0013742D File Offset: 0x0013662D
			internal TimeSpanTokenizer(ReadOnlySpan<char> input, int startPosition)
			{
				this._value = input;
				this._pos = startPosition;
			}

			// Token: 0x0600238C RID: 9100 RVA: 0x00137440 File Offset: 0x00136640
			internal unsafe TimeSpanParse.TimeSpanToken GetNextToken()
			{
				int pos = this._pos;
				if (pos >= this._value.Length)
				{
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.End);
				}
				int num = (int)(*this._value[pos] - 48);
				if (num <= 9)
				{
					int num2 = 0;
					if (num == 0)
					{
						num2 = 1;
						int num4;
						for (;;)
						{
							int num3 = this._pos + 1;
							this._pos = num3;
							if (num3 >= this._value.Length || (num4 = (int)(*this._value[this._pos] - 48)) > 9)
							{
								break;
							}
							if (num4 != 0)
							{
								goto IL_99;
							}
							num2++;
						}
						return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, 0, num2, default(ReadOnlySpan<char>));
						IL_99:
						num = num4;
					}
					do
					{
						int num3 = this._pos + 1;
						this._pos = num3;
						if (num3 >= this._value.Length)
						{
							goto IL_F6;
						}
						int num5 = (int)(*this._value[this._pos] - 48);
						if (num5 > 9)
						{
							goto IL_F6;
						}
						num = num * 10 + num5;
					}
					while (((long)num & (long)((ulong)-268435456)) == 0L);
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.NumOverflow);
					IL_F6:
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, num, num2, default(ReadOnlySpan<char>));
				}
				int num6 = 1;
				for (;;)
				{
					int num3 = this._pos + 1;
					this._pos = num3;
					if (num3 >= this._value.Length || *this._value[this._pos] - 48 <= 9)
					{
						break;
					}
					num6++;
				}
				return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Sep, 0, 0, this._value.Slice(pos, num6));
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x0600238D RID: 9101 RVA: 0x001375AD File Offset: 0x001367AD
			internal bool EOL
			{
				get
				{
					return this._pos >= this._value.Length - 1;
				}
			}

			// Token: 0x0600238E RID: 9102 RVA: 0x001375C7 File Offset: 0x001367C7
			internal void BackOne()
			{
				if (this._pos > 0)
				{
					this._pos--;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x0600238F RID: 9103 RVA: 0x001375E0 File Offset: 0x001367E0
			internal unsafe char NextChar
			{
				get
				{
					int num = this._pos + 1;
					this._pos = num;
					int num2 = num;
					if (num2 >= this._value.Length)
					{
						return '\0';
					}
					return (char)(*this._value[num2]);
				}
			}

			// Token: 0x04000912 RID: 2322
			private readonly ReadOnlySpan<char> _value;

			// Token: 0x04000913 RID: 2323
			private int _pos;
		}

		// Token: 0x02000235 RID: 565
		private ref struct TimeSpanRawInfo
		{
			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06002390 RID: 9104 RVA: 0x0013761C File Offset: 0x0013681C
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this._posLocInit)
					{
						this._posLoc = default(TimeSpanFormat.FormatLiterals);
						this._posLoc.Init(this._fullPosPattern, false);
						this._posLocInit = true;
					}
					return this._posLoc;
				}
			}

			// Token: 0x170007F7 RID: 2039
			// (get) Token: 0x06002391 RID: 9105 RVA: 0x00137656 File Offset: 0x00136856
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this._negLocInit)
					{
						this._negLoc = default(TimeSpanFormat.FormatLiterals);
						this._negLoc.Init(this._fullNegPattern, false);
						this._negLocInit = true;
					}
					return this._negLoc;
				}
			}

			// Token: 0x06002392 RID: 9106 RVA: 0x00137690 File Offset: 0x00136890
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002393 RID: 9107 RVA: 0x00137730 File Offset: 0x00136930
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002394 RID: 9108 RVA: 0x001377B4 File Offset: 0x001369B4
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 6 && this._numCount == 5 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.SecondFractionSep) && this._literals5.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002395 RID: 9109 RVA: 0x00137870 File Offset: 0x00136A70
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 2 && this._numCount == 1 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002396 RID: 9110 RVA: 0x001378C4 File Offset: 0x00136AC4
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 3 && this._numCount == 2 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002397 RID: 9111 RVA: 0x00137930 File Offset: 0x00136B30
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002398 RID: 9112 RVA: 0x001379B4 File Offset: 0x00136BB4
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06002399 RID: 9113 RVA: 0x00137A38 File Offset: 0x00136C38
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600239A RID: 9114 RVA: 0x00137AD8 File Offset: 0x00136CD8
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.SecondFractionSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600239B RID: 9115 RVA: 0x00137B78 File Offset: 0x00136D78
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this._lastSeenTTT = TimeSpanParse.TTT.None;
				this._tokenCount = 0;
				this._sepCount = 0;
				this._numCount = 0;
				this._fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this._fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this._posLocInit = false;
				this._negLocInit = false;
			}

			// Token: 0x0600239C RID: 9116 RVA: 0x00137BC8 File Offset: 0x00136DC8
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				switch (tok._ttt)
				{
				case TimeSpanParse.TTT.Num:
					if ((this._tokenCount == 0 && !this.AddSep(default(ReadOnlySpan<char>), ref result)) || !this.AddNum(tok, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.Sep:
					if (!this.AddSep(tok._sep, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.NumOverflow:
					return result.SetOverflowFailure();
				default:
					return result.SetBadTimeSpanFailure();
				}
				this._lastSeenTTT = tok._ttt;
				return true;
			}

			// Token: 0x0600239D RID: 9117 RVA: 0x00137C4C File Offset: 0x00136E4C
			private bool AddSep(ReadOnlySpan<char> sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._sepCount >= 6 || this._tokenCount >= 11)
				{
					return result.SetBadTimeSpanFailure();
				}
				int sepCount = this._sepCount;
				this._sepCount = sepCount + 1;
				switch (sepCount)
				{
				case 0:
					this._literals0 = sep;
					break;
				case 1:
					this._literals1 = sep;
					break;
				case 2:
					this._literals2 = sep;
					break;
				case 3:
					this._literals3 = sep;
					break;
				case 4:
					this._literals4 = sep;
					break;
				default:
					this._literals5 = sep;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x0600239E RID: 9118 RVA: 0x00137CE4 File Offset: 0x00136EE4
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._numCount >= 5 || this._tokenCount >= 11)
				{
					return result.SetBadTimeSpanFailure();
				}
				int numCount = this._numCount;
				this._numCount = numCount + 1;
				switch (numCount)
				{
				case 0:
					this._numbers0 = num;
					break;
				case 1:
					this._numbers1 = num;
					break;
				case 2:
					this._numbers2 = num;
					break;
				case 3:
					this._numbers3 = num;
					break;
				default:
					this._numbers4 = num;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x04000914 RID: 2324
			internal TimeSpanParse.TTT _lastSeenTTT;

			// Token: 0x04000915 RID: 2325
			internal int _tokenCount;

			// Token: 0x04000916 RID: 2326
			internal int _sepCount;

			// Token: 0x04000917 RID: 2327
			internal int _numCount;

			// Token: 0x04000918 RID: 2328
			private TimeSpanFormat.FormatLiterals _posLoc;

			// Token: 0x04000919 RID: 2329
			private TimeSpanFormat.FormatLiterals _negLoc;

			// Token: 0x0400091A RID: 2330
			private bool _posLocInit;

			// Token: 0x0400091B RID: 2331
			private bool _negLocInit;

			// Token: 0x0400091C RID: 2332
			private string _fullPosPattern;

			// Token: 0x0400091D RID: 2333
			private string _fullNegPattern;

			// Token: 0x0400091E RID: 2334
			internal TimeSpanParse.TimeSpanToken _numbers0;

			// Token: 0x0400091F RID: 2335
			internal TimeSpanParse.TimeSpanToken _numbers1;

			// Token: 0x04000920 RID: 2336
			internal TimeSpanParse.TimeSpanToken _numbers2;

			// Token: 0x04000921 RID: 2337
			internal TimeSpanParse.TimeSpanToken _numbers3;

			// Token: 0x04000922 RID: 2338
			internal TimeSpanParse.TimeSpanToken _numbers4;

			// Token: 0x04000923 RID: 2339
			internal ReadOnlySpan<char> _literals0;

			// Token: 0x04000924 RID: 2340
			internal ReadOnlySpan<char> _literals1;

			// Token: 0x04000925 RID: 2341
			internal ReadOnlySpan<char> _literals2;

			// Token: 0x04000926 RID: 2342
			internal ReadOnlySpan<char> _literals3;

			// Token: 0x04000927 RID: 2343
			internal ReadOnlySpan<char> _literals4;

			// Token: 0x04000928 RID: 2344
			internal ReadOnlySpan<char> _literals5;
		}

		// Token: 0x02000236 RID: 566
		private ref struct TimeSpanResult
		{
			// Token: 0x0600239F RID: 9119 RVA: 0x00137D6F File Offset: 0x00136F6F
			internal TimeSpanResult(bool throwOnFailure, ReadOnlySpan<char> originalTimeSpanString)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this._throwOnFailure = throwOnFailure;
				this._originalTimeSpanString = originalTimeSpanString;
			}

			// Token: 0x060023A0 RID: 9120 RVA: 0x00137D8B File Offset: 0x00136F8B
			internal bool SetNoFormatSpecifierFailure()
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new FormatException(SR.Format_NoFormatSpecifier);
			}

			// Token: 0x060023A1 RID: 9121 RVA: 0x00137DA1 File Offset: 0x00136FA1
			internal bool SetBadQuoteFailure(char failingCharacter)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new FormatException(SR.Format(SR.Format_BadQuote, failingCharacter));
			}

			// Token: 0x060023A2 RID: 9122 RVA: 0x00137DC2 File Offset: 0x00136FC2
			internal bool SetInvalidStringFailure()
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new FormatException(SR.Format_InvalidString);
			}

			// Token: 0x060023A3 RID: 9123 RVA: 0x00137DD8 File Offset: 0x00136FD8
			internal bool SetArgumentNullFailure(string argumentName)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new ArgumentNullException(argumentName, SR.ArgumentNull_String);
			}

			// Token: 0x060023A4 RID: 9124 RVA: 0x00137DEF File Offset: 0x00136FEF
			internal bool SetOverflowFailure()
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new OverflowException(SR.Format(SR.Overflow_TimeSpanElementTooLarge, new string(this._originalTimeSpanString)));
			}

			// Token: 0x060023A5 RID: 9125 RVA: 0x00137E15 File Offset: 0x00137015
			internal bool SetBadTimeSpanFailure()
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new FormatException(SR.Format(SR.Format_BadTimeSpan, new string(this._originalTimeSpanString)));
			}

			// Token: 0x060023A6 RID: 9126 RVA: 0x00137E3B File Offset: 0x0013703B
			internal bool SetBadFormatSpecifierFailure(char? formatSpecifierCharacter = null)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				throw new FormatException(SR.Format(SR.Format_BadFormatSpecifier, formatSpecifierCharacter));
			}

			// Token: 0x04000929 RID: 2345
			internal TimeSpan parsedTimeSpan;

			// Token: 0x0400092A RID: 2346
			private readonly bool _throwOnFailure;

			// Token: 0x0400092B RID: 2347
			private readonly ReadOnlySpan<char> _originalTimeSpanString;
		}

		// Token: 0x02000237 RID: 567
		private ref struct StringParser
		{
			// Token: 0x060023A7 RID: 9127 RVA: 0x00137E5C File Offset: 0x0013705C
			internal unsafe void NextChar()
			{
				if (this._pos < this._len)
				{
					this._pos++;
				}
				this._ch = (char)((this._pos < this._len) ? (*this._str[this._pos]) : 0);
			}

			// Token: 0x060023A8 RID: 9128 RVA: 0x00137EB0 File Offset: 0x001370B0
			internal unsafe char NextNonDigit()
			{
				for (int i = this._pos; i < this._len; i++)
				{
					char c = (char)(*this._str[i]);
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x060023A9 RID: 9129 RVA: 0x00137EF0 File Offset: 0x001370F0
			internal bool TryParse(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan = default(TimeSpan);
				this._str = input;
				this._len = input.Length;
				this._pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this._ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this._ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetOverflowFailure();
					}
				}
				else if (num < 0L)
				{
					return result.SetOverflowFailure();
				}
				this.SkipBlanks();
				if (this._pos < this._len)
				{
					return result.SetBadTimeSpanFailure();
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}

			// Token: 0x060023AA RID: 9130 RVA: 0x00137FE4 File Offset: 0x001371E4
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int pos = this._pos;
				while (this._ch >= '0' && this._ch <= '9')
				{
					if (((long)i & (long)((ulong)-268435456)) != 0L)
					{
						return result.SetOverflowFailure();
					}
					i = i * 10 + (int)this._ch - 48;
					if (i < 0)
					{
						return result.SetOverflowFailure();
					}
					this.NextChar();
				}
				if (pos == this._pos)
				{
					return result.SetBadTimeSpanFailure();
				}
				return i <= max || result.SetOverflowFailure();
			}

			// Token: 0x060023AB RID: 9131 RVA: 0x00138064 File Offset: 0x00137264
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this._ch != ':')
				{
					return result.SetBadTimeSpanFailure();
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this._ch == ':')
				{
					this.NextChar();
					if (this._ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this._ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this._ch >= '0' && this._ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this._ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x060023AC RID: 9132 RVA: 0x0013814A File Offset: 0x0013734A
			internal void SkipBlanks()
			{
				while (this._ch == ' ' || this._ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x0400092C RID: 2348
			private ReadOnlySpan<char> _str;

			// Token: 0x0400092D RID: 2349
			private char _ch;

			// Token: 0x0400092E RID: 2350
			private int _pos;

			// Token: 0x0400092F RID: 2351
			private int _len;
		}
	}
}
