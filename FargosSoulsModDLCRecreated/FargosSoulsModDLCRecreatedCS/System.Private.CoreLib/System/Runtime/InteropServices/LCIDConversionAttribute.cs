using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000492 RID: 1170
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		// Token: 0x060044A0 RID: 17568 RVA: 0x001793CD File Offset: 0x001785CD
		public LCIDConversionAttribute(int lcid)
		{
			this.Value = lcid;
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x001793DC File Offset: 0x001785DC
		public int Value { get; }
	}
}
