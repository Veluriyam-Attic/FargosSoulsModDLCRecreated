using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000437 RID: 1079
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Fma : Avx
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x00173DB3 File Offset: 0x00172FB3
		public new static bool IsSupported
		{
			get
			{
				return Fma.IsSupported;
			}
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x00173DBA File Offset: 0x00172FBA
		public static Vector128<float> MultiplyAdd(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplyAdd(a, b, c);
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x00173DC4 File Offset: 0x00172FC4
		public static Vector128<double> MultiplyAdd(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplyAdd(a, b, c);
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x00173DCE File Offset: 0x00172FCE
		public static Vector256<float> MultiplyAdd(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplyAdd(a, b, c);
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x00173DD8 File Offset: 0x00172FD8
		public static Vector256<double> MultiplyAdd(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplyAdd(a, b, c);
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x00173DE2 File Offset: 0x00172FE2
		public static Vector128<float> MultiplyAddScalar(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplyAddScalar(a, b, c);
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x00173DEC File Offset: 0x00172FEC
		public static Vector128<double> MultiplyAddScalar(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplyAddScalar(a, b, c);
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x00173DF6 File Offset: 0x00172FF6
		public static Vector128<float> MultiplyAddSubtract(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplyAddSubtract(a, b, c);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x00173E00 File Offset: 0x00173000
		public static Vector128<double> MultiplyAddSubtract(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplyAddSubtract(a, b, c);
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x00173E0A File Offset: 0x0017300A
		public static Vector256<float> MultiplyAddSubtract(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplyAddSubtract(a, b, c);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x00173E14 File Offset: 0x00173014
		public static Vector256<double> MultiplyAddSubtract(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplyAddSubtract(a, b, c);
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x00173E1E File Offset: 0x0017301E
		public static Vector128<float> MultiplySubtract(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplySubtract(a, b, c);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x00173E28 File Offset: 0x00173028
		public static Vector128<double> MultiplySubtract(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplySubtract(a, b, c);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x00173E32 File Offset: 0x00173032
		public static Vector256<float> MultiplySubtract(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplySubtract(a, b, c);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x00173E3C File Offset: 0x0017303C
		public static Vector256<double> MultiplySubtract(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplySubtract(a, b, c);
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x00173E46 File Offset: 0x00173046
		public static Vector128<float> MultiplySubtractScalar(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplySubtractScalar(a, b, c);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x00173E50 File Offset: 0x00173050
		public static Vector128<double> MultiplySubtractScalar(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplySubtractScalar(a, b, c);
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x00173E5A File Offset: 0x0017305A
		public static Vector128<float> MultiplySubtractAdd(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplySubtractAdd(a, b, c);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x00173E64 File Offset: 0x00173064
		public static Vector128<double> MultiplySubtractAdd(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplySubtractAdd(a, b, c);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x00173E6E File Offset: 0x0017306E
		public static Vector256<float> MultiplySubtractAdd(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplySubtractAdd(a, b, c);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x00173E78 File Offset: 0x00173078
		public static Vector256<double> MultiplySubtractAdd(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplySubtractAdd(a, b, c);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x00173E82 File Offset: 0x00173082
		public static Vector128<float> MultiplyAddNegated(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplyAddNegated(a, b, c);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00173E8C File Offset: 0x0017308C
		public static Vector128<double> MultiplyAddNegated(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplyAddNegated(a, b, c);
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x00173E96 File Offset: 0x00173096
		public static Vector256<float> MultiplyAddNegated(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplyAddNegated(a, b, c);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x00173EA0 File Offset: 0x001730A0
		public static Vector256<double> MultiplyAddNegated(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplyAddNegated(a, b, c);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x00173EAA File Offset: 0x001730AA
		public static Vector128<float> MultiplyAddNegatedScalar(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplyAddNegatedScalar(a, b, c);
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x00173EB4 File Offset: 0x001730B4
		public static Vector128<double> MultiplyAddNegatedScalar(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplyAddNegatedScalar(a, b, c);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x00173EBE File Offset: 0x001730BE
		public static Vector128<float> MultiplySubtractNegated(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplySubtractNegated(a, b, c);
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x00173EC8 File Offset: 0x001730C8
		public static Vector128<double> MultiplySubtractNegated(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplySubtractNegated(a, b, c);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00173ED2 File Offset: 0x001730D2
		public static Vector256<float> MultiplySubtractNegated(Vector256<float> a, Vector256<float> b, Vector256<float> c)
		{
			return Fma.MultiplySubtractNegated(a, b, c);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x00173EDC File Offset: 0x001730DC
		public static Vector256<double> MultiplySubtractNegated(Vector256<double> a, Vector256<double> b, Vector256<double> c)
		{
			return Fma.MultiplySubtractNegated(a, b, c);
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x00173EE6 File Offset: 0x001730E6
		public static Vector128<float> MultiplySubtractNegatedScalar(Vector128<float> a, Vector128<float> b, Vector128<float> c)
		{
			return Fma.MultiplySubtractNegatedScalar(a, b, c);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x00173EF0 File Offset: 0x001730F0
		public static Vector128<double> MultiplySubtractNegatedScalar(Vector128<double> a, Vector128<double> b, Vector128<double> c)
		{
			return Fma.MultiplySubtractNegatedScalar(a, b, c);
		}

		// Token: 0x02000438 RID: 1080
		[Intrinsic]
		public new abstract class X64 : Avx.X64
		{
			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06004060 RID: 16480 RVA: 0x00173EFA File Offset: 0x001730FA
			public new static bool IsSupported
			{
				get
				{
					return Fma.X64.IsSupported;
				}
			}
		}
	}
}
