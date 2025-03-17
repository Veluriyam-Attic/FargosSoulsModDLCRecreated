using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200064B RID: 1611
	internal sealed class InternalModuleBuilder : RuntimeModule
	{
		// Token: 0x06005130 RID: 20784 RVA: 0x00194B26 File Offset: 0x00193D26
		private InternalModuleBuilder()
		{
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00194B2E File Offset: 0x00193D2E
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalModuleBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00194B49 File Offset: 0x00193D49
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
