using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200069A RID: 1690
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class IOException : SystemException
	{
		// Token: 0x060055C2 RID: 21954 RVA: 0x001A4C53 File Offset: 0x001A3E53
		public IOException() : base(SR.Arg_IOException)
		{
			base.HResult = -2146232800;
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001A4C6B File Offset: 0x001A3E6B
		public IOException(string message) : base(message)
		{
			base.HResult = -2146232800;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x000DB07D File Offset: 0x000DA27D
		public IOException(string message, int hresult) : base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x001A4C7F File Offset: 0x001A3E7F
		public IOException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232800;
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected IOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
