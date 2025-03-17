using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200079A RID: 1946
	internal static class RuntimeEventSourceHelper
	{
		// Token: 0x06005DB6 RID: 23990 RVA: 0x001C4FC0 File Offset: 0x001C41C0
		internal static int GetCpuUsage()
		{
			long num;
			long num2;
			long num3;
			long num4;
			if (!Interop.Kernel32.GetProcessTimes(Interop.Kernel32.GetCurrentProcess(), out num, out num2, out num3, out num4))
			{
				return 0;
			}
			long num5;
			long num6;
			if (!Interop.Kernel32.GetSystemTimes(out num2, out num5, out num6))
			{
				return 0;
			}
			int result;
			if (RuntimeEventSourceHelper.s_prevSystemUserTime == 0L && RuntimeEventSourceHelper.s_prevSystemKernelTime == 0L)
			{
				result = 0;
			}
			else
			{
				long num7 = num4 - RuntimeEventSourceHelper.s_prevProcUserTime + (num3 - RuntimeEventSourceHelper.s_prevProcKernelTime);
				long num8 = num5 - RuntimeEventSourceHelper.s_prevSystemUserTime + (num6 - RuntimeEventSourceHelper.s_prevSystemKernelTime);
				result = (int)(num7 * 100L / num8);
			}
			RuntimeEventSourceHelper.s_prevProcUserTime = num4;
			RuntimeEventSourceHelper.s_prevProcKernelTime = num3;
			RuntimeEventSourceHelper.s_prevSystemUserTime = num5;
			RuntimeEventSourceHelper.s_prevSystemKernelTime = num6;
			return result;
		}

		// Token: 0x04001C8C RID: 7308
		private static long s_prevProcUserTime;

		// Token: 0x04001C8D RID: 7309
		private static long s_prevProcKernelTime;

		// Token: 0x04001C8E RID: 7310
		private static long s_prevSystemUserTime;

		// Token: 0x04001C8F RID: 7311
		private static long s_prevSystemKernelTime;
	}
}
