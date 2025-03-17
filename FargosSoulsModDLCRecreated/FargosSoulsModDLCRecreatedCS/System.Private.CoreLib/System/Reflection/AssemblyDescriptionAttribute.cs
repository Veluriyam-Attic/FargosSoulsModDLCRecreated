using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005BF RID: 1471
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		// Token: 0x06004BD8 RID: 19416 RVA: 0x0018B15C File Offset: 0x0018A35C
		public AssemblyDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x0018B16B File Offset: 0x0018A36B
		public string Description { get; }
	}
}
