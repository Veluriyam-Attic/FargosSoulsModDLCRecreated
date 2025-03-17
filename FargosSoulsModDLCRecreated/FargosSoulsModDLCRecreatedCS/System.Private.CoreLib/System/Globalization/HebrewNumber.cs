using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200020B RID: 523
	internal static class HebrewNumber
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x0012D6B4 File Offset: 0x0012C8B4
		internal static void Append(StringBuilder outputBuffer, int Number)
		{
			int length = outputBuffer.Length;
			char c = '\0';
			if (Number > 5000)
			{
				Number -= 5000;
			}
			int num = Number / 100;
			if (num > 0)
			{
				Number -= num * 100;
				for (int i = 0; i < num / 4; i++)
				{
					outputBuffer.Append('ת');
				}
				int num2 = num % 4;
				if (num2 > 0)
				{
					outputBuffer.Append((char)(1510 + num2));
				}
			}
			int num3 = Number / 10;
			Number %= 10;
			switch (num3)
			{
			case 0:
				c = '\0';
				break;
			case 1:
				c = 'י';
				break;
			case 2:
				c = 'כ';
				break;
			case 3:
				c = 'ל';
				break;
			case 4:
				c = 'מ';
				break;
			case 5:
				c = 'נ';
				break;
			case 6:
				c = 'ס';
				break;
			case 7:
				c = 'ע';
				break;
			case 8:
				c = 'פ';
				break;
			case 9:
				c = 'צ';
				break;
			}
			char c2 = (char)((Number > 0) ? (1488 + Number - 1) : 0);
			if (c2 == 'ה' && c == 'י')
			{
				c2 = 'ו';
				c = 'ט';
			}
			if (c2 == 'ו' && c == 'י')
			{
				c2 = 'ז';
				c = 'ט';
			}
			if (c != '\0')
			{
				outputBuffer.Append(c);
			}
			if (c2 != '\0')
			{
				outputBuffer.Append(c2);
			}
			if (outputBuffer.Length - length > 1)
			{
				outputBuffer.Insert(outputBuffer.Length - 1, '"');
				return;
			}
			outputBuffer.Append('\'');
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0012D834 File Offset: 0x0012CA34
		internal static HebrewNumberParsingState ParseByChar(char ch, ref HebrewNumberParsingContext context)
		{
			HebrewNumber.HebrewToken hebrewToken;
			if (ch == '\'')
			{
				hebrewToken = HebrewNumber.HebrewToken.SingleQuote;
			}
			else if (ch == '"')
			{
				hebrewToken = HebrewNumber.HebrewToken.DoubleQuote;
			}
			else
			{
				int num = (int)(ch - 'א');
				if (num < 0 || num >= HebrewNumber.s_hebrewValues.Length)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				hebrewToken = HebrewNumber.s_hebrewValues[num].token;
				if (hebrewToken == HebrewNumber.HebrewToken.Invalid)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				context.result += (int)HebrewNumber.s_hebrewValues[num].value;
			}
			context.state = HebrewNumber.s_numberPasingState[(int)(context.state * HebrewNumber.HS.X00 + (sbyte)hebrewToken)];
			if (context.state == HebrewNumber.HS._err)
			{
				return HebrewNumberParsingState.InvalidHebrewNumber;
			}
			if (context.state == HebrewNumber.HS.END)
			{
				return HebrewNumberParsingState.FoundEndOfHebrewNumber;
			}
			return HebrewNumberParsingState.ContinueParsing;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0012D8CE File Offset: 0x0012CACE
		internal static bool IsDigit(char ch)
		{
			if (ch >= 'א' && ch <= HebrewNumber.s_maxHebrewNumberCh)
			{
				return HebrewNumber.s_hebrewValues[(int)(ch - 'א')].value >= 0;
			}
			return ch == '\'' || ch == '"';
		}

		// Token: 0x04000840 RID: 2112
		private static readonly HebrewNumber.HebrewValue[] s_hebrewValues = new HebrewNumber.HebrewValue[]
		{
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 2),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 3),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 4),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 5),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 6),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 7),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 8),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit9, 9),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 10),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 20),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 30),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 40),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 50),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 60),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 70),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 80),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 90),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit100, 100),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 200),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 300),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit400, 400)
		};

		// Token: 0x04000841 RID: 2113
		private static readonly char s_maxHebrewNumberCh = (char)(1488 + HebrewNumber.s_hebrewValues.Length - 1);

		// Token: 0x04000842 RID: 2114
		private static readonly HebrewNumber.HS[] s_numberPasingState = new HebrewNumber.HS[]
		{
			HebrewNumber.HS.S400,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X0,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_100,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err
		};

		// Token: 0x0200020C RID: 524
		private enum HebrewToken : short
		{
			// Token: 0x04000844 RID: 2116
			Invalid = -1,
			// Token: 0x04000845 RID: 2117
			Digit400,
			// Token: 0x04000846 RID: 2118
			Digit200_300,
			// Token: 0x04000847 RID: 2119
			Digit100,
			// Token: 0x04000848 RID: 2120
			Digit10,
			// Token: 0x04000849 RID: 2121
			Digit1,
			// Token: 0x0400084A RID: 2122
			Digit6_7,
			// Token: 0x0400084B RID: 2123
			Digit7,
			// Token: 0x0400084C RID: 2124
			Digit9,
			// Token: 0x0400084D RID: 2125
			SingleQuote,
			// Token: 0x0400084E RID: 2126
			DoubleQuote
		}

		// Token: 0x0200020D RID: 525
		private struct HebrewValue
		{
			// Token: 0x06002139 RID: 8505 RVA: 0x0012DAF7 File Offset: 0x0012CCF7
			internal HebrewValue(HebrewNumber.HebrewToken token, short value)
			{
				this.token = token;
				this.value = value;
			}

			// Token: 0x0400084F RID: 2127
			internal HebrewNumber.HebrewToken token;

			// Token: 0x04000850 RID: 2128
			internal short value;
		}

		// Token: 0x0200020E RID: 526
		internal enum HS : sbyte
		{
			// Token: 0x04000852 RID: 2130
			_err = -1,
			// Token: 0x04000853 RID: 2131
			Start,
			// Token: 0x04000854 RID: 2132
			S400,
			// Token: 0x04000855 RID: 2133
			S400_400,
			// Token: 0x04000856 RID: 2134
			S400_X00,
			// Token: 0x04000857 RID: 2135
			S400_X0,
			// Token: 0x04000858 RID: 2136
			X00_DQ,
			// Token: 0x04000859 RID: 2137
			S400_X00_X0,
			// Token: 0x0400085A RID: 2138
			X0_DQ,
			// Token: 0x0400085B RID: 2139
			X,
			// Token: 0x0400085C RID: 2140
			X0,
			// Token: 0x0400085D RID: 2141
			X00,
			// Token: 0x0400085E RID: 2142
			S400_DQ,
			// Token: 0x0400085F RID: 2143
			S400_400_DQ,
			// Token: 0x04000860 RID: 2144
			S400_400_100,
			// Token: 0x04000861 RID: 2145
			S9,
			// Token: 0x04000862 RID: 2146
			X00_S9,
			// Token: 0x04000863 RID: 2147
			S9_DQ,
			// Token: 0x04000864 RID: 2148
			END = 100
		}
	}
}
