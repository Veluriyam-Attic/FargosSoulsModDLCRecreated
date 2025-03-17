using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000665 RID: 1637
	public enum OperandType
	{
		// Token: 0x04001723 RID: 5923
		InlineBrTarget,
		// Token: 0x04001724 RID: 5924
		InlineField,
		// Token: 0x04001725 RID: 5925
		InlineI,
		// Token: 0x04001726 RID: 5926
		InlineI8,
		// Token: 0x04001727 RID: 5927
		InlineMethod,
		// Token: 0x04001728 RID: 5928
		InlineNone,
		// Token: 0x04001729 RID: 5929
		[Obsolete("This API has been deprecated. https://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		// Token: 0x0400172A RID: 5930
		InlineR,
		// Token: 0x0400172B RID: 5931
		InlineSig = 9,
		// Token: 0x0400172C RID: 5932
		InlineString,
		// Token: 0x0400172D RID: 5933
		InlineSwitch,
		// Token: 0x0400172E RID: 5934
		InlineTok,
		// Token: 0x0400172F RID: 5935
		InlineType,
		// Token: 0x04001730 RID: 5936
		InlineVar,
		// Token: 0x04001731 RID: 5937
		ShortInlineBrTarget,
		// Token: 0x04001732 RID: 5938
		ShortInlineI,
		// Token: 0x04001733 RID: 5939
		ShortInlineR,
		// Token: 0x04001734 RID: 5940
		ShortInlineVar
	}
}
