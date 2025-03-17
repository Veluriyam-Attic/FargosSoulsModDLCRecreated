using System;

namespace System
{
	// Token: 0x020000EF RID: 239
	internal sealed class Empty
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private Empty()
		{
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000CE629 File Offset: 0x000CD829
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x040002E8 RID: 744
		public static readonly Empty Value = new Empty();
	}
}
