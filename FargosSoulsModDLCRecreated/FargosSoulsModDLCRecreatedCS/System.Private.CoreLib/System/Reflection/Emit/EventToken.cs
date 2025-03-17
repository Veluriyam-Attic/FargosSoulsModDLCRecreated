using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065C RID: 1628
	public struct EventToken
	{
		// Token: 0x06005381 RID: 21377 RVA: 0x0019AE6B File Offset: 0x0019A06B
		internal EventToken(int eventToken)
		{
			this.Token = eventToken;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005382 RID: 21378 RVA: 0x0019AE74 File Offset: 0x0019A074
		public readonly int Token { get; }

		// Token: 0x06005383 RID: 21379 RVA: 0x0019AE7C File Offset: 0x0019A07C
		public override int GetHashCode()
		{
			return this.Token;
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0019AE84 File Offset: 0x0019A084
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is EventToken)
			{
				EventToken obj2 = (EventToken)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0019AEA9 File Offset: 0x0019A0A9
		public bool Equals(EventToken obj)
		{
			return obj.Token == this.Token;
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0019AEBA File Offset: 0x0019A0BA
		public static bool operator ==(EventToken a, EventToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0019AEC4 File Offset: 0x0019A0C4
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001541 RID: 5441
		public static readonly EventToken Empty;
	}
}
