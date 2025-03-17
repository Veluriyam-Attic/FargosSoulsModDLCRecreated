using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003DF RID: 991
	[NullableContext(1)]
	[CLSCompliant(false)]
	public interface IFormatterConverter
	{
		// Token: 0x060031FF RID: 12799
		object Convert(object value, Type type);

		// Token: 0x06003200 RID: 12800
		object Convert(object value, TypeCode typeCode);

		// Token: 0x06003201 RID: 12801
		bool ToBoolean(object value);

		// Token: 0x06003202 RID: 12802
		char ToChar(object value);

		// Token: 0x06003203 RID: 12803
		sbyte ToSByte(object value);

		// Token: 0x06003204 RID: 12804
		byte ToByte(object value);

		// Token: 0x06003205 RID: 12805
		short ToInt16(object value);

		// Token: 0x06003206 RID: 12806
		ushort ToUInt16(object value);

		// Token: 0x06003207 RID: 12807
		int ToInt32(object value);

		// Token: 0x06003208 RID: 12808
		uint ToUInt32(object value);

		// Token: 0x06003209 RID: 12809
		long ToInt64(object value);

		// Token: 0x0600320A RID: 12810
		ulong ToUInt64(object value);

		// Token: 0x0600320B RID: 12811
		float ToSingle(object value);

		// Token: 0x0600320C RID: 12812
		double ToDouble(object value);

		// Token: 0x0600320D RID: 12813
		decimal ToDecimal(object value);

		// Token: 0x0600320E RID: 12814
		DateTime ToDateTime(object value);

		// Token: 0x0600320F RID: 12815
		[return: Nullable(2)]
		string ToString(object value);
	}
}
