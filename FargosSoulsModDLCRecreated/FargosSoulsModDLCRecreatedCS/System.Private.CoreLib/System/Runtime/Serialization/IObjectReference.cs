using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E0 RID: 992
	[NullableContext(1)]
	public interface IObjectReference
	{
		// Token: 0x06003210 RID: 12816
		object GetRealObject(StreamingContext context);
	}
}
