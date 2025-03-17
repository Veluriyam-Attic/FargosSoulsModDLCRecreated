using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200066F RID: 1647
	public struct TypeToken
	{
		// Token: 0x060053E1 RID: 21473 RVA: 0x0019C952 File Offset: 0x0019BB52
		internal TypeToken(int typeToken)
		{
			this.Token = typeToken;
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x0019C95B File Offset: 0x0019BB5B
		public readonly int Token { get; }

		// Token: 0x060053E3 RID: 21475 RVA: 0x0019C963 File Offset: 0x0019BB63
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x0019C96C File Offset: 0x0019BB6C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is TypeToken)
			{
				TypeToken obj2 = (TypeToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x0019C991 File Offset: 0x0019BB91
		public bool Equals(TypeToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0019C9A2 File Offset: 0x0019BBA2
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0019C9AC File Offset: 0x0019BBAC
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001774 RID: 6004
		public static readonly TypeToken Empty;
	}
}
