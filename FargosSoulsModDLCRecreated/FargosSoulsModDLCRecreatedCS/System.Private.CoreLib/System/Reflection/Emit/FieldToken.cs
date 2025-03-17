using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065D RID: 1629
	public struct FieldToken
	{
		// Token: 0x06005388 RID: 21384 RVA: 0x0019AED0 File Offset: 0x0019A0D0
		internal FieldToken(int fieldToken, Type fieldClass)
		{
			this.Token = fieldToken;
			this._class = fieldClass;
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x0019AEE0 File Offset: 0x0019A0E0
		public readonly int Token { get; }

		// Token: 0x0600538A RID: 21386 RVA: 0x0019AEE8 File Offset: 0x0019A0E8
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0019AEF0 File Offset: 0x0019A0F0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is FieldToken)
			{
				FieldToken obj2 = (FieldToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0019AF15 File Offset: 0x0019A115
		public bool Equals(FieldToken obj)
		{
			return obj.Token == this.Token && obj._class == this._class;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0019AF36 File Offset: 0x0019A136
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0019AF40 File Offset: 0x0019A140
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001543 RID: 5443
		public static readonly FieldToken Empty;

		// Token: 0x04001544 RID: 5444
		private readonly object _class;
	}
}
