using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200054B RID: 1355
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x0017D660 File Offset: 0x0017C860
		// (set) Token: 0x06004755 RID: 18261 RVA: 0x0017D668 File Offset: 0x0017C868
		public bool WrapNonExceptionThrows { get; set; }
	}
}
