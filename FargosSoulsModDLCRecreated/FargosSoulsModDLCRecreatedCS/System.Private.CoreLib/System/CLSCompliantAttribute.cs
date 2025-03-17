using System;

namespace System
{
	// Token: 0x020000DC RID: 220
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	public sealed class CLSCompliantAttribute : Attribute
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x000CAD3C File Offset: 0x000C9F3C
		public CLSCompliantAttribute(bool isCompliant)
		{
			this._compliant = isCompliant;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000CAD4B File Offset: 0x000C9F4B
		public bool IsCompliant
		{
			get
			{
				return this._compliant;
			}
		}

		// Token: 0x040002AF RID: 687
		private readonly bool _compliant;
	}
}
