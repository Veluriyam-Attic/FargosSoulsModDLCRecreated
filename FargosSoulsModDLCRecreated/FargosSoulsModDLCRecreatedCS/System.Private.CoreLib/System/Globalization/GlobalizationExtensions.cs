using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000201 RID: 513
	public static class GlobalizationExtensions
	{
		// Token: 0x060020BA RID: 8378 RVA: 0x0012B668 File Offset: 0x0012A868
		[NullableContext(1)]
		public static StringComparer GetStringComparer(this CompareInfo compareInfo, CompareOptions options)
		{
			if (compareInfo == null)
			{
				throw new ArgumentNullException("compareInfo");
			}
			if (options == CompareOptions.Ordinal)
			{
				return StringComparer.Ordinal;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return StringComparer.OrdinalIgnoreCase;
			}
			return new CultureAwareComparer(compareInfo, options);
		}
	}
}
