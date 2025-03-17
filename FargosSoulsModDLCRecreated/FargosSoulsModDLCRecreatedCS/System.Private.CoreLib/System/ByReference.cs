using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000D7 RID: 215
	[NonVersionable]
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal readonly ref struct ByReference<T>
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		public ByReference(ref T value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000C9DFC File Offset: 0x000C8FFC
		public ref T Value
		{
			[Intrinsic]
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x040002A5 RID: 677
		private readonly IntPtr _value;
	}
}
