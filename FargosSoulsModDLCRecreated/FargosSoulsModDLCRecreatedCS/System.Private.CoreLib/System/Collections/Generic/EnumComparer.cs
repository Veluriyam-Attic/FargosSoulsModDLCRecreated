using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020007D6 RID: 2006
	[Serializable]
	internal sealed class EnumComparer<T> : Comparer<T>, ISerializable where T : struct, Enum
	{
		// Token: 0x06006087 RID: 24711 RVA: 0x001CE104 File Offset: 0x001CD304
		public override int Compare(T x, T y)
		{
			return RuntimeHelpers.EnumCompareTo<T>(x, y);
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001CE10D File Offset: 0x001CD30D
		internal EnumComparer()
		{
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x001CE10D File Offset: 0x001CD30D
		private EnumComparer(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x001CE115 File Offset: 0x001CD315
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x001CE13A File Offset: 0x001CD33A
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectComparer<T>));
		}
	}
}
