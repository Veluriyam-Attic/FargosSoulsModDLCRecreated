using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x020001D0 RID: 464
	[NullableContext(1)]
	[Nullable(0)]
	[Intrinsic]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00109534 File Offset: 0x00108734
		public static Vector3 Zero
		{
			[Intrinsic]
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0010954A File Offset: 0x0010874A
		public static Vector3 One
		{
			[Intrinsic]
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00109560 File Offset: 0x00108760
		public static Vector3 UnitX
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00109576 File Offset: 0x00108776
		public static Vector3 UnitY
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0010958C File Offset: 0x0010878C
		public static Vector3 UnitZ
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x001095A2 File Offset: 0x001087A2
		public override readonly int GetHashCode()
		{
			return HashCode.Combine<float, float, float>(this.X, this.Y, this.Z);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x001095BC File Offset: 0x001087BC
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Vector3)
			{
				Vector3 other = (Vector3)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x001095E1 File Offset: 0x001087E1
		public override readonly string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x001095F3 File Offset: 0x001087F3
		public readonly string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00109604 File Offset: 0x00108804
		[NullableContext(2)]
		[return: Nullable(1)]
		public readonly string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(((IFormattable)this.X).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Y).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Z).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x001096A8 File Offset: 0x001088A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length()
		{
			float x = Vector3.Dot(this, this);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x001096CD File Offset: 0x001088CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float LengthSquared()
		{
			return Vector3.Dot(this, this);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x001096E0 File Offset: 0x001088E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			Vector3 vector = value1 - value2;
			float x = Vector3.Dot(vector, vector);
			return MathF.Sqrt(x);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00109704 File Offset: 0x00108904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			Vector3 vector = value1 - value2;
			return Vector3.Dot(vector, vector);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00109720 File Offset: 0x00108920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			float value2 = value.Length();
			return value / value2;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0010973C File Offset: 0x0010893C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			return new Vector3(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x001097A0 File Offset: 0x001089A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			float right = Vector3.Dot(vector, normal);
			Vector3 right2 = normal * right * 2f;
			return vector - right2;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x001097CE File Offset: 0x001089CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			return Vector3.Min(Vector3.Max(value1, min), max);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x001097E0 File Offset: 0x001089E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			Vector3 left = value1 * (1f - amount);
			Vector3 right = value2 * amount;
			return left + right;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0010980C File Offset: 0x00108A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x001098B0 File Offset: 0x00108AB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00109940 File Offset: 0x00108B40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
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
			return new Vector3(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00109A57 File Offset: 0x00108C57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			return left + right;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00109A60 File Offset: 0x00108C60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			return left - right;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00109A69 File Offset: 0x00108C69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			return left * right;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00109A72 File Offset: 0x00108C72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return left * right;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00109A7B File Offset: 0x00108C7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(float left, Vector3 right)
		{
			return left * right;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00109A84 File Offset: 0x00108C84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			return left / right;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00109A8D File Offset: 0x00108C8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00109A96 File Offset: 0x00108C96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Negate(Vector3 value)
		{
			return -value;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00109A9E File Offset: 0x00108C9E
		[Intrinsic]
		public Vector3(float value)
		{
			this = new Vector3(value, value, value);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00109AA9 File Offset: 0x00108CA9
		[Intrinsic]
		public Vector3(Vector2 value, float z)
		{
			this = new Vector3(value.X, value.Y, z);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00109ABE File Offset: 0x00108CBE
		[Intrinsic]
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00109AD5 File Offset: 0x00108CD5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00109AE0 File Offset: 0x00108CE0
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
			if (array.Length - index < 3)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00109B5D File Offset: 0x00108D5D
		[Intrinsic]
		public readonly bool Equals(Vector3 other)
		{
			return this == other;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00109B6B File Offset: 0x00108D6B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00109B98 File Offset: 0x00108D98
		[Intrinsic]
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00109C00 File Offset: 0x00108E00
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00109C66 File Offset: 0x00108E66
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Abs(Vector3 value)
		{
			return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00109C8E File Offset: 0x00108E8E
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SquareRoot(Vector3 value)
		{
			return new Vector3(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00109CB6 File Offset: 0x00108EB6
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00109CE4 File Offset: 0x00108EE4
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00109D12 File Offset: 0x00108F12
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00109D40 File Offset: 0x00108F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, float right)
		{
			return left * new Vector3(right);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00109D4E File Offset: 0x00108F4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float left, Vector3 right)
		{
			return new Vector3(left) * right;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00109D5C File Offset: 0x00108F5C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00109D8A File Offset: 0x00108F8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 value1, float value2)
		{
			return value1 / new Vector3(value2);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00109D98 File Offset: 0x00108F98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 value)
		{
			return Vector3.Zero - value;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00109DA5 File Offset: 0x00108FA5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00109DD3 File Offset: 0x00108FD3
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			return !(left == right);
		}

		// Token: 0x0400062D RID: 1581
		public float X;

		// Token: 0x0400062E RID: 1582
		public float Y;

		// Token: 0x0400062F RID: 1583
		public float Z;
	}
}
