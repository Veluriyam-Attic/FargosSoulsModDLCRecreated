using System;

namespace System.StubHelpers
{
	// Token: 0x020003A4 RID: 932
	internal static class DateMarshaler
	{
		// Token: 0x060030B2 RID: 12466 RVA: 0x00168020 File Offset: 0x00167220
		internal static double ConvertToNative(DateTime managedDate)
		{
			return managedDate.ToOADate();
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x00168029 File Offset: 0x00167229
		internal static long ConvertToManaged(double nativeDate)
		{
			return DateTime.DoubleDateToTicks(nativeDate);
		}
	}
}
