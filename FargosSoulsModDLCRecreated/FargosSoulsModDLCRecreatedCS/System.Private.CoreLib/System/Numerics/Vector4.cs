using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x020001D1 RID: 465
	[NullableContext(1)]
	[Nullable(0)]
	[Intrinsic]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00109DE0 File Offset: 0x00108FE0
		public static Vector4 Zero
		{
			[Intrinsic]
			get
			{
				return default(Vector4);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00109DF6 File Offset: 0x00108FF6
		public static Vector4 One
		{
			[Intrinsic]
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x00109E11 File Offset: 0x00109011
		public static Vector4 UnitX
		{
			get
			{
				return new Vector4(1f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00109E2C File Offset: 0x0010902C
		public static Vector4 UnitY
		{
			get
			{
				return new Vector4(0f, 1f, 0f, 0f);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00109E47 File Offset: 0x00109047
		public static Vector4 UnitZ
		{
			get
			{
				return new Vector4(0f, 0f, 1f, 0f);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00109E62 File Offset: 0x00109062
		public static Vector4 UnitW
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00109E7D File Offset: 0x0010907D
		public override readonly int GetHashCode()
		{
			return HashCode.Combine<float, float, float, float>(this.X, this.Y, this.Z, this.W);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00109E9C File Offset: 0x0010909C
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Vector4)
			{
				Vector4 other = (Vector4)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00109EC1 File Offset: 0x001090C1
		public override readonly string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00109ED3 File Offset: 0x001090D3
		public readonly string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00109EE4 File Offset: 0x001090E4
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
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Z.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.W.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00109FA0 File Offset: 0x001091A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length()
		{
			float x = Vector4.Dot(this, this);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00109FC5 File Offset: 0x001091C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float LengthSquared()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00109FD8 File Offset: 0x001091D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			Vector4 vector = value1 - value2;
			float x = Vector4.Dot(vector, vector);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00109FFC File Offset: 0x001091FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			Vector4 vector = value1 - value2;
			return Vector4.Dot(vector, vector);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0010A018 File Offset: 0x00109218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 vector)
		{
			float value = vector.Length();
			return vector / value;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0010A034 File Offset: 0x00109234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			return Vector4.Min(Vector4.Max(value1, min), max);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0010A043 File Offset: 0x00109243
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return value1 * (1f - amount) + value2 * amount;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0010A060 File Offset: 0x00109260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0010A0FC File Offset: 0x001092FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0010A1D0 File Offset: 0x001093D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
		{
			return new Vector4(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0010A2C0 File Offset: 0x001094C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0010A3B0 File Offset: 0x001095B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0010A4CC File Offset: 0x001096CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0010A5E9 File Offset: 0x001097E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Add(Vector4 left, Vector4 right)
		{
			return left + right;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0010A5F2 File Offset: 0x001097F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Subtract(Vector4 left, Vector4 right)
		{
			return left - right;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0010A5FB File Offset: 0x001097FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, Vector4 right)
		{
			return left * right;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0010A604 File Offset: 0x00109804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, float right)
		{
			return left * new Vector4(right, right, right, right);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0010A615 File Offset: 0x00109815
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(float left, Vector4 right)
		{
			return new Vector4(left, left, left, left) * right;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0010A626 File Offset: 0x00109826
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, Vector4 right)
		{
			return left / right;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0010A62F File Offset: 0x0010982F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0010A638 File Offset: 0x00109838
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Negate(Vector4 value)
		{
			return -value;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0010A640 File Offset: 0x00109840
		[Intrinsic]
		public Vector4(float value)
		{
			this = new Vector4(value, value, value, value);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0010A64C File Offset: 0x0010984C
		[Intrinsic]
		public Vector4(float x, float y, float z, float w)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0010A66B File Offset: 0x0010986B
		[Intrinsic]
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0010A693 File Offset: 0x00109893
		[Intrinsic]
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0010A6C0 File Offset: 0x001098C0
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0010A6CC File Offset: 0x001098CC
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
			if (array.Length - index < 4)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
			array[index + 3] = this.W;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0010A754 File Offset: 0x00109954
		[Intrinsic]
		public readonly bool Equals(Vector4 other)
		{
			return this == other;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0010A762 File Offset: 0x00109962
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0010A79C File Offset: 0x0010999C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z, (value1.W < value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0010A820 File Offset: 0x00109A20
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z, (value1.W > value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0010A8A2 File Offset: 0x00109AA2
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Abs(Vector4 value)
		{
			return new Vector4(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0010A8D5 File Offset: 0x00109AD5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SquareRoot(Vector4 value)
		{
			return new Vector4(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0010A908 File Offset: 0x00109B08
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0010A943 File Offset: 0x00109B43
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0010A97E File Offset: 0x00109B7E
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0010A9B9 File Offset: 0x00109BB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, float right)
		{
			return left * new Vector4(right);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0010A9C7 File Offset: 0x00109BC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float left, Vector4 right)
		{
			return new Vector4(left) * right;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0010A9D5 File Offset: 0x00109BD5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0010AA10 File Offset: 0x00109C10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 value1, float value2)
		{
			return value1 / new Vector4(value2);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0010AA1E File Offset: 0x00109C1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 value)
		{
			return Vector4.Zero - value;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0010AA2B File Offset: 0x00109C2B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0010AA67 File Offset: 0x00109C67
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !(left == right);
		}

		// Token: 0x04000630 RID: 1584
		public float X;

		// Token: 0x04000631 RID: 1585
		public float Y;

		// Token: 0x04000632 RID: 1586
		public float Z;

		// Token: 0x04000633 RID: 1587
		public float W;
	}
}
