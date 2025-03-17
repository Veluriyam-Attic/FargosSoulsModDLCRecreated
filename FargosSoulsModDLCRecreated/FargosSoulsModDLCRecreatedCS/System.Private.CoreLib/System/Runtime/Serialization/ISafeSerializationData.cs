using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E1 RID: 993
	[NullableContext(1)]
	public interface ISafeSerializationData
	{
		// Token: 0x06003211 RID: 12817
		void CompleteDeserialization(object deserialized);
	}
}
