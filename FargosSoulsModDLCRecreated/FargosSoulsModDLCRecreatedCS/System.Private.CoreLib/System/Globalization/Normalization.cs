using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200021A RID: 538
	internal static class Normalization
	{
		// Token: 0x0600221E RID: 8734 RVA: 0x00130DEB File Offset: 0x0012FFEB
		internal static bool IsNormalized(string strInput, NormalizationForm normalizationForm)
		{
			if (GlobalizationMode.Invariant)
			{
				return true;
			}
			if (!GlobalizationMode.UseNls)
			{
				return Normalization.IcuIsNormalized(strInput, normalizationForm);
			}
			return Normalization.NlsIsNormalized(strInput, normalizationForm);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00130E0C File Offset: 0x0013000C
		internal static string Normalize(string strInput, NormalizationForm normalizationForm)
		{
			if (GlobalizationMode.Invariant)
			{
				return strInput;
			}
			if (!GlobalizationMode.UseNls)
			{
				return Normalization.IcuNormalize(strInput, normalizationForm);
			}
			return Normalization.NlsNormalize(strInput, normalizationForm);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00130E30 File Offset: 0x00130030
		private unsafe static bool IcuIsNormalized(string strInput, NormalizationForm normalizationForm)
		{
			Normalization.ValidateArguments(strInput, normalizationForm);
			char* ptr;
			if (strInput == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = strInput.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* src = ptr;
			int num = Interop.Globalization.IsNormalized(normalizationForm, src, strInput.Length);
			char* ptr2 = null;
			if (num == -1)
			{
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
			}
			return num == 1;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00130E80 File Offset: 0x00130080
		private unsafe static string IcuNormalize(string strInput, NormalizationForm normalizationForm)
		{
			Normalization.ValidateArguments(strInput, normalizationForm);
			char[] array = null;
			try
			{
				Span<char> span2;
				if (strInput.Length <= 512)
				{
					Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)1024], 512);
					span2 = span;
				}
				else
				{
					span2 = (array = ArrayPool<char>.Shared.Rent(strInput.Length));
				}
				Span<char> span3 = span2;
				for (int i = 0; i < 2; i++)
				{
					int num;
					try
					{
						char* ptr;
						if (strInput == null)
						{
							ptr = null;
						}
						else
						{
							fixed (char* ptr2 = strInput.GetPinnableReference())
							{
								ptr = ptr2;
							}
						}
						char* src = ptr;
						try
						{
							fixed (char* ptr3 = MemoryMarshal.GetReference<char>(span3))
							{
								char* dstBuffer = ptr3;
								num = Interop.Globalization.NormalizeString(normalizationForm, src, strInput.Length, dstBuffer, span3.Length);
							}
						}
						finally
						{
							char* ptr3 = null;
						}
					}
					finally
					{
						char* ptr2 = null;
					}
					if (num == -1)
					{
						throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
					}
					if (num <= span3.Length)
					{
						ReadOnlySpan<char> readOnlySpan = span3.Slice(0, num);
						return readOnlySpan.SequenceEqual(strInput) ? strInput : new string(readOnlySpan);
					}
					if (i == 0)
					{
						if (array != null)
						{
							char[] array2 = array;
							array = null;
							ArrayPool<char>.Shared.Return(array2, false);
						}
						span3 = (array = ArrayPool<char>.Shared.Rent(num));
					}
				}
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array, false);
				}
			}
			string result;
			return result;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0013101C File Offset: 0x0013021C
		private static void ValidateArguments(string strInput, NormalizationForm normalizationForm)
		{
			if (OperatingSystem.IsBrowser())
			{
			}
			if (normalizationForm != NormalizationForm.FormC && normalizationForm != NormalizationForm.FormD && normalizationForm != NormalizationForm.FormKC && normalizationForm != NormalizationForm.FormKD)
			{
				throw new ArgumentException(SR.Argument_InvalidNormalizationForm, "normalizationForm");
			}
			if (Normalization.HasInvalidUnicodeSequence(strInput))
			{
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00131068 File Offset: 0x00130268
		private static bool HasInvalidUnicodeSequence(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c >= '\ud800')
				{
					if (c == '￾')
					{
						return true;
					}
					if (char.IsLowSurrogate(c))
					{
						return true;
					}
					if (char.IsHighSurrogate(c))
					{
						if (i + 1 >= s.Length || !char.IsLowSurrogate(s[i + 1]))
						{
							return true;
						}
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x001310D4 File Offset: 0x001302D4
		private unsafe static bool NlsIsNormalized(string strInput, NormalizationForm normalizationForm)
		{
			char* ptr;
			if (strInput == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = strInput.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* source = ptr;
			Interop.BOOL @bool = Interop.Normaliz.IsNormalizedString(normalizationForm, source, strInput.Length);
			char* ptr2 = null;
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error <= 8)
			{
				if (lastWin32Error == 0)
				{
					return @bool > Interop.BOOL.FALSE;
				}
				if (lastWin32Error == 8)
				{
					throw new OutOfMemoryException();
				}
			}
			else if (lastWin32Error == 87 || lastWin32Error == 1113)
			{
				if (normalizationForm != NormalizationForm.FormC && normalizationForm != NormalizationForm.FormD && normalizationForm != NormalizationForm.FormKC && normalizationForm != NormalizationForm.FormKD)
				{
					throw new ArgumentException(SR.Argument_InvalidNormalizationForm, "normalizationForm");
				}
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
			}
			throw new InvalidOperationException(SR.Format(SR.UnknownError_Num, lastWin32Error));
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00131174 File Offset: 0x00130374
		private unsafe static string NlsNormalize(string strInput, NormalizationForm normalizationForm)
		{
			if (strInput.Length == 0)
			{
				return string.Empty;
			}
			char[] array = null;
			try
			{
				Span<char> span2;
				if (strInput.Length <= 512)
				{
					Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)1024], 512);
					span2 = span;
				}
				else
				{
					span2 = (array = ArrayPool<char>.Shared.Rent(strInput.Length));
				}
				Span<char> span3 = span2;
				int num;
				int lastWin32Error;
				for (;;)
				{
					try
					{
						char* ptr;
						if (strInput == null)
						{
							ptr = null;
						}
						else
						{
							fixed (char* ptr2 = strInput.GetPinnableReference())
							{
								ptr = ptr2;
							}
						}
						char* source = ptr;
						try
						{
							fixed (char* ptr3 = MemoryMarshal.GetReference<char>(span3))
							{
								char* destination = ptr3;
								num = Interop.Normaliz.NormalizeString(normalizationForm, source, strInput.Length, destination, span3.Length);
							}
						}
						finally
						{
							char* ptr3 = null;
						}
					}
					finally
					{
						char* ptr2 = null;
					}
					lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error <= 8)
					{
						break;
					}
					if (lastWin32Error == 87)
					{
						goto IL_134;
					}
					if (lastWin32Error != 122)
					{
						goto Block_10;
					}
					num = Math.Abs(num);
					if (array != null)
					{
						char[] array2 = array;
						array = null;
						ArrayPool<char>.Shared.Return(array2, false);
					}
					span3 = (array = ArrayPool<char>.Shared.Rent(num));
				}
				if (lastWin32Error == 0)
				{
					ReadOnlySpan<char> readOnlySpan = span3.Slice(0, num);
					return readOnlySpan.SequenceEqual(strInput) ? strInput : new string(readOnlySpan);
				}
				if (lastWin32Error != 8)
				{
					goto IL_16A;
				}
				throw new OutOfMemoryException();
				Block_10:
				if (lastWin32Error != 1113)
				{
					goto IL_16A;
				}
				IL_134:
				if (normalizationForm != NormalizationForm.FormC && normalizationForm != NormalizationForm.FormD && normalizationForm != NormalizationForm.FormKC && normalizationForm != NormalizationForm.FormKD)
				{
					throw new ArgumentException(SR.Argument_InvalidNormalizationForm, "normalizationForm");
				}
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex, "strInput");
				IL_16A:
				throw new InvalidOperationException(SR.Format(SR.UnknownError_Num, lastWin32Error));
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array, false);
				}
			}
			string result;
			return result;
		}
	}
}
