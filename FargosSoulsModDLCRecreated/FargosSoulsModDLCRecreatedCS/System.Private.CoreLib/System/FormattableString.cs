using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class FormattableString : IFormattable
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000DCB RID: 3531
		public abstract string Format { get; }

		// Token: 0x06000DCC RID: 3532
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public abstract object[] GetArguments();

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000DCD RID: 3533
		public abstract int ArgumentCount { get; }

		// Token: 0x06000DCE RID: 3534
		[NullableContext(2)]
		public abstract object GetArgument(int index);

		// Token: 0x06000DCF RID: 3535
		public abstract string ToString([Nullable(2)] IFormatProvider formatProvider);

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000D00F9 File Offset: 0x000CF2F9
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000D0102 File Offset: 0x000CF302
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000D011D File Offset: 0x000CF31D
		public static string CurrentCulture(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000D0138 File Offset: 0x000CF338
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
	}
}
