using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005D5 RID: 1493
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class DefaultMemberAttribute : Attribute
	{
		// Token: 0x06004C36 RID: 19510 RVA: 0x0018B9C3 File Offset: 0x0018ABC3
		public DefaultMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0018B9D2 File Offset: 0x0018ABD2
		public string MemberName { get; }
	}
}
