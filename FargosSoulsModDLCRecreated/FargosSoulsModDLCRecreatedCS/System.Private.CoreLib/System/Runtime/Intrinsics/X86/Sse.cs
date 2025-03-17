using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200043F RID: 1087
	[CLSCompliant(false)]
	[Intrinsic]
	public abstract class Sse : X86Base
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x00173F5F File Offset: 0x0017315F
		public new static bool IsSupported
		{
			get
			{
				return Sse.IsSupported;
			}
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x00173F66 File Offset: 0x00173166
		public static Vector128<float> Add(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Add(left, right);
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x00173F6F File Offset: 0x0017316F
		public static Vector128<float> AddScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.AddScalar(left, right);
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x00173F78 File Offset: 0x00173178
		public static Vector128<float> And(Vector128<float> left, Vector128<float> right)
		{
			return Sse.And(left, right);
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x00173F81 File Offset: 0x00173181
		public static Vector128<float> AndNot(Vector128<float> left, Vector128<float> right)
		{
			return Sse.AndNot(left, right);
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x00173F8A File Offset: 0x0017318A
		public static Vector128<float> CompareEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareEqual(left, right);
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x00173F93 File Offset: 0x00173193
		public static bool CompareScalarOrderedEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedEqual(left, right);
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00173F9C File Offset: 0x0017319C
		public static bool CompareScalarUnorderedEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedEqual(left, right);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x00173FA5 File Offset: 0x001731A5
		public static Vector128<float> CompareScalarEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarEqual(left, right);
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x00173FAE File Offset: 0x001731AE
		public static Vector128<float> CompareGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareGreaterThan(left, right);
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x00173FB7 File Offset: 0x001731B7
		public static bool CompareScalarOrderedGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedGreaterThan(left, right);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00173FC0 File Offset: 0x001731C0
		public static bool CompareScalarUnorderedGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedGreaterThan(left, right);
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x00173FC9 File Offset: 0x001731C9
		public static Vector128<float> CompareScalarGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarGreaterThan(left, right);
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x00173FD2 File Offset: 0x001731D2
		public static Vector128<float> CompareGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x00173FDB File Offset: 0x001731DB
		public static bool CompareScalarOrderedGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x00173FE4 File Offset: 0x001731E4
		public static bool CompareScalarUnorderedGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x00173FED File Offset: 0x001731ED
		public static Vector128<float> CompareScalarGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x00173FF6 File Offset: 0x001731F6
		public static Vector128<float> CompareLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareLessThan(left, right);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x00173FFF File Offset: 0x001731FF
		public static bool CompareScalarOrderedLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedLessThan(left, right);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x00174008 File Offset: 0x00173208
		public static bool CompareScalarUnorderedLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedLessThan(left, right);
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x00174011 File Offset: 0x00173211
		public static Vector128<float> CompareScalarLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarLessThan(left, right);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0017401A File Offset: 0x0017321A
		public static Vector128<float> CompareLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareLessThanOrEqual(left, right);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x00174023 File Offset: 0x00173223
		public static bool CompareScalarOrderedLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedLessThanOrEqual(left, right);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0017402C File Offset: 0x0017322C
		public static bool CompareScalarUnorderedLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedLessThanOrEqual(left, right);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x00174035 File Offset: 0x00173235
		public static Vector128<float> CompareScalarLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarLessThanOrEqual(left, right);
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0017403E File Offset: 0x0017323E
		public static Vector128<float> CompareNotEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareNotEqual(left, right);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x00174047 File Offset: 0x00173247
		public static bool CompareScalarOrderedNotEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrderedNotEqual(left, right);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x00174050 File Offset: 0x00173250
		public static bool CompareScalarUnorderedNotEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnorderedNotEqual(left, right);
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x00174059 File Offset: 0x00173259
		public static Vector128<float> CompareScalarNotEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarNotEqual(left, right);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x00174062 File Offset: 0x00173262
		public static Vector128<float> CompareNotGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareNotGreaterThan(left, right);
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0017406B File Offset: 0x0017326B
		public static Vector128<float> CompareScalarNotGreaterThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarNotGreaterThan(left, right);
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x00174074 File Offset: 0x00173274
		public static Vector128<float> CompareNotGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareNotGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0017407D File Offset: 0x0017327D
		public static Vector128<float> CompareScalarNotGreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarNotGreaterThanOrEqual(left, right);
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x00174086 File Offset: 0x00173286
		public static Vector128<float> CompareNotLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareNotLessThan(left, right);
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0017408F File Offset: 0x0017328F
		public static Vector128<float> CompareScalarNotLessThan(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarNotLessThan(left, right);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x00174098 File Offset: 0x00173298
		public static Vector128<float> CompareNotLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareNotLessThanOrEqual(left, right);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x001740A1 File Offset: 0x001732A1
		public static Vector128<float> CompareScalarNotLessThanOrEqual(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarNotLessThanOrEqual(left, right);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x001740AA File Offset: 0x001732AA
		public static Vector128<float> CompareOrdered(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareOrdered(left, right);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x001740B3 File Offset: 0x001732B3
		public static Vector128<float> CompareScalarOrdered(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarOrdered(left, right);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x001740BC File Offset: 0x001732BC
		public static Vector128<float> CompareUnordered(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareUnordered(left, right);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x001740C5 File Offset: 0x001732C5
		public static Vector128<float> CompareScalarUnordered(Vector128<float> left, Vector128<float> right)
		{
			return Sse.CompareScalarUnordered(left, right);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x001740CE File Offset: 0x001732CE
		public static int ConvertToInt32(Vector128<float> value)
		{
			return Sse.ConvertToInt32(value);
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x001740D6 File Offset: 0x001732D6
		public static Vector128<float> ConvertScalarToVector128Single(Vector128<float> upper, int value)
		{
			return Sse.ConvertScalarToVector128Single(upper, value);
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x001740DF File Offset: 0x001732DF
		public static int ConvertToInt32WithTruncation(Vector128<float> value)
		{
			return Sse.ConvertToInt32WithTruncation(value);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x001740E7 File Offset: 0x001732E7
		public static Vector128<float> Divide(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Divide(left, right);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x001740F0 File Offset: 0x001732F0
		public static Vector128<float> DivideScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.DivideScalar(left, right);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x001740F9 File Offset: 0x001732F9
		public unsafe static Vector128<float> LoadVector128(float* address)
		{
			return Sse.LoadVector128(address);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x00174101 File Offset: 0x00173301
		public unsafe static Vector128<float> LoadScalarVector128(float* address)
		{
			return Sse.LoadScalarVector128(address);
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x00174109 File Offset: 0x00173309
		public unsafe static Vector128<float> LoadAlignedVector128(float* address)
		{
			return Sse.LoadAlignedVector128(address);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x00174111 File Offset: 0x00173311
		public unsafe static Vector128<float> LoadHigh(Vector128<float> lower, float* address)
		{
			return Sse.LoadHigh(lower, address);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0017411A File Offset: 0x0017331A
		public unsafe static Vector128<float> LoadLow(Vector128<float> upper, float* address)
		{
			return Sse.LoadLow(upper, address);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x00174123 File Offset: 0x00173323
		public static Vector128<float> Max(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Max(left, right);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0017412C File Offset: 0x0017332C
		public static Vector128<float> MaxScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.MaxScalar(left, right);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x00174135 File Offset: 0x00173335
		public static Vector128<float> Min(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Min(left, right);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0017413E File Offset: 0x0017333E
		public static Vector128<float> MinScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.MinScalar(left, right);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x00174147 File Offset: 0x00173347
		public static Vector128<float> MoveScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse.MoveScalar(upper, value);
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x00174150 File Offset: 0x00173350
		public static Vector128<float> MoveHighToLow(Vector128<float> left, Vector128<float> right)
		{
			return Sse.MoveHighToLow(left, right);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x00174159 File Offset: 0x00173359
		public static Vector128<float> MoveLowToHigh(Vector128<float> left, Vector128<float> right)
		{
			return Sse.MoveLowToHigh(left, right);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x00174162 File Offset: 0x00173362
		public static int MoveMask(Vector128<float> value)
		{
			return Sse.MoveMask(value);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0017416A File Offset: 0x0017336A
		public static Vector128<float> Multiply(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Multiply(left, right);
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x00174173 File Offset: 0x00173373
		public static Vector128<float> MultiplyScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.MultiplyScalar(left, right);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0017417C File Offset: 0x0017337C
		public static Vector128<float> Or(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Or(left, right);
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x00174185 File Offset: 0x00173385
		public unsafe static void Prefetch0(void* address)
		{
			Sse.Prefetch0(address);
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0017418D File Offset: 0x0017338D
		public unsafe static void Prefetch1(void* address)
		{
			Sse.Prefetch1(address);
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x00174195 File Offset: 0x00173395
		public unsafe static void Prefetch2(void* address)
		{
			Sse.Prefetch2(address);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0017419D File Offset: 0x0017339D
		public unsafe static void PrefetchNonTemporal(void* address)
		{
			Sse.PrefetchNonTemporal(address);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x001741A5 File Offset: 0x001733A5
		public static Vector128<float> Reciprocal(Vector128<float> value)
		{
			return Sse.Reciprocal(value);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x001741AD File Offset: 0x001733AD
		public static Vector128<float> ReciprocalScalar(Vector128<float> value)
		{
			return Sse.ReciprocalScalar(value);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x001741B5 File Offset: 0x001733B5
		public static Vector128<float> ReciprocalScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse.ReciprocalScalar(upper, value);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x001741BE File Offset: 0x001733BE
		public static Vector128<float> ReciprocalSqrt(Vector128<float> value)
		{
			return Sse.ReciprocalSqrt(value);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x001741C6 File Offset: 0x001733C6
		public static Vector128<float> ReciprocalSqrtScalar(Vector128<float> value)
		{
			return Sse.ReciprocalSqrtScalar(value);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x001741CE File Offset: 0x001733CE
		public static Vector128<float> ReciprocalSqrtScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse.ReciprocalSqrtScalar(upper, value);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x001741D7 File Offset: 0x001733D7
		public static Vector128<float> Shuffle(Vector128<float> left, Vector128<float> right, byte control)
		{
			return Sse.Shuffle(left, right, control);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x001741E1 File Offset: 0x001733E1
		public static Vector128<float> Sqrt(Vector128<float> value)
		{
			return Sse.Sqrt(value);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x001741E9 File Offset: 0x001733E9
		public static Vector128<float> SqrtScalar(Vector128<float> value)
		{
			return Sse.SqrtScalar(value);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x001741F1 File Offset: 0x001733F1
		public static Vector128<float> SqrtScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse.SqrtScalar(upper, value);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x001741FA File Offset: 0x001733FA
		public unsafe static void StoreAligned(float* address, Vector128<float> source)
		{
			Sse.StoreAligned(address, source);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x00174203 File Offset: 0x00173403
		public unsafe static void StoreAlignedNonTemporal(float* address, Vector128<float> source)
		{
			Sse.StoreAlignedNonTemporal(address, source);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x0017420C File Offset: 0x0017340C
		public unsafe static void Store(float* address, Vector128<float> source)
		{
			Sse.Store(address, source);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00174215 File Offset: 0x00173415
		public static void StoreFence()
		{
			Sse.StoreFence();
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x0017421C File Offset: 0x0017341C
		public unsafe static void StoreScalar(float* address, Vector128<float> source)
		{
			Sse.StoreScalar(address, source);
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x00174225 File Offset: 0x00173425
		public unsafe static void StoreHigh(float* address, Vector128<float> source)
		{
			Sse.StoreHigh(address, source);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0017422E File Offset: 0x0017342E
		public unsafe static void StoreLow(float* address, Vector128<float> source)
		{
			Sse.StoreLow(address, source);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00174237 File Offset: 0x00173437
		public static Vector128<float> Subtract(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Subtract(left, right);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x00174240 File Offset: 0x00173440
		public static Vector128<float> SubtractScalar(Vector128<float> left, Vector128<float> right)
		{
			return Sse.SubtractScalar(left, right);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x00174249 File Offset: 0x00173449
		public static Vector128<float> UnpackHigh(Vector128<float> left, Vector128<float> right)
		{
			return Sse.UnpackHigh(left, right);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x00174252 File Offset: 0x00173452
		public static Vector128<float> UnpackLow(Vector128<float> left, Vector128<float> right)
		{
			return Sse.UnpackLow(left, right);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0017425B File Offset: 0x0017345B
		public static Vector128<float> Xor(Vector128<float> left, Vector128<float> right)
		{
			return Sse.Xor(left, right);
		}

		// Token: 0x02000440 RID: 1088
		[Intrinsic]
		public new abstract class X64 : X86Base.X64
		{
			// Token: 0x17000A34 RID: 2612
			// (get) Token: 0x060040C5 RID: 16581 RVA: 0x00174264 File Offset: 0x00173464
			public new static bool IsSupported
			{
				get
				{
					return Sse.X64.IsSupported;
				}
			}

			// Token: 0x060040C6 RID: 16582 RVA: 0x0017426B File Offset: 0x0017346B
			public static long ConvertToInt64(Vector128<float> value)
			{
				return Sse.X64.ConvertToInt64(value);
			}

			// Token: 0x060040C7 RID: 16583 RVA: 0x00174273 File Offset: 0x00173473
			public static Vector128<float> ConvertScalarToVector128Single(Vector128<float> upper, long value)
			{
				return Sse.X64.ConvertScalarToVector128Single(upper, value);
			}

			// Token: 0x060040C8 RID: 16584 RVA: 0x0017427C File Offset: 0x0017347C
			public static long ConvertToInt64WithTruncation(Vector128<float> value)
			{
				return Sse.X64.ConvertToInt64WithTruncation(value);
			}
		}
	}
}
