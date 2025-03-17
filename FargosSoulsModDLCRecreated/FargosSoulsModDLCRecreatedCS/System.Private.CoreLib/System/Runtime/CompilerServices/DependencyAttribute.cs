using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200052C RID: 1324
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class DependencyAttribute : Attribute
	{
		// Token: 0x06004724 RID: 18212 RVA: 0x0017D544 File Offset: 0x0017C744
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.DependentAssembly = dependentAssemblyArgument;
			this.LoadHint = loadHintArgument;
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0017D55A File Offset: 0x0017C75A
		public string DependentAssembly { get; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x0017D562 File Offset: 0x0017C762
		public LoadHint LoadHint { get; }
	}
}
