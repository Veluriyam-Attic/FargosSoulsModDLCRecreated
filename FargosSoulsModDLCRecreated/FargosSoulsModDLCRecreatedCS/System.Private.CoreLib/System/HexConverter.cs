using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001C4 RID: 452
	internal static class HexConverter
	{
		// Token: 0x06001B92 RID: 7058 RVA: 0x001020FC File Offset: 0x001012FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToBytesBuffer(byte value, Span<byte> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)(((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00102150 File Offset: 0x00101350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToCharsBuffer(byte value, Span<char> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)(((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U | casing);
			*buffer[startingIndex + 1] = (char)(num2 & 255U);
			*buffer[startingIndex] = (char)(num2 >> 8);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x001021A8 File Offset: 0x001013A8
		public unsafe static void EncodeToUtf16(ReadOnlySpan<byte> bytes, Span<char> chars, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				HexConverter.ToCharsBuffer(*bytes[i], chars, i * 2, casing);
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x001021DC File Offset: 0x001013DC
		public unsafe static string ToString(ReadOnlySpan<byte> bytes, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			fixed (byte* pinnableReference = bytes.GetPinnableReference())
			{
				byte* value = pinnableReference;
				return string.Create<ValueTuple<IntPtr, int, HexConverter.Casing>>(bytes.Length * 2, new ValueTuple<IntPtr, int, HexConverter.Casing>((IntPtr)((void*)value), bytes.Length, casing), delegate(Span<char> chars, [TupleElementNames(new string[]
				{
					"Ptr",
					"Length",
					"casing"
				})] ValueTuple<IntPtr, int, HexConverter.Casing> args)
				{
					ReadOnlySpan<byte> bytes2 = new ReadOnlySpan<byte>((void*)args.Item1, args.Item2);
					HexConverter.EncodeToUtf16(bytes2, chars, args.Item3);
				});
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00102234 File Offset: 0x00101434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharUpper(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 7;
			}
			return (char)value;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0010224E File Offset: 0x0010144E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharLower(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 39;
			}
			return (char)value;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0010226C File Offset: 0x0010146C
		public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			int num;
			return HexConverter.TryDecodeFromUtf16(chars, bytes, out num);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00102284 File Offset: 0x00101484
		public unsafe static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsProcessed)
		{
			int num = 0;
			int i = 0;
			int num2 = 0;
			int num3 = 0;
			while (i < bytes.Length)
			{
				num2 = HexConverter.FromChar((int)(*chars[num + 1]));
				num3 = HexConverter.FromChar((int)(*chars[num]));
				if ((num2 | num3) == 255)
				{
					break;
				}
				*bytes[i++] = (byte)(num3 << 4 | num2);
				num += 2;
			}
			if (num2 == 255)
			{
				num++;
			}
			charsProcessed = num;
			return (num2 | num3) != 255;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00102304 File Offset: 0x00101504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int FromChar(int c)
		{
			if (c < HexConverter.CharToHexLookup.Length)
			{
				return (int)(*HexConverter.CharToHexLookup[c]);
			}
			return 255;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00102336 File Offset: 0x00101536
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexChar(int c)
		{
			return HexConverter.FromChar(c) != 255;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00102348 File Offset: 0x00101548
		public unsafe static ReadOnlySpan<byte> CharToHexLookup
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.21244F82B210125632917591768F6BF22EB6861F80C6C25A25BD26DFB580EA7B), 256);
			}
		}

		// Token: 0x020001C5 RID: 453
		public enum Casing : uint
		{
			// Token: 0x04000603 RID: 1539
			Upper,
			// Token: 0x04000604 RID: 1540
			Lower = 8224U
		}
	}
}
