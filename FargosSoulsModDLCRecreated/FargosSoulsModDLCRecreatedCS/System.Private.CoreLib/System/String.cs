using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000097 RID: 151
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	public sealed class String : IComparable, IEnumerable, IConvertible, IEnumerable<char>, IComparable<string>, IEquatable<string>, ICloneable
	{
		// Token: 0x1700008A RID: 138
		[IndexerName("Chars")]
		public extern char this[int index]
		{
			[Intrinsic]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000683 RID: 1667
		public extern int Length { [Intrinsic] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000684 RID: 1668
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FastAllocateString(int length);

		// Token: 0x06000685 RID: 1669
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTrailByte(byte data);

		// Token: 0x06000686 RID: 1670
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool TryGetTrailByte(out byte data);

		// Token: 0x06000687 RID: 1671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string Intern();

		// Token: 0x06000688 RID: 1672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string IsInterned();

		// Token: 0x06000689 RID: 1673 RVA: 0x000BE7AC File Offset: 0x000BD9AC
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return str.Intern();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000BE7C2 File Offset: 0x000BD9C2
		[return: Nullable(2)]
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return str.IsInterned();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000BE7D8 File Offset: 0x000BD9D8
		internal unsafe static void InternalCopy(string src, IntPtr dest, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (char* ptr = &src._firstChar)
			{
				char* ptr2 = ptr;
				byte* src2 = (byte*)ptr2;
				byte* dest2 = (byte*)((void*)dest);
				Buffer.Memcpy(dest2, src2, len);
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000BE808 File Offset: 0x000BDA08
		internal unsafe int GetBytesFromEncoding(byte* pbNativeBuffer, int cbNativeBuffer, Encoding encoding)
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* chars = ptr;
				return encoding.GetBytes(chars, this.Length, pbNativeBuffer, cbNativeBuffer);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000BE82E File Offset: 0x000BDA2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool EqualsHelper(string strA, string strB)
		{
			return SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(strA.GetRawStringData()), Unsafe.As<char, byte>(strB.GetRawStringData()), (UIntPtr)((IntPtr)strA.Length * (IntPtr)2));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000BE855 File Offset: 0x000BDA55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int CompareOrdinalHelper(string strA, int indexA, int countA, string strB, int indexB, int countB)
		{
			return SpanHelpers.SequenceCompareTo(Unsafe.Add<char>(strA.GetRawStringData(), indexA), countA, Unsafe.Add<char>(strB.GetRawStringData(), indexB), countB);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000BE878 File Offset: 0x000BDA78
		internal static bool EqualsOrdinalIgnoreCase(string strA, string strB)
		{
			return strA == strB || (strA != null && strB != null && strA.Length == strB.Length && string.EqualsOrdinalIgnoreCaseNoLengthCheck(strA, strB));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000BE89F File Offset: 0x000BDA9F
		private static bool EqualsOrdinalIgnoreCaseNoLengthCheck(string strA, string strB)
		{
			return Ordinal.EqualsIgnoreCase(strA.GetRawStringData(), strB.GetRawStringData(), strB.Length);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000BE8B8 File Offset: 0x000BDAB8
		private unsafe static int CompareOrdinalHelper(string strA, string strB)
		{
			int i = Math.Min(strA.Length, strB.Length);
			fixed (char* ptr = &strA._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB._firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					if (ptr5[1] == ptr6[1])
					{
						i -= 2;
						ptr5 += 2;
						ptr6 += 2;
						while (i >= 12)
						{
							if (*(long*)ptr5 == *(long*)ptr6)
							{
								if (*(long*)(ptr5 + 4) == *(long*)(ptr6 + 4))
								{
									if (*(long*)(ptr5 + 8) == *(long*)(ptr6 + 8))
									{
										i -= 12;
										ptr5 += 12;
										ptr6 += 12;
										continue;
									}
									ptr5 += 4;
									ptr6 += 4;
								}
								ptr5 += 4;
								ptr6 += 4;
							}
							if (*(int*)ptr5 == *(int*)ptr6)
							{
								ptr5 += 2;
								ptr6 += 2;
							}
							IL_112:
							if (*ptr5 != *ptr6)
							{
								return (int)(*ptr5 - *ptr6);
							}
							goto IL_122;
						}
						while (i > 0)
						{
							if (*(int*)ptr5 != *(int*)ptr6)
							{
								goto IL_112;
							}
							i -= 2;
							ptr5 += 2;
							ptr6 += 2;
						}
						return strA.Length - strB.Length;
					}
					IL_122:
					return (int)(ptr5[1] - ptr6[1]);
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000BE9F2 File Offset: 0x000BDBF2
		[NullableContext(2)]
		public static int Compare(string strA, string strB)
		{
			return string.Compare(strA, strB, StringComparison.CurrentCulture);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000BE9FC File Offset: 0x000BDBFC
		[NullableContext(2)]
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
			return string.Compare(strA, strB, comparisonType);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000BEA1C File Offset: 0x000BDC1C
		[NullableContext(2)]
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			if (strA == strB)
			{
				string.CheckStringComparison(comparisonType);
				return 0;
			}
			if (strA == null)
			{
				string.CheckStringComparison(comparisonType);
				return -1;
			}
			if (strB == null)
			{
				string.CheckStringComparison(comparisonType);
				return 1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(strA, strB, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				if (strA._firstChar != strB._firstChar)
				{
					return (int)(strA._firstChar - strB._firstChar);
				}
				return string.CompareOrdinalHelper(strA, strB);
			case StringComparison.OrdinalIgnoreCase:
				return Ordinal.CompareStringIgnoreCase(strA.GetRawStringData(), strA.Length, strB.GetRawStringData(), strB.Length);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000BEAE8 File Offset: 0x000BDCE8
		[NullableContext(2)]
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			return cultureInfo.CompareInfo.Compare(strA, strB, options);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000BEB10 File Offset: 0x000BDD10
		[NullableContext(2)]
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			CompareOptions options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
			return string.Compare(strA, strB, culture, options);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000BEB2E File Offset: 0x000BDD2E
		[NullableContext(2)]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			return string.Compare(strA, indexA, strB, indexB, length, false);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000BEB3C File Offset: 0x000BDD3C
		[NullableContext(2)]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			int num = length;
			int num2 = length;
			if (strA != null)
			{
				num = Math.Min(num, strA.Length - indexA);
			}
			if (strB != null)
			{
				num2 = Math.Min(num2, strB.Length - indexB);
			}
			CompareOptions options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, options);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000BEB94 File Offset: 0x000BDD94
		[NullableContext(2)]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			CompareOptions options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
			return string.Compare(strA, indexA, strB, indexB, length, culture, options);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000BEBB8 File Offset: 0x000BDDB8
		[NullableContext(2)]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			int num = length;
			int num2 = length;
			if (strA != null)
			{
				num = Math.Min(num, strA.Length - indexA);
			}
			if (strB != null)
			{
				num2 = Math.Min(num2, strB.Length - indexB);
			}
			return cultureInfo.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, options);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000BEC10 File Offset: 0x000BDE10
		[NullableContext(2)]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
				}
				if (indexA < 0 || indexB < 0)
				{
					string paramName = (indexA < 0) ? "indexA" : "indexB";
					throw new ArgumentOutOfRangeException(paramName, SR.ArgumentOutOfRange_Index);
				}
				if (strA.Length - indexA < 0 || strB.Length - indexB < 0)
				{
					string paramName2 = (strA.Length - indexA < 0) ? "indexA" : "indexB";
					throw new ArgumentOutOfRangeException(paramName2, SR.ArgumentOutOfRange_Index);
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				int num = Math.Min(length, strA.Length - indexA);
				int num2 = Math.Min(length, strB.Length - indexB);
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, string.GetCaseCompareOfComparisonCulture(comparisonType));
				case StringComparison.InvariantCulture:
				case StringComparison.InvariantCultureIgnoreCase:
					return CompareInfo.Invariant.Compare(strA, indexA, num, strB, indexB, num2, string.GetCaseCompareOfComparisonCulture(comparisonType));
				case StringComparison.Ordinal:
					return string.CompareOrdinalHelper(strA, indexA, num, strB, indexB, num2);
				default:
					return Ordinal.CompareStringIgnoreCase(Unsafe.Add<char>(strA.GetRawStringData(), indexA), num, Unsafe.Add<char>(strB.GetRawStringData(), indexB), num2);
				}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000BED56 File Offset: 0x000BDF56
		[NullableContext(2)]
		public static int CompareOrdinal(string strA, string strB)
		{
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			if (strA._firstChar != strB._firstChar)
			{
				return (int)(strA._firstChar - strB._firstChar);
			}
			return string.CompareOrdinalHelper(strA, strB);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000BED8B File Offset: 0x000BDF8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int CompareOrdinal(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			return SpanHelpers.SequenceCompareTo(MemoryMarshal.GetReference<char>(strA), strA.Length, MemoryMarshal.GetReference<char>(strB), strB.Length);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000BEDAC File Offset: 0x000BDFAC
		[NullableContext(2)]
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeCount);
				}
				if (indexA < 0 || indexB < 0)
				{
					string paramName = (indexA < 0) ? "indexA" : "indexB";
					throw new ArgumentOutOfRangeException(paramName, SR.ArgumentOutOfRange_Index);
				}
				int num = Math.Min(length, strA.Length - indexA);
				int num2 = Math.Min(length, strB.Length - indexB);
				if (num < 0 || num2 < 0)
				{
					string paramName2 = (num < 0) ? "indexA" : "indexB";
					throw new ArgumentOutOfRangeException(paramName2, SR.ArgumentOutOfRange_Index);
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				return string.CompareOrdinalHelper(strA, indexA, num, strB, indexB, num2);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000BEE64 File Offset: 0x000BE064
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(SR.Arg_MustBeString);
			}
			return this.CompareTo(text);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000BE9F2 File Offset: 0x000BDBF2
		[NullableContext(2)]
		public int CompareTo(string strB)
		{
			return string.Compare(this, strB, StringComparison.CurrentCulture);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000BEE92 File Offset: 0x000BE092
		public bool EndsWith(string value)
		{
			return this.EndsWith(value, StringComparison.CurrentCulture);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000BEE9C File Offset: 0x000BE09C
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsSuffix(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
			{
				int num = this.Length - value.Length;
				return num <= this.Length && this.AsSpan(num).SequenceEqual(value);
			}
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && Ordinal.CompareStringIgnoreCase(Unsafe.Add<char>(this.GetRawStringData(), this.Length - value.Length), value.Length, value.GetRawStringData(), value.Length) == 0;
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000BEFA0 File Offset: 0x000BE1A0
		public bool EndsWith(string value, bool ignoreCase, [Nullable(2)] CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			return cultureInfo.CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000BEFE0 File Offset: 0x000BE1E0
		public bool EndsWith(char value)
		{
			int num = this.Length - 1;
			return num < this.Length && this[num] == value;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000BF00C File Offset: 0x000BE20C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			string text = obj as string;
			return text != null && this.Length == text.Length && string.EqualsHelper(this, text);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000BF042 File Offset: 0x000BE242
		[NullableContext(2)]
		public bool Equals(string value)
		{
			return this == value || (value != null && this.Length == value.Length && string.EqualsHelper(this, value));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000BF068 File Offset: 0x000BE268
		[NullableContext(2)]
		public bool Equals(string value, StringComparison comparisonType)
		{
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value == null)
			{
				string.CheckStringComparison(comparisonType);
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.Ordinal:
				return this.Length == value.Length && string.EqualsHelper(this, value);
			case StringComparison.OrdinalIgnoreCase:
				return this.Length == value.Length && string.EqualsOrdinalIgnoreCaseNoLengthCheck(this, value);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000BF11C File Offset: 0x000BE31C
		[NullableContext(2)]
		public static bool Equals(string a, string b)
		{
			return a == b || (a != null && b != null && a.Length == b.Length && string.EqualsHelper(a, b));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000BF144 File Offset: 0x000BE344
		[NullableContext(2)]
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			if (a == b)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (a == null || b == null)
			{
				string.CheckStringComparison(comparisonType);
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(a, b, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.Ordinal:
				return a.Length == b.Length && string.EqualsHelper(a, b);
			case StringComparison.OrdinalIgnoreCase:
				return a.Length == b.Length && string.EqualsOrdinalIgnoreCaseNoLengthCheck(a, b);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000BF1FB File Offset: 0x000BE3FB
		[NullableContext(2)]
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000BF204 File Offset: 0x000BE404
		[NullableContext(2)]
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000BF210 File Offset: 0x000BE410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			ulong defaultSeed = Marvin.DefaultSeed;
			return Marvin.ComputeHash32(Unsafe.As<char, byte>(ref this._firstChar), (uint)(this._stringLength * 2), (uint)defaultSeed, (uint)(defaultSeed >> 32));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000BF242 File Offset: 0x000BE442
		public int GetHashCode(StringComparison comparisonType)
		{
			return StringComparer.FromComparison(comparisonType).GetHashCode(this);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000BF250 File Offset: 0x000BE450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal int GetHashCodeOrdinalIgnoreCase()
		{
			ulong defaultSeed = Marvin.DefaultSeed;
			return Marvin.ComputeHash32OrdinalIgnoreCase(ref this._firstChar, this._stringLength, (uint)defaultSeed, (uint)(defaultSeed >> 32));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000BF27C File Offset: 0x000BE47C
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHashCode(ReadOnlySpan<char> value)
		{
			ulong defaultSeed = Marvin.DefaultSeed;
			return Marvin.ComputeHash32(Unsafe.As<char, byte>(MemoryMarshal.GetReference<char>(value)), (uint)(value.Length * 2), (uint)defaultSeed, (uint)(defaultSeed >> 32));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000BF2B0 File Offset: 0x000BE4B0
		[NullableContext(0)]
		public static int GetHashCode(ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.GetHashCode(value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.GetHashCode(value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return string.GetHashCode(value);
			case StringComparison.OrdinalIgnoreCase:
				return string.GetHashCodeOrdinalIgnoreCase(value);
			default:
				ThrowHelper.ThrowArgumentException(ExceptionResource.NotSupported_StringComparison, ExceptionArgument.comparisonType);
				return 0;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000BF320 File Offset: 0x000BE520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetHashCodeOrdinalIgnoreCase(ReadOnlySpan<char> value)
		{
			ulong defaultSeed = Marvin.DefaultSeed;
			return Marvin.ComputeHash32OrdinalIgnoreCase(MemoryMarshal.GetReference<char>(value), value.Length, (uint)defaultSeed, (uint)(defaultSeed >> 32));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000BF34C File Offset: 0x000BE54C
		internal unsafe int GetNonRandomizedHashCode()
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				uint num = 352654597U;
				uint num2 = num;
				uint* ptr3 = (uint*)ptr2;
				int i = this.Length;
				while (i > 2)
				{
					i -= 4;
					num = (BitOperations.RotateLeft(num, 5) + num ^ *ptr3);
					num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ ptr3[1]);
					ptr3 += 2;
				}
				if (i > 0)
				{
					num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ *ptr3);
				}
				return (int)(num + num2 * 1566083941U);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000BF3C8 File Offset: 0x000BE5C8
		internal unsafe int GetNonRandomizedHashCodeOrdinalIgnoreCase()
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				uint num = 352654597U;
				uint num2 = num;
				uint* ptr3 = (uint*)ptr2;
				int i = this.Length;
				while (i > 2)
				{
					i -= 4;
					num = (BitOperations.RotateLeft(num, 5) + num ^ (*ptr3 | 2097184U));
					num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ (ptr3[1] | 2097184U));
					ptr3 += 2;
				}
				if (i > 0)
				{
					num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ (*ptr3 | 2097184U));
				}
				return (int)(num + num2 * 1566083941U);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000BF454 File Offset: 0x000BE654
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.StartsWith(value, StringComparison.CurrentCulture);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000BF46C File Offset: 0x000BE66C
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsPrefix(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return this.Length >= value.Length && this._firstChar == value._firstChar && (value.Length == 1 || SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(this.GetRawStringData()), Unsafe.As<char, byte>(value.GetRawStringData()), (UIntPtr)((IntPtr)value.Length * (IntPtr)2)));
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && Ordinal.EqualsIgnoreCase(this.GetRawStringData(), value.GetRawStringData(), value.Length);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000BF578 File Offset: 0x000BE778
		public bool StartsWith(string value, bool ignoreCase, [Nullable(2)] CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			return cultureInfo.CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000BF5B8 File Offset: 0x000BE7B8
		public bool StartsWith(char value)
		{
			return this.Length != 0 && this._firstChar == value;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000BF5CD File Offset: 0x000BE7CD
		internal static void CheckStringComparison(StringComparison comparisonType)
		{
			if (comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.NotSupported_StringComparison, ExceptionArgument.comparisonType);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000BF5DC File Offset: 0x000BE7DC
		internal static CompareOptions GetCaseCompareOfComparisonCulture(StringComparison comparisonType)
		{
			return (CompareOptions)(comparisonType & StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000BF5E4 File Offset: 0x000BE7E4
		private static CompareOptions GetCompareOptionsFromOrdinalStringComparison(StringComparison comparisonType)
		{
			return (CompareOptions)((comparisonType & -comparisonType) << 28);
		}

		// Token: 0x060006BB RID: 1723
		[NullableContext(2)]
		[DynamicDependency("Ctor(System.Char[])")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value);

		// Token: 0x060006BC RID: 1724 RVA: 0x000BF5FC File Offset: 0x000BE7FC
		private string Ctor(char[] value)
		{
			if (value == null || value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(value.Length);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, MemoryMarshal.GetArrayDataReference<char>(value), elementCount);
			return text;
		}

		// Token: 0x060006BD RID: 1725
		[DynamicDependency("Ctor(System.Char[],System.Int32,System.Int32)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value, int startIndex, int length);

		// Token: 0x060006BE RID: 1726 RVA: 0x000BF63C File Offset: 0x000BE83C
		private string Ctor(char[] value, int startIndex, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(length);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, Unsafe.Add<char>(MemoryMarshal.GetArrayDataReference<char>(value), startIndex), elementCount);
			return text;
		}

		// Token: 0x060006BF RID: 1727
		[CLSCompliant(false)]
		[NullableContext(0)]
		[DynamicDependency("Ctor(System.Char*)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value);

		// Token: 0x060006C0 RID: 1728 RVA: 0x000BF6C8 File Offset: 0x000BE8C8
		private unsafe string Ctor(char* ptr)
		{
			if (ptr == null)
			{
				return string.Empty;
			}
			int num = string.wcslen(ptr);
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, ref *ptr, elementCount);
			return text;
		}

		// Token: 0x060006C1 RID: 1729
		[NullableContext(0)]
		[CLSCompliant(false)]
		[DynamicDependency("Ctor(System.Char*,System.Int32,System.Int32)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value, int startIndex, int length);

		// Token: 0x060006C2 RID: 1730 RVA: 0x000BF710 File Offset: 0x000BE910
		private unsafe string Ctor(char* ptr, int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			char* ptr2 = ptr + startIndex;
			if (ptr2 < ptr)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_PartialWCHAR);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (ptr == null)
			{
				throw new ArgumentOutOfRangeException("ptr", SR.ArgumentOutOfRange_PartialWCHAR);
			}
			string text = string.FastAllocateString(length);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, ref *ptr2, elementCount);
			return text;
		}

		// Token: 0x060006C3 RID: 1731
		[NullableContext(0)]
		[CLSCompliant(false)]
		[DynamicDependency("Ctor(System.SByte*)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value);

		// Token: 0x060006C4 RID: 1732 RVA: 0x000BF79C File Offset: 0x000BE99C
		private unsafe string Ctor(sbyte* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			int numBytes = string.strlen((byte*)value);
			return string.CreateStringForSByteConstructor((byte*)value, numBytes);
		}

		// Token: 0x060006C5 RID: 1733
		[NullableContext(0)]
		[CLSCompliant(false)]
		[DynamicDependency("Ctor(System.SByte*,System.Int32,System.Int32)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		// Token: 0x060006C6 RID: 1734 RVA: 0x000BF7C4 File Offset: 0x000BE9C4
		private unsafe string Ctor(sbyte* value, int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (value == null)
			{
				if (length == 0)
				{
					return string.Empty;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				byte* ptr = (byte*)(value + startIndex);
				if (ptr < (byte*)value)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_PartialWCHAR);
				}
				return string.CreateStringForSByteConstructor(ptr, length);
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000BF834 File Offset: 0x000BEA34
		private unsafe static string CreateStringForSByteConstructor(byte* pb, int numBytes)
		{
			if (numBytes == 0)
			{
				return string.Empty;
			}
			int num = Interop.Kernel32.MultiByteToWideChar(0U, 1U, pb, numBytes, null, 0);
			if (num == 0)
			{
				throw new ArgumentException(SR.Arg_InvalidANSIString);
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &text._firstChar)
			{
				char* lpWideCharStr = ptr;
				num = Interop.Kernel32.MultiByteToWideChar(0U, 1U, pb, numBytes, lpWideCharStr, num);
			}
			if (num == 0)
			{
				throw new ArgumentException(SR.Arg_InvalidANSIString);
			}
			return text;
		}

		// Token: 0x060006C8 RID: 1736
		[NullableContext(0)]
		[CLSCompliant(false)]
		[DynamicDependency("Ctor(System.SByte*,System.Int32,System.Int32,System.Text.Encoding)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length, [Nullable(1)] Encoding enc);

		// Token: 0x060006C9 RID: 1737 RVA: 0x000BF894 File Offset: 0x000BEA94
		private unsafe string Ctor(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (enc == null)
			{
				return new string(value, startIndex, length);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (value == null)
			{
				if (length == 0)
				{
					return string.Empty;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				byte* ptr = (byte*)(value + startIndex);
				if (ptr < (byte*)value)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_PartialWCHAR);
				}
				return enc.GetString(new ReadOnlySpan<byte>((void*)ptr, length));
			}
		}

		// Token: 0x060006CA RID: 1738
		[DynamicDependency("Ctor(System.Char,System.Int32)")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char c, int count);

		// Token: 0x060006CB RID: 1739 RVA: 0x000BF918 File Offset: 0x000BEB18
		private unsafe string Ctor(char c, int count)
		{
			if (count > 0)
			{
				string text = string.FastAllocateString(count);
				if (c != '\0')
				{
					fixed (char* ptr = &text._firstChar)
					{
						char* ptr2 = ptr;
						uint num = (uint)((uint)c << 16 | c);
						uint* ptr3 = (uint*)ptr2;
						if (count >= 4)
						{
							count -= 4;
							do
							{
								*ptr3 = num;
								ptr3[1] = num;
								ptr3 += 2;
								count -= 4;
							}
							while (count >= 0);
						}
						if ((count & 2) != 0)
						{
							*ptr3 = num;
							ptr3++;
						}
						if ((count & 1) != 0)
						{
							*(short*)ptr3 = (short)c;
						}
					}
				}
				return text;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
		}

		// Token: 0x060006CC RID: 1740
		[NullableContext(0)]
		[DynamicDependency("Ctor(System.ReadOnlySpan{System.Char})")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(ReadOnlySpan<char> value);

		// Token: 0x060006CD RID: 1741 RVA: 0x000BF9A4 File Offset: 0x000BEBA4
		private string Ctor(ReadOnlySpan<char> value)
		{
			if (value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(value.Length);
			Buffer.Memmove<char>(ref text._firstChar, MemoryMarshal.GetReference<char>(value), (UIntPtr)value.Length);
			return text;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000BF9E8 File Offset: 0x000BEBE8
		public static string Create<[Nullable(2)] TState>(int length, TState state, SpanAction<char, TState> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (length > 0)
			{
				string text = string.FastAllocateString(length);
				action(new Span<char>(text.GetRawStringData(), length), state);
				return text;
			}
			if (length == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("length");
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000BFA38 File Offset: 0x000BEC38
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ReadOnlySpan<char>([Nullable(2)] string value)
		{
			if (value == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(value.GetRawStringData(), value.Length);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000BFA63 File Offset: 0x000BEC63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool TryGetSpan(int startIndex, int count, out ReadOnlySpan<char> slice)
		{
			if ((ulong)startIndex + (ulong)count > (ulong)this.Length)
			{
				slice = default(ReadOnlySpan<char>);
				return false;
			}
			slice = new ReadOnlySpan<char>(Unsafe.Add<char>(ref this._firstChar, startIndex), count);
			return true;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000AC098 File Offset: 0x000AB298
		public object Clone()
		{
			return this;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000BFA98 File Offset: 0x000BEC98
		public static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			string text = string.FastAllocateString(str.Length);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, ref str._firstChar, elementCount);
			return text;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000BFADC File Offset: 0x000BECDC
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count > this.Length - sourceIndex)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_IndexCount);
			}
			if (destinationIndex > destination.Length - count || destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_IndexCount);
			}
			Buffer.Memmove<char>(Unsafe.Add<char>(MemoryMarshal.GetArrayDataReference<char>(destination), destinationIndex), Unsafe.Add<char>(ref this._firstChar, sourceIndex), (UIntPtr)count);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000BFB7C File Offset: 0x000BED7C
		public char[] ToCharArray()
		{
			if (this.Length == 0)
			{
				return Array.Empty<char>();
			}
			char[] array = new char[this.Length];
			Buffer.Memmove<char>(MemoryMarshal.GetArrayDataReference<char>(array), ref this._firstChar, (UIntPtr)this.Length);
			return array;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000BFBBC File Offset: 0x000BEDBC
		public char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (length > 0)
			{
				char[] array = new char[length];
				Buffer.Memmove<char>(MemoryMarshal.GetArrayDataReference<char>(array), Unsafe.Add<char>(ref this._firstChar, startIndex), (UIntPtr)length);
				return array;
			}
			if (length == 0)
			{
				return Array.Empty<char>();
			}
			throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000BFC2F File Offset: 0x000BEE2F
		[NullableContext(2)]
		[NonVersionable]
		public static bool IsNullOrEmpty([NotNullWhen(false)] string value)
		{
			return value == null || 0 >= value.Length;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000BFC40 File Offset: 0x000BEE40
		[NullableContext(2)]
		public static bool IsNullOrWhiteSpace([NotNullWhen(false)] string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000BFC74 File Offset: 0x000BEE74
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NonVersionable]
		public ref readonly char GetPinnableReference()
		{
			return ref this._firstChar;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000BFC74 File Offset: 0x000BEE74
		internal ref char GetRawStringData()
		{
			return ref this._firstChar;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000BFC7C File Offset: 0x000BEE7C
		internal unsafe static string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
		{
			int charCount = encoding.GetCharCount(bytes, byteLength);
			if (charCount == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(charCount);
			fixed (char* ptr = &text._firstChar)
			{
				char* chars = ptr;
				int chars2 = encoding.GetChars(bytes, byteLength, chars, charCount);
			}
			return text;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000BFCBC File Offset: 0x000BEEBC
		internal static string CreateFromChar(char c)
		{
			string text = string.FastAllocateString(1);
			text._firstChar = c;
			return text;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000BFCD8 File Offset: 0x000BEED8
		internal unsafe static string CreateFromChar(char c1, char c2)
		{
			string text = string.FastAllocateString(2);
			text._firstChar = c1;
			*Unsafe.Add<char>(ref text._firstChar, 1) = c2;
			return text;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000BFD02 File Offset: 0x000BEF02
		internal unsafe static void wstrcpy(char* dmem, char* smem, int charCount)
		{
			Buffer.Memmove((byte*)dmem, (byte*)smem, (UIntPtr)(charCount * 2));
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000AC098 File Offset: 0x000AB298
		public override string ToString()
		{
			return this;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000AC098 File Offset: 0x000AB298
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000BFD0F File Offset: 0x000BEF0F
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000BFD0F File Offset: 0x000BEF0F
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000BFD0F File Offset: 0x000BEF0F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000BFD17 File Offset: 0x000BEF17
		public StringRuneEnumerator EnumerateRunes()
		{
			return new StringRuneEnumerator(this);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000BFD20 File Offset: 0x000BEF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static int wcslen(char* ptr)
		{
			int num = SpanHelpers.IndexOf(ref *ptr, '\0', int.MaxValue);
			if (num < 0)
			{
				string.ThrowMustBeNullTerminatedString();
			}
			return num;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000BFD44 File Offset: 0x000BEF44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static int strlen(byte* ptr)
		{
			int num = SpanHelpers.IndexOf(ref *ptr, 0, int.MaxValue);
			if (num < 0)
			{
				string.ThrowMustBeNullTerminatedString();
			}
			return num;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000BFD68 File Offset: 0x000BEF68
		[DoesNotReturn]
		private static void ThrowMustBeNullTerminatedString()
		{
			throw new ArgumentException(SR.Arg_MustBeNullTerminatedString);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000BFD74 File Offset: 0x000BEF74
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000BFD78 File Offset: 0x000BEF78
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000BFD81 File Offset: 0x000BEF81
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000BFD8A File Offset: 0x000BEF8A
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000BFD93 File Offset: 0x000BEF93
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000BFD9C File Offset: 0x000BEF9C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000BFDA5 File Offset: 0x000BEFA5
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000BFDAE File Offset: 0x000BEFAE
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000BFDB7 File Offset: 0x000BEFB7
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000BFDC0 File Offset: 0x000BEFC0
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000BFDC9 File Offset: 0x000BEFC9
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000BFDD2 File Offset: 0x000BEFD2
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000BFDDB File Offset: 0x000BEFDB
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000BFDE4 File Offset: 0x000BEFE4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000BFDED File Offset: 0x000BEFED
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000B5086 File Offset: 0x000B4286
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000BFDF6 File Offset: 0x000BEFF6
		public bool IsNormalized()
		{
			return this.IsNormalized(NormalizationForm.FormC);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000BFDFF File Offset: 0x000BEFFF
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			return (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)) || Normalization.IsNormalized(this, normalizationForm);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000BFE22 File Offset: 0x000BF022
		public string Normalize()
		{
			return this.Normalize(NormalizationForm.FormC);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000BFE2B File Offset: 0x000BF02B
		public string Normalize(NormalizationForm normalizationForm)
		{
			if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD))
			{
				return this;
			}
			return Normalization.Normalize(this, normalizationForm);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000BFE50 File Offset: 0x000BF050
		private unsafe bool IsAscii()
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* pBuffer = ptr;
				return ASCIIUtility.GetIndexOfFirstNonAsciiChar(pBuffer, (UIntPtr)this.Length) == (UIntPtr)this.Length;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000BFE7D File Offset: 0x000BF07D
		private static void FillStringChecked(string dest, int destPos, string src)
		{
			if (src.Length > dest.Length - destPos)
			{
				throw new IndexOutOfRangeException();
			}
			Buffer.Memmove<char>(Unsafe.Add<char>(ref dest._firstChar, destPos), ref src._firstChar, (UIntPtr)src.Length);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000BFEB3 File Offset: 0x000BF0B3
		public static string Concat([Nullable(2)] object arg0)
		{
			return ((arg0 != null) ? arg0.ToString() : null) ?? string.Empty;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000BFECA File Offset: 0x000BF0CA
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Concat(object arg0, object arg1)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000BFEF1 File Offset: 0x000BF0F1
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Concat(object arg0, object arg1, object arg2)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			if (arg2 == null)
			{
				arg2 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString() + arg2.ToString();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000BFF28 File Offset: 0x000BF128
		public static string Concat([Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.Length <= 1)
			{
				string result;
				if (args.Length != 0)
				{
					object obj = args[0];
					if ((result = ((obj != null) ? obj.ToString() : null)) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			string[] array = new string[args.Length];
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				object obj2 = args[i];
				string text = ((obj2 != null) ? obj2.ToString() : null) ?? string.Empty;
				array[i] = text;
				num += text.Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			if (num == 0)
			{
				return string.Empty;
			}
			string text2 = string.FastAllocateString(num);
			int num2 = 0;
			foreach (string text3 in array)
			{
				string.FillStringChecked(text2, num2, text3);
				num2 += text3.Length;
			}
			return text2;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000C0000 File Offset: 0x000BF200
		public unsafe static string Concat<[Nullable(2)] T>(IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (typeof(T) == typeof(char))
			{
				using (IEnumerator<char> enumerator = Unsafe.As<IEnumerable<char>>(values).GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						return string.Empty;
					}
					char c = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return string.CreateFromChar(c);
					}
					Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
					ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
					valueStringBuilder.Append(c);
					do
					{
						c = enumerator.Current;
						valueStringBuilder.Append(c);
					}
					while (enumerator.MoveNext());
					return valueStringBuilder.ToString();
				}
			}
			string result;
			using (IEnumerator<T> enumerator2 = values.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					T t = enumerator2.Current;
					string text = (t != null) ? t.ToString() : null;
					if (!enumerator2.MoveNext())
					{
						result = (text ?? string.Empty);
					}
					else
					{
						Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
						ValueStringBuilder valueStringBuilder2 = new ValueStringBuilder(initialBuffer);
						valueStringBuilder2.Append(text);
						do
						{
							t = enumerator2.Current;
							if (t != null)
							{
								valueStringBuilder2.Append(t.ToString());
							}
						}
						while (enumerator2.MoveNext());
						result = valueStringBuilder2.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000C01B4 File Offset: 0x000BF3B4
		public unsafe static string Concat([Nullable(new byte[]
		{
			1,
			2
		})] IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string result;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					string text = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						result = (text ?? string.Empty);
					}
					else
					{
						Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
						ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
						valueStringBuilder.Append(text);
						do
						{
							valueStringBuilder.Append(enumerator.Current);
						}
						while (enumerator.MoveNext());
						result = valueStringBuilder.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000C0268 File Offset: 0x000BF468
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Concat(string str0, string str1)
		{
			if (string.IsNullOrEmpty(str0))
			{
				if (string.IsNullOrEmpty(str1))
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (string.IsNullOrEmpty(str1))
				{
					return str0;
				}
				int length = str0.Length;
				string text = string.FastAllocateString(length + str1.Length);
				string.FillStringChecked(text, 0, str0);
				string.FillStringChecked(text, length, str1);
				return text;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000C02C0 File Offset: 0x000BF4C0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Concat(string str0, string str1, string str2)
		{
			if (string.IsNullOrEmpty(str0))
			{
				return str1 + str2;
			}
			if (string.IsNullOrEmpty(str1))
			{
				return str0 + str2;
			}
			if (string.IsNullOrEmpty(str2))
			{
				return str0 + str1;
			}
			int length = str0.Length + str1.Length + str2.Length;
			string text = string.FastAllocateString(length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			return text;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000C0344 File Offset: 0x000BF544
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Concat(string str0, string str1, string str2, string str3)
		{
			if (string.IsNullOrEmpty(str0))
			{
				return str1 + str2 + str3;
			}
			if (string.IsNullOrEmpty(str1))
			{
				return str0 + str2 + str3;
			}
			if (string.IsNullOrEmpty(str2))
			{
				return str0 + str1 + str3;
			}
			if (string.IsNullOrEmpty(str3))
			{
				return str0 + str1 + str2;
			}
			int length = str0.Length + str1.Length + str2.Length + str3.Length;
			string text = string.FastAllocateString(length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			string.FillStringChecked(text, str0.Length + str1.Length + str2.Length, str3);
			return text;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000C0400 File Offset: 0x000BF600
		[NullableContext(0)]
		[return: Nullable(1)]
		public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1)
		{
			int num = checked(str0.Length + str1.Length);
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			Span<char> destination = new Span<char>(text.GetRawStringData(), text.Length);
			str0.CopyTo(destination);
			str1.CopyTo(destination.Slice(str0.Length));
			return text;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000C0460 File Offset: 0x000BF660
		[NullableContext(0)]
		[return: Nullable(1)]
		public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
		{
			int num = checked(str0.Length + str1.Length + str2.Length);
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			Span<char> destination = new Span<char>(text.GetRawStringData(), text.Length);
			str0.CopyTo(destination);
			destination = destination.Slice(str0.Length);
			str1.CopyTo(destination);
			destination = destination.Slice(str1.Length);
			str2.CopyTo(destination);
			return text;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000C04E0 File Offset: 0x000BF6E0
		[NullableContext(0)]
		[return: Nullable(1)]
		public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2, ReadOnlySpan<char> str3)
		{
			int num = checked(str0.Length + str1.Length + str2.Length + str3.Length);
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			Span<char> destination = new Span<char>(text.GetRawStringData(), text.Length);
			str0.CopyTo(destination);
			destination = destination.Slice(str0.Length);
			str1.CopyTo(destination);
			destination = destination.Slice(str1.Length);
			str2.CopyTo(destination);
			destination = destination.Slice(str2.Length);
			str3.CopyTo(destination);
			return text;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000C0580 File Offset: 0x000BF780
		public static string Concat([Nullable(new byte[]
		{
			1,
			2
		})] params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length <= 1)
			{
				string result;
				if (values.Length != 0)
				{
					if ((result = values[0]) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			long num = 0L;
			foreach (string text in values)
			{
				if (text != null)
				{
					num += (long)text.Length;
				}
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			int num2 = (int)num;
			if (num2 == 0)
			{
				return string.Empty;
			}
			string text2 = string.FastAllocateString(num2);
			int num3 = 0;
			foreach (string text3 in values)
			{
				if (!string.IsNullOrEmpty(text3))
				{
					int length = text3.Length;
					if (length > num2 - num3)
					{
						num3 = -1;
						break;
					}
					string.FillStringChecked(text2, num3, text3);
					num3 += length;
				}
			}
			if (num3 != num2)
			{
				return string.Concat((string[])values.Clone());
			}
			return text2;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000C065F File Offset: 0x000BF85F
		public static string Format(string format, [Nullable(2)] object arg0)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000C066E File Offset: 0x000BF86E
		public static string Format(string format, [Nullable(2)] object arg0, [Nullable(2)] object arg1)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000C067E File Offset: 0x000BF87E
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Format([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000C068F File Offset: 0x000BF88F
		public static string Format(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000C06B6 File Offset: 0x000BF8B6
		public static string Format([Nullable(2)] IFormatProvider provider, string format, [Nullable(2)] object arg0)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000C06C5 File Offset: 0x000BF8C5
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Format(IFormatProvider provider, [Nullable(1)] string format, object arg0, object arg1)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000C06D5 File Offset: 0x000BF8D5
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Format(IFormatProvider provider, [Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000C06E7 File Offset: 0x000BF8E7
		public static string Format([Nullable(2)] IFormatProvider provider, string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000C0710 File Offset: 0x000BF910
		private unsafe static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.EnsureCapacity(format.Length + args.Length * 8);
			valueStringBuilder.AppendFormatHelper(provider, format, args);
			return valueStringBuilder.ToString();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000C0774 File Offset: 0x000BF974
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int length = this.Length;
			int length2 = value.Length;
			if (length == 0)
			{
				return value;
			}
			if (length2 == 0)
			{
				return this;
			}
			int length3 = length + length2;
			string text = string.FastAllocateString(length3);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &value._firstChar)
				{
					char* smem = ptr3;
					fixed (char* ptr4 = &text._firstChar)
					{
						char* ptr5 = ptr4;
						string.wstrcpy(ptr5, ptr2, startIndex);
						string.wstrcpy(ptr5 + startIndex, smem, length2);
						string.wstrcpy(ptr5 + startIndex + length2, ptr2 + startIndex, length - startIndex);
					}
				}
			}
			return text;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000C0832 File Offset: 0x000BFA32
		public static string Join(char separator, [Nullable(new byte[]
		{
			1,
			2
		})] params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000C084D File Offset: 0x000BFA4D
		public unsafe static string Join(char separator, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] values)
		{
			return string.JoinCore(&separator, 1, values);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000C0859 File Offset: 0x000BFA59
		public unsafe static string Join<[Nullable(2)] T>(char separator, IEnumerable<T> values)
		{
			return string.JoinCore<T>(&separator, 1, values);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000C0865 File Offset: 0x000BFA65
		public unsafe static string Join(char separator, [Nullable(new byte[]
		{
			1,
			2
		})] string[] value, int startIndex, int count)
		{
			return string.JoinCore(&separator, 1, value, startIndex, count);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000C0873 File Offset: 0x000BFA73
		public static string Join([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000C0890 File Offset: 0x000BFA90
		public unsafe static string Join([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] values)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			fixed (char* ptr = &separator._firstChar)
			{
				char* separator2 = ptr;
				return string.JoinCore(separator2, separator.Length, values);
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000C08C0 File Offset: 0x000BFAC0
		public unsafe static string Join<[Nullable(2)] T>([Nullable(2)] string separator, IEnumerable<T> values)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			fixed (char* ptr = &separator._firstChar)
			{
				char* separator2 = ptr;
				return string.JoinCore<T>(separator2, separator.Length, values);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000C08F0 File Offset: 0x000BFAF0
		public unsafe static string Join([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string result;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					string text = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						result = (text ?? string.Empty);
					}
					else
					{
						Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
						ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
						valueStringBuilder.Append(text);
						do
						{
							valueStringBuilder.Append(separator);
							valueStringBuilder.Append(enumerator.Current);
						}
						while (enumerator.MoveNext());
						result = valueStringBuilder.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000C09AC File Offset: 0x000BFBAC
		public unsafe static string Join([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] string[] value, int startIndex, int count)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			fixed (char* ptr = &separator._firstChar)
			{
				char* separator2 = ptr;
				return string.JoinCore(separator2, separator.Length, value, startIndex, count);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000C09DC File Offset: 0x000BFBDC
		private unsafe static string JoinCore(char* separator, int separatorLength, object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				return string.Empty;
			}
			object obj = values[0];
			string text = (obj != null) ? obj.ToString() : null;
			if (values.Length == 1)
			{
				return text ?? string.Empty;
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.Append(text);
			for (int i = 1; i < values.Length; i++)
			{
				valueStringBuilder.Append(separator, separatorLength);
				object obj2 = values[i];
				if (obj2 != null)
				{
					valueStringBuilder.Append(obj2.ToString());
				}
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000C0A80 File Offset: 0x000BFC80
		private unsafe static string JoinCore<T>(char* separator, int separatorLength, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string result;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					T t = enumerator.Current;
					string text = (t != null) ? t.ToString() : null;
					if (!enumerator.MoveNext())
					{
						result = (text ?? string.Empty);
					}
					else
					{
						Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
						ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
						valueStringBuilder.Append(text);
						do
						{
							t = enumerator.Current;
							valueStringBuilder.Append(separator, separatorLength);
							if (t != null)
							{
								valueStringBuilder.Append(t.ToString());
							}
						}
						while (enumerator.MoveNext());
						result = valueStringBuilder.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000C0B74 File Offset: 0x000BFD74
		private unsafe static string JoinCore(char* separator, int separatorLength, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (count <= 1)
			{
				string result;
				if (count != 0)
				{
					if ((result = value[startIndex]) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			long num = (long)(count - 1) * (long)separatorLength;
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			int num2 = (int)num;
			int i = startIndex;
			int num3 = startIndex + count;
			while (i < num3)
			{
				string text = value[i];
				if (text != null)
				{
					num2 += text.Length;
					if (num2 < 0)
					{
						throw new OutOfMemoryException();
					}
				}
				i++;
			}
			string text2 = string.FastAllocateString(num2);
			int num4 = 0;
			int j = startIndex;
			int num5 = startIndex + count;
			while (j < num5)
			{
				string text3 = value[j];
				if (text3 != null)
				{
					int length = text3.Length;
					if (length > num2 - num4)
					{
						num4 = -1;
						break;
					}
					string.FillStringChecked(text2, num4, text3);
					num4 += length;
				}
				if (j < num5 - 1)
				{
					fixed (char* ptr = &text2._firstChar)
					{
						char* ptr2 = ptr;
						if (separatorLength == 1)
						{
							ptr2[num4] = *separator;
						}
						else
						{
							string.wstrcpy(ptr2 + num4, separator, separatorLength);
						}
					}
					num4 += separatorLength;
				}
				j++;
			}
			if (num4 != num2)
			{
				return string.JoinCore(separator, separatorLength, (string[])value.Clone(), startIndex, count);
			}
			return text2;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000C0CDF File Offset: 0x000BFEDF
		public string PadLeft(int totalWidth)
		{
			return this.PadLeft(totalWidth, ' ');
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000C0CEC File Offset: 0x000BFEEC
		public unsafe string PadLeft(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int length = this.Length;
			int num = totalWidth - length;
			if (num <= 0)
			{
				return this;
			}
			string text = string.FastAllocateString(totalWidth);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				for (int i = 0; i < num; i++)
				{
					ptr2[i] = paddingChar;
				}
				fixed (char* ptr3 = &this._firstChar)
				{
					char* smem = ptr3;
					string.wstrcpy(ptr2 + num, smem, length);
				}
			}
			return text;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000C0D6E File Offset: 0x000BFF6E
		public string PadRight(int totalWidth)
		{
			return this.PadRight(totalWidth, ' ');
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000C0D7C File Offset: 0x000BFF7C
		public unsafe string PadRight(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int length = this.Length;
			int num = totalWidth - length;
			if (num <= 0)
			{
				return this;
			}
			string text = string.FastAllocateString(totalWidth);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &this._firstChar)
				{
					char* smem = ptr3;
					string.wstrcpy(ptr2, smem, length);
				}
				for (int i = 0; i < num; i++)
				{
					ptr2[length + i] = paddingChar;
				}
			}
			return text;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000C0DFC File Offset: 0x000BFFFC
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
			}
			int length = this.Length;
			if (count > length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_IndexCount);
			}
			if (count == 0)
			{
				return this;
			}
			int num = length - count;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr4, ptr2, startIndex);
					string.wstrcpy(ptr4 + startIndex, ptr2 + startIndex + count, num - startIndex);
				}
			}
			return text;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000C0EAC File Offset: 0x000C00AC
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndexLessThanLength);
			}
			return this.Substring(0, startIndex);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000C0EE3 File Offset: 0x000C00E3
		public string Replace(string oldValue, [Nullable(2)] string newValue, bool ignoreCase, [Nullable(2)] CultureInfo culture)
		{
			return this.ReplaceCore(oldValue, newValue, (culture != null) ? culture.CompareInfo : null, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000C0F04 File Offset: 0x000C0104
		public string Replace(string oldValue, [Nullable(2)] string newValue, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.CurrentCulture.CompareInfo, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CompareInfo.Invariant, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return this.Replace(oldValue, newValue);
			case StringComparison.OrdinalIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CompareInfo.Invariant, CompareOptions.OrdinalIgnoreCase);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000C0F8C File Offset: 0x000C018C
		private string ReplaceCore(string oldValue, string newValue, CompareInfo ci, CompareOptions options)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException(SR.Argument_StringZeroLength, "oldValue");
			}
			return string.ReplaceCore(this, oldValue.AsSpan(), newValue.AsSpan(), ci ?? CultureInfo.CurrentCulture.CompareInfo, options) ?? this;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000C0FEC File Offset: 0x000C01EC
		private unsafe static string ReplaceCore(ReadOnlySpan<char> searchSpace, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue, CompareInfo compareInfo, CompareOptions options)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.EnsureCapacity(searchSpace.Length);
			bool flag = false;
			for (;;)
			{
				int num2;
				int num = compareInfo.IndexOf(searchSpace, oldValue, options, out num2);
				if (num < 0 || num2 == 0)
				{
					break;
				}
				valueStringBuilder.Append(searchSpace.Slice(0, num));
				valueStringBuilder.Append(newValue);
				searchSpace = searchSpace.Slice(num + num2);
				flag = true;
			}
			if (!flag)
			{
				valueStringBuilder.Dispose();
				return null;
			}
			valueStringBuilder.Append(searchSpace);
			return valueStringBuilder.ToString();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000C1084 File Offset: 0x000C0284
		public string Replace(char oldChar, char newChar)
		{
			if (oldChar == newChar)
			{
				return this;
			}
			int num = this.IndexOf(oldChar);
			if (num < 0)
			{
				return this;
			}
			int i = this.Length - num;
			string text = string.FastAllocateString(this.Length);
			int num2 = num;
			if (num2 > 0)
			{
				Buffer.Memmove<char>(ref text._firstChar, ref this._firstChar, (UIntPtr)num2);
			}
			ref ushort ptr = ref Unsafe.Add<ushort>(Unsafe.As<char, ushort>(ref this._firstChar), num2);
			ref ushort ptr2 = ref Unsafe.Add<ushort>(Unsafe.As<char, ushort>(ref text._firstChar), num2);
			if (Vector.IsHardwareAccelerated && i >= Vector<ushort>.Count)
			{
				Vector<ushort> right = new Vector<ushort>((ushort)oldChar);
				Vector<ushort> left = new Vector<ushort>((ushort)newChar);
				do
				{
					Vector<ushort> vector = Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<ushort, byte>(ref ptr));
					Vector<ushort> condition = Vector.Equals<ushort>(vector, right);
					Vector<ushort> value = Vector.ConditionalSelect<ushort>(condition, left, vector);
					Unsafe.WriteUnaligned<Vector<ushort>>(Unsafe.As<ushort, byte>(ref ptr2), value);
					ptr = Unsafe.Add<ushort>(ref ptr, Vector<ushort>.Count);
					ptr2 = Unsafe.Add<ushort>(ref ptr2, Vector<ushort>.Count);
					i -= Vector<ushort>.Count;
				}
				while (i >= Vector<ushort>.Count);
			}
			while (i > 0)
			{
				ushort num3 = ptr;
				ptr2 = (ushort)((num3 == (ushort)oldChar) ? newChar : ((char)num3));
				ptr = Unsafe.Add<ushort>(ref ptr, 1);
				ptr2 = Unsafe.Add<ushort>(ref ptr2, 1);
				i--;
			}
			return text;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000C11B0 File Offset: 0x000C03B0
		public unsafe string Replace(string oldValue, [Nullable(2)] string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException(SR.Argument_StringZeroLength, "oldValue");
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			Span<int> initialSpan = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(initialSpan);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				int i = 0;
				int num = this.Length - oldValue.Length;
				IL_B8:
				while (i <= num)
				{
					char* ptr3 = ptr2 + i;
					for (int j = 0; j < oldValue.Length; j++)
					{
						if (ptr3[j] != oldValue[j])
						{
							i++;
							goto IL_B8;
						}
					}
					valueListBuilder.Append(i);
					i += oldValue.Length;
				}
			}
			if (valueListBuilder.Length == 0)
			{
				return this;
			}
			string result = this.ReplaceHelper(oldValue.Length, newValue, valueListBuilder.AsSpan());
			valueListBuilder.Dispose();
			return result;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000C12A8 File Offset: 0x000C04A8
		private unsafe string ReplaceHelper(int oldValueLength, string newValue, ReadOnlySpan<int> indices)
		{
			long num = (long)this.Length + (long)(newValue.Length - oldValueLength) * (long)indices.Length;
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			string text = string.FastAllocateString((int)num);
			Span<char> span = new Span<char>(text.GetRawStringData(), text.Length);
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < indices.Length; i++)
			{
				int num4 = *indices[i];
				int num5 = num4 - num2;
				if (num5 != 0)
				{
					this.AsSpan(num2, num5).CopyTo(span.Slice(num3));
					num3 += num5;
				}
				num2 = num4 + oldValueLength;
				newValue.AsSpan().CopyTo(span.Slice(num3));
				num3 += newValue.Length;
			}
			this.AsSpan(num2).CopyTo(span.Slice(num3));
			return text;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000C138E File Offset: 0x000C058E
		public string[] Split(char separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(new ReadOnlySpan<char>(ref separator, 1), int.MaxValue, options);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000C13A4 File Offset: 0x000C05A4
		public string[] Split(char separator, int count, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(new ReadOnlySpan<char>(ref separator, 1), count, options);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000C13B6 File Offset: 0x000C05B6
		public string[] Split([Nullable(2)] params char[] separator)
		{
			return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000C13CA File Offset: 0x000C05CA
		public string[] Split([Nullable(2)] char[] separator, int count)
		{
			return this.SplitInternal(separator, count, StringSplitOptions.None);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000C13DA File Offset: 0x000C05DA
		public string[] Split([Nullable(2)] char[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(separator, int.MaxValue, options);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000C13EE File Offset: 0x000C05EE
		public string[] Split([Nullable(2)] char[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(separator, count, options);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000C1400 File Offset: 0x000C0600
		private unsafe string[] SplitInternal(ReadOnlySpan<char> separators, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
			}
			string.CheckStringSplitOptions(options);
			while (count > 1 && this.Length != 0)
			{
				if (separators.IsEmpty)
				{
					options &= ~StringSplitOptions.TrimEntries;
				}
				Span<int> initialSpan = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
				ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(initialSpan);
				this.MakeSeparatorList(separators, ref valueListBuilder);
				ReadOnlySpan<int> sepList = valueListBuilder.AsSpan();
				if (sepList.Length != 0)
				{
					string[] result = (options != StringSplitOptions.None) ? this.SplitWithPostProcessing(sepList, default(ReadOnlySpan<int>), 1, count, options) : this.SplitWithoutPostProcessing(sepList, default(ReadOnlySpan<int>), 1, count);
					valueListBuilder.Dispose();
					return result;
				}
				count = 1;
			}
			string text = this;
			if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None && count > 0)
			{
				text = text.Trim();
			}
			if ((options & StringSplitOptions.RemoveEmptyEntries) != StringSplitOptions.None && text.Length == 0)
			{
				count = 0;
			}
			if (count != 0)
			{
				return new string[]
				{
					text
				};
			}
			return Array.Empty<string>();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000C14EC File Offset: 0x000C06EC
		public string[] Split([Nullable(2)] string separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(separator ?? string.Empty, null, int.MaxValue, options);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000C1505 File Offset: 0x000C0705
		public string[] Split([Nullable(2)] string separator, int count, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(separator ?? string.Empty, null, count, options);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000C151A File Offset: 0x000C071A
		public string[] Split([Nullable(new byte[]
		{
			2,
			1
		})] string[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(null, separator, int.MaxValue, options);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000C152A File Offset: 0x000C072A
		public string[] Split([Nullable(new byte[]
		{
			2,
			1
		})] string[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(null, separator, count, options);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000C1538 File Offset: 0x000C0738
		private unsafe string[] SplitInternal(string separator, string[] separators, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NegativeCount);
			}
			string.CheckStringSplitOptions(options);
			bool flag = separator != null;
			if (!flag && (separators == null || separators.Length == 0))
			{
				return this.SplitInternal(default(ReadOnlySpan<char>), count, options);
			}
			while (count > 1 && this.Length != 0)
			{
				if (flag)
				{
					if (separator.Length != 0)
					{
						return this.SplitInternal(separator, count, options);
					}
					count = 1;
				}
				else
				{
					Span<int> initialSpan = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
					ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(initialSpan);
					initialSpan = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
					ValueListBuilder<int> valueListBuilder2 = new ValueListBuilder<int>(initialSpan);
					this.MakeSeparatorList(separators, ref valueListBuilder, ref valueListBuilder2);
					ReadOnlySpan<int> sepList = valueListBuilder.AsSpan();
					ReadOnlySpan<int> lengthList = valueListBuilder2.AsSpan();
					if (sepList.Length == 0)
					{
						return new string[]
						{
							this
						};
					}
					string[] result = (options != StringSplitOptions.None) ? this.SplitWithPostProcessing(sepList, lengthList, 0, count, options) : this.SplitWithoutPostProcessing(sepList, lengthList, 0, count);
					valueListBuilder.Dispose();
					valueListBuilder2.Dispose();
					return result;
				}
			}
			string text = this;
			if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None && count > 0)
			{
				text = text.Trim();
			}
			if ((options & StringSplitOptions.RemoveEmptyEntries) != StringSplitOptions.None && text.Length == 0)
			{
				count = 0;
			}
			if (count != 0)
			{
				return new string[]
				{
					text
				};
			}
			return Array.Empty<string>();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000C1684 File Offset: 0x000C0884
		private unsafe string[] SplitInternal(string separator, int count, StringSplitOptions options)
		{
			Span<int> initialSpan = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(initialSpan);
			this.MakeSeparatorList(separator, ref valueListBuilder);
			ReadOnlySpan<int> sepList = valueListBuilder.AsSpan();
			if (sepList.Length != 0)
			{
				string[] result = (options != StringSplitOptions.None) ? this.SplitWithPostProcessing(sepList, default(ReadOnlySpan<int>), separator.Length, count, options) : this.SplitWithoutPostProcessing(sepList, default(ReadOnlySpan<int>), separator.Length, count);
				valueListBuilder.Dispose();
				return result;
			}
			string text = this;
			if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None)
			{
				text = text.Trim();
			}
			if (text.Length != 0 || (options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.None)
			{
				return new string[]
				{
					text
				};
			}
			return Array.Empty<string>();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000C1738 File Offset: 0x000C0938
		private unsafe string[] SplitWithoutPostProcessing(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count)
		{
			int num = 0;
			int num2 = 0;
			count--;
			int num3 = (sepList.Length < count) ? sepList.Length : count;
			string[] array = new string[num3 + 1];
			int num4 = 0;
			while (num4 < num3 && num < this.Length)
			{
				array[num2++] = this.Substring(num, *sepList[num4] - num);
				num = *sepList[num4] + (lengthList.IsEmpty ? defaultLength : (*lengthList[num4]));
				num4++;
			}
			if (num < this.Length && num3 >= 0)
			{
				array[num2] = this.Substring(num);
			}
			else if (num2 == num3)
			{
				array[num2] = string.Empty;
			}
			return array;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000C17EC File Offset: 0x000C09EC
		private unsafe string[] SplitWithPostProcessing(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count, StringSplitOptions options)
		{
			int length = sepList.Length;
			int num = (length < count) ? (length + 1) : count;
			string[] array = new string[num];
			int num2 = 0;
			int num3 = 0;
			int i = 0;
			ReadOnlySpan<char> span;
			while (i < length)
			{
				span = this.AsSpan(num2, *sepList[i] - num2);
				if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None)
				{
					span = span.Trim();
				}
				if (!span.IsEmpty || (options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.None)
				{
					array[num3++] = span.ToString();
				}
				num2 = *sepList[i] + (lengthList.IsEmpty ? defaultLength : (*lengthList[i]));
				if (num3 == count - 1)
				{
					if ((options & StringSplitOptions.RemoveEmptyEntries) != StringSplitOptions.None)
					{
						while (++i < length)
						{
							span = this.AsSpan(num2, *sepList[i] - num2);
							if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None)
							{
								span = span.Trim();
							}
							if (!span.IsEmpty)
							{
								break;
							}
							num2 = *sepList[i] + (lengthList.IsEmpty ? defaultLength : (*lengthList[i]));
						}
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			span = this.AsSpan(num2);
			if ((options & StringSplitOptions.TrimEntries) != StringSplitOptions.None)
			{
				span = span.Trim();
			}
			if (!span.IsEmpty || (options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.None)
			{
				array[num3++] = span.ToString();
			}
			Array.Resize<string>(ref array, num3);
			return array;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000C1950 File Offset: 0x000C0B50
		private unsafe void MakeSeparatorList(ReadOnlySpan<char> separators, ref ValueListBuilder<int> sepListBuilder)
		{
			switch (separators.Length)
			{
			case 0:
				for (int i = 0; i < this.Length; i++)
				{
					if (char.IsWhiteSpace(this[i]))
					{
						sepListBuilder.Append(i);
					}
				}
				return;
			case 1:
			{
				char c = (char)(*separators[0]);
				for (int j = 0; j < this.Length; j++)
				{
					if (this[j] == c)
					{
						sepListBuilder.Append(j);
					}
				}
				return;
			}
			case 2:
			{
				char c = (char)(*separators[0]);
				char c2 = (char)(*separators[1]);
				for (int k = 0; k < this.Length; k++)
				{
					char c3 = this[k];
					if (c3 == c || c3 == c2)
					{
						sepListBuilder.Append(k);
					}
				}
				return;
			}
			case 3:
			{
				char c = (char)(*separators[0]);
				char c2 = (char)(*separators[1]);
				char c4 = (char)(*separators[2]);
				for (int l = 0; l < this.Length; l++)
				{
					char c5 = this[l];
					if (c5 == c || c5 == c2 || c5 == c4)
					{
						sepListBuilder.Append(l);
					}
				}
				return;
			}
			default:
			{
				string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
				uint* charMap = (uint*)(&probabilisticMap);
				string.InitializeProbabilisticMap(charMap, separators);
				for (int m = 0; m < this.Length; m++)
				{
					char c6 = this[m];
					if (string.IsCharBitSet(charMap, (byte)c6) && string.IsCharBitSet(charMap, (byte)(c6 >> 8)) && separators.Contains(c6))
					{
						sepListBuilder.Append(m);
					}
				}
				return;
			}
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000C1ADC File Offset: 0x000C0CDC
		private void MakeSeparatorList(string separator, ref ValueListBuilder<int> sepListBuilder)
		{
			int length = separator.Length;
			for (int i = 0; i < this.Length; i++)
			{
				if (this[i] == separator[0] && length <= this.Length - i && (length == 1 || this.AsSpan(i, length).SequenceEqual(separator)))
				{
					sepListBuilder.Append(i);
					i += length - 1;
				}
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000C1B44 File Offset: 0x000C0D44
		private void MakeSeparatorList(string[] separators, ref ValueListBuilder<int> sepListBuilder, ref ValueListBuilder<int> lengthListBuilder)
		{
			for (int i = 0; i < this.Length; i++)
			{
				foreach (string text in separators)
				{
					if (!string.IsNullOrEmpty(text))
					{
						int length = text.Length;
						if (this[i] == text[0] && length <= this.Length - i && (length == 1 || this.AsSpan(i, length).SequenceEqual(text)))
						{
							sepListBuilder.Append(i);
							lengthListBuilder.Append(length);
							i += length - 1;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000C1BCD File Offset: 0x000C0DCD
		private static void CheckStringSplitOptions(StringSplitOptions options)
		{
			if ((options & ~(StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)) != StringSplitOptions.None)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000C1BDE File Offset: 0x000C0DDE
		public string Substring(int startIndex)
		{
			return this.Substring(startIndex, this.Length - startIndex);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000C1BF0 File Offset: 0x000C0DF0
		public string Substring(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndexLargerThanLength);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_IndexLength);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (startIndex == 0 && length == this.Length)
			{
				return this;
			}
			return this.InternalSubString(startIndex, length);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000C1C78 File Offset: 0x000C0E78
		private string InternalSubString(int startIndex, int length)
		{
			string text = string.FastAllocateString(length);
			UIntPtr elementCount = (UIntPtr)text.Length;
			Buffer.Memmove<char>(ref text._firstChar, Unsafe.Add<char>(ref this._firstChar, startIndex), elementCount);
			return text;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000C1CAD File Offset: 0x000C0EAD
		public string ToLower()
		{
			return this.ToLower(null);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000C1CB8 File Offset: 0x000C0EB8
		public string ToLower([Nullable(2)] CultureInfo culture)
		{
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			return cultureInfo.TextInfo.ToLower(this);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000C1CDC File Offset: 0x000C0EDC
		public string ToLowerInvariant()
		{
			return TextInfo.Invariant.ToLower(this);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000C1CE9 File Offset: 0x000C0EE9
		public string ToUpper()
		{
			return this.ToUpper(null);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000C1CF4 File Offset: 0x000C0EF4
		public string ToUpper([Nullable(2)] CultureInfo culture)
		{
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			return cultureInfo.TextInfo.ToUpper(this);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000C1D18 File Offset: 0x000C0F18
		public string ToUpperInvariant()
		{
			return TextInfo.Invariant.ToUpper(this);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000C1D25 File Offset: 0x000C0F25
		public string Trim()
		{
			return this.TrimWhiteSpaceHelper(TrimType.Both);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000C1D2E File Offset: 0x000C0F2E
		public unsafe string Trim(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, TrimType.Both);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000C1D3C File Offset: 0x000C0F3C
		public unsafe string Trim([Nullable(2)] params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(TrimType.Both);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* trimChars2 = ptr;
				return this.TrimHelper(trimChars2, trimChars.Length, TrimType.Both);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000C1D6E File Offset: 0x000C0F6E
		public string TrimStart()
		{
			return this.TrimWhiteSpaceHelper(TrimType.Head);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000C1D77 File Offset: 0x000C0F77
		public unsafe string TrimStart(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, TrimType.Head);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000C1D84 File Offset: 0x000C0F84
		public unsafe string TrimStart([Nullable(2)] params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(TrimType.Head);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* trimChars2 = ptr;
				return this.TrimHelper(trimChars2, trimChars.Length, TrimType.Head);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000C1DB6 File Offset: 0x000C0FB6
		public string TrimEnd()
		{
			return this.TrimWhiteSpaceHelper(TrimType.Tail);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000C1DBF File Offset: 0x000C0FBF
		public unsafe string TrimEnd(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, TrimType.Tail);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000C1DCC File Offset: 0x000C0FCC
		public unsafe string TrimEnd([Nullable(2)] params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(TrimType.Tail);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* trimChars2 = ptr;
				return this.TrimHelper(trimChars2, trimChars.Length, TrimType.Tail);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000C1E00 File Offset: 0x000C1000
		private string TrimWhiteSpaceHelper(TrimType trimType)
		{
			int num = this.Length - 1;
			int num2 = 0;
			if ((trimType & TrimType.Head) != (TrimType)0)
			{
				num2 = 0;
				while (num2 < this.Length && char.IsWhiteSpace(this[num2]))
				{
					num2++;
				}
			}
			if ((trimType & TrimType.Tail) != (TrimType)0)
			{
				num = this.Length - 1;
				while (num >= num2 && char.IsWhiteSpace(this[num]))
				{
					num--;
				}
			}
			return this.CreateTrimmedString(num2, num);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000C1E6C File Offset: 0x000C106C
		private unsafe string TrimHelper(char* trimChars, int trimCharsLength, TrimType trimType)
		{
			int i = this.Length - 1;
			int j = 0;
			if ((trimType & TrimType.Head) != (TrimType)0)
			{
				for (j = 0; j < this.Length; j++)
				{
					char c = this[j];
					int num = 0;
					while (num < trimCharsLength && trimChars[num] != c)
					{
						num++;
					}
					if (num == trimCharsLength)
					{
						break;
					}
				}
			}
			if ((trimType & TrimType.Tail) != (TrimType)0)
			{
				for (i = this.Length - 1; i >= j; i--)
				{
					char c2 = this[i];
					int num2 = 0;
					while (num2 < trimCharsLength && trimChars[num2] != c2)
					{
						num2++;
					}
					if (num2 == trimCharsLength)
					{
						break;
					}
				}
			}
			return this.CreateTrimmedString(j, i);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000C1F0C File Offset: 0x000C110C
		private string CreateTrimmedString(int start, int end)
		{
			int num = end - start + 1;
			if (num == this.Length)
			{
				return this;
			}
			if (num != 0)
			{
				return this.InternalSubString(start, num);
			}
			return string.Empty;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000C1F3B File Offset: 0x000C113B
		public bool Contains(string value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return SpanHelpers.IndexOf(ref this._firstChar, this.Length, ref value._firstChar, value.Length) >= 0;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000C1F69 File Offset: 0x000C1169
		public bool Contains(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000C1F79 File Offset: 0x000C1179
		public bool Contains(char value)
		{
			return SpanHelpers.Contains(ref this._firstChar, value, this.Length);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000C1F8D File Offset: 0x000C118D
		public bool Contains(char value, StringComparison comparisonType)
		{
			return this.IndexOf(value, comparisonType) != -1;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000C1F9D File Offset: 0x000C119D
		public int IndexOf(char value)
		{
			return SpanHelpers.IndexOf(ref this._firstChar, value, this.Length);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000C1FB1 File Offset: 0x000C11B1
		public int IndexOf(char value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000C1FC4 File Offset: 0x000C11C4
		public int IndexOf(char value, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return this.IndexOf(value);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
				throw new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000C2048 File Offset: 0x000C1248
		public int IndexOf(char value, int startIndex, int count)
		{
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
			}
			int num = SpanHelpers.IndexOf(Unsafe.Add<char>(ref this._firstChar, startIndex), value, count);
			if (num != -1)
			{
				return num + startIndex;
			}
			return num;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000C20A6 File Offset: 0x000C12A6
		public int IndexOfAny(char[] anyOf)
		{
			return this.IndexOfAny(anyOf, 0, this.Length);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000C20B6 File Offset: 0x000C12B6
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000C20C8 File Offset: 0x000C12C8
		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException("anyOf");
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
			}
			if (anyOf.Length != 0 && anyOf.Length <= 5)
			{
				int num = new ReadOnlySpan<char>(Unsafe.Add<char>(ref this._firstChar, startIndex), count).IndexOfAny(anyOf);
				if (num != -1)
				{
					return num + startIndex;
				}
				return num;
			}
			else
			{
				if (anyOf.Length > 5)
				{
					return this.IndexOfCharArray(anyOf, startIndex, count);
				}
				return -1;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000C215C File Offset: 0x000C135C
		private unsafe int IndexOfCharArray(char[] anyOf, int startIndex, int count)
		{
			string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
			uint* charMap = (uint*)(&probabilisticMap);
			string.InitializeProbabilisticMap(charMap, anyOf);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				while (count > 0)
				{
					int num = (int)(*ptr3);
					if (string.IsCharBitSet(charMap, (byte)num) && string.IsCharBitSet(charMap, (byte)(num >> 8)) && string.ArrayContains((char)num, anyOf))
					{
						return (int)((long)(ptr3 - ptr2));
					}
					count--;
					ptr3++;
				}
				return -1;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000C21D8 File Offset: 0x000C13D8
		private unsafe static void InitializeProbabilisticMap(uint* charMap, ReadOnlySpan<char> anyOf)
		{
			bool flag = false;
			for (int i = 0; i < anyOf.Length; i++)
			{
				int num = (int)(*anyOf[i]);
				string.SetCharBit(charMap, (byte)num);
				num >>= 8;
				if (num == 0)
				{
					flag = true;
				}
				else
				{
					string.SetCharBit(charMap, (byte)num);
				}
			}
			if (flag)
			{
				*charMap |= 1U;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000C222C File Offset: 0x000C142C
		private static bool ArrayContains(char searchChar, char[] anyOf)
		{
			for (int i = 0; i < anyOf.Length; i++)
			{
				if (anyOf[i] == searchChar)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000C2250 File Offset: 0x000C1450
		private unsafe static bool IsCharBitSet(uint* charMap, byte value)
		{
			return (charMap[value & 7] & 1U << (value >> 3)) > 0U;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000C2267 File Offset: 0x000C1467
		private unsafe static void SetCharBit(uint* charMap, byte value)
		{
			charMap[value & 7] |= 1U << (value >> 3);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000C227D File Offset: 0x000C147D
		public int IndexOf(string value)
		{
			return this.IndexOf(value, StringComparison.CurrentCulture);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000C2287 File Offset: 0x000C1487
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000C2292 File Offset: 0x000C1492
		public int IndexOf(string value, int startIndex, int count)
		{
			return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000C229E File Offset: 0x000C149E
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000C22AF File Offset: 0x000C14AF
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000C22C4 File Offset: 0x000C14C4
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, startIndex, count, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
			case StringComparison.OrdinalIgnoreCase:
				return Ordinal.IndexOf(this, value, startIndex, count, comparisonType == StringComparison.OrdinalIgnoreCase);
			default:
				throw (value == null) ? new ArgumentNullException("value") : new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000C2350 File Offset: 0x000C1550
		public int LastIndexOf(char value)
		{
			return SpanHelpers.LastIndexOf(ref this._firstChar, value, this.Length);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000C2364 File Offset: 0x000C1564
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000C2374 File Offset: 0x000C1574
		public int LastIndexOf(char value, int startIndex, int count)
		{
			if (this.Length == 0)
			{
				return -1;
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
			}
			int num = startIndex + 1 - count;
			int num2 = SpanHelpers.LastIndexOf(Unsafe.Add<char>(ref this._firstChar, num), value, count);
			if (num2 != -1)
			{
				return num2 + num;
			}
			return num2;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000C23DD File Offset: 0x000C15DD
		public int LastIndexOfAny(char[] anyOf)
		{
			return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000C23F4 File Offset: 0x000C15F4
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000C2404 File Offset: 0x000C1604
		public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException("anyOf");
			}
			if (this.Length == 0)
			{
				return -1;
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0 || count - 1 > startIndex)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Count);
			}
			if (anyOf.Length > 1)
			{
				return this.LastIndexOfCharArray(anyOf, startIndex, count);
			}
			if (anyOf.Length == 1)
			{
				return this.LastIndexOf(anyOf[0], startIndex, count);
			}
			return -1;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000C2480 File Offset: 0x000C1680
		private unsafe int LastIndexOfCharArray(char[] anyOf, int startIndex, int count)
		{
			string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
			uint* charMap = (uint*)(&probabilisticMap);
			string.InitializeProbabilisticMap(charMap, anyOf);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				while (count > 0)
				{
					int num = (int)(*ptr3);
					if (string.IsCharBitSet(charMap, (byte)num) && string.IsCharBitSet(charMap, (byte)(num >> 8)) && string.ArrayContains((char)num, anyOf))
					{
						return (int)((long)(ptr3 - ptr2));
					}
					count--;
					ptr3--;
				}
				return -1;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000C24FA File Offset: 0x000C16FA
		public int LastIndexOf(string value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000C2512 File Offset: 0x000C1712
		public int LastIndexOf(string value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.CurrentCulture);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000C2520 File Offset: 0x000C1720
		public int LastIndexOf(string value, int startIndex, int count)
		{
			return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000C252C File Offset: 0x000C172C
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000C2544 File Offset: 0x000C1744
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000C2554 File Offset: 0x000C1754
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.LastIndexOf(this, value, startIndex, count, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.Invariant.LastIndexOf(this, value, startIndex, count, string.GetCompareOptionsFromOrdinalStringComparison(comparisonType));
			default:
				throw (value == null) ? new ArgumentNullException("value") : new ArgumentException(SR.NotSupported_StringComparison, "comparisonType");
			}
		}

		// Token: 0x04000211 RID: 529
		[Intrinsic]
		public static readonly string Empty;

		// Token: 0x04000212 RID: 530
		[NonSerialized]
		private readonly int _stringLength;

		// Token: 0x04000213 RID: 531
		[NonSerialized]
		private char _firstChar;

		// Token: 0x02000098 RID: 152
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		private struct ProbabilisticMap
		{
		}
	}
}
