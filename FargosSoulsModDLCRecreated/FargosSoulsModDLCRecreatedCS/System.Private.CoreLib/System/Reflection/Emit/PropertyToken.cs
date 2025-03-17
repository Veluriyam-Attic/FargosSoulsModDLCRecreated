using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000669 RID: 1641
	public struct PropertyToken
	{
		// Token: 0x060053B7 RID: 21431 RVA: 0x0019C260 File Offset: 0x0019B460
		internal PropertyToken(int propertyToken)
		{
			this.Token = propertyToken;
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x0019C269 File Offset: 0x0019B469
		public readonly int Token { get; }

		// Token: 0x060053B9 RID: 21433 RVA: 0x0019C271 File Offset: 0x0019B471
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0019C27C File Offset: 0x0019B47C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is PropertyToken)
			{
				PropertyToken obj2 = (PropertyToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0019C2A1 File Offset: 0x0019B4A1
		public bool Equals(PropertyToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0019C2B2 File Offset: 0x0019B4B2
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0019C2BC File Offset: 0x0019B4BC
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001745 RID: 5957
		public static readonly PropertyToken Empty;
	}
}
