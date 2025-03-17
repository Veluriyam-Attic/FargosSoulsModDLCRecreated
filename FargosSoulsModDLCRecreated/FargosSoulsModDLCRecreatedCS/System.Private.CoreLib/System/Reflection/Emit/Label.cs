using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065F RID: 1631
	public readonly struct Label : IEquatable<Label>
	{
		// Token: 0x0600538F RID: 21391 RVA: 0x0019AF4C File Offset: 0x0019A14C
		internal Label(int label)
		{
			this.m_label = label;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0019AF55 File Offset: 0x0019A155
		internal int GetLabelValue()
		{
			return this.m_label;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0019AF55 File Offset: 0x0019A155
		public override int GetHashCode()
		{
			return this.m_label;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0019AF60 File Offset: 0x0019A160
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is Label)
			{
				Label obj2 = (Label)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0019AF85 File Offset: 0x0019A185
		public bool Equals(Label obj)
		{
			return obj.m_label == this.m_label;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0019AF95 File Offset: 0x0019A195
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0019AF9F File Offset: 0x0019A19F
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04001550 RID: 5456
		internal readonly int m_label;
	}
}
