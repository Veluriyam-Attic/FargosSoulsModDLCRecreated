using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000431 RID: 1073
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Avx2 : Avx
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x00171CD0 File Offset: 0x00170ED0
		public new static bool IsSupported
		{
			get
			{
				return Avx2.IsSupported;
			}
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x00171CD7 File Offset: 0x00170ED7
		public static Vector256<byte> Abs(Vector256<sbyte> value)
		{
			return Avx2.Abs(value);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x00171CDF File Offset: 0x00170EDF
		public static Vector256<ushort> Abs(Vector256<short> value)
		{
			return Avx2.Abs(value);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x00171CE7 File Offset: 0x00170EE7
		public static Vector256<uint> Abs(Vector256<int> value)
		{
			return Avx2.Abs(value);
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x00171CEF File Offset: 0x00170EEF
		public static Vector256<sbyte> Add(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x00171CF8 File Offset: 0x00170EF8
		public static Vector256<byte> Add(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x00171D01 File Offset: 0x00170F01
		public static Vector256<short> Add(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x00171D0A File Offset: 0x00170F0A
		public static Vector256<ushort> Add(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x00171D13 File Offset: 0x00170F13
		public static Vector256<int> Add(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x00171D1C File Offset: 0x00170F1C
		public static Vector256<uint> Add(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00171D25 File Offset: 0x00170F25
		public static Vector256<long> Add(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x00171D2E File Offset: 0x00170F2E
		public static Vector256<ulong> Add(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.Add(left, right);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x00171D37 File Offset: 0x00170F37
		public static Vector256<sbyte> AddSaturate(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.AddSaturate(left, right);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x00171D40 File Offset: 0x00170F40
		public static Vector256<byte> AddSaturate(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.AddSaturate(left, right);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x00171D49 File Offset: 0x00170F49
		public static Vector256<short> AddSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.AddSaturate(left, right);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x00171D52 File Offset: 0x00170F52
		public static Vector256<ushort> AddSaturate(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.AddSaturate(left, right);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00171D5B File Offset: 0x00170F5B
		public static Vector256<sbyte> AlignRight(Vector256<sbyte> left, Vector256<sbyte> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x00171D65 File Offset: 0x00170F65
		public static Vector256<byte> AlignRight(Vector256<byte> left, Vector256<byte> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x00171D6F File Offset: 0x00170F6F
		public static Vector256<short> AlignRight(Vector256<short> left, Vector256<short> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00171D79 File Offset: 0x00170F79
		public static Vector256<ushort> AlignRight(Vector256<ushort> left, Vector256<ushort> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00171D83 File Offset: 0x00170F83
		public static Vector256<int> AlignRight(Vector256<int> left, Vector256<int> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x00171D8D File Offset: 0x00170F8D
		public static Vector256<uint> AlignRight(Vector256<uint> left, Vector256<uint> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x00171D97 File Offset: 0x00170F97
		public static Vector256<long> AlignRight(Vector256<long> left, Vector256<long> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00171DA1 File Offset: 0x00170FA1
		public static Vector256<ulong> AlignRight(Vector256<ulong> left, Vector256<ulong> right, byte mask)
		{
			return Avx2.AlignRight(left, right, mask);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x00171DAB File Offset: 0x00170FAB
		public static Vector256<sbyte> And(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x00171DB4 File Offset: 0x00170FB4
		public static Vector256<byte> And(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x00171DBD File Offset: 0x00170FBD
		public static Vector256<short> And(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00171DC6 File Offset: 0x00170FC6
		public static Vector256<ushort> And(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00171DCF File Offset: 0x00170FCF
		public static Vector256<int> And(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00171DD8 File Offset: 0x00170FD8
		public static Vector256<uint> And(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x00171DE1 File Offset: 0x00170FE1
		public static Vector256<long> And(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00171DEA File Offset: 0x00170FEA
		public static Vector256<ulong> And(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.And(left, right);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00171DF3 File Offset: 0x00170FF3
		public static Vector256<sbyte> AndNot(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x00171DFC File Offset: 0x00170FFC
		public static Vector256<byte> AndNot(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00171E05 File Offset: 0x00171005
		public static Vector256<short> AndNot(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x00171E0E File Offset: 0x0017100E
		public static Vector256<ushort> AndNot(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x00171E17 File Offset: 0x00171017
		public static Vector256<int> AndNot(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00171E20 File Offset: 0x00171020
		public static Vector256<uint> AndNot(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x00171E29 File Offset: 0x00171029
		public static Vector256<long> AndNot(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00171E32 File Offset: 0x00171032
		public static Vector256<ulong> AndNot(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.AndNot(left, right);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00171E3B File Offset: 0x0017103B
		public static Vector256<byte> Average(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Average(left, right);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x00171E44 File Offset: 0x00171044
		public static Vector256<ushort> Average(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Average(left, right);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00171E4D File Offset: 0x0017104D
		public static Vector128<int> Blend(Vector128<int> left, Vector128<int> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00171E57 File Offset: 0x00171057
		public static Vector128<uint> Blend(Vector128<uint> left, Vector128<uint> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x00171E61 File Offset: 0x00171061
		public static Vector256<short> Blend(Vector256<short> left, Vector256<short> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00171E6B File Offset: 0x0017106B
		public static Vector256<ushort> Blend(Vector256<ushort> left, Vector256<ushort> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00171E75 File Offset: 0x00171075
		public static Vector256<int> Blend(Vector256<int> left, Vector256<int> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00171E7F File Offset: 0x0017107F
		public static Vector256<uint> Blend(Vector256<uint> left, Vector256<uint> right, byte control)
		{
			return Avx2.Blend(left, right, control);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00171E89 File Offset: 0x00171089
		public static Vector256<sbyte> BlendVariable(Vector256<sbyte> left, Vector256<sbyte> right, Vector256<sbyte> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00171E93 File Offset: 0x00171093
		public static Vector256<byte> BlendVariable(Vector256<byte> left, Vector256<byte> right, Vector256<byte> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00171E9D File Offset: 0x0017109D
		public static Vector256<short> BlendVariable(Vector256<short> left, Vector256<short> right, Vector256<short> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x00171EA7 File Offset: 0x001710A7
		public static Vector256<ushort> BlendVariable(Vector256<ushort> left, Vector256<ushort> right, Vector256<ushort> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00171EB1 File Offset: 0x001710B1
		public static Vector256<int> BlendVariable(Vector256<int> left, Vector256<int> right, Vector256<int> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x00171EBB File Offset: 0x001710BB
		public static Vector256<uint> BlendVariable(Vector256<uint> left, Vector256<uint> right, Vector256<uint> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00171EC5 File Offset: 0x001710C5
		public static Vector256<long> BlendVariable(Vector256<long> left, Vector256<long> right, Vector256<long> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x00171ECF File Offset: 0x001710CF
		public static Vector256<ulong> BlendVariable(Vector256<ulong> left, Vector256<ulong> right, Vector256<ulong> mask)
		{
			return Avx2.BlendVariable(left, right, mask);
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x00171ED9 File Offset: 0x001710D9
		public static Vector128<byte> BroadcastScalarToVector128(Vector128<byte> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00171EE1 File Offset: 0x001710E1
		public static Vector128<sbyte> BroadcastScalarToVector128(Vector128<sbyte> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00171EE9 File Offset: 0x001710E9
		public static Vector128<short> BroadcastScalarToVector128(Vector128<short> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00171EF1 File Offset: 0x001710F1
		public static Vector128<ushort> BroadcastScalarToVector128(Vector128<ushort> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00171EF9 File Offset: 0x001710F9
		public static Vector128<int> BroadcastScalarToVector128(Vector128<int> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x00171F01 File Offset: 0x00171101
		public static Vector128<uint> BroadcastScalarToVector128(Vector128<uint> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00171F09 File Offset: 0x00171109
		public static Vector128<long> BroadcastScalarToVector128(Vector128<long> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00171F11 File Offset: 0x00171111
		public static Vector128<ulong> BroadcastScalarToVector128(Vector128<ulong> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00171F19 File Offset: 0x00171119
		public static Vector128<float> BroadcastScalarToVector128(Vector128<float> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00171F21 File Offset: 0x00171121
		public static Vector128<double> BroadcastScalarToVector128(Vector128<double> value)
		{
			return Avx2.BroadcastScalarToVector128(value);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00171F29 File Offset: 0x00171129
		public unsafe static Vector128<byte> BroadcastScalarToVector128(byte* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x00171F31 File Offset: 0x00171131
		public unsafe static Vector128<sbyte> BroadcastScalarToVector128(sbyte* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x00171F39 File Offset: 0x00171139
		public unsafe static Vector128<short> BroadcastScalarToVector128(short* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00171F41 File Offset: 0x00171141
		public unsafe static Vector128<ushort> BroadcastScalarToVector128(ushort* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x00171F49 File Offset: 0x00171149
		public unsafe static Vector128<int> BroadcastScalarToVector128(int* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00171F51 File Offset: 0x00171151
		public unsafe static Vector128<uint> BroadcastScalarToVector128(uint* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x00171F59 File Offset: 0x00171159
		public unsafe static Vector128<long> BroadcastScalarToVector128(long* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00171F61 File Offset: 0x00171161
		public unsafe static Vector128<ulong> BroadcastScalarToVector128(ulong* source)
		{
			return Avx2.BroadcastScalarToVector128(source);
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00171F69 File Offset: 0x00171169
		public static Vector256<byte> BroadcastScalarToVector256(Vector128<byte> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00171F71 File Offset: 0x00171171
		public static Vector256<sbyte> BroadcastScalarToVector256(Vector128<sbyte> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00171F79 File Offset: 0x00171179
		public static Vector256<short> BroadcastScalarToVector256(Vector128<short> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x00171F81 File Offset: 0x00171181
		public static Vector256<ushort> BroadcastScalarToVector256(Vector128<ushort> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x00171F89 File Offset: 0x00171189
		public static Vector256<int> BroadcastScalarToVector256(Vector128<int> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x00171F91 File Offset: 0x00171191
		public static Vector256<uint> BroadcastScalarToVector256(Vector128<uint> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x00171F99 File Offset: 0x00171199
		public static Vector256<long> BroadcastScalarToVector256(Vector128<long> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x00171FA1 File Offset: 0x001711A1
		public static Vector256<ulong> BroadcastScalarToVector256(Vector128<ulong> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x00171FA9 File Offset: 0x001711A9
		public static Vector256<float> BroadcastScalarToVector256(Vector128<float> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x00171FB1 File Offset: 0x001711B1
		public static Vector256<double> BroadcastScalarToVector256(Vector128<double> value)
		{
			return Avx2.BroadcastScalarToVector256(value);
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x00171FB9 File Offset: 0x001711B9
		public unsafe static Vector256<byte> BroadcastScalarToVector256(byte* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x00171FC1 File Offset: 0x001711C1
		public unsafe static Vector256<sbyte> BroadcastScalarToVector256(sbyte* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x00171FC9 File Offset: 0x001711C9
		public unsafe static Vector256<short> BroadcastScalarToVector256(short* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00171FD1 File Offset: 0x001711D1
		public unsafe static Vector256<ushort> BroadcastScalarToVector256(ushort* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00171FD9 File Offset: 0x001711D9
		public unsafe static Vector256<int> BroadcastScalarToVector256(int* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x00171FE1 File Offset: 0x001711E1
		public unsafe static Vector256<uint> BroadcastScalarToVector256(uint* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00171FE9 File Offset: 0x001711E9
		public unsafe static Vector256<long> BroadcastScalarToVector256(long* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00171FF1 File Offset: 0x001711F1
		public unsafe static Vector256<ulong> BroadcastScalarToVector256(ulong* source)
		{
			return Avx2.BroadcastScalarToVector256(source);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x00171FF9 File Offset: 0x001711F9
		public unsafe static Vector256<sbyte> BroadcastVector128ToVector256(sbyte* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x00172001 File Offset: 0x00171201
		public unsafe static Vector256<byte> BroadcastVector128ToVector256(byte* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x00172009 File Offset: 0x00171209
		public unsafe static Vector256<short> BroadcastVector128ToVector256(short* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x00172011 File Offset: 0x00171211
		public unsafe static Vector256<ushort> BroadcastVector128ToVector256(ushort* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x00172019 File Offset: 0x00171219
		public unsafe static Vector256<int> BroadcastVector128ToVector256(int* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x00172021 File Offset: 0x00171221
		public unsafe static Vector256<uint> BroadcastVector128ToVector256(uint* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x00172029 File Offset: 0x00171229
		public unsafe static Vector256<long> BroadcastVector128ToVector256(long* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00172031 File Offset: 0x00171231
		public unsafe static Vector256<ulong> BroadcastVector128ToVector256(ulong* address)
		{
			return Avx2.BroadcastVector128ToVector256(address);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x00172039 File Offset: 0x00171239
		public static Vector256<sbyte> CompareEqual(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x00172042 File Offset: 0x00171242
		public static Vector256<byte> CompareEqual(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0017204B File Offset: 0x0017124B
		public static Vector256<short> CompareEqual(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00172054 File Offset: 0x00171254
		public static Vector256<ushort> CompareEqual(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0017205D File Offset: 0x0017125D
		public static Vector256<int> CompareEqual(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00172066 File Offset: 0x00171266
		public static Vector256<uint> CompareEqual(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0017206F File Offset: 0x0017126F
		public static Vector256<long> CompareEqual(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00172078 File Offset: 0x00171278
		public static Vector256<ulong> CompareEqual(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.CompareEqual(left, right);
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x00172081 File Offset: 0x00171281
		public static Vector256<sbyte> CompareGreaterThan(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.CompareGreaterThan(left, right);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0017208A File Offset: 0x0017128A
		public static Vector256<short> CompareGreaterThan(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.CompareGreaterThan(left, right);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x00172093 File Offset: 0x00171293
		public static Vector256<int> CompareGreaterThan(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.CompareGreaterThan(left, right);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0017209C File Offset: 0x0017129C
		public static Vector256<long> CompareGreaterThan(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.CompareGreaterThan(left, right);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x001720A5 File Offset: 0x001712A5
		public static int ConvertToInt32(Vector256<int> value)
		{
			return Avx2.ConvertToInt32(value);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x001720AD File Offset: 0x001712AD
		public static uint ConvertToUInt32(Vector256<uint> value)
		{
			return Avx2.ConvertToUInt32(value);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x001720B5 File Offset: 0x001712B5
		public static Vector256<short> ConvertToVector256Int16(Vector128<sbyte> value)
		{
			return Avx2.ConvertToVector256Int16(value);
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x001720BD File Offset: 0x001712BD
		public static Vector256<short> ConvertToVector256Int16(Vector128<byte> value)
		{
			return Avx2.ConvertToVector256Int16(value);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x001720C5 File Offset: 0x001712C5
		public static Vector256<int> ConvertToVector256Int32(Vector128<sbyte> value)
		{
			return Avx2.ConvertToVector256Int32(value);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x001720CD File Offset: 0x001712CD
		public static Vector256<int> ConvertToVector256Int32(Vector128<byte> value)
		{
			return Avx2.ConvertToVector256Int32(value);
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x001720D5 File Offset: 0x001712D5
		public static Vector256<int> ConvertToVector256Int32(Vector128<short> value)
		{
			return Avx2.ConvertToVector256Int32(value);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x001720DD File Offset: 0x001712DD
		public static Vector256<int> ConvertToVector256Int32(Vector128<ushort> value)
		{
			return Avx2.ConvertToVector256Int32(value);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x001720E5 File Offset: 0x001712E5
		public static Vector256<long> ConvertToVector256Int64(Vector128<sbyte> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x001720ED File Offset: 0x001712ED
		public static Vector256<long> ConvertToVector256Int64(Vector128<byte> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x001720F5 File Offset: 0x001712F5
		public static Vector256<long> ConvertToVector256Int64(Vector128<short> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x001720FD File Offset: 0x001712FD
		public static Vector256<long> ConvertToVector256Int64(Vector128<ushort> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00172105 File Offset: 0x00171305
		public static Vector256<long> ConvertToVector256Int64(Vector128<int> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0017210D File Offset: 0x0017130D
		public static Vector256<long> ConvertToVector256Int64(Vector128<uint> value)
		{
			return Avx2.ConvertToVector256Int64(value);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x00172115 File Offset: 0x00171315
		public unsafe static Vector256<short> ConvertToVector256Int16(sbyte* address)
		{
			return Avx2.ConvertToVector256Int16(address);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0017211D File Offset: 0x0017131D
		public unsafe static Vector256<short> ConvertToVector256Int16(byte* address)
		{
			return Avx2.ConvertToVector256Int16(address);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x00172125 File Offset: 0x00171325
		public unsafe static Vector256<int> ConvertToVector256Int32(sbyte* address)
		{
			return Avx2.ConvertToVector256Int32(address);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0017212D File Offset: 0x0017132D
		public unsafe static Vector256<int> ConvertToVector256Int32(byte* address)
		{
			return Avx2.ConvertToVector256Int32(address);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x00172135 File Offset: 0x00171335
		public unsafe static Vector256<int> ConvertToVector256Int32(short* address)
		{
			return Avx2.ConvertToVector256Int32(address);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0017213D File Offset: 0x0017133D
		public unsafe static Vector256<int> ConvertToVector256Int32(ushort* address)
		{
			return Avx2.ConvertToVector256Int32(address);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x00172145 File Offset: 0x00171345
		public unsafe static Vector256<long> ConvertToVector256Int64(sbyte* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0017214D File Offset: 0x0017134D
		public unsafe static Vector256<long> ConvertToVector256Int64(byte* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x00172155 File Offset: 0x00171355
		public unsafe static Vector256<long> ConvertToVector256Int64(short* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0017215D File Offset: 0x0017135D
		public unsafe static Vector256<long> ConvertToVector256Int64(ushort* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00172165 File Offset: 0x00171365
		public unsafe static Vector256<long> ConvertToVector256Int64(int* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0017216D File Offset: 0x0017136D
		public unsafe static Vector256<long> ConvertToVector256Int64(uint* address)
		{
			return Avx2.ConvertToVector256Int64(address);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00172175 File Offset: 0x00171375
		public new static Vector128<sbyte> ExtractVector128(Vector256<sbyte> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0017217E File Offset: 0x0017137E
		public new static Vector128<byte> ExtractVector128(Vector256<byte> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00172187 File Offset: 0x00171387
		public new static Vector128<short> ExtractVector128(Vector256<short> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00172190 File Offset: 0x00171390
		public new static Vector128<ushort> ExtractVector128(Vector256<ushort> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00172199 File Offset: 0x00171399
		public new static Vector128<int> ExtractVector128(Vector256<int> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x001721A2 File Offset: 0x001713A2
		public new static Vector128<uint> ExtractVector128(Vector256<uint> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x001721AB File Offset: 0x001713AB
		public new static Vector128<long> ExtractVector128(Vector256<long> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x001721B4 File Offset: 0x001713B4
		public new static Vector128<ulong> ExtractVector128(Vector256<ulong> value, byte index)
		{
			return Avx2.ExtractVector128(value, index);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x001721C0 File Offset: 0x001713C0
		public unsafe static Vector128<int> GatherVector128(int* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00172224 File Offset: 0x00171424
		public unsafe static Vector128<uint> GatherVector128(uint* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00172288 File Offset: 0x00171488
		public unsafe static Vector128<long> GatherVector128(long* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x001722EC File Offset: 0x001714EC
		public unsafe static Vector128<ulong> GatherVector128(ulong* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00172350 File Offset: 0x00171550
		public unsafe static Vector128<float> GatherVector128(float* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x001723B4 File Offset: 0x001715B4
		public unsafe static Vector128<double> GatherVector128(double* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00172418 File Offset: 0x00171618
		public unsafe static Vector128<int> GatherVector128(int* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x0017247C File Offset: 0x0017167C
		public unsafe static Vector128<uint> GatherVector128(uint* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x001724E0 File Offset: 0x001716E0
		public unsafe static Vector128<long> GatherVector128(long* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00172544 File Offset: 0x00171744
		public unsafe static Vector128<ulong> GatherVector128(ulong* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x001725A8 File Offset: 0x001717A8
		public unsafe static Vector128<float> GatherVector128(float* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0017260C File Offset: 0x0017180C
		public unsafe static Vector128<double> GatherVector128(double* baseAddress, Vector128<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x00172670 File Offset: 0x00171870
		public unsafe static Vector256<int> GatherVector256(int* baseAddress, Vector256<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x001726D4 File Offset: 0x001718D4
		public unsafe static Vector256<uint> GatherVector256(uint* baseAddress, Vector256<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x00172738 File Offset: 0x00171938
		public unsafe static Vector256<long> GatherVector256(long* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0017279C File Offset: 0x0017199C
		public unsafe static Vector256<ulong> GatherVector256(ulong* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x00172800 File Offset: 0x00171A00
		public unsafe static Vector256<float> GatherVector256(float* baseAddress, Vector256<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00172864 File Offset: 0x00171A64
		public unsafe static Vector256<double> GatherVector256(double* baseAddress, Vector128<int> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x001728C8 File Offset: 0x00171AC8
		public unsafe static Vector128<int> GatherVector128(int* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x0017292C File Offset: 0x00171B2C
		public unsafe static Vector128<uint> GatherVector128(uint* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x00172990 File Offset: 0x00171B90
		public unsafe static Vector256<long> GatherVector256(long* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x001729F4 File Offset: 0x00171BF4
		public unsafe static Vector256<ulong> GatherVector256(ulong* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00172A58 File Offset: 0x00171C58
		public unsafe static Vector128<float> GatherVector128(float* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector128(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector128(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector128(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector128(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00172ABC File Offset: 0x00171CBC
		public unsafe static Vector256<double> GatherVector256(double* baseAddress, Vector256<long> index, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherVector256(baseAddress, index, 1);
			case 2:
				return Avx2.GatherVector256(baseAddress, index, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherVector256(baseAddress, index, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherVector256(baseAddress, index, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x00172B20 File Offset: 0x00171D20
		public unsafe static Vector128<int> GatherMaskVector128(Vector128<int> source, int* baseAddress, Vector128<int> index, Vector128<int> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x00172B90 File Offset: 0x00171D90
		public unsafe static Vector128<uint> GatherMaskVector128(Vector128<uint> source, uint* baseAddress, Vector128<int> index, Vector128<uint> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x00172C00 File Offset: 0x00171E00
		public unsafe static Vector128<long> GatherMaskVector128(Vector128<long> source, long* baseAddress, Vector128<int> index, Vector128<long> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00172C70 File Offset: 0x00171E70
		public unsafe static Vector128<ulong> GatherMaskVector128(Vector128<ulong> source, ulong* baseAddress, Vector128<int> index, Vector128<ulong> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x00172CE0 File Offset: 0x00171EE0
		public unsafe static Vector128<float> GatherMaskVector128(Vector128<float> source, float* baseAddress, Vector128<int> index, Vector128<float> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00172D50 File Offset: 0x00171F50
		public unsafe static Vector128<double> GatherMaskVector128(Vector128<double> source, double* baseAddress, Vector128<int> index, Vector128<double> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00172DC0 File Offset: 0x00171FC0
		public unsafe static Vector128<int> GatherMaskVector128(Vector128<int> source, int* baseAddress, Vector128<long> index, Vector128<int> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00172E30 File Offset: 0x00172030
		public unsafe static Vector128<uint> GatherMaskVector128(Vector128<uint> source, uint* baseAddress, Vector128<long> index, Vector128<uint> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00172EA0 File Offset: 0x001720A0
		public unsafe static Vector128<long> GatherMaskVector128(Vector128<long> source, long* baseAddress, Vector128<long> index, Vector128<long> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00172F10 File Offset: 0x00172110
		public unsafe static Vector128<ulong> GatherMaskVector128(Vector128<ulong> source, ulong* baseAddress, Vector128<long> index, Vector128<ulong> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x00172F80 File Offset: 0x00172180
		public unsafe static Vector128<float> GatherMaskVector128(Vector128<float> source, float* baseAddress, Vector128<long> index, Vector128<float> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x00172FF0 File Offset: 0x001721F0
		public unsafe static Vector128<double> GatherMaskVector128(Vector128<double> source, double* baseAddress, Vector128<long> index, Vector128<double> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x00173060 File Offset: 0x00172260
		public unsafe static Vector256<int> GatherMaskVector256(Vector256<int> source, int* baseAddress, Vector256<int> index, Vector256<int> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001730D0 File Offset: 0x001722D0
		public unsafe static Vector256<uint> GatherMaskVector256(Vector256<uint> source, uint* baseAddress, Vector256<int> index, Vector256<uint> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x00173140 File Offset: 0x00172340
		public unsafe static Vector256<long> GatherMaskVector256(Vector256<long> source, long* baseAddress, Vector128<int> index, Vector256<long> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x001731B0 File Offset: 0x001723B0
		public unsafe static Vector256<ulong> GatherMaskVector256(Vector256<ulong> source, ulong* baseAddress, Vector128<int> index, Vector256<ulong> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00173220 File Offset: 0x00172420
		public unsafe static Vector256<float> GatherMaskVector256(Vector256<float> source, float* baseAddress, Vector256<int> index, Vector256<float> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00173290 File Offset: 0x00172490
		public unsafe static Vector256<double> GatherMaskVector256(Vector256<double> source, double* baseAddress, Vector128<int> index, Vector256<double> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x00173300 File Offset: 0x00172500
		public unsafe static Vector128<int> GatherMaskVector128(Vector128<int> source, int* baseAddress, Vector256<long> index, Vector128<int> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x00173370 File Offset: 0x00172570
		public unsafe static Vector128<uint> GatherMaskVector128(Vector128<uint> source, uint* baseAddress, Vector256<long> index, Vector128<uint> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x001733E0 File Offset: 0x001725E0
		public unsafe static Vector256<long> GatherMaskVector256(Vector256<long> source, long* baseAddress, Vector256<long> index, Vector256<long> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00173450 File Offset: 0x00172650
		public unsafe static Vector256<ulong> GatherMaskVector256(Vector256<ulong> source, ulong* baseAddress, Vector256<long> index, Vector256<ulong> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x001734C0 File Offset: 0x001726C0
		public unsafe static Vector128<float> GatherMaskVector128(Vector128<float> source, float* baseAddress, Vector256<long> index, Vector128<float> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector128(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00173530 File Offset: 0x00172730
		public unsafe static Vector256<double> GatherMaskVector256(Vector256<double> source, double* baseAddress, Vector256<long> index, Vector256<double> mask, byte scale)
		{
			switch (scale)
			{
			case 1:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 1);
			case 2:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 2);
			case 3:
				break;
			case 4:
				return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 4);
			default:
				if (scale == 8)
				{
					return Avx2.GatherMaskVector256(source, baseAddress, index, mask, 8);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("scale");
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0017359D File Offset: 0x0017279D
		public static Vector256<short> HorizontalAdd(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.HorizontalAdd(left, right);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x001735A6 File Offset: 0x001727A6
		public static Vector256<int> HorizontalAdd(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.HorizontalAdd(left, right);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x001735AF File Offset: 0x001727AF
		public static Vector256<short> HorizontalAddSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.HorizontalAddSaturate(left, right);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x001735B8 File Offset: 0x001727B8
		public static Vector256<short> HorizontalSubtract(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.HorizontalSubtract(left, right);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x001735C1 File Offset: 0x001727C1
		public static Vector256<int> HorizontalSubtract(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.HorizontalSubtract(left, right);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x001735CA File Offset: 0x001727CA
		public static Vector256<short> HorizontalSubtractSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.HorizontalSubtractSaturate(left, right);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x001735D3 File Offset: 0x001727D3
		public new static Vector256<sbyte> InsertVector128(Vector256<sbyte> value, Vector128<sbyte> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x001735DD File Offset: 0x001727DD
		public new static Vector256<byte> InsertVector128(Vector256<byte> value, Vector128<byte> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x001735E7 File Offset: 0x001727E7
		public new static Vector256<short> InsertVector128(Vector256<short> value, Vector128<short> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x001735F1 File Offset: 0x001727F1
		public new static Vector256<ushort> InsertVector128(Vector256<ushort> value, Vector128<ushort> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x001735FB File Offset: 0x001727FB
		public new static Vector256<int> InsertVector128(Vector256<int> value, Vector128<int> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x00173605 File Offset: 0x00172805
		public new static Vector256<uint> InsertVector128(Vector256<uint> value, Vector128<uint> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0017360F File Offset: 0x0017280F
		public new static Vector256<long> InsertVector128(Vector256<long> value, Vector128<long> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00173619 File Offset: 0x00172819
		public new static Vector256<ulong> InsertVector128(Vector256<ulong> value, Vector128<ulong> data, byte index)
		{
			return Avx2.InsertVector128(value, data, index);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x00173623 File Offset: 0x00172823
		public unsafe static Vector256<sbyte> LoadAlignedVector256NonTemporal(sbyte* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0017362B File Offset: 0x0017282B
		public unsafe static Vector256<byte> LoadAlignedVector256NonTemporal(byte* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x00173633 File Offset: 0x00172833
		public unsafe static Vector256<short> LoadAlignedVector256NonTemporal(short* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x0017363B File Offset: 0x0017283B
		public unsafe static Vector256<ushort> LoadAlignedVector256NonTemporal(ushort* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x00173643 File Offset: 0x00172843
		public unsafe static Vector256<int> LoadAlignedVector256NonTemporal(int* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x0017364B File Offset: 0x0017284B
		public unsafe static Vector256<uint> LoadAlignedVector256NonTemporal(uint* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x00173653 File Offset: 0x00172853
		public unsafe static Vector256<long> LoadAlignedVector256NonTemporal(long* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0017365B File Offset: 0x0017285B
		public unsafe static Vector256<ulong> LoadAlignedVector256NonTemporal(ulong* address)
		{
			return Avx2.LoadAlignedVector256NonTemporal(address);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x00173663 File Offset: 0x00172863
		public unsafe static Vector128<int> MaskLoad(int* address, Vector128<int> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0017366C File Offset: 0x0017286C
		public unsafe static Vector128<uint> MaskLoad(uint* address, Vector128<uint> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x00173675 File Offset: 0x00172875
		public unsafe static Vector128<long> MaskLoad(long* address, Vector128<long> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0017367E File Offset: 0x0017287E
		public unsafe static Vector128<ulong> MaskLoad(ulong* address, Vector128<ulong> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x00173687 File Offset: 0x00172887
		public unsafe static Vector256<int> MaskLoad(int* address, Vector256<int> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00173690 File Offset: 0x00172890
		public unsafe static Vector256<uint> MaskLoad(uint* address, Vector256<uint> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x00173699 File Offset: 0x00172899
		public unsafe static Vector256<long> MaskLoad(long* address, Vector256<long> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x001736A2 File Offset: 0x001728A2
		public unsafe static Vector256<ulong> MaskLoad(ulong* address, Vector256<ulong> mask)
		{
			return Avx2.MaskLoad(address, mask);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001736AB File Offset: 0x001728AB
		public unsafe static void MaskStore(int* address, Vector128<int> mask, Vector128<int> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x001736B5 File Offset: 0x001728B5
		public unsafe static void MaskStore(uint* address, Vector128<uint> mask, Vector128<uint> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x001736BF File Offset: 0x001728BF
		public unsafe static void MaskStore(long* address, Vector128<long> mask, Vector128<long> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x001736C9 File Offset: 0x001728C9
		public unsafe static void MaskStore(ulong* address, Vector128<ulong> mask, Vector128<ulong> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x001736D3 File Offset: 0x001728D3
		public unsafe static void MaskStore(int* address, Vector256<int> mask, Vector256<int> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x001736DD File Offset: 0x001728DD
		public unsafe static void MaskStore(uint* address, Vector256<uint> mask, Vector256<uint> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x001736E7 File Offset: 0x001728E7
		public unsafe static void MaskStore(long* address, Vector256<long> mask, Vector256<long> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x001736F1 File Offset: 0x001728F1
		public unsafe static void MaskStore(ulong* address, Vector256<ulong> mask, Vector256<ulong> source)
		{
			Avx2.MaskStore(address, mask, source);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x001736FB File Offset: 0x001728FB
		public static Vector256<int> MultiplyAddAdjacent(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.MultiplyAddAdjacent(left, right);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00173704 File Offset: 0x00172904
		public static Vector256<short> MultiplyAddAdjacent(Vector256<byte> left, Vector256<sbyte> right)
		{
			return Avx2.MultiplyAddAdjacent(left, right);
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0017370D File Offset: 0x0017290D
		public static Vector256<sbyte> Max(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00173716 File Offset: 0x00172916
		public static Vector256<byte> Max(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x0017371F File Offset: 0x0017291F
		public static Vector256<short> Max(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x00173728 File Offset: 0x00172928
		public static Vector256<ushort> Max(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x00173731 File Offset: 0x00172931
		public static Vector256<int> Max(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0017373A File Offset: 0x0017293A
		public static Vector256<uint> Max(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Max(left, right);
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x00173743 File Offset: 0x00172943
		public static Vector256<sbyte> Min(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0017374C File Offset: 0x0017294C
		public static Vector256<byte> Min(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x00173755 File Offset: 0x00172955
		public static Vector256<short> Min(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0017375E File Offset: 0x0017295E
		public static Vector256<ushort> Min(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x00173767 File Offset: 0x00172967
		public static Vector256<int> Min(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x00173770 File Offset: 0x00172970
		public static Vector256<uint> Min(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Min(left, right);
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x00173779 File Offset: 0x00172979
		public static int MoveMask(Vector256<sbyte> value)
		{
			return Avx2.MoveMask(value);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00173781 File Offset: 0x00172981
		public static int MoveMask(Vector256<byte> value)
		{
			return Avx2.MoveMask(value);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x00173789 File Offset: 0x00172989
		public static Vector256<ushort> MultipleSumAbsoluteDifferences(Vector256<byte> left, Vector256<byte> right, byte mask)
		{
			return Avx2.MultipleSumAbsoluteDifferences(left, right, mask);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x00173793 File Offset: 0x00172993
		public static Vector256<long> Multiply(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Multiply(left, right);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0017379C File Offset: 0x0017299C
		public static Vector256<ulong> Multiply(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Multiply(left, right);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x001737A5 File Offset: 0x001729A5
		public static Vector256<short> MultiplyHigh(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.MultiplyHigh(left, right);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001737AE File Offset: 0x001729AE
		public static Vector256<ushort> MultiplyHigh(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.MultiplyHigh(left, right);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x001737B7 File Offset: 0x001729B7
		public static Vector256<short> MultiplyHighRoundScale(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.MultiplyHighRoundScale(left, right);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x001737C0 File Offset: 0x001729C0
		public static Vector256<short> MultiplyLow(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.MultiplyLow(left, right);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x001737C9 File Offset: 0x001729C9
		public static Vector256<ushort> MultiplyLow(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.MultiplyLow(left, right);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x001737D2 File Offset: 0x001729D2
		public static Vector256<int> MultiplyLow(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.MultiplyLow(left, right);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x001737DB File Offset: 0x001729DB
		public static Vector256<uint> MultiplyLow(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.MultiplyLow(left, right);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x001737E4 File Offset: 0x001729E4
		public static Vector256<sbyte> Or(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x001737ED File Offset: 0x001729ED
		public static Vector256<byte> Or(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x001737F6 File Offset: 0x001729F6
		public static Vector256<short> Or(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x001737FF File Offset: 0x001729FF
		public static Vector256<ushort> Or(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00173808 File Offset: 0x00172A08
		public static Vector256<int> Or(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00173811 File Offset: 0x00172A11
		public static Vector256<uint> Or(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0017381A File Offset: 0x00172A1A
		public static Vector256<long> Or(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00173823 File Offset: 0x00172A23
		public static Vector256<ulong> Or(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.Or(left, right);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0017382C File Offset: 0x00172A2C
		public static Vector256<sbyte> PackSignedSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.PackSignedSaturate(left, right);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00173835 File Offset: 0x00172A35
		public static Vector256<short> PackSignedSaturate(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.PackSignedSaturate(left, right);
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0017383E File Offset: 0x00172A3E
		public static Vector256<byte> PackUnsignedSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.PackUnsignedSaturate(left, right);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x00173847 File Offset: 0x00172A47
		public static Vector256<ushort> PackUnsignedSaturate(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.PackUnsignedSaturate(left, right);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x00173850 File Offset: 0x00172A50
		public new static Vector256<sbyte> Permute2x128(Vector256<sbyte> left, Vector256<sbyte> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0017385A File Offset: 0x00172A5A
		public new static Vector256<byte> Permute2x128(Vector256<byte> left, Vector256<byte> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x00173864 File Offset: 0x00172A64
		public new static Vector256<short> Permute2x128(Vector256<short> left, Vector256<short> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0017386E File Offset: 0x00172A6E
		public new static Vector256<ushort> Permute2x128(Vector256<ushort> left, Vector256<ushort> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00173878 File Offset: 0x00172A78
		public new static Vector256<int> Permute2x128(Vector256<int> left, Vector256<int> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00173882 File Offset: 0x00172A82
		public new static Vector256<uint> Permute2x128(Vector256<uint> left, Vector256<uint> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0017388C File Offset: 0x00172A8C
		public new static Vector256<long> Permute2x128(Vector256<long> left, Vector256<long> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00173896 File Offset: 0x00172A96
		public new static Vector256<ulong> Permute2x128(Vector256<ulong> left, Vector256<ulong> right, byte control)
		{
			return Avx2.Permute2x128(left, right, control);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x001738A0 File Offset: 0x00172AA0
		public static Vector256<long> Permute4x64(Vector256<long> value, byte control)
		{
			return Avx2.Permute4x64(value, control);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x001738A9 File Offset: 0x00172AA9
		public static Vector256<ulong> Permute4x64(Vector256<ulong> value, byte control)
		{
			return Avx2.Permute4x64(value, control);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x001738B2 File Offset: 0x00172AB2
		public static Vector256<double> Permute4x64(Vector256<double> value, byte control)
		{
			return Avx2.Permute4x64(value, control);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x001738BB File Offset: 0x00172ABB
		public static Vector256<int> PermuteVar8x32(Vector256<int> left, Vector256<int> control)
		{
			return Avx2.PermuteVar8x32(left, control);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x001738C4 File Offset: 0x00172AC4
		public static Vector256<uint> PermuteVar8x32(Vector256<uint> left, Vector256<uint> control)
		{
			return Avx2.PermuteVar8x32(left, control);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x001738CD File Offset: 0x00172ACD
		public static Vector256<float> PermuteVar8x32(Vector256<float> left, Vector256<int> control)
		{
			return Avx2.PermuteVar8x32(left, control);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x001738D6 File Offset: 0x00172AD6
		public static Vector256<short> ShiftLeftLogical(Vector256<short> value, Vector128<short> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x001738DF File Offset: 0x00172ADF
		public static Vector256<ushort> ShiftLeftLogical(Vector256<ushort> value, Vector128<ushort> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x001738E8 File Offset: 0x00172AE8
		public static Vector256<int> ShiftLeftLogical(Vector256<int> value, Vector128<int> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x001738F1 File Offset: 0x00172AF1
		public static Vector256<uint> ShiftLeftLogical(Vector256<uint> value, Vector128<uint> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x001738FA File Offset: 0x00172AFA
		public static Vector256<long> ShiftLeftLogical(Vector256<long> value, Vector128<long> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00173903 File Offset: 0x00172B03
		public static Vector256<ulong> ShiftLeftLogical(Vector256<ulong> value, Vector128<ulong> count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0017390C File Offset: 0x00172B0C
		public static Vector256<short> ShiftLeftLogical(Vector256<short> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x00173915 File Offset: 0x00172B15
		public static Vector256<ushort> ShiftLeftLogical(Vector256<ushort> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x0017391E File Offset: 0x00172B1E
		public static Vector256<int> ShiftLeftLogical(Vector256<int> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x00173927 File Offset: 0x00172B27
		public static Vector256<uint> ShiftLeftLogical(Vector256<uint> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x00173930 File Offset: 0x00172B30
		public static Vector256<long> ShiftLeftLogical(Vector256<long> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00173939 File Offset: 0x00172B39
		public static Vector256<ulong> ShiftLeftLogical(Vector256<ulong> value, byte count)
		{
			return Avx2.ShiftLeftLogical(value, count);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x00173942 File Offset: 0x00172B42
		public static Vector256<sbyte> ShiftLeftLogical128BitLane(Vector256<sbyte> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0017394B File Offset: 0x00172B4B
		public static Vector256<byte> ShiftLeftLogical128BitLane(Vector256<byte> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00173954 File Offset: 0x00172B54
		public static Vector256<short> ShiftLeftLogical128BitLane(Vector256<short> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0017395D File Offset: 0x00172B5D
		public static Vector256<ushort> ShiftLeftLogical128BitLane(Vector256<ushort> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x00173966 File Offset: 0x00172B66
		public static Vector256<int> ShiftLeftLogical128BitLane(Vector256<int> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x0017396F File Offset: 0x00172B6F
		public static Vector256<uint> ShiftLeftLogical128BitLane(Vector256<uint> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00173978 File Offset: 0x00172B78
		public static Vector256<long> ShiftLeftLogical128BitLane(Vector256<long> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00173981 File Offset: 0x00172B81
		public static Vector256<ulong> ShiftLeftLogical128BitLane(Vector256<ulong> value, byte numBytes)
		{
			return Avx2.ShiftLeftLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0017398A File Offset: 0x00172B8A
		public static Vector256<int> ShiftLeftLogicalVariable(Vector256<int> value, Vector256<uint> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x00173993 File Offset: 0x00172B93
		public static Vector256<uint> ShiftLeftLogicalVariable(Vector256<uint> value, Vector256<uint> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0017399C File Offset: 0x00172B9C
		public static Vector256<long> ShiftLeftLogicalVariable(Vector256<long> value, Vector256<ulong> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x001739A5 File Offset: 0x00172BA5
		public static Vector256<ulong> ShiftLeftLogicalVariable(Vector256<ulong> value, Vector256<ulong> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x001739AE File Offset: 0x00172BAE
		public static Vector128<int> ShiftLeftLogicalVariable(Vector128<int> value, Vector128<uint> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x001739B7 File Offset: 0x00172BB7
		public static Vector128<uint> ShiftLeftLogicalVariable(Vector128<uint> value, Vector128<uint> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x001739C0 File Offset: 0x00172BC0
		public static Vector128<long> ShiftLeftLogicalVariable(Vector128<long> value, Vector128<ulong> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x001739C9 File Offset: 0x00172BC9
		public static Vector128<ulong> ShiftLeftLogicalVariable(Vector128<ulong> value, Vector128<ulong> count)
		{
			return Avx2.ShiftLeftLogicalVariable(value, count);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x001739D2 File Offset: 0x00172BD2
		public static Vector256<short> ShiftRightArithmetic(Vector256<short> value, Vector128<short> count)
		{
			return Avx2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x001739DB File Offset: 0x00172BDB
		public static Vector256<int> ShiftRightArithmetic(Vector256<int> value, Vector128<int> count)
		{
			return Avx2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x001739E4 File Offset: 0x00172BE4
		public static Vector256<short> ShiftRightArithmetic(Vector256<short> value, byte count)
		{
			return Avx2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x001739ED File Offset: 0x00172BED
		public static Vector256<int> ShiftRightArithmetic(Vector256<int> value, byte count)
		{
			return Avx2.ShiftRightArithmetic(value, count);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x001739F6 File Offset: 0x00172BF6
		public static Vector256<int> ShiftRightArithmeticVariable(Vector256<int> value, Vector256<uint> count)
		{
			return Avx2.ShiftRightArithmeticVariable(value, count);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x001739FF File Offset: 0x00172BFF
		public static Vector128<int> ShiftRightArithmeticVariable(Vector128<int> value, Vector128<uint> count)
		{
			return Avx2.ShiftRightArithmeticVariable(value, count);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x00173A08 File Offset: 0x00172C08
		public static Vector256<short> ShiftRightLogical(Vector256<short> value, Vector128<short> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x00173A11 File Offset: 0x00172C11
		public static Vector256<ushort> ShiftRightLogical(Vector256<ushort> value, Vector128<ushort> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00173A1A File Offset: 0x00172C1A
		public static Vector256<int> ShiftRightLogical(Vector256<int> value, Vector128<int> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x00173A23 File Offset: 0x00172C23
		public static Vector256<uint> ShiftRightLogical(Vector256<uint> value, Vector128<uint> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x00173A2C File Offset: 0x00172C2C
		public static Vector256<long> ShiftRightLogical(Vector256<long> value, Vector128<long> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x00173A35 File Offset: 0x00172C35
		public static Vector256<ulong> ShiftRightLogical(Vector256<ulong> value, Vector128<ulong> count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x00173A3E File Offset: 0x00172C3E
		public static Vector256<short> ShiftRightLogical(Vector256<short> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x00173A47 File Offset: 0x00172C47
		public static Vector256<ushort> ShiftRightLogical(Vector256<ushort> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x00173A50 File Offset: 0x00172C50
		public static Vector256<int> ShiftRightLogical(Vector256<int> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x00173A59 File Offset: 0x00172C59
		public static Vector256<uint> ShiftRightLogical(Vector256<uint> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x00173A62 File Offset: 0x00172C62
		public static Vector256<long> ShiftRightLogical(Vector256<long> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x00173A6B File Offset: 0x00172C6B
		public static Vector256<ulong> ShiftRightLogical(Vector256<ulong> value, byte count)
		{
			return Avx2.ShiftRightLogical(value, count);
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x00173A74 File Offset: 0x00172C74
		public static Vector256<sbyte> ShiftRightLogical128BitLane(Vector256<sbyte> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x00173A7D File Offset: 0x00172C7D
		public static Vector256<byte> ShiftRightLogical128BitLane(Vector256<byte> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x00173A86 File Offset: 0x00172C86
		public static Vector256<short> ShiftRightLogical128BitLane(Vector256<short> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x00173A8F File Offset: 0x00172C8F
		public static Vector256<ushort> ShiftRightLogical128BitLane(Vector256<ushort> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x00173A98 File Offset: 0x00172C98
		public static Vector256<int> ShiftRightLogical128BitLane(Vector256<int> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x00173AA1 File Offset: 0x00172CA1
		public static Vector256<uint> ShiftRightLogical128BitLane(Vector256<uint> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x00173AAA File Offset: 0x00172CAA
		public static Vector256<long> ShiftRightLogical128BitLane(Vector256<long> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x00173AB3 File Offset: 0x00172CB3
		public static Vector256<ulong> ShiftRightLogical128BitLane(Vector256<ulong> value, byte numBytes)
		{
			return Avx2.ShiftRightLogical128BitLane(value, numBytes);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x00173ABC File Offset: 0x00172CBC
		public static Vector256<int> ShiftRightLogicalVariable(Vector256<int> value, Vector256<uint> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x00173AC5 File Offset: 0x00172CC5
		public static Vector256<uint> ShiftRightLogicalVariable(Vector256<uint> value, Vector256<uint> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x00173ACE File Offset: 0x00172CCE
		public static Vector256<long> ShiftRightLogicalVariable(Vector256<long> value, Vector256<ulong> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x00173AD7 File Offset: 0x00172CD7
		public static Vector256<ulong> ShiftRightLogicalVariable(Vector256<ulong> value, Vector256<ulong> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x00173AE0 File Offset: 0x00172CE0
		public static Vector128<int> ShiftRightLogicalVariable(Vector128<int> value, Vector128<uint> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x00173AE9 File Offset: 0x00172CE9
		public static Vector128<uint> ShiftRightLogicalVariable(Vector128<uint> value, Vector128<uint> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x00173AF2 File Offset: 0x00172CF2
		public static Vector128<long> ShiftRightLogicalVariable(Vector128<long> value, Vector128<ulong> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00173AFB File Offset: 0x00172CFB
		public static Vector128<ulong> ShiftRightLogicalVariable(Vector128<ulong> value, Vector128<ulong> count)
		{
			return Avx2.ShiftRightLogicalVariable(value, count);
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x00173B04 File Offset: 0x00172D04
		public static Vector256<sbyte> Shuffle(Vector256<sbyte> value, Vector256<sbyte> mask)
		{
			return Avx2.Shuffle(value, mask);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x00173B0D File Offset: 0x00172D0D
		public static Vector256<byte> Shuffle(Vector256<byte> value, Vector256<byte> mask)
		{
			return Avx2.Shuffle(value, mask);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x00173B16 File Offset: 0x00172D16
		public static Vector256<int> Shuffle(Vector256<int> value, byte control)
		{
			return Avx2.Shuffle(value, control);
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x00173B1F File Offset: 0x00172D1F
		public static Vector256<uint> Shuffle(Vector256<uint> value, byte control)
		{
			return Avx2.Shuffle(value, control);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x00173B28 File Offset: 0x00172D28
		public static Vector256<short> ShuffleHigh(Vector256<short> value, byte control)
		{
			return Avx2.ShuffleHigh(value, control);
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00173B31 File Offset: 0x00172D31
		public static Vector256<ushort> ShuffleHigh(Vector256<ushort> value, byte control)
		{
			return Avx2.ShuffleHigh(value, control);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00173B3A File Offset: 0x00172D3A
		public static Vector256<short> ShuffleLow(Vector256<short> value, byte control)
		{
			return Avx2.ShuffleLow(value, control);
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x00173B43 File Offset: 0x00172D43
		public static Vector256<ushort> ShuffleLow(Vector256<ushort> value, byte control)
		{
			return Avx2.ShuffleLow(value, control);
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x00173B4C File Offset: 0x00172D4C
		public static Vector256<sbyte> Sign(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Sign(left, right);
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x00173B55 File Offset: 0x00172D55
		public static Vector256<short> Sign(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Sign(left, right);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00173B5E File Offset: 0x00172D5E
		public static Vector256<int> Sign(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Sign(left, right);
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x00173B67 File Offset: 0x00172D67
		public static Vector256<sbyte> Subtract(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x00173B70 File Offset: 0x00172D70
		public static Vector256<byte> Subtract(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x00173B79 File Offset: 0x00172D79
		public static Vector256<short> Subtract(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x00173B82 File Offset: 0x00172D82
		public static Vector256<ushort> Subtract(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x00173B8B File Offset: 0x00172D8B
		public static Vector256<int> Subtract(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x00173B94 File Offset: 0x00172D94
		public static Vector256<uint> Subtract(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x00173B9D File Offset: 0x00172D9D
		public static Vector256<long> Subtract(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x00173BA6 File Offset: 0x00172DA6
		public static Vector256<ulong> Subtract(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.Subtract(left, right);
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x00173BAF File Offset: 0x00172DAF
		public static Vector256<sbyte> SubtractSaturate(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.SubtractSaturate(left, right);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x00173BB8 File Offset: 0x00172DB8
		public static Vector256<short> SubtractSaturate(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.SubtractSaturate(left, right);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x00173BC1 File Offset: 0x00172DC1
		public static Vector256<byte> SubtractSaturate(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.SubtractSaturate(left, right);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x00173BCA File Offset: 0x00172DCA
		public static Vector256<ushort> SubtractSaturate(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.SubtractSaturate(left, right);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x00173BD3 File Offset: 0x00172DD3
		public static Vector256<ushort> SumAbsoluteDifferences(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.SumAbsoluteDifferences(left, right);
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x00173BDC File Offset: 0x00172DDC
		public static Vector256<sbyte> UnpackHigh(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x00173BE5 File Offset: 0x00172DE5
		public static Vector256<byte> UnpackHigh(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x00173BEE File Offset: 0x00172DEE
		public static Vector256<short> UnpackHigh(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x00173BF7 File Offset: 0x00172DF7
		public static Vector256<ushort> UnpackHigh(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x00173C00 File Offset: 0x00172E00
		public static Vector256<int> UnpackHigh(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x00173C09 File Offset: 0x00172E09
		public static Vector256<uint> UnpackHigh(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x00173C12 File Offset: 0x00172E12
		public static Vector256<long> UnpackHigh(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x00173C1B File Offset: 0x00172E1B
		public static Vector256<ulong> UnpackHigh(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.UnpackHigh(left, right);
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x00173C24 File Offset: 0x00172E24
		public static Vector256<sbyte> UnpackLow(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00173C2D File Offset: 0x00172E2D
		public static Vector256<byte> UnpackLow(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00173C36 File Offset: 0x00172E36
		public static Vector256<short> UnpackLow(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x00173C3F File Offset: 0x00172E3F
		public static Vector256<ushort> UnpackLow(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x00173C48 File Offset: 0x00172E48
		public static Vector256<int> UnpackLow(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x00173C51 File Offset: 0x00172E51
		public static Vector256<uint> UnpackLow(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x00173C5A File Offset: 0x00172E5A
		public static Vector256<long> UnpackLow(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x00173C63 File Offset: 0x00172E63
		public static Vector256<ulong> UnpackLow(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.UnpackLow(left, right);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x00173C6C File Offset: 0x00172E6C
		public static Vector256<sbyte> Xor(Vector256<sbyte> left, Vector256<sbyte> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x00173C75 File Offset: 0x00172E75
		public static Vector256<byte> Xor(Vector256<byte> left, Vector256<byte> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x00173C7E File Offset: 0x00172E7E
		public static Vector256<short> Xor(Vector256<short> left, Vector256<short> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x00173C87 File Offset: 0x00172E87
		public static Vector256<ushort> Xor(Vector256<ushort> left, Vector256<ushort> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x00173C90 File Offset: 0x00172E90
		public static Vector256<int> Xor(Vector256<int> left, Vector256<int> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x00173C99 File Offset: 0x00172E99
		public static Vector256<uint> Xor(Vector256<uint> left, Vector256<uint> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x00173CA2 File Offset: 0x00172EA2
		public static Vector256<long> Xor(Vector256<long> left, Vector256<long> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x00173CAB File Offset: 0x00172EAB
		public static Vector256<ulong> Xor(Vector256<ulong> left, Vector256<ulong> right)
		{
			return Avx2.Xor(left, right);
		}

		// Token: 0x02000432 RID: 1074
		[Intrinsic]
		public new abstract class X64 : Avx.X64
		{
			// Token: 0x17000A26 RID: 2598
			// (get) Token: 0x06004022 RID: 16418 RVA: 0x00173CB4 File Offset: 0x00172EB4
			public new static bool IsSupported
			{
				get
				{
					return Avx2.X64.IsSupported;
				}
			}
		}
	}
}
