using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000443 RID: 1091
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Sse3 : Sse2
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x00174D35 File Offset: 0x00173F35
		public new static bool IsSupported
		{
			get
			{
				return Sse3.IsSupported;
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00174D3C File Offset: 0x00173F3C
		public static Vector128<float> AddSubtract(Vector128<float> left, Vector128<float> right)
		{
			return Sse3.AddSubtract(left, right);
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x00174D45 File Offset: 0x00173F45
		public static Vector128<double> AddSubtract(Vector128<double> left, Vector128<double> right)
		{
			return Sse3.AddSubtract(left, right);
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x00174D4E File Offset: 0x00173F4E
		public static Vector128<float> HorizontalAdd(Vector128<float> left, Vector128<float> right)
		{
			return Sse3.HorizontalAdd(left, right);
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x00174D57 File Offset: 0x00173F57
		public static Vector128<double> HorizontalAdd(Vector128<double> left, Vector128<double> right)
		{
			return Sse3.HorizontalAdd(left, right);
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x00174D60 File Offset: 0x00173F60
		public static Vector128<float> HorizontalSubtract(Vector128<float> left, Vector128<float> right)
		{
			return Sse3.HorizontalSubtract(left, right);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x00174D69 File Offset: 0x00173F69
		public static Vector128<double> HorizontalSubtract(Vector128<double> left, Vector128<double> right)
		{
			return Sse3.HorizontalSubtract(left, right);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x00174D72 File Offset: 0x00173F72
		public unsafe static Vector128<double> LoadAndDuplicateToVector128(double* address)
		{
			return Sse3.LoadAndDuplicateToVector128(address);
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x00174D7A File Offset: 0x00173F7A
		public unsafe static Vector128<sbyte> LoadDquVector128(sbyte* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x00174D82 File Offset: 0x00173F82
		public unsafe static Vector128<byte> LoadDquVector128(byte* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x00174D8A File Offset: 0x00173F8A
		public unsafe static Vector128<short> LoadDquVector128(short* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x00174D92 File Offset: 0x00173F92
		public unsafe static Vector128<ushort> LoadDquVector128(ushort* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x00174D9A File Offset: 0x00173F9A
		public unsafe static Vector128<int> LoadDquVector128(int* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x00174DA2 File Offset: 0x00173FA2
		public unsafe static Vector128<uint> LoadDquVector128(uint* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x00174DAA File Offset: 0x00173FAA
		public unsafe static Vector128<long> LoadDquVector128(long* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x00174DB2 File Offset: 0x00173FB2
		public unsafe static Vector128<ulong> LoadDquVector128(ulong* address)
		{
			return Sse3.LoadDquVector128(address);
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x00174DBA File Offset: 0x00173FBA
		public static Vector128<double> MoveAndDuplicate(Vector128<double> source)
		{
			return Sse3.MoveAndDuplicate(source);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00174DC2 File Offset: 0x00173FC2
		public static Vector128<float> MoveHighAndDuplicate(Vector128<float> source)
		{
			return Sse3.MoveHighAndDuplicate(source);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x00174DCA File Offset: 0x00173FCA
		public static Vector128<float> MoveLowAndDuplicate(Vector128<float> source)
		{
			return Sse3.MoveLowAndDuplicate(source);
		}

		// Token: 0x02000444 RID: 1092
		[Intrinsic]
		public new abstract class X64 : Sse2.X64
		{
			// Token: 0x17000A38 RID: 2616
			// (get) Token: 0x06004212 RID: 16914 RVA: 0x00174DD2 File Offset: 0x00173FD2
			public new static bool IsSupported
			{
				get
				{
					return Sse3.X64.IsSupported;
				}
			}
		}
	}
}
