using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x020003D9 RID: 985
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x0016A57E File Offset: 0x0016977E
		public string TargetedPatchBand { get; }

		// Token: 0x060031F4 RID: 12788 RVA: 0x0016A586 File Offset: 0x00169786
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.TargetedPatchBand = targetedPatchBand;
		}
	}
}
