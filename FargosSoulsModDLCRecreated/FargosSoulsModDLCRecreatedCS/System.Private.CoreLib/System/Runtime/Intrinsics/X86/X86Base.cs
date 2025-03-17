using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200042A RID: 1066
	[Intrinsic]
	public abstract class X86Base
	{
		// Token: 0x06003D93 RID: 15763
		[DllImport("QCall")]
		private unsafe static extern void __cpuidex(int* cpuInfo, int functionId, int subFunctionId);

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x0017138C File Offset: 0x0017058C
		public static bool IsSupported
		{
			get
			{
				return X86Base.IsSupported;
			}
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x00171393 File Offset: 0x00170593
		internal static uint BitScanForward(uint value)
		{
			return X86Base.BitScanForward(value);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x0017139B File Offset: 0x0017059B
		internal static uint BitScanReverse(uint value)
		{
			return X86Base.BitScanReverse(value);
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x001713A4 File Offset: 0x001705A4
		[return: TupleElementNames(new string[]
		{
			"Eax",
			"Ebx",
			"Ecx",
			"Edx"
		})]
		public unsafe static ValueTuple<int, int, int, int> CpuId(int functionId, int subFunctionId)
		{
			int* ptr = stackalloc int[(UIntPtr)16];
			X86Base.__cpuidex(ptr, functionId, subFunctionId);
			return new ValueTuple<int, int, int, int>(*ptr, ptr[1], ptr[2], ptr[3]);
		}

		// Token: 0x0200042B RID: 1067
		[Intrinsic]
		public abstract class X64
		{
			// Token: 0x17000A20 RID: 2592
			// (get) Token: 0x06003D98 RID: 15768 RVA: 0x001713D8 File Offset: 0x001705D8
			public static bool IsSupported
			{
				get
				{
					return X86Base.X64.IsSupported;
				}
			}

			// Token: 0x06003D99 RID: 15769 RVA: 0x001713DF File Offset: 0x001705DF
			internal static ulong BitScanForward(ulong value)
			{
				return X86Base.X64.BitScanForward(value);
			}

			// Token: 0x06003D9A RID: 15770 RVA: 0x001713E7 File Offset: 0x001705E7
			internal static ulong BitScanReverse(ulong value)
			{
				return X86Base.X64.BitScanReverse(value);
			}
		}
	}
}
