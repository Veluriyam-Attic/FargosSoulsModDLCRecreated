using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005CD RID: 1485
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		// Token: 0x06004BF9 RID: 19449 RVA: 0x0018B5EC File Offset: 0x0018A7EC
		public AssemblyVersionAttribute(string version)
		{
			this.Version = version;
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x0018B5FB File Offset: 0x0018A7FB
		public string Version { get; }
	}
}
