using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A1 RID: 1441
	internal struct MetadataEnumResult
	{
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x00186561 File Offset: 0x00185761
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000B47 RID: 2887
		public unsafe int this[int index]
		{
			get
			{
				if (this.largeResult != null)
				{
					return this.largeResult[index];
				}
				fixed (int* ptr = &this.smallResult.FixedElementField)
				{
					int* ptr2 = ptr;
					return ptr2[index];
				}
			}
		}

		// Token: 0x04001268 RID: 4712
		private int[] largeResult;

		// Token: 0x04001269 RID: 4713
		private int length;

		// Token: 0x0400126A RID: 4714
		[FixedBuffer(typeof(int), 16)]
		private MetadataEnumResult.<smallResult>e__FixedBuffer smallResult;

		// Token: 0x020005A2 RID: 1442
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <smallResult>e__FixedBuffer
		{
			// Token: 0x0400126B RID: 4715
			public int FixedElementField;
		}
	}
}
