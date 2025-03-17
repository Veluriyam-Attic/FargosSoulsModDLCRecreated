using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000533 RID: 1331
	public static class FormattableStringFactory
	{
		// Token: 0x0600472F RID: 18223 RVA: 0x0017D590 File Offset: 0x0017C790
		[NullableContext(1)]
		public static FormattableString Create(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x02000534 RID: 1332
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06004730 RID: 18224 RVA: 0x0017D5B5 File Offset: 0x0017C7B5
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06004731 RID: 18225 RVA: 0x0017D5CB File Offset: 0x0017C7CB
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06004732 RID: 18226 RVA: 0x0017D5D3 File Offset: 0x0017C7D3
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06004733 RID: 18227 RVA: 0x0017D5DB File Offset: 0x0017C7DB
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x06004734 RID: 18228 RVA: 0x0017D5E5 File Offset: 0x0017C7E5
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x06004735 RID: 18229 RVA: 0x0017D5EF File Offset: 0x0017C7EF
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x0400111A RID: 4378
			private readonly string _format;

			// Token: 0x0400111B RID: 4379
			private readonly object[] _arguments;
		}
	}
}
