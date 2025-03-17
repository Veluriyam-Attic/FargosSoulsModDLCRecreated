using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007DC RID: 2012
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x060060B1 RID: 24753 RVA: 0x001CE852 File Offset: 0x001CDA52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x001CE858 File Offset: 0x001CDA58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x001CE115 File Offset: 0x001CD315
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
