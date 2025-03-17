using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200042F RID: 1071
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Avx : Sse42
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x00171432 File Offset: 0x00170632
		public new static bool IsSupported
		{
			get
			{
				return Avx.IsSupported;
			}
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00171439 File Offset: 0x00170639
		public static Vector256<float> Add(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Add(left, right);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00171442 File Offset: 0x00170642
		public static Vector256<double> Add(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Add(left, right);
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x0017144B File Offset: 0x0017064B
		public static Vector256<float> AddSubtract(Vector256<float> left, Vector256<float> right)
		{
			return Avx.AddSubtract(left, right);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00171454 File Offset: 0x00170654
		public static Vector256<double> AddSubtract(Vector256<double> left, Vector256<double> right)
		{
			return Avx.AddSubtract(left, right);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x0017145D File Offset: 0x0017065D
		public static Vector256<float> And(Vector256<float> left, Vector256<float> right)
		{
			return Avx.And(left, right);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00171466 File Offset: 0x00170666
		public static Vector256<double> And(Vector256<double> left, Vector256<double> right)
		{
			return Avx.And(left, right);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0017146F File Offset: 0x0017066F
		public static Vector256<float> AndNot(Vector256<float> left, Vector256<float> right)
		{
			return Avx.AndNot(left, right);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00171478 File Offset: 0x00170678
		public static Vector256<double> AndNot(Vector256<double> left, Vector256<double> right)
		{
			return Avx.AndNot(left, right);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00171481 File Offset: 0x00170681
		public static Vector256<float> Blend(Vector256<float> left, Vector256<float> right, byte control)
		{
			return Avx.Blend(left, right, control);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0017148B File Offset: 0x0017068B
		public static Vector256<double> Blend(Vector256<double> left, Vector256<double> right, byte control)
		{
			return Avx.Blend(left, right, control);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00171495 File Offset: 0x00170695
		public static Vector256<float> BlendVariable(Vector256<float> left, Vector256<float> right, Vector256<float> mask)
		{
			return Avx.BlendVariable(left, right, mask);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x0017149F File Offset: 0x0017069F
		public static Vector256<double> BlendVariable(Vector256<double> left, Vector256<double> right, Vector256<double> mask)
		{
			return Avx.BlendVariable(left, right, mask);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x001714A9 File Offset: 0x001706A9
		public unsafe static Vector128<float> BroadcastScalarToVector128(float* source)
		{
			return Avx.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x001714B1 File Offset: 0x001706B1
		public unsafe static Vector256<float> BroadcastScalarToVector256(float* source)
		{
			return Avx.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x001714B9 File Offset: 0x001706B9
		public unsafe static Vector256<double> BroadcastScalarToVector256(double* source)
		{
			return Avx.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x001714C1 File Offset: 0x001706C1
		public unsafe static Vector256<float> BroadcastVector128ToVector256(float* address)
		{
			return Avx.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x001714C9 File Offset: 0x001706C9
		public unsafe static Vector256<double> BroadcastVector128ToVector256(double* address)
		{
			return Avx.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x001714D1 File Offset: 0x001706D1
		public static Vector256<float> Ceiling(Vector256<float> value)
		{
			return Avx.Ceiling(value);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x001714D9 File Offset: 0x001706D9
		public static Vector256<double> Ceiling(Vector256<double> value)
		{
			return Avx.Ceiling(value);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x001714E1 File Offset: 0x001706E1
		public static Vector128<float> Compare(Vector128<float> left, Vector128<float> right, FloatComparisonMode mode)
		{
			return Avx.Compare(left, right, mode);
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x001714EB File Offset: 0x001706EB
		public static Vector128<double> Compare(Vector128<double> left, Vector128<double> right, FloatComparisonMode mode)
		{
			return Avx.Compare(left, right, mode);
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x001714F5 File Offset: 0x001706F5
		public static Vector256<float> Compare(Vector256<float> left, Vector256<float> right, FloatComparisonMode mode)
		{
			return Avx.Compare(left, right, mode);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x001714FF File Offset: 0x001706FF
		public static Vector256<double> Compare(Vector256<double> left, Vector256<double> right, FloatComparisonMode mode)
		{
			return Avx.Compare(left, right, mode);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00171509 File Offset: 0x00170709
		public static Vector256<float> CompareEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedEqualNonSignaling);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00171513 File Offset: 0x00170713
		public static Vector256<double> CompareEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedEqualNonSignaling);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x0017151D File Offset: 0x0017071D
		public static Vector256<float> CompareGreaterThan(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanSignaling);
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x00171528 File Offset: 0x00170728
		public static Vector256<double> CompareGreaterThan(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanSignaling);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00171533 File Offset: 0x00170733
		public static Vector256<float> CompareGreaterThanOrEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanOrEqualSignaling);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x0017153E File Offset: 0x0017073E
		public static Vector256<double> CompareGreaterThanOrEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanOrEqualSignaling);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00171549 File Offset: 0x00170749
		public static Vector256<float> CompareLessThan(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanSignaling);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00171553 File Offset: 0x00170753
		public static Vector256<double> CompareLessThan(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanSignaling);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0017155D File Offset: 0x0017075D
		public static Vector256<float> CompareLessThanOrEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanOrEqualSignaling);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x00171567 File Offset: 0x00170767
		public static Vector256<double> CompareLessThanOrEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanOrEqualSignaling);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00171571 File Offset: 0x00170771
		public static Vector256<float> CompareNotEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotEqualNonSignaling);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0017157B File Offset: 0x0017077B
		public static Vector256<double> CompareNotEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotEqualNonSignaling);
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x00171585 File Offset: 0x00170785
		public static Vector256<float> CompareNotGreaterThan(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotGreaterThanSignaling);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x00171590 File Offset: 0x00170790
		public static Vector256<double> CompareNotGreaterThan(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotGreaterThanSignaling);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x0017159B File Offset: 0x0017079B
		public static Vector256<float> CompareNotGreaterThanOrEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotGreaterThanOrEqualSignaling);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x001715A6 File Offset: 0x001707A6
		public static Vector256<double> CompareNotGreaterThanOrEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotGreaterThanOrEqualSignaling);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x001715B1 File Offset: 0x001707B1
		public static Vector256<float> CompareNotLessThan(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotLessThanSignaling);
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x001715BB File Offset: 0x001707BB
		public static Vector256<double> CompareNotLessThan(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotLessThanSignaling);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x001715C5 File Offset: 0x001707C5
		public static Vector256<float> CompareNotLessThanOrEqual(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotLessThanOrEqualSignaling);
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x001715CF File Offset: 0x001707CF
		public static Vector256<double> CompareNotLessThanOrEqual(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNotLessThanOrEqualSignaling);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x001715D9 File Offset: 0x001707D9
		public static Vector256<float> CompareOrdered(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedNonSignaling);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x001715E3 File Offset: 0x001707E3
		public static Vector256<double> CompareOrdered(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.OrderedNonSignaling);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x001715ED File Offset: 0x001707ED
		public static Vector128<double> CompareScalar(Vector128<double> left, Vector128<double> right, FloatComparisonMode mode)
		{
			return Avx.CompareScalar(left, right, mode);
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x001715F7 File Offset: 0x001707F7
		public static Vector128<float> CompareScalar(Vector128<float> left, Vector128<float> right, FloatComparisonMode mode)
		{
			return Avx.CompareScalar(left, right, mode);
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00171601 File Offset: 0x00170801
		public static Vector256<float> CompareUnordered(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNonSignaling);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0017160B File Offset: 0x0017080B
		public static Vector256<double> CompareUnordered(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Compare(left, right, FloatComparisonMode.UnorderedNonSignaling);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x00171615 File Offset: 0x00170815
		public static Vector128<int> ConvertToVector128Int32(Vector256<double> value)
		{
			return Avx.ConvertToVector128Int32(value);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0017161D File Offset: 0x0017081D
		public static Vector128<float> ConvertToVector128Single(Vector256<double> value)
		{
			return Avx.ConvertToVector128Single(value);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00171625 File Offset: 0x00170825
		public static Vector256<int> ConvertToVector256Int32(Vector256<float> value)
		{
			return Avx.ConvertToVector256Int32(value);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x0017162D File Offset: 0x0017082D
		public static Vector256<float> ConvertToVector256Single(Vector256<int> value)
		{
			return Avx.ConvertToVector256Single(value);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00171635 File Offset: 0x00170835
		public static Vector256<double> ConvertToVector256Double(Vector128<float> value)
		{
			return Avx.ConvertToVector256Double(value);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0017163D File Offset: 0x0017083D
		public static Vector256<double> ConvertToVector256Double(Vector128<int> value)
		{
			return Avx.ConvertToVector256Double(value);
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00171645 File Offset: 0x00170845
		public static Vector128<int> ConvertToVector128Int32WithTruncation(Vector256<double> value)
		{
			return Avx.ConvertToVector128Int32WithTruncation(value);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0017164D File Offset: 0x0017084D
		public static Vector256<int> ConvertToVector256Int32WithTruncation(Vector256<float> value)
		{
			return Avx.ConvertToVector256Int32WithTruncation(value);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x00171655 File Offset: 0x00170855
		public static Vector256<float> Divide(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Divide(left, right);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0017165E File Offset: 0x0017085E
		public static Vector256<double> Divide(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Divide(left, right);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00171667 File Offset: 0x00170867
		public static Vector256<float> DotProduct(Vector256<float> left, Vector256<float> right, byte control)
		{
			return Avx.DotProduct(left, right, control);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00171671 File Offset: 0x00170871
		public static Vector256<float> DuplicateEvenIndexed(Vector256<float> value)
		{
			return Avx.DuplicateEvenIndexed(value);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00171679 File Offset: 0x00170879
		public static Vector256<double> DuplicateEvenIndexed(Vector256<double> value)
		{
			return Avx.DuplicateEvenIndexed(value);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00171681 File Offset: 0x00170881
		public static Vector256<float> DuplicateOddIndexed(Vector256<float> value)
		{
			return Avx.DuplicateOddIndexed(value);
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00171689 File Offset: 0x00170889
		public static Vector128<byte> ExtractVector128(Vector256<byte> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x00171692 File Offset: 0x00170892
		public static Vector128<sbyte> ExtractVector128(Vector256<sbyte> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x0017169B File Offset: 0x0017089B
		public static Vector128<short> ExtractVector128(Vector256<short> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x001716A4 File Offset: 0x001708A4
		public static Vector128<ushort> ExtractVector128(Vector256<ushort> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x001716AD File Offset: 0x001708AD
		public static Vector128<int> ExtractVector128(Vector256<int> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x001716B6 File Offset: 0x001708B6
		public static Vector128<uint> ExtractVector128(Vector256<uint> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x001716BF File Offset: 0x001708BF
		public static Vector128<long> ExtractVector128(Vector256<long> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x001716C8 File Offset: 0x001708C8
		public static Vector128<ulong> ExtractVector128(Vector256<ulong> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x001716D1 File Offset: 0x001708D1
		public static Vector128<float> ExtractVector128(Vector256<float> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x001716DA File Offset: 0x001708DA
		public static Vector128<double> ExtractVector128(Vector256<double> value, byte index)
		{
			return Avx.ExtractVector128(value, index);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x001716E3 File Offset: 0x001708E3
		public static Vector256<float> Floor(Vector256<float> value)
		{
			return Avx.Floor(value);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x001716EB File Offset: 0x001708EB
		public static Vector256<double> Floor(Vector256<double> value)
		{
			return Avx.Floor(value);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x001716F3 File Offset: 0x001708F3
		public static Vector256<float> HorizontalAdd(Vector256<float> left, Vector256<float> right)
		{
			return Avx.HorizontalAdd(left, right);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001716FC File Offset: 0x001708FC
		public static Vector256<double> HorizontalAdd(Vector256<double> left, Vector256<double> right)
		{
			return Avx.HorizontalAdd(left, right);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00171705 File Offset: 0x00170905
		public static Vector256<float> HorizontalSubtract(Vector256<float> left, Vector256<float> right)
		{
			return Avx.HorizontalSubtract(left, right);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x0017170E File Offset: 0x0017090E
		public static Vector256<double> HorizontalSubtract(Vector256<double> left, Vector256<double> right)
		{
			return Avx.HorizontalSubtract(left, right);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00171717 File Offset: 0x00170917
		public static Vector256<byte> InsertVector128(Vector256<byte> value, Vector128<byte> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x00171721 File Offset: 0x00170921
		public static Vector256<sbyte> InsertVector128(Vector256<sbyte> value, Vector128<sbyte> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0017172B File Offset: 0x0017092B
		public static Vector256<short> InsertVector128(Vector256<short> value, Vector128<short> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00171735 File Offset: 0x00170935
		public static Vector256<ushort> InsertVector128(Vector256<ushort> value, Vector128<ushort> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x0017173F File Offset: 0x0017093F
		public static Vector256<int> InsertVector128(Vector256<int> value, Vector128<int> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x00171749 File Offset: 0x00170949
		public static Vector256<uint> InsertVector128(Vector256<uint> value, Vector128<uint> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x00171753 File Offset: 0x00170953
		public static Vector256<long> InsertVector128(Vector256<long> value, Vector128<long> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x0017175D File Offset: 0x0017095D
		public static Vector256<ulong> InsertVector128(Vector256<ulong> value, Vector128<ulong> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00171767 File Offset: 0x00170967
		public static Vector256<float> InsertVector128(Vector256<float> value, Vector128<float> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00171771 File Offset: 0x00170971
		public static Vector256<double> InsertVector128(Vector256<double> value, Vector128<double> data, byte index)
		{
			return Avx.InsertVector128(value, data, index);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x0017177B File Offset: 0x0017097B
		public unsafe static Vector256<sbyte> LoadVector256(sbyte* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00171783 File Offset: 0x00170983
		public unsafe static Vector256<byte> LoadVector256(byte* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x0017178B File Offset: 0x0017098B
		public unsafe static Vector256<short> LoadVector256(short* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00171793 File Offset: 0x00170993
		public unsafe static Vector256<ushort> LoadVector256(ushort* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x0017179B File Offset: 0x0017099B
		public unsafe static Vector256<int> LoadVector256(int* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x001717A3 File Offset: 0x001709A3
		public unsafe static Vector256<uint> LoadVector256(uint* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x001717AB File Offset: 0x001709AB
		public unsafe static Vector256<long> LoadVector256(long* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x001717B3 File Offset: 0x001709B3
		public unsafe static Vector256<ulong> LoadVector256(ulong* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x001717BB File Offset: 0x001709BB
		public unsafe static Vector256<float> LoadVector256(float* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x001717C3 File Offset: 0x001709C3
		public unsafe static Vector256<double> LoadVector256(double* address)
		{
			return Avx.LoadVector256(address);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x001717CB File Offset: 0x001709CB
		public unsafe static Vector256<sbyte> LoadAlignedVector256(sbyte* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x001717D3 File Offset: 0x001709D3
		public unsafe static Vector256<byte> LoadAlignedVector256(byte* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x001717DB File Offset: 0x001709DB
		public unsafe static Vector256<short> LoadAlignedVector256(short* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x001717E3 File Offset: 0x001709E3
		public unsafe static Vector256<ushort> LoadAlignedVector256(ushort* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x001717EB File Offset: 0x001709EB
		public unsafe static Vector256<int> LoadAlignedVector256(int* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x001717F3 File Offset: 0x001709F3
		public unsafe static Vector256<uint> LoadAlignedVector256(uint* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x001717FB File Offset: 0x001709FB
		public unsafe static Vector256<long> LoadAlignedVector256(long* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00171803 File Offset: 0x00170A03
		public unsafe static Vector256<ulong> LoadAlignedVector256(ulong* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x0017180B File Offset: 0x00170A0B
		public unsafe static Vector256<float> LoadAlignedVector256(float* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00171813 File Offset: 0x00170A13
		public unsafe static Vector256<double> LoadAlignedVector256(double* address)
		{
			return Avx.LoadAlignedVector256(address);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x0017181B File Offset: 0x00170A1B
		public unsafe static Vector256<sbyte> LoadDquVector256(sbyte* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00171823 File Offset: 0x00170A23
		public unsafe static Vector256<byte> LoadDquVector256(byte* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x0017182B File Offset: 0x00170A2B
		public unsafe static Vector256<short> LoadDquVector256(short* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00171833 File Offset: 0x00170A33
		public unsafe static Vector256<ushort> LoadDquVector256(ushort* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x0017183B File Offset: 0x00170A3B
		public unsafe static Vector256<int> LoadDquVector256(int* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00171843 File Offset: 0x00170A43
		public unsafe static Vector256<uint> LoadDquVector256(uint* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0017184B File Offset: 0x00170A4B
		public unsafe static Vector256<long> LoadDquVector256(long* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00171853 File Offset: 0x00170A53
		public unsafe static Vector256<ulong> LoadDquVector256(ulong* address)
		{
			return Avx.LoadDquVector256(address);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x0017185B File Offset: 0x00170A5B
		public unsafe static Vector128<float> MaskLoad(float* address, Vector128<float> mask)
		{
			return Avx.MaskLoad(address, mask);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00171864 File Offset: 0x00170A64
		public unsafe static Vector128<double> MaskLoad(double* address, Vector128<double> mask)
		{
			return Avx.MaskLoad(address, mask);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x0017186D File Offset: 0x00170A6D
		public unsafe static Vector256<float> MaskLoad(float* address, Vector256<float> mask)
		{
			return Avx.MaskLoad(address, mask);
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00171876 File Offset: 0x00170A76
		public unsafe static Vector256<double> MaskLoad(double* address, Vector256<double> mask)
		{
			return Avx.MaskLoad(address, mask);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x0017187F File Offset: 0x00170A7F
		public unsafe static void MaskStore(float* address, Vector128<float> mask, Vector128<float> source)
		{
			Avx.MaskStore(address, mask, source);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00171889 File Offset: 0x00170A89
		public unsafe static void MaskStore(double* address, Vector128<double> mask, Vector128<double> source)
		{
			Avx.MaskStore(address, mask, source);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00171893 File Offset: 0x00170A93
		public unsafe static void MaskStore(float* address, Vector256<float> mask, Vector256<float> source)
		{
			Avx.MaskStore(address, mask, source);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x0017189D File Offset: 0x00170A9D
		public unsafe static void MaskStore(double* address, Vector256<double> mask, Vector256<double> source)
		{
			Avx.MaskStore(address, mask, source);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x001718A7 File Offset: 0x00170AA7
		public static Vector256<float> Max(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Max(left, right);
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x001718B0 File Offset: 0x00170AB0
		public static Vector256<double> Max(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Max(left, right);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x001718B9 File Offset: 0x00170AB9
		public static Vector256<float> Min(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Min(left, right);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x001718C2 File Offset: 0x00170AC2
		public static Vector256<double> Min(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Min(left, right);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x001718CB File Offset: 0x00170ACB
		public static int MoveMask(Vector256<float> value)
		{
			return Avx.MoveMask(value);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x001718D3 File Offset: 0x00170AD3
		public static int MoveMask(Vector256<double> value)
		{
			return Avx.MoveMask(value);
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x001718DB File Offset: 0x00170ADB
		public static Vector256<float> Multiply(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Multiply(left, right);
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x001718E4 File Offset: 0x00170AE4
		public static Vector256<double> Multiply(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Multiply(left, right);
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x001718ED File Offset: 0x00170AED
		public static Vector256<float> Or(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Or(left, right);
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001718F6 File Offset: 0x00170AF6
		public static Vector256<double> Or(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Or(left, right);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x001718FF File Offset: 0x00170AFF
		public static Vector128<float> Permute(Vector128<float> value, byte control)
		{
			return Avx.Permute(value, control);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x00171908 File Offset: 0x00170B08
		public static Vector128<double> Permute(Vector128<double> value, byte control)
		{
			return Avx.Permute(value, control);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00171911 File Offset: 0x00170B11
		public static Vector256<float> Permute(Vector256<float> value, byte control)
		{
			return Avx.Permute(value, control);
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0017191A File Offset: 0x00170B1A
		public static Vector256<double> Permute(Vector256<double> value, byte control)
		{
			return Avx.Permute(value, control);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00171923 File Offset: 0x00170B23
		public static Vector256<byte> Permute2x128(Vector256<byte> left, Vector256<byte> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x0017192D File Offset: 0x00170B2D
		public static Vector256<sbyte> Permute2x128(Vector256<sbyte> left, Vector256<sbyte> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00171937 File Offset: 0x00170B37
		public static Vector256<short> Permute2x128(Vector256<short> left, Vector256<short> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x00171941 File Offset: 0x00170B41
		public static Vector256<ushort> Permute2x128(Vector256<ushort> left, Vector256<ushort> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x0017194B File Offset: 0x00170B4B
		public static Vector256<int> Permute2x128(Vector256<int> left, Vector256<int> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00171955 File Offset: 0x00170B55
		public static Vector256<uint> Permute2x128(Vector256<uint> left, Vector256<uint> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x0017195F File Offset: 0x00170B5F
		public static Vector256<long> Permute2x128(Vector256<long> left, Vector256<long> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00171969 File Offset: 0x00170B69
		public static Vector256<ulong> Permute2x128(Vector256<ulong> left, Vector256<ulong> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00171973 File Offset: 0x00170B73
		public static Vector256<float> Permute2x128(Vector256<float> left, Vector256<float> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x0017197D File Offset: 0x00170B7D
		public static Vector256<double> Permute2x128(Vector256<double> left, Vector256<double> right, byte control)
		{
			return Avx.Permute2x128(left, right, control);
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00171987 File Offset: 0x00170B87
		public static Vector128<float> PermuteVar(Vector128<float> left, Vector128<int> control)
		{
			return Avx.PermuteVar(left, control);
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00171990 File Offset: 0x00170B90
		public static Vector128<double> PermuteVar(Vector128<double> left, Vector128<long> control)
		{
			return Avx.PermuteVar(left, control);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00171999 File Offset: 0x00170B99
		public static Vector256<float> PermuteVar(Vector256<float> left, Vector256<int> control)
		{
			return Avx.PermuteVar(left, control);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x001719A2 File Offset: 0x00170BA2
		public static Vector256<double> PermuteVar(Vector256<double> left, Vector256<long> control)
		{
			return Avx.PermuteVar(left, control);
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x001719AB File Offset: 0x00170BAB
		public static Vector256<float> Reciprocal(Vector256<float> value)
		{
			return Avx.Reciprocal(value);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x001719B3 File Offset: 0x00170BB3
		public static Vector256<float> ReciprocalSqrt(Vector256<float> value)
		{
			return Avx.ReciprocalSqrt(value);
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x001719BB File Offset: 0x00170BBB
		public static Vector256<float> RoundToNearestInteger(Vector256<float> value)
		{
			return Avx.RoundToNearestInteger(value);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x001719C3 File Offset: 0x00170BC3
		public static Vector256<float> RoundToNegativeInfinity(Vector256<float> value)
		{
			return Avx.RoundToNegativeInfinity(value);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x001719CB File Offset: 0x00170BCB
		public static Vector256<float> RoundToPositiveInfinity(Vector256<float> value)
		{
			return Avx.RoundToPositiveInfinity(value);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x001719D3 File Offset: 0x00170BD3
		public static Vector256<float> RoundToZero(Vector256<float> value)
		{
			return Avx.RoundToZero(value);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x001719DB File Offset: 0x00170BDB
		public static Vector256<float> RoundCurrentDirection(Vector256<float> value)
		{
			return Avx.RoundCurrentDirection(value);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x001719E3 File Offset: 0x00170BE3
		public static Vector256<double> RoundToNearestInteger(Vector256<double> value)
		{
			return Avx.RoundToNearestInteger(value);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x001719EB File Offset: 0x00170BEB
		public static Vector256<double> RoundToNegativeInfinity(Vector256<double> value)
		{
			return Avx.RoundToNegativeInfinity(value);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x001719F3 File Offset: 0x00170BF3
		public static Vector256<double> RoundToPositiveInfinity(Vector256<double> value)
		{
			return Avx.RoundToPositiveInfinity(value);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x001719FB File Offset: 0x00170BFB
		public static Vector256<double> RoundToZero(Vector256<double> value)
		{
			return Avx.RoundToZero(value);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00171A03 File Offset: 0x00170C03
		public static Vector256<double> RoundCurrentDirection(Vector256<double> value)
		{
			return Avx.RoundCurrentDirection(value);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00171A0B File Offset: 0x00170C0B
		public static Vector256<float> Shuffle(Vector256<float> value, Vector256<float> right, byte control)
		{
			return Avx.Shuffle(value, right, control);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00171A15 File Offset: 0x00170C15
		public static Vector256<double> Shuffle(Vector256<double> value, Vector256<double> right, byte control)
		{
			return Avx.Shuffle(value, right, control);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00171A1F File Offset: 0x00170C1F
		public static Vector256<float> Sqrt(Vector256<float> value)
		{
			return Avx.Sqrt(value);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x00171A27 File Offset: 0x00170C27
		public static Vector256<double> Sqrt(Vector256<double> value)
		{
			return Avx.Sqrt(value);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x00171A2F File Offset: 0x00170C2F
		public unsafe static void StoreAligned(sbyte* address, Vector256<sbyte> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00171A38 File Offset: 0x00170C38
		public unsafe static void StoreAligned(byte* address, Vector256<byte> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00171A41 File Offset: 0x00170C41
		public unsafe static void StoreAligned(short* address, Vector256<short> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00171A4A File Offset: 0x00170C4A
		public unsafe static void StoreAligned(ushort* address, Vector256<ushort> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00171A53 File Offset: 0x00170C53
		public unsafe static void StoreAligned(int* address, Vector256<int> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00171A5C File Offset: 0x00170C5C
		public unsafe static void StoreAligned(uint* address, Vector256<uint> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00171A65 File Offset: 0x00170C65
		public unsafe static void StoreAligned(long* address, Vector256<long> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00171A6E File Offset: 0x00170C6E
		public unsafe static void StoreAligned(ulong* address, Vector256<ulong> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00171A77 File Offset: 0x00170C77
		public unsafe static void StoreAligned(float* address, Vector256<float> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00171A80 File Offset: 0x00170C80
		public unsafe static void StoreAligned(double* address, Vector256<double> source)
		{
			Avx.StoreAligned(address, source);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00171A89 File Offset: 0x00170C89
		public unsafe static void StoreAlignedNonTemporal(sbyte* address, Vector256<sbyte> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00171A92 File Offset: 0x00170C92
		public unsafe static void StoreAlignedNonTemporal(byte* address, Vector256<byte> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00171A9B File Offset: 0x00170C9B
		public unsafe static void StoreAlignedNonTemporal(short* address, Vector256<short> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00171AA4 File Offset: 0x00170CA4
		public unsafe static void StoreAlignedNonTemporal(ushort* address, Vector256<ushort> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00171AAD File Offset: 0x00170CAD
		public unsafe static void StoreAlignedNonTemporal(int* address, Vector256<int> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00171AB6 File Offset: 0x00170CB6
		public unsafe static void StoreAlignedNonTemporal(uint* address, Vector256<uint> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00171ABF File Offset: 0x00170CBF
		public unsafe static void StoreAlignedNonTemporal(long* address, Vector256<long> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00171AC8 File Offset: 0x00170CC8
		public unsafe static void StoreAlignedNonTemporal(ulong* address, Vector256<ulong> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x00171AD1 File Offset: 0x00170CD1
		public unsafe static void StoreAlignedNonTemporal(float* address, Vector256<float> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x00171ADA File Offset: 0x00170CDA
		public unsafe static void StoreAlignedNonTemporal(double* address, Vector256<double> source)
		{
			Avx.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x00171AE3 File Offset: 0x00170CE3
		public unsafe static void Store(sbyte* address, Vector256<sbyte> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00171AEC File Offset: 0x00170CEC
		public unsafe static void Store(byte* address, Vector256<byte> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x00171AF5 File Offset: 0x00170CF5
		public unsafe static void Store(short* address, Vector256<short> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00171AFE File Offset: 0x00170CFE
		public unsafe static void Store(ushort* address, Vector256<ushort> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x00171B07 File Offset: 0x00170D07
		public unsafe static void Store(int* address, Vector256<int> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00171B10 File Offset: 0x00170D10
		public unsafe static void Store(uint* address, Vector256<uint> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00171B19 File Offset: 0x00170D19
		public unsafe static void Store(long* address, Vector256<long> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00171B22 File Offset: 0x00170D22
		public unsafe static void Store(ulong* address, Vector256<ulong> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00171B2B File Offset: 0x00170D2B
		public unsafe static void Store(float* address, Vector256<float> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00171B34 File Offset: 0x00170D34
		public unsafe static void Store(double* address, Vector256<double> source)
		{
			Avx.Store(address, source);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00171B3D File Offset: 0x00170D3D
		public static Vector256<float> Subtract(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Subtract(left, right);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00171B46 File Offset: 0x00170D46
		public static Vector256<double> Subtract(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Subtract(left, right);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00171B4F File Offset: 0x00170D4F
		public static bool TestC(Vector128<float> left, Vector128<float> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00171B58 File Offset: 0x00170D58
		public static bool TestC(Vector128<double> left, Vector128<double> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00171B61 File Offset: 0x00170D61
		public static bool TestC(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00171B6A File Offset: 0x00170D6A
		public static bool TestC(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00171B73 File Offset: 0x00170D73
		public static bool TestC(Vector256<short> left, Vector256<short> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00171B7C File Offset: 0x00170D7C
		public static bool TestC(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00171B85 File Offset: 0x00170D85
		public static bool TestC(Vector256<int> left, Vector256<int> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00171B8E File Offset: 0x00170D8E
		public static bool TestC(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00171B97 File Offset: 0x00170D97
		public static bool TestC(Vector256<long> left, Vector256<long> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00171BA0 File Offset: 0x00170DA0
		public static bool TestC(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00171BA9 File Offset: 0x00170DA9
		public static bool TestC(Vector256<float> left, Vector256<float> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00171BB2 File Offset: 0x00170DB2
		public static bool TestC(Vector256<double> left, Vector256<double> right)
		{
			return Avx.TestC(left, right);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00171BBB File Offset: 0x00170DBB
		public static bool TestNotZAndNotC(Vector128<float> left, Vector128<float> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00171BC4 File Offset: 0x00170DC4
		public static bool TestNotZAndNotC(Vector128<double> left, Vector128<double> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00171BCD File Offset: 0x00170DCD
		public static bool TestNotZAndNotC(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00171BD6 File Offset: 0x00170DD6
		public static bool TestNotZAndNotC(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00171BDF File Offset: 0x00170DDF
		public static bool TestNotZAndNotC(Vector256<short> left, Vector256<short> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00171BE8 File Offset: 0x00170DE8
		public static bool TestNotZAndNotC(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00171BF1 File Offset: 0x00170DF1
		public static bool TestNotZAndNotC(Vector256<int> left, Vector256<int> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00171BFA File Offset: 0x00170DFA
		public static bool TestNotZAndNotC(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x00171C03 File Offset: 0x00170E03
		public static bool TestNotZAndNotC(Vector256<long> left, Vector256<long> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x00171C0C File Offset: 0x00170E0C
		public static bool TestNotZAndNotC(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00171C15 File Offset: 0x00170E15
		public static bool TestNotZAndNotC(Vector256<float> left, Vector256<float> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00171C1E File Offset: 0x00170E1E
		public static bool TestNotZAndNotC(Vector256<double> left, Vector256<double> right)
		{
			return Avx.TestNotZAndNotC(left, right);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00171C27 File Offset: 0x00170E27
		public static bool TestZ(Vector128<float> left, Vector128<float> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00171C30 File Offset: 0x00170E30
		public static bool TestZ(Vector128<double> left, Vector128<double> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00171C39 File Offset: 0x00170E39
		public static bool TestZ(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00171C42 File Offset: 0x00170E42
		public static bool TestZ(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00171C4B File Offset: 0x00170E4B
		public static bool TestZ(Vector256<short> left, Vector256<short> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x00171C54 File Offset: 0x00170E54
		public static bool TestZ(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00171C5D File Offset: 0x00170E5D
		public static bool TestZ(Vector256<int> left, Vector256<int> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00171C66 File Offset: 0x00170E66
		public static bool TestZ(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x00171C6F File Offset: 0x00170E6F
		public static bool TestZ(Vector256<long> left, Vector256<long> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x00171C78 File Offset: 0x00170E78
		public static bool TestZ(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x00171C81 File Offset: 0x00170E81
		public static bool TestZ(Vector256<float> left, Vector256<float> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x00171C8A File Offset: 0x00170E8A
		public static bool TestZ(Vector256<double> left, Vector256<double> right)
		{
			return Avx.TestZ(left, right);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00171C93 File Offset: 0x00170E93
		public static Vector256<float> UnpackHigh(Vector256<float> left, Vector256<float> right)
		{
			return Avx.UnpackHigh(left, right);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00171C9C File Offset: 0x00170E9C
		public static Vector256<double> UnpackHigh(Vector256<double> left, Vector256<double> right)
		{
			return Avx.UnpackHigh(left, right);
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00171CA5 File Offset: 0x00170EA5
		public static Vector256<float> UnpackLow(Vector256<float> left, Vector256<float> right)
		{
			return Avx.UnpackLow(left, right);
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x00171CAE File Offset: 0x00170EAE
		public static Vector256<double> UnpackLow(Vector256<double> left, Vector256<double> right)
		{
			return Avx.UnpackLow(left, right);
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x00171CB7 File Offset: 0x00170EB7
		public static Vector256<float> Xor(Vector256<float> left, Vector256<float> right)
		{
			return Avx.Xor(left, right);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x00171CC0 File Offset: 0x00170EC0
		public static Vector256<double> Xor(Vector256<double> left, Vector256<double> right)
		{
			return Avx.Xor(left, right);
		}

		// Token: 0x02000430 RID: 1072
		[Intrinsic]
		public new abstract class X64 : Sse42.X64
		{
			// Token: 0x17000A24 RID: 2596
			// (get) Token: 0x06003E97 RID: 16023 RVA: 0x00171CC9 File Offset: 0x00170EC9
			public new static bool IsSupported
			{
				get
				{
					return Avx.X64.IsSupported;
				}
			}
		}
	}
}
