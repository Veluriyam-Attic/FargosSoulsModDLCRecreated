using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x02000402 RID: 1026
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		// Token: 0x06003298 RID: 12952 RVA: 0x0016B800 File Offset: 0x0016AA00
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x0016B81D File Offset: 0x0016AA1D
		public string FrameworkName
		{
			get
			{
				return this._frameworkName;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x0016B825 File Offset: 0x0016AA25
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x0016B82D File Offset: 0x0016AA2D
		[Nullable(2)]
		public string FrameworkDisplayName
		{
			[NullableContext(2)]
			get
			{
				return this._frameworkDisplayName;
			}
			[NullableContext(2)]
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x04000E3B RID: 3643
		private readonly string _frameworkName;

		// Token: 0x04000E3C RID: 3644
		private string _frameworkDisplayName;
	}
}
