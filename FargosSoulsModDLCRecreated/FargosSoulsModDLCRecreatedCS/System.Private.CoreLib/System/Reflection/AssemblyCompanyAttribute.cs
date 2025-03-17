using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005B8 RID: 1464
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		// Token: 0x06004BCC RID: 19404 RVA: 0x0018B0D2 File Offset: 0x0018A2D2
		public AssemblyCompanyAttribute(string company)
		{
			this.Company = company;
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0018B0E1 File Offset: 0x0018A2E1
		public string Company { get; }
	}
}
