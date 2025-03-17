using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200021E RID: 542
	internal static class OrdinalCasing
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x00132320 File Offset: 0x00131520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static char ToUpper(char c)
		{
			int num = (int)(c >> 8);
			if (num == 0)
			{
				return (char)OrdinalCasing.s_basicLatin[(int)c];
			}
			ushort[] array = OrdinalCasing.s_casingTable[num];
			if (array == OrdinalCasing.s_noCasingPage)
			{
				return c;
			}
			if (array == null)
			{
				array = OrdinalCasing.InitOrdinalCasingPage(num);
			}
			return (char)array[(int)(c & 'ÿ')];
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00132361 File Offset: 0x00131561
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static char ToUpperInvariantMode(char c)
		{
			if (c > 'ÿ')
			{
				return c;
			}
			return (char)OrdinalCasing.s_basicLatin[(int)c];
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00132374 File Offset: 0x00131574
		public unsafe static void ToUpperInvariantMode(this ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = OrdinalCasing.ToUpperInvariantMode((char)(*source[i]));
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x001323AC File Offset: 0x001315AC
		internal unsafe static void ToUpperOrdinal(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				char c = (char)(*source[i]);
				if (c <= 'ÿ')
				{
					*destination[i] = (char)OrdinalCasing.s_basicLatin[(int)c];
				}
				else if (char.IsHighSurrogate(c) && i < source.Length - 1 && char.IsLowSurrogate((char)(*source[i + 1])))
				{
					ushort num;
					ushort num2;
					OrdinalCasing.ToUpperSurrogate((ushort)c, *source[i + 1], out num, out num2);
					*destination[i] = (char)num;
					*destination[i + 1] = (char)num2;
					i++;
				}
				else
				{
					*destination[i] = OrdinalCasing.ToUpper(c);
				}
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0013245C File Offset: 0x0013165C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ToUpperSurrogate(ushort h, ushort l, out ushort hr, out ushort lr)
		{
			if (h <= 55299)
			{
				if (h != 55297)
				{
					if (h == 55299)
					{
						if (l - 56512 <= 50)
						{
							hr = h;
							lr = l - 56512 + 56448;
							return;
						}
					}
				}
				else
				{
					if (l - 56360 <= 39)
					{
						hr = h;
						lr = l - 56360 + 56320;
						return;
					}
					if (l - 56536 <= 35)
					{
						hr = h;
						lr = l - 56536 + 56496;
						return;
					}
				}
			}
			else if (h != 55302)
			{
				if (h != 55323)
				{
					if (h == 55354)
					{
						if (l - 56610 <= 33)
						{
							hr = h;
							lr = l - 56610 + 56576;
							return;
						}
					}
				}
				else if (l - 56928 <= 31)
				{
					hr = h;
					lr = l - 56928 + 56896;
					return;
				}
			}
			else if (l - 56512 <= 31)
			{
				hr = h;
				lr = l - 56512 + 56480;
				return;
			}
			hr = h;
			lr = l;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0013256C File Offset: 0x0013176C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool EqualSurrogate(char h1, char l1, char h2, char l2)
		{
			ushort num;
			ushort num2;
			OrdinalCasing.ToUpperSurrogate((ushort)h1, (ushort)l1, out num, out num2);
			ushort num3;
			ushort num4;
			OrdinalCasing.ToUpperSurrogate((ushort)h2, (ushort)l2, out num3, out num4);
			return num == num3 && num2 == num4;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0013259C File Offset: 0x0013179C
		internal static int CompareStringIgnoreCase(ref char strA, int lengthA, ref char strB, int lengthB)
		{
			int num = Math.Min(lengthA, lengthB);
			ref char ptr = ref strA;
			ref char ptr2 = ref strB;
			while (num != 0)
			{
				if (ptr <= 'ÿ' || num == 1 || !char.IsHighSurrogate(ptr) || !char.IsHighSurrogate(ptr2))
				{
					if (ptr == ptr2)
					{
						num--;
						ptr = Unsafe.Add<char>(ref ptr, 1);
						ptr2 = Unsafe.Add<char>(ref ptr2, 1);
					}
					else
					{
						char c = OrdinalCasing.ToUpper(ptr);
						char c2 = OrdinalCasing.ToUpper(ptr2);
						if (c != c2)
						{
							return (int)(c - c2);
						}
						num--;
						ptr = Unsafe.Add<char>(ref ptr, 1);
						ptr2 = Unsafe.Add<char>(ref ptr2, 1);
					}
				}
				else
				{
					char c3 = ptr;
					char c4 = ptr2;
					num--;
					ptr = Unsafe.Add<char>(ref ptr, 1);
					ptr2 = Unsafe.Add<char>(ref ptr2, 1);
					if (!char.IsLowSurrogate(ptr) || !char.IsLowSurrogate(ptr2))
					{
						if (c3 != c4)
						{
							return (int)(c3 - c4);
						}
					}
					else
					{
						ushort num2;
						ushort num3;
						OrdinalCasing.ToUpperSurrogate((ushort)c3, (ushort)ptr, out num2, out num3);
						ushort num4;
						ushort num5;
						OrdinalCasing.ToUpperSurrogate((ushort)c4, (ushort)ptr2, out num4, out num5);
						if (num2 != num4)
						{
							return (int)(num2 - num4);
						}
						if (num3 != num5)
						{
							return (int)(num3 - num5);
						}
						num--;
						ptr = Unsafe.Add<char>(ref ptr, 1);
						ptr2 = Unsafe.Add<char>(ref ptr2, 1);
					}
				}
			}
			return lengthA - lengthB;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x001326B4 File Offset: 0x001318B4
		internal unsafe static int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr + (source.Length - value.Length);
					char* ptr4 = ptr2 + value.Length - 1;
					for (char* ptr5 = ptr; ptr5 == ptr3; ptr5++)
					{
						char* ptr6 = ptr2;
						char* ptr7 = ptr5;
						while (ptr6 == ptr4)
						{
							if (!char.IsHighSurrogate(*ptr6) || ptr6 == ptr4)
							{
								if (*ptr6 != *ptr7 && OrdinalCasing.ToUpper(*ptr6) != OrdinalCasing.ToUpper(*ptr7))
								{
									break;
								}
								ptr6++;
								ptr7++;
							}
							else if (char.IsHighSurrogate(*ptr7) && char.IsLowSurrogate(ptr7[1]) && char.IsLowSurrogate(ptr6[1]))
							{
								if (!OrdinalCasing.EqualSurrogate(*ptr7, ptr7[1], *ptr6, ptr6[1]))
								{
									break;
								}
								ptr7 += 2;
								ptr6 += 2;
							}
							else
							{
								if (*ptr6 != *ptr7)
								{
									break;
								}
								ptr7++;
								ptr6++;
							}
						}
						if (ptr6 != ptr4)
						{
							return (int)((long)(ptr5 - ptr));
						}
					}
					return -1;
				}
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x001327D0 File Offset: 0x001319D0
		internal unsafe static int LastIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr2 + value.Length - 1;
					for (char* ptr4 = ptr + (source.Length - value.Length); ptr4 >= ptr; ptr4--)
					{
						char* ptr5 = ptr2;
						char* ptr6 = ptr4;
						while (ptr5 == ptr3)
						{
							if (!char.IsHighSurrogate(*ptr5) || ptr5 == ptr3)
							{
								if (*ptr5 != *ptr6 && OrdinalCasing.ToUpper(*ptr5) != OrdinalCasing.ToUpper(*ptr6))
								{
									break;
								}
								ptr5++;
								ptr6++;
							}
							else if (char.IsHighSurrogate(*ptr6) && char.IsLowSurrogate(ptr6[1]) && char.IsLowSurrogate(ptr5[1]))
							{
								if (!OrdinalCasing.EqualSurrogate(*ptr6, ptr6[1], *ptr5, ptr5[1]))
								{
									break;
								}
								ptr6 += 2;
								ptr5 += 2;
							}
							else
							{
								if (*ptr5 != *ptr6)
								{
									break;
								}
								ptr6++;
								ptr5++;
							}
						}
						if (ptr5 != ptr3)
						{
							return (int)((long)(ptr4 - ptr));
						}
					}
					return -1;
				}
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x001328E8 File Offset: 0x00131AE8
		private unsafe static ushort[] InitOrdinalCasingPage(int pageNumber)
		{
			ushort[] array = new ushort[256];
			ushort[] array2;
			ushort* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			char* pTarget = (char*)ptr;
			Interop.Globalization.InitOrdinalCasingPage(pageNumber, pTarget);
			array2 = null;
			OrdinalCasing.s_casingTable[pageNumber] = array;
			return array;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0013292C File Offset: 0x00131B2C
		// Note: this type is marked as 'beforefieldinit'.
		static OrdinalCasing()
		{
			ushort[][] array = new ushort[256][];
			array[0] = OrdinalCasing.s_basicLatin;
			array[17] = OrdinalCasing.s_noCasingPage;
			array[20] = OrdinalCasing.s_noCasingPage;
			array[21] = OrdinalCasing.s_noCasingPage;
			array[34] = OrdinalCasing.s_noCasingPage;
			array[35] = OrdinalCasing.s_noCasingPage;
			array[37] = OrdinalCasing.s_noCasingPage;
			array[38] = OrdinalCasing.s_noCasingPage;
			array[39] = OrdinalCasing.s_noCasingPage;
			array[40] = OrdinalCasing.s_noCasingPage;
			array[41] = OrdinalCasing.s_noCasingPage;
			array[42] = OrdinalCasing.s_noCasingPage;
			array[51] = OrdinalCasing.s_noCasingPage;
			array[52] = OrdinalCasing.s_noCasingPage;
			array[53] = OrdinalCasing.s_noCasingPage;
			array[54] = OrdinalCasing.s_noCasingPage;
			array[55] = OrdinalCasing.s_noCasingPage;
			array[56] = OrdinalCasing.s_noCasingPage;
			array[57] = OrdinalCasing.s_noCasingPage;
			array[58] = OrdinalCasing.s_noCasingPage;
			array[59] = OrdinalCasing.s_noCasingPage;
			array[60] = OrdinalCasing.s_noCasingPage;
			array[61] = OrdinalCasing.s_noCasingPage;
			array[62] = OrdinalCasing.s_noCasingPage;
			array[63] = OrdinalCasing.s_noCasingPage;
			array[64] = OrdinalCasing.s_noCasingPage;
			array[65] = OrdinalCasing.s_noCasingPage;
			array[66] = OrdinalCasing.s_noCasingPage;
			array[67] = OrdinalCasing.s_noCasingPage;
			array[68] = OrdinalCasing.s_noCasingPage;
			array[69] = OrdinalCasing.s_noCasingPage;
			array[70] = OrdinalCasing.s_noCasingPage;
			array[71] = OrdinalCasing.s_noCasingPage;
			array[72] = OrdinalCasing.s_noCasingPage;
			array[73] = OrdinalCasing.s_noCasingPage;
			array[74] = OrdinalCasing.s_noCasingPage;
			array[75] = OrdinalCasing.s_noCasingPage;
			array[76] = OrdinalCasing.s_noCasingPage;
			array[77] = OrdinalCasing.s_noCasingPage;
			array[78] = OrdinalCasing.s_noCasingPage;
			array[79] = OrdinalCasing.s_noCasingPage;
			array[80] = OrdinalCasing.s_noCasingPage;
			array[81] = OrdinalCasing.s_noCasingPage;
			array[82] = OrdinalCasing.s_noCasingPage;
			array[83] = OrdinalCasing.s_noCasingPage;
			array[84] = OrdinalCasing.s_noCasingPage;
			array[85] = OrdinalCasing.s_noCasingPage;
			array[86] = OrdinalCasing.s_noCasingPage;
			array[87] = OrdinalCasing.s_noCasingPage;
			array[88] = OrdinalCasing.s_noCasingPage;
			array[89] = OrdinalCasing.s_noCasingPage;
			array[90] = OrdinalCasing.s_noCasingPage;
			array[91] = OrdinalCasing.s_noCasingPage;
			array[92] = OrdinalCasing.s_noCasingPage;
			array[93] = OrdinalCasing.s_noCasingPage;
			array[94] = OrdinalCasing.s_noCasingPage;
			array[95] = OrdinalCasing.s_noCasingPage;
			array[96] = OrdinalCasing.s_noCasingPage;
			array[97] = OrdinalCasing.s_noCasingPage;
			array[98] = OrdinalCasing.s_noCasingPage;
			array[99] = OrdinalCasing.s_noCasingPage;
			array[100] = OrdinalCasing.s_noCasingPage;
			array[101] = OrdinalCasing.s_noCasingPage;
			array[102] = OrdinalCasing.s_noCasingPage;
			array[103] = OrdinalCasing.s_noCasingPage;
			array[104] = OrdinalCasing.s_noCasingPage;
			array[105] = OrdinalCasing.s_noCasingPage;
			array[106] = OrdinalCasing.s_noCasingPage;
			array[107] = OrdinalCasing.s_noCasingPage;
			array[108] = OrdinalCasing.s_noCasingPage;
			array[109] = OrdinalCasing.s_noCasingPage;
			array[110] = OrdinalCasing.s_noCasingPage;
			array[111] = OrdinalCasing.s_noCasingPage;
			array[112] = OrdinalCasing.s_noCasingPage;
			array[113] = OrdinalCasing.s_noCasingPage;
			array[114] = OrdinalCasing.s_noCasingPage;
			array[115] = OrdinalCasing.s_noCasingPage;
			array[116] = OrdinalCasing.s_noCasingPage;
			array[117] = OrdinalCasing.s_noCasingPage;
			array[118] = OrdinalCasing.s_noCasingPage;
			array[119] = OrdinalCasing.s_noCasingPage;
			array[120] = OrdinalCasing.s_noCasingPage;
			array[121] = OrdinalCasing.s_noCasingPage;
			array[122] = OrdinalCasing.s_noCasingPage;
			array[123] = OrdinalCasing.s_noCasingPage;
			array[124] = OrdinalCasing.s_noCasingPage;
			array[125] = OrdinalCasing.s_noCasingPage;
			array[126] = OrdinalCasing.s_noCasingPage;
			array[127] = OrdinalCasing.s_noCasingPage;
			array[128] = OrdinalCasing.s_noCasingPage;
			array[129] = OrdinalCasing.s_noCasingPage;
			array[130] = OrdinalCasing.s_noCasingPage;
			array[131] = OrdinalCasing.s_noCasingPage;
			array[132] = OrdinalCasing.s_noCasingPage;
			array[133] = OrdinalCasing.s_noCasingPage;
			array[134] = OrdinalCasing.s_noCasingPage;
			array[135] = OrdinalCasing.s_noCasingPage;
			array[136] = OrdinalCasing.s_noCasingPage;
			array[137] = OrdinalCasing.s_noCasingPage;
			array[138] = OrdinalCasing.s_noCasingPage;
			array[139] = OrdinalCasing.s_noCasingPage;
			array[140] = OrdinalCasing.s_noCasingPage;
			array[141] = OrdinalCasing.s_noCasingPage;
			array[142] = OrdinalCasing.s_noCasingPage;
			array[143] = OrdinalCasing.s_noCasingPage;
			array[144] = OrdinalCasing.s_noCasingPage;
			array[145] = OrdinalCasing.s_noCasingPage;
			array[146] = OrdinalCasing.s_noCasingPage;
			array[147] = OrdinalCasing.s_noCasingPage;
			array[148] = OrdinalCasing.s_noCasingPage;
			array[149] = OrdinalCasing.s_noCasingPage;
			array[150] = OrdinalCasing.s_noCasingPage;
			array[151] = OrdinalCasing.s_noCasingPage;
			array[152] = OrdinalCasing.s_noCasingPage;
			array[153] = OrdinalCasing.s_noCasingPage;
			array[154] = OrdinalCasing.s_noCasingPage;
			array[155] = OrdinalCasing.s_noCasingPage;
			array[156] = OrdinalCasing.s_noCasingPage;
			array[157] = OrdinalCasing.s_noCasingPage;
			array[158] = OrdinalCasing.s_noCasingPage;
			array[160] = OrdinalCasing.s_noCasingPage;
			array[161] = OrdinalCasing.s_noCasingPage;
			array[162] = OrdinalCasing.s_noCasingPage;
			array[163] = OrdinalCasing.s_noCasingPage;
			array[165] = OrdinalCasing.s_noCasingPage;
			array[172] = OrdinalCasing.s_noCasingPage;
			array[173] = OrdinalCasing.s_noCasingPage;
			array[174] = OrdinalCasing.s_noCasingPage;
			array[175] = OrdinalCasing.s_noCasingPage;
			array[176] = OrdinalCasing.s_noCasingPage;
			array[177] = OrdinalCasing.s_noCasingPage;
			array[178] = OrdinalCasing.s_noCasingPage;
			array[179] = OrdinalCasing.s_noCasingPage;
			array[180] = OrdinalCasing.s_noCasingPage;
			array[181] = OrdinalCasing.s_noCasingPage;
			array[182] = OrdinalCasing.s_noCasingPage;
			array[183] = OrdinalCasing.s_noCasingPage;
			array[184] = OrdinalCasing.s_noCasingPage;
			array[185] = OrdinalCasing.s_noCasingPage;
			array[186] = OrdinalCasing.s_noCasingPage;
			array[187] = OrdinalCasing.s_noCasingPage;
			array[188] = OrdinalCasing.s_noCasingPage;
			array[189] = OrdinalCasing.s_noCasingPage;
			array[190] = OrdinalCasing.s_noCasingPage;
			array[191] = OrdinalCasing.s_noCasingPage;
			array[192] = OrdinalCasing.s_noCasingPage;
			array[193] = OrdinalCasing.s_noCasingPage;
			array[194] = OrdinalCasing.s_noCasingPage;
			array[195] = OrdinalCasing.s_noCasingPage;
			array[196] = OrdinalCasing.s_noCasingPage;
			array[197] = OrdinalCasing.s_noCasingPage;
			array[198] = OrdinalCasing.s_noCasingPage;
			array[199] = OrdinalCasing.s_noCasingPage;
			array[200] = OrdinalCasing.s_noCasingPage;
			array[201] = OrdinalCasing.s_noCasingPage;
			array[202] = OrdinalCasing.s_noCasingPage;
			array[203] = OrdinalCasing.s_noCasingPage;
			array[204] = OrdinalCasing.s_noCasingPage;
			array[205] = OrdinalCasing.s_noCasingPage;
			array[206] = OrdinalCasing.s_noCasingPage;
			array[207] = OrdinalCasing.s_noCasingPage;
			array[208] = OrdinalCasing.s_noCasingPage;
			array[209] = OrdinalCasing.s_noCasingPage;
			array[210] = OrdinalCasing.s_noCasingPage;
			array[211] = OrdinalCasing.s_noCasingPage;
			array[212] = OrdinalCasing.s_noCasingPage;
			array[213] = OrdinalCasing.s_noCasingPage;
			array[214] = OrdinalCasing.s_noCasingPage;
			array[216] = OrdinalCasing.s_noCasingPage;
			array[217] = OrdinalCasing.s_noCasingPage;
			array[218] = OrdinalCasing.s_noCasingPage;
			array[219] = OrdinalCasing.s_noCasingPage;
			array[220] = OrdinalCasing.s_noCasingPage;
			array[221] = OrdinalCasing.s_noCasingPage;
			array[222] = OrdinalCasing.s_noCasingPage;
			array[223] = OrdinalCasing.s_noCasingPage;
			array[224] = OrdinalCasing.s_noCasingPage;
			array[225] = OrdinalCasing.s_noCasingPage;
			array[226] = OrdinalCasing.s_noCasingPage;
			array[227] = OrdinalCasing.s_noCasingPage;
			array[228] = OrdinalCasing.s_noCasingPage;
			array[229] = OrdinalCasing.s_noCasingPage;
			array[230] = OrdinalCasing.s_noCasingPage;
			array[231] = OrdinalCasing.s_noCasingPage;
			array[232] = OrdinalCasing.s_noCasingPage;
			array[233] = OrdinalCasing.s_noCasingPage;
			array[234] = OrdinalCasing.s_noCasingPage;
			array[235] = OrdinalCasing.s_noCasingPage;
			array[236] = OrdinalCasing.s_noCasingPage;
			array[237] = OrdinalCasing.s_noCasingPage;
			array[238] = OrdinalCasing.s_noCasingPage;
			array[239] = OrdinalCasing.s_noCasingPage;
			array[240] = OrdinalCasing.s_noCasingPage;
			array[241] = OrdinalCasing.s_noCasingPage;
			array[242] = OrdinalCasing.s_noCasingPage;
			array[243] = OrdinalCasing.s_noCasingPage;
			array[244] = OrdinalCasing.s_noCasingPage;
			array[245] = OrdinalCasing.s_noCasingPage;
			array[246] = OrdinalCasing.s_noCasingPage;
			array[247] = OrdinalCasing.s_noCasingPage;
			array[248] = OrdinalCasing.s_noCasingPage;
			array[249] = OrdinalCasing.s_noCasingPage;
			array[252] = OrdinalCasing.s_noCasingPage;
			OrdinalCasing.s_casingTable = array;
		}

		// Token: 0x040008C0 RID: 2240
		private static ushort[] s_noCasingPage = Array.Empty<ushort>();

		// Token: 0x040008C1 RID: 2241
		private static ushort[] s_basicLatin = new ushort[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			64,
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			91,
			92,
			93,
			94,
			95,
			96,
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			123,
			124,
			125,
			126,
			127,
			128,
			129,
			130,
			131,
			132,
			133,
			134,
			135,
			136,
			137,
			138,
			139,
			140,
			141,
			142,
			143,
			144,
			145,
			146,
			147,
			148,
			149,
			150,
			151,
			152,
			153,
			154,
			155,
			156,
			157,
			158,
			159,
			160,
			161,
			162,
			163,
			164,
			165,
			166,
			167,
			168,
			169,
			170,
			171,
			172,
			173,
			174,
			175,
			176,
			177,
			178,
			179,
			180,
			924,
			182,
			183,
			184,
			185,
			186,
			187,
			188,
			189,
			190,
			191,
			192,
			193,
			194,
			195,
			196,
			197,
			198,
			199,
			200,
			201,
			202,
			203,
			204,
			205,
			206,
			207,
			208,
			209,
			210,
			211,
			212,
			213,
			214,
			215,
			216,
			217,
			218,
			219,
			220,
			221,
			222,
			223,
			192,
			193,
			194,
			195,
			196,
			197,
			198,
			199,
			200,
			201,
			202,
			203,
			204,
			205,
			206,
			207,
			208,
			209,
			210,
			211,
			212,
			213,
			214,
			247,
			216,
			217,
			218,
			219,
			220,
			221,
			222,
			376
		};

		// Token: 0x040008C2 RID: 2242
		private static ushort[][] s_casingTable;
	}
}
