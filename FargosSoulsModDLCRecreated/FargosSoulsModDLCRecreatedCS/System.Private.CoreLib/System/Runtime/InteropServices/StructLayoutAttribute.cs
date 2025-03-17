using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A0 RID: 1184
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x060044DF RID: 17631 RVA: 0x00179B36 File Offset: 0x00178D36
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this.Value = layoutKind;
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x00179B36 File Offset: 0x00178D36
		public StructLayoutAttribute(short layoutKind)
		{
			this.Value = layoutKind;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x00179B45 File Offset: 0x00178D45
		public LayoutKind Value { get; }

		// Token: 0x04000F67 RID: 3943
		public int Pack;

		// Token: 0x04000F68 RID: 3944
		public int Size;

		// Token: 0x04000F69 RID: 3945
		public CharSet CharSet;
	}
}
