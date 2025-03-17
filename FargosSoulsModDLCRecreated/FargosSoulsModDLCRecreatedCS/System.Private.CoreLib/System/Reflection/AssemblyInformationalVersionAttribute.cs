using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C2 RID: 1474
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		// Token: 0x06004BE1 RID: 19425 RVA: 0x0018B1B0 File Offset: 0x0018A3B0
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.InformationalVersion = informationalVersion;
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x0018B1BF File Offset: 0x0018A3BF
		public string InformationalVersion { get; }
	}
}
