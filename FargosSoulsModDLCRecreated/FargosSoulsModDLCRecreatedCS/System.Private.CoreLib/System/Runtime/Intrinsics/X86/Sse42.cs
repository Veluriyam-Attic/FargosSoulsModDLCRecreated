using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000447 RID: 1095
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Sse42 : Sse41
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x001752C9 File Offset: 0x001744C9
		public new static bool IsSupported
		{
			get
			{
				return Sse42.IsSupported;
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x001752D0 File Offset: 0x001744D0
		public static Vector128<long> CompareGreaterThan(Vector128<long> left, Vector128<long> right)
		{
			return Sse42.CompareGreaterThan(left, right);
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x001752D9 File Offset: 0x001744D9
		public static uint Crc32(uint crc, byte data)
		{
			return Sse42.Crc32(crc, data);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x001752E2 File Offset: 0x001744E2
		public static uint Crc32(uint crc, ushort data)
		{
			return Sse42.Crc32(crc, data);
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x001752EB File Offset: 0x001744EB
		public static uint Crc32(uint crc, uint data)
		{
			return Sse42.Crc32(crc, data);
		}

		// Token: 0x02000448 RID: 1096
		[Intrinsic]
		public new abstract class X64 : Sse41.X64
		{
			// Token: 0x17000A3C RID: 2620
			// (get) Token: 0x060042A9 RID: 17065 RVA: 0x001752F4 File Offset: 0x001744F4
			public new static bool IsSupported
			{
				get
				{
					return Sse42.X64.IsSupported;
				}
			}

			// Token: 0x060042AA RID: 17066 RVA: 0x001752FB File Offset: 0x001744FB
			public static ulong Crc32(ulong crc, ulong data)
			{
				return Sse42.X64.Crc32(crc, data);
			}
		}
	}
}
