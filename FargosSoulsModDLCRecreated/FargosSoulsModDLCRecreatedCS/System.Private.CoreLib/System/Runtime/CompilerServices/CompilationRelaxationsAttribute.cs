using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000517 RID: 1303
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x060046CC RID: 18124 RVA: 0x0017C225 File Offset: 0x0017B425
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.CompilationRelaxations = relaxations;
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x0017C225 File Offset: 0x0017B425
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.CompilationRelaxations = relaxations;
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x0017C234 File Offset: 0x0017B434
		public int CompilationRelaxations { get; }
	}
}
