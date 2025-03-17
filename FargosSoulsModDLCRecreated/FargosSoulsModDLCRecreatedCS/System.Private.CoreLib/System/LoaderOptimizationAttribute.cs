using System;

namespace System
{
	// Token: 0x02000146 RID: 326
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x000DB6D1 File Offset: 0x000DA8D1
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000DB6E0 File Offset: 0x000DA8E0
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000DB6F0 File Offset: 0x000DA8F0
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x04000413 RID: 1043
		private readonly byte _val;
	}
}
