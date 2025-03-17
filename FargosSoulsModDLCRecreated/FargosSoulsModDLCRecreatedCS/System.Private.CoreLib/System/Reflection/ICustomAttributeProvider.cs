using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005DC RID: 1500
	[NullableContext(1)]
	public interface ICustomAttributeProvider
	{
		// Token: 0x06004C58 RID: 19544
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004C59 RID: 19545
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004C5A RID: 19546
		bool IsDefined(Type attributeType, bool inherit);
	}
}
