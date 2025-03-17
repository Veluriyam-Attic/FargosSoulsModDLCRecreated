using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001C9 RID: 457
	[Intrinsic]
	public struct Matrix3x2 : IEquatable<Matrix3x2>
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0010277D File Offset: 0x0010197D
		public static Matrix3x2 Identity
		{
			get
			{
				return Matrix3x2._identity;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00102784 File Offset: 0x00101984
		public readonly bool IsIdentity
		{
			get
			{
				return this.M11 == 1f && this.M22 == 1f && this.M12 == 0f && this.M21 == 0f && this.M31 == 0f && this.M32 == 0f;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x001027E1 File Offset: 0x001019E1
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x001027F4 File Offset: 0x001019F4
		public Vector2 Translation
		{
			readonly get
			{
				return new Vector2(this.M31, this.M32);
			}
			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0010280E File Offset: 0x00101A0E
		public Matrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M21 = m21;
			this.M22 = m22;
			this.M31 = m31;
			this.M32 = m32;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00102840 File Offset: 0x00101A40
		public static Matrix3x2 CreateTranslation(Vector2 position)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M31 = position.X;
			identity.M32 = position.Y;
			return identity;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00102870 File Offset: 0x00101A70
		public static Matrix3x2 CreateTranslation(float xPosition, float yPosition)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M31 = xPosition;
			identity.M32 = yPosition;
			return identity;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00102894 File Offset: 0x00101A94
		public static Matrix3x2 CreateScale(float xScale, float yScale)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M11 = xScale;
			identity.M22 = yScale;
			return identity;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x001028B8 File Offset: 0x00101AB8
		public static Matrix3x2 CreateScale(float xScale, float yScale, Vector2 centerPoint)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			identity.M11 = xScale;
			identity.M22 = yScale;
			identity.M31 = m;
			identity.M32 = m2;
			return identity;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0010290C File Offset: 0x00101B0C
		public static Matrix3x2 CreateScale(Vector2 scales)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M11 = scales.X;
			identity.M22 = scales.Y;
			return identity;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0010293C File Offset: 0x00101B3C
		public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			identity.M11 = scales.X;
			identity.M22 = scales.Y;
			identity.M31 = m;
			identity.M32 = m2;
			return identity;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x001029A4 File Offset: 0x00101BA4
		public static Matrix3x2 CreateScale(float scale)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M11 = scale;
			identity.M22 = scale;
			return identity;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x001029C8 File Offset: 0x00101BC8
		public static Matrix3x2 CreateScale(float scale, Vector2 centerPoint)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			identity.M11 = scale;
			identity.M22 = scale;
			identity.M31 = m;
			identity.M32 = m2;
			return identity;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00102A1C File Offset: 0x00101C1C
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			float m = MathF.Tan(radiansX);
			float m2 = MathF.Tan(radiansY);
			identity.M12 = m2;
			identity.M21 = m;
			return identity;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00102A50 File Offset: 0x00101C50
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY, Vector2 centerPoint)
		{
			Matrix3x2 identity = Matrix3x2._identity;
			float num = MathF.Tan(radiansX);
			float num2 = MathF.Tan(radiansY);
			float m = -centerPoint.Y * num;
			float m2 = -centerPoint.X * num2;
			identity.M12 = num2;
			identity.M21 = num;
			identity.M31 = m;
			identity.M32 = m2;
			return identity;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00102AA8 File Offset: 0x00101CA8
		public static Matrix3x2 CreateRotation(float radians)
		{
			radians = MathF.IEEERemainder(radians, 6.2831855f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			Matrix3x2 identity = Matrix3x2._identity;
			identity.M11 = num;
			identity.M12 = num2;
			identity.M21 = -num2;
			identity.M22 = num;
			return identity;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00102B70 File Offset: 0x00101D70
		public static Matrix3x2 CreateRotation(float radians, Vector2 centerPoint)
		{
			radians = MathF.IEEERemainder(radians, 6.2831855f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix3x2 result;
			result.M11 = num;
			result.M12 = num2;
			result.M21 = -num2;
			result.M22 = num;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00102C74 File Offset: 0x00101E74
		public readonly float GetDeterminant()
		{
			return this.M11 * this.M22 - this.M21 * this.M12;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00102C94 File Offset: 0x00101E94
		public static bool Invert(Matrix3x2 matrix, out Matrix3x2 result)
		{
			float num = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
			if (MathF.Abs(num) < 1E-45f)
			{
				result = new Matrix3x2(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num2 = 1f / num;
			result.M11 = matrix.M22 * num2;
			result.M12 = -matrix.M12 * num2;
			result.M21 = -matrix.M21 * num2;
			result.M22 = matrix.M11 * num2;
			result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * num2;
			result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * num2;
			return true;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00102D80 File Offset: 0x00101F80
		public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, float amount)
		{
			Matrix3x2 result;
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			return result;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00102E3C File Offset: 0x0010203C
		public static Matrix3x2 Negate(Matrix3x2 value)
		{
			Matrix3x2 result;
			result.M11 = -value.M11;
			result.M12 = -value.M12;
			result.M21 = -value.M21;
			result.M22 = -value.M22;
			result.M31 = -value.M31;
			result.M32 = -value.M32;
			return result;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00102EA0 File Offset: 0x001020A0
		public static Matrix3x2 Add(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00102F28 File Offset: 0x00102128
		public static Matrix3x2 Subtract(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00102FB0 File Offset: 0x001021B0
		public static Matrix3x2 Multiply(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00103098 File Offset: 0x00102298
		public static Matrix3x2 Multiply(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00102E3C File Offset: 0x0010203C
		public static Matrix3x2 operator -(Matrix3x2 value)
		{
			Matrix3x2 result;
			result.M11 = -value.M11;
			result.M12 = -value.M12;
			result.M21 = -value.M21;
			result.M22 = -value.M22;
			result.M31 = -value.M31;
			result.M32 = -value.M32;
			return result;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00102EA0 File Offset: 0x001020A0
		public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00102F28 File Offset: 0x00102128
		public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00102FB0 File Offset: 0x001021B0
		public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00103098 File Offset: 0x00102298
		public static Matrix3x2 operator *(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00103100 File Offset: 0x00102300
		public static bool operator ==(Matrix3x2 value1, Matrix3x2 value2)
		{
			return value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M31 == value2.M31 && value1.M32 == value2.M32;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00103164 File Offset: 0x00102364
		public static bool operator !=(Matrix3x2 value1, Matrix3x2 value2)
		{
			return value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M31 != value2.M31 || value1.M32 != value2.M32;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00103100 File Offset: 0x00102300
		public readonly bool Equals(Matrix3x2 other)
		{
			return this.M11 == other.M11 && this.M22 == other.M22 && this.M12 == other.M12 && this.M21 == other.M21 && this.M31 == other.M31 && this.M32 == other.M32;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x001031CC File Offset: 0x001023CC
		[NullableContext(2)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Matrix3x2)
			{
				Matrix3x2 other = (Matrix3x2)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x001031F4 File Offset: 0x001023F4
		[NullableContext(1)]
		public override readonly string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}", new object[]
			{
				this.M11,
				this.M12,
				this.M21,
				this.M22,
				this.M31,
				this.M32
			});
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0010326A File Offset: 0x0010246A
		public override readonly int GetHashCode()
		{
			return HashCode.Combine<float, float, float, float, float, float>(this.M11, this.M12, this.M21, this.M22, this.M31, this.M32);
		}

		// Token: 0x04000607 RID: 1543
		public float M11;

		// Token: 0x04000608 RID: 1544
		public float M12;

		// Token: 0x04000609 RID: 1545
		public float M21;

		// Token: 0x0400060A RID: 1546
		public float M22;

		// Token: 0x0400060B RID: 1547
		public float M31;

		// Token: 0x0400060C RID: 1548
		public float M32;

		// Token: 0x0400060D RID: 1549
		private static readonly Matrix3x2 _identity = new Matrix3x2(1f, 0f, 0f, 1f, 0f, 0f);
	}
}
