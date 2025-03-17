using System;

namespace System
{
	// Token: 0x0200011A RID: 282
	internal struct DateTimeRawInfo
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x000D769A File Offset: 0x000D689A
		internal unsafe void Init(int* numberBuffer)
		{
			this.month = -1;
			this.year = -1;
			this.dayOfWeek = -1;
			this.era = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
			this.fraction = -1.0;
			this.num = numberBuffer;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000D76D8 File Offset: 0x000D68D8
		internal unsafe void AddNumber(int value)
		{
			ref int ptr = ref *this.num;
			int num = this.numCount;
			this.numCount = num + 1;
			*(ref ptr + (IntPtr)num * 4) = value;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000D7702 File Offset: 0x000D6902
		internal unsafe int GetNumber(int index)
		{
			return this.num[index];
		}

		// Token: 0x0400036D RID: 877
		private unsafe int* num;

		// Token: 0x0400036E RID: 878
		internal int numCount;

		// Token: 0x0400036F RID: 879
		internal int month;

		// Token: 0x04000370 RID: 880
		internal int year;

		// Token: 0x04000371 RID: 881
		internal int dayOfWeek;

		// Token: 0x04000372 RID: 882
		internal int era;

		// Token: 0x04000373 RID: 883
		internal DateTimeParse.TM timeMark;

		// Token: 0x04000374 RID: 884
		internal double fraction;

		// Token: 0x04000375 RID: 885
		internal bool hasSameDateAndTimeSeparators;
	}
}
