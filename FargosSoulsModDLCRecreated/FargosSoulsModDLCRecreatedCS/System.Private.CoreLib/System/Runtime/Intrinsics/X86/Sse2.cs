using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000441 RID: 1089
	[CLSCompliant(false)]
	[Intrinsic]
	public abstract class Sse2 : Sse
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x00174284 File Offset: 0x00173484
		public new static bool IsSupported
		{
			get
			{
				return Sse2.IsSupported;
			}
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0017428B File Offset: 0x0017348B
		public static Vector128<byte> Add(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x00174294 File Offset: 0x00173494
		public static Vector128<sbyte> Add(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0017429D File Offset: 0x0017349D
		public static Vector128<short> Add(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x001742A6 File Offset: 0x001734A6
		public static Vector128<ushort> Add(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x001742AF File Offset: 0x001734AF
		public static Vector128<int> Add(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x001742B8 File Offset: 0x001734B8
		public static Vector128<uint> Add(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x001742C1 File Offset: 0x001734C1
		public static Vector128<long> Add(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x001742CA File Offset: 0x001734CA
		public static Vector128<ulong> Add(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x001742D3 File Offset: 0x001734D3
		public static Vector128<double> Add(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Add(left, right);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x001742DC File Offset: 0x001734DC
		public static Vector128<double> AddScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.AddScalar(left, right);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x001742E5 File Offset: 0x001734E5
		public static Vector128<sbyte> AddSaturate(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.AddSaturate(left, right);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x001742EE File Offset: 0x001734EE
		public static Vector128<byte> AddSaturate(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.AddSaturate(left, right);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x001742F7 File Offset: 0x001734F7
		public static Vector128<short> AddSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.AddSaturate(left, right);
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00174300 File Offset: 0x00173500
		public static Vector128<ushort> AddSaturate(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.AddSaturate(left, right);
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00174309 File Offset: 0x00173509
		public static Vector128<byte> And(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00174312 File Offset: 0x00173512
		public static Vector128<sbyte> And(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0017431B File Offset: 0x0017351B
		public static Vector128<short> And(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00174324 File Offset: 0x00173524
		public static Vector128<ushort> And(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x0017432D File Offset: 0x0017352D
		public static Vector128<int> And(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x00174336 File Offset: 0x00173536
		public static Vector128<uint> And(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0017433F File Offset: 0x0017353F
		public static Vector128<long> And(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00174348 File Offset: 0x00173548
		public static Vector128<ulong> And(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00174351 File Offset: 0x00173551
		public static Vector128<double> And(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.And(left, right);
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x0017435A File Offset: 0x0017355A
		public static Vector128<byte> AndNot(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00174363 File Offset: 0x00173563
		public static Vector128<sbyte> AndNot(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0017436C File Offset: 0x0017356C
		public static Vector128<short> AndNot(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00174375 File Offset: 0x00173575
		public static Vector128<ushort> AndNot(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0017437E File Offset: 0x0017357E
		public static Vector128<int> AndNot(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00174387 File Offset: 0x00173587
		public static Vector128<uint> AndNot(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00174390 File Offset: 0x00173590
		public static Vector128<long> AndNot(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00174399 File Offset: 0x00173599
		public static Vector128<ulong> AndNot(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x001743A2 File Offset: 0x001735A2
		public static Vector128<double> AndNot(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.AndNot(left, right);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x001743AB File Offset: 0x001735AB
		public static Vector128<byte> Average(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Average(left, right);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x001743B4 File Offset: 0x001735B4
		public static Vector128<ushort> Average(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.Average(left, right);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x001743BD File Offset: 0x001735BD
		public static Vector128<sbyte> CompareEqual(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x001743C6 File Offset: 0x001735C6
		public static Vector128<byte> CompareEqual(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x001743CF File Offset: 0x001735CF
		public static Vector128<short> CompareEqual(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x001743D8 File Offset: 0x001735D8
		public static Vector128<ushort> CompareEqual(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x001743E1 File Offset: 0x001735E1
		public static Vector128<int> CompareEqual(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x001743EA File Offset: 0x001735EA
		public static Vector128<uint> CompareEqual(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x001743F3 File Offset: 0x001735F3
		public static Vector128<double> CompareEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareEqual(left, right);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x001743FC File Offset: 0x001735FC
		public static bool CompareScalarOrderedEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedEqual(left, right);
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x00174405 File Offset: 0x00173605
		public static bool CompareScalarUnorderedEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedEqual(left, right);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0017440E File Offset: 0x0017360E
		public static Vector128<double> CompareScalarEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarEqual(left, right);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x00174417 File Offset: 0x00173617
		public static Vector128<sbyte> CompareGreaterThan(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.CompareGreaterThan(left, right);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x00174420 File Offset: 0x00173620
		public static Vector128<short> CompareGreaterThan(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.CompareGreaterThan(left, right);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x00174429 File Offset: 0x00173629
		public static Vector128<int> CompareGreaterThan(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.CompareGreaterThan(left, right);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x00174432 File Offset: 0x00173632
		public static Vector128<double> CompareGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareGreaterThan(left, right);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0017443B File Offset: 0x0017363B
		public static bool CompareScalarOrderedGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedGreaterThan(left, right);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00174444 File Offset: 0x00173644
		public static bool CompareScalarUnorderedGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedGreaterThan(left, right);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0017444D File Offset: 0x0017364D
		public static Vector128<double> CompareScalarGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarGreaterThan(left, right);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x00174456 File Offset: 0x00173656
		public static Vector128<double> CompareGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareGreaterThanOrEqual(left, right);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x0017445F File Offset: 0x0017365F
		public static bool CompareScalarOrderedGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedGreaterThanOrEqual(left, right);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x00174468 File Offset: 0x00173668
		public static bool CompareScalarUnorderedGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedGreaterThanOrEqual(left, right);
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x00174471 File Offset: 0x00173671
		public static Vector128<double> CompareScalarGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarGreaterThanOrEqual(left, right);
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0017447A File Offset: 0x0017367A
		public static Vector128<sbyte> CompareLessThan(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.CompareLessThan(left, right);
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00174483 File Offset: 0x00173683
		public static Vector128<short> CompareLessThan(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.CompareLessThan(left, right);
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0017448C File Offset: 0x0017368C
		public static Vector128<int> CompareLessThan(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.CompareLessThan(left, right);
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00174495 File Offset: 0x00173695
		public static Vector128<double> CompareLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareLessThan(left, right);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x0017449E File Offset: 0x0017369E
		public static bool CompareScalarOrderedLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedLessThan(left, right);
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x001744A7 File Offset: 0x001736A7
		public static bool CompareScalarUnorderedLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedLessThan(left, right);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001744B0 File Offset: 0x001736B0
		public static Vector128<double> CompareScalarLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarLessThan(left, right);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x001744B9 File Offset: 0x001736B9
		public static Vector128<double> CompareLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareLessThanOrEqual(left, right);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x001744C2 File Offset: 0x001736C2
		public static bool CompareScalarOrderedLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedLessThanOrEqual(left, right);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x001744CB File Offset: 0x001736CB
		public static bool CompareScalarUnorderedLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedLessThanOrEqual(left, right);
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x001744D4 File Offset: 0x001736D4
		public static Vector128<double> CompareScalarLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarLessThanOrEqual(left, right);
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x001744DD File Offset: 0x001736DD
		public static Vector128<double> CompareNotEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareNotEqual(left, right);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x001744E6 File Offset: 0x001736E6
		public static bool CompareScalarOrderedNotEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrderedNotEqual(left, right);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x001744EF File Offset: 0x001736EF
		public static bool CompareScalarUnorderedNotEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnorderedNotEqual(left, right);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x001744F8 File Offset: 0x001736F8
		public static Vector128<double> CompareScalarNotEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarNotEqual(left, right);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x00174501 File Offset: 0x00173701
		public static Vector128<double> CompareNotGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareNotGreaterThan(left, right);
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0017450A File Offset: 0x0017370A
		public static Vector128<double> CompareScalarNotGreaterThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarNotGreaterThan(left, right);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x00174513 File Offset: 0x00173713
		public static Vector128<double> CompareNotGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareNotGreaterThanOrEqual(left, right);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0017451C File Offset: 0x0017371C
		public static Vector128<double> CompareScalarNotGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarNotGreaterThanOrEqual(left, right);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x00174525 File Offset: 0x00173725
		public static Vector128<double> CompareNotLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareNotLessThan(left, right);
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0017452E File Offset: 0x0017372E
		public static Vector128<double> CompareScalarNotLessThan(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarNotLessThan(left, right);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x00174537 File Offset: 0x00173737
		public static Vector128<double> CompareNotLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareNotLessThanOrEqual(left, right);
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x00174540 File Offset: 0x00173740
		public static Vector128<double> CompareScalarNotLessThanOrEqual(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarNotLessThanOrEqual(left, right);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x00174549 File Offset: 0x00173749
		public static Vector128<double> CompareOrdered(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareOrdered(left, right);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x00174552 File Offset: 0x00173752
		public static Vector128<double> CompareScalarOrdered(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarOrdered(left, right);
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x0017455B File Offset: 0x0017375B
		public static Vector128<double> CompareUnordered(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareUnordered(left, right);
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x00174564 File Offset: 0x00173764
		public static Vector128<double> CompareScalarUnordered(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.CompareScalarUnordered(left, right);
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0017456D File Offset: 0x0017376D
		public static Vector128<int> ConvertToVector128Int32(Vector128<float> value)
		{
			return Sse2.ConvertToVector128Int32(value);
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x00174575 File Offset: 0x00173775
		public static Vector128<int> ConvertToVector128Int32(Vector128<double> value)
		{
			return Sse2.ConvertToVector128Int32(value);
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0017457D File Offset: 0x0017377D
		public static Vector128<float> ConvertToVector128Single(Vector128<int> value)
		{
			return Sse2.ConvertToVector128Single(value);
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x00174585 File Offset: 0x00173785
		public static Vector128<float> ConvertToVector128Single(Vector128<double> value)
		{
			return Sse2.ConvertToVector128Single(value);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0017458D File Offset: 0x0017378D
		public static Vector128<double> ConvertToVector128Double(Vector128<int> value)
		{
			return Sse2.ConvertToVector128Double(value);
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00174595 File Offset: 0x00173795
		public static Vector128<double> ConvertToVector128Double(Vector128<float> value)
		{
			return Sse2.ConvertToVector128Double(value);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0017459D File Offset: 0x0017379D
		public static int ConvertToInt32(Vector128<double> value)
		{
			return Sse2.ConvertToInt32(value);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x001745A5 File Offset: 0x001737A5
		public static int ConvertToInt32(Vector128<int> value)
		{
			return Sse2.ConvertToInt32(value);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x001745AD File Offset: 0x001737AD
		public static uint ConvertToUInt32(Vector128<uint> value)
		{
			return Sse2.ConvertToUInt32(value);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x001745B5 File Offset: 0x001737B5
		public static Vector128<double> ConvertScalarToVector128Double(Vector128<double> upper, int value)
		{
			return Sse2.ConvertScalarToVector128Double(upper, value);
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x001745BE File Offset: 0x001737BE
		public static Vector128<double> ConvertScalarToVector128Double(Vector128<double> upper, Vector128<float> value)
		{
			return Sse2.ConvertScalarToVector128Double(upper, value);
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x001745C7 File Offset: 0x001737C7
		public static Vector128<int> ConvertScalarToVector128Int32(int value)
		{
			return Sse2.ConvertScalarToVector128Int32(value);
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x001745CF File Offset: 0x001737CF
		public static Vector128<float> ConvertScalarToVector128Single(Vector128<float> upper, Vector128<double> value)
		{
			return Sse2.ConvertScalarToVector128Single(upper, value);
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x001745D8 File Offset: 0x001737D8
		public static Vector128<uint> ConvertScalarToVector128UInt32(uint value)
		{
			return Sse2.ConvertScalarToVector128UInt32(value);
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x001745E0 File Offset: 0x001737E0
		public static Vector128<int> ConvertToVector128Int32WithTruncation(Vector128<float> value)
		{
			return Sse2.ConvertToVector128Int32WithTruncation(value);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x001745E8 File Offset: 0x001737E8
		public static Vector128<int> ConvertToVector128Int32WithTruncation(Vector128<double> value)
		{
			return Sse2.ConvertToVector128Int32WithTruncation(value);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x001745F0 File Offset: 0x001737F0
		public static int ConvertToInt32WithTruncation(Vector128<double> value)
		{
			return Sse2.ConvertToInt32WithTruncation(value);
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x001745F8 File Offset: 0x001737F8
		public static Vector128<double> Divide(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Divide(left, right);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x00174601 File Offset: 0x00173801
		public static Vector128<double> DivideScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.DivideScalar(left, right);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x0017460A File Offset: 0x0017380A
		public static ushort Extract(Vector128<ushort> value, byte index)
		{
			return Sse2.Extract(value, index);
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x00174613 File Offset: 0x00173813
		public static Vector128<short> Insert(Vector128<short> value, short data, byte index)
		{
			return Sse2.Insert(value, data, index);
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x0017461D File Offset: 0x0017381D
		public static Vector128<ushort> Insert(Vector128<ushort> value, ushort data, byte index)
		{
			return Sse2.Insert(value, data, index);
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x00174627 File Offset: 0x00173827
		public unsafe static Vector128<sbyte> LoadVector128(sbyte* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x0017462F File Offset: 0x0017382F
		public unsafe static Vector128<byte> LoadVector128(byte* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x00174637 File Offset: 0x00173837
		public unsafe static Vector128<short> LoadVector128(short* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0017463F File Offset: 0x0017383F
		public unsafe static Vector128<ushort> LoadVector128(ushort* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x00174647 File Offset: 0x00173847
		public unsafe static Vector128<int> LoadVector128(int* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x0017464F File Offset: 0x0017384F
		public unsafe static Vector128<uint> LoadVector128(uint* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x00174657 File Offset: 0x00173857
		public unsafe static Vector128<long> LoadVector128(long* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0017465F File Offset: 0x0017385F
		public unsafe static Vector128<ulong> LoadVector128(ulong* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x00174667 File Offset: 0x00173867
		public unsafe static Vector128<double> LoadVector128(double* address)
		{
			return Sse2.LoadVector128(address);
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x0017466F File Offset: 0x0017386F
		public unsafe static Vector128<double> LoadScalarVector128(double* address)
		{
			return Sse2.LoadScalarVector128(address);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x00174677 File Offset: 0x00173877
		public unsafe static Vector128<sbyte> LoadAlignedVector128(sbyte* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0017467F File Offset: 0x0017387F
		public unsafe static Vector128<byte> LoadAlignedVector128(byte* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x00174687 File Offset: 0x00173887
		public unsafe static Vector128<short> LoadAlignedVector128(short* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0017468F File Offset: 0x0017388F
		public unsafe static Vector128<ushort> LoadAlignedVector128(ushort* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x00174697 File Offset: 0x00173897
		public unsafe static Vector128<int> LoadAlignedVector128(int* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0017469F File Offset: 0x0017389F
		public unsafe static Vector128<uint> LoadAlignedVector128(uint* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x001746A7 File Offset: 0x001738A7
		public unsafe static Vector128<long> LoadAlignedVector128(long* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x001746AF File Offset: 0x001738AF
		public unsafe static Vector128<ulong> LoadAlignedVector128(ulong* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x001746B7 File Offset: 0x001738B7
		public unsafe static Vector128<double> LoadAlignedVector128(double* address)
		{
			return Sse2.LoadAlignedVector128(address);
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x001746BF File Offset: 0x001738BF
		public static void LoadFence()
		{
			Sse2.LoadFence();
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x001746C6 File Offset: 0x001738C6
		public unsafe static Vector128<double> LoadHigh(Vector128<double> lower, double* address)
		{
			return Sse2.LoadHigh(lower, address);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x001746CF File Offset: 0x001738CF
		public unsafe static Vector128<double> LoadLow(Vector128<double> upper, double* address)
		{
			return Sse2.LoadLow(upper, address);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x001746D8 File Offset: 0x001738D8
		public unsafe static Vector128<int> LoadScalarVector128(int* address)
		{
			return Sse2.LoadScalarVector128(address);
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x001746E0 File Offset: 0x001738E0
		public unsafe static Vector128<uint> LoadScalarVector128(uint* address)
		{
			return Sse2.LoadScalarVector128(address);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x001746E8 File Offset: 0x001738E8
		public unsafe static Vector128<long> LoadScalarVector128(long* address)
		{
			return Sse2.LoadScalarVector128(address);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x001746F0 File Offset: 0x001738F0
		public unsafe static Vector128<ulong> LoadScalarVector128(ulong* address)
		{
			return Sse2.LoadScalarVector128(address);
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x001746F8 File Offset: 0x001738F8
		public unsafe static void MaskMove(Vector128<sbyte> source, Vector128<sbyte> mask, sbyte* address)
		{
			Sse2.MaskMove(source, mask, address);
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x00174702 File Offset: 0x00173902
		public unsafe static void MaskMove(Vector128<byte> source, Vector128<byte> mask, byte* address)
		{
			Sse2.MaskMove(source, mask, address);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x0017470C File Offset: 0x0017390C
		public static Vector128<byte> Max(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Max(left, right);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x00174715 File Offset: 0x00173915
		public static Vector128<short> Max(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Max(left, right);
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0017471E File Offset: 0x0017391E
		public static Vector128<double> Max(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Max(left, right);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x00174727 File Offset: 0x00173927
		public static Vector128<double> MaxScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.MaxScalar(left, right);
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x00174730 File Offset: 0x00173930
		public static void MemoryFence()
		{
			Sse2.MemoryFence();
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x00174737 File Offset: 0x00173937
		public static Vector128<byte> Min(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Min(left, right);
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x00174740 File Offset: 0x00173940
		public static Vector128<short> Min(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Min(left, right);
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x00174749 File Offset: 0x00173949
		public static Vector128<double> Min(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Min(left, right);
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x00174752 File Offset: 0x00173952
		public static Vector128<double> MinScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.MinScalar(left, right);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x0017475B File Offset: 0x0017395B
		public static Vector128<double> MoveScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse2.MoveScalar(upper, value);
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00174764 File Offset: 0x00173964
		public static int MoveMask(Vector128<sbyte> value)
		{
			return Sse2.MoveMask(value);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x0017476C File Offset: 0x0017396C
		public static int MoveMask(Vector128<byte> value)
		{
			return Sse2.MoveMask(value);
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x00174774 File Offset: 0x00173974
		public static int MoveMask(Vector128<double> value)
		{
			return Sse2.MoveMask(value);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0017477C File Offset: 0x0017397C
		public static Vector128<long> MoveScalar(Vector128<long> value)
		{
			return Sse2.MoveScalar(value);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x00174784 File Offset: 0x00173984
		public static Vector128<ulong> MoveScalar(Vector128<ulong> value)
		{
			return Sse2.MoveScalar(value);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0017478C File Offset: 0x0017398C
		public static Vector128<ulong> Multiply(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.Multiply(left, right);
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x00174795 File Offset: 0x00173995
		public static Vector128<double> Multiply(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Multiply(left, right);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0017479E File Offset: 0x0017399E
		public static Vector128<double> MultiplyScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.MultiplyScalar(left, right);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x001747A7 File Offset: 0x001739A7
		public static Vector128<short> MultiplyHigh(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.MultiplyHigh(left, right);
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x001747B0 File Offset: 0x001739B0
		public static Vector128<ushort> MultiplyHigh(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.MultiplyHigh(left, right);
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x001747B9 File Offset: 0x001739B9
		public static Vector128<int> MultiplyAddAdjacent(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.MultiplyAddAdjacent(left, right);
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x001747C2 File Offset: 0x001739C2
		public static Vector128<short> MultiplyLow(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.MultiplyLow(left, right);
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x001747CB File Offset: 0x001739CB
		public static Vector128<ushort> MultiplyLow(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.MultiplyLow(left, right);
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x001747D4 File Offset: 0x001739D4
		public static Vector128<byte> Or(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x001747DD File Offset: 0x001739DD
		public static Vector128<sbyte> Or(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x001747E6 File Offset: 0x001739E6
		public static Vector128<short> Or(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x001747EF File Offset: 0x001739EF
		public static Vector128<ushort> Or(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x001747F8 File Offset: 0x001739F8
		public static Vector128<int> Or(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x00174801 File Offset: 0x00173A01
		public static Vector128<uint> Or(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x0017480A File Offset: 0x00173A0A
		public static Vector128<long> Or(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x00174813 File Offset: 0x00173A13
		public static Vector128<ulong> Or(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0017481C File Offset: 0x00173A1C
		public static Vector128<double> Or(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Or(left, right);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x00174825 File Offset: 0x00173A25
		public static Vector128<sbyte> PackSignedSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.PackSignedSaturate(left, right);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0017482E File Offset: 0x00173A2E
		public static Vector128<short> PackSignedSaturate(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.PackSignedSaturate(left, right);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00174837 File Offset: 0x00173A37
		public static Vector128<byte> PackUnsignedSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.PackUnsignedSaturate(left, right);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x00174840 File Offset: 0x00173A40
		public static Vector128<ushort> SumAbsoluteDifferences(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.SumAbsoluteDifferences(left, right);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x00174849 File Offset: 0x00173A49
		public static Vector128<int> Shuffle(Vector128<int> value, byte control)
		{
			return Sse2.Shuffle(value, control);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x00174852 File Offset: 0x00173A52
		public static Vector128<uint> Shuffle(Vector128<uint> value, byte control)
		{
			return Sse2.Shuffle(value, control);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x0017485B File Offset: 0x00173A5B
		public static Vector128<double> Shuffle(Vector128<double> left, Vector128<double> right, byte control)
		{
			return Sse2.Shuffle(left, right, control);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00174865 File Offset: 0x00173A65
		public static Vector128<short> ShuffleHigh(Vector128<short> value, byte control)
		{
			return Sse2.ShuffleHigh(value, control);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x0017486E File Offset: 0x00173A6E
		public static Vector128<ushort> ShuffleHigh(Vector128<ushort> value, byte control)
		{
			return Sse2.ShuffleHigh(value, control);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x00174877 File Offset: 0x00173A77
		public static Vector128<short> ShuffleLow(Vector128<short> value, byte control)
		{
			return Sse2.ShuffleLow(value, control);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00174880 File Offset: 0x00173A80
		public static Vector128<ushort> ShuffleLow(Vector128<ushort> value, byte control)
		{
			return Sse2.ShuffleLow(value, control);
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x00174889 File Offset: 0x00173A89
		public static Vector128<short> ShiftLeftLogical(Vector128<short> value, Vector128<short> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x00174892 File Offset: 0x00173A92
		public static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> value, Vector128<ushort> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x0017489B File Offset: 0x00173A9B
		public static Vector128<int> ShiftLeftLogical(Vector128<int> value, Vector128<int> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x001748A4 File Offset: 0x00173AA4
		public static Vector128<uint> ShiftLeftLogical(Vector128<uint> value, Vector128<uint> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x001748AD File Offset: 0x00173AAD
		public static Vector128<long> ShiftLeftLogical(Vector128<long> value, Vector128<long> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x001748B6 File Offset: 0x00173AB6
		public static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> value, Vector128<ulong> count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x001748BF File Offset: 0x00173ABF
		public static Vector128<short> ShiftLeftLogical(Vector128<short> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x001748C8 File Offset: 0x00173AC8
		public static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x001748D1 File Offset: 0x00173AD1
		public static Vector128<int> ShiftLeftLogical(Vector128<int> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x001748DA File Offset: 0x00173ADA
		public static Vector128<uint> ShiftLeftLogical(Vector128<uint> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x001748E3 File Offset: 0x00173AE3
		public static Vector128<long> ShiftLeftLogical(Vector128<long> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x001748EC File Offset: 0x00173AEC
		public static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> value, byte count)
		{
			return Sse2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x001748F5 File Offset: 0x00173AF5
		public static Vector128<sbyte> ShiftLeftLogical128BitLane(Vector128<sbyte> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x001748FE File Offset: 0x00173AFE
		public static Vector128<byte> ShiftLeftLogical128BitLane(Vector128<byte> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x00174907 File Offset: 0x00173B07
		public static Vector128<short> ShiftLeftLogical128BitLane(Vector128<short> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x00174910 File Offset: 0x00173B10
		public static Vector128<ushort> ShiftLeftLogical128BitLane(Vector128<ushort> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x00174919 File Offset: 0x00173B19
		public static Vector128<int> ShiftLeftLogical128BitLane(Vector128<int> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x00174922 File Offset: 0x00173B22
		public static Vector128<uint> ShiftLeftLogical128BitLane(Vector128<uint> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0017492B File Offset: 0x00173B2B
		public static Vector128<long> ShiftLeftLogical128BitLane(Vector128<long> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x00174934 File Offset: 0x00173B34
		public static Vector128<ulong> ShiftLeftLogical128BitLane(Vector128<ulong> value, byte numBytes)
		{
			return Sse2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0017493D File Offset: 0x00173B3D
		public static Vector128<short> ShiftRightArithmetic(Vector128<short> value, Vector128<short> count)
		{
			return Sse2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x00174946 File Offset: 0x00173B46
		public static Vector128<int> ShiftRightArithmetic(Vector128<int> value, Vector128<int> count)
		{
			return Sse2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0017494F File Offset: 0x00173B4F
		public static Vector128<short> ShiftRightArithmetic(Vector128<short> value, byte count)
		{
			return Sse2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x00174958 File Offset: 0x00173B58
		public static Vector128<int> ShiftRightArithmetic(Vector128<int> value, byte count)
		{
			return Sse2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x00174961 File Offset: 0x00173B61
		public static Vector128<short> ShiftRightLogical(Vector128<short> value, Vector128<short> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x0017496A File Offset: 0x00173B6A
		public static Vector128<ushort> ShiftRightLogical(Vector128<ushort> value, Vector128<ushort> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x00174973 File Offset: 0x00173B73
		public static Vector128<int> ShiftRightLogical(Vector128<int> value, Vector128<int> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x0017497C File Offset: 0x00173B7C
		public static Vector128<uint> ShiftRightLogical(Vector128<uint> value, Vector128<uint> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x00174985 File Offset: 0x00173B85
		public static Vector128<long> ShiftRightLogical(Vector128<long> value, Vector128<long> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x0017498E File Offset: 0x00173B8E
		public static Vector128<ulong> ShiftRightLogical(Vector128<ulong> value, Vector128<ulong> count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x00174997 File Offset: 0x00173B97
		public static Vector128<short> ShiftRightLogical(Vector128<short> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x001749A0 File Offset: 0x00173BA0
		public static Vector128<ushort> ShiftRightLogical(Vector128<ushort> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x001749A9 File Offset: 0x00173BA9
		public static Vector128<int> ShiftRightLogical(Vector128<int> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x001749B2 File Offset: 0x00173BB2
		public static Vector128<uint> ShiftRightLogical(Vector128<uint> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x001749BB File Offset: 0x00173BBB
		public static Vector128<long> ShiftRightLogical(Vector128<long> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x001749C4 File Offset: 0x00173BC4
		public static Vector128<ulong> ShiftRightLogical(Vector128<ulong> value, byte count)
		{
			return Sse2.ShiftRightLogical(value, count);
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x001749CD File Offset: 0x00173BCD
		public static Vector128<sbyte> ShiftRightLogical128BitLane(Vector128<sbyte> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x001749D6 File Offset: 0x00173BD6
		public static Vector128<byte> ShiftRightLogical128BitLane(Vector128<byte> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x001749DF File Offset: 0x00173BDF
		public static Vector128<short> ShiftRightLogical128BitLane(Vector128<short> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x001749E8 File Offset: 0x00173BE8
		public static Vector128<ushort> ShiftRightLogical128BitLane(Vector128<ushort> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x001749F1 File Offset: 0x00173BF1
		public static Vector128<int> ShiftRightLogical128BitLane(Vector128<int> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x001749FA File Offset: 0x00173BFA
		public static Vector128<uint> ShiftRightLogical128BitLane(Vector128<uint> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x00174A03 File Offset: 0x00173C03
		public static Vector128<long> ShiftRightLogical128BitLane(Vector128<long> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x00174A0C File Offset: 0x00173C0C
		public static Vector128<ulong> ShiftRightLogical128BitLane(Vector128<ulong> value, byte numBytes)
		{
			return Sse2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00174A15 File Offset: 0x00173C15
		public static Vector128<double> Sqrt(Vector128<double> value)
		{
			return Sse2.Sqrt(value);
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x00174A1D File Offset: 0x00173C1D
		public static Vector128<double> SqrtScalar(Vector128<double> value)
		{
			return Sse2.SqrtScalar(value);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00174A25 File Offset: 0x00173C25
		public static Vector128<double> SqrtScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse2.SqrtScalar(upper, value);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x00174A2E File Offset: 0x00173C2E
		public unsafe static void StoreScalar(double* address, Vector128<double> source)
		{
			Sse2.StoreScalar(address, source);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x00174A37 File Offset: 0x00173C37
		public unsafe static void StoreScalar(int* address, Vector128<int> source)
		{
			Sse2.StoreScalar(address, source);
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x00174A40 File Offset: 0x00173C40
		public unsafe static void StoreScalar(long* address, Vector128<long> source)
		{
			Sse2.StoreScalar(address, source);
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x00174A49 File Offset: 0x00173C49
		public unsafe static void StoreScalar(uint* address, Vector128<uint> source)
		{
			Sse2.StoreScalar(address, source);
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x00174A52 File Offset: 0x00173C52
		public unsafe static void StoreScalar(ulong* address, Vector128<ulong> source)
		{
			Sse2.StoreScalar(address, source);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x00174A5B File Offset: 0x00173C5B
		public unsafe static void StoreAligned(sbyte* address, Vector128<sbyte> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00174A64 File Offset: 0x00173C64
		public unsafe static void StoreAligned(byte* address, Vector128<byte> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00174A6D File Offset: 0x00173C6D
		public unsafe static void StoreAligned(short* address, Vector128<short> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x00174A76 File Offset: 0x00173C76
		public unsafe static void StoreAligned(ushort* address, Vector128<ushort> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x00174A7F File Offset: 0x00173C7F
		public unsafe static void StoreAligned(int* address, Vector128<int> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x00174A88 File Offset: 0x00173C88
		public unsafe static void StoreAligned(uint* address, Vector128<uint> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x00174A91 File Offset: 0x00173C91
		public unsafe static void StoreAligned(long* address, Vector128<long> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00174A9A File Offset: 0x00173C9A
		public unsafe static void StoreAligned(ulong* address, Vector128<ulong> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x00174AA3 File Offset: 0x00173CA3
		public unsafe static void StoreAligned(double* address, Vector128<double> source)
		{
			Sse2.StoreAligned(address, source);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x00174AAC File Offset: 0x00173CAC
		public unsafe static void StoreAlignedNonTemporal(sbyte* address, Vector128<sbyte> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x00174AB5 File Offset: 0x00173CB5
		public unsafe static void StoreAlignedNonTemporal(byte* address, Vector128<byte> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00174ABE File Offset: 0x00173CBE
		public unsafe static void StoreAlignedNonTemporal(short* address, Vector128<short> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x00174AC7 File Offset: 0x00173CC7
		public unsafe static void StoreAlignedNonTemporal(ushort* address, Vector128<ushort> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x00174AD0 File Offset: 0x00173CD0
		public unsafe static void StoreAlignedNonTemporal(int* address, Vector128<int> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x00174AD9 File Offset: 0x00173CD9
		public unsafe static void StoreAlignedNonTemporal(uint* address, Vector128<uint> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x00174AE2 File Offset: 0x00173CE2
		public unsafe static void StoreAlignedNonTemporal(long* address, Vector128<long> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x00174AEB File Offset: 0x00173CEB
		public unsafe static void StoreAlignedNonTemporal(ulong* address, Vector128<ulong> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x00174AF4 File Offset: 0x00173CF4
		public unsafe static void StoreAlignedNonTemporal(double* address, Vector128<double> source)
		{
			Sse2.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x00174AFD File Offset: 0x00173CFD
		public unsafe static void Store(sbyte* address, Vector128<sbyte> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x00174B06 File Offset: 0x00173D06
		public unsafe static void Store(byte* address, Vector128<byte> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x00174B0F File Offset: 0x00173D0F
		public unsafe static void Store(short* address, Vector128<short> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x00174B18 File Offset: 0x00173D18
		public unsafe static void Store(ushort* address, Vector128<ushort> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x00174B21 File Offset: 0x00173D21
		public unsafe static void Store(int* address, Vector128<int> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00174B2A File Offset: 0x00173D2A
		public unsafe static void Store(uint* address, Vector128<uint> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00174B33 File Offset: 0x00173D33
		public unsafe static void Store(long* address, Vector128<long> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x00174B3C File Offset: 0x00173D3C
		public unsafe static void Store(ulong* address, Vector128<ulong> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x00174B45 File Offset: 0x00173D45
		public unsafe static void Store(double* address, Vector128<double> source)
		{
			Sse2.Store(address, source);
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x00174B4E File Offset: 0x00173D4E
		public unsafe static void StoreHigh(double* address, Vector128<double> source)
		{
			Sse2.StoreHigh(address, source);
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x00174B57 File Offset: 0x00173D57
		public unsafe static void StoreLow(double* address, Vector128<double> source)
		{
			Sse2.StoreLow(address, source);
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x00174B60 File Offset: 0x00173D60
		public unsafe static void StoreNonTemporal(int* address, int value)
		{
			Sse2.StoreNonTemporal(address, value);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00174B69 File Offset: 0x00173D69
		public unsafe static void StoreNonTemporal(uint* address, uint value)
		{
			Sse2.StoreNonTemporal(address, value);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00174B72 File Offset: 0x00173D72
		public static Vector128<byte> Subtract(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00174B7B File Offset: 0x00173D7B
		public static Vector128<sbyte> Subtract(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x00174B84 File Offset: 0x00173D84
		public static Vector128<short> Subtract(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x00174B8D File Offset: 0x00173D8D
		public static Vector128<ushort> Subtract(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x00174B96 File Offset: 0x00173D96
		public static Vector128<int> Subtract(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x00174B9F File Offset: 0x00173D9F
		public static Vector128<uint> Subtract(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x00174BA8 File Offset: 0x00173DA8
		public static Vector128<long> Subtract(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x00174BB1 File Offset: 0x00173DB1
		public static Vector128<ulong> Subtract(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x00174BBA File Offset: 0x00173DBA
		public static Vector128<double> Subtract(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Subtract(left, right);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x00174BC3 File Offset: 0x00173DC3
		public static Vector128<double> SubtractScalar(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.SubtractScalar(left, right);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x00174BCC File Offset: 0x00173DCC
		public static Vector128<sbyte> SubtractSaturate(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.SubtractSaturate(left, right);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x00174BD5 File Offset: 0x00173DD5
		public static Vector128<short> SubtractSaturate(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.SubtractSaturate(left, right);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00174BDE File Offset: 0x00173DDE
		public static Vector128<byte> SubtractSaturate(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.SubtractSaturate(left, right);
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x00174BE7 File Offset: 0x00173DE7
		public static Vector128<ushort> SubtractSaturate(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.SubtractSaturate(left, right);
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x00174BF0 File Offset: 0x00173DF0
		public static Vector128<byte> UnpackHigh(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x00174BF9 File Offset: 0x00173DF9
		public static Vector128<sbyte> UnpackHigh(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x00174C02 File Offset: 0x00173E02
		public static Vector128<short> UnpackHigh(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00174C0B File Offset: 0x00173E0B
		public static Vector128<ushort> UnpackHigh(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x00174C14 File Offset: 0x00173E14
		public static Vector128<int> UnpackHigh(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00174C1D File Offset: 0x00173E1D
		public static Vector128<uint> UnpackHigh(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x00174C26 File Offset: 0x00173E26
		public static Vector128<long> UnpackHigh(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x00174C2F File Offset: 0x00173E2F
		public static Vector128<ulong> UnpackHigh(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00174C38 File Offset: 0x00173E38
		public static Vector128<double> UnpackHigh(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.UnpackHigh(left, right);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00174C41 File Offset: 0x00173E41
		public static Vector128<byte> UnpackLow(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00174C4A File Offset: 0x00173E4A
		public static Vector128<sbyte> UnpackLow(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x00174C53 File Offset: 0x00173E53
		public static Vector128<short> UnpackLow(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x00174C5C File Offset: 0x00173E5C
		public static Vector128<ushort> UnpackLow(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x00174C65 File Offset: 0x00173E65
		public static Vector128<int> UnpackLow(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00174C6E File Offset: 0x00173E6E
		public static Vector128<uint> UnpackLow(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00174C77 File Offset: 0x00173E77
		public static Vector128<long> UnpackLow(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00174C80 File Offset: 0x00173E80
		public static Vector128<ulong> UnpackLow(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00174C89 File Offset: 0x00173E89
		public static Vector128<double> UnpackLow(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.UnpackLow(left, right);
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00174C92 File Offset: 0x00173E92
		public static Vector128<byte> Xor(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00174C9B File Offset: 0x00173E9B
		public static Vector128<sbyte> Xor(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x00174CA4 File Offset: 0x00173EA4
		public static Vector128<short> Xor(Vector128<short> left, Vector128<short> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00174CAD File Offset: 0x00173EAD
		public static Vector128<ushort> Xor(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00174CB6 File Offset: 0x00173EB6
		public static Vector128<int> Xor(Vector128<int> left, Vector128<int> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00174CBF File Offset: 0x00173EBF
		public static Vector128<uint> Xor(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00174CC8 File Offset: 0x00173EC8
		public static Vector128<long> Xor(Vector128<long> left, Vector128<long> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00174CD1 File Offset: 0x00173ED1
		public static Vector128<ulong> Xor(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00174CDA File Offset: 0x00173EDA
		public static Vector128<double> Xor(Vector128<double> left, Vector128<double> right)
		{
			return Sse2.Xor(left, right);
		}

		// Token: 0x02000442 RID: 1090
		[Intrinsic]
		public new abstract class X64 : Sse.X64
		{
			// Token: 0x17000A36 RID: 2614
			// (get) Token: 0x060041F5 RID: 16885 RVA: 0x00174CE3 File Offset: 0x00173EE3
			public new static bool IsSupported
			{
				get
				{
					return Sse2.X64.IsSupported;
				}
			}

			// Token: 0x060041F6 RID: 16886 RVA: 0x00174CEA File Offset: 0x00173EEA
			public static long ConvertToInt64(Vector128<double> value)
			{
				return Sse2.X64.ConvertToInt64(value);
			}

			// Token: 0x060041F7 RID: 16887 RVA: 0x00174CF2 File Offset: 0x00173EF2
			public static long ConvertToInt64(Vector128<long> value)
			{
				return Sse2.X64.ConvertToInt64(value);
			}

			// Token: 0x060041F8 RID: 16888 RVA: 0x00174CFA File Offset: 0x00173EFA
			public static ulong ConvertToUInt64(Vector128<ulong> value)
			{
				return Sse2.X64.ConvertToUInt64(value);
			}

			// Token: 0x060041F9 RID: 16889 RVA: 0x00174D02 File Offset: 0x00173F02
			public static Vector128<double> ConvertScalarToVector128Double(Vector128<double> upper, long value)
			{
				return Sse2.X64.ConvertScalarToVector128Double(upper, value);
			}

			// Token: 0x060041FA RID: 16890 RVA: 0x00174D0B File Offset: 0x00173F0B
			public static Vector128<long> ConvertScalarToVector128Int64(long value)
			{
				return Sse2.X64.ConvertScalarToVector128Int64(value);
			}

			// Token: 0x060041FB RID: 16891 RVA: 0x00174D13 File Offset: 0x00173F13
			public static Vector128<ulong> ConvertScalarToVector128UInt64(ulong value)
			{
				return Sse2.X64.ConvertScalarToVector128UInt64(value);
			}

			// Token: 0x060041FC RID: 16892 RVA: 0x00174D1B File Offset: 0x00173F1B
			public static long ConvertToInt64WithTruncation(Vector128<double> value)
			{
				return Sse2.X64.ConvertToInt64WithTruncation(value);
			}

			// Token: 0x060041FD RID: 16893 RVA: 0x00174D23 File Offset: 0x00173F23
			public unsafe static void StoreNonTemporal(long* address, long value)
			{
				Sse2.X64.StoreNonTemporal(address, value);
			}

			// Token: 0x060041FE RID: 16894 RVA: 0x00174D2C File Offset: 0x00173F2C
			public unsafe static void StoreNonTemporal(ulong* address, ulong value)
			{
				Sse2.X64.StoreNonTemporal(address, value);
			}
		}
	}
}
