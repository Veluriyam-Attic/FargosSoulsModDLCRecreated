using System;

namespace System.Globalization
{
	// Token: 0x020001FD RID: 509
	public class DaylightTime
	{
		// Token: 0x06002088 RID: 8328 RVA: 0x0012AAD8 File Offset: 0x00129CD8
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this._start = start;
			this._end = end;
			this._delta = delta;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0012AAF5 File Offset: 0x00129CF5
		public DateTime Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0012AAFD File Offset: 0x00129CFD
		public DateTime End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0012AB05 File Offset: 0x00129D05
		public TimeSpan Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x0400080A RID: 2058
		private readonly DateTime _start;

		// Token: 0x0400080B RID: 2059
		private readonly DateTime _end;

		// Token: 0x0400080C RID: 2060
		private readonly TimeSpan _delta;
	}
}
