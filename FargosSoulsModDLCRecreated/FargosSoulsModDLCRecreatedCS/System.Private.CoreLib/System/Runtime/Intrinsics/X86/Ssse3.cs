using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000449 RID: 1097
	[CLSCompliant(false)]
	[Intrinsic]
	public abstract class Ssse3 : Sse3
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x00175304 File Offset: 0x00174504
		public new static bool IsSupported
		{
			get
			{
				return Ssse3.IsSupported;
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0017530B File Offset: 0x0017450B
		public static Vector128<byte> Abs(Vector128<sbyte> value)
		{
			return Ssse3.Abs(value);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x00175313 File Offset: 0x00174513
		public static Vector128<ushort> Abs(Vector128<short> value)
		{
			return Ssse3.Abs(value);
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x0017531B File Offset: 0x0017451B
		public static Vector128<uint> Abs(Vector128<int> value)
		{
			return Ssse3.Abs(value);
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x00175323 File Offset: 0x00174523
		public static Vector128<sbyte> AlignRight(Vector128<sbyte> left, Vector128<sbyte> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0017532D File Offset: 0x0017452D
		public static Vector128<byte> AlignRight(Vector128<byte> left, Vector128<byte> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00175337 File Offset: 0x00174537
		public static Vector128<short> AlignRight(Vector128<short> left, Vector128<short> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x00175341 File Offset: 0x00174541
		public static Vector128<ushort> AlignRight(Vector128<ushort> left, Vector128<ushort> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0017534B File Offset: 0x0017454B
		public static Vector128<int> AlignRight(Vector128<int> left, Vector128<int> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x00175355 File Offset: 0x00174555
		public static Vector128<uint> AlignRight(Vector128<uint> left, Vector128<uint> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0017535F File Offset: 0x0017455F
		public static Vector128<long> AlignRight(Vector128<long> left, Vector128<long> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x00175369 File Offset: 0x00174569
		public static Vector128<ulong> AlignRight(Vector128<ulong> left, Vector128<ulong> right, byte mask)
		{
			return Ssse3.AlignRight(left, right, mask);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x00175373 File Offset: 0x00174573
		public static Vector128<short> HorizontalAdd(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.HorizontalAdd(left, right);
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0017537C File Offset: 0x0017457C
		public static Vector128<int> HorizontalAdd(Vector128<int> left, Vector128<int> right)
		{
			return Ssse3.HorizontalAdd(left, right);
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x00175385 File Offset: 0x00174585
		public static Vector128<short> HorizontalAddSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.HorizontalAddSaturate(left, right);
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0017538E File Offset: 0x0017458E
		public static Vector128<short> HorizontalSubtract(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.HorizontalSubtract(left, right);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x00175397 File Offset: 0x00174597
		public static Vector128<int> HorizontalSubtract(Vector128<int> left, Vector128<int> right)
		{
			return Ssse3.HorizontalSubtract(left, right);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x001753A0 File Offset: 0x001745A0
		public static Vector128<short> HorizontalSubtractSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.HorizontalSubtractSaturate(left, right);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x001753A9 File Offset: 0x001745A9
		public static Vector128<short> MultiplyAddAdjacent(Vector128<byte> left, Vector128<sbyte> right)
		{
			return Ssse3.MultiplyAddAdjacent(left, right);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x001753B2 File Offset: 0x001745B2
		public static Vector128<short> MultiplyHighRoundScale(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.MultiplyHighRoundScale(left, right);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x001753BB File Offset: 0x001745BB
		public static Vector128<sbyte> Shuffle(Vector128<sbyte> value, Vector128<sbyte> mask)
		{
			return Ssse3.Shuffle(value, mask);
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x001753C4 File Offset: 0x001745C4
		public static Vector128<byte> Shuffle(Vector128<byte> value, Vector128<byte> mask)
		{
			return Ssse3.Shuffle(value, mask);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x001753CD File Offset: 0x001745CD
		public static Vector128<sbyte> Sign(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Ssse3.Sign(left, right);
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x001753D6 File Offset: 0x001745D6
		public static Vector128<short> Sign(Vector128<short> left, Vector128<short> right)
		{
			return Ssse3.Sign(left, right);
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x001753DF File Offset: 0x001745DF
		public static Vector128<int> Sign(Vector128<int> left, Vector128<int> right)
		{
			return Ssse3.Sign(left, right);
		}

		// Token: 0x0200044A RID: 1098
		[Intrinsic]
		public new abstract class X64 : Sse3.X64
		{
			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x060042C4 RID: 17092 RVA: 0x001753E8 File Offset: 0x001745E8
			public new static bool IsSupported
			{
				get
				{
					return Ssse3.X64.IsSupported;
				}
			}
		}
	}
}
