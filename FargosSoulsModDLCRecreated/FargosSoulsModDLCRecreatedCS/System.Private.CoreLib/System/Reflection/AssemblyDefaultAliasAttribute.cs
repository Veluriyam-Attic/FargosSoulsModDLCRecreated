using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005BD RID: 1469
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		// Token: 0x06004BD4 RID: 19412 RVA: 0x0018B12E File Offset: 0x0018A32E
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.DefaultAlias = defaultAlias;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x0018B13D File Offset: 0x0018A33D
		public string DefaultAlias { get; }
	}
}
