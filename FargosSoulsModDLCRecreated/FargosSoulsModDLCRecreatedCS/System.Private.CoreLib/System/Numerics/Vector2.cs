using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x020001CF RID: 463
	[NullableContext(1)]
	[Nullable(0)]
	[Intrinsic]
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00108EBC File Offset: 0x001080BC
		public static Vector2 Zero
		{
			[Intrinsic]
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00108ED2 File Offset: 0x001080D2
		public static Vector2 One
		{
			[Intrinsic]
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00108EE3 File Offset: 0x001080E3
		public static Vector2 UnitX
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00108EF4 File Offset: 0x001080F4
		public static Vector2 UnitY
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00108F05 File Offset: 0x00108105
		public override readonly int GetHashCode()
		{
			return HashCode.Combine<float, float>(this.X, this.Y);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00108F18 File Offset: 0x00108118
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Vector2)
			{
				Vector2 other = (Vector2)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00108F3D File Offset: 0x0010813D
		public override readonly string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00108F4F File Offset: 0x0010814F
		public readonly string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00108F60 File Offset: 0x00108160
		[NullableContext(2)]
		[return: Nullable(1)]
		public readonly string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(this.X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Y.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00108FD0 File Offset: 0x001081D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length()
		{
			float x = Vector2.Dot(this, this);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00108FF5 File Offset: 0x001081F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float LengthSquared()
		{
			return Vector2.Dot(this, this);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00109008 File Offset: 0x00108208
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			Vector2 vector = value1 - value2;
			float x = Vector2.Dot(vector, vector);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0010902C File Offset: 0x0010822C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			Vector2 vector = value1 - value2;
			return Vector2.Dot(vector, vector);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00109048 File Offset: 0x00108248
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Normalize(Vector2 value)
		{
			float value2 = value.Length();
			return value / value2;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00109064 File Offset: 0x00108264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			float num = Vector2.Dot(vector, normal);
			return vector - 2f * num * normal;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0010908C File Offset: 0x0010828C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			return Vector2.Min(Vector2.Max(value1, min), max);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0010909B File Offset: 0x0010829B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return value1 * (1f - amount) + value2 * amount;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x001090B8 File Offset: 0x001082B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix3x2 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00109110 File Offset: 0x00108310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00109166 File Offset: 0x00108366
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix3x2 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x001091A3 File Offset: 0x001083A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix4x4 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x001091E0 File Offset: 0x001083E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num3;
			float num5 = rotation.X * num;
			float num6 = rotation.X * num2;
			float num7 = rotation.Y * num2;
			float num8 = rotation.Z * num3;
			return new Vector2(value.X * (1f - num7 - num8) + value.Y * (num6 - num4), value.X * (num6 + num4) + value.Y * (1f - num5 - num8));
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00109289 File Offset: 0x00108489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Add(Vector2 left, Vector2 right)
		{
			return left + right;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00109292 File Offset: 0x00108492
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Subtract(Vector2 left, Vector2 right)
		{
			return left - right;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0010929B File Offset: 0x0010849B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, Vector2 right)
		{
			return left * right;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x001092A4 File Offset: 0x001084A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, float right)
		{
			return left * right;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x001092AD File Offset: 0x001084AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(float left, Vector2 right)
		{
			return left * right;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x001092B6 File Offset: 0x001084B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, Vector2 right)
		{
			return left / right;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x001092BF File Offset: 0x001084BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x001092C8 File Offset: 0x001084C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Negate(Vector2 value)
		{
			return -value;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x001092D0 File Offset: 0x001084D0
		[Intrinsic]
		public Vector2(float value)
		{
			this = new Vector2(value, value);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x001092DA File Offset: 0x001084DA
		[Intrinsic]
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x001092EA File Offset: 0x001084EA
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x001092F4 File Offset: 0x001084F4
		[Intrinsic]
		public readonly void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException(SR.Arg_NullArgumentNullRef);
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format(SR.Arg_ArgumentOutOfRangeException, index));
			}
			if (array.Length - index < 2)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00109366 File Offset: 0x00108566
		[Intrinsic]
		public readonly bool Equals(Vector2 other)
		{
			return this == other;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00109374 File Offset: 0x00108574
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00109391 File Offset: 0x00108591
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x001093D0 File Offset: 0x001085D0
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0010940F File Offset: 0x0010860F
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Abs(Vector2 value)
		{
			return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0010942C File Offset: 0x0010862C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SquareRoot(Vector2 value)
		{
			return new Vector2(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00109449 File Offset: 0x00108649
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator +(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X + right.X, left.Y + right.Y);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0010946A File Offset: 0x0010866A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X - right.X, left.Y - right.Y);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0010948B File Offset: 0x0010868B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X * right.X, left.Y * right.Y);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x001094AC File Offset: 0x001086AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(float left, Vector2 right)
		{
			return new Vector2(left, left) * right;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x001094BB File Offset: 0x001086BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 left, float right)
		{
			return left * new Vector2(right, right);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x001094CA File Offset: 0x001086CA
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X / right.X, left.Y / right.Y);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x001094EB File Offset: 0x001086EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 value1, float value2)
		{
			return value1 / new Vector2(value2);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x001094F9 File Offset: 0x001086F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 value)
		{
			return Vector2.Zero - value;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00109506 File Offset: 0x00108706
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00109526 File Offset: 0x00108726
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2 left, Vector2 right)
		{
			return !(left == right);
		}

		// Token: 0x0400062B RID: 1579
		public float X;

		// Token: 0x0400062C RID: 1580
		public float Y;
	}
}
