using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000295 RID: 661
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x06002760 RID: 10080 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private CompressedStack()
		{
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000B3617 File Offset: 0x000B2817
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00144D11 File Offset: 0x00143F11
		public static CompressedStack Capture()
		{
			return CompressedStack.GetCompressedStack();
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000AC098 File Offset: 0x000AB298
		public CompressedStack CreateCopy()
		{
			return this;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00144D18 File Offset: 0x00143F18
		public static CompressedStack GetCompressedStack()
		{
			return new CompressedStack();
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00144D1F File Offset: 0x00143F1F
		public static void Run(CompressedStack compressedStack, ContextCallback callback, [Nullable(2)] object state)
		{
			if (compressedStack == null)
			{
				throw new ArgumentNullException("compressedStack");
			}
			callback(state);
		}
	}
}
