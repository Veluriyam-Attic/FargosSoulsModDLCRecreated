using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001CA RID: 458
	[Intrinsic]
	public struct Matrix4x4 : IEquatable<Matrix4x4>
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x001032BF File Offset: 0x001024BF
		public static Matrix4x4 Identity
		{
			get
			{
				return Matrix4x4._identity;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x001032C8 File Offset: 0x001024C8
		public readonly bool IsIdentity
		{
			get
			{
				return this.M11 == 1f && this.M22 == 1f && this.M33 == 1f && this.M44 == 1f && this.M12 == 0f && this.M13 == 0f && this.M14 == 0f && this.M21 == 0f && this.M23 == 0f && this.M24 == 0f && this.M31 == 0f && this.M32 == 0f && this.M34 == 0f && this.M41 == 0f && this.M42 == 0f && this.M43 == 0f;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x001033B9 File Offset: 0x001025B9
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x001033D2 File Offset: 0x001025D2
		public Vector3 Translation
		{
			readonly get
			{
				return new Vector3(this.M41, this.M42, this.M43);
			}
			set
			{
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x001033F8 File Offset: 0x001025F8
		public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00103484 File Offset: 0x00102684
		public Matrix4x4(Matrix3x2 value)
		{
			this.M11 = value.M11;
			this.M12 = value.M12;
			this.M13 = 0f;
			this.M14 = 0f;
			this.M21 = value.M21;
			this.M22 = value.M22;
			this.M23 = 0f;
			this.M24 = 0f;
			this.M31 = 0f;
			this.M32 = 0f;
			this.M33 = 1f;
			this.M34 = 0f;
			this.M41 = value.M31;
			this.M42 = value.M32;
			this.M43 = 0f;
			this.M44 = 1f;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00103548 File Offset: 0x00102748
		public static Matrix4x4 CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
		{
			Vector3 vector = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = -cameraForwardVector;
			}
			else
			{
				vector = Vector3.Multiply(vector, 1f / MathF.Sqrt(num));
			}
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 result;
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00103694 File Offset: 0x00102894
		public static Matrix4x4 CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector)
		{
			Vector3 vector = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = -cameraForwardVector;
			}
			else
			{
				vector = Vector3.Multiply(vector, 1f / MathF.Sqrt(num));
			}
			float x = Vector3.Dot(rotateAxis, vector);
			Vector3 vector2;
			Vector3 vector3;
			if (MathF.Abs(x) > 0.99825466f)
			{
				vector2 = objectForwardVector;
				x = Vector3.Dot(rotateAxis, vector2);
				if (MathF.Abs(x) > 0.99825466f)
				{
					vector2 = ((MathF.Abs(rotateAxis.Z) > 0.99825466f) ? new Vector3(1f, 0f, 0f) : new Vector3(0f, 0f, -1f));
				}
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, vector2));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, rotateAxis));
			}
			else
			{
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, vector));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, rotateAxis));
			}
			Matrix4x4 result;
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = 0f;
			result.M21 = rotateAxis.X;
			result.M22 = rotateAxis.Y;
			result.M23 = rotateAxis.Z;
			result.M24 = 0f;
			result.M31 = vector2.X;
			result.M32 = vector2.Y;
			result.M33 = vector2.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00103878 File Offset: 0x00102A78
		public static Matrix4x4 CreateTranslation(Vector3 position)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M41 = position.X;
			identity.M42 = position.Y;
			identity.M43 = position.Z;
			return identity;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x001038B4 File Offset: 0x00102AB4
		public static Matrix4x4 CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M41 = xPosition;
			identity.M42 = yPosition;
			identity.M43 = zPosition;
			return identity;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x001038E0 File Offset: 0x00102AE0
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = xScale;
			identity.M22 = yScale;
			identity.M33 = zScale;
			return identity;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0010390C File Offset: 0x00102B0C
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			float m3 = centerPoint.Z * (1f - zScale);
			identity.M11 = xScale;
			identity.M22 = yScale;
			identity.M33 = zScale;
			identity.M41 = m;
			identity.M42 = m2;
			identity.M43 = m3;
			return identity;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00103980 File Offset: 0x00102B80
		public static Matrix4x4 CreateScale(Vector3 scales)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = scales.X;
			identity.M22 = scales.Y;
			identity.M33 = scales.Z;
			return identity;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x001039BC File Offset: 0x00102BBC
		public static Matrix4x4 CreateScale(Vector3 scales, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			float m3 = centerPoint.Z * (1f - scales.Z);
			identity.M11 = scales.X;
			identity.M22 = scales.Y;
			identity.M33 = scales.Z;
			identity.M41 = m;
			identity.M42 = m2;
			identity.M43 = m3;
			return identity;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00103A4C File Offset: 0x00102C4C
		public static Matrix4x4 CreateScale(float scale)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = scale;
			identity.M22 = scale;
			identity.M33 = scale;
			return identity;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00103A78 File Offset: 0x00102C78
		public static Matrix4x4 CreateScale(float scale, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			float m3 = centerPoint.Z * (1f - scale);
			identity.M11 = scale;
			identity.M22 = scale;
			identity.M33 = scale;
			identity.M41 = m;
			identity.M42 = m2;
			identity.M43 = m3;
			return identity;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00103AEC File Offset: 0x00102CEC
		public static Matrix4x4 CreateRotationX(float radians)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			identity.M22 = num;
			identity.M23 = num2;
			identity.M32 = -num2;
			identity.M33 = num;
			return identity;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00103B30 File Offset: 0x00102D30
		public static Matrix4x4 CreateRotationX(float radians, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.Y * (1f - num) + centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) - centerPoint.Y * num2;
			identity.M22 = num;
			identity.M23 = num2;
			identity.M32 = -num2;
			identity.M33 = num;
			identity.M42 = m;
			identity.M43 = m2;
			return identity;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00103BB8 File Offset: 0x00102DB8
		public static Matrix4x4 CreateRotationY(float radians)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			identity.M11 = num;
			identity.M13 = -num2;
			identity.M31 = num2;
			identity.M33 = num;
			return identity;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00103BFC File Offset: 0x00102DFC
		public static Matrix4x4 CreateRotationY(float radians, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) - centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) + centerPoint.X * num2;
			identity.M11 = num;
			identity.M13 = -num2;
			identity.M31 = num2;
			identity.M33 = num;
			identity.M41 = m;
			identity.M43 = m2;
			return identity;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00103C84 File Offset: 0x00102E84
		public static Matrix4x4 CreateRotationZ(float radians)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			identity.M11 = num;
			identity.M12 = num2;
			identity.M21 = -num2;
			identity.M22 = num;
			return identity;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00103CC8 File Offset: 0x00102EC8
		public static Matrix4x4 CreateRotationZ(float radians, Vector3 centerPoint)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			identity.M11 = num;
			identity.M12 = num2;
			identity.M21 = -num2;
			identity.M22 = num;
			identity.M41 = m;
			identity.M42 = m2;
			return identity;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00103D50 File Offset: 0x00102F50
		public static Matrix4x4 CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = MathF.Sin(angle);
			float num2 = MathF.Cos(angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = num3 + num2 * (1f - num3);
			identity.M12 = num6 - num2 * num6 + num * z;
			identity.M13 = num7 - num2 * num7 - num * y;
			identity.M21 = num6 - num2 * num6 - num * z;
			identity.M22 = num4 + num2 * (1f - num4);
			identity.M23 = num8 - num2 * num8 + num * x;
			identity.M31 = num7 - num2 * num7 + num * y;
			identity.M32 = num8 - num2 * num8 - num * x;
			identity.M33 = num5 + num2 * (1f - num5);
			return identity;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00103E5C File Offset: 0x0010305C
		public static Matrix4x4 CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			if (fieldOfView <= 0f || fieldOfView >= 3.1415927f)
			{
				throw new ArgumentOutOfRangeException("fieldOfView");
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			float num = 1f / MathF.Tan(fieldOfView * 0.5f);
			float m = num / aspectRatio;
			Matrix4x4 result;
			result.M11 = m;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			float num2 = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
			result.M33 = num2;
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num2;
			return result;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00103F98 File Offset: 0x00103198
		public static Matrix4x4 CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result;
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			float num = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
			result.M33 = num;
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num;
			return result;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x001040A4 File Offset: 0x001032A4
		public static Matrix4x4 CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result;
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			float num = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
			result.M33 = num;
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * num;
			result.M41 = (result.M42 = (result.M44 = 0f));
			return result;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x001041C8 File Offset: 0x001033C8
		public static Matrix4x4 CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = 2f / width;
			identity.M22 = 2f / height;
			identity.M33 = 1f / (zNearPlane - zFarPlane);
			identity.M43 = zNearPlane / (zNearPlane - zFarPlane);
			return identity;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00104214 File Offset: 0x00103414
		public static Matrix4x4 CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = 2f / (right - left);
			identity.M22 = 2f / (top - bottom);
			identity.M33 = 1f / (zNearPlane - zFarPlane);
			identity.M41 = (left + right) / (left - right);
			identity.M42 = (top + bottom) / (bottom - top);
			identity.M43 = zNearPlane / (zNearPlane - zFarPlane);
			return identity;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00104288 File Offset: 0x00103488
		public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = vector2.X;
			identity.M12 = vector3.X;
			identity.M13 = vector.X;
			identity.M21 = vector2.Y;
			identity.M22 = vector3.Y;
			identity.M23 = vector.Y;
			identity.M31 = vector2.Z;
			identity.M32 = vector3.Z;
			identity.M33 = vector.Z;
			identity.M41 = -Vector3.Dot(vector2, cameraPosition);
			identity.M42 = -Vector3.Dot(vector3, cameraPosition);
			identity.M43 = -Vector3.Dot(vector, cameraPosition);
			return identity;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00104360 File Offset: 0x00103560
		public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
		{
			Vector3 vector = Vector3.Normalize(-forward);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = vector2.X;
			identity.M12 = vector2.Y;
			identity.M13 = vector2.Z;
			identity.M21 = vector3.X;
			identity.M22 = vector3.Y;
			identity.M23 = vector3.Z;
			identity.M31 = vector.X;
			identity.M32 = vector.Y;
			identity.M33 = vector.Z;
			identity.M41 = position.X;
			identity.M42 = position.Y;
			identity.M43 = position.Z;
			return identity;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00104434 File Offset: 0x00103634
		public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
		{
			Matrix4x4 identity = Matrix4x4._identity;
			float num = quaternion.X * quaternion.X;
			float num2 = quaternion.Y * quaternion.Y;
			float num3 = quaternion.Z * quaternion.Z;
			float num4 = quaternion.X * quaternion.Y;
			float num5 = quaternion.Z * quaternion.W;
			float num6 = quaternion.Z * quaternion.X;
			float num7 = quaternion.Y * quaternion.W;
			float num8 = quaternion.Y * quaternion.Z;
			float num9 = quaternion.X * quaternion.W;
			identity.M11 = 1f - 2f * (num2 + num3);
			identity.M12 = 2f * (num4 + num5);
			identity.M13 = 2f * (num6 - num7);
			identity.M21 = 2f * (num4 - num5);
			identity.M22 = 1f - 2f * (num3 + num);
			identity.M23 = 2f * (num8 + num9);
			identity.M31 = 2f * (num6 + num7);
			identity.M32 = 2f * (num8 - num9);
			identity.M33 = 1f - 2f * (num2 + num);
			return identity;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0010457C File Offset: 0x0010377C
		public static Matrix4x4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			Quaternion quaternion = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
			return Matrix4x4.CreateFromQuaternion(quaternion);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00104598 File Offset: 0x00103798
		public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
		{
			Plane plane2 = Plane.Normalize(plane);
			float num = plane2.Normal.X * lightDirection.X + plane2.Normal.Y * lightDirection.Y + plane2.Normal.Z * lightDirection.Z;
			float num2 = -plane2.Normal.X;
			float num3 = -plane2.Normal.Y;
			float num4 = -plane2.Normal.Z;
			float num5 = -plane2.D;
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = num2 * lightDirection.X + num;
			identity.M21 = num3 * lightDirection.X;
			identity.M31 = num4 * lightDirection.X;
			identity.M41 = num5 * lightDirection.X;
			identity.M12 = num2 * lightDirection.Y;
			identity.M22 = num3 * lightDirection.Y + num;
			identity.M32 = num4 * lightDirection.Y;
			identity.M42 = num5 * lightDirection.Y;
			identity.M13 = num2 * lightDirection.Z;
			identity.M23 = num3 * lightDirection.Z;
			identity.M33 = num4 * lightDirection.Z + num;
			identity.M43 = num5 * lightDirection.Z;
			identity.M44 = num;
			return identity;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x001046E8 File Offset: 0x001038E8
		public static Matrix4x4 CreateReflection(Plane value)
		{
			value = Plane.Normalize(value);
			float x = value.Normal.X;
			float y = value.Normal.Y;
			float z = value.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			Matrix4x4 identity = Matrix4x4._identity;
			identity.M11 = num * x + 1f;
			identity.M12 = num2 * x;
			identity.M13 = num3 * x;
			identity.M21 = num * y;
			identity.M22 = num2 * y + 1f;
			identity.M23 = num3 * y;
			identity.M31 = num * z;
			identity.M32 = num2 * z;
			identity.M33 = num3 * z + 1f;
			identity.M41 = num * value.D;
			identity.M42 = num2 * value.D;
			identity.M43 = num3 * value.D;
			return identity;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x001047E8 File Offset: 0x001039E8
		public readonly float GetDeterminant()
		{
			float m = this.M11;
			float m2 = this.M12;
			float m3 = this.M13;
			float m4 = this.M14;
			float m5 = this.M21;
			float m6 = this.M22;
			float m7 = this.M23;
			float m8 = this.M24;
			float m9 = this.M31;
			float m10 = this.M32;
			float m11 = this.M33;
			float m12 = this.M34;
			float m13 = this.M41;
			float m14 = this.M42;
			float m15 = this.M43;
			float m16 = this.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			return m * (m6 * num - m7 * num2 + m8 * num3) - m2 * (m5 * num - m7 * num4 + m8 * num5) + m3 * (m5 * num2 - m6 * num4 + m8 * num6) - m4 * (m5 * num3 - m6 * num5 + m7 * num6);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0010490E File Offset: 0x00103B0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<float> Permute(Vector128<float> value, byte control)
		{
			if (Avx.IsSupported)
			{
				return Avx.Permute(value, control);
			}
			if (Sse.IsSupported)
			{
				return Sse.Shuffle(value, value, control);
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00104934 File Offset: 0x00103B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
		{
			if (Sse.IsSupported)
			{
				return Matrix4x4.<Invert>g__SseImpl|59_0(matrix, out result);
			}
			return Matrix4x4.<Invert>g__SoftwareFallback|59_1(matrix, out result);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0010494C File Offset: 0x00103B4C
		public unsafe static bool Decompose(Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			bool result = true;
			fixed (Vector3* ptr = &scale)
			{
				Vector3* ptr2 = ptr;
				float* ptr3 = (float*)ptr2;
				Matrix4x4.VectorBasis vectorBasis;
				Vector3** ptr4 = (Vector3**)(&vectorBasis);
				Matrix4x4 identity = Matrix4x4.Identity;
				Matrix4x4.CanonicalBasis canonicalBasis = default(Matrix4x4.CanonicalBasis);
				Vector3* ptr5 = &canonicalBasis.Row0;
				canonicalBasis.Row0 = new Vector3(1f, 0f, 0f);
				canonicalBasis.Row1 = new Vector3(0f, 1f, 0f);
				canonicalBasis.Row2 = new Vector3(0f, 0f, 1f);
				translation = new Vector3(matrix.M41, matrix.M42, matrix.M43);
				*(IntPtr*)ptr4 = &identity.M11;
				*(IntPtr*)(ptr4 + sizeof(Vector3*) / sizeof(Vector3*)) = &identity.M21;
				*(IntPtr*)(ptr4 + (IntPtr)2 * (IntPtr)sizeof(Vector3*) / (IntPtr)sizeof(Vector3*)) = &identity.M31;
				*(*(IntPtr*)ptr4) = new Vector3(matrix.M11, matrix.M12, matrix.M13);
				*(*(IntPtr*)(ptr4 + sizeof(Vector3*) / sizeof(Vector3*))) = new Vector3(matrix.M21, matrix.M22, matrix.M23);
				*(*(IntPtr*)(ptr4 + (IntPtr)2 * (IntPtr)sizeof(Vector3*) / (IntPtr)sizeof(Vector3*))) = new Vector3(matrix.M31, matrix.M32, matrix.M33);
				scale.X = ((IntPtr*)ptr4)->Length();
				scale.Y = ((IntPtr*)(ptr4 + sizeof(Vector3*) / sizeof(Vector3*)))->Length();
				scale.Z = ((IntPtr*)(ptr4 + (IntPtr)2 * (IntPtr)sizeof(Vector3*) / (IntPtr)sizeof(Vector3*)))->Length();
				float num = *ptr3;
				float num2 = ptr3[1];
				float num3 = ptr3[2];
				uint num4;
				uint num5;
				uint num6;
				if (num < num2)
				{
					if (num2 < num3)
					{
						num4 = 2U;
						num5 = 1U;
						num6 = 0U;
					}
					else
					{
						num4 = 1U;
						if (num < num3)
						{
							num5 = 2U;
							num6 = 0U;
						}
						else
						{
							num5 = 0U;
							num6 = 2U;
						}
					}
				}
				else if (num < num3)
				{
					num4 = 2U;
					num5 = 0U;
					num6 = 1U;
				}
				else
				{
					num4 = 0U;
					if (num2 < num3)
					{
						num5 = 2U;
						num6 = 1U;
					}
					else
					{
						num5 = 1U;
						num6 = 2U;
					}
				}
				if (ptr3[(ulong)num4 * 4UL / 4UL] < 0.0001f)
				{
					*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = ptr5[(ulong)num4 * (ulong)((long)sizeof(Vector3)) / (ulong)sizeof(Vector3)];
				}
				*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = Vector3.Normalize(*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))));
				if (ptr3[(ulong)num5 * 4UL / 4UL] < 0.0001f)
				{
					float num7 = MathF.Abs(((IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*)))->X);
					float num8 = MathF.Abs(((IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*)))->Y);
					float num9 = MathF.Abs(((IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*)))->Z);
					uint num10;
					if (num7 < num8)
					{
						if (num8 < num9)
						{
							num10 = 0U;
						}
						else if (num7 < num9)
						{
							num10 = 0U;
						}
						else
						{
							num10 = 2U;
						}
					}
					else if (num7 < num9)
					{
						num10 = 1U;
					}
					else if (num8 < num9)
					{
						num10 = 1U;
					}
					else
					{
						num10 = 2U;
					}
					*(*(IntPtr*)(ptr4 + (ulong)num5 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = Vector3.Cross(*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))), ptr5[(ulong)num10 * (ulong)((long)sizeof(Vector3)) / (ulong)sizeof(Vector3)]);
				}
				*(*(IntPtr*)(ptr4 + (ulong)num5 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = Vector3.Normalize(*(*(IntPtr*)(ptr4 + (ulong)num5 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))));
				if (ptr3[(ulong)num6 * 4UL / 4UL] < 0.0001f)
				{
					*(*(IntPtr*)(ptr4 + (ulong)num6 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = Vector3.Cross(*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))), *(*(IntPtr*)(ptr4 + (ulong)num5 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))));
				}
				*(*(IntPtr*)(ptr4 + (ulong)num6 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = Vector3.Normalize(*(*(IntPtr*)(ptr4 + (ulong)num6 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))));
				float num11 = identity.GetDeterminant();
				if (num11 < 0f)
				{
					ptr3[(ulong)num4 * 4UL / 4UL] = -ptr3[(ulong)num4 * 4UL / 4UL];
					*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))) = -(*(*(IntPtr*)(ptr4 + (ulong)num4 * (ulong)((long)sizeof(Vector3*)) / (ulong)sizeof(Vector3*))));
					num11 = -num11;
				}
				num11 -= 1f;
				num11 *= num11;
				if (0.0001f < num11)
				{
					rotation = Quaternion.Identity;
					result = false;
				}
				else
				{
					rotation = Quaternion.CreateFromRotationMatrix(identity);
				}
			}
			return result;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00104DCC File Offset: 0x00103FCC
		public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
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
			Matrix4x4 result;
			result.M11 = value.M11 * num13 + value.M12 * num14 + value.M13 * num15;
			result.M12 = value.M11 * num16 + value.M12 * num17 + value.M13 * num18;
			result.M13 = value.M11 * num19 + value.M12 * num20 + value.M13 * num21;
			result.M14 = value.M14;
			result.M21 = value.M21 * num13 + value.M22 * num14 + value.M23 * num15;
			result.M22 = value.M21 * num16 + value.M22 * num17 + value.M23 * num18;
			result.M23 = value.M21 * num19 + value.M22 * num20 + value.M23 * num21;
			result.M24 = value.M24;
			result.M31 = value.M31 * num13 + value.M32 * num14 + value.M33 * num15;
			result.M32 = value.M31 * num16 + value.M32 * num17 + value.M33 * num18;
			result.M33 = value.M31 * num19 + value.M32 * num20 + value.M33 * num21;
			result.M34 = value.M34;
			result.M41 = value.M41 * num13 + value.M42 * num14 + value.M43 * num15;
			result.M42 = value.M41 * num16 + value.M42 * num17 + value.M43 * num18;
			result.M43 = value.M41 * num19 + value.M42 * num20 + value.M43 * num21;
			result.M44 = value.M44;
			return result;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00105094 File Offset: 0x00104294
		public unsafe static Matrix4x4 Transpose(Matrix4x4 matrix)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector128<float> left = AdvSimd.LoadVector128(&matrix.M11);
				Vector128<float> right = AdvSimd.LoadVector128(&matrix.M31);
				Vector128<float> left2 = AdvSimd.Arm64.ZipLow(left, right);
				Vector128<float> left3 = AdvSimd.Arm64.ZipHigh(left, right);
				Vector128<float> left4 = AdvSimd.LoadVector128(&matrix.M21);
				Vector128<float> right2 = AdvSimd.LoadVector128(&matrix.M41);
				Vector128<float> right3 = AdvSimd.Arm64.ZipLow(left4, right2);
				Vector128<float> right4 = AdvSimd.Arm64.ZipHigh(left4, right2);
				AdvSimd.Store(&matrix.M11, AdvSimd.Arm64.ZipLow(left2, right3));
				AdvSimd.Store(&matrix.M21, AdvSimd.Arm64.ZipHigh(left2, right3));
				AdvSimd.Store(&matrix.M31, AdvSimd.Arm64.ZipLow(left3, right4));
				AdvSimd.Store(&matrix.M41, AdvSimd.Arm64.ZipHigh(left3, right4));
				return matrix;
			}
			if (Sse.IsSupported)
			{
				Vector128<float> left5 = Sse.LoadVector128(&matrix.M11);
				Vector128<float> right5 = Sse.LoadVector128(&matrix.M21);
				Vector128<float> left6 = Sse.LoadVector128(&matrix.M31);
				Vector128<float> right6 = Sse.LoadVector128(&matrix.M41);
				Vector128<float> vector = Sse.UnpackLow(left5, right5);
				Vector128<float> vector2 = Sse.UnpackLow(left6, right6);
				Vector128<float> vector3 = Sse.UnpackHigh(left5, right5);
				Vector128<float> vector4 = Sse.UnpackHigh(left6, right6);
				Sse.Store(&matrix.M11, Sse.MoveLowToHigh(vector, vector2));
				Sse.Store(&matrix.M21, Sse.MoveHighToLow(vector2, vector));
				Sse.Store(&matrix.M31, Sse.MoveLowToHigh(vector3, vector4));
				Sse.Store(&matrix.M41, Sse.MoveHighToLow(vector4, vector3));
				return matrix;
			}
			Matrix4x4 result;
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;
			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;
			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;
			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
			return result;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00105304 File Offset: 0x00104504
		public unsafe static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, float amount)
		{
			if (AdvSimd.IsSupported)
			{
				Vector128<float> t = Vector128.Create(amount);
				AdvSimd.Store(&matrix1.M11, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M11), AdvSimd.LoadVector128(&matrix2.M11), t));
				AdvSimd.Store(&matrix1.M21, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M21), AdvSimd.LoadVector128(&matrix2.M21), t));
				AdvSimd.Store(&matrix1.M31, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M31), AdvSimd.LoadVector128(&matrix2.M31), t));
				AdvSimd.Store(&matrix1.M41, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M41), AdvSimd.LoadVector128(&matrix2.M41), t));
				return matrix1;
			}
			if (Sse.IsSupported)
			{
				Vector128<float> t2 = Vector128.Create(amount);
				Sse.Store(&matrix1.M11, VectorMath.Lerp(Sse.LoadVector128(&matrix1.M11), Sse.LoadVector128(&matrix2.M11), t2));
				Sse.Store(&matrix1.M21, VectorMath.Lerp(Sse.LoadVector128(&matrix1.M21), Sse.LoadVector128(&matrix2.M21), t2));
				Sse.Store(&matrix1.M31, VectorMath.Lerp(Sse.LoadVector128(&matrix1.M31), Sse.LoadVector128(&matrix2.M31), t2));
				Sse.Store(&matrix1.M41, VectorMath.Lerp(Sse.LoadVector128(&matrix1.M41), Sse.LoadVector128(&matrix2.M41), t2));
				return matrix1;
			}
			Matrix4x4 result;
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
			return result;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00105670 File Offset: 0x00104870
		public static Matrix4x4 Negate(Matrix4x4 value)
		{
			return -value;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00105678 File Offset: 0x00104878
		public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2)
		{
			return value1 + value2;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00105681 File Offset: 0x00104881
		public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2)
		{
			return value1 - value2;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0010568A File Offset: 0x0010488A
		public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
		{
			return value1 * value2;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00105693 File Offset: 0x00104893
		public static Matrix4x4 Multiply(Matrix4x4 value1, float value2)
		{
			return value1 * value2;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0010569C File Offset: 0x0010489C
		public unsafe static Matrix4x4 operator -(Matrix4x4 value)
		{
			if (AdvSimd.IsSupported)
			{
				AdvSimd.Store(&value.M11, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M11)));
				AdvSimd.Store(&value.M21, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M21)));
				AdvSimd.Store(&value.M31, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M31)));
				AdvSimd.Store(&value.M41, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M41)));
				return value;
			}
			if (Sse.IsSupported)
			{
				Vector128<float> zero = Vector128<float>.Zero;
				Sse.Store(&value.M11, Sse.Subtract(zero, Sse.LoadVector128(&value.M11)));
				Sse.Store(&value.M21, Sse.Subtract(zero, Sse.LoadVector128(&value.M21)));
				Sse.Store(&value.M31, Sse.Subtract(zero, Sse.LoadVector128(&value.M31)));
				Sse.Store(&value.M41, Sse.Subtract(zero, Sse.LoadVector128(&value.M41)));
				return value;
			}
			Matrix4x4 result;
			result.M11 = -value.M11;
			result.M12 = -value.M12;
			result.M13 = -value.M13;
			result.M14 = -value.M14;
			result.M21 = -value.M21;
			result.M22 = -value.M22;
			result.M23 = -value.M23;
			result.M24 = -value.M24;
			result.M31 = -value.M31;
			result.M32 = -value.M32;
			result.M33 = -value.M33;
			result.M34 = -value.M34;
			result.M41 = -value.M41;
			result.M42 = -value.M42;
			result.M43 = -value.M43;
			result.M44 = -value.M44;
			return result;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x001058A4 File Offset: 0x00104AA4
		public unsafe static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (AdvSimd.IsSupported)
			{
				AdvSimd.Store(&value1.M11, AdvSimd.Add(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)));
				AdvSimd.Store(&value1.M21, AdvSimd.Add(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)));
				AdvSimd.Store(&value1.M31, AdvSimd.Add(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)));
				AdvSimd.Store(&value1.M41, AdvSimd.Add(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41)));
				return value1;
			}
			if (Sse.IsSupported)
			{
				Sse.Store(&value1.M11, Sse.Add(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)));
				Sse.Store(&value1.M21, Sse.Add(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)));
				Sse.Store(&value1.M31, Sse.Add(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)));
				Sse.Store(&value1.M41, Sse.Add(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41)));
				return value1;
			}
			Matrix4x4 result;
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M13 = value1.M13 + value2.M13;
			result.M14 = value1.M14 + value2.M14;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M23 = value1.M23 + value2.M23;
			result.M24 = value1.M24 + value2.M24;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			result.M33 = value1.M33 + value2.M33;
			result.M34 = value1.M34 + value2.M34;
			result.M41 = value1.M41 + value2.M41;
			result.M42 = value1.M42 + value2.M42;
			result.M43 = value1.M43 + value2.M43;
			result.M44 = value1.M44 + value2.M44;
			return result;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00105B6C File Offset: 0x00104D6C
		public unsafe static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (AdvSimd.IsSupported)
			{
				AdvSimd.Store(&value1.M11, AdvSimd.Subtract(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)));
				AdvSimd.Store(&value1.M21, AdvSimd.Subtract(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)));
				AdvSimd.Store(&value1.M31, AdvSimd.Subtract(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)));
				AdvSimd.Store(&value1.M41, AdvSimd.Subtract(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41)));
				return value1;
			}
			if (Sse.IsSupported)
			{
				Sse.Store(&value1.M11, Sse.Subtract(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)));
				Sse.Store(&value1.M21, Sse.Subtract(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)));
				Sse.Store(&value1.M31, Sse.Subtract(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)));
				Sse.Store(&value1.M41, Sse.Subtract(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41)));
				return value1;
			}
			Matrix4x4 result;
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M13 = value1.M13 - value2.M13;
			result.M14 = value1.M14 - value2.M14;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M23 = value1.M23 - value2.M23;
			result.M24 = value1.M24 - value2.M24;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			result.M33 = value1.M33 - value2.M33;
			result.M34 = value1.M34 - value2.M34;
			result.M41 = value1.M41 - value2.M41;
			result.M42 = value1.M42 - value2.M42;
			result.M43 = value1.M43 - value2.M43;
			result.M44 = value1.M44 - value2.M44;
			return result;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00105E34 File Offset: 0x00105034
		public unsafe static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				Matrix4x4 result;
				Unsafe.SkipInit<Matrix4x4>(out result);
				Vector128<float> right = AdvSimd.LoadVector128(&value1.M11);
				Vector128<float> addend = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), right, 0);
				Vector128<float> addend2 = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M21), right, 1);
				Vector128<float> left = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend, AdvSimd.LoadVector128(&value2.M31), right, 2);
				Vector128<float> right2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend2, AdvSimd.LoadVector128(&value2.M41), right, 3);
				AdvSimd.Store(&result.M11, AdvSimd.Add(left, right2));
				Vector128<float> right3 = AdvSimd.LoadVector128(&value1.M21);
				addend = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), right3, 0);
				addend2 = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M21), right3, 1);
				left = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend, AdvSimd.LoadVector128(&value2.M31), right3, 2);
				right2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend2, AdvSimd.LoadVector128(&value2.M41), right3, 3);
				AdvSimd.Store(&result.M21, AdvSimd.Add(left, right2));
				Vector128<float> right4 = AdvSimd.LoadVector128(&value1.M31);
				addend = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), right4, 0);
				addend2 = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M21), right4, 1);
				left = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend, AdvSimd.LoadVector128(&value2.M31), right4, 2);
				right2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend2, AdvSimd.LoadVector128(&value2.M41), right4, 3);
				AdvSimd.Store(&result.M31, AdvSimd.Add(left, right2));
				Vector128<float> right5 = AdvSimd.LoadVector128(&value1.M41);
				addend = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), right5, 0);
				addend2 = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M21), right5, 1);
				left = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend, AdvSimd.LoadVector128(&value2.M31), right5, 2);
				right2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(addend2, AdvSimd.LoadVector128(&value2.M41), right5, 3);
				AdvSimd.Store(&result.M41, AdvSimd.Add(left, right2));
				return result;
			}
			if (Sse.IsSupported)
			{
				Vector128<float> vector = Sse.LoadVector128(&value1.M11);
				Sse.Store(&value1.M11, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0), Sse.LoadVector128(&value2.M11)), Sse.Multiply(Sse.Shuffle(vector, vector, 85), Sse.LoadVector128(&value2.M21))), Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 170), Sse.LoadVector128(&value2.M31)), Sse.Multiply(Sse.Shuffle(vector, vector, byte.MaxValue), Sse.LoadVector128(&value2.M41)))));
				vector = Sse.LoadVector128(&value1.M21);
				Sse.Store(&value1.M21, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0), Sse.LoadVector128(&value2.M11)), Sse.Multiply(Sse.Shuffle(vector, vector, 85), Sse.LoadVector128(&value2.M21))), Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 170), Sse.LoadVector128(&value2.M31)), Sse.Multiply(Sse.Shuffle(vector, vector, byte.MaxValue), Sse.LoadVector128(&value2.M41)))));
				vector = Sse.LoadVector128(&value1.M31);
				Sse.Store(&value1.M31, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0), Sse.LoadVector128(&value2.M11)), Sse.Multiply(Sse.Shuffle(vector, vector, 85), Sse.LoadVector128(&value2.M21))), Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 170), Sse.LoadVector128(&value2.M31)), Sse.Multiply(Sse.Shuffle(vector, vector, byte.MaxValue), Sse.LoadVector128(&value2.M41)))));
				vector = Sse.LoadVector128(&value1.M41);
				Sse.Store(&value1.M41, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0), Sse.LoadVector128(&value2.M11)), Sse.Multiply(Sse.Shuffle(vector, vector, 85), Sse.LoadVector128(&value2.M21))), Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 170), Sse.LoadVector128(&value2.M31)), Sse.Multiply(Sse.Shuffle(vector, vector, byte.MaxValue), Sse.LoadVector128(&value2.M41)))));
				return value1;
			}
			Matrix4x4 result2;
			result2.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
			result2.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
			result2.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
			result2.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
			result2.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
			result2.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
			result2.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
			result2.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
			result2.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
			result2.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
			result2.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
			result2.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
			result2.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
			result2.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
			result2.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
			result2.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
			return result2;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x001066D8 File Offset: 0x001058D8
		public unsafe static Matrix4x4 operator *(Matrix4x4 value1, float value2)
		{
			if (AdvSimd.IsSupported)
			{
				Vector128<float> right = Vector128.Create(value2);
				AdvSimd.Store(&value1.M11, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M11), right));
				AdvSimd.Store(&value1.M21, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M21), right));
				AdvSimd.Store(&value1.M31, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M31), right));
				AdvSimd.Store(&value1.M41, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M41), right));
				return value1;
			}
			if (Sse.IsSupported)
			{
				Vector128<float> right2 = Vector128.Create(value2);
				Sse.Store(&value1.M11, Sse.Multiply(Sse.LoadVector128(&value1.M11), right2));
				Sse.Store(&value1.M21, Sse.Multiply(Sse.LoadVector128(&value1.M21), right2));
				Sse.Store(&value1.M31, Sse.Multiply(Sse.LoadVector128(&value1.M31), right2));
				Sse.Store(&value1.M41, Sse.Multiply(Sse.LoadVector128(&value1.M41), right2));
				return value1;
			}
			Matrix4x4 result;
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M13 = value1.M13 * value2;
			result.M14 = value1.M14 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M23 = value1.M23 * value2;
			result.M24 = value1.M24 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			result.M33 = value1.M33 * value2;
			result.M34 = value1.M34 * value2;
			result.M41 = value1.M41 * value2;
			result.M42 = value1.M42 * value2;
			result.M43 = value1.M43 * value2;
			result.M44 = value1.M44 * value2;
			return result;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x001068FC File Offset: 0x00105AFC
		public unsafe static bool operator ==(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return VectorMath.Equal(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) && VectorMath.Equal(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) && VectorMath.Equal(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) && VectorMath.Equal(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41));
			}
			if (Sse.IsSupported)
			{
				return VectorMath.Equal(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) && VectorMath.Equal(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) && VectorMath.Equal(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)) && VectorMath.Equal(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));
			}
			return value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00106B1C File Offset: 0x00105D1C
		public unsafe static bool operator !=(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) || VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) || VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) || VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41));
			}
			if (Sse.IsSupported)
			{
				return VectorMath.NotEqual(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) || VectorMath.NotEqual(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) || VectorMath.NotEqual(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)) || VectorMath.NotEqual(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));
			}
			return value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 || value1.M14 != value2.M14 || value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M23 != value2.M23 || value1.M24 != value2.M24 || value1.M31 != value2.M31 || value1.M32 != value2.M32 || value1.M33 != value2.M33 || value1.M34 != value2.M34 || value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43 || value1.M44 != value2.M44;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00106D41 File Offset: 0x00105F41
		public readonly bool Equals(Matrix4x4 other)
		{
			return this == other;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00106D50 File Offset: 0x00105F50
		[NullableContext(2)]
		public override readonly bool Equals(object obj)
		{
			if (obj is Matrix4x4)
			{
				Matrix4x4 value = (Matrix4x4)obj;
				return this == value;
			}
			return false;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00106D7C File Offset: 0x00105F7C
		[NullableContext(1)]
		public override readonly string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}", new object[]
			{
				this.M11,
				this.M12,
				this.M13,
				this.M14,
				this.M21,
				this.M22,
				this.M23,
				this.M24,
				this.M31,
				this.M32,
				this.M33,
				this.M34,
				this.M41,
				this.M42,
				this.M43,
				this.M44
			});
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00106E88 File Offset: 0x00106088
		public override readonly int GetHashCode()
		{
			HashCode hashCode = default(HashCode);
			hashCode.Add<float>(this.M11);
			hashCode.Add<float>(this.M12);
			hashCode.Add<float>(this.M13);
			hashCode.Add<float>(this.M14);
			hashCode.Add<float>(this.M21);
			hashCode.Add<float>(this.M22);
			hashCode.Add<float>(this.M23);
			hashCode.Add<float>(this.M24);
			hashCode.Add<float>(this.M31);
			hashCode.Add<float>(this.M32);
			hashCode.Add<float>(this.M33);
			hashCode.Add<float>(this.M34);
			hashCode.Add<float>(this.M41);
			hashCode.Add<float>(this.M42);
			hashCode.Add<float>(this.M43);
			hashCode.Add<float>(this.M44);
			return hashCode.ToHashCode();
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00106FDC File Offset: 0x001061DC
		[CompilerGenerated]
		internal unsafe static bool <Invert>g__SseImpl|59_0(Matrix4x4 matrix, out Matrix4x4 result)
		{
			if (!Sse.IsSupported)
			{
				throw new PlatformNotSupportedException();
			}
			Vector128<float> vector = Sse.LoadVector128(&matrix.M11);
			Vector128<float> vector2 = Sse.LoadVector128(&matrix.M21);
			Vector128<float> vector3 = Sse.LoadVector128(&matrix.M31);
			Vector128<float> vector4 = Sse.LoadVector128(&matrix.M41);
			Vector128<float> left = Sse.Shuffle(vector, vector2, 68);
			Vector128<float> left2 = Sse.Shuffle(vector, vector2, 238);
			Vector128<float> vector5 = Sse.Shuffle(vector3, vector4, 68);
			Vector128<float> right = Sse.Shuffle(vector3, vector4, 238);
			vector = Sse.Shuffle(left, vector5, 136);
			vector2 = Sse.Shuffle(left, vector5, 221);
			vector3 = Sse.Shuffle(left2, right, 136);
			vector4 = Sse.Shuffle(left2, right, 221);
			Vector128<float> vector6 = Matrix4x4.Permute(vector3, 80);
			Vector128<float> vector7 = Matrix4x4.Permute(vector4, 238);
			Vector128<float> vector8 = Matrix4x4.Permute(vector, 80);
			Vector128<float> vector9 = Matrix4x4.Permute(vector2, 238);
			Vector128<float> vector10 = Sse.Shuffle(vector3, vector, 136);
			Vector128<float> vector11 = Sse.Shuffle(vector4, vector2, 221);
			Vector128<float> vector12 = Sse.Multiply(vector6, vector7);
			Vector128<float> vector13 = Sse.Multiply(vector8, vector9);
			Vector128<float> vector14 = Sse.Multiply(vector10, vector11);
			vector6 = Matrix4x4.Permute(vector3, 238);
			vector7 = Matrix4x4.Permute(vector4, 80);
			vector8 = Matrix4x4.Permute(vector, 238);
			vector9 = Matrix4x4.Permute(vector2, 80);
			vector10 = Sse.Shuffle(vector3, vector, 221);
			vector11 = Sse.Shuffle(vector4, vector2, 136);
			vector12 = Sse.Subtract(vector12, Sse.Multiply(vector6, vector7));
			vector13 = Sse.Subtract(vector13, Sse.Multiply(vector8, vector9));
			vector14 = Sse.Subtract(vector14, Sse.Multiply(vector10, vector11));
			vector9 = Sse.Shuffle(vector12, vector14, 93);
			vector6 = Matrix4x4.Permute(vector2, 73);
			vector7 = Sse.Shuffle(vector9, vector12, 50);
			vector8 = Matrix4x4.Permute(vector, 18);
			vector9 = Sse.Shuffle(vector9, vector12, 153);
			Vector128<float> vector15 = Sse.Shuffle(vector13, vector14, 253);
			vector10 = Matrix4x4.Permute(vector4, 73);
			vector11 = Sse.Shuffle(vector15, vector13, 50);
			Vector128<float> vector16 = Matrix4x4.Permute(vector3, 18);
			vector15 = Sse.Shuffle(vector15, vector13, 153);
			Vector128<float> vector17 = Sse.Multiply(vector6, vector7);
			Vector128<float> vector18 = Sse.Multiply(vector8, vector9);
			Vector128<float> vector19 = Sse.Multiply(vector10, vector11);
			Vector128<float> vector20 = Sse.Multiply(vector16, vector15);
			vector9 = Sse.Shuffle(vector12, vector14, 4);
			vector6 = Matrix4x4.Permute(vector2, 158);
			vector7 = Sse.Shuffle(vector12, vector9, 147);
			vector8 = Matrix4x4.Permute(vector, 123);
			vector9 = Sse.Shuffle(vector12, vector9, 38);
			vector15 = Sse.Shuffle(vector13, vector14, 164);
			vector10 = Matrix4x4.Permute(vector4, 158);
			vector11 = Sse.Shuffle(vector13, vector15, 147);
			vector16 = Matrix4x4.Permute(vector3, 123);
			vector15 = Sse.Shuffle(vector13, vector15, 38);
			vector17 = Sse.Subtract(vector17, Sse.Multiply(vector6, vector7));
			vector18 = Sse.Subtract(vector18, Sse.Multiply(vector8, vector9));
			vector19 = Sse.Subtract(vector19, Sse.Multiply(vector10, vector11));
			vector20 = Sse.Subtract(vector20, Sse.Multiply(vector16, vector15));
			vector6 = Matrix4x4.Permute(vector2, 51);
			vector7 = Sse.Shuffle(vector12, vector14, 74);
			vector7 = Matrix4x4.Permute(vector7, 44);
			vector8 = Matrix4x4.Permute(vector, 141);
			vector9 = Sse.Shuffle(vector12, vector14, 76);
			vector9 = Matrix4x4.Permute(vector9, 147);
			vector10 = Matrix4x4.Permute(vector4, 51);
			vector11 = Sse.Shuffle(vector13, vector14, 234);
			vector11 = Matrix4x4.Permute(vector11, 44);
			vector16 = Matrix4x4.Permute(vector3, 141);
			vector15 = Sse.Shuffle(vector13, vector14, 236);
			vector15 = Matrix4x4.Permute(vector15, 147);
			vector6 = Sse.Multiply(vector6, vector7);
			vector8 = Sse.Multiply(vector8, vector9);
			vector10 = Sse.Multiply(vector10, vector11);
			vector16 = Sse.Multiply(vector16, vector15);
			Vector128<float> right2 = Sse.Subtract(vector17, vector6);
			vector17 = Sse.Add(vector17, vector6);
			Vector128<float> right3 = Sse.Add(vector18, vector8);
			vector18 = Sse.Subtract(vector18, vector8);
			Vector128<float> right4 = Sse.Subtract(vector19, vector10);
			vector19 = Sse.Add(vector19, vector10);
			Vector128<float> right5 = Sse.Add(vector20, vector16);
			vector20 = Sse.Subtract(vector20, vector16);
			vector17 = Sse.Shuffle(vector17, right2, 216);
			vector18 = Sse.Shuffle(vector18, right3, 216);
			vector19 = Sse.Shuffle(vector19, right4, 216);
			vector20 = Sse.Shuffle(vector20, right5, 216);
			vector17 = Matrix4x4.Permute(vector17, 216);
			vector18 = Matrix4x4.Permute(vector18, 216);
			vector19 = Matrix4x4.Permute(vector19, 216);
			vector20 = Matrix4x4.Permute(vector20, 216);
			vector5 = vector;
			float num = Vector4.Dot(vector17.AsVector4(), vector5.AsVector4());
			if (MathF.Abs(num) < 1E-45f)
			{
				result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			Vector128<float> left3 = Vector128.Create(1f);
			Vector128<float> right6 = Vector128.Create(num);
			right6 = Sse.Divide(left3, right6);
			vector = Sse.Multiply(vector17, right6);
			vector2 = Sse.Multiply(vector18, right6);
			vector3 = Sse.Multiply(vector19, right6);
			vector4 = Sse.Multiply(vector20, right6);
			Unsafe.SkipInit<Matrix4x4>(out result);
			ref Vector128<float> ptr = ref Unsafe.As<Matrix4x4, Vector128<float>>(ref result);
			ptr = vector;
			*Unsafe.Add<Vector128<float>>(ref ptr, 1) = vector2;
			*Unsafe.Add<Vector128<float>>(ref ptr, 2) = vector3;
			*Unsafe.Add<Vector128<float>>(ref ptr, 3) = vector4;
			return true;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x001075A0 File Offset: 0x001067A0
		[CompilerGenerated]
		internal static bool <Invert>g__SoftwareFallback|59_1(Matrix4x4 matrix, out Matrix4x4 result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			float num7 = m6 * num - m7 * num2 + m8 * num3;
			float num8 = -(m5 * num - m7 * num4 + m8 * num5);
			float num9 = m5 * num2 - m6 * num4 + m8 * num6;
			float num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
			float num11 = m * num7 + m2 * num8 + m3 * num9 + m4 * num10;
			if (MathF.Abs(num11) < 1E-45f)
			{
				result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num12 = 1f / num11;
			result.M11 = num7 * num12;
			result.M21 = num8 * num12;
			result.M31 = num9 * num12;
			result.M41 = num10 * num12;
			result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num12;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num12;
			result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num12;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num12;
			float num13 = m7 * m16 - m8 * m15;
			float num14 = m6 * m16 - m8 * m14;
			float num15 = m6 * m15 - m7 * m14;
			float num16 = m5 * m16 - m8 * m13;
			float num17 = m5 * m15 - m7 * m13;
			float num18 = m5 * m14 - m6 * m13;
			result.M13 = (m2 * num13 - m3 * num14 + m4 * num15) * num12;
			result.M23 = -(m * num13 - m3 * num16 + m4 * num17) * num12;
			result.M33 = (m * num14 - m2 * num16 + m4 * num18) * num12;
			result.M43 = -(m * num15 - m2 * num17 + m3 * num18) * num12;
			float num19 = m7 * m12 - m8 * m11;
			float num20 = m6 * m12 - m8 * m10;
			float num21 = m6 * m11 - m7 * m10;
			float num22 = m5 * m12 - m8 * m9;
			float num23 = m5 * m11 - m7 * m9;
			float num24 = m5 * m10 - m6 * m9;
			result.M14 = -(m2 * num19 - m3 * num20 + m4 * num21) * num12;
			result.M24 = (m * num19 - m3 * num22 + m4 * num23) * num12;
			result.M34 = -(m * num20 - m2 * num22 + m4 * num24) * num12;
			result.M44 = (m * num21 - m2 * num23 + m3 * num24) * num12;
			return true;
		}

		// Token: 0x0400060E RID: 1550
		public float M11;

		// Token: 0x0400060F RID: 1551
		public float M12;

		// Token: 0x04000610 RID: 1552
		public float M13;

		// Token: 0x04000611 RID: 1553
		public float M14;

		// Token: 0x04000612 RID: 1554
		public float M21;

		// Token: 0x04000613 RID: 1555
		public float M22;

		// Token: 0x04000614 RID: 1556
		public float M23;

		// Token: 0x04000615 RID: 1557
		public float M24;

		// Token: 0x04000616 RID: 1558
		public float M31;

		// Token: 0x04000617 RID: 1559
		public float M32;

		// Token: 0x04000618 RID: 1560
		public float M33;

		// Token: 0x04000619 RID: 1561
		public float M34;

		// Token: 0x0400061A RID: 1562
		public float M41;

		// Token: 0x0400061B RID: 1563
		public float M42;

		// Token: 0x0400061C RID: 1564
		public float M43;

		// Token: 0x0400061D RID: 1565
		public float M44;

		// Token: 0x0400061E RID: 1566
		private static readonly Matrix4x4 _identity = new Matrix4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		// Token: 0x020001CB RID: 459
		private struct CanonicalBasis
		{
			// Token: 0x0400061F RID: 1567
			public Vector3 Row0;

			// Token: 0x04000620 RID: 1568
			public Vector3 Row1;

			// Token: 0x04000621 RID: 1569
			public Vector3 Row2;
		}

		// Token: 0x020001CC RID: 460
		private struct VectorBasis
		{
			// Token: 0x04000622 RID: 1570
			public unsafe Vector3* Element0;

			// Token: 0x04000623 RID: 1571
			public unsafe Vector3* Element1;

			// Token: 0x04000624 RID: 1572
			public unsafe Vector3* Element2;
		}
	}
}
