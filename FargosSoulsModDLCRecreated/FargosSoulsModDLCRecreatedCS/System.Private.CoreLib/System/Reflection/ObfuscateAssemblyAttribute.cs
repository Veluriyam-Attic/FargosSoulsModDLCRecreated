using System;

namespace System.Reflection
{
	// Token: 0x020005EF RID: 1519
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		// Token: 0x06004CD0 RID: 19664 RVA: 0x0018BF4B File Offset: 0x0018B14B
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.AssemblyIsPrivate = assemblyIsPrivate;
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0018BF61 File Offset: 0x0018B161
		public bool AssemblyIsPrivate { get; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x0018BF69 File Offset: 0x0018B169
		// (set) Token: 0x06004CD3 RID: 19667 RVA: 0x0018BF71 File Offset: 0x0018B171
		public bool StripAfterObfuscation { get; set; } = true;
	}
}
