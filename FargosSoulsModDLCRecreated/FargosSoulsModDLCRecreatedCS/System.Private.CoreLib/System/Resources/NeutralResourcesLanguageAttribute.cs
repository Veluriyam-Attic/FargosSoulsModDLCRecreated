using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000576 RID: 1398
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		// Token: 0x060047EA RID: 18410 RVA: 0x0017EEE5 File Offset: 0x0017E0E5
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this.CultureName = cultureName;
			this.Location = 0;
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0017EF0C File Offset: 0x0017E10C
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(SR.Format(SR.Arg_InvalidNeutralResourcesLanguage_FallbackLoc, location));
			}
			this.CultureName = cultureName;
			this.Location = location;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x0017EF68 File Offset: 0x0017E168
		public string CultureName { get; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0017EF70 File Offset: 0x0017E170
		public UltimateResourceFallbackLocation Location { get; }
	}
}
