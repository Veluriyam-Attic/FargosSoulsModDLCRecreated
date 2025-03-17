using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000584 RID: 1412
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		// Token: 0x06004879 RID: 18553 RVA: 0x00181F24 File Offset: 0x00181124
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x00181F41 File Offset: 0x00181141
		public string Version { get; }
	}
}
