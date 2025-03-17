using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000667 RID: 1639
	public struct ParameterToken
	{
		// Token: 0x060053B0 RID: 21424 RVA: 0x0019C1F9 File Offset: 0x0019B3F9
		internal ParameterToken(int parameterToken)
		{
			this.Token = parameterToken;
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0019C202 File Offset: 0x0019B402
		public readonly int Token { get; }

		// Token: 0x060053B2 RID: 21426 RVA: 0x0019C20A File Offset: 0x0019B40A
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0019C214 File Offset: 0x0019B414
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is ParameterToken)
			{
				ParameterToken obj2 = (ParameterToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0019C239 File Offset: 0x0019B439
		public bool Equals(ParameterToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0019C24A File Offset: 0x0019B44A
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0019C254 File Offset: 0x0019B454
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !(a == b);
		}

		// Token: 0x0400173F RID: 5951
		public static readonly ParameterToken Empty;
	}
}
