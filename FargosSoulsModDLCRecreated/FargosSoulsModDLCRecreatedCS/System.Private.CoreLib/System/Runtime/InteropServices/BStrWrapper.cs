using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000467 RID: 1127
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class BStrWrapper
	{
		// Token: 0x0600443E RID: 17470 RVA: 0x00178D27 File Offset: 0x00177F27
		public BStrWrapper(string value)
		{
			this.WrappedObject = value;
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00178D36 File Offset: 0x00177F36
		public BStrWrapper(object value)
		{
			this.WrappedObject = (string)value;
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x00178D4A File Offset: 0x00177F4A
		public string WrappedObject { get; }
	}
}
