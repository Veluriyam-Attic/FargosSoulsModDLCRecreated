using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200012A RID: 298
	[NullableContext(2)]
	[CLSCompliant(false)]
	public interface IConvertible
	{
		// Token: 0x06000F58 RID: 3928
		TypeCode GetTypeCode();

		// Token: 0x06000F59 RID: 3929
		bool ToBoolean(IFormatProvider provider);

		// Token: 0x06000F5A RID: 3930
		char ToChar(IFormatProvider provider);

		// Token: 0x06000F5B RID: 3931
		sbyte ToSByte(IFormatProvider provider);

		// Token: 0x06000F5C RID: 3932
		byte ToByte(IFormatProvider provider);

		// Token: 0x06000F5D RID: 3933
		short ToInt16(IFormatProvider provider);

		// Token: 0x06000F5E RID: 3934
		ushort ToUInt16(IFormatProvider provider);

		// Token: 0x06000F5F RID: 3935
		int ToInt32(IFormatProvider provider);

		// Token: 0x06000F60 RID: 3936
		uint ToUInt32(IFormatProvider provider);

		// Token: 0x06000F61 RID: 3937
		long ToInt64(IFormatProvider provider);

		// Token: 0x06000F62 RID: 3938
		ulong ToUInt64(IFormatProvider provider);

		// Token: 0x06000F63 RID: 3939
		float ToSingle(IFormatProvider provider);

		// Token: 0x06000F64 RID: 3940
		double ToDouble(IFormatProvider provider);

		// Token: 0x06000F65 RID: 3941
		decimal ToDecimal(IFormatProvider provider);

		// Token: 0x06000F66 RID: 3942
		DateTime ToDateTime(IFormatProvider provider);

		// Token: 0x06000F67 RID: 3943
		[NullableContext(1)]
		string ToString([Nullable(2)] IFormatProvider provider);

		// Token: 0x06000F68 RID: 3944
		[NullableContext(1)]
		object ToType(Type conversionType, [Nullable(2)] IFormatProvider provider);
	}
}
