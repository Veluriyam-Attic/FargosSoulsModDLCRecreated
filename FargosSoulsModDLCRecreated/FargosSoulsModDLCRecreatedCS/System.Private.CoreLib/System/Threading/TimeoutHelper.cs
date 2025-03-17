using System;

namespace System.Threading
{
	// Token: 0x020002D8 RID: 728
	internal static class TimeoutHelper
	{
		// Token: 0x060028E7 RID: 10471 RVA: 0x0014A1A9 File Offset: 0x001493A9
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0014A1B0 File Offset: 0x001493B0
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
