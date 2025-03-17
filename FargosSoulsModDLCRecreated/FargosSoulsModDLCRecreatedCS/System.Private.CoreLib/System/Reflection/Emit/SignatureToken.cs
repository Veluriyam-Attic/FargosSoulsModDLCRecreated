using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200066A RID: 1642
	public struct SignatureToken
	{
		// Token: 0x060053BE RID: 21438 RVA: 0x0019C2C8 File Offset: 0x0019B4C8
		internal SignatureToken(int signatureToken)
		{
			this.Token = signatureToken;
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x0019C2D1 File Offset: 0x0019B4D1
		public readonly int Token { get; }

		// Token: 0x060053C0 RID: 21440 RVA: 0x0019C2D9 File Offset: 0x0019B4D9
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0019C2E4 File Offset: 0x0019B4E4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SignatureToken)
			{
				SignatureToken obj2 = (SignatureToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0019C309 File Offset: 0x0019B509
		public bool Equals(SignatureToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0019C31A File Offset: 0x0019B51A
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0019C324 File Offset: 0x0019B524
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001747 RID: 5959
		public static readonly SignatureToken Empty;
	}
}
