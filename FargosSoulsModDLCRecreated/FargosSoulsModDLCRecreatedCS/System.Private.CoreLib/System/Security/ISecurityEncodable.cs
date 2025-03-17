using System;
using System.Runtime.CompilerServices;

namespace System.Security
{
	// Token: 0x020003B5 RID: 949
	[NullableContext(1)]
	public interface ISecurityEncodable
	{
		// Token: 0x06003111 RID: 12561
		void FromXml(SecurityElement e);

		// Token: 0x06003112 RID: 12562
		[NullableContext(2)]
		SecurityElement ToXml();
	}
}
