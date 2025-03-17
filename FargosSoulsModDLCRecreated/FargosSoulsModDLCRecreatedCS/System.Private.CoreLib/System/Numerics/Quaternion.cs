using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001CE RID: 462
	[Intrinsic]
	public struct Quaternion : IEquatable<Quaternion>
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x001080BA File Offset: 0x001072BA
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x001080D5 File Offset: 0x001072D5
		public readonly bool IsIdentity
		{
			get
			{
				return this.X == 0f && this.Y == 0f && this.Z == 0f && this.W == 1f;
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0010810D File Offset: 0x0010730D
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0010812C File Offset: 0x0010732C
		public Quaternion(Vector3 vectorPart, float scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0010815C File Offset: 0x0010735C
		public readonly float Length()
		{
			float x = this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
			return MathF.Sqrt(x);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x001081A7 File Offset: 0x001073A7
		public readonly float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x001081E0 File Offset: 0x001073E0
		public static Quaternion Normalize(Quaternion value)
		{
			float x = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num = 1f / MathF.Sqrt(x);
			Quaternion result;
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
			result.W = value.W * num;
			return result;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00108270 File Offset: 0x00107470
		public static Quaternion Conjugate(Quaternion value)
		{
			Quaternion result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
			return result;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x001082B8 File Offset: 0x001074B8
		public static Quaternion Inverse(Quaternion value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num2 = 1f / num;
			Quaternion result;
			result.X = -value.X * num2;
			result.Y = -value.Y * num2;
			result.Z = -value.Z * num2;
			result.W = value.W * num2;
			return result;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00108348 File Offset: 0x00107548
		public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = angle * 0.5f;
			float num = MathF.Sin(x);
			float w = MathF.Cos(x);
			Quaternion result;
			result.X = axis.X * num;
			result.Y = axis.Y * num;
			result.Z = axis.Z * num;
			result.W = w;
			return result;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x001083A4 File Offset: 0x001075A4
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			float x = roll * 0.5f;
			float num = MathF.Sin(x);
			float num2 = MathF.Cos(x);
			float x2 = pitch * 0.5f;
			float num3 = MathF.Sin(x2);
			float num4 = MathF.Cos(x2);
			float x3 = yaw * 0.5f;
			float num5 = MathF.Sin(x3);
			float num6 = MathF.Cos(x3);
			Quaternion result;
			result.X = num6 * num3 * num2 + num5 * num4 * num;
			result.Y = num5 * num4 * num2 - num6 * num3 * num;
			result.Z = num6 * num4 * num - num5 * num3 * num2;
			result.W = num6 * num4 * num2 + num5 * num3 * num;
			return result;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00108450 File Offset: 0x00107650
		public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
		{
			float num = matrix.M11 + matrix.M22 + matrix.M33;
			Quaternion result = default(Quaternion);
			if (num > 0f)
			{
				float num2 = MathF.Sqrt(num + 1f);
				result.W = num2 * 0.5f;
				num2 = 0.5f / num2;
				result.X = (matrix.M23 - matrix.M32) * num2;
				result.Y = (matrix.M31 - matrix.M13) * num2;
				result.Z = (matrix.M12 - matrix.M21) * num2;
			}
			else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
			{
				float num3 = MathF.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
				float num4 = 0.5f / num3;
				result.X = 0.5f * num3;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				float num5 = MathF.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
				float num6 = 0.5f / num5;
				result.X = (matrix.M21 + matrix.M12) * num6;
				result.Y = 0.5f * num5;
				result.Z = (matrix.M32 + matrix.M23) * num6;
				result.W = (matrix.M31 - matrix.M13) * num6;
			}
			else
			{
				float num7 = MathF.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
				float num8 = 0.5f / num7;
				result.X = (matrix.M31 + matrix.M13) * num8;
				result.Y = (matrix.M32 + matrix.M23) * num8;
				result.Z = 0.5f * num7;
				result.W = (matrix.M12 - matrix.M21) * num8;
			}
			return result;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x001086A1 File Offset: 0x001078A1
		public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x001086DC File Offset: 0x001078DC
		public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			bool flag = false;
			if (num < 0f)
			{
				flag = true;
				num = -num;
			}
			float num2;
			float num3;
			if (num > 0.999999f)
			{
				num2 = 1f - amount;
				num3 = (flag ? (-amount) : amount);
			}
			else
			{
				float num4 = MathF.Acos(num);
				float num5 = 1f / MathF.Sin(num4);
				num2 = MathF.Sin((1f - amount) * num4) * num5;
				num3 = (flag ? (-MathF.Sin(amount * num4) * num5) : (MathF.Sin(amount * num4) * num5));
			}
			Quaternion result;
			result.X = num2 * quaternion1.X + num3 * quaternion2.X;
			result.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
			result.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
			result.W = num2 * quaternion1.W + num3 * quaternion2.W;
			return result;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00108800 File Offset: 0x00107A00
		public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = 1f - amount;
			Quaternion quaternion3 = default(Quaternion);
			float num2 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			if (num2 >= 0f)
			{
				quaternion3.X = num * quaternion1.X + amount * quaternion2.X;
				quaternion3.Y = num * quaternion1.Y + amount * quaternion2.Y;
				quaternion3.Z = num * quaternion1.Z + amount * quaternion2.Z;
				quaternion3.W = num * quaternion1.W + amount * quaternion2.W;
			}
			else
			{
				quaternion3.X = num * quaternion1.X - amount * quaternion2.X;
				quaternion3.Y = num * quaternion1.Y - amount * quaternion2.Y;
				quaternion3.Z = num * quaternion1.Z - amount * quaternion2.Z;
				quaternion3.W = num * quaternion1.W - amount * quaternion2.W;
			}
			float x = quaternion3.X * quaternion3.X + quaternion3.Y * quaternion3.Y + quaternion3.Z * quaternion3.Z + quaternion3.W * quaternion3.W;
			float num3 = 1f / MathF.Sqrt(x);
			quaternion3.X *= num3;
			quaternion3.Y *= num3;
			quaternion3.Z *= num3;
			quaternion3.W *= num3;
			return quaternion3;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x001089A0 File Offset: 0x00107BA0
		public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
			float x = value2.X;
			float y = value2.Y;
			float z = value2.Z;
			float w = value2.W;
			float x2 = value1.X;
			float y2 = value1.Y;
			float z2 = value1.Z;
			float w2 = value1.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result;
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00108A68 File Offset: 0x00107C68
		public static Quaternion Negate(Quaternion value)
		{
			Quaternion result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
			return result;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00108AB0 File Offset: 0x00107CB0
		public static Quaternion Add(Quaternion value1, Quaternion value2)
		{
			Quaternion result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00108B10 File Offset: 0x00107D10
		public static Quaternion Subtract(Quaternion value1, Quaternion value2)
		{
			Quaternion result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00108B70 File Offset: 0x00107D70
		public static Quaternion Multiply(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result;
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00108C38 File Offset: 0x00107E38
		public static Quaternion Multiply(Quaternion value1, float value2)
		{
			Quaternion result;
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00108C84 File Offset: 0x00107E84
		public static Quaternion Divide(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = -value2.X * num2;
			float num4 = -value2.Y * num2;
			float num5 = -value2.Z * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result;
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00108A68 File Offset: 0x00107C68
		public static Quaternion operator -(Quaternion value)
		{
			Quaternion result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
			return result;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00108AB0 File Offset: 0x00107CB0
		public static Quaternion operator +(Quaternion value1, Quaternion value2)
		{
			Quaternion result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00108B10 File Offset: 0x00107D10
		public static Quaternion operator -(Quaternion value1, Quaternion value2)
		{
			Quaternion result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00108B70 File Offset: 0x00107D70
		public static Quaternion operator *(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result;
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00108C38 File Offset: 0x00107E38
		public static Quaternion operator *(Quaternion value1, float value2)
		{
			Quaternion result;
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00108C84 File Offset: 0x00107E84
		public static Quaternion operator /(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = -value2.X * num2;
			float num4 = -value2.Y * num2;
			float num5 = -value2.Z * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result;
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00108D9D File Offset: 0x00107F9D
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00108DD9 File Offset: 0x00107FD9
		public static bool operator !=(Quaternion value1, Quaternion value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z || value1.W != value2.W;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00108D9D File Offset: 0x00107F9D
		public readonly bool Equals(Quaternion other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00108E18 File Offset: 0x00108018
		[NullableContext(2)]
		public override readonly bool Equals(object obj)
		{
			return obj is Quaternion && this.Equals((Quaternion)obj);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00108E30 File Offset: 0x00108030
		[NullableContext(1)]
		public override readonly string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", new object[]
			{
				this.X,
				this.Y,
				this.Z,
				this.W
			});
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00108E8A File Offset: 0x0010808A
		public override readonly int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x04000627 RID: 1575
		public float X;

		// Token: 0x04000628 RID: 1576
		public float Y;

		// Token: 0x04000629 RID: 1577
		public float Z;

		// Token: 0x0400062A RID: 1578
		public float W;
	}
}
