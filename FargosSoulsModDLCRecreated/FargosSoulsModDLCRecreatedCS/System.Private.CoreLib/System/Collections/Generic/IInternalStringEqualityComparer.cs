using System;

namespace System.Collections.Generic
{
	// Token: 0x020007FA RID: 2042
	internal interface IInternalStringEqualityComparer : IEqualityComparer<string>
	{
		// Token: 0x0600619B RID: 24987
		IEqualityComparer<string> GetUnderlyingEqualityComparer();

		// Token: 0x0600619C RID: 24988 RVA: 0x001D2270 File Offset: 0x001D1470
		internal static IEqualityComparer<string> GetUnderlyingEqualityComparer(IEqualityComparer<string> outerComparer)
		{
			if (outerComparer == null)
			{
				return EqualityComparer<string>.Default;
			}
			IInternalStringEqualityComparer internalStringEqualityComparer = outerComparer as IInternalStringEqualityComparer;
			if (internalStringEqualityComparer != null)
			{
				return internalStringEqualityComparer.GetUnderlyingEqualityComparer();
			}
			return outerComparer;
		}
	}
}
