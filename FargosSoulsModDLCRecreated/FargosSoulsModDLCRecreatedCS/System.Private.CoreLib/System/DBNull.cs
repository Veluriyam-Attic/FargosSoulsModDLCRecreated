using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E7 RID: 231
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private DBNull()
		{
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000CE60E File Offset: 0x000CD80E
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(SR.NotSupported_DBNullSerial);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000CE620 File Offset: 0x000CD820
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000CE629 File Offset: 0x000CD829
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000CE629 File Offset: 0x000CD829
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return string.Empty;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000CE630 File Offset: 0x000CD830
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000CE633 File Offset: 0x000CD833
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000CE633 File Offset: 0x000CD833
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000CE633 File Offset: 0x000CD833
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000CE633 File Offset: 0x000CD833
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000CE633 File Offset: 0x000CD833
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000CE633 File Offset: 0x000CD833
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000CE633 File Offset: 0x000CD833
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000CE633 File Offset: 0x000CD833
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000CE633 File Offset: 0x000CD833
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000CE633 File Offset: 0x000CD833
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000CE633 File Offset: 0x000CD833
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000CE633 File Offset: 0x000CD833
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000CE633 File Offset: 0x000CD833
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000CE633 File Offset: 0x000CD833
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.InvalidCast_FromDBNull);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000B5086 File Offset: 0x000B4286
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002CC RID: 716
		public static readonly DBNull Value = new DBNull();
	}
}
