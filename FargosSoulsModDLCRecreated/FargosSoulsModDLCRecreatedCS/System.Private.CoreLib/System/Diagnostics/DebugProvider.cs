using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006DA RID: 1754
	[NullableContext(2)]
	[Nullable(0)]
	public class DebugProvider
	{
		// Token: 0x060058D2 RID: 22738 RVA: 0x001B10C4 File Offset: 0x001B02C4
		[DoesNotReturn]
		public virtual void Fail(string message, string detailMessage)
		{
			string stackTrace;
			try
			{
				stackTrace = new StackTrace(0, true).ToString(StackTrace.TraceFormat.Normal);
			}
			catch
			{
				stackTrace = "";
			}
			this.WriteAssert(stackTrace, message, detailMessage);
			DebugProvider.FailCore(stackTrace, message, detailMessage, "Assertion failed.");
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x001B1110 File Offset: 0x001B0310
		internal void WriteAssert(string stackTrace, string message, string detailMessage)
		{
			this.WriteLine(string.Concat(new string[]
			{
				SR.DebugAssertBanner,
				"\r\n",
				SR.DebugAssertShortMessage,
				"\r\n",
				message,
				"\r\n",
				SR.DebugAssertLongMessage,
				"\r\n",
				detailMessage,
				"\r\n",
				stackTrace
			}));
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x001B1180 File Offset: 0x001B0380
		public virtual void Write(string message)
		{
			object obj = DebugProvider.s_lock;
			lock (obj)
			{
				if (message == null)
				{
					DebugProvider.WriteCore(string.Empty);
				}
				else
				{
					if (this._needIndent)
					{
						message = this.GetIndentString() + message;
						this._needIndent = false;
					}
					DebugProvider.WriteCore(message);
					if (message.EndsWith("\r\n", StringComparison.Ordinal))
					{
						this._needIndent = true;
					}
				}
			}
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x001B1200 File Offset: 0x001B0400
		public virtual void WriteLine(string message)
		{
			this.Write(message + "\r\n");
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void OnIndentLevelChanged(int indentLevel)
		{
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void OnIndentSizeChanged(int indentSize)
		{
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x001B1214 File Offset: 0x001B0414
		private string GetIndentString()
		{
			int num = Debug.IndentSize * Debug.IndentLevel;
			string indentString = this._indentString;
			if (indentString != null && indentString.Length == num)
			{
				return this._indentString;
			}
			return this._indentString = new string(' ', num);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x001B125C File Offset: 0x001B045C
		[NullableContext(1)]
		public static void FailCore(string stackTrace, [Nullable(2)] string message, [Nullable(2)] string detailMessage, string errorSource)
		{
			if (DebugProvider.s_FailCore != null)
			{
				DebugProvider.s_FailCore(stackTrace, message, detailMessage, errorSource);
				return;
			}
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			DebugProvider.DebugAssertException ex = new DebugProvider.DebugAssertException(message, detailMessage, stackTrace);
			Environment.FailFast(ex.Message, ex, errorSource);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x001B12A4 File Offset: 0x001B04A4
		[NullableContext(1)]
		public static void WriteCore(string message)
		{
			if (DebugProvider.s_WriteCore != null)
			{
				DebugProvider.s_WriteCore(message);
				return;
			}
			object obj = DebugProvider.s_ForLock;
			lock (obj)
			{
				if (message.Length <= 4091)
				{
					DebugProvider.WriteToDebugger(message);
				}
				else
				{
					int i;
					for (i = 0; i < message.Length - 4091; i += 4091)
					{
						DebugProvider.WriteToDebugger(message.Substring(i, 4091));
					}
					DebugProvider.WriteToDebugger(message.Substring(i));
				}
			}
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x001B1340 File Offset: 0x001B0540
		private static void WriteToDebugger(string message)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
				return;
			}
			Interop.Kernel32.OutputDebugString(message ?? string.Empty);
		}

		// Token: 0x04001980 RID: 6528
		private static readonly object s_lock = new object();

		// Token: 0x04001981 RID: 6529
		private bool _needIndent = true;

		// Token: 0x04001982 RID: 6530
		private string _indentString;

		// Token: 0x04001983 RID: 6531
		internal static Action<string, string, string, string> s_FailCore;

		// Token: 0x04001984 RID: 6532
		internal static Action<string> s_WriteCore;

		// Token: 0x04001985 RID: 6533
		private static readonly object s_ForLock = new object();

		// Token: 0x020006DB RID: 1755
		private sealed class DebugAssertException : Exception
		{
			// Token: 0x060058DE RID: 22750 RVA: 0x001B1386 File Offset: 0x001B0586
			internal DebugAssertException(string message, string detailMessage, string stackTrace) : base(DebugProvider.DebugAssertException.Terminate(message) + DebugProvider.DebugAssertException.Terminate(detailMessage) + stackTrace)
			{
			}

			// Token: 0x060058DF RID: 22751 RVA: 0x001B13A0 File Offset: 0x001B05A0
			private static string Terminate(string s)
			{
				if (s == null)
				{
					return s;
				}
				s = s.Trim();
				if (s.Length > 0)
				{
					s += "\r\n";
				}
				return s;
			}
		}
	}
}
