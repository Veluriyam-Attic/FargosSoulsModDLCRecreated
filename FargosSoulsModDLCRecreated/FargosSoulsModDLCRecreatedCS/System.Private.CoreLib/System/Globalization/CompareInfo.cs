using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020001E8 RID: 488
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	public sealed class CompareInfo : IDeserializationCallback
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x00120073 File Offset: 0x0011F273
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture._name;
			this.InitSort(culture);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00120090 File Offset: 0x0011F290
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(SR.Argument_OnlyMscorlib, "assembly");
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x001200E4 File Offset: 0x0011F2E4
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(SR.Argument_OnlyMscorlib, "assembly");
			}
			return CompareInfo.GetCompareInfo(name);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00120145 File Offset: 0x0011F345
		public static CompareInfo GetCompareInfo(int culture)
		{
			if (CultureData.IsCustomCultureId(culture))
			{
				throw new ArgumentException(SR.Argument_CustomCultureCannotBePassedByNumber, "culture");
			}
			return CultureInfo.GetCultureInfo(culture).CompareInfo;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0012016A File Offset: 0x0011F36A
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00120185 File Offset: 0x0011F385
		public static bool IsSortable(char ch)
		{
			return CompareInfo.IsSortable(MemoryMarshal.CreateReadOnlySpan<char>(ref ch, 1));
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00120194 File Offset: 0x0011F394
		public static bool IsSortable(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return CompareInfo.IsSortable(text.AsSpan());
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x001201AF File Offset: 0x0011F3AF
		[NullableContext(0)]
		public static bool IsSortable(ReadOnlySpan<char> text)
		{
			if (text.Length == 0)
			{
				return false;
			}
			if (GlobalizationMode.Invariant)
			{
				return true;
			}
			if (!GlobalizationMode.UseNls)
			{
				return CompareInfo.IcuIsSortable(text);
			}
			return CompareInfo.NlsIsSortable(text);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x001201DC File Offset: 0x0011F3DC
		public unsafe static bool IsSortable(Rune value)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
			Span<char> destination = span;
			int length = value.EncodeToUtf16(destination);
			return CompareInfo.IsSortable(destination.Slice(0, length));
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00120212 File Offset: 0x0011F412
		[MemberNotNull("_sortName")]
		private void InitSort(CultureInfo culture)
		{
			this._sortName = culture.SortName;
			if (GlobalizationMode.UseNls)
			{
				this.NlsInitSortHandle();
				return;
			}
			this.IcuInitSortHandle();
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00120234 File Offset: 0x0011F434
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0012023D File Offset: 0x0011F43D
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0012023D File Offset: 0x0011F43D
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00120245 File Offset: 0x0011F445
		private void OnDeserialized()
		{
			if (this.m_name == null)
			{
				this.m_name = CultureInfo.GetCultureInfo(this.culture)._name;
				return;
			}
			this.InitSort(CultureInfo.GetCultureInfo(this.m_name));
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00120277 File Offset: 0x0011F477
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0012028F File Offset: 0x0011F48F
		public string Name
		{
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this._sortName;
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x001202C2 File Offset: 0x0011F4C2
		[NullableContext(2)]
		public int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x001202D0 File Offset: 0x0011F4D0
		[NullableContext(2)]
		public int Compare(string string1, string string2, CompareOptions options)
		{
			int result;
			if (string1 == null)
			{
				result = ((string2 == null) ? 0 : -1);
			}
			else
			{
				if (string2 != null)
				{
					return this.Compare(string1.AsSpan(), string2.AsSpan(), options);
				}
				result = 1;
			}
			CompareInfo.CheckCompareOptionsForCompare(options);
			return result;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0012030C File Offset: 0x0011F50C
		internal int CompareOptionIgnoreCase(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareStringCore(string1, string2, CompareOptions.IgnoreCase);
			}
			return Ordinal.CompareIgnoreCaseInvariantMode(MemoryMarshal.GetReference<char>(string1), string1.Length, MemoryMarshal.GetReference<char>(string2), string2.Length);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0012033E File Offset: 0x0011F53E
		[NullableContext(2)]
		public int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00120350 File Offset: 0x0011F550
		[NullableContext(2)]
		public int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			return this.Compare(string1, offset1, (string1 == null) ? 0 : (string1.Length - offset1), string2, offset2, (string2 == null) ? 0 : (string2.Length - offset2), options);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0012037C File Offset: 0x0011F57C
		[NullableContext(2)]
		public int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0012038C File Offset: 0x0011F58C
		[NullableContext(2)]
		public int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			ReadOnlySpan<char> string3 = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> string4 = default(ReadOnlySpan<char>);
			if (string1 == null)
			{
				if (offset1 != 0)
				{
					goto IL_6E;
				}
				if (length1 != 0)
				{
					goto IL_6E;
				}
			}
			else if (!string1.TryGetSpan(offset1, length1, out string3))
			{
				goto IL_6E;
			}
			if (string2 == null)
			{
				if (offset2 != 0)
				{
					goto IL_6E;
				}
				if (length2 != 0)
				{
					goto IL_6E;
				}
			}
			else if (!string2.TryGetSpan(offset2, length2, out string4))
			{
				goto IL_6E;
			}
			int result;
			if (string1 == null)
			{
				result = ((string2 == null) ? 0 : -1);
			}
			else
			{
				if (string2 != null)
				{
					return this.Compare(string3, string4, options);
				}
				result = 1;
			}
			CompareInfo.CheckCompareOptionsForCompare(options);
			return result;
			IL_6E:
			if (length1 < 0 || length2 < 0)
			{
				throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (offset1 < 0 || offset2 < 0)
			{
				throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
			{
				throw new ArgumentOutOfRangeException("string1", SR.ArgumentOutOfRange_OffsetLength);
			}
			throw new ArgumentOutOfRangeException("string2", SR.ArgumentOutOfRange_OffsetLength);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00120480 File Offset: 0x0011F680
		[NullableContext(0)]
		public int Compare(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options = CompareOptions.None)
		{
			if (string1 == string2)
			{
				CompareInfo.CheckCompareOptionsForCompare(options);
				return 0;
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					return this.CompareStringCore(string1, string2, options);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return string1.SequenceCompareTo(string2);
				}
				return Ordinal.CompareStringIgnoreCase(MemoryMarshal.GetReference<char>(string1), string1.Length, MemoryMarshal.GetReference<char>(string2), string2.Length);
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return string1.SequenceCompareTo(string2);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return Ordinal.CompareStringIgnoreCase(MemoryMarshal.GetReference<char>(string1), string1.Length, MemoryMarshal.GetReference<char>(string2), string2.Length);
				}
				CompareInfo.ThrowCompareOptionsCheckFailed(options);
				return -1;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00120524 File Offset: 0x0011F724
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void CheckCompareOptionsForCompare(CompareOptions options)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				CompareInfo.ThrowCompareOptionsCheckFailed(options);
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00120545 File Offset: 0x0011F745
		[DoesNotReturn]
		[StackTraceHidden]
		private static void ThrowCompareOptionsCheckFailed(CompareOptions options)
		{
			throw new ArgumentException(((options & CompareOptions.Ordinal) != CompareOptions.None) ? SR.Argument_CompareOptionOrdinal : SR.Argument_InvalidFlag, "options");
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00120566 File Offset: 0x0011F766
		private int CompareStringCore(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuCompareString(string1, string2, options);
			}
			return this.NlsCompareString(string1, string2, options);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00120582 File Offset: 0x0011F782
		public bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (prefix == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.prefix);
			}
			return this.IsPrefix(source.AsSpan(), prefix.AsSpan(), options);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x001205AC File Offset: 0x0011F7AC
		[NullableContext(0)]
		public bool IsPrefix(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options = CompareOptions.None)
		{
			if (prefix.IsEmpty)
			{
				return true;
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					return this.StartsWithCore(source, prefix, options, null);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return source.StartsWith(prefix);
				}
				return source.StartsWithOrdinalIgnoreCase(prefix);
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return source.StartsWith(prefix);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.StartsWithOrdinalIgnoreCase(prefix);
				}
				CompareInfo.ThrowCompareOptionsCheckFailed(options);
				return false;
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0012061C File Offset: 0x0011F81C
		[NullableContext(0)]
		public unsafe bool IsPrefix(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, out int matchLength)
		{
			bool flag;
			if (GlobalizationMode.Invariant || prefix.IsEmpty || (options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				flag = this.IsPrefix(source, prefix, options);
				matchLength = (flag ? prefix.Length : 0);
			}
			else
			{
				int num = 0;
				flag = this.StartsWithCore(source, prefix, options, &num);
				matchLength = num;
			}
			return flag;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0012066F File Offset: 0x0011F86F
		private unsafe bool StartsWithCore(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, int* matchLengthPtr)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuStartsWith(source, prefix, options, matchLengthPtr);
			}
			return this.NlsStartsWith(source, prefix, options, matchLengthPtr);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0012068F File Offset: 0x0011F88F
		public bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0012069A File Offset: 0x0011F89A
		public bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (suffix == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.suffix);
			}
			return this.IsSuffix(source.AsSpan(), suffix.AsSpan(), options);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x001206C4 File Offset: 0x0011F8C4
		[NullableContext(0)]
		public bool IsSuffix(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options = CompareOptions.None)
		{
			if (suffix.IsEmpty)
			{
				return true;
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					return this.EndsWithCore(source, suffix, options, null);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return source.EndsWith(suffix);
				}
				return source.EndsWithOrdinalIgnoreCase(suffix);
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return source.EndsWith(suffix);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.EndsWithOrdinalIgnoreCase(suffix);
				}
				CompareInfo.ThrowCompareOptionsCheckFailed(options);
				return false;
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00120734 File Offset: 0x0011F934
		[NullableContext(0)]
		public unsafe bool IsSuffix(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, out int matchLength)
		{
			bool flag;
			if (GlobalizationMode.Invariant || suffix.IsEmpty || (options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				flag = this.IsSuffix(source, suffix, options);
				matchLength = (flag ? suffix.Length : 0);
			}
			else
			{
				int num = 0;
				flag = this.EndsWithCore(source, suffix, options, &num);
				matchLength = num;
			}
			return flag;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00120787 File Offset: 0x0011F987
		public bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00120792 File Offset: 0x0011F992
		private unsafe bool EndsWithCore(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, int* matchLengthPtr)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuEndsWith(source, suffix, options, matchLengthPtr);
			}
			return this.NlsEndsWith(source, suffix, options, matchLengthPtr);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x001207B2 File Offset: 0x0011F9B2
		public int IndexOf(string source, char value)
		{
			return this.IndexOf(source, value, CompareOptions.None);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x001207BD File Offset: 0x0011F9BD
		public int IndexOf(string source, string value)
		{
			return this.IndexOf(source, value, CompareOptions.None);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x001207C8 File Offset: 0x0011F9C8
		public int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return this.IndexOf(source, MemoryMarshal.CreateReadOnlySpan<char>(ref value, 1), options);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x001207E9 File Offset: 0x0011F9E9
		public int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return this.IndexOf(source.AsSpan(), value.AsSpan(), options);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00120811 File Offset: 0x0011FA11
		public int IndexOf(string source, char value, int startIndex)
		{
			return this.IndexOf(source, value, startIndex, CompareOptions.None);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0012081D File Offset: 0x0011FA1D
		public int IndexOf(string source, string value, int startIndex)
		{
			return this.IndexOf(source, value, startIndex, CompareOptions.None);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00120829 File Offset: 0x0011FA29
		public int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00120848 File Offset: 0x0011FA48
		public int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00120867 File Offset: 0x0011FA67
		public int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00120875 File Offset: 0x0011FA75
		public int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00120884 File Offset: 0x0011FA84
		public int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			ReadOnlySpan<char> source2;
			if (!source.TryGetSpan(startIndex, count, out source2))
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
			int num = this.IndexOf(source2, MemoryMarshal.CreateReadOnlySpan<char>(ref value, 1), options);
			if (num >= 0)
			{
				num += startIndex;
			}
			return num;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x001208E0 File Offset: 0x0011FAE0
		public int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			ReadOnlySpan<char> source2;
			if (!source.TryGetSpan(startIndex, count, out source2))
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
			int num = this.IndexOf(source2, value, options);
			if (num >= 0)
			{
				num += startIndex;
			}
			return num;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00120940 File Offset: 0x0011FB40
		[NullableContext(0)]
		public int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options = CompareOptions.None)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					if (value.IsEmpty)
					{
						return 0;
					}
					return this.IndexOfCore(source, value, options, null, true);
				}
				else
				{
					if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
					{
						return source.IndexOf(value);
					}
					return Ordinal.IndexOfOrdinalIgnoreCase(source, value);
				}
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return source.IndexOf(value);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return Ordinal.IndexOfOrdinalIgnoreCase(source, value);
				}
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
				return -1;
			}
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x001209B4 File Offset: 0x0011FBB4
		[NullableContext(0)]
		public unsafe int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options, out int matchLength)
		{
			int num;
			int result = this.IndexOf(source, value, &num, options, true);
			matchLength = num;
			return result;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x001209D4 File Offset: 0x0011FBD4
		[NullableContext(0)]
		public unsafe int IndexOf(ReadOnlySpan<char> source, Rune value, CompareOptions options = CompareOptions.None)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
			Span<char> destination = span;
			int length = value.EncodeToUtf16(destination);
			return this.IndexOf(source, destination.Slice(0, length), options);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00120A10 File Offset: 0x0011FC10
		private unsafe int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, int* matchLengthPtr, CompareOptions options, bool fromBeginning)
		{
			*matchLengthPtr = 0;
			int num = 0;
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					if (!value.IsEmpty)
					{
						return this.IndexOfCore(source, value, options, matchLengthPtr, fromBeginning);
					}
					if (!fromBeginning)
					{
						return source.Length;
					}
					return 0;
				}
				else if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					num = (fromBeginning ? source.IndexOf(value) : source.LastIndexOf(value));
				}
				else
				{
					num = (fromBeginning ? Ordinal.IndexOfOrdinalIgnoreCase(source, value) : Ordinal.LastIndexOfOrdinalIgnoreCase(source, value));
				}
			}
			else if (options == CompareOptions.Ordinal)
			{
				num = (fromBeginning ? source.IndexOf(value) : source.LastIndexOf(value));
			}
			else if (options == CompareOptions.OrdinalIgnoreCase)
			{
				num = (fromBeginning ? Ordinal.IndexOfOrdinalIgnoreCase(source, value) : Ordinal.LastIndexOfOrdinalIgnoreCase(source, value));
			}
			else
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
			}
			if (num >= 0)
			{
				*matchLengthPtr = value.Length;
			}
			return num;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00120AE0 File Offset: 0x0011FCE0
		private unsafe int IndexOfCore(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr, bool fromBeginning)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuIndexOfCore(source, target, options, matchLengthPtr, fromBeginning);
			}
			return this.NlsIndexOfCore(source, target, options, matchLengthPtr, fromBeginning);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00120B04 File Offset: 0x0011FD04
		public int LastIndexOf(string source, char value)
		{
			return this.LastIndexOf(source, value, CompareOptions.None);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00120B0F File Offset: 0x0011FD0F
		public int LastIndexOf(string source, string value)
		{
			return this.LastIndexOf(source, value, CompareOptions.None);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00120B1A File Offset: 0x0011FD1A
		public int LastIndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return this.LastIndexOf(source, MemoryMarshal.CreateReadOnlySpan<char>(ref value, 1), options);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00120B3B File Offset: 0x0011FD3B
		public int LastIndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return this.LastIndexOf(source.AsSpan(), value.AsSpan(), options);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00120B63 File Offset: 0x0011FD63
		public int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00120B72 File Offset: 0x0011FD72
		public int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00120B81 File Offset: 0x0011FD81
		public int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00120B91 File Offset: 0x0011FD91
		public int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00120BA1 File Offset: 0x0011FDA1
		public int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00120BAF File Offset: 0x0011FDAF
		public int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00120BC0 File Offset: 0x0011FDC0
		public int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			while (startIndex >= source.Length)
			{
				if (startIndex == -1 && source.Length == 0)
				{
					count = 0;
					break;
				}
				if (startIndex != source.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
					break;
				}
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			startIndex = startIndex - count + 1;
			ReadOnlySpan<char> source2;
			if (!source.TryGetSpan(startIndex, count, out source2))
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = this.LastIndexOf(source2, MemoryMarshal.CreateReadOnlySpan<char>(ref value, 1), options);
			if (num >= 0)
			{
				num += startIndex;
			}
			return num;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00120C48 File Offset: 0x0011FE48
		public int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			while (startIndex >= source.Length)
			{
				if (startIndex == -1 && source.Length == 0)
				{
					count = 0;
					break;
				}
				if (startIndex != source.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
					break;
				}
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			startIndex = startIndex - count + 1;
			ReadOnlySpan<char> source2;
			if (!source.TryGetSpan(startIndex, count, out source2))
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = this.LastIndexOf(source2, value, options);
			if (num >= 0)
			{
				num += startIndex;
			}
			return num;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00120CD8 File Offset: 0x0011FED8
		[NullableContext(0)]
		public int LastIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options = CompareOptions.None)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					if (value.IsEmpty)
					{
						return source.Length;
					}
					return this.IndexOfCore(source, value, options, null, false);
				}
				else
				{
					if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
					{
						return source.LastIndexOf(value);
					}
					return Ordinal.LastIndexOfOrdinalIgnoreCase(source, value);
				}
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return source.LastIndexOf(value);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return Ordinal.LastIndexOfOrdinalIgnoreCase(source, value);
				}
				throw new ArgumentException(SR.Argument_InvalidFlag, "options");
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00120D54 File Offset: 0x0011FF54
		[NullableContext(0)]
		public unsafe int LastIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options, out int matchLength)
		{
			int num;
			int result = this.IndexOf(source, value, &num, options, false);
			matchLength = num;
			return result;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00120D74 File Offset: 0x0011FF74
		[NullableContext(0)]
		public unsafe int LastIndexOf(ReadOnlySpan<char> source, Rune value, CompareOptions options = CompareOptions.None)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
			Span<char> destination = span;
			int length = value.EncodeToUtf16(destination);
			return this.LastIndexOf(source, destination.Slice(0, length), options);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00120DAD File Offset: 0x0011FFAD
		public SortKey GetSortKey(string source, CompareOptions options)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, options);
			}
			return this.CreateSortKeyCore(source, options);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00120DC7 File Offset: 0x0011FFC7
		public SortKey GetSortKey(string source)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, CompareOptions.None);
			}
			return this.CreateSortKeyCore(source, CompareOptions.None);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00120DE1 File Offset: 0x0011FFE1
		private SortKey CreateSortKeyCore(string source, CompareOptions options)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuCreateSortKey(source, options);
			}
			return this.NlsCreateSortKey(source, options);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00120DFB File Offset: 0x0011FFFB
		[NullableContext(0)]
		public int GetSortKey(ReadOnlySpan<char> source, Span<byte> destination, CompareOptions options = CompareOptions.None)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
			}
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantGetSortKey(source, destination, options);
			}
			return this.GetSortKeyCore(source, destination, options);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00120E29 File Offset: 0x00120029
		private int GetSortKeyCore(ReadOnlySpan<char> source, Span<byte> destination, CompareOptions options)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetSortKey(source, destination, options);
			}
			return this.NlsGetSortKey(source, destination, options);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00120E45 File Offset: 0x00120045
		[NullableContext(0)]
		public int GetSortKeyLength(ReadOnlySpan<char> source, CompareOptions options = CompareOptions.None)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
			}
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantGetSortKeyLength(source, options);
			}
			return this.GetSortKeyLengthCore(source, options);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00120E71 File Offset: 0x00120071
		private int GetSortKeyLengthCore(ReadOnlySpan<char> source, CompareOptions options)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetSortKeyLength(source, options);
			}
			return this.NlsGetSortKeyLength(source, options);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00120E8C File Offset: 0x0012008C
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00120EB6 File Offset: 0x001200B6
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00120EC3 File Offset: 0x001200C3
		public int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			return this.GetHashCode(source.AsSpan(), options);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00120EDC File Offset: 0x001200DC
		[NullableContext(0)]
		public int GetHashCode(ReadOnlySpan<char> source, CompareOptions options)
		{
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) == CompareOptions.None)
			{
				if (!GlobalizationMode.Invariant)
				{
					return this.GetHashCodeOfStringCore(source, options);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return string.GetHashCode(source);
				}
				return string.GetHashCodeOrdinalIgnoreCase(source);
			}
			else
			{
				if (options == CompareOptions.Ordinal)
				{
					return string.GetHashCode(source);
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return string.GetHashCodeOrdinalIgnoreCase(source);
				}
				CompareInfo.ThrowCompareOptionsCheckFailed(options);
				return -1;
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00120F3A File Offset: 0x0012013A
		private int GetHashCodeOfStringCore(ReadOnlySpan<char> source, CompareOptions options)
		{
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetHashCodeOfString(source, options);
			}
			return this.NlsGetHashCodeOfString(source, options);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00120F54 File Offset: 0x00120154
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00120F68 File Offset: 0x00120168
		public SortVersion Version
		{
			get
			{
				if (this.m_SortVersion == null)
				{
					if (GlobalizationMode.Invariant)
					{
						this.m_SortVersion = new SortVersion(0, 127, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127));
					}
					else
					{
						this.m_SortVersion = (GlobalizationMode.UseNls ? this.NlsGetSortVersion() : this.IcuGetSortVersion());
					}
				}
				return this.m_SortVersion;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00120FCC File Offset: 0x001201CC
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.Name).LCID;
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00120FE0 File Offset: 0x001201E0
		private void IcuInitSortHandle()
		{
			if (GlobalizationMode.Invariant)
			{
				this._isAsciiEqualityOrdinal = true;
				return;
			}
			this._isAsciiEqualityOrdinal = (this._sortName.Length == 0 || (this._sortName.Length >= 2 && this._sortName[0] == 'e' && this._sortName[1] == 'n' && (this._sortName.Length == 2 || this._sortName[2] == '-')));
			this._sortHandle = CompareInfo.SortHandleCache.GetCachedSortHandle(this._sortName);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00121078 File Offset: 0x00120278
		private unsafe int IcuCompareString(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(string1))
			{
				char* lpStr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(string2))
				{
					char* lpStr2 = reference2;
					return Interop.Globalization.CompareString(this._sortHandle, lpStr, string1.Length, lpStr2, string2.Length, options);
				}
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x001210B8 File Offset: 0x001202B8
		private unsafe int IcuIndexOfCore(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr, bool fromBeginning)
		{
			if (this._isAsciiEqualityOrdinal && CompareInfo.CanUseAsciiOrdinalForOptions(options))
			{
				if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
				{
					return this.IndexOfOrdinalIgnoreCaseHelper(source, target, options, matchLengthPtr, fromBeginning);
				}
				return this.IndexOfOrdinalHelper(source, target, options, matchLengthPtr, fromBeginning);
			}
			else
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(source))
				{
					char* pSource = reference;
					fixed (char* reference2 = MemoryMarshal.GetReference<char>(target))
					{
						char* target2 = reference2;
						if (fromBeginning)
						{
							return Interop.Globalization.IndexOf(this._sortHandle, target2, target.Length, pSource, source.Length, options, matchLengthPtr);
						}
						return Interop.Globalization.LastIndexOf(this._sortHandle, target2, target.Length, pSource, source.Length, options, matchLengthPtr);
					}
				}
			}
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0012114C File Offset: 0x0012034C
		private unsafe int IndexOfOrdinalIgnoreCaseHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr, bool fromBeginning)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(target))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					int i = 0;
					while (i < target.Length)
					{
						char c = ptr4[i];
						if (c < '\u0080' && *CompareInfo.HighCharTable[(int)c] == 0)
						{
							i++;
						}
						else
						{
							IL_1DE:
							if (fromBeginning)
							{
								return Interop.Globalization.IndexOf(this._sortHandle, ptr4, target.Length, ptr3, source.Length, options, matchLengthPtr);
							}
							return Interop.Globalization.LastIndexOf(this._sortHandle, ptr4, target.Length, ptr3, source.Length, options, matchLengthPtr);
						}
					}
					if (target.Length > source.Length)
					{
						for (int j = 0; j < source.Length; j++)
						{
							char c2 = ptr3[j];
							if (c2 >= '\u0080' || *CompareInfo.HighCharTable[(int)c2] != 0)
							{
								goto IL_1DE;
							}
						}
						return -1;
					}
					int num;
					int num2;
					int num3;
					if (fromBeginning)
					{
						num = 0;
						num2 = source.Length - target.Length + 1;
						num3 = 1;
					}
					else
					{
						num = source.Length - target.Length;
						num2 = -1;
						num3 = -1;
					}
					int num4 = num;
					IL_1D3:
					while (num4 != num2)
					{
						int k = 0;
						int num5 = num4;
						while (k < target.Length)
						{
							char c3 = ptr3[num5];
							char c4 = ptr4[k];
							if (c3 >= '\u0080' || *CompareInfo.HighCharTable[(int)c3] != 0)
							{
								goto IL_1DE;
							}
							if (c3 != c4)
							{
								if (c3 - 'a' <= '\u0019')
								{
									c3 -= ' ';
								}
								if (c4 - 'a' <= '\u0019')
								{
									c4 -= ' ';
								}
								if (c3 != c4)
								{
									if (num5 < source.Length - 1 && (ptr3 + num5)[1] >= '\u0080')
									{
										goto IL_1DE;
									}
									num4 += num3;
									goto IL_1D3;
								}
							}
							k++;
							num5++;
						}
						if (num5 >= source.Length || ptr3[num5] < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = target.Length;
							}
							return num4;
						}
						goto IL_1DE;
					}
					return -1;
				}
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0012137C File Offset: 0x0012057C
		private unsafe int IndexOfOrdinalHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr, bool fromBeginning)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(target))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					int i = 0;
					while (i < target.Length)
					{
						char c = ptr4[i];
						if (c < '\u0080' && *CompareInfo.HighCharTable[(int)c] == 0)
						{
							i++;
						}
						else
						{
							IL_1AD:
							if (fromBeginning)
							{
								return Interop.Globalization.IndexOf(this._sortHandle, ptr4, target.Length, ptr3, source.Length, options, matchLengthPtr);
							}
							return Interop.Globalization.LastIndexOf(this._sortHandle, ptr4, target.Length, ptr3, source.Length, options, matchLengthPtr);
						}
					}
					if (target.Length > source.Length)
					{
						for (int j = 0; j < source.Length; j++)
						{
							char c2 = ptr3[j];
							if (c2 >= '\u0080' || *CompareInfo.HighCharTable[(int)c2] != 0)
							{
								goto IL_1AD;
							}
						}
						return -1;
					}
					int num;
					int num2;
					int num3;
					if (fromBeginning)
					{
						num = 0;
						num2 = source.Length - target.Length + 1;
						num3 = 1;
					}
					else
					{
						num = source.Length - target.Length;
						num2 = -1;
						num3 = -1;
					}
					int num4 = num;
					IL_1A2:
					while (num4 != num2)
					{
						int k = 0;
						int num5 = num4;
						while (k < target.Length)
						{
							char c3 = ptr3[num5];
							char c4 = ptr4[k];
							if (c3 >= '\u0080' || *CompareInfo.HighCharTable[(int)c3] != 0)
							{
								goto IL_1AD;
							}
							if (c3 != c4)
							{
								if (num5 < source.Length - 1 && (ptr3 + num5)[1] >= '\u0080')
								{
									goto IL_1AD;
								}
								num4 += num3;
								goto IL_1A2;
							}
							else
							{
								k++;
								num5++;
							}
						}
						if (num5 >= source.Length || ptr3[num5] < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = target.Length;
							}
							return num4;
						}
						goto IL_1AD;
					}
					return -1;
				}
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0012157C File Offset: 0x0012077C
		private unsafe bool IcuStartsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, int* matchLengthPtr)
		{
			if (!this._isAsciiEqualityOrdinal || !CompareInfo.CanUseAsciiOrdinalForOptions(options))
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(source))
				{
					char* source2 = reference;
					fixed (char* reference2 = MemoryMarshal.GetReference<char>(prefix))
					{
						char* target = reference2;
						return Interop.Globalization.StartsWith(this._sortHandle, target, prefix.Length, source2, source.Length, options, matchLengthPtr);
					}
				}
			}
			if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
			{
				return this.StartsWithOrdinalIgnoreCaseHelper(source, prefix, options, matchLengthPtr);
			}
			return this.StartsWithOrdinalHelper(source, prefix, options, matchLengthPtr);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x001215E8 File Offset: 0x001207E8
		private unsafe bool StartsWithOrdinalIgnoreCaseHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, int* matchLengthPtr)
		{
			int num = Math.Min(source.Length, prefix.Length);
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(prefix))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					while (num != 0)
					{
						int num2 = (int)(*ptr3);
						int num3 = (int)(*ptr4);
						if (num2 < 128 && num3 < 128 && *CompareInfo.HighCharTable[num2] == 0 && *CompareInfo.HighCharTable[num3] == 0)
						{
							if (num2 == num3)
							{
								ptr3++;
								ptr4++;
								num--;
								continue;
							}
							if (num2 - 97 <= 25)
							{
								num2 -= 32;
							}
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num2 == num3)
							{
								ptr3++;
								ptr4++;
								num--;
								continue;
							}
							if ((ptr3 >= ptr + source.Length - 1 || ptr3[1] < '\u0080') && (ptr4 >= ptr2 + prefix.Length - 1 || ptr4[1] < '\u0080'))
							{
								return false;
							}
						}
						IL_15F:
						return Interop.Globalization.StartsWith(this._sortHandle, ptr2, prefix.Length, ptr, source.Length, options, matchLengthPtr);
					}
					if (source.Length < prefix.Length)
					{
						if (*ptr4 < '\u0080')
						{
							return false;
						}
						goto IL_15F;
					}
					else
					{
						if (source.Length <= prefix.Length || *ptr3 < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = prefix.Length;
							}
							return true;
						}
						goto IL_15F;
					}
				}
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00121774 File Offset: 0x00120974
		private unsafe bool StartsWithOrdinalHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, int* matchLengthPtr)
		{
			int num = Math.Min(source.Length, prefix.Length);
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(prefix))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					while (num != 0)
					{
						int num2 = (int)(*ptr3);
						int num3 = (int)(*ptr4);
						if (num2 < 128 && num3 < 128 && *CompareInfo.HighCharTable[num2] == 0 && *CompareInfo.HighCharTable[num3] == 0)
						{
							if (num2 == num3)
							{
								ptr3++;
								ptr4++;
								num--;
								continue;
							}
							if ((ptr3 >= ptr + source.Length - 1 || ptr3[1] < '\u0080') && (ptr4 >= ptr2 + prefix.Length - 1 || ptr4[1] < '\u0080'))
							{
								return false;
							}
						}
						IL_127:
						return Interop.Globalization.StartsWith(this._sortHandle, ptr2, prefix.Length, ptr, source.Length, options, matchLengthPtr);
					}
					if (source.Length < prefix.Length)
					{
						if (*ptr4 < '\u0080')
						{
							return false;
						}
						goto IL_127;
					}
					else
					{
						if (source.Length <= prefix.Length || *ptr3 < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = prefix.Length;
							}
							return true;
						}
						goto IL_127;
					}
				}
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x001218C8 File Offset: 0x00120AC8
		private unsafe bool IcuEndsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, int* matchLengthPtr)
		{
			if (!this._isAsciiEqualityOrdinal || !CompareInfo.CanUseAsciiOrdinalForOptions(options))
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(source))
				{
					char* source2 = reference;
					fixed (char* reference2 = MemoryMarshal.GetReference<char>(suffix))
					{
						char* target = reference2;
						return Interop.Globalization.EndsWith(this._sortHandle, target, suffix.Length, source2, source.Length, options, matchLengthPtr);
					}
				}
			}
			if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
			{
				return this.EndsWithOrdinalIgnoreCaseHelper(source, suffix, options, matchLengthPtr);
			}
			return this.EndsWithOrdinalHelper(source, suffix, options, matchLengthPtr);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00121934 File Offset: 0x00120B34
		private unsafe bool EndsWithOrdinalIgnoreCaseHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, int* matchLengthPtr)
		{
			int num = Math.Min(source.Length, suffix.Length);
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(suffix))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr + source.Length - 1;
					char* ptr4 = ptr2 + suffix.Length - 1;
					while (num != 0)
					{
						int num2 = (int)(*ptr3);
						int num3 = (int)(*ptr4);
						if (num2 < 128 && num3 < 128 && *CompareInfo.HighCharTable[num2] == 0 && *CompareInfo.HighCharTable[num3] == 0)
						{
							if (num2 == num3)
							{
								ptr3--;
								ptr4--;
								num--;
								continue;
							}
							if (num2 - 97 <= 25)
							{
								num2 -= 32;
							}
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num2 == num3)
							{
								ptr3--;
								ptr4--;
								num--;
								continue;
							}
							if ((ptr3 == ptr || *(ptr3 - 1) < '\u0080') && (ptr4 == ptr2 || *(ptr4 - 1) < '\u0080'))
							{
								return false;
							}
						}
						IL_15F:
						return Interop.Globalization.EndsWith(this._sortHandle, ptr2, suffix.Length, ptr, source.Length, options, matchLengthPtr);
					}
					if (source.Length < suffix.Length)
					{
						if (*ptr4 < '\u0080')
						{
							return false;
						}
						goto IL_15F;
					}
					else
					{
						if (source.Length <= suffix.Length || *ptr3 < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = suffix.Length;
							}
							return true;
						}
						goto IL_15F;
					}
				}
			}
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00121AC0 File Offset: 0x00120CC0
		private unsafe bool EndsWithOrdinalHelper(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, int* matchLengthPtr)
		{
			int num = Math.Min(source.Length, suffix.Length);
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(suffix))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr + source.Length - 1;
					char* ptr4 = ptr2 + suffix.Length - 1;
					while (num != 0)
					{
						int num2 = (int)(*ptr3);
						int num3 = (int)(*ptr4);
						if (num2 < 128 && num3 < 128 && *CompareInfo.HighCharTable[num2] == 0 && *CompareInfo.HighCharTable[num3] == 0)
						{
							if (num2 == num3)
							{
								ptr3--;
								ptr4--;
								num--;
								continue;
							}
							if ((ptr3 == ptr || *(ptr3 - 1) < '\u0080') && (ptr4 == ptr2 || *(ptr4 - 1) < '\u0080'))
							{
								return false;
							}
						}
						IL_127:
						return Interop.Globalization.EndsWith(this._sortHandle, ptr2, suffix.Length, ptr, source.Length, options, matchLengthPtr);
					}
					if (source.Length < suffix.Length)
					{
						if (*ptr4 < '\u0080')
						{
							return false;
						}
						goto IL_127;
					}
					else
					{
						if (source.Length <= suffix.Length || *ptr3 < '\u0080')
						{
							if (matchLengthPtr != null)
							{
								*matchLengthPtr = suffix.Length;
							}
							return true;
						}
						goto IL_127;
					}
				}
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00121C14 File Offset: 0x00120E14
		private unsafe SortKey IcuCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(SR.Argument_InvalidFlag, "options");
			}
			char* ptr;
			if (source == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = source.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* str = ptr;
			int sortKey = Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, null, 0, options);
			byte[] array = new byte[sortKey];
			byte[] array2;
			byte* sortKey2;
			if ((array2 = array) == null || array2.Length == 0)
			{
				sortKey2 = null;
			}
			else
			{
				sortKey2 = &array2[0];
			}
			if (Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, sortKey2, sortKey, options) != sortKey)
			{
				throw new ArgumentException(SR.Arg_ExternalException);
			}
			array2 = null;
			char* ptr2 = null;
			return new SortKey(this, source, options, array);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00121CC8 File Offset: 0x00120EC8
		private unsafe int IcuGetSortKey(ReadOnlySpan<char> source, Span<byte> destination, CompareOptions options)
		{
			int sortKey2;
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* str = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* sortKey = reference2;
					sortKey2 = Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, sortKey, destination.Length, options);
				}
			}
			if (sortKey2 > destination.Length)
			{
				if (sortKey2 <= destination.Length)
				{
					throw new ArgumentException(SR.Arg_ExternalException);
				}
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			return sortKey2;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00121D38 File Offset: 0x00120F38
		private unsafe int IcuGetSortKeyLength(ReadOnlySpan<char> source, CompareOptions options)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* str = reference;
				return Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, null, 0, options);
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00121D68 File Offset: 0x00120F68
		private static bool IcuIsSortable(ReadOnlySpan<char> text)
		{
			Rune value;
			int start;
			while (Rune.DecodeFromUtf16(text, out value, out start) == OperationStatus.Done)
			{
				UnicodeCategory unicodeCategory = Rune.GetUnicodeCategory(value);
				if (unicodeCategory == UnicodeCategory.PrivateUse || unicodeCategory == UnicodeCategory.OtherNotAssigned)
				{
					return false;
				}
				text = text.Slice(start);
				if (text.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00121DAC File Offset: 0x00120FAC
		private unsafe int IcuGetHashCodeOfString(ReadOnlySpan<char> source, CompareOptions options)
		{
			int num = (source.Length > 262144) ? 0 : (4 * source.Length);
			byte[] array = null;
			Span<byte> span2;
			if (num <= 1024)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)1024], 1024);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(num));
			}
			Span<byte> span3 = span2;
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(source))
			{
				char* str = nonNullPinnableReference;
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(span3))
				{
					byte* sortKey = reference;
					num = Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, sortKey, span3.Length, options);
				}
				if (num > span3.Length)
				{
					if (array != null)
					{
						ArrayPool<byte>.Shared.Return(array, false);
					}
					span3 = (array = ArrayPool<byte>.Shared.Rent(num));
					fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(span3))
					{
						byte* sortKey2 = reference2;
						num = Interop.Globalization.GetSortKey(this._sortHandle, str, source.Length, sortKey2, span3.Length, options);
					}
				}
			}
			if (num == 0 || num > span3.Length)
			{
				throw new ArgumentException(SR.Arg_ExternalException);
			}
			int result = Marvin.ComputeHash32(span3.Slice(0, num), Marvin.DefaultSeed);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00121EED File Offset: 0x001210ED
		private static bool CanUseAsciiOrdinalForOptions(CompareOptions options)
		{
			return (options & CompareOptions.IgnoreSymbols) == CompareOptions.None;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00121EF8 File Offset: 0x001210F8
		private SortVersion IcuGetSortVersion()
		{
			int sortVersion = Interop.Globalization.GetSortVersion(this._sortHandle);
			return new SortVersion(sortVersion, this.LCID, new Guid(sortVersion, 0, 0, 0, 0, 0, 0, (byte)(this.LCID >> 24), (byte)((this.LCID & 16711680) >> 16), (byte)((this.LCID & 65280) >> 8), (byte)(this.LCID & 255)));
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00121F5F File Offset: 0x0012115F
		[Nullable(0)]
		private unsafe static ReadOnlySpan<bool> HighCharTable
		{
			get
			{
				return new ReadOnlySpan<bool>((void*)(&<PrivateImplementationDetails>.B9BDFD5832DF62D1AFD47AB884BBAB1236EDD5C1CB9E4A08D9FCEDBAB4644E56), 128);
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00121F70 File Offset: 0x00121170
		internal unsafe static int InvariantIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase, bool fromBeginning = true)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* source2 = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference2;
					return CompareInfo.InvariantFindString(source2, source.Length, value2, value.Length, ignoreCase, fromBeginning);
				}
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00121FA8 File Offset: 0x001211A8
		private unsafe static int InvariantFindString(char* source, int sourceCount, char* value, int valueCount, bool ignoreCase, bool fromBeginning)
		{
			if (valueCount == 0)
			{
				if (!fromBeginning)
				{
					return sourceCount;
				}
				return 0;
			}
			else
			{
				if (sourceCount < valueCount)
				{
					return -1;
				}
				if (fromBeginning)
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c = CompareInfo.InvariantCaseFold(*value);
						for (int i = 0; i <= num; i++)
						{
							char c2 = CompareInfo.InvariantCaseFold(source[i]);
							if (c2 == c)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									c2 = CompareInfo.InvariantCaseFold(source[i + j]);
									char c3 = CompareInfo.InvariantCaseFold(value[j]);
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c4 = *value;
						for (int i = 0; i <= num; i++)
						{
							char c2 = source[i];
							if (c2 == c4)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									c2 = source[i + j];
									char c3 = value[j];
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				else
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c5 = CompareInfo.InvariantCaseFold(*value);
						for (int i = num; i >= 0; i--)
						{
							char c2 = CompareInfo.InvariantCaseFold(source[i]);
							if (c2 == c5)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									c2 = CompareInfo.InvariantCaseFold(source[i + j]);
									char c3 = CompareInfo.InvariantCaseFold(value[j]);
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c6 = *value;
						for (int i = num; i >= 0; i--)
						{
							char c2 = source[i];
							if (c2 == c6)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									c2 = source[i + j];
									char c3 = value[j];
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				return -1;
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00122130 File Offset: 0x00121330
		private static char InvariantCaseFold(char c)
		{
			if (c - 'a' > '\u0019')
			{
				return c;
			}
			return c - ' ';
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00122144 File Offset: 0x00121344
		private SortKey InvariantCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(SR.Argument_InvalidFlag, "options");
			}
			byte[] array;
			if (source.Length == 0)
			{
				array = Array.Empty<byte>();
			}
			else
			{
				array = new byte[source.Length * 2];
				if ((options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) != CompareOptions.None)
				{
					CompareInfo.InvariantCreateSortKeyOrdinalIgnoreCase(source, array);
				}
				else
				{
					CompareInfo.InvariantCreateSortKeyOrdinal(source, array);
				}
			}
			return new SortKey(this, source, options, array);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x001221CC File Offset: 0x001213CC
		private unsafe static void InvariantCreateSortKeyOrdinal(ReadOnlySpan<char> source, Span<byte> sortKey)
		{
			for (int i = 0; i < source.Length; i++)
			{
				BinaryPrimitives.WriteUInt16BigEndian(sortKey, *source[i]);
				sortKey = sortKey.Slice(2);
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00122204 File Offset: 0x00121404
		private unsafe static void InvariantCreateSortKeyOrdinalIgnoreCase(ReadOnlySpan<char> source, Span<byte> sortKey)
		{
			for (int i = 0; i < source.Length; i++)
			{
				BinaryPrimitives.WriteUInt16BigEndian(sortKey, (ushort)CompareInfo.InvariantCaseFold((char)(*source[i])));
				sortKey = sortKey.Slice(2);
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00122241 File Offset: 0x00121441
		private int InvariantGetSortKey(ReadOnlySpan<char> source, Span<byte> destination, CompareOptions options)
		{
			if (destination.Length < source.Length * 2)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
			{
				CompareInfo.InvariantCreateSortKeyOrdinal(source, destination);
			}
			else
			{
				CompareInfo.InvariantCreateSortKeyOrdinalIgnoreCase(source, destination);
			}
			return source.Length * 2;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00122278 File Offset: 0x00121478
		private int InvariantGetSortKeyLength(ReadOnlySpan<char> source, CompareOptions options)
		{
			int num = source.Length * 2;
			if (num < 0)
			{
				throw new ArgumentException(SR.ArgumentOutOfRange_GetByteCountOverflow, "source");
			}
			return num;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x001222A4 File Offset: 0x001214A4
		private void NlsInitSortHandle()
		{
			this._sortHandle = CompareInfo.NlsGetSortHandle(this._sortName);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x001222B8 File Offset: 0x001214B8
		internal unsafe static IntPtr NlsGetSortHandle(string cultureName)
		{
			if (GlobalizationMode.Invariant)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr;
			int num = Interop.Kernel32.LCMapStringEx(cultureName, 536870912U, null, 0, (void*)(&intPtr), IntPtr.Size, null, null, IntPtr.Zero);
			if (num > 0)
			{
				int num2 = 0;
				char c = 'a';
				num = Interop.Kernel32.LCMapStringEx(null, 262144U, &c, 1, (void*)(&num2), 4, null, null, intPtr);
				if (num > 1)
				{
					return intPtr;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00122320 File Offset: 0x00121520
		private unsafe static int FindStringOrdinal(uint dwFindStringOrdinalFlags, ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool bIgnoreCase)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* lpStringSource = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* lpStringValue = reference2;
					return Interop.Kernel32.FindStringOrdinal(dwFindStringOrdinalFlags, lpStringSource, source.Length, lpStringValue, value.Length, bIgnoreCase ? Interop.BOOL.TRUE : Interop.BOOL.FALSE);
				}
			}
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00122364 File Offset: 0x00121564
		internal static int NlsIndexOfOrdinalCore(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase, bool fromBeginning)
		{
			uint dwFindStringOrdinalFlags = fromBeginning ? 4194304U : 8388608U;
			return CompareInfo.FindStringOrdinal(dwFindStringOrdinalFlags, source, value, ignoreCase);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0012238C File Offset: 0x0012158C
		private unsafe int NlsGetHashCodeOfString(ReadOnlySpan<char> source, CompareOptions options)
		{
			if (!Environment.IsWindows8OrAbove)
			{
				source = source.ToString();
			}
			int num = source.Length;
			if (num == 0)
			{
				source = string.Empty;
				num = -1;
			}
			uint dwMapFlags = (uint)(1024 | CompareInfo.GetNativeCompareFlags(options));
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* lpSrcStr = reference;
				int num2 = Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, null, 0, null, null, this._sortHandle);
				if (num2 == 0)
				{
					throw new ArgumentException(SR.Arg_ExternalException);
				}
				byte[] array = null;
				Span<byte> span2;
				if (num2 <= 512)
				{
					Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)512], 512);
					span2 = span;
				}
				else
				{
					span2 = (array = ArrayPool<byte>.Shared.Rent(num2));
				}
				Span<byte> span3 = span2;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(span3))
				{
					byte* lpDestStr = reference2;
					if (Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, (void*)lpDestStr, num2, null, null, this._sortHandle) != num2)
					{
						throw new ArgumentException(SR.Arg_ExternalException);
					}
				}
				int result = Marvin.ComputeHash32(span3.Slice(0, num2), Marvin.DefaultSeed);
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
				return result;
			}
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x001224E8 File Offset: 0x001216E8
		internal unsafe static int NlsCompareStringOrdinalIgnoreCase(ref char string1, int count1, ref char string2, int count2)
		{
			fixed (char* ptr = &string1)
			{
				char* lpString = ptr;
				fixed (char* ptr2 = &string2)
				{
					char* lpString2 = ptr2;
					int num = Interop.Kernel32.CompareStringOrdinal(lpString, count1, lpString2, count2, true);
					if (num == 0)
					{
						throw new ArgumentException(SR.Arg_ExternalException);
					}
					return num - 2;
				}
			}
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00122520 File Offset: 0x00121720
		private unsafe int NlsCompareString(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			string text = (this._sortHandle != IntPtr.Zero) ? null : this._sortName;
			if (string1.IsEmpty)
			{
				string1 = string.Empty;
			}
			if (string2.IsEmpty)
			{
				string2 = string.Empty;
			}
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = text.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* lpLocaleName = ptr;
			fixed (char* reference = MemoryMarshal.GetReference<char>(string1))
			{
				char* lpString = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(string2))
				{
					char* lpString2 = reference2;
					int num = Interop.Kernel32.CompareStringEx(lpLocaleName, (uint)CompareInfo.GetNativeCompareFlags(options), lpString, string1.Length, lpString2, string2.Length, null, null, this._sortHandle);
					if (num == 0)
					{
						throw new ArgumentException(SR.Arg_ExternalException);
					}
					return num - 2;
				}
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x001225DC File Offset: 0x001217DC
		private unsafe int FindString(uint dwFindNLSStringFlags, ReadOnlySpan<char> lpStringSource, ReadOnlySpan<char> lpStringValue, int* pcchFound)
		{
			string text = (this._sortHandle != IntPtr.Zero) ? null : this._sortName;
			int num = lpStringSource.Length;
			if (num == 0)
			{
				lpStringSource = string.Empty;
				num = -1;
			}
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = text.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* lpLocaleName = ptr;
			fixed (char* reference = MemoryMarshal.GetReference<char>(lpStringSource))
			{
				char* lpStringSource2 = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(lpStringValue))
				{
					char* lpStringValue2 = reference2;
					return Interop.Kernel32.FindNLSStringEx(lpLocaleName, dwFindNLSStringFlags, lpStringSource2, num, lpStringValue2, lpStringValue.Length, pcchFound, null, null, this._sortHandle);
				}
			}
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0012266C File Offset: 0x0012186C
		private unsafe int NlsIndexOfCore(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr, bool fromBeginning)
		{
			uint num = fromBeginning ? 4194304U : 8388608U;
			return this.FindString(num | (uint)CompareInfo.GetNativeCompareFlags(options), source, target, matchLengthPtr);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0012269C File Offset: 0x0012189C
		private unsafe bool NlsStartsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options, int* matchLengthPtr)
		{
			int num = this.FindString((uint)(1048576 | CompareInfo.GetNativeCompareFlags(options)), source, prefix, matchLengthPtr);
			if (num >= 0)
			{
				if (matchLengthPtr != null)
				{
					*matchLengthPtr += num;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x001226D4 File Offset: 0x001218D4
		private unsafe bool NlsEndsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options, int* matchLengthPtr)
		{
			int num = this.FindString((uint)(2097152 | CompareInfo.GetNativeCompareFlags(options)), source, suffix, null);
			if (num >= 0)
			{
				if (matchLengthPtr != null)
				{
					*matchLengthPtr = source.Length - num;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00122714 File Offset: 0x00121914
		private unsafe SortKey NlsCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(SR.Argument_InvalidFlag, "options");
			}
			uint dwMapFlags = (uint)(1024 | CompareInfo.GetNativeCompareFlags(options));
			int num = source.Length;
			if (num == 0)
			{
				num = -1;
			}
			char* ptr;
			if (source == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = source.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* lpSrcStr = ptr;
			int num2 = Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, null, 0, null, null, this._sortHandle);
			if (num2 == 0)
			{
				throw new ArgumentException(SR.Arg_ExternalException);
			}
			byte[] array = new byte[num2];
			byte[] array2;
			byte* lpDestStr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				lpDestStr = null;
			}
			else
			{
				lpDestStr = &array2[0];
			}
			if (Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, (void*)lpDestStr, array.Length, null, null, this._sortHandle) != num2)
			{
				throw new ArgumentException(SR.Arg_ExternalException);
			}
			array2 = null;
			char* ptr2 = null;
			return new SortKey(this, source, options, array);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0012282C File Offset: 0x00121A2C
		private unsafe int NlsGetSortKey(ReadOnlySpan<char> source, Span<byte> destination, CompareOptions options)
		{
			if (destination.IsEmpty)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			if (!Environment.IsWindows8OrAbove)
			{
				source = source.ToString();
			}
			uint dwMapFlags = (uint)(1024 | CompareInfo.GetNativeCompareFlags(options));
			int num = source.Length;
			if (num == 0)
			{
				source = string.Empty;
				num = -1;
			}
			int num3;
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* lpSrcStr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* lpDestStr = reference2;
					if (!Environment.IsWindows8OrAbove)
					{
						int num2 = Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, null, 0, null, null, this._sortHandle);
						if (num2 > destination.Length)
						{
							ThrowHelper.ThrowArgumentException_DestinationTooShort();
						}
						if (num2 <= 0)
						{
							throw new ArgumentException(SR.Arg_ExternalException);
						}
					}
					num3 = Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, (void*)lpDestStr, destination.Length, null, null, this._sortHandle);
				}
			}
			if (num3 <= 0)
			{
				if (Marshal.GetLastWin32Error() != 122)
				{
					throw new ArgumentException(SR.Arg_ExternalException);
				}
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			return num3;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0012295C File Offset: 0x00121B5C
		private unsafe int NlsGetSortKeyLength(ReadOnlySpan<char> source, CompareOptions options)
		{
			uint dwMapFlags = (uint)(1024 | CompareInfo.GetNativeCompareFlags(options));
			int num = source.Length;
			if (num == 0)
			{
				source = string.Empty;
				num = -1;
			}
			int num2;
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* lpSrcStr = reference;
				num2 = Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._sortName, dwMapFlags, lpSrcStr, num, null, 0, null, null, this._sortHandle);
			}
			if (num2 <= 0)
			{
				throw new ArgumentException(SR.Arg_ExternalException);
			}
			return num2;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x001229E0 File Offset: 0x00121BE0
		private unsafe static bool NlsIsSortable(ReadOnlySpan<char> text)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(text))
			{
				char* lpString = reference;
				return Interop.Kernel32.IsNLSDefinedString(1, 0U, IntPtr.Zero, lpString, text.Length);
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00122A0C File Offset: 0x00121C0C
		private static int GetNativeCompareFlags(CompareOptions options)
		{
			int num = 134217728;
			if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
			{
				num |= 1;
			}
			if ((options & CompareOptions.IgnoreKanaType) != CompareOptions.None)
			{
				num |= 65536;
			}
			if ((options & CompareOptions.IgnoreNonSpace) != CompareOptions.None)
			{
				num |= 2;
			}
			if ((options & CompareOptions.IgnoreSymbols) != CompareOptions.None)
			{
				num |= 4;
			}
			if ((options & CompareOptions.IgnoreWidth) != CompareOptions.None)
			{
				num |= 131072;
			}
			if ((options & CompareOptions.StringSort) != CompareOptions.None)
			{
				num |= 4096;
			}
			if (options == CompareOptions.Ordinal)
			{
				num = 1073741824;
			}
			return num;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00122A78 File Offset: 0x00121C78
		private unsafe SortVersion NlsGetSortVersion()
		{
			Interop.Kernel32.NlsVersionInfoEx nlsVersionInfoEx = default(Interop.Kernel32.NlsVersionInfoEx);
			nlsVersionInfoEx.dwNLSVersionInfoSize = sizeof(Interop.Kernel32.NlsVersionInfoEx);
			Interop.Kernel32.GetNLSVersionEx(1, this._sortName, &nlsVersionInfoEx);
			return new SortVersion(nlsVersionInfoEx.dwNLSVersion, (nlsVersionInfoEx.dwEffectiveId == 0) ? this.LCID : nlsVersionInfoEx.dwEffectiveId, nlsVersionInfoEx.guidCustomVersion);
		}

		// Token: 0x040006D1 RID: 1745
		internal static readonly CompareInfo Invariant = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x040006D2 RID: 1746
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x040006D3 RID: 1747
		[NonSerialized]
		private IntPtr _sortHandle;

		// Token: 0x040006D4 RID: 1748
		[NonSerialized]
		private string _sortName;

		// Token: 0x040006D5 RID: 1749
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;

		// Token: 0x040006D6 RID: 1750
		private int culture;

		// Token: 0x040006D7 RID: 1751
		[NonSerialized]
		private bool _isAsciiEqualityOrdinal;

		// Token: 0x020001E9 RID: 489
		private static class SortHandleCache
		{
			// Token: 0x06001EF3 RID: 7923 RVA: 0x00122AE4 File Offset: 0x00121CE4
			internal static IntPtr GetCachedSortHandle(string sortName)
			{
				Dictionary<string, IntPtr> obj = CompareInfo.SortHandleCache.s_sortNameToSortHandleCache;
				IntPtr result;
				lock (obj)
				{
					IntPtr intPtr;
					if (!CompareInfo.SortHandleCache.s_sortNameToSortHandleCache.TryGetValue(sortName, out intPtr))
					{
						Interop.Globalization.ResultCode sortHandle = Interop.Globalization.GetSortHandle(sortName, out intPtr);
						if (sortHandle == Interop.Globalization.ResultCode.OutOfMemory)
						{
							throw new OutOfMemoryException();
						}
						if (sortHandle != Interop.Globalization.ResultCode.Success)
						{
							throw new ExternalException(SR.Arg_ExternalException);
						}
						try
						{
							CompareInfo.SortHandleCache.s_sortNameToSortHandleCache.Add(sortName, intPtr);
						}
						catch
						{
							Interop.Globalization.CloseSortHandle(intPtr);
							throw;
						}
					}
					result = intPtr;
				}
				return result;
			}

			// Token: 0x040006D8 RID: 1752
			private static readonly Dictionary<string, IntPtr> s_sortNameToSortHandleCache = new Dictionary<string, IntPtr>();
		}
	}
}
