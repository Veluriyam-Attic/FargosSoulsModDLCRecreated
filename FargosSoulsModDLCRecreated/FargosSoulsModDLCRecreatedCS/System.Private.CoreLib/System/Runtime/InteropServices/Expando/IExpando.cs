using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	// Token: 0x020004B6 RID: 1206
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x06004538 RID: 17720
		FieldInfo AddField(string name);

		// Token: 0x06004539 RID: 17721
		void RemoveMember(MemberInfo m);
	}
}
