using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003DE RID: 990
	[NullableContext(2)]
	public interface IDeserializationCallback
	{
		// Token: 0x060031FE RID: 12798
		void OnDeserialization(object sender);
	}
}
