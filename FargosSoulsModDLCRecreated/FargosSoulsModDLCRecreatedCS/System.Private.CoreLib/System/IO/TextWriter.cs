using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020006B6 RID: 1718
	[Nullable(0)]
	[NullableContext(1)]
	public abstract class TextWriter : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x06005732 RID: 22322 RVA: 0x001ACC2A File Offset: 0x001ABE2A
		protected TextWriter()
		{
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x001ACC48 File Offset: 0x001ABE48
		[NullableContext(2)]
		protected TextWriter(IFormatProvider formatProvider)
		{
			this._internalFormatProvider = formatProvider;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x001ACC6D File Offset: 0x001ABE6D
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				if (this._internalFormatProvider == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._internalFormatProvider;
			}
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x001A9ECD File Offset: 0x001A90CD
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x000AB30B File Offset: 0x000AA50B
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x001A9ECD File Offset: 0x001A90CD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x001ACC84 File Offset: 0x001ABE84
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				this.Dispose();
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception exception)
			{
				valueTask = ValueTask.FromException(exception);
			}
			return valueTask;
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void Flush()
		{
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600573A RID: 22330
		public abstract Encoding Encoding { get; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x001ACCC0 File Offset: 0x001ABEC0
		// (set) Token: 0x0600573C RID: 22332 RVA: 0x001ACCC8 File Offset: 0x001ABEC8
		public virtual string NewLine
		{
			get
			{
				return this.CoreNewLineStr;
			}
			[param: AllowNull]
			set
			{
				if (value == null)
				{
					value = "\r\n";
				}
				this.CoreNewLineStr = value;
				this.CoreNewLine = value.ToCharArray();
			}
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void Write(char value)
		{
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x001ACCE7 File Offset: 0x001ABEE7
		[NullableContext(2)]
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x001ACCF8 File Offset: 0x001ABEF8
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x001ACD6C File Offset: 0x001ABF6C
		[NullableContext(0)]
		public virtual void Write(ReadOnlySpan<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(new Span<char>(array));
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x001ACDC8 File Offset: 0x001ABFC8
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x001ACDDF File Offset: 0x001ABFDF
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001ACDF4 File Offset: 0x001ABFF4
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x001ACE09 File Offset: 0x001AC009
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x001ACE1E File Offset: 0x001AC01E
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x001ACE33 File Offset: 0x001AC033
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x001ACE48 File Offset: 0x001AC048
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x001ACE5D File Offset: 0x001AC05D
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x001ACE72 File Offset: 0x001AC072
		[NullableContext(2)]
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x001ACE84 File Offset: 0x001AC084
		[NullableContext(2)]
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x001ACEC0 File Offset: 0x001AC0C0
		[NullableContext(2)]
		public virtual void Write(StringBuilder value)
		{
			if (value != null)
			{
				foreach (ReadOnlyMemory<char> readOnlyMemory in value.GetChunks())
				{
					this.Write(readOnlyMemory.Span);
				}
			}
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x001ACEFF File Offset: 0x001AC0FF
		public virtual void Write(string format, [Nullable(2)] object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x001ACF14 File Offset: 0x001AC114
		[NullableContext(2)]
		public virtual void Write([Nullable(1)] string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001ACF2A File Offset: 0x001AC12A
		[NullableContext(2)]
		public virtual void Write([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001ACF42 File Offset: 0x001AC142
		public virtual void Write(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x001ACF57 File Offset: 0x001AC157
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x001ACF65 File Offset: 0x001AC165
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x001ACF74 File Offset: 0x001AC174
		[NullableContext(2)]
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x001ACF83 File Offset: 0x001AC183
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001ACF94 File Offset: 0x001AC194
		[NullableContext(0)]
		public virtual void WriteLine(ReadOnlySpan<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(new Span<char>(array));
				this.WriteLine(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x001ACFF0 File Offset: 0x001AC1F0
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x001ACFFF File Offset: 0x001AC1FF
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x001AD00E File Offset: 0x001AC20E
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x001AD01D File Offset: 0x001AC21D
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x001AD02C File Offset: 0x001AC22C
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x001AD03B File Offset: 0x001AC23B
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x001AD04A File Offset: 0x001AC24A
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x001AD059 File Offset: 0x001AC259
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001AD068 File Offset: 0x001AC268
		[NullableContext(2)]
		public virtual void WriteLine(string value)
		{
			if (value != null)
			{
				this.Write(value);
			}
			this.Write(this.CoreNewLineStr);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x001AD080 File Offset: 0x001AC280
		[NullableContext(2)]
		public virtual void WriteLine(StringBuilder value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x001AD090 File Offset: 0x001AC290
		[NullableContext(2)]
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x001AD0D1 File Offset: 0x001AC2D1
		public virtual void WriteLine(string format, [Nullable(2)] object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x001AD0E6 File Offset: 0x001AC2E6
		[NullableContext(2)]
		public virtual void WriteLine([Nullable(1)] string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x001AD0FC File Offset: 0x001AC2FC
		[NullableContext(2)]
		public virtual void WriteLine([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001AD114 File Offset: 0x001AC314
		public virtual void WriteLine(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x001AD12C File Offset: 0x001AC32C
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> state2 = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.Write(tuple.Item2);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x001AD178 File Offset: 0x001AC378
		public virtual Task WriteAsync([Nullable(2)] string value)
		{
			Tuple<TextWriter, string> state2 = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.Write(tuple.Item2);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x001AD1C2 File Offset: 0x001AC3C2
		public virtual Task WriteAsync([Nullable(2)] StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (value != null)
			{
				return this.<WriteAsync>g__WriteAsyncCore|60_0(value, cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x001AD1E5 File Offset: 0x001AC3E5
		public Task WriteAsync([Nullable(2)] char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x001AD1FC File Offset: 0x001AC3FC
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> state2 = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x001AD248 File Offset: 0x001AC448
		[NullableContext(0)]
		[return: Nullable(1)]
		public virtual Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			ArraySegment<char> arraySegment;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				return Task.Factory.StartNew(delegate(object state)
				{
					Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
					tuple.Item1.Write(tuple.Item2.Span);
				}, Tuple.Create<TextWriter, ReadOnlyMemory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			return this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x001AD2C4 File Offset: 0x001AC4C4
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> state2 = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x001AD310 File Offset: 0x001AC510
		public virtual Task WriteLineAsync([Nullable(2)] string value)
		{
			Tuple<TextWriter, string> state2 = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x001AD35A File Offset: 0x001AC55A
		public virtual Task WriteLineAsync([Nullable(2)] StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (value != null)
			{
				return this.<WriteLineAsync>g__WriteLineAsyncCore|66_0(value, cancellationToken);
			}
			return this.WriteAsync(this.CoreNewLine, cancellationToken);
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x001AD38A File Offset: 0x001AC58A
		public Task WriteLineAsync([Nullable(2)] char[] buffer)
		{
			if (buffer == null)
			{
				return this.WriteLineAsync();
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x001AD3A4 File Offset: 0x001AC5A4
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> state2 = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
			}, state2, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x001AD3F0 File Offset: 0x001AC5F0
		[NullableContext(0)]
		[return: Nullable(1)]
		public virtual Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			ArraySegment<char> arraySegment;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				return Task.Factory.StartNew(delegate(object state)
				{
					Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
					tuple.Item1.WriteLine(tuple.Item2.Span);
				}, Tuple.Create<TextWriter, ReadOnlyMemory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			return this.WriteLineAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x001AD46A File Offset: 0x001AC66A
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x001AD478 File Offset: 0x001AC678
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((TextWriter)state).Flush();
			}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x001AD4AF File Offset: 0x001AC6AF
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!(writer is TextWriter.SyncTextWriter))
			{
				return new TextWriter.SyncTextWriter(writer);
			}
			return writer;
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x001AD4EC File Offset: 0x001AC6EC
		[CompilerGenerated]
		private Task <WriteAsync>g__WriteAsyncCore|60_0(StringBuilder sb, CancellationToken ct)
		{
			TextWriter.<<WriteAsync>g__WriteAsyncCore|60_0>d <<WriteAsync>g__WriteAsyncCore|60_0>d;
			<<WriteAsync>g__WriteAsyncCore|60_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<WriteAsync>g__WriteAsyncCore|60_0>d.<>4__this = this;
			<<WriteAsync>g__WriteAsyncCore|60_0>d.sb = sb;
			<<WriteAsync>g__WriteAsyncCore|60_0>d.ct = ct;
			<<WriteAsync>g__WriteAsyncCore|60_0>d.<>1__state = -1;
			<<WriteAsync>g__WriteAsyncCore|60_0>d.<>t__builder.Start<TextWriter.<<WriteAsync>g__WriteAsyncCore|60_0>d>(ref <<WriteAsync>g__WriteAsyncCore|60_0>d);
			return <<WriteAsync>g__WriteAsyncCore|60_0>d.<>t__builder.Task;
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x001AD540 File Offset: 0x001AC740
		[CompilerGenerated]
		private Task <WriteLineAsync>g__WriteLineAsyncCore|66_0(StringBuilder sb, CancellationToken ct)
		{
			TextWriter.<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d <<WriteLineAsync>g__WriteLineAsyncCore|66_0>d;
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.<>4__this = this;
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.sb = sb;
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.ct = ct;
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.<>1__state = -1;
			<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.<>t__builder.Start<TextWriter.<<WriteLineAsync>g__WriteLineAsyncCore|66_0>d>(ref <<WriteLineAsync>g__WriteLineAsyncCore|66_0>d);
			return <<WriteLineAsync>g__WriteLineAsyncCore|66_0>d.<>t__builder.Task;
		}

		// Token: 0x0400190E RID: 6414
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x0400190F RID: 6415
		private static readonly char[] s_coreNewLine = "\r\n".ToCharArray();

		// Token: 0x04001910 RID: 6416
		protected char[] CoreNewLine = TextWriter.s_coreNewLine;

		// Token: 0x04001911 RID: 6417
		private string CoreNewLineStr = "\r\n";

		// Token: 0x04001912 RID: 6418
		private readonly IFormatProvider _internalFormatProvider;

		// Token: 0x020006B7 RID: 1719
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x06005776 RID: 22390 RVA: 0x001AD593 File Offset: 0x001AC793
			internal NullTextWriter() : base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17000E46 RID: 3654
			// (get) Token: 0x06005777 RID: 22391 RVA: 0x001A8FAA File Offset: 0x001A81AA
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06005778 RID: 22392 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06005779 RID: 22393 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Write(string value)
			{
			}

			// Token: 0x0600577A RID: 22394 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void WriteLine()
			{
			}

			// Token: 0x0600577B RID: 22395 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void WriteLine(string value)
			{
			}

			// Token: 0x0600577C RID: 22396 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void WriteLine(object value)
			{
			}

			// Token: 0x0600577D RID: 22397 RVA: 0x000AB30B File Offset: 0x000AA50B
			public override void Write(char value)
			{
			}
		}

		// Token: 0x020006B8 RID: 1720
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x0600577E RID: 22398 RVA: 0x001AD5A0 File Offset: 0x001AC7A0
			internal SyncTextWriter(TextWriter t) : base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17000E47 RID: 3655
			// (get) Token: 0x0600577F RID: 22399 RVA: 0x001AD5B5 File Offset: 0x001AC7B5
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17000E48 RID: 3656
			// (get) Token: 0x06005780 RID: 22400 RVA: 0x001AD5C2 File Offset: 0x001AC7C2
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17000E49 RID: 3657
			// (get) Token: 0x06005781 RID: 22401 RVA: 0x001AD5CF File Offset: 0x001AC7CF
			// (set) Token: 0x06005782 RID: 22402 RVA: 0x001AD5DC File Offset: 0x001AC7DC
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				[param: AllowNull]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06005783 RID: 22403 RVA: 0x001AD5EA File Offset: 0x001AC7EA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06005784 RID: 22404 RVA: 0x001AD5F7 File Offset: 0x001AC7F7
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06005785 RID: 22405 RVA: 0x001AD607 File Offset: 0x001AC807
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06005786 RID: 22406 RVA: 0x001AD614 File Offset: 0x001AC814
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005787 RID: 22407 RVA: 0x001AD622 File Offset: 0x001AC822
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06005788 RID: 22408 RVA: 0x001AD630 File Offset: 0x001AC830
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06005789 RID: 22409 RVA: 0x001AD640 File Offset: 0x001AC840
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ReadOnlySpan<char> buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x0600578A RID: 22410 RVA: 0x001AD64E File Offset: 0x001AC84E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600578B RID: 22411 RVA: 0x001AD65C File Offset: 0x001AC85C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600578C RID: 22412 RVA: 0x001AD66A File Offset: 0x001AC86A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600578D RID: 22413 RVA: 0x001AD678 File Offset: 0x001AC878
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600578E RID: 22414 RVA: 0x001AD686 File Offset: 0x001AC886
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600578F RID: 22415 RVA: 0x001AD694 File Offset: 0x001AC894
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005790 RID: 22416 RVA: 0x001AD6A2 File Offset: 0x001AC8A2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005791 RID: 22417 RVA: 0x001AD6B0 File Offset: 0x001AC8B0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005792 RID: 22418 RVA: 0x001AD6BE File Offset: 0x001AC8BE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005793 RID: 22419 RVA: 0x001AD6CC File Offset: 0x001AC8CC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(StringBuilder value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005794 RID: 22420 RVA: 0x001AD6DA File Offset: 0x001AC8DA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06005795 RID: 22421 RVA: 0x001AD6E8 File Offset: 0x001AC8E8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06005796 RID: 22422 RVA: 0x001AD6F7 File Offset: 0x001AC8F7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06005797 RID: 22423 RVA: 0x001AD707 File Offset: 0x001AC907
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06005798 RID: 22424 RVA: 0x001AD719 File Offset: 0x001AC919
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x06005799 RID: 22425 RVA: 0x001AD728 File Offset: 0x001AC928
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x0600579A RID: 22426 RVA: 0x001AD735 File Offset: 0x001AC935
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x0600579B RID: 22427 RVA: 0x001AD743 File Offset: 0x001AC943
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x0600579C RID: 22428 RVA: 0x001AD751 File Offset: 0x001AC951
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x0600579D RID: 22429 RVA: 0x001AD75F File Offset: 0x001AC95F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x0600579E RID: 22430 RVA: 0x001AD76F File Offset: 0x001AC96F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ReadOnlySpan<char> buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x0600579F RID: 22431 RVA: 0x001AD77D File Offset: 0x001AC97D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A0 RID: 22432 RVA: 0x001AD78B File Offset: 0x001AC98B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A1 RID: 22433 RVA: 0x001AD799 File Offset: 0x001AC999
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A2 RID: 22434 RVA: 0x001AD7A7 File Offset: 0x001AC9A7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A3 RID: 22435 RVA: 0x001AD7B5 File Offset: 0x001AC9B5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A4 RID: 22436 RVA: 0x001AD7C3 File Offset: 0x001AC9C3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A5 RID: 22437 RVA: 0x001AD7D1 File Offset: 0x001AC9D1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A6 RID: 22438 RVA: 0x001AD7DF File Offset: 0x001AC9DF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A7 RID: 22439 RVA: 0x001AD7ED File Offset: 0x001AC9ED
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(StringBuilder value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A8 RID: 22440 RVA: 0x001AD7FB File Offset: 0x001AC9FB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060057A9 RID: 22441 RVA: 0x001AD809 File Offset: 0x001ACA09
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x060057AA RID: 22442 RVA: 0x001AD818 File Offset: 0x001ACA18
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x060057AB RID: 22443 RVA: 0x001AD828 File Offset: 0x001ACA28
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x060057AC RID: 22444 RVA: 0x001AD83A File Offset: 0x001ACA3A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x060057AD RID: 22445 RVA: 0x001AD84C File Offset: 0x001ACA4C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override ValueTask DisposeAsync()
			{
				base.Dispose();
				return default(ValueTask);
			}

			// Token: 0x060057AE RID: 22446 RVA: 0x001AC187 File Offset: 0x001AB387
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057AF RID: 22447 RVA: 0x001AC195 File Offset: 0x001AB395
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057B0 RID: 22448 RVA: 0x001AD868 File Offset: 0x001ACA68
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled(cancellationToken);
				}
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057B1 RID: 22449 RVA: 0x001AC1A3 File Offset: 0x001AB3A3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x060057B2 RID: 22450 RVA: 0x001AC1B3 File Offset: 0x001AB3B3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled(cancellationToken);
				}
				this.Write(buffer.Span);
				return Task.CompletedTask;
			}

			// Token: 0x060057B3 RID: 22451 RVA: 0x001AC2D1 File Offset: 0x001AB4D1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled(cancellationToken);
				}
				this.WriteLine(buffer.Span);
				return Task.CompletedTask;
			}

			// Token: 0x060057B4 RID: 22452 RVA: 0x001AC23B File Offset: 0x001AB43B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057B5 RID: 22453 RVA: 0x001AD886 File Offset: 0x001ACA86
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync()
			{
				this.WriteLine();
				return Task.CompletedTask;
			}

			// Token: 0x060057B6 RID: 22454 RVA: 0x001AC249 File Offset: 0x001AB449
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057B7 RID: 22455 RVA: 0x001AD893 File Offset: 0x001ACA93
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(StringBuilder value, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled(cancellationToken);
				}
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x060057B8 RID: 22456 RVA: 0x001AC2C1 File Offset: 0x001AB4C1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x060057B9 RID: 22457 RVA: 0x001AD8B1 File Offset: 0x001ACAB1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x04001913 RID: 6419
			private readonly TextWriter _out;
		}
	}
}
