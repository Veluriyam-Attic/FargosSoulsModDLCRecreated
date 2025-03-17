using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000481 RID: 1153
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ErrorWrapper
	{
		// Token: 0x06004475 RID: 17525 RVA: 0x001791AD File Offset: 0x001783AD
		public ErrorWrapper(int errorCode)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001791BC File Offset: 0x001783BC
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException(SR.Arg_MustBeInt32, "errorCode");
			}
			this.ErrorCode = (int)errorCode;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x001791E8 File Offset: 0x001783E8
		public ErrorWrapper(Exception e)
		{
			this.ErrorCode = Marshal.GetHRForException(e);
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x001791FC File Offset: 0x001783FC
		public int ErrorCode { get; }
	}
}
