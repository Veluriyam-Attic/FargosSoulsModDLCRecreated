using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200016F RID: 367
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x000E787C File Offset: 0x000E6A7C
		public PlatformNotSupportedException() : base(SR.Arg_PlatformNotSupported)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000E7894 File Offset: 0x000E6A94
		public PlatformNotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000E78A8 File Offset: 0x000E6AA8
		public PlatformNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000E78BD File Offset: 0x000E6ABD
		[NullableContext(1)]
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
