using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Globalization
{
	// Token: 0x02000223 RID: 547
	[NullableContext(1)]
	[Nullable(0)]
	public class StringInfo
	{
		// Token: 0x060022CC RID: 8908 RVA: 0x00133D1D File Offset: 0x00132F1D
		public StringInfo() : this(string.Empty)
		{
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00133D2A File Offset: 0x00132F2A
		public StringInfo(string value)
		{
			this.String = value;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00133D3C File Offset: 0x00132F3C
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			StringInfo stringInfo = value as StringInfo;
			return stringInfo != null && this._str.Equals(stringInfo._str);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00133D66 File Offset: 0x00132F66
		public override int GetHashCode()
		{
			return this._str.GetHashCode();
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x00133D73 File Offset: 0x00132F73
		[Nullable(2)]
		private int[] Indexes
		{
			get
			{
				if (this._indexes == null && this.String.Length > 0)
				{
					this._indexes = StringInfo.ParseCombiningCharacters(this.String);
				}
				return this._indexes;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00133DA2 File Offset: 0x00132FA2
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x00133DAA File Offset: 0x00132FAA
		public string String
		{
			get
			{
				return this._str;
			}
			[MemberNotNull("_str")]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._str = value;
				this._indexes = null;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00133DC9 File Offset: 0x00132FC9
		public int LengthInTextElements
		{
			get
			{
				int[] indexes = this.Indexes;
				if (indexes == null)
				{
					return 0;
				}
				return indexes.Length;
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00133DD9 File Offset: 0x00132FD9
		public string SubstringByTextElements(int startingTextElement)
		{
			int[] indexes = this.Indexes;
			return this.SubstringByTextElements(startingTextElement, ((indexes != null) ? indexes.Length : 0) - startingTextElement);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00133DF4 File Offset: 0x00132FF4
		public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
		{
			int[] array = this.Indexes ?? Array.Empty<int>();
			if (startingTextElement >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", startingTextElement, SR.Arg_ArgumentOutOfRangeException);
			}
			if (lengthInTextElements > array.Length - startingTextElement)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", lengthInTextElements, SR.Arg_ArgumentOutOfRangeException);
			}
			int num = array[startingTextElement];
			Index index = new Index(0, true);
			if (startingTextElement + lengthInTextElements < array.Length)
			{
				index = array[startingTextElement + lengthInTextElements];
			}
			string @string = this.String;
			int length = @string.Length;
			int num2 = num;
			int length2 = index.GetOffset(length) - num2;
			return @string.Substring(num2, length2);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00133E91 File Offset: 0x00133091
		public static string GetNextTextElement(string str)
		{
			return StringInfo.GetNextTextElement(str, 0);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00133E9A File Offset: 0x0013309A
		public static string GetNextTextElement(string str, int index)
		{
			if (str == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.str);
			}
			if (index > str.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return str.Substring(index, TextSegmentationUtility.GetLengthOfFirstUtf16ExtendedGraphemeCluster(str.AsSpan(index)));
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00133EC7 File Offset: 0x001330C7
		public static TextElementEnumerator GetTextElementEnumerator(string str)
		{
			return StringInfo.GetTextElementEnumerator(str, 0);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00133ED0 File Offset: 0x001330D0
		public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
		{
			if (str == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.str);
			}
			if (index > str.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return new TextElementEnumerator(str, index);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00133EF4 File Offset: 0x001330F4
		public unsafe static int[] ParseCombiningCharacters(string str)
		{
			if (str == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.str);
			}
			if (str.Length > 256)
			{
				return StringInfo.ParseCombiningCharactersForLargeString(str);
			}
			int length = str.Length;
			Span<int> span = new Span<int>(stackalloc byte[checked(unchecked((UIntPtr)length) * 4)], length);
			Span<int> span2 = span;
			int length2 = 0;
			ReadOnlySpan<char> input = str;
			while (!input.IsEmpty)
			{
				*span2[length2++] = str.Length - input.Length;
				input = input.Slice(TextSegmentationUtility.GetLengthOfFirstUtf16ExtendedGraphemeCluster(input));
			}
			return span2.Slice(0, length2).ToArray();
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00133F88 File Offset: 0x00133188
		private static int[] ParseCombiningCharactersForLargeString(string str)
		{
			List<int> list = new List<int>();
			ReadOnlySpan<char> input = str;
			while (!input.IsEmpty)
			{
				list.Add(str.Length - input.Length);
				input = input.Slice(TextSegmentationUtility.GetLengthOfFirstUtf16ExtendedGraphemeCluster(input));
			}
			return list.ToArray();
		}

		// Token: 0x040008D1 RID: 2257
		private string _str;

		// Token: 0x040008D2 RID: 2258
		private int[] _indexes;
	}
}
