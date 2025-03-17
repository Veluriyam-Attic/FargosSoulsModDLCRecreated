using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001CD RID: 461
	[Intrinsic]
	public struct Plane : IEquatable<Plane>
	{
		// Token: 0x06001C14 RID: 7188 RVA: 0x00107932 File Offset: 0x00106B32
		public Plane(float x, float y, float z, float d)
		{
			this.Normal = new Vector3(x, y, z);
			this.D = d;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0010794A File Offset: 0x00106B4A
		public Plane(Vector3 normal, float d)
		{
			this.Normal = normal;
			this.D = d;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0010795A File Offset: 0x00106B5A
		public Plane(Vector4 value)
		{
			this.Normal = new Vector3(value.X, value.Y, value.Z);
			this.D = value.W;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00107988 File Offset: 0x00106B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = point2 - point1;
				Vector3 vector2 = point3 - point1;
				Vector3 value = Vector3.Cross(vector, vector2);
				Vector3 vector3 = Vector3.Normalize(value);
				float d = -Vector3.Dot(vector3, point1);
				return new Plane(vector3, d);
			}
			float num = point2.X - point1.X;
			float num2 = point2.Y - point1.Y;
			float num3 = point2.Z - point1.Z;
			float num4 = point3.X - point1.X;
			float num5 = point3.Y - point1.Y;
			float num6 = point3.Z - point1.Z;
			float num7 = num2 * num6 - num3 * num5;
			float num8 = num3 * num4 - num * num6;
			float num9 = num * num5 - num2 * num4;
			float x = num7 * num7 + num8 * num8 + num9 * num9;
			float num10 = 1f / MathF.Sqrt(x);
			Vector3 vector4 = new Vector3(num7 * num10, num8 * num10, num9 * num10);
			return new Plane(vector4, -(vector4.X * point1.X + vector4.Y * point1.Y + vector4.Z * point1.Z));
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00107ABC File Offset: 0x00106CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Normal.LengthSquared();
				if (MathF.Abs(num - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num2 = MathF.Sqrt(num);
				return new Plane(value.Normal / num2, value.D / num2);
			}
			else
			{
				float num3 = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
				if (MathF.Abs(num3 - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num4 = 1f / MathF.Sqrt(num3);
				return new Plane(value.Normal.X * num4, value.Normal.Y * num4, value.Normal.Z * num4, value.D * num4);
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00107BB4 File Offset: 0x00106DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Matrix4x4 matrix)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Invert(matrix, out matrix4x);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			return new Plane(x * matrix4x.M11 + y * matrix4x.M12 + z * matrix4x.M13 + d * matrix4x.M14, x * matrix4x.M21 + y * matrix4x.M22 + z * matrix4x.M23 + d * matrix4x.M24, x * matrix4x.M31 + y * matrix4x.M32 + z * matrix4x.M33 + d * matrix4x.M34, x * matrix4x.M41 + y * matrix4x.M42 + z * matrix4x.M43 + d * matrix4x.M44);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00107C8C File Offset: 0x00106E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Quaternion rotation)
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
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			return new Plane(x * num13 + y * num14 + z * num15, x * num16 + y * num17 + z * num18, x * num19 + y * num20 + z * num21, plane.D);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00107DD0 File Offset: 0x00106FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Plane plane, Vector4 value)
		{
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D * value.W;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00107E24 File Offset: 0x00107024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotCoordinate(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value) + plane.D;
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00107E8C File Offset: 0x0010708C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotNormal(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value);
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00107EE8 File Offset: 0x001070E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Plane value1, Plane value2)
		{
			return value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z && value1.D == value2.D;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00107F50 File Offset: 0x00107150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Plane value1, Plane value2)
		{
			return value1.Normal.X != value2.Normal.X || value1.Normal.Y != value2.Normal.Y || value1.Normal.Z != value2.Normal.Z || value1.D != value2.D;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00107FB8 File Offset: 0x001071B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Plane other)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return this.Normal.Equals(other.Normal) && this.D == other.D;
			}
			return this.Normal.X == other.Normal.X && this.Normal.Y == other.Normal.Y && this.Normal.Z == other.Normal.Z && this.D == other.D;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00108048 File Offset: 0x00107248
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly bool Equals(object obj)
		{
			return obj is Plane && this.Equals((Plane)obj);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00108060 File Offset: 0x00107260
		[NullableContext(1)]
		public override readonly string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{Normal:{0} D:{1}}}", this.Normal.ToString(), this.D.ToString(currentCulture));
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0010809B File Offset: 0x0010729B
		public override readonly int GetHashCode()
		{
			return this.Normal.GetHashCode() + this.D.GetHashCode();
		}

		// Token: 0x04000625 RID: 1573
		public Vector3 Normal;

		// Token: 0x04000626 RID: 1574
		public float D;
	}
}
