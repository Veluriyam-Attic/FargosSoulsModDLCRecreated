using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200026F RID: 623
	internal static class ProcessorIdCache
	{
		// Token: 0x06002644 RID: 9796 RVA: 0x00141B94 File Offset: 0x00140D94
		private static int RefreshCurrentProcessorId()
		{
			int num = Thread.GetCurrentProcessorNumber();
			if (num < 0)
			{
				num = Environment.CurrentManagedThreadId;
			}
			ProcessorIdCache.t_currentProcessorIdCache = ((num << 16 & int.MaxValue) | ProcessorIdCache.s_processorIdRefreshRate);
			return num;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00141BC8 File Offset: 0x00140DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetCurrentProcessorId()
		{
			int num = ProcessorIdCache.t_currentProcessorIdCache--;
			if ((num & 65535) == 0)
			{
				return ProcessorIdCache.RefreshCurrentProcessorId();
			}
			return num >> 16;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00141BF8 File Offset: 0x00140DF8
		internal static bool ProcessorNumberSpeedCheck()
		{
			double num = double.MaxValue;
			double num2 = double.MaxValue;
			ProcessorIdCache.UninlinedThreadStatic();
			if (Thread.GetCurrentProcessorNumber() < 0)
			{
				ProcessorIdCache.s_processorIdRefreshRate = 65535;
				return false;
			}
			long num3 = Stopwatch.Frequency / 1000000L + 1L;
			for (int i = 0; i < 10; i++)
			{
				int num4 = 8;
				long num5;
				do
				{
					num4 *= 2;
					num5 = Stopwatch.GetTimestamp();
					for (int j = 0; j < num4; j++)
					{
						Thread.GetCurrentProcessorNumber();
					}
					num5 = Stopwatch.GetTimestamp() - num5;
				}
				while (num5 < num3);
				num = Math.Min(num, (double)num5 / (double)num4);
				num4 /= 4;
				do
				{
					num4 *= 2;
					num5 = Stopwatch.GetTimestamp();
					for (int k = 0; k < num4; k++)
					{
						ProcessorIdCache.UninlinedThreadStatic();
					}
					num5 = Stopwatch.GetTimestamp() - num5;
				}
				while (num5 < num3);
				num2 = Math.Min(num2, (double)num5 / (double)num4);
			}
			ProcessorIdCache.s_processorIdRefreshRate = Math.Min((int)(num * 5.0 / num2), 5000);
			return ProcessorIdCache.s_processorIdRefreshRate <= 5;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00141D09 File Offset: 0x00140F09
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int UninlinedThreadStatic()
		{
			return ProcessorIdCache.t_currentProcessorIdCache;
		}

		// Token: 0x040009F4 RID: 2548
		[ThreadStatic]
		private static int t_currentProcessorIdCache;

		// Token: 0x040009F5 RID: 2549
		private static int s_processorIdRefreshRate;
	}
}
