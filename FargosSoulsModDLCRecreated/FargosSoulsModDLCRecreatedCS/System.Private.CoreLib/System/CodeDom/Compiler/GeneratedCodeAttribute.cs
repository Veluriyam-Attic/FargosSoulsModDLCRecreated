using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000241 RID: 577
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class GeneratedCodeAttribute : Attribute
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x00138BC1 File Offset: 0x00137DC1
		public GeneratedCodeAttribute(string tool, string version)
		{
			this._tool = tool;
			this._version = version;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00138BD7 File Offset: 0x00137DD7
		public string Tool
		{
			get
			{
				return this._tool;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x00138BDF File Offset: 0x00137DDF
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0400096A RID: 2410
		private readonly string _tool;

		// Token: 0x0400096B RID: 2411
		private readonly string _version;
	}
}
