using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200066C RID: 1644
	public struct StringToken
	{
		// Token: 0x060053C5 RID: 21445 RVA: 0x0019C330 File Offset: 0x0019B530
		internal StringToken(int str)
		{
			this.Token = str;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060053C6 RID: 21446 RVA: 0x0019C339 File Offset: 0x0019B539
		public readonly int Token { get; }

		// Token: 0x060053C7 RID: 21447 RVA: 0x0019C341 File Offset: 0x0019B541
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0019C34C File Offset: 0x0019B54C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is StringToken)
			{
				StringToken obj2 = (StringToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0019C371 File Offset: 0x0019B571
		public bool Equals(StringToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0019C382 File Offset: 0x0019B582
		public static bool operator ==(StringToken a, StringToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0019C38C File Offset: 0x0019B58C
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !(a == b);
		}
	}
}
