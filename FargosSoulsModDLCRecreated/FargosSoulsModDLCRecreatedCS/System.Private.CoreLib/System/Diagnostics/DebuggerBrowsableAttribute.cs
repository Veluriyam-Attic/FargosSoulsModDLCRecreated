using System;

namespace System.Diagnostics
{
	// Token: 0x020006D2 RID: 1746
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		// Token: 0x060058AD RID: 22701 RVA: 0x001B0E1B File Offset: 0x001B001B
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.State = state;
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x001B0E3D File Offset: 0x001B003D
		public DebuggerBrowsableState State { get; }
	}
}
