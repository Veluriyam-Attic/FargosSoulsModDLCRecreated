using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E2 RID: 994
	[NullableContext(1)]
	public interface ISerializable
	{
		// Token: 0x06003212 RID: 12818
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
