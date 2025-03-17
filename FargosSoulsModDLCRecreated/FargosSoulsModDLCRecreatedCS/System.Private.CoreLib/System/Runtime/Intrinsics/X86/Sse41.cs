using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000445 RID: 1093
	[CLSCompliant(false)]
	[Intrinsic]
	public abstract class Sse41 : Ssse3
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x00174DD9 File Offset: 0x00173FD9
		public new static bool IsSupported
		{
			get
			{
				return Sse41.IsSupported;
			}
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00174DE0 File Offset: 0x00173FE0
		public static Vector128<short> Blend(Vector128<short> left, Vector128<short> right, byte control)
		{
			return Sse41.Blend(left, right, control);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x00174DEA File Offset: 0x00173FEA
		public static Vector128<ushort> Blend(Vector128<ushort> left, Vector128<ushort> right, byte control)
		{
			return Sse41.Blend(left, right, control);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00174DF4 File Offset: 0x00173FF4
		public static Vector128<float> Blend(Vector128<float> left, Vector128<float> right, byte control)
		{
			return Sse41.Blend(left, right, control);
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x00174DFE File Offset: 0x00173FFE
		public static Vector128<double> Blend(Vector128<double> left, Vector128<double> right, byte control)
		{
			return Sse41.Blend(left, right, control);
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00174E08 File Offset: 0x00174008
		public static Vector128<sbyte> BlendVariable(Vector128<sbyte> left, Vector128<sbyte> right, Vector128<sbyte> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00174E12 File Offset: 0x00174012
		public static Vector128<byte> BlendVariable(Vector128<byte> left, Vector128<byte> right, Vector128<byte> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x00174E1C File Offset: 0x0017401C
		public static Vector128<short> BlendVariable(Vector128<short> left, Vector128<short> right, Vector128<short> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x00174E26 File Offset: 0x00174026
		public static Vector128<ushort> BlendVariable(Vector128<ushort> left, Vector128<ushort> right, Vector128<ushort> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x00174E30 File Offset: 0x00174030
		public static Vector128<int> BlendVariable(Vector128<int> left, Vector128<int> right, Vector128<int> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x00174E3A File Offset: 0x0017403A
		public static Vector128<uint> BlendVariable(Vector128<uint> left, Vector128<uint> right, Vector128<uint> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00174E44 File Offset: 0x00174044
		public static Vector128<long> BlendVariable(Vector128<long> left, Vector128<long> right, Vector128<long> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x00174E4E File Offset: 0x0017404E
		public static Vector128<ulong> BlendVariable(Vector128<ulong> left, Vector128<ulong> right, Vector128<ulong> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x00174E58 File Offset: 0x00174058
		public static Vector128<float> BlendVariable(Vector128<float> left, Vector128<float> right, Vector128<float> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x00174E62 File Offset: 0x00174062
		public static Vector128<double> BlendVariable(Vector128<double> left, Vector128<double> right, Vector128<double> mask)
		{
			return Sse41.BlendVariable(left, right, mask);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x00174E6C File Offset: 0x0017406C
		public static Vector128<float> Ceiling(Vector128<float> value)
		{
			return Sse41.Ceiling(value);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x00174E74 File Offset: 0x00174074
		public static Vector128<double> Ceiling(Vector128<double> value)
		{
			return Sse41.Ceiling(value);
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00174E7C File Offset: 0x0017407C
		public static Vector128<double> CeilingScalar(Vector128<double> value)
		{
			return Sse41.CeilingScalar(value);
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x00174E84 File Offset: 0x00174084
		public static Vector128<float> CeilingScalar(Vector128<float> value)
		{
			return Sse41.CeilingScalar(value);
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00174E8C File Offset: 0x0017408C
		public static Vector128<double> CeilingScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.CeilingScalar(upper, value);
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x00174E95 File Offset: 0x00174095
		public static Vector128<float> CeilingScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.CeilingScalar(upper, value);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x00174E9E File Offset: 0x0017409E
		public static Vector128<long> CompareEqual(Vector128<long> left, Vector128<long> right)
		{
			return Sse41.CompareEqual(left, right);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00174EA7 File Offset: 0x001740A7
		public static Vector128<ulong> CompareEqual(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse41.CompareEqual(left, right);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x00174EB0 File Offset: 0x001740B0
		public static Vector128<short> ConvertToVector128Int16(Vector128<sbyte> value)
		{
			return Sse41.ConvertToVector128Int16(value);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x00174EB8 File Offset: 0x001740B8
		public static Vector128<short> ConvertToVector128Int16(Vector128<byte> value)
		{
			return Sse41.ConvertToVector128Int16(value);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x00174EC0 File Offset: 0x001740C0
		public static Vector128<int> ConvertToVector128Int32(Vector128<sbyte> value)
		{
			return Sse41.ConvertToVector128Int32(value);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x00174EC8 File Offset: 0x001740C8
		public static Vector128<int> ConvertToVector128Int32(Vector128<byte> value)
		{
			return Sse41.ConvertToVector128Int32(value);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x00174ED0 File Offset: 0x001740D0
		public static Vector128<int> ConvertToVector128Int32(Vector128<short> value)
		{
			return Sse41.ConvertToVector128Int32(value);
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x00174ED8 File Offset: 0x001740D8
		public static Vector128<int> ConvertToVector128Int32(Vector128<ushort> value)
		{
			return Sse41.ConvertToVector128Int32(value);
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x00174EE0 File Offset: 0x001740E0
		public static Vector128<long> ConvertToVector128Int64(Vector128<sbyte> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x00174EE8 File Offset: 0x001740E8
		public static Vector128<long> ConvertToVector128Int64(Vector128<byte> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x00174EF0 File Offset: 0x001740F0
		public static Vector128<long> ConvertToVector128Int64(Vector128<short> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x00174EF8 File Offset: 0x001740F8
		public static Vector128<long> ConvertToVector128Int64(Vector128<ushort> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x00174F00 File Offset: 0x00174100
		public static Vector128<long> ConvertToVector128Int64(Vector128<int> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x00174F08 File Offset: 0x00174108
		public static Vector128<long> ConvertToVector128Int64(Vector128<uint> value)
		{
			return Sse41.ConvertToVector128Int64(value);
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x00174F10 File Offset: 0x00174110
		public unsafe static Vector128<short> ConvertToVector128Int16(sbyte* address)
		{
			return Sse41.ConvertToVector128Int16(address);
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x00174F18 File Offset: 0x00174118
		public unsafe static Vector128<short> ConvertToVector128Int16(byte* address)
		{
			return Sse41.ConvertToVector128Int16(address);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00174F20 File Offset: 0x00174120
		public unsafe static Vector128<int> ConvertToVector128Int32(sbyte* address)
		{
			return Sse41.ConvertToVector128Int32(address);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x00174F28 File Offset: 0x00174128
		public unsafe static Vector128<int> ConvertToVector128Int32(byte* address)
		{
			return Sse41.ConvertToVector128Int32(address);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x00174F30 File Offset: 0x00174130
		public unsafe static Vector128<int> ConvertToVector128Int32(short* address)
		{
			return Sse41.ConvertToVector128Int32(address);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00174F38 File Offset: 0x00174138
		public unsafe static Vector128<int> ConvertToVector128Int32(ushort* address)
		{
			return Sse41.ConvertToVector128Int32(address);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00174F40 File Offset: 0x00174140
		public unsafe static Vector128<long> ConvertToVector128Int64(sbyte* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x00174F48 File Offset: 0x00174148
		public unsafe static Vector128<long> ConvertToVector128Int64(byte* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x00174F50 File Offset: 0x00174150
		public unsafe static Vector128<long> ConvertToVector128Int64(short* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00174F58 File Offset: 0x00174158
		public unsafe static Vector128<long> ConvertToVector128Int64(ushort* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x00174F60 File Offset: 0x00174160
		public unsafe static Vector128<long> ConvertToVector128Int64(int* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00174F68 File Offset: 0x00174168
		public unsafe static Vector128<long> ConvertToVector128Int64(uint* address)
		{
			return Sse41.ConvertToVector128Int64(address);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00174F70 File Offset: 0x00174170
		public static Vector128<float> DotProduct(Vector128<float> left, Vector128<float> right, byte control)
		{
			return Sse41.DotProduct(left, right, control);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00174F7A File Offset: 0x0017417A
		public static Vector128<double> DotProduct(Vector128<double> left, Vector128<double> right, byte control)
		{
			return Sse41.DotProduct(left, right, control);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00174F84 File Offset: 0x00174184
		public static byte Extract(Vector128<byte> value, byte index)
		{
			return Sse41.Extract(value, index);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00174F8D File Offset: 0x0017418D
		public static int Extract(Vector128<int> value, byte index)
		{
			return Sse41.Extract(value, index);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x00174F96 File Offset: 0x00174196
		public static uint Extract(Vector128<uint> value, byte index)
		{
			return Sse41.Extract(value, index);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00174F9F File Offset: 0x0017419F
		public static float Extract(Vector128<float> value, byte index)
		{
			return Sse41.Extract(value, index);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00174FA8 File Offset: 0x001741A8
		public static Vector128<float> Floor(Vector128<float> value)
		{
			return Sse41.Floor(value);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00174FB0 File Offset: 0x001741B0
		public static Vector128<double> Floor(Vector128<double> value)
		{
			return Sse41.Floor(value);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00174FB8 File Offset: 0x001741B8
		public static Vector128<double> FloorScalar(Vector128<double> value)
		{
			return Sse41.FloorScalar(value);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x00174FC0 File Offset: 0x001741C0
		public static Vector128<float> FloorScalar(Vector128<float> value)
		{
			return Sse41.FloorScalar(value);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x00174FC8 File Offset: 0x001741C8
		public static Vector128<double> FloorScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.FloorScalar(upper, value);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x00174FD1 File Offset: 0x001741D1
		public static Vector128<float> FloorScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.FloorScalar(upper, value);
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x00174FDA File Offset: 0x001741DA
		public static Vector128<sbyte> Insert(Vector128<sbyte> value, sbyte data, byte index)
		{
			return Sse41.Insert(value, data, index);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00174FE4 File Offset: 0x001741E4
		public static Vector128<byte> Insert(Vector128<byte> value, byte data, byte index)
		{
			return Sse41.Insert(value, data, index);
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x00174FEE File Offset: 0x001741EE
		public static Vector128<int> Insert(Vector128<int> value, int data, byte index)
		{
			return Sse41.Insert(value, data, index);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x00174FF8 File Offset: 0x001741F8
		public static Vector128<uint> Insert(Vector128<uint> value, uint data, byte index)
		{
			return Sse41.Insert(value, data, index);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x00175002 File Offset: 0x00174202
		public static Vector128<float> Insert(Vector128<float> value, Vector128<float> data, byte index)
		{
			return Sse41.Insert(value, data, index);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0017500C File Offset: 0x0017420C
		public static Vector128<sbyte> Max(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse41.Max(left, right);
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x00175015 File Offset: 0x00174215
		public static Vector128<ushort> Max(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse41.Max(left, right);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x0017501E File Offset: 0x0017421E
		public static Vector128<int> Max(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.Max(left, right);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x00175027 File Offset: 0x00174227
		public static Vector128<uint> Max(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.Max(left, right);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00175030 File Offset: 0x00174230
		public static Vector128<sbyte> Min(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse41.Min(left, right);
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x00175039 File Offset: 0x00174239
		public static Vector128<ushort> Min(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse41.Min(left, right);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x00175042 File Offset: 0x00174242
		public static Vector128<int> Min(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.Min(left, right);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0017504B File Offset: 0x0017424B
		public static Vector128<uint> Min(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.Min(left, right);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00175054 File Offset: 0x00174254
		public static Vector128<ushort> MinHorizontal(Vector128<ushort> value)
		{
			return Sse41.MinHorizontal(value);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0017505C File Offset: 0x0017425C
		public static Vector128<ushort> MultipleSumAbsoluteDifferences(Vector128<byte> left, Vector128<byte> right, byte mask)
		{
			return Sse41.MultipleSumAbsoluteDifferences(left, right, mask);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x00175066 File Offset: 0x00174266
		public static Vector128<long> Multiply(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.Multiply(left, right);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0017506F File Offset: 0x0017426F
		public static Vector128<int> MultiplyLow(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.MultiplyLow(left, right);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00175078 File Offset: 0x00174278
		public static Vector128<uint> MultiplyLow(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.MultiplyLow(left, right);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x00175081 File Offset: 0x00174281
		public static Vector128<ushort> PackUnsignedSaturate(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.PackUnsignedSaturate(left, right);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0017508A File Offset: 0x0017428A
		public static Vector128<float> RoundToNearestInteger(Vector128<float> value)
		{
			return Sse41.RoundToNearestInteger(value);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x00175092 File Offset: 0x00174292
		public static Vector128<float> RoundToNegativeInfinity(Vector128<float> value)
		{
			return Sse41.RoundToNegativeInfinity(value);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0017509A File Offset: 0x0017429A
		public static Vector128<float> RoundToPositiveInfinity(Vector128<float> value)
		{
			return Sse41.RoundToPositiveInfinity(value);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x001750A2 File Offset: 0x001742A2
		public static Vector128<float> RoundToZero(Vector128<float> value)
		{
			return Sse41.RoundToZero(value);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x001750AA File Offset: 0x001742AA
		public static Vector128<float> RoundCurrentDirection(Vector128<float> value)
		{
			return Sse41.RoundCurrentDirection(value);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x001750B2 File Offset: 0x001742B2
		public static Vector128<double> RoundToNearestInteger(Vector128<double> value)
		{
			return Sse41.RoundToNearestInteger(value);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x001750BA File Offset: 0x001742BA
		public static Vector128<double> RoundToNegativeInfinity(Vector128<double> value)
		{
			return Sse41.RoundToNegativeInfinity(value);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x001750C2 File Offset: 0x001742C2
		public static Vector128<double> RoundToPositiveInfinity(Vector128<double> value)
		{
			return Sse41.RoundToPositiveInfinity(value);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x001750CA File Offset: 0x001742CA
		public static Vector128<double> RoundToZero(Vector128<double> value)
		{
			return Sse41.RoundToZero(value);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x001750D2 File Offset: 0x001742D2
		public static Vector128<double> RoundCurrentDirection(Vector128<double> value)
		{
			return Sse41.RoundCurrentDirection(value);
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x001750DA File Offset: 0x001742DA
		public static Vector128<double> RoundCurrentDirectionScalar(Vector128<double> value)
		{
			return Sse41.RoundCurrentDirectionScalar(value);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x001750E2 File Offset: 0x001742E2
		public static Vector128<double> RoundToNearestIntegerScalar(Vector128<double> value)
		{
			return Sse41.RoundToNearestIntegerScalar(value);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x001750EA File Offset: 0x001742EA
		public static Vector128<double> RoundToNegativeInfinityScalar(Vector128<double> value)
		{
			return Sse41.RoundToNegativeInfinityScalar(value);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x001750F2 File Offset: 0x001742F2
		public static Vector128<double> RoundToPositiveInfinityScalar(Vector128<double> value)
		{
			return Sse41.RoundToPositiveInfinityScalar(value);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x001750FA File Offset: 0x001742FA
		public static Vector128<double> RoundToZeroScalar(Vector128<double> value)
		{
			return Sse41.RoundToZeroScalar(value);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x00175102 File Offset: 0x00174302
		public static Vector128<double> RoundCurrentDirectionScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.RoundCurrentDirectionScalar(upper, value);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0017510B File Offset: 0x0017430B
		public static Vector128<double> RoundToNearestIntegerScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.RoundToNearestIntegerScalar(upper, value);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x00175114 File Offset: 0x00174314
		public static Vector128<double> RoundToNegativeInfinityScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.RoundToNegativeInfinityScalar(upper, value);
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0017511D File Offset: 0x0017431D
		public static Vector128<double> RoundToPositiveInfinityScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.RoundToPositiveInfinityScalar(upper, value);
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x00175126 File Offset: 0x00174326
		public static Vector128<double> RoundToZeroScalar(Vector128<double> upper, Vector128<double> value)
		{
			return Sse41.RoundToZeroScalar(upper, value);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0017512F File Offset: 0x0017432F
		public static Vector128<float> RoundCurrentDirectionScalar(Vector128<float> value)
		{
			return Sse41.RoundCurrentDirectionScalar(value);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x00175137 File Offset: 0x00174337
		public static Vector128<float> RoundToNearestIntegerScalar(Vector128<float> value)
		{
			return Sse41.RoundToNearestIntegerScalar(value);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0017513F File Offset: 0x0017433F
		public static Vector128<float> RoundToNegativeInfinityScalar(Vector128<float> value)
		{
			return Sse41.RoundToNegativeInfinityScalar(value);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x00175147 File Offset: 0x00174347
		public static Vector128<float> RoundToPositiveInfinityScalar(Vector128<float> value)
		{
			return Sse41.RoundToPositiveInfinityScalar(value);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0017514F File Offset: 0x0017434F
		public static Vector128<float> RoundToZeroScalar(Vector128<float> value)
		{
			return Sse41.RoundToZeroScalar(value);
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x00175157 File Offset: 0x00174357
		public static Vector128<float> RoundCurrentDirectionScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.RoundCurrentDirectionScalar(upper, value);
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x00175160 File Offset: 0x00174360
		public static Vector128<float> RoundToNearestIntegerScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.RoundToNearestIntegerScalar(upper, value);
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x00175169 File Offset: 0x00174369
		public static Vector128<float> RoundToNegativeInfinityScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.RoundToNegativeInfinityScalar(upper, value);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x00175172 File Offset: 0x00174372
		public static Vector128<float> RoundToPositiveInfinityScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.RoundToPositiveInfinityScalar(upper, value);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x0017517B File Offset: 0x0017437B
		public static Vector128<float> RoundToZeroScalar(Vector128<float> upper, Vector128<float> value)
		{
			return Sse41.RoundToZeroScalar(upper, value);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x00175184 File Offset: 0x00174384
		public unsafe static Vector128<sbyte> LoadAlignedVector128NonTemporal(sbyte* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x0017518C File Offset: 0x0017438C
		public unsafe static Vector128<byte> LoadAlignedVector128NonTemporal(byte* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x00175194 File Offset: 0x00174394
		public unsafe static Vector128<short> LoadAlignedVector128NonTemporal(short* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x0017519C File Offset: 0x0017439C
		public unsafe static Vector128<ushort> LoadAlignedVector128NonTemporal(ushort* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x001751A4 File Offset: 0x001743A4
		public unsafe static Vector128<int> LoadAlignedVector128NonTemporal(int* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x001751AC File Offset: 0x001743AC
		public unsafe static Vector128<uint> LoadAlignedVector128NonTemporal(uint* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x001751B4 File Offset: 0x001743B4
		public unsafe static Vector128<long> LoadAlignedVector128NonTemporal(long* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x001751BC File Offset: 0x001743BC
		public unsafe static Vector128<ulong> LoadAlignedVector128NonTemporal(ulong* address)
		{
			return Sse41.LoadAlignedVector128NonTemporal(address);
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x001751C4 File Offset: 0x001743C4
		public static bool TestC(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x001751CD File Offset: 0x001743CD
		public static bool TestC(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x001751D6 File Offset: 0x001743D6
		public static bool TestC(Vector128<short> left, Vector128<short> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x001751DF File Offset: 0x001743DF
		public static bool TestC(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x001751E8 File Offset: 0x001743E8
		public static bool TestC(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x001751F1 File Offset: 0x001743F1
		public static bool TestC(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x001751FA File Offset: 0x001743FA
		public static bool TestC(Vector128<long> left, Vector128<long> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x00175203 File Offset: 0x00174403
		public static bool TestC(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse41.TestC(left, right);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x0017520C File Offset: 0x0017440C
		public static bool TestNotZAndNotC(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x00175215 File Offset: 0x00174415
		public static bool TestNotZAndNotC(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x0017521E File Offset: 0x0017441E
		public static bool TestNotZAndNotC(Vector128<short> left, Vector128<short> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x00175227 File Offset: 0x00174427
		public static bool TestNotZAndNotC(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x00175230 File Offset: 0x00174430
		public static bool TestNotZAndNotC(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x00175239 File Offset: 0x00174439
		public static bool TestNotZAndNotC(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x00175242 File Offset: 0x00174442
		public static bool TestNotZAndNotC(Vector128<long> left, Vector128<long> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x0017524B File Offset: 0x0017444B
		public static bool TestNotZAndNotC(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse41.TestNotZAndNotC(left, right);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00175254 File Offset: 0x00174454
		public static bool TestZ(Vector128<sbyte> left, Vector128<sbyte> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x0017525D File Offset: 0x0017445D
		public static bool TestZ(Vector128<byte> left, Vector128<byte> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x00175266 File Offset: 0x00174466
		public static bool TestZ(Vector128<short> left, Vector128<short> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x0017526F File Offset: 0x0017446F
		public static bool TestZ(Vector128<ushort> left, Vector128<ushort> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00175278 File Offset: 0x00174478
		public static bool TestZ(Vector128<int> left, Vector128<int> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00175281 File Offset: 0x00174481
		public static bool TestZ(Vector128<uint> left, Vector128<uint> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x0017528A File Offset: 0x0017448A
		public static bool TestZ(Vector128<long> left, Vector128<long> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x00175293 File Offset: 0x00174493
		public static bool TestZ(Vector128<ulong> left, Vector128<ulong> right)
		{
			return Sse41.TestZ(left, right);
		}

		// Token: 0x02000446 RID: 1094
		[Intrinsic]
		public new abstract class X64 : Ssse3.X64
		{
			// Token: 0x17000A3A RID: 2618
			// (get) Token: 0x0600429F RID: 17055 RVA: 0x0017529C File Offset: 0x0017449C
			public new static bool IsSupported
			{
				get
				{
					return Sse41.X64.IsSupported;
				}
			}

			// Token: 0x060042A0 RID: 17056 RVA: 0x001752A3 File Offset: 0x001744A3
			public static long Extract(Vector128<long> value, byte index)
			{
				return Sse41.X64.Extract(value, index);
			}

			// Token: 0x060042A1 RID: 17057 RVA: 0x001752AC File Offset: 0x001744AC
			public static ulong Extract(Vector128<ulong> value, byte index)
			{
				return Sse41.X64.Extract(value, index);
			}

			// Token: 0x060042A2 RID: 17058 RVA: 0x001752B5 File Offset: 0x001744B5
			public static Vector128<long> Insert(Vector128<long> value, long data, byte index)
			{
				return Sse41.X64.Insert(value, data, index);
			}

			// Token: 0x060042A3 RID: 17059 RVA: 0x001752BF File Offset: 0x001744BF
			public static Vector128<ulong> Insert(Vector128<ulong> value, ulong data, byte index)
			{
				return Sse41.X64.Insert(value, data, index);
			}
		}
	}
}
