using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020006CE RID: 1742
	[Nullable(0)]
	[NullableContext(2)]
	public static class Debug
	{
		// Token: 0x06005882 RID: 22658 RVA: 0x001B0B4B File Offset: 0x001AFD4B
		[NullableContext(1)]
		public static DebugProvider SetProvider(DebugProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			return Interlocked.Exchange<DebugProvider>(ref Debug.s_provider, provider);
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x000AC09E File Offset: 0x000AB29E
		// (set) Token: 0x06005884 RID: 22660 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static bool AutoFlush
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x001B0B66 File Offset: 0x001AFD66
		// (set) Token: 0x06005886 RID: 22662 RVA: 0x001B0B6D File Offset: 0x001AFD6D
		public static int IndentLevel
		{
			get
			{
				return Debug.t_indentLevel;
			}
			set
			{
				Debug.t_indentLevel = ((value < 0) ? 0 : value);
				Debug.s_provider.OnIndentLevelChanged(Debug.t_indentLevel);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06005887 RID: 22663 RVA: 0x001B0B8D File Offset: 0x001AFD8D
		// (set) Token: 0x06005888 RID: 22664 RVA: 0x001B0B96 File Offset: 0x001AFD96
		public static int IndentSize
		{
			get
			{
				return Debug.s_indentSize;
			}
			set
			{
				Debug.s_indentSize = ((value < 0) ? 0 : value);
				Debug.s_provider.OnIndentSizeChanged(Debug.s_indentSize);
			}
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Conditional("DEBUG")]
		public static void Close()
		{
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Conditional("DEBUG")]
		public static void Flush()
		{
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x001B0BBA File Offset: 0x001AFDBA
		[Conditional("DEBUG")]
		public static void Indent()
		{
			Debug.IndentLevel++;
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x001B0BC8 File Offset: 0x001AFDC8
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			Debug.IndentLevel--;
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x001B0BD6 File Offset: 0x001AFDD6
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			Debug.WriteLine(message);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x001B0BDE File Offset: 0x001AFDDE
		[NullableContext(1)]
		[Conditional("DEBUG")]
		public static void Print(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			Debug.WriteLine(string.Format(null, format, args));
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x001B0BED File Offset: 0x001AFDED
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool condition)
		{
			Debug.Assert(condition, string.Empty, string.Empty);
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x001B0BFF File Offset: 0x001AFDFF
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool condition, string message)
		{
			Debug.Assert(condition, message, string.Empty);
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x001B0C0D File Offset: 0x001AFE0D
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool condition, string message, string detailMessage)
		{
			if (!condition)
			{
				Debug.Fail(message, detailMessage);
			}
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x001B0C1C File Offset: 0x001AFE1C
		internal static void ContractFailure(string message, string detailMessage, string failureKindMessage)
		{
			string stackTrace;
			try
			{
				stackTrace = new StackTrace(2, true).ToString(StackTrace.TraceFormat.Normal);
			}
			catch
			{
				stackTrace = "";
			}
			Debug.s_provider.WriteAssert(stackTrace, message, detailMessage);
			DebugProvider.FailCore(stackTrace, message, detailMessage, failureKindMessage);
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x001B0C6C File Offset: 0x001AFE6C
		[Conditional("DEBUG")]
		[DoesNotReturn]
		public static void Fail(string message)
		{
			Debug.Fail(message, string.Empty);
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x001B0C79 File Offset: 0x001AFE79
		[DoesNotReturn]
		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Fail(string message, string detailMessage)
		{
			Debug.s_provider.Fail(message, detailMessage);
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x001B0C89 File Offset: 0x001AFE89
		[Conditional("DEBUG")]
		[NullableContext(1)]
		public static void Assert([DoesNotReturnIf(false)] bool condition, [Nullable(2)] string message, string detailMessageFormat, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			Debug.Assert(condition, message, string.Format(detailMessageFormat, args));
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001B0C99 File Offset: 0x001AFE99
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
			Debug.s_provider.WriteLine(message);
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x001B0CA8 File Offset: 0x001AFEA8
		[Conditional("DEBUG")]
		public static void Write(string message)
		{
			Debug.s_provider.Write(message);
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x001B0CB7 File Offset: 0x001AFEB7
		[Conditional("DEBUG")]
		public static void WriteLine(object value)
		{
			Debug.WriteLine((value != null) ? value.ToString() : null);
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x001B0CCA File Offset: 0x001AFECA
		[Conditional("DEBUG")]
		public static void WriteLine(object value, string category)
		{
			Debug.WriteLine((value != null) ? value.ToString() : null, category);
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x001B0BDE File Offset: 0x001AFDDE
		[Conditional("DEBUG")]
		[NullableContext(1)]
		public static void WriteLine(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			Debug.WriteLine(string.Format(null, format, args));
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x001B0CDE File Offset: 0x001AFEDE
		[Conditional("DEBUG")]
		public static void WriteLine(string message, string category)
		{
			if (category == null)
			{
				Debug.WriteLine(message);
				return;
			}
			Debug.WriteLine(category + ": " + message);
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x001B0CFB File Offset: 0x001AFEFB
		[Conditional("DEBUG")]
		public static void Write(object value)
		{
			Debug.Write((value != null) ? value.ToString() : null);
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x001B0D0E File Offset: 0x001AFF0E
		[Conditional("DEBUG")]
		public static void Write(string message, string category)
		{
			if (category == null)
			{
				Debug.Write(message);
				return;
			}
			Debug.Write(category + ": " + message);
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x001B0D2B File Offset: 0x001AFF2B
		[Conditional("DEBUG")]
		public static void Write(object value, string category)
		{
			Debug.Write((value != null) ? value.ToString() : null, category);
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x001B0D3F File Offset: 0x001AFF3F
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				Debug.Write(message);
			}
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x001B0D4A File Offset: 0x001AFF4A
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				Debug.Write(value);
			}
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x001B0D55 File Offset: 0x001AFF55
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				Debug.Write(message, category);
			}
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x001B0D61 File Offset: 0x001AFF61
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				Debug.Write(value, category);
			}
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x001B0D6D File Offset: 0x001AFF6D
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				Debug.WriteLine(value);
			}
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x001B0D78 File Offset: 0x001AFF78
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				Debug.WriteLine(value, category);
			}
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x001B0D84 File Offset: 0x001AFF84
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				Debug.WriteLine(message);
			}
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x001B0D8F File Offset: 0x001AFF8F
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				Debug.WriteLine(message, category);
			}
		}

		// Token: 0x04001964 RID: 6500
		private static volatile DebugProvider s_provider = new DebugProvider();

		// Token: 0x04001965 RID: 6501
		[ThreadStatic]
		private static int t_indentLevel;

		// Token: 0x04001966 RID: 6502
		private static volatile int s_indentSize = 4;
	}
}
