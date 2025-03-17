using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020007DD RID: 2013
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct, Enum
	{
		// Token: 0x060060B6 RID: 24758 RVA: 0x001CE869 File Offset: 0x001CDA69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(T x, T y)
		{
			return RuntimeHelpers.EnumEquals<T>(x, y);
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x001CE874 File Offset: 0x001CDA74
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (RuntimeHelpers.EnumEquals<T>(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x001CE8A4 File Offset: 0x001CDAA4
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (RuntimeHelpers.EnumEquals<T>(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x001CE5E9 File Offset: 0x001CD7E9
		internal EnumEqualityComparer()
		{
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x001CE5E9 File Offset: 0x001CD7E9
		private EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x001CE8D6 File Offset: 0x001CDAD6
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x001CE900 File Offset: 0x001CDB00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x001CE115 File Offset: 0x001CD315
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
