using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005EB RID: 1515
	public sealed class Missing : ISerializable
	{
		// Token: 0x06004C93 RID: 19603 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private Missing()
		{
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x000B3617 File Offset: 0x000B2817
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0400139C RID: 5020
		[Nullable(1)]
		public static readonly Missing Value = new Missing();
	}
}
