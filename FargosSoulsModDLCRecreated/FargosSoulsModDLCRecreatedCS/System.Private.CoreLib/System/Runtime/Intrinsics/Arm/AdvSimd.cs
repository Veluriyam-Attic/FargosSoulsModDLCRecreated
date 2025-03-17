using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x0200041A RID: 1050
	[CLSCompliant(false)]
	public abstract class AdvSimd : ArmBase
	{
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Abs(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Abs(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Abs(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Abs(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Abs(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Abs(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Abs(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Abs(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AbsSaturate(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AbsSaturate(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AbsSaturate(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AbsSaturate(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AbsSaturate(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> AbsSaturate(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> AbsScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsoluteCompareGreaterThan(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> AbsoluteCompareGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsoluteCompareGreaterThanOrEqual(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> AbsoluteCompareGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsoluteCompareLessThan(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> AbsoluteCompareLessThan(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsoluteCompareLessThanOrEqual(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> AbsoluteCompareLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AbsoluteDifference(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AbsoluteDifference(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AbsoluteDifference(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AbsoluteDifference(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AbsoluteDifference(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AbsoluteDifference(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AbsoluteDifference(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AbsoluteDifference(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifference(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifference(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AbsoluteDifference(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> AbsoluteDifference(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifference(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifference(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AbsoluteDifferenceAdd(Vector64<byte> addend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AbsoluteDifferenceAdd(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AbsoluteDifferenceAdd(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AbsoluteDifferenceAdd(Vector64<sbyte> addend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AbsoluteDifferenceAdd(Vector64<ushort> addend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AbsoluteDifferenceAdd(Vector64<uint> addend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AbsoluteDifferenceAdd(Vector128<byte> addend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AbsoluteDifferenceAdd(Vector128<short> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AbsoluteDifferenceAdd(Vector128<int> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> AbsoluteDifferenceAdd(Vector128<sbyte> addend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceAdd(Vector128<ushort> addend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceAdd(Vector128<uint> addend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningLower(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningLower(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningLower(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningLower(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningLower(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningLower(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningLowerAndAdd(Vector128<ushort> addend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AbsoluteDifferenceWideningLowerAndAdd(Vector128<int> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AbsoluteDifferenceWideningLowerAndAdd(Vector128<long> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AbsoluteDifferenceWideningLowerAndAdd(Vector128<short> addend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningLowerAndAdd(Vector128<uint> addend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningLowerAndAdd(Vector128<ulong> addend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningUpper(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningUpper(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningUpper(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningUpper(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningUpper(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningUpper(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AbsoluteDifferenceWideningUpperAndAdd(Vector128<ushort> addend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AbsoluteDifferenceWideningUpperAndAdd(Vector128<int> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AbsoluteDifferenceWideningUpperAndAdd(Vector128<long> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AbsoluteDifferenceWideningUpperAndAdd(Vector128<short> addend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AbsoluteDifferenceWideningUpperAndAdd(Vector128<uint> addend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AbsoluteDifferenceWideningUpperAndAdd(Vector128<ulong> addend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Add(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Add(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Add(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Add(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Add(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Add(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Add(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Add(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Add(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Add(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Add(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Add(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Add(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Add(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Add(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Add(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AddHighNarrowingLower(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddHighNarrowingLower(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddHighNarrowingLower(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AddHighNarrowingLower(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddHighNarrowingLower(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddHighNarrowingLower(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AddHighNarrowingUpper(Vector64<byte> lower, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddHighNarrowingUpper(Vector64<short> lower, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddHighNarrowingUpper(Vector64<int> lower, Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> AddHighNarrowingUpper(Vector64<sbyte> lower, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddHighNarrowingUpper(Vector64<ushort> lower, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddHighNarrowingUpper(Vector64<uint> lower, Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AddPairwise(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddPairwise(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddPairwise(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AddPairwise(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AddPairwise(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddPairwise(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddPairwise(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddPairwiseWidening(Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddPairwiseWidening(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddPairwiseWidening(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddPairwiseWidening(Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddPairwiseWidening(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddPairwiseWidening(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddPairwiseWidening(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddPairwiseWidening(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddPairwiseWidening(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddPairwiseWidening(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddPairwiseWideningAndAdd(Vector64<ushort> addend, Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddPairwiseWideningAndAdd(Vector64<int> addend, Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddPairwiseWideningAndAdd(Vector64<short> addend, Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddPairwiseWideningAndAdd(Vector64<uint> addend, Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddPairwiseWideningAndAdd(Vector128<ushort> addend, Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddPairwiseWideningAndAdd(Vector128<int> addend, Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddPairwiseWideningAndAdd(Vector128<long> addend, Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddPairwiseWideningAndAdd(Vector128<short> addend, Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddPairwiseWideningAndAdd(Vector128<uint> addend, Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddPairwiseWideningAndAdd(Vector128<ulong> addend, Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> AddPairwiseWideningAndAddScalar(Vector64<long> addend, Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> AddPairwiseWideningAndAddScalar(Vector64<ulong> addend, Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> AddPairwiseWideningScalar(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> AddPairwiseWideningScalar(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AddRoundedHighNarrowingLower(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddRoundedHighNarrowingLower(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddRoundedHighNarrowingLower(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AddRoundedHighNarrowingLower(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddRoundedHighNarrowingLower(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddRoundedHighNarrowingLower(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AddRoundedHighNarrowingUpper(Vector64<byte> lower, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddRoundedHighNarrowingUpper(Vector64<short> lower, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddRoundedHighNarrowingUpper(Vector64<int> lower, Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> AddRoundedHighNarrowingUpper(Vector64<sbyte> lower, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddRoundedHighNarrowingUpper(Vector64<ushort> lower, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddRoundedHighNarrowingUpper(Vector64<uint> lower, Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> AddSaturate(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> AddSaturate(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> AddSaturate(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> AddSaturate(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> AddSaturate(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> AddSaturate(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> AddSaturate(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddSaturate(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddSaturate(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddSaturate(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> AddSaturate(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddSaturate(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddSaturate(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddSaturate(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> AddSaturateScalar(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> AddSaturateScalar(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> AddScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> AddScalar(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> AddScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> AddScalar(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddWideningLower(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddWideningLower(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddWideningLower(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddWideningLower(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddWideningLower(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddWideningLower(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddWideningLower(Vector128<short> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddWideningLower(Vector128<int> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddWideningLower(Vector128<long> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddWideningLower(Vector128<ushort> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddWideningLower(Vector128<uint> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddWideningLower(Vector128<ulong> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddWideningUpper(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddWideningUpper(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddWideningUpper(Vector128<short> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> AddWideningUpper(Vector128<int> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddWideningUpper(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> AddWideningUpper(Vector128<long> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> AddWideningUpper(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> AddWideningUpper(Vector128<ushort> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddWideningUpper(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> AddWideningUpper(Vector128<uint> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddWideningUpper(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> AddWideningUpper(Vector128<ulong> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> And(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> And(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> And(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> And(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> And(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> And(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> And(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> And(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> And(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> And(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> And(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> And(Vector128<double> left, Vector128<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> And(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> And(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> And(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> And(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> And(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> And(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> And(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> And(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> BitwiseClear(Vector64<byte> value, Vector64<byte> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> BitwiseClear(Vector64<double> value, Vector64<double> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> BitwiseClear(Vector64<short> value, Vector64<short> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> BitwiseClear(Vector64<int> value, Vector64<int> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> BitwiseClear(Vector64<long> value, Vector64<long> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> BitwiseClear(Vector64<sbyte> value, Vector64<sbyte> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> BitwiseClear(Vector64<float> value, Vector64<float> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> BitwiseClear(Vector64<ushort> value, Vector64<ushort> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> BitwiseClear(Vector64<uint> value, Vector64<uint> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> BitwiseClear(Vector64<ulong> value, Vector64<ulong> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> BitwiseClear(Vector128<byte> value, Vector128<byte> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> BitwiseClear(Vector128<double> value, Vector128<double> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> BitwiseClear(Vector128<short> value, Vector128<short> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> BitwiseClear(Vector128<int> value, Vector128<int> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> BitwiseClear(Vector128<long> value, Vector128<long> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> BitwiseClear(Vector128<sbyte> value, Vector128<sbyte> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> BitwiseClear(Vector128<float> value, Vector128<float> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> BitwiseClear(Vector128<ushort> value, Vector128<ushort> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> BitwiseClear(Vector128<uint> value, Vector128<uint> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> BitwiseClear(Vector128<ulong> value, Vector128<ulong> mask)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> BitwiseSelect(Vector64<byte> select, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> BitwiseSelect(Vector64<double> select, Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> BitwiseSelect(Vector64<short> select, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> BitwiseSelect(Vector64<int> select, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> BitwiseSelect(Vector64<long> select, Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> BitwiseSelect(Vector64<sbyte> select, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> BitwiseSelect(Vector64<float> select, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> BitwiseSelect(Vector64<ushort> select, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> BitwiseSelect(Vector64<uint> select, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> BitwiseSelect(Vector64<ulong> select, Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> BitwiseSelect(Vector128<byte> select, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> BitwiseSelect(Vector128<double> select, Vector128<double> left, Vector128<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> BitwiseSelect(Vector128<short> select, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> BitwiseSelect(Vector128<int> select, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> BitwiseSelect(Vector128<long> select, Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> BitwiseSelect(Vector128<sbyte> select, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> BitwiseSelect(Vector128<float> select, Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> BitwiseSelect(Vector128<ushort> select, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> BitwiseSelect(Vector128<uint> select, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> BitwiseSelect(Vector128<ulong> select, Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Ceiling(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Ceiling(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> CeilingScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CeilingScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareEqual(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareEqual(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareEqual(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareEqual(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareEqual(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareEqual(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareEqual(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareEqual(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareEqual(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareEqual(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareEqual(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareEqual(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareEqual(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareEqual(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareGreaterThan(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareGreaterThan(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareGreaterThan(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareGreaterThan(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareGreaterThan(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareGreaterThan(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareGreaterThan(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareGreaterThan(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareGreaterThan(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareGreaterThan(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareGreaterThan(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareGreaterThan(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareGreaterThan(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareGreaterThanOrEqual(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareGreaterThanOrEqual(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareGreaterThanOrEqual(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareGreaterThanOrEqual(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareGreaterThanOrEqual(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareGreaterThanOrEqual(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareGreaterThanOrEqual(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareGreaterThanOrEqual(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareGreaterThanOrEqual(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareGreaterThanOrEqual(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareGreaterThanOrEqual(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareGreaterThanOrEqual(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareGreaterThanOrEqual(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareLessThan(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareLessThan(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareLessThan(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareLessThan(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareLessThan(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareLessThan(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareLessThan(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareLessThan(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareLessThan(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareLessThan(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareLessThan(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareLessThan(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareLessThan(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareLessThan(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareLessThanOrEqual(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareLessThanOrEqual(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareLessThanOrEqual(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareLessThanOrEqual(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareLessThanOrEqual(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareLessThanOrEqual(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareLessThanOrEqual(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareLessThanOrEqual(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareLessThanOrEqual(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareLessThanOrEqual(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareLessThanOrEqual(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareLessThanOrEqual(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareLessThanOrEqual(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> CompareTest(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> CompareTest(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> CompareTest(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> CompareTest(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> CompareTest(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> CompareTest(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> CompareTest(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> CompareTest(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> CompareTest(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> CompareTest(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> CompareTest(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> CompareTest(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> CompareTest(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> CompareTest(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundAwayFromZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ConvertToInt32RoundAwayFromZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundAwayFromZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToEven(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ConvertToInt32RoundToEven(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToEvenScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToNegativeInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ConvertToInt32RoundToNegativeInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToNegativeInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToPositiveInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ConvertToInt32RoundToPositiveInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToPositiveInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ConvertToInt32RoundToZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ConvertToInt32RoundToZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ConvertToSingle(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ConvertToSingle(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ConvertToSingle(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ConvertToSingle(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ConvertToSingleScalar(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ConvertToSingleScalar(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundAwayFromZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ConvertToUInt32RoundAwayFromZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundAwayFromZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToEven(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ConvertToUInt32RoundToEven(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToEvenScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToNegativeInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ConvertToUInt32RoundToNegativeInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToNegativeInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToPositiveInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ConvertToUInt32RoundToPositiveInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToPositiveInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ConvertToUInt32RoundToZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ConvertToUInt32RoundToZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> DivideScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> DivideScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> DuplicateSelectedScalarToVector64(Vector64<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> DuplicateSelectedScalarToVector64(Vector64<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DuplicateSelectedScalarToVector64(Vector64<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> DuplicateSelectedScalarToVector64(Vector64<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> DuplicateSelectedScalarToVector64(Vector64<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> DuplicateSelectedScalarToVector64(Vector64<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DuplicateSelectedScalarToVector64(Vector64<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> DuplicateSelectedScalarToVector64(Vector128<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> DuplicateSelectedScalarToVector64(Vector128<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DuplicateSelectedScalarToVector64(Vector128<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> DuplicateSelectedScalarToVector64(Vector128<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> DuplicateSelectedScalarToVector64(Vector128<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> DuplicateSelectedScalarToVector64(Vector128<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DuplicateSelectedScalarToVector64(Vector128<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> DuplicateSelectedScalarToVector128(Vector64<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> DuplicateSelectedScalarToVector128(Vector64<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DuplicateSelectedScalarToVector128(Vector64<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> DuplicateSelectedScalarToVector128(Vector64<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> DuplicateSelectedScalarToVector128(Vector64<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> DuplicateSelectedScalarToVector128(Vector64<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DuplicateSelectedScalarToVector128(Vector64<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> DuplicateSelectedScalarToVector128(Vector128<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> DuplicateSelectedScalarToVector128(Vector128<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DuplicateSelectedScalarToVector128(Vector128<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> DuplicateSelectedScalarToVector128(Vector128<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> DuplicateSelectedScalarToVector128(Vector128<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> DuplicateSelectedScalarToVector128(Vector128<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DuplicateSelectedScalarToVector128(Vector128<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> DuplicateToVector64(byte value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> DuplicateToVector64(short value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DuplicateToVector64(int value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> DuplicateToVector64(sbyte value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> DuplicateToVector64(float value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> DuplicateToVector64(ushort value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DuplicateToVector64(uint value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> DuplicateToVector128(byte value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> DuplicateToVector128(short value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DuplicateToVector128(int value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> DuplicateToVector128(sbyte value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> DuplicateToVector128(float value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> DuplicateToVector128(ushort value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DuplicateToVector128(uint value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000B3617 File Offset: 0x000B2817
		public static byte Extract(Vector64<byte> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000B3617 File Offset: 0x000B2817
		public static short Extract(Vector64<short> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000B3617 File Offset: 0x000B2817
		public static int Extract(Vector64<int> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000B3617 File Offset: 0x000B2817
		public static sbyte Extract(Vector64<sbyte> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000B3617 File Offset: 0x000B2817
		public static float Extract(Vector64<float> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000B3617 File Offset: 0x000B2817
		public static ushort Extract(Vector64<ushort> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint Extract(Vector64<uint> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000B3617 File Offset: 0x000B2817
		public static byte Extract(Vector128<byte> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000B3617 File Offset: 0x000B2817
		public static double Extract(Vector128<double> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000B3617 File Offset: 0x000B2817
		public static short Extract(Vector128<short> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000B3617 File Offset: 0x000B2817
		public static int Extract(Vector128<int> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000B3617 File Offset: 0x000B2817
		public static long Extract(Vector128<long> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000B3617 File Offset: 0x000B2817
		public static sbyte Extract(Vector128<sbyte> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000B3617 File Offset: 0x000B2817
		public static float Extract(Vector128<float> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000B3617 File Offset: 0x000B2817
		public static ushort Extract(Vector128<ushort> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint Extract(Vector128<uint> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000B3617 File Offset: 0x000B2817
		public static ulong Extract(Vector128<ulong> vector, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ExtractNarrowingLower(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ExtractNarrowingLower(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ExtractNarrowingLower(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ExtractNarrowingLower(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ExtractNarrowingLower(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ExtractNarrowingLower(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ExtractNarrowingSaturateLower(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ExtractNarrowingSaturateLower(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ExtractNarrowingSaturateLower(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ExtractNarrowingSaturateLower(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ExtractNarrowingSaturateLower(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ExtractNarrowingSaturateLower(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ExtractNarrowingSaturateUnsignedLower(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ExtractNarrowingSaturateUnsignedLower(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ExtractNarrowingSaturateUnsignedLower(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ExtractNarrowingSaturateUnsignedUpper(Vector64<byte> lower, Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ExtractNarrowingSaturateUnsignedUpper(Vector64<ushort> lower, Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ExtractNarrowingSaturateUnsignedUpper(Vector64<uint> lower, Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ExtractNarrowingSaturateUpper(Vector64<byte> lower, Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ExtractNarrowingSaturateUpper(Vector64<short> lower, Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ExtractNarrowingSaturateUpper(Vector64<int> lower, Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ExtractNarrowingSaturateUpper(Vector64<sbyte> lower, Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ExtractNarrowingSaturateUpper(Vector64<ushort> lower, Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ExtractNarrowingSaturateUpper(Vector64<uint> lower, Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ExtractNarrowingUpper(Vector64<byte> lower, Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ExtractNarrowingUpper(Vector64<short> lower, Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ExtractNarrowingUpper(Vector64<int> lower, Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ExtractNarrowingUpper(Vector64<sbyte> lower, Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ExtractNarrowingUpper(Vector64<ushort> lower, Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ExtractNarrowingUpper(Vector64<uint> lower, Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ExtractVector64(Vector64<byte> upper, Vector64<byte> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ExtractVector64(Vector64<short> upper, Vector64<short> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ExtractVector64(Vector64<int> upper, Vector64<int> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ExtractVector64(Vector64<sbyte> upper, Vector64<sbyte> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ExtractVector64(Vector64<float> upper, Vector64<float> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ExtractVector64(Vector64<ushort> upper, Vector64<ushort> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ExtractVector64(Vector64<uint> upper, Vector64<uint> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ExtractVector128(Vector128<byte> upper, Vector128<byte> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> ExtractVector128(Vector128<double> upper, Vector128<double> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ExtractVector128(Vector128<short> upper, Vector128<short> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ExtractVector128(Vector128<int> upper, Vector128<int> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ExtractVector128(Vector128<long> upper, Vector128<long> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ExtractVector128(Vector128<sbyte> upper, Vector128<sbyte> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ExtractVector128(Vector128<float> upper, Vector128<float> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ExtractVector128(Vector128<ushort> upper, Vector128<ushort> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ExtractVector128(Vector128<uint> upper, Vector128<uint> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ExtractVector128(Vector128<ulong> upper, Vector128<ulong> lower, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Floor(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Floor(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> FloorScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FloorScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> FusedAddHalving(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> FusedAddHalving(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> FusedAddHalving(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> FusedAddHalving(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> FusedAddHalving(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> FusedAddHalving(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> FusedAddHalving(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> FusedAddHalving(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> FusedAddHalving(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> FusedAddHalving(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> FusedAddHalving(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> FusedAddHalving(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> FusedAddRoundedHalving(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> FusedAddRoundedHalving(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> FusedAddRoundedHalving(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> FusedAddRoundedHalving(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> FusedAddRoundedHalving(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> FusedAddRoundedHalving(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> FusedAddRoundedHalving(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> FusedAddRoundedHalving(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> FusedAddRoundedHalving(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> FusedAddRoundedHalving(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> FusedAddRoundedHalving(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> FusedAddRoundedHalving(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplyAdd(Vector64<float> addend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> FusedMultiplyAdd(Vector128<float> addend, Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> FusedMultiplyAddNegatedScalar(Vector64<double> addend, Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplyAddNegatedScalar(Vector64<float> addend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> FusedMultiplyAddScalar(Vector64<double> addend, Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplyAddScalar(Vector64<float> addend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplySubtract(Vector64<float> minuend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> FusedMultiplySubtract(Vector128<float> minuend, Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> FusedMultiplySubtractNegatedScalar(Vector64<double> minuend, Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplySubtractNegatedScalar(Vector64<float> minuend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> FusedMultiplySubtractScalar(Vector64<double> minuend, Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> FusedMultiplySubtractScalar(Vector64<float> minuend, Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> FusedSubtractHalving(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> FusedSubtractHalving(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> FusedSubtractHalving(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> FusedSubtractHalving(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> FusedSubtractHalving(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> FusedSubtractHalving(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> FusedSubtractHalving(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> FusedSubtractHalving(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> FusedSubtractHalving(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> FusedSubtractHalving(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> FusedSubtractHalving(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> FusedSubtractHalving(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Insert(Vector64<byte> vector, byte index, byte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Insert(Vector64<short> vector, byte index, short data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Insert(Vector64<int> vector, byte index, int data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Insert(Vector64<sbyte> vector, byte index, sbyte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Insert(Vector64<float> vector, byte index, float data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Insert(Vector64<ushort> vector, byte index, ushort data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Insert(Vector64<uint> vector, byte index, uint data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Insert(Vector128<byte> vector, byte index, byte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> Insert(Vector128<double> vector, byte index, double data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Insert(Vector128<short> vector, byte index, short data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Insert(Vector128<int> vector, byte index, int data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Insert(Vector128<long> vector, byte index, long data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Insert(Vector128<sbyte> vector, byte index, sbyte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Insert(Vector128<float> vector, byte index, float data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Insert(Vector128<ushort> vector, byte index, ushort data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Insert(Vector128<uint> vector, byte index, uint data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Insert(Vector128<ulong> vector, byte index, ulong data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> InsertScalar(Vector128<double> result, byte resultIndex, Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> InsertScalar(Vector128<long> result, byte resultIndex, Vector64<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> InsertScalar(Vector128<ulong> result, byte resultIndex, Vector64<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> LeadingSignCount(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> LeadingSignCount(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> LeadingSignCount(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> LeadingSignCount(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> LeadingSignCount(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> LeadingSignCount(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> LeadingZeroCount(Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> LeadingZeroCount(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> LeadingZeroCount(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> LeadingZeroCount(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> LeadingZeroCount(Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> LeadingZeroCount(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> LeadingZeroCount(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> LeadingZeroCount(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> LeadingZeroCount(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> LeadingZeroCount(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> LeadingZeroCount(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> LeadingZeroCount(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<byte> LoadAndInsertScalar(Vector64<byte> value, byte index, byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<short> LoadAndInsertScalar(Vector64<short> value, byte index, short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<int> LoadAndInsertScalar(Vector64<int> value, byte index, int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<sbyte> LoadAndInsertScalar(Vector64<sbyte> value, byte index, sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<float> LoadAndInsertScalar(Vector64<float> value, byte index, float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<ushort> LoadAndInsertScalar(Vector64<ushort> value, byte index, ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<uint> LoadAndInsertScalar(Vector64<uint> value, byte index, uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<byte> LoadAndInsertScalar(Vector128<byte> value, byte index, byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<double> LoadAndInsertScalar(Vector128<double> value, byte index, double* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<short> LoadAndInsertScalar(Vector128<short> value, byte index, short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<int> LoadAndInsertScalar(Vector128<int> value, byte index, int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<long> LoadAndInsertScalar(Vector128<long> value, byte index, long* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<sbyte> LoadAndInsertScalar(Vector128<sbyte> value, byte index, sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<float> LoadAndInsertScalar(Vector128<float> value, byte index, float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<ushort> LoadAndInsertScalar(Vector128<ushort> value, byte index, ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<uint> LoadAndInsertScalar(Vector128<uint> value, byte index, uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<ulong> LoadAndInsertScalar(Vector128<ulong> value, byte index, ulong* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<byte> LoadAndReplicateToVector64(byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<short> LoadAndReplicateToVector64(short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<int> LoadAndReplicateToVector64(int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<sbyte> LoadAndReplicateToVector64(sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<float> LoadAndReplicateToVector64(float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<ushort> LoadAndReplicateToVector64(ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<uint> LoadAndReplicateToVector64(uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<byte> LoadAndReplicateToVector128(byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<short> LoadAndReplicateToVector128(short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<int> LoadAndReplicateToVector128(int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<sbyte> LoadAndReplicateToVector128(sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<float> LoadAndReplicateToVector128(float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<ushort> LoadAndReplicateToVector128(ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<uint> LoadAndReplicateToVector128(uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<byte> LoadVector64(byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<double> LoadVector64(double* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<short> LoadVector64(short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<int> LoadVector64(int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<long> LoadVector64(long* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<sbyte> LoadVector64(sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<float> LoadVector64(float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<ushort> LoadVector64(ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<uint> LoadVector64(uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector64<ulong> LoadVector64(ulong* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<byte> LoadVector128(byte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<double> LoadVector128(double* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<short> LoadVector128(short* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<int> LoadVector128(int* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<long> LoadVector128(long* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<sbyte> LoadVector128(sbyte* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<float> LoadVector128(float* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<ushort> LoadVector128(ushort* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<uint> LoadVector128(uint* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static Vector128<ulong> LoadVector128(ulong* address)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Max(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Max(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Max(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Max(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Max(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Max(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Max(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Max(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Max(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Max(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Max(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Max(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Max(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Max(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MaxNumber(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> MaxNumber(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> MaxNumberScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MaxNumberScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> MaxPairwise(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MaxPairwise(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MaxPairwise(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> MaxPairwise(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MaxPairwise(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MaxPairwise(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MaxPairwise(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Min(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Min(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Min(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Min(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Min(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Min(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Min(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Min(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Min(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Min(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Min(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Min(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Min(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Min(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MinNumber(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> MinNumber(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> MinNumberScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MinNumberScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> MinPairwise(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MinPairwise(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MinPairwise(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> MinPairwise(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MinPairwise(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MinPairwise(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MinPairwise(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Multiply(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Multiply(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Multiply(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Multiply(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Multiply(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Multiply(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Multiply(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Multiply(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Multiply(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Multiply(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Multiply(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Multiply(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Multiply(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Multiply(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> MultiplyAdd(Vector64<byte> addend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyAdd(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyAdd(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> MultiplyAdd(Vector64<sbyte> addend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyAdd(Vector64<ushort> addend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyAdd(Vector64<uint> addend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> MultiplyAdd(Vector128<byte> addend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyAdd(Vector128<short> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyAdd(Vector128<int> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> MultiplyAdd(Vector128<sbyte> addend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyAdd(Vector128<ushort> addend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyAdd(Vector128<uint> addend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyAddByScalar(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyAddByScalar(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyAddByScalar(Vector64<ushort> addend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyAddByScalar(Vector64<uint> addend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyAddByScalar(Vector128<short> addend, Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyAddByScalar(Vector128<int> addend, Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyAddByScalar(Vector128<ushort> addend, Vector128<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyAddByScalar(Vector128<uint> addend, Vector128<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyAddBySelectedScalar(Vector64<short> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyAddBySelectedScalar(Vector64<short> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyAddBySelectedScalar(Vector64<int> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyAddBySelectedScalar(Vector64<int> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyAddBySelectedScalar(Vector64<ushort> addend, Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyAddBySelectedScalar(Vector64<ushort> addend, Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyAddBySelectedScalar(Vector64<uint> addend, Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyAddBySelectedScalar(Vector64<uint> addend, Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyAddBySelectedScalar(Vector128<short> addend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyAddBySelectedScalar(Vector128<short> addend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyAddBySelectedScalar(Vector128<int> addend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyAddBySelectedScalar(Vector128<int> addend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyAddBySelectedScalar(Vector128<ushort> addend, Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyAddBySelectedScalar(Vector128<ushort> addend, Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyAddBySelectedScalar(Vector128<uint> addend, Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyAddBySelectedScalar(Vector128<uint> addend, Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyByScalar(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyByScalar(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyByScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyByScalar(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyByScalar(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyByScalar(Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyByScalar(Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> MultiplyByScalar(Vector128<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyByScalar(Vector128<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyByScalar(Vector128<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyBySelectedScalar(Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyBySelectedScalar(Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyBySelectedScalar(Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyBySelectedScalar(Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyBySelectedScalar(Vector64<float> left, Vector64<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyBySelectedScalar(Vector64<float> left, Vector128<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyBySelectedScalar(Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplyBySelectedScalar(Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyBySelectedScalar(Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplyBySelectedScalar(Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyBySelectedScalar(Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyBySelectedScalar(Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalar(Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalar(Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> MultiplyBySelectedScalar(Vector128<float> left, Vector64<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> MultiplyBySelectedScalar(Vector128<float> left, Vector128<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyBySelectedScalar(Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyBySelectedScalar(Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalar(Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalar(Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLower(Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLower(Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLower(Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLower(Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLower(Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLower(Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLower(Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLower(Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<int> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<int> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<long> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<long> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<uint> addend, Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<uint> addend, Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<ulong> addend, Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLowerAndAdd(Vector128<ulong> addend, Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<int> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<int> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<long> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<long> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<uint> minuend, Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<uint> minuend, Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<ulong> minuend, Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningLowerAndSubtract(Vector128<ulong> minuend, Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpper(Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpper(Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpper(Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpper(Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpper(Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpper(Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpper(Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpper(Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<int> addend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<int> addend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<long> addend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<long> addend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<uint> addend, Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<uint> addend, Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<ulong> addend, Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpperAndAdd(Vector128<ulong> addend, Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<int> minuend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<int> minuend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<long> minuend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<long> minuend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<uint> minuend, Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<uint> minuend, Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<ulong> minuend, Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyBySelectedScalarWideningUpperAndSubtract(Vector128<ulong> minuend, Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyDoublingByScalarSaturateHigh(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyDoublingByScalarSaturateHigh(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyDoublingByScalarSaturateHigh(Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingByScalarSaturateHigh(Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyDoublingBySelectedScalarSaturateHigh(Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyDoublingBySelectedScalarSaturateHigh(Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyDoublingBySelectedScalarSaturateHigh(Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyDoublingBySelectedScalarSaturateHigh(Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyDoublingBySelectedScalarSaturateHigh(Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyDoublingBySelectedScalarSaturateHigh(Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingBySelectedScalarSaturateHigh(Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingBySelectedScalarSaturateHigh(Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyDoublingSaturateHigh(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyDoublingSaturateHigh(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyDoublingSaturateHigh(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingSaturateHigh(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerAndAddSaturate(Vector128<int> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerAndAddSaturate(Vector128<long> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerAndSubtractSaturate(Vector128<int> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerAndSubtractSaturate(Vector128<long> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerByScalarAndAddSaturate(Vector128<int> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerByScalarAndAddSaturate(Vector128<long> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerByScalarAndSubtractSaturate(Vector128<int> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerByScalarAndSubtractSaturate(Vector128<long> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerBySelectedScalarAndAddSaturate(Vector128<int> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerBySelectedScalarAndAddSaturate(Vector128<int> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerBySelectedScalarAndAddSaturate(Vector128<long> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerBySelectedScalarAndAddSaturate(Vector128<long> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerBySelectedScalarAndSubtractSaturate(Vector128<int> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningLowerBySelectedScalarAndSubtractSaturate(Vector128<int> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerBySelectedScalarAndSubtractSaturate(Vector128<long> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningLowerBySelectedScalarAndSubtractSaturate(Vector128<long> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateLower(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateLower(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateLowerByScalar(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateLowerByScalar(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateLowerBySelectedScalar(Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateLowerBySelectedScalar(Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateLowerBySelectedScalar(Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateLowerBySelectedScalar(Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateUpper(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateUpper(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateUpperByScalar(Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateUpperByScalar(Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateUpperBySelectedScalar(Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningSaturateUpperBySelectedScalar(Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateUpperBySelectedScalar(Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningSaturateUpperBySelectedScalar(Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperAndAddSaturate(Vector128<int> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperAndAddSaturate(Vector128<long> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperAndSubtractSaturate(Vector128<int> minuend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperAndSubtractSaturate(Vector128<long> minuend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperByScalarAndAddSaturate(Vector128<int> addend, Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperByScalarAndAddSaturate(Vector128<long> addend, Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperByScalarAndSubtractSaturate(Vector128<int> minuend, Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperByScalarAndSubtractSaturate(Vector128<long> minuend, Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperBySelectedScalarAndAddSaturate(Vector128<int> addend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperBySelectedScalarAndAddSaturate(Vector128<int> addend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperBySelectedScalarAndAddSaturate(Vector128<long> addend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperBySelectedScalarAndAddSaturate(Vector128<long> addend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperBySelectedScalarAndSubtractSaturate(Vector128<int> minuend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyDoublingWideningUpperBySelectedScalarAndSubtractSaturate(Vector128<int> minuend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperBySelectedScalarAndSubtractSaturate(Vector128<long> minuend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyDoublingWideningUpperBySelectedScalarAndSubtractSaturate(Vector128<long> minuend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingByScalarSaturateHigh(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingByScalarSaturateHigh(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingByScalarSaturateHigh(Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingByScalarSaturateHigh(Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarSaturateHigh(Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingSaturateHigh(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingSaturateHigh(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingSaturateHigh(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingSaturateHigh(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> MultiplyScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyScalarBySelectedScalar(Vector64<float> left, Vector64<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> MultiplyScalarBySelectedScalar(Vector64<float> left, Vector128<float> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> MultiplySubtract(Vector64<byte> minuend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplySubtract(Vector64<short> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplySubtract(Vector64<int> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> MultiplySubtract(Vector64<sbyte> minuend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplySubtract(Vector64<ushort> minuend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplySubtract(Vector64<uint> minuend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> MultiplySubtract(Vector128<byte> minuend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplySubtract(Vector128<short> minuend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplySubtract(Vector128<int> minuend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> MultiplySubtract(Vector128<sbyte> minuend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplySubtract(Vector128<ushort> minuend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplySubtract(Vector128<uint> minuend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplySubtractByScalar(Vector64<short> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplySubtractByScalar(Vector64<int> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplySubtractByScalar(Vector64<ushort> minuend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplySubtractByScalar(Vector64<uint> minuend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplySubtractByScalar(Vector128<short> minuend, Vector128<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplySubtractByScalar(Vector128<int> minuend, Vector128<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplySubtractByScalar(Vector128<ushort> minuend, Vector128<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplySubtractByScalar(Vector128<uint> minuend, Vector128<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplySubtractBySelectedScalar(Vector64<short> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplySubtractBySelectedScalar(Vector64<short> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplySubtractBySelectedScalar(Vector64<int> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplySubtractBySelectedScalar(Vector64<int> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplySubtractBySelectedScalar(Vector64<ushort> minuend, Vector64<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> MultiplySubtractBySelectedScalar(Vector64<ushort> minuend, Vector64<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplySubtractBySelectedScalar(Vector64<uint> minuend, Vector64<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> MultiplySubtractBySelectedScalar(Vector64<uint> minuend, Vector64<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplySubtractBySelectedScalar(Vector128<short> minuend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplySubtractBySelectedScalar(Vector128<short> minuend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplySubtractBySelectedScalar(Vector128<int> minuend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplySubtractBySelectedScalar(Vector128<int> minuend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplySubtractBySelectedScalar(Vector128<ushort> minuend, Vector128<ushort> left, Vector64<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplySubtractBySelectedScalar(Vector128<ushort> minuend, Vector128<ushort> left, Vector128<ushort> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplySubtractBySelectedScalar(Vector128<uint> minuend, Vector128<uint> left, Vector64<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplySubtractBySelectedScalar(Vector128<uint> minuend, Vector128<uint> left, Vector128<uint> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningLower(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningLower(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningLower(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningLower(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningLower(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningLower(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningLowerAndAdd(Vector128<ushort> addend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningLowerAndAdd(Vector128<int> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningLowerAndAdd(Vector128<long> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningLowerAndAdd(Vector128<short> addend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningLowerAndAdd(Vector128<uint> addend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningLowerAndAdd(Vector128<ulong> addend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningLowerAndSubtract(Vector128<ushort> minuend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningLowerAndSubtract(Vector128<int> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningLowerAndSubtract(Vector128<long> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningLowerAndSubtract(Vector128<short> minuend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningLowerAndSubtract(Vector128<uint> minuend, Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningLowerAndSubtract(Vector128<ulong> minuend, Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningUpper(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningUpper(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningUpper(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningUpper(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningUpper(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningUpper(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningUpperAndAdd(Vector128<ushort> addend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningUpperAndAdd(Vector128<int> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningUpperAndAdd(Vector128<long> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningUpperAndAdd(Vector128<short> addend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningUpperAndAdd(Vector128<uint> addend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningUpperAndAdd(Vector128<ulong> addend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> MultiplyWideningUpperAndSubtract(Vector128<ushort> minuend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyWideningUpperAndSubtract(Vector128<int> minuend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> MultiplyWideningUpperAndSubtract(Vector128<long> minuend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyWideningUpperAndSubtract(Vector128<short> minuend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> MultiplyWideningUpperAndSubtract(Vector128<uint> minuend, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> MultiplyWideningUpperAndSubtract(Vector128<ulong> minuend, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Negate(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Negate(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Negate(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Negate(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Negate(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Negate(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Negate(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Negate(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> NegateSaturate(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> NegateSaturate(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> NegateSaturate(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> NegateSaturate(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> NegateSaturate(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> NegateSaturate(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> NegateScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> NegateScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Not(Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> Not(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Not(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Not(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> Not(Vector64<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Not(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Not(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Not(Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Not(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> Not(Vector64<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Not(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> Not(Vector128<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Not(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Not(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Not(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Not(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Not(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Not(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Not(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Not(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Or(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> Or(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Or(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Or(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> Or(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Or(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Or(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Or(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Or(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> Or(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Or(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> Or(Vector128<double> left, Vector128<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Or(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Or(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Or(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Or(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Or(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Or(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Or(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Or(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> OrNot(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> OrNot(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> OrNot(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> OrNot(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> OrNot(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> OrNot(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> OrNot(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> OrNot(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> OrNot(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> OrNot(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> OrNot(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> OrNot(Vector128<double> left, Vector128<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> OrNot(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> OrNot(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> OrNot(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> OrNot(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> OrNot(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> OrNot(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> OrNot(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> OrNot(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> PolynomialMultiply(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> PolynomialMultiply(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> PolynomialMultiply(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> PolynomialMultiply(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> PolynomialMultiplyWideningLower(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> PolynomialMultiplyWideningLower(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> PolynomialMultiplyWideningUpper(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> PolynomialMultiplyWideningUpper(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> PopCount(Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> PopCount(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> PopCount(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> PopCount(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ReciprocalEstimate(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ReciprocalEstimate(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ReciprocalEstimate(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ReciprocalEstimate(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ReciprocalSquareRootEstimate(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ReciprocalSquareRootEstimate(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ReciprocalSquareRootEstimate(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ReciprocalSquareRootEstimate(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ReciprocalSquareRootStep(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ReciprocalSquareRootStep(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> ReciprocalStep(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> ReciprocalStep(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ReverseElement16(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ReverseElement16(Vector64<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ReverseElement16(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ReverseElement16(Vector64<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ReverseElement16(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ReverseElement16(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ReverseElement16(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ReverseElement16(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ReverseElement32(Vector64<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ReverseElement32(Vector64<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ReverseElement32(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ReverseElement32(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ReverseElement8(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ReverseElement8(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ReverseElement8(Vector64<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ReverseElement8(Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ReverseElement8(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ReverseElement8(Vector64<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ReverseElement8(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ReverseElement8(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ReverseElement8(Vector128<long> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ReverseElement8(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ReverseElement8(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ReverseElement8(Vector128<ulong> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundAwayFromZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> RoundAwayFromZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> RoundAwayFromZeroScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundAwayFromZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToNearest(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> RoundToNearest(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> RoundToNearestScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToNearestScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToNegativeInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> RoundToNegativeInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> RoundToNegativeInfinityScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToNegativeInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToPositiveInfinity(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> RoundToPositiveInfinity(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> RoundToPositiveInfinityScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToPositiveInfinityScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToZero(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> RoundToZero(Vector128<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> RoundToZeroScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> RoundToZeroScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftArithmetic(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftArithmetic(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftArithmetic(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftArithmetic(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftArithmetic(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftArithmetic(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftArithmetic(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftArithmeticRounded(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftArithmeticRounded(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftArithmeticRounded(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftArithmeticRounded(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftArithmeticRounded(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftArithmeticRounded(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftArithmeticRounded(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftArithmeticRoundedSaturate(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftArithmeticRoundedSaturate(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftArithmeticRoundedSaturate(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftArithmeticRoundedSaturate(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftArithmeticRoundedSaturate(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftArithmeticRoundedSaturate(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftArithmeticRoundedSaturate(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftArithmeticRoundedSaturateScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftArithmeticRoundedScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftArithmeticSaturate(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftArithmeticSaturate(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftArithmeticSaturate(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftArithmeticSaturate(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftArithmeticSaturate(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftArithmeticSaturate(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftArithmeticSaturate(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftArithmeticSaturateScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftArithmeticScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLeftAndInsert(Vector64<byte> left, Vector64<byte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLeftAndInsert(Vector64<short> left, Vector64<short> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLeftAndInsert(Vector64<int> left, Vector64<int> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLeftAndInsert(Vector64<sbyte> left, Vector64<sbyte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLeftAndInsert(Vector64<ushort> left, Vector64<ushort> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLeftAndInsert(Vector64<uint> left, Vector64<uint> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLeftAndInsert(Vector128<byte> left, Vector128<byte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLeftAndInsert(Vector128<short> left, Vector128<short> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLeftAndInsert(Vector128<int> left, Vector128<int> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLeftAndInsert(Vector128<long> left, Vector128<long> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLeftAndInsert(Vector128<sbyte> left, Vector128<sbyte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftAndInsert(Vector128<ushort> left, Vector128<ushort> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftAndInsert(Vector128<uint> left, Vector128<uint> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftAndInsert(Vector128<ulong> left, Vector128<ulong> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLeftAndInsertScalar(Vector64<long> left, Vector64<long> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLeftAndInsertScalar(Vector64<ulong> left, Vector64<ulong> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLeftLogical(Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLeftLogical(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLeftLogical(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLeftLogical(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLeftLogical(Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLeftLogical(Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLeftLogical(Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLeftLogical(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLeftLogical(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLeftLogical(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftLogical(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftLogical(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftLogical(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLeftLogicalSaturate(Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLeftLogicalSaturate(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLeftLogicalSaturate(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLeftLogicalSaturate(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLeftLogicalSaturate(Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLeftLogicalSaturate(Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLeftLogicalSaturate(Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLeftLogicalSaturate(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLeftLogicalSaturate(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLeftLogicalSaturate(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLeftLogicalSaturate(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftLogicalSaturate(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftLogicalSaturate(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftLogicalSaturate(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLeftLogicalSaturateScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLeftLogicalSaturateScalar(Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLeftLogicalSaturateUnsigned(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLeftLogicalSaturateUnsigned(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLeftLogicalSaturateUnsigned(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftLogicalSaturateUnsigned(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftLogicalSaturateUnsigned(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftLogicalSaturateUnsigned(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLeftLogicalSaturateUnsigned(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLeftLogicalSaturateUnsignedScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLeftLogicalScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLeftLogicalScalar(Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftLogicalWideningLower(Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLeftLogicalWideningLower(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLeftLogicalWideningLower(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLeftLogicalWideningLower(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftLogicalWideningLower(Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftLogicalWideningLower(Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLeftLogicalWideningUpper(Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLeftLogicalWideningUpper(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLeftLogicalWideningUpper(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLeftLogicalWideningUpper(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLeftLogicalWideningUpper(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLeftLogicalWideningUpper(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLogical(Vector64<byte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLogical(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLogical(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLogical(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLogical(Vector64<ushort> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLogical(Vector64<uint> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLogical(Vector128<byte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLogical(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLogical(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLogical(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLogical(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLogical(Vector128<ushort> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLogical(Vector128<uint> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLogical(Vector128<ulong> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLogicalRounded(Vector64<byte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLogicalRounded(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLogicalRounded(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLogicalRounded(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLogicalRounded(Vector64<ushort> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLogicalRounded(Vector64<uint> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLogicalRounded(Vector128<byte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLogicalRounded(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLogicalRounded(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLogicalRounded(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLogicalRounded(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLogicalRounded(Vector128<ushort> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLogicalRounded(Vector128<uint> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLogicalRounded(Vector128<ulong> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLogicalRoundedSaturate(Vector64<byte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLogicalRoundedSaturate(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLogicalRoundedSaturate(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLogicalRoundedSaturate(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLogicalRoundedSaturate(Vector64<ushort> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLogicalRoundedSaturate(Vector64<uint> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLogicalRoundedSaturate(Vector128<byte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLogicalRoundedSaturate(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLogicalRoundedSaturate(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLogicalRoundedSaturate(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLogicalRoundedSaturate(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLogicalRoundedSaturate(Vector128<ushort> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLogicalRoundedSaturate(Vector128<uint> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLogicalRoundedSaturate(Vector128<ulong> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLogicalRoundedSaturateScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLogicalRoundedSaturateScalar(Vector64<ulong> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLogicalRoundedScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLogicalRoundedScalar(Vector64<ulong> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftLogicalSaturate(Vector64<byte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftLogicalSaturate(Vector64<short> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftLogicalSaturate(Vector64<int> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftLogicalSaturate(Vector64<sbyte> value, Vector64<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftLogicalSaturate(Vector64<ushort> value, Vector64<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftLogicalSaturate(Vector64<uint> value, Vector64<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftLogicalSaturate(Vector128<byte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftLogicalSaturate(Vector128<short> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftLogicalSaturate(Vector128<int> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftLogicalSaturate(Vector128<long> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftLogicalSaturate(Vector128<sbyte> value, Vector128<sbyte> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftLogicalSaturate(Vector128<ushort> value, Vector128<short> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftLogicalSaturate(Vector128<uint> value, Vector128<int> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftLogicalSaturate(Vector128<ulong> value, Vector128<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLogicalSaturateScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLogicalSaturateScalar(Vector64<ulong> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftLogicalScalar(Vector64<long> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftLogicalScalar(Vector64<ulong> value, Vector64<long> count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightAndInsert(Vector64<byte> left, Vector64<byte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightAndInsert(Vector64<short> left, Vector64<short> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightAndInsert(Vector64<int> left, Vector64<int> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightAndInsert(Vector64<sbyte> left, Vector64<sbyte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightAndInsert(Vector64<ushort> left, Vector64<ushort> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightAndInsert(Vector64<uint> left, Vector64<uint> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightAndInsert(Vector128<byte> left, Vector128<byte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightAndInsert(Vector128<short> left, Vector128<short> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightAndInsert(Vector128<int> left, Vector128<int> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightAndInsert(Vector128<long> left, Vector128<long> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightAndInsert(Vector128<sbyte> left, Vector128<sbyte> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightAndInsert(Vector128<ushort> left, Vector128<ushort> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightAndInsert(Vector128<uint> left, Vector128<uint> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftRightAndInsert(Vector128<ulong> left, Vector128<ulong> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightAndInsertScalar(Vector64<long> left, Vector64<long> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftRightAndInsertScalar(Vector64<ulong> left, Vector64<ulong> right, byte shift)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmetic(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmetic(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmetic(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmetic(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmetic(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightArithmetic(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmetic(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmeticAdd(Vector64<short> addend, Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmeticAdd(Vector64<int> addend, Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmeticAdd(Vector64<sbyte> addend, Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmeticAdd(Vector128<short> addend, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmeticAdd(Vector128<int> addend, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightArithmeticAdd(Vector128<long> addend, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmeticAdd(Vector128<sbyte> addend, Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightArithmeticAddScalar(Vector64<long> addend, Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmeticNarrowingSaturateLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmeticNarrowingSaturateLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmeticNarrowingSaturateLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightArithmeticNarrowingSaturateUnsignedLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightArithmeticNarrowingSaturateUnsignedLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightArithmeticNarrowingSaturateUnsignedLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightArithmeticNarrowingSaturateUnsignedUpper(Vector64<byte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightArithmeticNarrowingSaturateUnsignedUpper(Vector64<ushort> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightArithmeticNarrowingSaturateUnsignedUpper(Vector64<uint> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmeticNarrowingSaturateUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmeticNarrowingSaturateUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmeticNarrowingSaturateUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmeticRounded(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmeticRounded(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmeticRounded(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmeticRounded(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmeticRounded(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightArithmeticRounded(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmeticRounded(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmeticRoundedAdd(Vector64<short> addend, Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmeticRoundedAdd(Vector64<int> addend, Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmeticRoundedAdd(Vector64<sbyte> addend, Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmeticRoundedAdd(Vector128<short> addend, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmeticRoundedAdd(Vector128<int> addend, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightArithmeticRoundedAdd(Vector128<long> addend, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmeticRoundedAdd(Vector128<sbyte> addend, Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightArithmeticRoundedAddScalar(Vector64<long> addend, Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightArithmeticRoundedNarrowingSaturateLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightArithmeticRoundedNarrowingSaturateLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightArithmeticRoundedNarrowingSaturateLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedUpper(Vector64<byte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedUpper(Vector64<ushort> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedUpper(Vector64<uint> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightArithmeticRoundedNarrowingSaturateUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightArithmeticRoundedNarrowingSaturateUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightArithmeticRoundedNarrowingSaturateUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightArithmeticRoundedScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightArithmeticScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogical(Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogical(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogical(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogical(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogical(Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogical(Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogical(Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogical(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogical(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightLogical(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogical(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogical(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogical(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftRightLogical(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalAdd(Vector64<byte> addend, Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalAdd(Vector64<short> addend, Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalAdd(Vector64<int> addend, Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalAdd(Vector64<sbyte> addend, Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalAdd(Vector64<ushort> addend, Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalAdd(Vector64<uint> addend, Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalAdd(Vector128<byte> addend, Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalAdd(Vector128<short> addend, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalAdd(Vector128<int> addend, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightLogicalAdd(Vector128<long> addend, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalAdd(Vector128<sbyte> addend, Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalAdd(Vector128<ushort> addend, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalAdd(Vector128<uint> addend, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftRightLogicalAdd(Vector128<ulong> addend, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightLogicalAddScalar(Vector64<long> addend, Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftRightLogicalAddScalar(Vector64<ulong> addend, Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalNarrowingLower(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalNarrowingLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalNarrowingLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalNarrowingLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalNarrowingLower(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalNarrowingLower(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalNarrowingSaturateLower(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalNarrowingSaturateLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalNarrowingSaturateLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalNarrowingSaturateLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalNarrowingSaturateLower(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalNarrowingSaturateLower(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalNarrowingSaturateUpper(Vector64<byte> lower, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalNarrowingSaturateUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalNarrowingSaturateUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalNarrowingSaturateUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalNarrowingSaturateUpper(Vector64<ushort> lower, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalNarrowingSaturateUpper(Vector64<uint> lower, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalNarrowingUpper(Vector64<byte> lower, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalNarrowingUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalNarrowingUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalNarrowingUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalNarrowingUpper(Vector64<ushort> lower, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalNarrowingUpper(Vector64<uint> lower, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalRounded(Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalRounded(Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalRounded(Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalRounded(Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalRounded(Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalRounded(Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalRounded(Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalRounded(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalRounded(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightLogicalRounded(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalRounded(Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalRounded(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalRounded(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftRightLogicalRounded(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalRoundedAdd(Vector64<byte> addend, Vector64<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalRoundedAdd(Vector64<short> addend, Vector64<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalRoundedAdd(Vector64<int> addend, Vector64<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalRoundedAdd(Vector64<sbyte> addend, Vector64<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalRoundedAdd(Vector64<ushort> addend, Vector64<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalRoundedAdd(Vector64<uint> addend, Vector64<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalRoundedAdd(Vector128<byte> addend, Vector128<byte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalRoundedAdd(Vector128<short> addend, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalRoundedAdd(Vector128<int> addend, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ShiftRightLogicalRoundedAdd(Vector128<long> addend, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalRoundedAdd(Vector128<sbyte> addend, Vector128<sbyte> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalRoundedAdd(Vector128<ushort> addend, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalRoundedAdd(Vector128<uint> addend, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ShiftRightLogicalRoundedAdd(Vector128<ulong> addend, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightLogicalRoundedAddScalar(Vector64<long> addend, Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftRightLogicalRoundedAddScalar(Vector64<ulong> addend, Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalRoundedNarrowingLower(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalRoundedNarrowingLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalRoundedNarrowingLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalRoundedNarrowingLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalRoundedNarrowingLower(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalRoundedNarrowingLower(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> ShiftRightLogicalRoundedNarrowingSaturateLower(Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<byte> lower, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<ushort> lower, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalRoundedNarrowingSaturateUpper(Vector64<uint> lower, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> ShiftRightLogicalRoundedNarrowingUpper(Vector64<byte> lower, Vector128<ushort> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ShiftRightLogicalRoundedNarrowingUpper(Vector64<short> lower, Vector128<int> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ShiftRightLogicalRoundedNarrowingUpper(Vector64<int> lower, Vector128<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> ShiftRightLogicalRoundedNarrowingUpper(Vector64<sbyte> lower, Vector128<short> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ShiftRightLogicalRoundedNarrowingUpper(Vector64<ushort> lower, Vector128<uint> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ShiftRightLogicalRoundedNarrowingUpper(Vector64<uint> lower, Vector128<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightLogicalRoundedScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftRightLogicalRoundedScalar(Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> ShiftRightLogicalScalar(Vector64<long> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> ShiftRightLogicalScalar(Vector64<ulong> value, byte count)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SignExtendWideningLower(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SignExtendWideningLower(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SignExtendWideningLower(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SignExtendWideningUpper(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SignExtendWideningUpper(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SignExtendWideningUpper(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> SqrtScalar(Vector64<double> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> SqrtScalar(Vector64<float> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(byte* address, Vector64<byte> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(double* address, Vector64<double> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(short* address, Vector64<short> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(int* address, Vector64<int> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(long* address, Vector64<long> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(sbyte* address, Vector64<sbyte> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(float* address, Vector64<float> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(ushort* address, Vector64<ushort> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(uint* address, Vector64<uint> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(ulong* address, Vector64<ulong> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(byte* address, Vector128<byte> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(double* address, Vector128<double> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(short* address, Vector128<short> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(int* address, Vector128<int> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(long* address, Vector128<long> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(sbyte* address, Vector128<sbyte> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(float* address, Vector128<float> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(ushort* address, Vector128<ushort> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(uint* address, Vector128<uint> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void Store(ulong* address, Vector128<ulong> source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(byte* address, Vector64<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(short* address, Vector64<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(int* address, Vector64<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(sbyte* address, Vector64<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(float* address, Vector64<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(ushort* address, Vector64<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(uint* address, Vector64<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(byte* address, Vector128<byte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(double* address, Vector128<double> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(short* address, Vector128<short> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(int* address, Vector128<int> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(long* address, Vector128<long> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(sbyte* address, Vector128<sbyte> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(float* address, Vector128<float> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(ushort* address, Vector128<ushort> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(uint* address, Vector128<uint> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000B3617 File Offset: 0x000B2817
		public unsafe static void StoreSelectedScalar(ulong* address, Vector128<ulong> value, byte index)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Subtract(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Subtract(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Subtract(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Subtract(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Subtract(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Subtract(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Subtract(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Subtract(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Subtract(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Subtract(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Subtract(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Subtract(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Subtract(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Subtract(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Subtract(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Subtract(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> SubtractHighNarrowingLower(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> SubtractHighNarrowingLower(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> SubtractHighNarrowingLower(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> SubtractHighNarrowingLower(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> SubtractHighNarrowingLower(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> SubtractHighNarrowingLower(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> SubtractHighNarrowingUpper(Vector64<byte> lower, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractHighNarrowingUpper(Vector64<short> lower, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractHighNarrowingUpper(Vector64<int> lower, Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> SubtractHighNarrowingUpper(Vector64<sbyte> lower, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractHighNarrowingUpper(Vector64<ushort> lower, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractHighNarrowingUpper(Vector64<uint> lower, Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> SubtractRoundedHighNarrowingLower(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> SubtractRoundedHighNarrowingLower(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> SubtractRoundedHighNarrowingLower(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> SubtractRoundedHighNarrowingLower(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> SubtractRoundedHighNarrowingLower(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> SubtractRoundedHighNarrowingLower(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> SubtractRoundedHighNarrowingUpper(Vector64<byte> lower, Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractRoundedHighNarrowingUpper(Vector64<short> lower, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractRoundedHighNarrowingUpper(Vector64<int> lower, Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> SubtractRoundedHighNarrowingUpper(Vector64<sbyte> lower, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractRoundedHighNarrowingUpper(Vector64<ushort> lower, Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractRoundedHighNarrowingUpper(Vector64<uint> lower, Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> SubtractSaturate(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> SubtractSaturate(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> SubtractSaturate(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> SubtractSaturate(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> SubtractSaturate(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> SubtractSaturate(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> SubtractSaturate(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractSaturate(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractSaturate(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SubtractSaturate(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> SubtractSaturate(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractSaturate(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractSaturate(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> SubtractSaturate(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> SubtractSaturateScalar(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> SubtractSaturateScalar(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> SubtractScalar(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> SubtractScalar(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> SubtractScalar(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> SubtractScalar(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractWideningLower(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractWideningLower(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SubtractWideningLower(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractWideningLower(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractWideningLower(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> SubtractWideningLower(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractWideningLower(Vector128<short> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractWideningLower(Vector128<int> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SubtractWideningLower(Vector128<long> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractWideningLower(Vector128<ushort> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractWideningLower(Vector128<uint> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> SubtractWideningLower(Vector128<ulong> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractWideningUpper(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractWideningUpper(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractWideningUpper(Vector128<short> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> SubtractWideningUpper(Vector128<int> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SubtractWideningUpper(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> SubtractWideningUpper(Vector128<long> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> SubtractWideningUpper(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> SubtractWideningUpper(Vector128<ushort> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractWideningUpper(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> SubtractWideningUpper(Vector128<uint> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> SubtractWideningUpper(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> SubtractWideningUpper(Vector128<ulong> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> VectorTableLookup(Vector128<byte> table, Vector64<byte> byteIndexes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> VectorTableLookup(Vector128<sbyte> table, Vector64<sbyte> byteIndexes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> VectorTableLookupExtension(Vector64<byte> defaultValues, Vector128<byte> table, Vector64<byte> byteIndexes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> VectorTableLookupExtension(Vector64<sbyte> defaultValues, Vector128<sbyte> table, Vector64<sbyte> byteIndexes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<byte> Xor(Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<double> Xor(Vector64<double> left, Vector64<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> Xor(Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> Xor(Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<long> Xor(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<sbyte> Xor(Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<float> Xor(Vector64<float> left, Vector64<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ushort> Xor(Vector64<ushort> left, Vector64<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> Xor(Vector64<uint> left, Vector64<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<ulong> Xor(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Xor(Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<double> Xor(Vector128<double> left, Vector128<double> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> Xor(Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> Xor(Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> Xor(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<sbyte> Xor(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<float> Xor(Vector128<float> left, Vector128<float> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> Xor(Vector128<ushort> left, Vector128<ushort> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> Xor(Vector128<uint> left, Vector128<uint> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> Xor(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ZeroExtendWideningLower(Vector64<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ZeroExtendWideningLower(Vector64<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ZeroExtendWideningLower(Vector64<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ZeroExtendWideningLower(Vector64<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ZeroExtendWideningLower(Vector64<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ZeroExtendWideningLower(Vector64<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ushort> ZeroExtendWideningUpper(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> ZeroExtendWideningUpper(Vector128<short> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> ZeroExtendWideningUpper(Vector128<int> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> ZeroExtendWideningUpper(Vector128<sbyte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ZeroExtendWideningUpper(Vector128<ushort> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> ZeroExtendWideningUpper(Vector128<uint> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0200041B RID: 1051
		public new abstract class Arm64 : ArmBase.Arm64
		{
			// Token: 0x17000A10 RID: 2576
			// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000AC09B File Offset: 0x000AB29B
			public new static bool IsSupported
			{
				[Intrinsic]
				get
				{
					return false;
				}
			}

			// Token: 0x06003AE4 RID: 15076 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Abs(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AE5 RID: 15077 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> Abs(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AE6 RID: 15078 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> AbsSaturate(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AE7 RID: 15079 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AbsSaturateScalar(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AE8 RID: 15080 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AbsSaturateScalar(Vector64<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AE9 RID: 15081 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> AbsSaturateScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AEA RID: 15082 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AbsSaturateScalar(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AEB RID: 15083 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> AbsScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AEC RID: 15084 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AbsoluteCompareGreaterThan(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AED RID: 15085 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AbsoluteCompareGreaterThanScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AEE RID: 15086 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AbsoluteCompareGreaterThanScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AEF RID: 15087 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AbsoluteCompareGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF0 RID: 15088 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AbsoluteCompareGreaterThanOrEqualScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF1 RID: 15089 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AbsoluteCompareGreaterThanOrEqualScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF2 RID: 15090 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AbsoluteCompareLessThan(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF3 RID: 15091 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AbsoluteCompareLessThanScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF4 RID: 15092 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AbsoluteCompareLessThanScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF5 RID: 15093 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AbsoluteCompareLessThanOrEqual(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF6 RID: 15094 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AbsoluteCompareLessThanOrEqualScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF7 RID: 15095 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AbsoluteCompareLessThanOrEqualScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF8 RID: 15096 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AbsoluteDifference(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AF9 RID: 15097 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AbsoluteDifferenceScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFA RID: 15098 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AbsoluteDifferenceScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFB RID: 15099 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Add(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFC RID: 15100 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> AddAcross(Vector64<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFD RID: 15101 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddAcross(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFE RID: 15102 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AddAcross(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003AFF RID: 15103 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddAcross(Vector64<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B00 RID: 15104 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> AddAcross(Vector128<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B01 RID: 15105 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddAcross(Vector128<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B02 RID: 15106 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddAcross(Vector128<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B03 RID: 15107 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AddAcross(Vector128<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B04 RID: 15108 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddAcross(Vector128<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B05 RID: 15109 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddAcross(Vector128<uint> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B06 RID: 15110 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddAcrossWidening(Vector64<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B07 RID: 15111 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddAcrossWidening(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B08 RID: 15112 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddAcrossWidening(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B09 RID: 15113 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddAcrossWidening(Vector64<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0A RID: 15114 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddAcrossWidening(Vector128<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0B RID: 15115 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddAcrossWidening(Vector128<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0C RID: 15116 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> AddAcrossWidening(Vector128<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0D RID: 15117 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddAcrossWidening(Vector128<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0E RID: 15118 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddAcrossWidening(Vector128<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B0F RID: 15119 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> AddAcrossWidening(Vector128<uint> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B10 RID: 15120 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> AddPairwise(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B11 RID: 15121 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> AddPairwise(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B12 RID: 15122 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> AddPairwise(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B13 RID: 15123 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> AddPairwise(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B14 RID: 15124 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> AddPairwise(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B15 RID: 15125 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> AddPairwise(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B16 RID: 15126 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> AddPairwise(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B17 RID: 15127 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> AddPairwise(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B18 RID: 15128 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> AddPairwise(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B19 RID: 15129 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> AddPairwise(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1A RID: 15130 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> AddPairwiseScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1B RID: 15131 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> AddPairwiseScalar(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1C RID: 15132 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> AddPairwiseScalar(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1D RID: 15133 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> AddPairwiseScalar(Vector128<ulong> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1E RID: 15134 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> AddSaturate(Vector64<byte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B1F RID: 15135 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddSaturate(Vector64<short> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B20 RID: 15136 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddSaturate(Vector64<int> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B21 RID: 15137 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AddSaturate(Vector64<sbyte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B22 RID: 15138 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddSaturate(Vector64<ushort> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B23 RID: 15139 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddSaturate(Vector64<uint> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B24 RID: 15140 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> AddSaturate(Vector128<byte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B25 RID: 15141 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> AddSaturate(Vector128<short> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B26 RID: 15142 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> AddSaturate(Vector128<int> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B27 RID: 15143 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> AddSaturate(Vector128<long> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B28 RID: 15144 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> AddSaturate(Vector128<sbyte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B29 RID: 15145 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> AddSaturate(Vector128<ushort> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2A RID: 15146 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> AddSaturate(Vector128<uint> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2B RID: 15147 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> AddSaturate(Vector128<ulong> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2C RID: 15148 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> AddSaturateScalar(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2D RID: 15149 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> AddSaturateScalar(Vector64<byte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2E RID: 15150 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddSaturateScalar(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B2F RID: 15151 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> AddSaturateScalar(Vector64<short> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B30 RID: 15152 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddSaturateScalar(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B31 RID: 15153 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> AddSaturateScalar(Vector64<int> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B32 RID: 15154 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> AddSaturateScalar(Vector64<long> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B33 RID: 15155 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AddSaturateScalar(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B34 RID: 15156 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> AddSaturateScalar(Vector64<sbyte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B35 RID: 15157 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddSaturateScalar(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B36 RID: 15158 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> AddSaturateScalar(Vector64<ushort> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B37 RID: 15159 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddSaturateScalar(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B38 RID: 15160 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> AddSaturateScalar(Vector64<uint> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B39 RID: 15161 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> AddSaturateScalar(Vector64<ulong> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3A RID: 15162 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Ceiling(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3B RID: 15163 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareEqual(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3C RID: 15164 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareEqual(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3D RID: 15165 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareEqual(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3E RID: 15166 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareEqualScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B3F RID: 15167 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareEqualScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B40 RID: 15168 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> CompareEqualScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B41 RID: 15169 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareEqualScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B42 RID: 15170 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareGreaterThan(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B43 RID: 15171 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareGreaterThan(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B44 RID: 15172 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareGreaterThan(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B45 RID: 15173 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareGreaterThanScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B46 RID: 15174 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareGreaterThanScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B47 RID: 15175 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> CompareGreaterThanScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B48 RID: 15176 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareGreaterThanScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B49 RID: 15177 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareGreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4A RID: 15178 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareGreaterThanOrEqual(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4B RID: 15179 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareGreaterThanOrEqual(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4C RID: 15180 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareGreaterThanOrEqualScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4D RID: 15181 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareGreaterThanOrEqualScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4E RID: 15182 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> CompareGreaterThanOrEqualScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B4F RID: 15183 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareGreaterThanOrEqualScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B50 RID: 15184 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareLessThan(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B51 RID: 15185 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareLessThan(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B52 RID: 15186 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareLessThan(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B53 RID: 15187 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareLessThanScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B54 RID: 15188 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareLessThanScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B55 RID: 15189 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> CompareLessThanScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B56 RID: 15190 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareLessThanScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B57 RID: 15191 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareLessThanOrEqual(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B58 RID: 15192 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareLessThanOrEqual(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B59 RID: 15193 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareLessThanOrEqual(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5A RID: 15194 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareLessThanOrEqualScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5B RID: 15195 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareLessThanOrEqualScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5C RID: 15196 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> CompareLessThanOrEqualScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5D RID: 15197 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareLessThanOrEqualScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5E RID: 15198 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> CompareTest(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B5F RID: 15199 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> CompareTest(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B60 RID: 15200 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> CompareTest(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B61 RID: 15201 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> CompareTestScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B62 RID: 15202 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> CompareTestScalar(Vector64<long> left, Vector64<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B63 RID: 15203 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> CompareTestScalar(Vector64<ulong> left, Vector64<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B64 RID: 15204 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ConvertToDouble(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B65 RID: 15205 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ConvertToDouble(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B66 RID: 15206 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ConvertToDouble(Vector128<ulong> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B67 RID: 15207 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ConvertToDoubleScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B68 RID: 15208 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ConvertToDoubleScalar(Vector64<ulong> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B69 RID: 15209 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ConvertToDoubleUpper(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6A RID: 15210 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ConvertToInt64RoundAwayFromZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6B RID: 15211 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> ConvertToInt64RoundAwayFromZeroScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6C RID: 15212 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ConvertToInt64RoundToEven(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6D RID: 15213 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> ConvertToInt64RoundToEvenScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6E RID: 15214 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ConvertToInt64RoundToNegativeInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B6F RID: 15215 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> ConvertToInt64RoundToNegativeInfinityScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B70 RID: 15216 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ConvertToInt64RoundToPositiveInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B71 RID: 15217 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> ConvertToInt64RoundToPositiveInfinityScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B72 RID: 15218 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ConvertToInt64RoundToZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B73 RID: 15219 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> ConvertToInt64RoundToZeroScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B74 RID: 15220 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ConvertToSingleLower(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B75 RID: 15221 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ConvertToSingleRoundToOddLower(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B76 RID: 15222 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> ConvertToSingleRoundToOddUpper(Vector64<float> lower, Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B77 RID: 15223 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> ConvertToSingleUpper(Vector64<float> lower, Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B78 RID: 15224 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ConvertToUInt64RoundAwayFromZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B79 RID: 15225 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> ConvertToUInt64RoundAwayFromZeroScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7A RID: 15226 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ConvertToUInt64RoundToEven(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7B RID: 15227 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> ConvertToUInt64RoundToEvenScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7C RID: 15228 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ConvertToUInt64RoundToNegativeInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7D RID: 15229 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> ConvertToUInt64RoundToNegativeInfinityScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7E RID: 15230 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ConvertToUInt64RoundToPositiveInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B7F RID: 15231 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> ConvertToUInt64RoundToPositiveInfinityScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B80 RID: 15232 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ConvertToUInt64RoundToZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B81 RID: 15233 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ulong> ConvertToUInt64RoundToZeroScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B82 RID: 15234 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> Divide(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B83 RID: 15235 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Divide(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B84 RID: 15236 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> Divide(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B85 RID: 15237 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> DuplicateSelectedScalarToVector128(Vector128<double> value, byte index)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B86 RID: 15238 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> DuplicateSelectedScalarToVector128(Vector128<long> value, byte index)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B87 RID: 15239 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> DuplicateSelectedScalarToVector128(Vector128<ulong> value, byte index)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B88 RID: 15240 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> DuplicateToVector128(double value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B89 RID: 15241 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> DuplicateToVector128(long value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8A RID: 15242 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> DuplicateToVector128(ulong value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8B RID: 15243 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ExtractNarrowingSaturateScalar(Vector64<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8C RID: 15244 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ExtractNarrowingSaturateScalar(Vector64<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8D RID: 15245 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ExtractNarrowingSaturateScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8E RID: 15246 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ExtractNarrowingSaturateScalar(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B8F RID: 15247 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ExtractNarrowingSaturateScalar(Vector64<uint> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B90 RID: 15248 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ExtractNarrowingSaturateScalar(Vector64<ulong> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B91 RID: 15249 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ExtractNarrowingSaturateUnsignedScalar(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B92 RID: 15250 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ExtractNarrowingSaturateUnsignedScalar(Vector64<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B93 RID: 15251 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ExtractNarrowingSaturateUnsignedScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B94 RID: 15252 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Floor(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B95 RID: 15253 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplyAdd(Vector128<double> addend, Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B96 RID: 15254 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplyAddByScalar(Vector64<float> addend, Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B97 RID: 15255 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplyAddByScalar(Vector128<double> addend, Vector128<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B98 RID: 15256 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplyAddByScalar(Vector128<float> addend, Vector128<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B99 RID: 15257 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplyAddBySelectedScalar(Vector64<float> addend, Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9A RID: 15258 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplyAddBySelectedScalar(Vector64<float> addend, Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9B RID: 15259 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplyAddBySelectedScalar(Vector128<double> addend, Vector128<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9C RID: 15260 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplyAddBySelectedScalar(Vector128<float> addend, Vector128<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9D RID: 15261 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplyAddBySelectedScalar(Vector128<float> addend, Vector128<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9E RID: 15262 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> FusedMultiplyAddScalarBySelectedScalar(Vector64<double> addend, Vector64<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003B9F RID: 15263 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplyAddScalarBySelectedScalar(Vector64<float> addend, Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA0 RID: 15264 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplyAddScalarBySelectedScalar(Vector64<float> addend, Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA1 RID: 15265 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplySubtract(Vector128<double> minuend, Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA2 RID: 15266 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplySubtractByScalar(Vector64<float> minuend, Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA3 RID: 15267 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplySubtractByScalar(Vector128<double> minuend, Vector128<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA4 RID: 15268 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplySubtractByScalar(Vector128<float> minuend, Vector128<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA5 RID: 15269 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplySubtractBySelectedScalar(Vector64<float> minuend, Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA6 RID: 15270 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplySubtractBySelectedScalar(Vector64<float> minuend, Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA7 RID: 15271 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> FusedMultiplySubtractBySelectedScalar(Vector128<double> minuend, Vector128<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA8 RID: 15272 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplySubtractBySelectedScalar(Vector128<float> minuend, Vector128<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BA9 RID: 15273 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> FusedMultiplySubtractBySelectedScalar(Vector128<float> minuend, Vector128<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAA RID: 15274 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> FusedMultiplySubtractScalarBySelectedScalar(Vector64<double> minuend, Vector64<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAB RID: 15275 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplySubtractScalarBySelectedScalar(Vector64<float> minuend, Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAC RID: 15276 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> FusedMultiplySubtractScalarBySelectedScalar(Vector64<float> minuend, Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAD RID: 15277 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> InsertSelectedScalar(Vector64<byte> result, byte resultIndex, Vector64<byte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAE RID: 15278 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> InsertSelectedScalar(Vector64<byte> result, byte resultIndex, Vector128<byte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BAF RID: 15279 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> InsertSelectedScalar(Vector64<short> result, byte resultIndex, Vector64<short> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB0 RID: 15280 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> InsertSelectedScalar(Vector64<short> result, byte resultIndex, Vector128<short> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB1 RID: 15281 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> InsertSelectedScalar(Vector64<int> result, byte resultIndex, Vector64<int> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB2 RID: 15282 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> InsertSelectedScalar(Vector64<int> result, byte resultIndex, Vector128<int> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB3 RID: 15283 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> InsertSelectedScalar(Vector64<sbyte> result, byte resultIndex, Vector64<sbyte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB4 RID: 15284 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> InsertSelectedScalar(Vector64<sbyte> result, byte resultIndex, Vector128<sbyte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB5 RID: 15285 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> InsertSelectedScalar(Vector64<float> result, byte resultIndex, Vector64<float> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB6 RID: 15286 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> InsertSelectedScalar(Vector64<float> result, byte resultIndex, Vector128<float> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB7 RID: 15287 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> InsertSelectedScalar(Vector64<ushort> result, byte resultIndex, Vector64<ushort> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB8 RID: 15288 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> InsertSelectedScalar(Vector64<ushort> result, byte resultIndex, Vector128<ushort> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BB9 RID: 15289 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> InsertSelectedScalar(Vector64<uint> result, byte resultIndex, Vector64<uint> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBA RID: 15290 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> InsertSelectedScalar(Vector64<uint> result, byte resultIndex, Vector128<uint> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBB RID: 15291 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> InsertSelectedScalar(Vector128<byte> result, byte resultIndex, Vector64<byte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBC RID: 15292 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> InsertSelectedScalar(Vector128<byte> result, byte resultIndex, Vector128<byte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBD RID: 15293 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> InsertSelectedScalar(Vector128<double> result, byte resultIndex, Vector128<double> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBE RID: 15294 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> InsertSelectedScalar(Vector128<short> result, byte resultIndex, Vector64<short> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BBF RID: 15295 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> InsertSelectedScalar(Vector128<short> result, byte resultIndex, Vector128<short> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC0 RID: 15296 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> InsertSelectedScalar(Vector128<int> result, byte resultIndex, Vector64<int> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC1 RID: 15297 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> InsertSelectedScalar(Vector128<int> result, byte resultIndex, Vector128<int> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC2 RID: 15298 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> InsertSelectedScalar(Vector128<long> result, byte resultIndex, Vector128<long> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC3 RID: 15299 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> InsertSelectedScalar(Vector128<sbyte> result, byte resultIndex, Vector64<sbyte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC4 RID: 15300 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> InsertSelectedScalar(Vector128<sbyte> result, byte resultIndex, Vector128<sbyte> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC5 RID: 15301 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> InsertSelectedScalar(Vector128<float> result, byte resultIndex, Vector64<float> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC6 RID: 15302 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> InsertSelectedScalar(Vector128<float> result, byte resultIndex, Vector128<float> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC7 RID: 15303 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> InsertSelectedScalar(Vector128<ushort> result, byte resultIndex, Vector64<ushort> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC8 RID: 15304 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> InsertSelectedScalar(Vector128<ushort> result, byte resultIndex, Vector128<ushort> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BC9 RID: 15305 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> InsertSelectedScalar(Vector128<uint> result, byte resultIndex, Vector64<uint> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCA RID: 15306 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> InsertSelectedScalar(Vector128<uint> result, byte resultIndex, Vector128<uint> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCB RID: 15307 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> InsertSelectedScalar(Vector128<ulong> result, byte resultIndex, Vector128<ulong> value, byte valueIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCC RID: 15308 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static Vector128<double> LoadAndReplicateToVector128(double* address)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCD RID: 15309 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static Vector128<long> LoadAndReplicateToVector128(long* address)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCE RID: 15310 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static Vector128<ulong> LoadAndReplicateToVector128(ulong* address)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BCF RID: 15311 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Max(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD0 RID: 15312 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> MaxAcross(Vector64<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD1 RID: 15313 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MaxAcross(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD2 RID: 15314 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> MaxAcross(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD3 RID: 15315 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> MaxAcross(Vector64<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD4 RID: 15316 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> MaxAcross(Vector128<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD5 RID: 15317 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MaxAcross(Vector128<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD6 RID: 15318 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MaxAcross(Vector128<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD7 RID: 15319 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> MaxAcross(Vector128<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD8 RID: 15320 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxAcross(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BD9 RID: 15321 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> MaxAcross(Vector128<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDA RID: 15322 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> MaxAcross(Vector128<uint> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDB RID: 15323 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MaxNumber(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDC RID: 15324 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxNumberAcross(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDD RID: 15325 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxNumberPairwise(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDE RID: 15326 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MaxNumberPairwise(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BDF RID: 15327 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MaxNumberPairwise(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE0 RID: 15328 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxNumberPairwiseScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE1 RID: 15329 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MaxNumberPairwiseScalar(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE2 RID: 15330 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> MaxPairwise(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE3 RID: 15331 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MaxPairwise(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE4 RID: 15332 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> MaxPairwise(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE5 RID: 15333 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> MaxPairwise(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE6 RID: 15334 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> MaxPairwise(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE7 RID: 15335 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MaxPairwise(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE8 RID: 15336 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> MaxPairwise(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BE9 RID: 15337 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> MaxPairwise(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BEA RID: 15338 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxPairwiseScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BEB RID: 15339 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MaxPairwiseScalar(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BEC RID: 15340 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MaxScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BED RID: 15341 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MaxScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BEE RID: 15342 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Min(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BEF RID: 15343 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> MinAcross(Vector64<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF0 RID: 15344 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MinAcross(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF1 RID: 15345 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> MinAcross(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF2 RID: 15346 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> MinAcross(Vector64<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF3 RID: 15347 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> MinAcross(Vector128<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF4 RID: 15348 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MinAcross(Vector128<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF5 RID: 15349 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MinAcross(Vector128<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF6 RID: 15350 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> MinAcross(Vector128<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF7 RID: 15351 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinAcross(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF8 RID: 15352 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> MinAcross(Vector128<ushort> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> MinAcross(Vector128<uint> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFA RID: 15354 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MinNumber(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFB RID: 15355 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinNumberAcross(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFC RID: 15356 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinNumberPairwise(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFD RID: 15357 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MinNumberPairwise(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFE RID: 15358 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MinNumberPairwise(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003BFF RID: 15359 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinNumberPairwiseScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C00 RID: 15360 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MinNumberPairwiseScalar(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C01 RID: 15361 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> MinPairwise(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C02 RID: 15362 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MinPairwise(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C03 RID: 15363 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> MinPairwise(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C04 RID: 15364 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> MinPairwise(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C05 RID: 15365 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> MinPairwise(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C06 RID: 15366 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MinPairwise(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C07 RID: 15367 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> MinPairwise(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C08 RID: 15368 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> MinPairwise(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C09 RID: 15369 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinPairwiseScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0A RID: 15370 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MinPairwiseScalar(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0B RID: 15371 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MinScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0C RID: 15372 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MinScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0D RID: 15373 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Multiply(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0E RID: 15374 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MultiplyByScalar(Vector128<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C0F RID: 15375 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MultiplyBySelectedScalar(Vector128<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C10 RID: 15376 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyDoublingSaturateHighScalar(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C11 RID: 15377 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingSaturateHighScalar(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C12 RID: 15378 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyDoublingScalarBySelectedScalarSaturateHigh(Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C13 RID: 15379 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyDoublingScalarBySelectedScalarSaturateHigh(Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C14 RID: 15380 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingScalarBySelectedScalarSaturateHigh(Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C15 RID: 15381 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingScalarBySelectedScalarSaturateHigh(Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C16 RID: 15382 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningAndAddSaturateScalar(Vector64<int> addend, Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C17 RID: 15383 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningAndAddSaturateScalar(Vector64<long> addend, Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C18 RID: 15384 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningAndSubtractSaturateScalar(Vector64<int> minuend, Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C19 RID: 15385 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningAndSubtractSaturateScalar(Vector64<long> minuend, Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1A RID: 15386 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningSaturateScalar(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1B RID: 15387 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningSaturateScalar(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1C RID: 15388 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningSaturateScalarBySelectedScalar(Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1D RID: 15389 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningSaturateScalarBySelectedScalar(Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1E RID: 15390 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningSaturateScalarBySelectedScalar(Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C1F RID: 15391 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningSaturateScalarBySelectedScalar(Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C20 RID: 15392 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningScalarBySelectedScalarAndAddSaturate(Vector64<int> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C21 RID: 15393 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningScalarBySelectedScalarAndAddSaturate(Vector64<int> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C22 RID: 15394 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningScalarBySelectedScalarAndAddSaturate(Vector64<long> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C23 RID: 15395 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningScalarBySelectedScalarAndAddSaturate(Vector64<long> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C24 RID: 15396 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningScalarBySelectedScalarAndSubtractSaturate(Vector64<int> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C25 RID: 15397 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyDoublingWideningScalarBySelectedScalarAndSubtractSaturate(Vector64<int> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C26 RID: 15398 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningScalarBySelectedScalarAndSubtractSaturate(Vector64<long> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C27 RID: 15399 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> MultiplyDoublingWideningScalarBySelectedScalarAndSubtractSaturate(Vector64<long> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C28 RID: 15400 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtended(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C29 RID: 15401 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MultiplyExtended(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2A RID: 15402 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MultiplyExtended(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2B RID: 15403 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MultiplyExtendedByScalar(Vector128<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2C RID: 15404 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtendedBySelectedScalar(Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2D RID: 15405 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtendedBySelectedScalar(Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2E RID: 15406 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> MultiplyExtendedBySelectedScalar(Vector128<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C2F RID: 15407 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MultiplyExtendedBySelectedScalar(Vector128<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C30 RID: 15408 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> MultiplyExtendedBySelectedScalar(Vector128<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C31 RID: 15409 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MultiplyExtendedScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C32 RID: 15410 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtendedScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C33 RID: 15411 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MultiplyExtendedScalarBySelectedScalar(Vector64<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C34 RID: 15412 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtendedScalarBySelectedScalar(Vector64<float> left, Vector64<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C35 RID: 15413 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> MultiplyExtendedScalarBySelectedScalar(Vector64<float> left, Vector128<float> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C36 RID: 15414 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingSaturateHighScalar(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C37 RID: 15415 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingSaturateHighScalar(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C38 RID: 15416 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarSaturateHigh(Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C39 RID: 15417 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarSaturateHigh(Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3A RID: 15418 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarSaturateHigh(Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3B RID: 15419 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarSaturateHigh(Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3C RID: 15420 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> MultiplyScalarBySelectedScalar(Vector64<double> left, Vector128<double> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3D RID: 15421 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Negate(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3E RID: 15422 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> Negate(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C3F RID: 15423 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> NegateSaturate(Vector128<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C40 RID: 15424 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> NegateSaturateScalar(Vector64<short> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C41 RID: 15425 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> NegateSaturateScalar(Vector64<int> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C42 RID: 15426 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> NegateSaturateScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C43 RID: 15427 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> NegateSaturateScalar(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C44 RID: 15428 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<long> NegateScalar(Vector64<long> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C45 RID: 15429 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ReciprocalEstimate(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C46 RID: 15430 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ReciprocalEstimateScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C47 RID: 15431 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ReciprocalEstimateScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C48 RID: 15432 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ReciprocalExponentScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C49 RID: 15433 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ReciprocalExponentScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4A RID: 15434 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ReciprocalSquareRootEstimate(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4B RID: 15435 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ReciprocalSquareRootEstimateScalar(Vector64<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4C RID: 15436 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ReciprocalSquareRootEstimateScalar(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4D RID: 15437 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ReciprocalSquareRootStep(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4E RID: 15438 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ReciprocalSquareRootStepScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C4F RID: 15439 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ReciprocalSquareRootStepScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C50 RID: 15440 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ReciprocalStep(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C51 RID: 15441 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<double> ReciprocalStepScalar(Vector64<double> left, Vector64<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C52 RID: 15442 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ReciprocalStepScalar(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C53 RID: 15443 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> RoundAwayFromZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C54 RID: 15444 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> RoundToNearest(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C55 RID: 15445 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> RoundToNegativeInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C56 RID: 15446 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> RoundToPositiveInfinity(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C57 RID: 15447 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> RoundToZero(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C58 RID: 15448 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftArithmeticRoundedSaturateScalar(Vector64<short> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C59 RID: 15449 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftArithmeticRoundedSaturateScalar(Vector64<int> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5A RID: 15450 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftArithmeticRoundedSaturateScalar(Vector64<sbyte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5B RID: 15451 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftArithmeticSaturateScalar(Vector64<short> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5C RID: 15452 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftArithmeticSaturateScalar(Vector64<int> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5D RID: 15453 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftArithmeticSaturateScalar(Vector64<sbyte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5E RID: 15454 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftLeftLogicalSaturateScalar(Vector64<byte> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C5F RID: 15455 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftLeftLogicalSaturateScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C60 RID: 15456 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftLeftLogicalSaturateScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C61 RID: 15457 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftLeftLogicalSaturateScalar(Vector64<sbyte> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C62 RID: 15458 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftLeftLogicalSaturateScalar(Vector64<ushort> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C63 RID: 15459 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftLeftLogicalSaturateScalar(Vector64<uint> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C64 RID: 15460 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftLeftLogicalSaturateUnsignedScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C65 RID: 15461 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftLeftLogicalSaturateUnsignedScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C66 RID: 15462 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftLeftLogicalSaturateUnsignedScalar(Vector64<sbyte> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C67 RID: 15463 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftLogicalRoundedSaturateScalar(Vector64<byte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C68 RID: 15464 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftLogicalRoundedSaturateScalar(Vector64<short> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C69 RID: 15465 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftLogicalRoundedSaturateScalar(Vector64<int> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6A RID: 15466 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftLogicalRoundedSaturateScalar(Vector64<sbyte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6B RID: 15467 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftLogicalRoundedSaturateScalar(Vector64<ushort> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6C RID: 15468 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftLogicalRoundedSaturateScalar(Vector64<uint> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6D RID: 15469 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftLogicalSaturateScalar(Vector64<byte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6E RID: 15470 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftLogicalSaturateScalar(Vector64<short> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C6F RID: 15471 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftLogicalSaturateScalar(Vector64<int> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C70 RID: 15472 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftLogicalSaturateScalar(Vector64<sbyte> value, Vector64<sbyte> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C71 RID: 15473 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftLogicalSaturateScalar(Vector64<ushort> value, Vector64<short> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C72 RID: 15474 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftLogicalSaturateScalar(Vector64<uint> value, Vector64<int> count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C73 RID: 15475 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftRightArithmeticNarrowingSaturateScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C74 RID: 15476 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftRightArithmeticNarrowingSaturateScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C75 RID: 15477 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftRightArithmeticNarrowingSaturateScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C76 RID: 15478 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftRightArithmeticNarrowingSaturateUnsignedScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C77 RID: 15479 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftRightArithmeticNarrowingSaturateUnsignedScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C78 RID: 15480 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftRightArithmeticNarrowingSaturateUnsignedScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C79 RID: 15481 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftRightArithmeticRoundedNarrowingSaturateScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7A RID: 15482 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftRightArithmeticRoundedNarrowingSaturateScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7B RID: 15483 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftRightArithmeticRoundedNarrowingSaturateScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7C RID: 15484 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7D RID: 15485 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7E RID: 15486 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftRightArithmeticRoundedNarrowingSaturateUnsignedScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C7F RID: 15487 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftRightLogicalNarrowingSaturateScalar(Vector64<ushort> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C80 RID: 15488 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftRightLogicalNarrowingSaturateScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C81 RID: 15489 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftRightLogicalNarrowingSaturateScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C82 RID: 15490 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftRightLogicalNarrowingSaturateScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C83 RID: 15491 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftRightLogicalNarrowingSaturateScalar(Vector64<uint> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C84 RID: 15492 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftRightLogicalNarrowingSaturateScalar(Vector64<ulong> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C85 RID: 15493 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<ushort> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C86 RID: 15494 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<int> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C87 RID: 15495 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<long> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C88 RID: 15496 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<short> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C89 RID: 15497 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<uint> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8A RID: 15498 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ShiftRightLogicalRoundedNarrowingSaturateScalar(Vector64<ulong> value, byte count)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8B RID: 15499 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> Sqrt(Vector64<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8C RID: 15500 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Sqrt(Vector128<double> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8D RID: 15501 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> Sqrt(Vector128<float> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8E RID: 15502 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(byte* address, Vector64<byte> value1, Vector64<byte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C8F RID: 15503 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(double* address, Vector64<double> value1, Vector64<double> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C90 RID: 15504 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(short* address, Vector64<short> value1, Vector64<short> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C91 RID: 15505 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(int* address, Vector64<int> value1, Vector64<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C92 RID: 15506 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(long* address, Vector64<long> value1, Vector64<long> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C93 RID: 15507 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(sbyte* address, Vector64<sbyte> value1, Vector64<sbyte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C94 RID: 15508 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(float* address, Vector64<float> value1, Vector64<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C95 RID: 15509 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(ushort* address, Vector64<ushort> value1, Vector64<ushort> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C96 RID: 15510 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(uint* address, Vector64<uint> value1, Vector64<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C97 RID: 15511 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(ulong* address, Vector64<ulong> value1, Vector64<ulong> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C98 RID: 15512 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(byte* address, Vector128<byte> value1, Vector128<byte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C99 RID: 15513 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(double* address, Vector128<double> value1, Vector128<double> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9A RID: 15514 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(short* address, Vector128<short> value1, Vector128<short> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9B RID: 15515 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(int* address, Vector128<int> value1, Vector128<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9C RID: 15516 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(long* address, Vector128<long> value1, Vector128<long> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9D RID: 15517 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(sbyte* address, Vector128<sbyte> value1, Vector128<sbyte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9E RID: 15518 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(float* address, Vector128<float> value1, Vector128<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003C9F RID: 15519 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(ushort* address, Vector128<ushort> value1, Vector128<ushort> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA0 RID: 15520 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(uint* address, Vector128<uint> value1, Vector128<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA1 RID: 15521 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePair(ulong* address, Vector128<ulong> value1, Vector128<ulong> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA2 RID: 15522 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(byte* address, Vector64<byte> value1, Vector64<byte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA3 RID: 15523 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(double* address, Vector64<double> value1, Vector64<double> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA4 RID: 15524 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(short* address, Vector64<short> value1, Vector64<short> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA5 RID: 15525 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(int* address, Vector64<int> value1, Vector64<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA6 RID: 15526 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(long* address, Vector64<long> value1, Vector64<long> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA7 RID: 15527 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(sbyte* address, Vector64<sbyte> value1, Vector64<sbyte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA8 RID: 15528 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(float* address, Vector64<float> value1, Vector64<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CA9 RID: 15529 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(ushort* address, Vector64<ushort> value1, Vector64<ushort> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAA RID: 15530 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(uint* address, Vector64<uint> value1, Vector64<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAB RID: 15531 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(ulong* address, Vector64<ulong> value1, Vector64<ulong> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAC RID: 15532 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(byte* address, Vector128<byte> value1, Vector128<byte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAD RID: 15533 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(double* address, Vector128<double> value1, Vector128<double> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAE RID: 15534 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(short* address, Vector128<short> value1, Vector128<short> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CAF RID: 15535 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(int* address, Vector128<int> value1, Vector128<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB0 RID: 15536 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(long* address, Vector128<long> value1, Vector128<long> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB1 RID: 15537 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(sbyte* address, Vector128<sbyte> value1, Vector128<sbyte> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB2 RID: 15538 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(float* address, Vector128<float> value1, Vector128<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB3 RID: 15539 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(ushort* address, Vector128<ushort> value1, Vector128<ushort> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB4 RID: 15540 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(uint* address, Vector128<uint> value1, Vector128<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB5 RID: 15541 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairNonTemporal(ulong* address, Vector128<ulong> value1, Vector128<ulong> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB6 RID: 15542 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalar(int* address, Vector64<int> value1, Vector64<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB7 RID: 15543 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalar(float* address, Vector64<float> value1, Vector64<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB8 RID: 15544 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalar(uint* address, Vector64<uint> value1, Vector64<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CB9 RID: 15545 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalarNonTemporal(int* address, Vector64<int> value1, Vector64<int> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBA RID: 15546 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalarNonTemporal(float* address, Vector64<float> value1, Vector64<float> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBB RID: 15547 RVA: 0x000B3617 File Offset: 0x000B2817
			public unsafe static void StorePairScalarNonTemporal(uint* address, Vector64<uint> value1, Vector64<uint> value2)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBC RID: 15548 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> Subtract(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBD RID: 15549 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> SubtractSaturateScalar(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBE RID: 15550 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> SubtractSaturateScalar(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CBF RID: 15551 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> SubtractSaturateScalar(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC0 RID: 15552 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> SubtractSaturateScalar(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC1 RID: 15553 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> SubtractSaturateScalar(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC2 RID: 15554 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> SubtractSaturateScalar(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC3 RID: 15555 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ReverseElementBits(Vector64<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC4 RID: 15556 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ReverseElementBits(Vector64<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC5 RID: 15557 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> ReverseElementBits(Vector128<byte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC6 RID: 15558 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> ReverseElementBits(Vector128<sbyte> value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC7 RID: 15559 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> TransposeEven(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC8 RID: 15560 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> TransposeEven(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CC9 RID: 15561 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> TransposeEven(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCA RID: 15562 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> TransposeEven(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCB RID: 15563 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> TransposeEven(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCC RID: 15564 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> TransposeEven(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCD RID: 15565 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> TransposeEven(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCE RID: 15566 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> TransposeEven(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CCF RID: 15567 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> TransposeEven(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD0 RID: 15568 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> TransposeEven(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD1 RID: 15569 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> TransposeEven(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD2 RID: 15570 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> TransposeEven(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD3 RID: 15571 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> TransposeEven(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD4 RID: 15572 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> TransposeEven(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD5 RID: 15573 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> TransposeEven(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD6 RID: 15574 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> TransposeEven(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD7 RID: 15575 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> TransposeEven(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD8 RID: 15576 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> TransposeOdd(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CD9 RID: 15577 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> TransposeOdd(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDA RID: 15578 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> TransposeOdd(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDB RID: 15579 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> TransposeOdd(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDC RID: 15580 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> TransposeOdd(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDD RID: 15581 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> TransposeOdd(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDE RID: 15582 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> TransposeOdd(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CDF RID: 15583 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> TransposeOdd(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE0 RID: 15584 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> TransposeOdd(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE1 RID: 15585 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> TransposeOdd(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE2 RID: 15586 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> TransposeOdd(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE3 RID: 15587 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> TransposeOdd(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE4 RID: 15588 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> TransposeOdd(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE5 RID: 15589 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> TransposeOdd(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE6 RID: 15590 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> TransposeOdd(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE7 RID: 15591 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> TransposeOdd(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE8 RID: 15592 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> TransposeOdd(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CE9 RID: 15593 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> UnzipEven(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CEA RID: 15594 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> UnzipEven(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CEB RID: 15595 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> UnzipEven(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CEC RID: 15596 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> UnzipEven(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CED RID: 15597 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> UnzipEven(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CEE RID: 15598 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> UnzipEven(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CEF RID: 15599 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> UnzipEven(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF0 RID: 15600 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> UnzipEven(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF1 RID: 15601 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> UnzipEven(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF2 RID: 15602 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> UnzipEven(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF3 RID: 15603 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> UnzipEven(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF4 RID: 15604 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> UnzipEven(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF5 RID: 15605 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> UnzipEven(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF6 RID: 15606 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> UnzipEven(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF7 RID: 15607 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> UnzipEven(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF8 RID: 15608 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> UnzipEven(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CF9 RID: 15609 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> UnzipEven(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFA RID: 15610 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> UnzipOdd(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFB RID: 15611 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> UnzipOdd(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFC RID: 15612 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> UnzipOdd(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFD RID: 15613 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> UnzipOdd(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFE RID: 15614 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> UnzipOdd(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003CFF RID: 15615 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> UnzipOdd(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D00 RID: 15616 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> UnzipOdd(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D01 RID: 15617 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> UnzipOdd(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D02 RID: 15618 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> UnzipOdd(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D03 RID: 15619 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> UnzipOdd(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D04 RID: 15620 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> UnzipOdd(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D05 RID: 15621 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> UnzipOdd(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D06 RID: 15622 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> UnzipOdd(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D07 RID: 15623 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> UnzipOdd(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D08 RID: 15624 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> UnzipOdd(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D09 RID: 15625 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> UnzipOdd(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0A RID: 15626 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> UnzipOdd(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0B RID: 15627 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> VectorTableLookup(Vector128<byte> table, Vector128<byte> byteIndexes)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0C RID: 15628 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> VectorTableLookup(Vector128<sbyte> table, Vector128<sbyte> byteIndexes)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0D RID: 15629 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> VectorTableLookupExtension(Vector128<byte> defaultValues, Vector128<byte> table, Vector128<byte> byteIndexes)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0E RID: 15630 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> VectorTableLookupExtension(Vector128<sbyte> defaultValues, Vector128<sbyte> table, Vector128<sbyte> byteIndexes)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D0F RID: 15631 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ZipHigh(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D10 RID: 15632 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ZipHigh(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D11 RID: 15633 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ZipHigh(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D12 RID: 15634 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ZipHigh(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D13 RID: 15635 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ZipHigh(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D14 RID: 15636 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ZipHigh(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D15 RID: 15637 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ZipHigh(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D16 RID: 15638 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> ZipHigh(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D17 RID: 15639 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ZipHigh(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D18 RID: 15640 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> ZipHigh(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D19 RID: 15641 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> ZipHigh(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1A RID: 15642 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ZipHigh(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1B RID: 15643 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> ZipHigh(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1C RID: 15644 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> ZipHigh(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1D RID: 15645 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> ZipHigh(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1E RID: 15646 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> ZipHigh(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D1F RID: 15647 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ZipHigh(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D20 RID: 15648 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<byte> ZipLow(Vector64<byte> left, Vector64<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D21 RID: 15649 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> ZipLow(Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D22 RID: 15650 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> ZipLow(Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D23 RID: 15651 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<sbyte> ZipLow(Vector64<sbyte> left, Vector64<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D24 RID: 15652 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<float> ZipLow(Vector64<float> left, Vector64<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D25 RID: 15653 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<ushort> ZipLow(Vector64<ushort> left, Vector64<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D26 RID: 15654 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<uint> ZipLow(Vector64<uint> left, Vector64<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D27 RID: 15655 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<byte> ZipLow(Vector128<byte> left, Vector128<byte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D28 RID: 15656 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<double> ZipLow(Vector128<double> left, Vector128<double> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D29 RID: 15657 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<short> ZipLow(Vector128<short> left, Vector128<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2A RID: 15658 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<int> ZipLow(Vector128<int> left, Vector128<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2B RID: 15659 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<long> ZipLow(Vector128<long> left, Vector128<long> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2C RID: 15660 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<sbyte> ZipLow(Vector128<sbyte> left, Vector128<sbyte> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2D RID: 15661 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<float> ZipLow(Vector128<float> left, Vector128<float> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2E RID: 15662 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ushort> ZipLow(Vector128<ushort> left, Vector128<ushort> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D2F RID: 15663 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<uint> ZipLow(Vector128<uint> left, Vector128<uint> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D30 RID: 15664 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector128<ulong> ZipLow(Vector128<ulong> left, Vector128<ulong> right)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
