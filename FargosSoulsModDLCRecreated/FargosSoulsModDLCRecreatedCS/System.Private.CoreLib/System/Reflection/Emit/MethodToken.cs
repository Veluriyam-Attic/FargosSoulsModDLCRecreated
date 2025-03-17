using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000660 RID: 1632
	public struct MethodToken
	{
		// Token: 0x06005396 RID: 21398 RVA: 0x0019AFAB File Offset: 0x0019A1AB
		internal MethodToken(int methodToken)
		{
			this.Token = methodToken;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x0019AFB4 File Offset: 0x0019A1B4
		public readonly int Token { get; }

		// Token: 0x06005398 RID: 21400 RVA: 0x0019AFBC File Offset: 0x0019A1BC
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0019AFC4 File Offset: 0x0019A1C4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is MethodToken)
			{
				MethodToken obj2 = (MethodToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0019AFE9 File Offset: 0x0019A1E9
		public bool Equals(MethodToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0019AFFA File Offset: 0x0019A1FA
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0019B004 File Offset: 0x0019A204
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001551 RID: 5457
		public static readonly MethodToken Empty;
	}
}
