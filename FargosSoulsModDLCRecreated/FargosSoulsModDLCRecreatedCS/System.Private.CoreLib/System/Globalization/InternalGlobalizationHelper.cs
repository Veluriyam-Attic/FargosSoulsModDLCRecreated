using System;

namespace System.Globalization
{
	// Token: 0x02000213 RID: 531
	internal static class InternalGlobalizationHelper
	{
		// Token: 0x0600218F RID: 8591 RVA: 0x0012F820 File Offset: 0x0012EA20
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, SR.Overflow_TimeSpanTooLong);
			}
			return num * 10000000L;
		}
	}
}
