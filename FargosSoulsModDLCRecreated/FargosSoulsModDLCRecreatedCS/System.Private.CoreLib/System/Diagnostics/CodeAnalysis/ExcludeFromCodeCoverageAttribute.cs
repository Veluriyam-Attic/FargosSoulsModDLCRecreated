using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006FE RID: 1790
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class ExcludeFromCodeCoverageAttribute : Attribute
	{
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x001B1CD9 File Offset: 0x001B0ED9
		// (set) Token: 0x0600596E RID: 22894 RVA: 0x001B1CE1 File Offset: 0x001B0EE1
		public string Justification { get; set; }
	}
}
