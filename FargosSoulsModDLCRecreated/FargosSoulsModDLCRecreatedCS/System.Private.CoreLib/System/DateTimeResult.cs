using System;
using System.Globalization;

namespace System
{
	// Token: 0x0200011D RID: 285
	internal ref struct DateTimeResult
	{
		// Token: 0x06000EC7 RID: 3783 RVA: 0x000D7710 File Offset: 0x000D6910
		internal void Init(ReadOnlySpan<char> originalDateTimeString)
		{
			this.originalDateTimeString = originalDateTimeString;
			this.Year = -1;
			this.Month = -1;
			this.Day = -1;
			this.fraction = -1.0;
			this.era = -1;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000D7744 File Offset: 0x000D6944
		internal void SetDate(int year, int month, int day)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000D775B File Offset: 0x000D695B
		internal void SetBadFormatSpecifierFailure()
		{
			this.SetBadFormatSpecifierFailure(ReadOnlySpan<char>.Empty);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000D7768 File Offset: 0x000D6968
		internal void SetBadFormatSpecifierFailure(ReadOnlySpan<char> failedFormatSpecifier)
		{
			this.failure = ParseFailureKind.FormatWithFormatSpecifier;
			this.failureMessageID = "Format_BadFormatSpecifier";
			this.failedFormatSpecifier = failedFormatSpecifier;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000D7783 File Offset: 0x000D6983
		internal void SetBadDateTimeFailure()
		{
			this.failure = ParseFailureKind.FormatWithOriginalDateTime;
			this.failureMessageID = "Format_BadDateTime";
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000D779E File Offset: 0x000D699E
		internal void SetFailure(ParseFailureKind failure, string failureMessageID)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000D77B5 File Offset: 0x000D69B5
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000D77CC File Offset: 0x000D69CC
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
			this.failureArgumentName = failureArgumentName;
		}

		// Token: 0x0400038F RID: 911
		internal int Year;

		// Token: 0x04000390 RID: 912
		internal int Month;

		// Token: 0x04000391 RID: 913
		internal int Day;

		// Token: 0x04000392 RID: 914
		internal int Hour;

		// Token: 0x04000393 RID: 915
		internal int Minute;

		// Token: 0x04000394 RID: 916
		internal int Second;

		// Token: 0x04000395 RID: 917
		internal double fraction;

		// Token: 0x04000396 RID: 918
		internal int era;

		// Token: 0x04000397 RID: 919
		internal ParseFlags flags;

		// Token: 0x04000398 RID: 920
		internal TimeSpan timeZoneOffset;

		// Token: 0x04000399 RID: 921
		internal Calendar calendar;

		// Token: 0x0400039A RID: 922
		internal DateTime parsedDate;

		// Token: 0x0400039B RID: 923
		internal ParseFailureKind failure;

		// Token: 0x0400039C RID: 924
		internal string failureMessageID;

		// Token: 0x0400039D RID: 925
		internal object failureMessageFormatArgument;

		// Token: 0x0400039E RID: 926
		internal string failureArgumentName;

		// Token: 0x0400039F RID: 927
		internal ReadOnlySpan<char> originalDateTimeString;

		// Token: 0x040003A0 RID: 928
		internal ReadOnlySpan<char> failedFormatSpecifier;
	}
}
