using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004F8 RID: 1272
	[StructLayout(LayoutKind.Explicit)]
	internal struct MethodTable
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x0017ACCD File Offset: 0x00179ECD
		public bool HasComponentSize
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x0017ACDE File Offset: 0x00179EDE
		public bool ContainsGCPointers
		{
			get
			{
				return (this.Flags & 16777216U) > 0U;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x0017ACEF File Offset: 0x00179EEF
		public bool NonTrivialInterfaceCast
		{
			get
			{
				return (this.Flags & 1080557568U) > 0U;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x0017AD00 File Offset: 0x00179F00
		public bool HasTypeEquivalence
		{
			get
			{
				return (this.Flags & 16384U) > 0U;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x0017AD11 File Offset: 0x00179F11
		public bool IsMultiDimensionalArray
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.BaseSize > (uint)(3 * sizeof(IntPtr));
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x0017AD23 File Offset: 0x00179F23
		public int MultiDimensionalArrayRank
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)((this.BaseSize - (uint)(3 * sizeof(IntPtr))) / 8U);
			}
		}

		// Token: 0x040010C1 RID: 4289
		[FieldOffset(0)]
		public ushort ComponentSize;

		// Token: 0x040010C2 RID: 4290
		[FieldOffset(0)]
		private uint Flags;

		// Token: 0x040010C3 RID: 4291
		[FieldOffset(4)]
		public uint BaseSize;

		// Token: 0x040010C4 RID: 4292
		[FieldOffset(14)]
		public ushort InterfaceCount;

		// Token: 0x040010C5 RID: 4293
		[FieldOffset(16)]
		public unsafe MethodTable* ParentMethodTable;

		// Token: 0x040010C6 RID: 4294
		[FieldOffset(48)]
		public unsafe void* ElementType;

		// Token: 0x040010C7 RID: 4295
		[FieldOffset(56)]
		public unsafe MethodTable** InterfaceMap;
	}
}
