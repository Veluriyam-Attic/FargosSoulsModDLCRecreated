using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006AF RID: 1711
	[NullableContext(1)]
	[Nullable(0)]
	public class StringWriter : TextWriter
	{
		// Token: 0x060056E7 RID: 22247 RVA: 0x001ABEE0 File Offset: 0x001AB0E0
		public StringWriter() : this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x001ABEF2 File Offset: 0x001AB0F2
		[NullableContext(2)]
		public StringWriter(IFormatProvider formatProvider) : this(new StringBuilder(), formatProvider)
		{
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x001ABF00 File Offset: 0x001AB100
		public StringWriter(StringBuilder sb) : this(sb, CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x001ABF0E File Offset: 0x001AB10E
		public StringWriter(StringBuilder sb, [Nullable(2)] IFormatProvider formatProvider) : base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", SR.ArgumentNull_Buffer);
			}
			this._sb = sb;
			this._isOpen = true;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x001ABF38 File Offset: 0x001AB138
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x001ABF41 File Offset: 0x001AB141
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060056ED RID: 22253 RVA: 0x001ABF51 File Offset: 0x001AB151
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.s_encoding == null)
				{
					StringWriter.s_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.s_encoding;
			}
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x001ABF71 File Offset: 0x001AB171
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x001ABF79 File Offset: 0x001AB179
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(value);
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x001ABF9C File Offset: 0x001AB19C
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.ArgumentNull_Buffer);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(SR.Argument_InvalidOffLen);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(buffer, index, count);
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001AC01C File Offset: 0x001AB21C
		[NullableContext(0)]
		public override void Write(ReadOnlySpan<char> buffer)
		{
			if (base.GetType() != typeof(StringWriter))
			{
				base.Write(buffer);
				return;
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(buffer);
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x001AC069 File Offset: 0x001AB269
		[NullableContext(2)]
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x001AC090 File Offset: 0x001AB290
		[NullableContext(2)]
		public override void Write(StringBuilder value)
		{
			if (base.GetType() != typeof(StringWriter))
			{
				base.Write(value);
				return;
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(value);
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001AC0E0 File Offset: 0x001AB2E0
		[NullableContext(0)]
		public override void WriteLine(ReadOnlySpan<char> buffer)
		{
			if (base.GetType() != typeof(StringWriter))
			{
				base.WriteLine(buffer);
				return;
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(buffer);
			this.WriteLine();
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x001AC134 File Offset: 0x001AB334
		[NullableContext(2)]
		public override void WriteLine(StringBuilder value)
		{
			if (base.GetType() != typeof(StringWriter))
			{
				base.WriteLine(value);
				return;
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(value);
			this.WriteLine();
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x001AC187 File Offset: 0x001AB387
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x001AC195 File Offset: 0x001AB395
		public override Task WriteAsync([Nullable(2)] string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x001AC1A3 File Offset: 0x001AB3A3
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x001AC1B3 File Offset: 0x001AB3B3
		[NullableContext(0)]
		[return: Nullable(1)]
		public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			this.Write(buffer.Span);
			return Task.CompletedTask;
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x001AC1D8 File Offset: 0x001AB3D8
		public override Task WriteAsync([Nullable(2)] StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StringWriter))
			{
				return base.WriteAsync(value, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(value);
			return Task.CompletedTask;
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x001AC23B File Offset: 0x001AB43B
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x001AC249 File Offset: 0x001AB449
		public override Task WriteLineAsync([Nullable(2)] string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x001AC258 File Offset: 0x001AB458
		public override Task WriteLineAsync([Nullable(2)] StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StringWriter))
			{
				return base.WriteLineAsync(value, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_WriterClosed);
			}
			this._sb.Append(value);
			this.WriteLine();
			return Task.CompletedTask;
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x001AC2C1 File Offset: 0x001AB4C1
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x001AC2D1 File Offset: 0x001AB4D1
		[NullableContext(0)]
		[return: Nullable(1)]
		public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			this.WriteLine(buffer.Span);
			return Task.CompletedTask;
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x001AC2F5 File Offset: 0x001AB4F5
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x001AC2FC File Offset: 0x001AB4FC
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x040018F7 RID: 6391
		private static volatile UnicodeEncoding s_encoding;

		// Token: 0x040018F8 RID: 6392
		private readonly StringBuilder _sb;

		// Token: 0x040018F9 RID: 6393
		private bool _isOpen;
	}
}
