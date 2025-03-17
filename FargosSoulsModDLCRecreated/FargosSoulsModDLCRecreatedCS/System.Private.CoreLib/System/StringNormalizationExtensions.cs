using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200018A RID: 394
	[NullableContext(1)]
	[Nullable(0)]
	public static class StringNormalizationExtensions
	{
		// Token: 0x06001804 RID: 6148 RVA: 0x000F29EF File Offset: 0x000F1BEF
		public static bool IsNormalized(this string strInput)
		{
			return strInput.IsNormalized(NormalizationForm.FormC);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000F29F8 File Offset: 0x000F1BF8
		public static bool IsNormalized(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.IsNormalized(normalizationForm);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000F2A0F File Offset: 0x000F1C0F
		public static string Normalize(this string strInput)
		{
			return strInput.Normalize(NormalizationForm.FormC);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000F2A18 File Offset: 0x000F1C18
		public static string Normalize(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.Normalize(normalizationForm);
		}
	}
}
