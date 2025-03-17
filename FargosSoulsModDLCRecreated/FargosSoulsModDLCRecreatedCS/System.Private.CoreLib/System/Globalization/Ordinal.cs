using System;
using System.Text.Unicode;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200021D RID: 541
	internal static class Ordinal
	{
		// Token: 0x06002272 RID: 8818 RVA: 0x00131F10 File Offset: 0x00131110
		internal static int CompareStringIgnoreCase(ref char strA, int lengthA, ref char strB, int lengthB)
		{
			int num = Math.Min(lengthA, lengthB);
			int num2 = num;
			ref char ptr = ref strA;
			ref char ptr2 = ref strB;
			char c = GlobalizationMode.Invariant ? char.MaxValue : '\u007f';
			while (num != 0 && ptr <= c && ptr2 <= c)
			{
				if (ptr != ptr2 && ((ptr | ' ') != (ptr2 | ' ') || (ptr | ' ') - 'a' > '\u0019'))
				{
					int num3 = (int)ptr;
					int num4 = (int)ptr2;
					if (ptr - 'a' <= '\u0019')
					{
						num3 -= 32;
					}
					if (ptr2 - 'a' <= '\u0019')
					{
						num4 -= 32;
					}
					return num3 - num4;
				}
				num--;
				ptr = Unsafe.Add<char>(ref ptr, 1);
				ptr2 = Unsafe.Add<char>(ref ptr2, 1);
			}
			if (num == 0 || GlobalizationMode.Invariant)
			{
				return lengthA - lengthB;
			}
			num2 -= num;
			return Ordinal.CompareStringIgnoreCaseNonAscii(ref ptr, lengthA - num2, ref ptr2, lengthB - num2);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00131FCF File Offset: 0x001311CF
		internal static int CompareStringIgnoreCaseNonAscii(ref char strA, int lengthA, ref char strB, int lengthB)
		{
			if (GlobalizationMode.Invariant)
			{
				return Ordinal.CompareIgnoreCaseInvariantMode(ref strA, lengthA, ref strB, lengthB);
			}
			if (GlobalizationMode.UseNls)
			{
				return CompareInfo.NlsCompareStringOrdinalIgnoreCase(ref strA, lengthA, ref strB, lengthB);
			}
			return OrdinalCasing.CompareStringIgnoreCase(ref strA, lengthA, ref strB, lengthB);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00131FFC File Offset: 0x001311FC
		internal unsafe static bool EqualsIgnoreCase(ref char charA, ref char charB, int length)
		{
			IntPtr intPtr = IntPtr.Zero;
			while (length >= 4)
			{
				ulong num = Unsafe.ReadUnaligned<ulong>(Unsafe.As<char, byte>(Unsafe.AddByteOffset<char>(ref charA, intPtr)));
				ulong num2 = Unsafe.ReadUnaligned<ulong>(Unsafe.As<char, byte>(Unsafe.AddByteOffset<char>(ref charB, intPtr)));
				ulong num3 = num | num2;
				if (!Utf16Utility.AllCharsInUInt32AreAscii((uint)num3 | (uint)(num3 >> 32)))
				{
					IL_F7:
					return Ordinal.CompareStringIgnoreCase(Unsafe.AddByteOffset<char>(ref charA, intPtr), length, Unsafe.AddByteOffset<char>(ref charB, intPtr), length) == 0;
				}
				if (!Utf16Utility.UInt64OrdinalIgnoreCaseAscii(num, num2))
				{
					return false;
				}
				intPtr += 8;
				length -= 4;
			}
			if (length >= 2)
			{
				uint num4 = Unsafe.ReadUnaligned<uint>(Unsafe.As<char, byte>(Unsafe.AddByteOffset<char>(ref charA, intPtr)));
				uint num5 = Unsafe.ReadUnaligned<uint>(Unsafe.As<char, byte>(Unsafe.AddByteOffset<char>(ref charB, intPtr)));
				if (!Utf16Utility.AllCharsInUInt32AreAscii(num4 | num5))
				{
					goto IL_F7;
				}
				if (!Utf16Utility.UInt32OrdinalIgnoreCaseAscii(num4, num5))
				{
					return false;
				}
				intPtr += 4;
				length -= 2;
			}
			if (length == 0)
			{
				return true;
			}
			uint num6 = (uint)(*Unsafe.AddByteOffset<char>(ref charA, intPtr));
			uint num7 = (uint)(*Unsafe.AddByteOffset<char>(ref charB, intPtr));
			if ((num6 | num7) > 127U)
			{
				goto IL_F7;
			}
			if (num6 == num7)
			{
				return true;
			}
			num6 |= 32U;
			return num6 - 97U <= 25U && num6 == (num7 | 32U);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00132118 File Offset: 0x00131318
		internal static int CompareIgnoreCaseInvariantMode(ref char strA, int lengthA, ref char strB, int lengthB)
		{
			int num = Math.Min(lengthA, lengthB);
			ref char ptr = ref strA;
			ref char ptr2 = ref strB;
			while (num != 0)
			{
				if (ptr == ptr2)
				{
					num--;
					ptr = Unsafe.Add<char>(ref ptr, 1);
					ptr2 = Unsafe.Add<char>(ref ptr2, 1);
				}
				else
				{
					char c = OrdinalCasing.ToUpperInvariantMode(ptr);
					char c2 = OrdinalCasing.ToUpperInvariantMode(ptr2);
					if (c != c2)
					{
						return (int)(c - c2);
					}
					num--;
					ptr = Unsafe.Add<char>(ref ptr, 1);
					ptr2 = Unsafe.Add<char>(ref ptr2, 1);
				}
			}
			return lengthA - lengthB;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00132188 File Offset: 0x00131388
		internal static int IndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			ReadOnlySpan<char> readOnlySpan;
			if (!source.TryGetSpan(startIndex, count, out readOnlySpan))
			{
				if (startIndex > source.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
				}
				else
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
				}
			}
			int num = ignoreCase ? Ordinal.IndexOfOrdinalIgnoreCase(readOnlySpan, value) : readOnlySpan.IndexOf(value);
			if (num < 0)
			{
				return num;
			}
			return num + startIndex;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x001321F8 File Offset: 0x001313F8
		internal static int IndexOfOrdinalIgnoreCase(ReadOnlySpan<char> source, ReadOnlySpan<char> value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			if (value.Length > source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(source, value, true, true);
			}
			if (GlobalizationMode.UseNls)
			{
				return CompareInfo.NlsIndexOfOrdinalCore(source, value, true, true);
			}
			return OrdinalCasing.IndexOf(source, value);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0013224C File Offset: 0x0013144C
		internal static int LastIndexOfOrdinalIgnoreCase(ReadOnlySpan<char> source, ReadOnlySpan<char> value)
		{
			if (value.Length == 0)
			{
				return source.Length;
			}
			if (value.Length > source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(source, value, true, false);
			}
			if (GlobalizationMode.UseNls)
			{
				return CompareInfo.NlsIndexOfOrdinalCore(source, value, true, false);
			}
			return OrdinalCasing.LastIndexOf(source, value);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x001322A8 File Offset: 0x001314A8
		internal static int ToUpperOrdinal(ReadOnlySpan<char> source, Span<char> destination)
		{
			if (source.Overlaps(destination))
			{
				throw new InvalidOperationException(SR.InvalidOperation_SpanOverlappedOperation);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				source.ToUpperInvariantMode(destination);
				return source.Length;
			}
			if (GlobalizationMode.UseNls)
			{
				TextInfo.Invariant.ChangeCaseToUpper(source, destination);
				return source.Length;
			}
			OrdinalCasing.ToUpperOrdinal(source, destination);
			return source.Length;
		}
	}
}
