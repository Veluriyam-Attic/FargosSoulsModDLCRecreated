using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200053B RID: 1339
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x06004740 RID: 18240 RVA: 0x0017D603 File Offset: 0x0017C803
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this.AssemblyName = assemblyName;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x0017D619 File Offset: 0x0017C819
		public string AssemblyName { get; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x0017D621 File Offset: 0x0017C821
		// (set) Token: 0x06004743 RID: 18243 RVA: 0x0017D629 File Offset: 0x0017C829
		public bool AllInternalsVisible { get; set; } = true;
	}
}
