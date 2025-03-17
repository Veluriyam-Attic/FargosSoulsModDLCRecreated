using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005E3 RID: 1507
	[NullableContext(1)]
	[Nullable(0)]
	public class LocalVariableInfo
	{
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004C6D RID: 19565 RVA: 0x000C26FD File Offset: 0x000C18FD
		public virtual Type LocalType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004C6E RID: 19566 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int LocalIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected LocalVariableInfo()
		{
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0018BC0C File Offset: 0x0018AE0C
		public override string ToString()
		{
			string text = this.LocalType.ToString() + " (" + this.LocalIndex.ToString() + ")";
			if (this.IsPinned)
			{
				text += " (pinned)";
			}
			return text;
		}
	}
}
