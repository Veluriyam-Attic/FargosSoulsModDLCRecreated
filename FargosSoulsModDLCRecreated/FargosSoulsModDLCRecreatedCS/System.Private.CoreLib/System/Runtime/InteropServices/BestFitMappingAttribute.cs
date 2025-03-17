using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000466 RID: 1126
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x00178D10 File Offset: 0x00177F10
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this.BestFitMapping = BestFitMapping;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x00178D1F File Offset: 0x00177F1F
		public bool BestFitMapping { get; }

		// Token: 0x04000F03 RID: 3843
		public bool ThrowOnUnmappableChar;
	}
}
