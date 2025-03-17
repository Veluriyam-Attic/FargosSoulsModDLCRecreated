using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005BC RID: 1468
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		// Token: 0x06004BD2 RID: 19410 RVA: 0x0018B117 File Offset: 0x0018A317
		public AssemblyCultureAttribute(string culture)
		{
			this.Culture = culture;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x0018B126 File Offset: 0x0018A326
		public string Culture { get; }
	}
}
