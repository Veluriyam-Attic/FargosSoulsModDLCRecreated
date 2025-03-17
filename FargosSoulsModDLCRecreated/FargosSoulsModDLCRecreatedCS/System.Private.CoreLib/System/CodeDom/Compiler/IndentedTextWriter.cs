using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000242 RID: 578
	[Nullable(0)]
	[NullableContext(1)]
	public class IndentedTextWriter : TextWriter
	{
		// Token: 0x060023F0 RID: 9200 RVA: 0x00138BE7 File Offset: 0x00137DE7
		public IndentedTextWriter(TextWriter writer) : this(writer, "    ")
		{
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00138BF5 File Offset: 0x00137DF5
		public IndentedTextWriter(TextWriter writer, string tabString) : base(CultureInfo.InvariantCulture)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this._writer = writer;
			this._tabString = tabString;
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x00138C1E File Offset: 0x00137E1E
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x00138C2B File Offset: 0x00137E2B
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x00138C38 File Offset: 0x00137E38
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			[param: AllowNull]
			set
			{
				this._writer.NewLine = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x00138C46 File Offset: 0x00137E46
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x00138C4E File Offset: 0x00137E4E
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				this._indentLevel = Math.Max(value, 0);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x00138C5D File Offset: 0x00137E5D
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00138C65 File Offset: 0x00137E65
		public override void Close()
		{
			this._writer.Close();
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00138C72 File Offset: 0x00137E72
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00138C80 File Offset: 0x00137E80
		protected virtual void OutputTabs()
		{
			if (this._tabsPending)
			{
				for (int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._tabString);
				}
				this._tabsPending = false;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00138CBE File Offset: 0x00137EBE
		[NullableContext(2)]
		public override void Write(string s)
		{
			this.OutputTabs();
			this._writer.Write(s);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00138CD2 File Offset: 0x00137ED2
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00138CE6 File Offset: 0x00137EE6
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00138CFA File Offset: 0x00137EFA
		[NullableContext(2)]
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00138D0E File Offset: 0x00137F0E
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00138D24 File Offset: 0x00137F24
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00138D38 File Offset: 0x00137F38
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00138D4C File Offset: 0x00137F4C
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00138D60 File Offset: 0x00137F60
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00138D74 File Offset: 0x00137F74
		[NullableContext(2)]
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00138D88 File Offset: 0x00137F88
		public override void Write(string format, [Nullable(2)] object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00138D9D File Offset: 0x00137F9D
		[NullableContext(2)]
		public override void Write([Nullable(1)] string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00138DB3 File Offset: 0x00137FB3
		public override void Write(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00138DC8 File Offset: 0x00137FC8
		[NullableContext(2)]
		public void WriteLineNoTabs(string s)
		{
			this._writer.WriteLine(s);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00138DD6 File Offset: 0x00137FD6
		[NullableContext(2)]
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this._writer.WriteLine(s);
			this._tabsPending = true;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00138DF1 File Offset: 0x00137FF1
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00138E0B File Offset: 0x0013800B
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00138E26 File Offset: 0x00138026
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00138E41 File Offset: 0x00138041
		[NullableContext(2)]
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00138E5C File Offset: 0x0013805C
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00138E79 File Offset: 0x00138079
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00138E94 File Offset: 0x00138094
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00138EAF File Offset: 0x001380AF
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00138ECA File Offset: 0x001380CA
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00138EE5 File Offset: 0x001380E5
		[NullableContext(2)]
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00138F00 File Offset: 0x00138100
		public override void WriteLine(string format, [Nullable(2)] object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00138F1C File Offset: 0x0013811C
		[NullableContext(2)]
		public override void WriteLine([Nullable(1)] string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00138F39 File Offset: 0x00138139
		public override void WriteLine(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00138F55 File Offset: 0x00138155
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x0400096C RID: 2412
		private readonly TextWriter _writer;

		// Token: 0x0400096D RID: 2413
		private readonly string _tabString;

		// Token: 0x0400096E RID: 2414
		private int _indentLevel;

		// Token: 0x0400096F RID: 2415
		private bool _tabsPending;

		// Token: 0x04000970 RID: 2416
		public const string DefaultTabString = "    ";
	}
}
