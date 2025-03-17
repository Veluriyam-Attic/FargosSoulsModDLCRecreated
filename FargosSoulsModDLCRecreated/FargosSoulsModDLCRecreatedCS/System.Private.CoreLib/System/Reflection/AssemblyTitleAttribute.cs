using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005CB RID: 1483
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		// Token: 0x06004BF5 RID: 19445 RVA: 0x0018B5BE File Offset: 0x0018A7BE
		public AssemblyTitleAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0018B5CD File Offset: 0x0018A7CD
		public string Title { get; }
	}
}
