using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005B9 RID: 1465
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		// Token: 0x06004BCE RID: 19406 RVA: 0x0018B0E9 File Offset: 0x0018A2E9
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.Configuration = configuration;
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x0018B0F8 File Offset: 0x0018A2F8
		public string Configuration { get; }
	}
}
